using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp;

/// <inheritdoc/>
unsafe partial class GraphicsDevice
{
#if !NET6_0_OR_GREATER
    /// <summary>
    /// A cached <see cref="WaitForSingleObjectCallbackDelegate"/> instance wrapping <see cref="WaitForSingleObjectCallbackForRegisterDeviceLostCallback(void*, byte)"/>.
    /// </summary>
    private static readonly WaitForSingleObjectCallbackDelegate WaitForSingleObjectCallbackForRegisterDeviceLostCallbackWrapper = WaitForSingleObjectCallbackForRegisterDeviceLostCallback;
#endif

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if the current device has been lost.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the current device has been lost.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void ThrowIfDeviceLost()
    {
        // This method is called as a check before performing any operations. In order to minimize overhead,
        // the GetDeviceRemovedReason() method is not called here, and the local field is just used instead.
        // That field is set by the device lost callback anyway, and the extra precision of calling the API
        // explicitly is not needed since there are no thread safety guarantees in this scenario anyway.
        // That is, the check isn't meant to cover 100% of cases, as a device might be lost right after this
        // check is performed anyway. This check is only meant to catch the common scenarios to help users.
        // For the same reason, this method doesn't have to worry about raising the event either.
        if (Volatile.Read(ref Unsafe.As<HRESULT, int>(ref this.deviceRemovedReason)) != S.S_OK)
        {
            static void Throw(object self) => throw new InvalidOperationException($"The device \"{self}\" has been lost and can no longer be used.");

            Throw(this);
        }
    }

    /// <summary>
    /// Raises the <see cref="DeviceLost"/> event if needed.
    /// </summary>
    private void QueueRaiseDeviceLostEventIfNeeded()
    {
        if (IsDisposed)
        {
            return;
        }

        // If the current device removed reason is not S_OK, it means the event
        // has already been raised (but the device will keep reporting that reason).
        // In that case, do nothing to avoid raising it again while the device is alive.
        if (Volatile.Read(ref Unsafe.As<HRESULT, int>(ref this.deviceRemovedReason)) != S.S_OK)
        {
            return;
        }

        HRESULT result = this.d3D12Device.Get()->GetDeviceRemovedReason();

        // Only raise the event once, and store the device removed reason to track it
        if (result != S.S_OK &&
            Interlocked.CompareExchange(
            ref Unsafe.As<HRESULT, int>(ref this.deviceRemovedReason),
            (int)result,
            S.S_OK) == S.S_OK)
        {
            QueueRaiseDeviceLostEvent();
        }
    }

    /// <summary>
    /// Queues the <see cref="DeviceLost"/> event being raised on the thread pool.
    /// </summary>
    private void QueueRaiseDeviceLostEvent()
    {
        static void RaiseDeviceLostEvent(GraphicsDevice device)
        {
            HRESULT result = Volatile.Read(ref Unsafe.As<HRESULT, int>(ref device.deviceRemovedReason));

            DeviceLostReason reason = (int)result switch
            {
                DXGI.DXGI_ERROR_DEVICE_HUNG => DeviceLostReason.DeviceHung,
                DXGI.DXGI_ERROR_DEVICE_REMOVED => DeviceLostReason.DeviceRemoved,
                DXGI.DXGI_ERROR_DEVICE_RESET => DeviceLostReason.DeviceReset,
                DXGI.DXGI_ERROR_DRIVER_INTERNAL_ERROR => DeviceLostReason.DriverInternalError,
                DXGI.DXGI_ERROR_INVALID_CALL => DeviceLostReason.InvalidCall,
                DXGI.DXGI_ERROR_ACCESS_DENIED => DeviceLostReason.AccessDenied,
                _ => DeviceLostReason.UnspecifiedError
            };

            device.DeviceLost?.Invoke(device, reason);
        }

        ThreadPool.QueueUserWorkItem(static state => RaiseDeviceLostEvent((GraphicsDevice)state!), this);
    }

    /// <summary>
    /// Registers a callback to notify whenever the current device is lost.
    /// </summary>
    /// <param name="device">The current <see cref="GraphicsDevice"/> instance.</param>
    /// <param name="deviceHandle">The resulting <see cref="GCHandle"/> used as callback state.</param>
    /// <param name="deviceRemovedEvent">The resulting device lost event for the callback.</param>
    /// <param name="deviceRemovedWaitHandle">The resulting device lost wait handle for the callback.</param>
    private static void RegisterDeviceLostCallback(
        GraphicsDevice device,
        out GCHandle deviceHandle,
        out HANDLE deviceRemovedEvent,
        out HANDLE deviceRemovedWaitHandle)
    {
        HANDLE eventHandle = Windows.CreateEventW(null, Windows.FALSE, Windows.FALSE, null);

        // Whenever a device is lost, for any reason, all fences are signaled to the highest value.
        // As such, a fence event with a value of ulong.MaxValue can act as a device lost callback.
        device.d3D12ComputeFence.Get()->SetEventOnCompletion(ulong.MaxValue, eventHandle).Assert();

        GCHandle handle = GCHandle.Alloc(device, GCHandleType.Weak);
        HANDLE waitHandle;

        _ = Windows.RegisterWaitForSingleObject(
            phNewWaitObject: &waitHandle,
            hObject: eventHandle,
#if NET6_0_OR_GREATER
            Callback: &WaitForSingleObjectCallbackForRegisterDeviceLostCallback,
#else
            Callback: (void*)Marshal.GetFunctionPointerForDelegate(WaitForSingleObjectCallbackForRegisterDeviceLostCallbackWrapper),
#endif
            Context: (void*)GCHandle.ToIntPtr(handle),
            dwMilliseconds: Windows.INFINITE,
            dwFlags: 0);

        deviceHandle = handle;
        deviceRemovedEvent = eventHandle;
        deviceRemovedWaitHandle = waitHandle;
    }

    /// <summary>
    /// Unregisters a callback setup by <see cref="RegisterDeviceLostCallback"/>.
    /// </summary>
    /// <param name="device">The current <see cref="GraphicsDevice"/> instance.</param>
    private static void UnregisterDeviceLostCallback(GraphicsDevice device)
    {
        // As per the UnregisterWait docs:
        // "If any callback functions associated with the timer have not completed when UnregisterWait is called,
        // UnregisterWait unregisters the wait on the callback functions and fails with the ERROR_IO_PENDING error code."
        // To ensure the handle is always correctly disposed, there are three scenarios to take into account:
        //   1) If the callback is executed first and has completed, this call will return S_OK. No additional cleanup
        //      is needed, as the wait callback will have already freed the handle. In that case, do nothing.
        //   2) If the callback is pending, this call will return ERROR_IO_PENDING. If that happens, no additional work
        //      is needed in this case either, as the callback will take care of freeing the handle during execution.
        //   3) If the callback has never been invoked, the handle can safely be freed.
        // In all cases, the OS will take care of checking against race conditions, so no interlocked APIs are needed.
        if (Windows.UnregisterWait(device.deviceRemovedWaitHandle) == S.S_OK &&
            device.deviceHandle.IsAllocated)
        {
            device.deviceHandle.Free();
        }

        _ = Windows.CloseHandle(device.deviceRemovedEvent);
    }

    /// <summary>
    /// The callback to signal a device lost event.
    /// </summary>
    /// <param name="pContext">The input context.</param>
    /// <param name="timedOut">Whether the wait has timed out.</param>
    [UnmanagedCallersOnly]
    private static void WaitForSingleObjectCallbackForRegisterDeviceLostCallback(void* pContext, byte timedOut)
    {
        GCHandle handle = GCHandle.FromIntPtr((IntPtr)pContext);
        GraphicsDevice? device = Unsafe.As<GraphicsDevice>(handle.Target);

        // Since the GCHandle is weak, it's possible that if the callback races against the finalizer thread,
        // the call to UnregisterDeviceLostCallback might not be able to unregister the wait before the object
        // is collected, which causes the GCHandle to return null. To guard against this, it is crucial to
        // free the handle from the input context, and not by trying to get the one stored in the field on
        // the target object, as that might just be null. The handle is guaranteed to be allocated when the
        // callback is executed. Freeing it from here is needed so that the cleanup upon disposal might skip
        // it if there's a pending execution. That ensures the handle is always disposed, without the disposal
        // path having to lock and wait for it.
        handle.Free();

        // If the device is available, then also queue the device lost event to be raised on the thread pool
        if (device is not null)
        {
            device.QueueRaiseDeviceLostEventIfNeeded();
        }
    }
}
