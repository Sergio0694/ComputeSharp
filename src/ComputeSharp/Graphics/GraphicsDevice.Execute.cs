using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Commands.Interop;
using ComputeSharp.Interop;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;
#if !NET6_0_OR_GREATER
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp;

/// <inheritdoc/>
unsafe partial class GraphicsDevice : NativeObject
{
#if !NET6_0_OR_GREATER
    /// <summary>
    /// A delegate for a callback to pass to <see cref="Windows.RegisterWaitForSingleObject(HANDLE*, HANDLE, void*, void*, uint, uint)"/>.
    /// </summary>
    /// <param name="pContext">The input context.</param>
    /// <param name="timedOut">Whether the wait has timed out.</param>
    private delegate void WaitForSingleObjectCallbackDelegate(void* pContext, byte timedOut);

    /// <summary>
    /// A cached <see cref="WaitForSingleObjectCallbackDelegate"/> instance wrapping <see cref="WaitForSingleObjectCallback(void*, byte)"/>.
    /// </summary>
    private static readonly WaitForSingleObjectCallbackDelegate WaitForSingleObjectCallbackWrapper = WaitForSingleObjectCallback;
#endif

    /// <summary>
    /// Executes a given command list and waits for the operation to be completed.
    /// </summary>
    /// <param name="commandList">The input <see cref="CommandList"/> to execute.</param>
    internal void ExecuteCommandList(ref CommandList commandList)
    {
        ref readonly ID3D12CommandListPool commandListPool = ref Unsafe.NullRef<ID3D12CommandListPool>();
        ID3D12CommandQueue* d3D12CommandQueue;
        ID3D12Fence* d3D12Fence;
        ref ulong d3D12FenceValue = ref Unsafe.NullRef<ulong>();

        // Get the target command queue, fence and pool for the list type
        switch (commandList.D3D12CommandListType)
        {
            case D3D12_COMMAND_LIST_TYPE_COMPUTE:
                commandListPool = ref this.computeCommandListPool;
                d3D12CommandQueue = this.d3D12ComputeCommandQueue;
                d3D12Fence = this.d3D12ComputeFence;
                d3D12FenceValue = ref this.nextD3D12ComputeFenceValue;
                break;
            case D3D12_COMMAND_LIST_TYPE_COPY:
                commandListPool = ref this.copyCommandListPool;
                d3D12CommandQueue = this.d3D12CopyCommandQueue;
                d3D12Fence = this.d3D12CopyFence;
                d3D12FenceValue = ref this.nextD3D12CopyFenceValue;
                break;
            default: ThrowHelper.ThrowArgumentException(); return;
        }

        // Execute the command list
        d3D12CommandQueue->ExecuteCommandLists(1, commandList.GetD3D12CommandListAddressOf());

#if NET6_0_OR_GREATER
        ulong updatedFenceValue = Interlocked.Increment(ref d3D12FenceValue);
#else
        ulong updatedFenceValue = (ulong)Interlocked.Increment(ref Unsafe.As<ulong, long>(ref d3D12FenceValue));
#endif

        // Signal to the target fence with the updated value. Note that incrementing the
        // target fence value to signal must be done after executing the command list.
        d3D12CommandQueue->Signal(d3D12Fence, updatedFenceValue).Assert();

        // If the fence value hasn't been reached, wait until the operation completes
        if (updatedFenceValue > d3D12Fence->GetCompletedValue())
        {
            d3D12Fence->SetEventOnCompletion(updatedFenceValue, default).Assert();
        }

        // Return the rented command list and command allocator so that they can be reused
        commandListPool.Return(commandList.DetachD3D12CommandList(), commandList.DetachD3D12CommandAllocator());
    }

    /// <summary>
    /// Executes a given command list and returns an awaitable for the operation to be completed.
    /// </summary>
    /// <param name="commandList">The input <see cref="ValueTask"/> to execute.</param>
    /// <returns>The <see cref="ValueTask"/> object representing the operation to wait for.</returns>
    /// <remarks>This method is only supported for compute operations.</remarks>
    internal ValueTask ExecuteCommandListAsync(ref CommandList commandList)
    {
        // Execute the command list
        this.d3D12ComputeCommandQueue.Get()->ExecuteCommandLists(1, commandList.GetD3D12CommandListAddressOf());

#if NET6_0_OR_GREATER
        ulong updatedFenceValue = Interlocked.Increment(ref this.nextD3D12ComputeFenceValue);
#else
        ulong updatedFenceValue = (ulong)Interlocked.Increment(ref Unsafe.As<ulong, long>(ref this.nextD3D12ComputeFenceValue));
#endif

        // Signal to the target fence with the updated value. Note that incrementing the
        // target fence value to signal must be done after executing the command list.
        this.d3D12ComputeCommandQueue.Get()->Signal(this.d3D12ComputeFence.Get(), updatedFenceValue).Assert();

        // If the fence value has been reached, complete synchronously
        if (updatedFenceValue <= this.d3D12ComputeFence.Get()->GetCompletedValue())
        {
            // Return the rented command list and command allocator so that they can be reused
            this.computeCommandListPool.Return(commandList.DetachD3D12CommandList(), commandList.DetachD3D12CommandAllocator());

#if NET6_0_OR_GREATER
            return ValueTask.CompletedTask;
#else
            return new(Task.CompletedTask);
#endif
        }

        // Setup the awaitable task
        Task computeTask = WaitForFenceAsync(
            updatedFenceValue,
            this,
            commandList.DetachD3D12CommandList(),
            commandList.DetachD3D12CommandAllocator());

        return new(computeTask);
    }

    /// <summary>
    /// A context to support <see cref="WaitForSingleObjectCallback"/>.
    /// </summary>
    private struct CallbackContext
    {
        /// <summary>
        /// The <see cref="GCHandle"/> wrapping the <see cref="TaskCompletionSource"/> object to signal completion.
        /// </summary>
        public GCHandle TaskCompletionSourceHandle;

        /// <summary>
        /// The <see cref="GCHandle"/> wrapping the <see cref="GraphicsDevice"/> instance in use.
        /// </summary>
        public GCHandle GraphicsDeviceHandle;

        /// <summary>
        /// The <see cref="ID3D12GraphicsCommandList"/> used to queue the operations.
        /// </summary>
        public ID3D12GraphicsCommandList* D3D12GraphicsCommandList;

        /// <summary>
        /// The <see cref="ID3D12GraphicsCommandList"/> object backing <see name="D3D12GraphicsCommandList"/>.
        /// </summary>
        public ID3D12CommandAllocator* D3D12CommandAllocator;

        /// <summary>
        /// The event handle set when the target fence value is reached.
        /// </summary>
        public HANDLE EventHandle;
    }

    /// <summary>
    /// Asynchronously waits for a target fence value to be completed.
    /// </summary>
    /// <param name="d3D12FenceValue">The target fence value to wait for.</param>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance executing the operations.</param>
    /// <param name="d3D12GraphicsCommandList">The <see cref="ID3D12GraphicsCommandList"/> used to queue the operations.</param>
    /// <param name="d3D12CommandAllocator">The <see cref="ID3D12GraphicsCommandList"/> object backing <paramref name="d3D12GraphicsCommandList"/>.</param>
    /// <returns>The <see cref="ValueTask"/> object representing the operation to wait for.</returns>
    /// <remarks>This method is only supported for compute operations.</remarks>
    private static Task WaitForFenceAsync(
        ulong d3D12FenceValue,
        GraphicsDevice device,
        ID3D12GraphicsCommandList* d3D12GraphicsCommandList,
        ID3D12CommandAllocator* d3D12CommandAllocator)
    {
        HANDLE eventHandle = Windows.CreateEventW(null, Windows.FALSE, Windows.FALSE, null);

        device.d3D12ComputeFence.Get()->SetEventOnCompletion(d3D12FenceValue, eventHandle).Assert();

        TaskCompletionSource taskCompletionSource = new();
        CallbackContext* callbackContext = (CallbackContext*)NativeMemory.Alloc((nuint)sizeof(CallbackContext));

        callbackContext->TaskCompletionSourceHandle = GCHandle.Alloc(taskCompletionSource);
        callbackContext->GraphicsDeviceHandle = GCHandle.Alloc(device);
        callbackContext->D3D12GraphicsCommandList = d3D12GraphicsCommandList;
        callbackContext->D3D12CommandAllocator = d3D12CommandAllocator;
        callbackContext->EventHandle = eventHandle;

        HANDLE waitHandle;

        _ = Windows.RegisterWaitForSingleObject(
            phNewWaitObject: &waitHandle,
            hObject: eventHandle,
#if NET6_0_OR_GREATER
            Callback: &WaitForSingleObjectCallback,
#else
            Callback: (void*)Marshal.GetFunctionPointerForDelegate(WaitForSingleObjectCallbackWrapper),
#endif
            Context: callbackContext,
            dwMilliseconds: Windows.INFINITE,
            dwFlags: 0);

        return taskCompletionSource.Task;
    }

    /// <summary>
    /// The callback to signal the completion of a compute pipeline.
    /// </summary>
    /// <param name="pContext">The input context.</param>
    /// <param name="timedOut">Whether the wait has timed out.</param>
    [UnmanagedCallersOnly]
    private static void WaitForSingleObjectCallback(void* pContext, byte timedOut)
    {
        CallbackContext* callbackContext = (CallbackContext*)pContext;

        TaskCompletionSource taskCompletionSource = Unsafe.As<TaskCompletionSource>(callbackContext->TaskCompletionSourceHandle.Target)!;
        GraphicsDevice device = Unsafe.As<GraphicsDevice>(callbackContext->GraphicsDeviceHandle.Target)!;
        ID3D12GraphicsCommandList* d3D12GraphicsCommandList = callbackContext->D3D12GraphicsCommandList;
        ID3D12CommandAllocator* d3D12CommandAllocator = callbackContext->D3D12CommandAllocator;
        HANDLE eventHandle = callbackContext->EventHandle;

        callbackContext->TaskCompletionSourceHandle.Free();
        callbackContext->GraphicsDeviceHandle.Free();

        device.computeCommandListPool.Return(d3D12GraphicsCommandList, d3D12CommandAllocator);

        _ = Windows.CloseHandle(eventHandle);

        NativeMemory.Free(callbackContext);

        taskCompletionSource.SetResult();
    }
}
