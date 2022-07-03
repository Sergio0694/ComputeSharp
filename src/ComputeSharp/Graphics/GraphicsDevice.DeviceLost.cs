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
    private static readonly WaitForSingleObjectCallbackDelegate WaitForSingleObjectCallbackForRegisterDeviceLostCallbackWrapper = WaitForSingleObjectCallbackForWaitForFenceAsync;
#endif

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
    private static void RegisterDeviceLostCallback(GraphicsDevice device, out GCHandle deviceHandle, out HANDLE deviceRemovedEvent)
    {
        HANDLE eventHandle = Windows.CreateEventW(null, Windows.FALSE, Windows.FALSE, null);

        // Whenever a device is lost, for any reason, all fences are signaled to the highest value.
        // As such, a fence event with a value of ulong.MaxValue can act as a device lost callback.
        device.d3D12ComputeFence.Get()->SetEventOnCompletion(ulong.MaxValue, eventHandle).Assert();

        GCHandle handle = deviceHandle = GCHandle.Alloc(device, GCHandleType.Weak);
        HANDLE waitHandle;

        _ = Windows.RegisterWaitForSingleObject(
            phNewWaitObject: &waitHandle,
            hObject: eventHandle,
#if NET6_0_OR_GREATER
            Callback: &WaitForSingleObjectCallbackForRegisterDeviceLostCallback,
#else
            Callback: (void*)Marshal.GetFunctionPointerForDelegate(WaitForSingleObjectCallbackForRegisterDeviceLostCallbackWrapper),
#endif
            Context: (void*)(IntPtr)handle,
            dwMilliseconds: Windows.INFINITE,
            dwFlags: 0);

        deviceRemovedEvent = waitHandle;
    }

    /// <summary>
    /// Unregisters a callback setup by <see cref="RegisterDeviceLostCallback"/>.
    /// </summary>
    /// <param name="device">The current <see cref="GraphicsDevice"/> instance.</param>
    private static void UnregisterDeviceLostCallback(GraphicsDevice device)
    {
        _ = Windows.UnregisterWait(device.deviceRemovedEvent);
        _ = Windows.CloseHandle(device.deviceRemovedEvent);

        device.deviceHandle.Free();
    }

    /// <summary>
    /// The callback to signal a device lost event.
    /// </summary>
    /// <param name="pContext">The input context.</param>
    /// <param name="timedOut">Whether the wait has timed out.</param>
    [UnmanagedCallersOnly]
    private static void WaitForSingleObjectCallbackForRegisterDeviceLostCallback(void* pContext, byte timedOut)
    {
        GraphicsDevice device = Unsafe.As<GraphicsDevice>(GCHandle.FromIntPtr((IntPtr)pContext).Target!);

        device.QueueRaiseDeviceLostEventIfNeeded();
    }
}
