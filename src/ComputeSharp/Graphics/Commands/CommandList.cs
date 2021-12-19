using System;
using System.Runtime.CompilerServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;

namespace ComputeSharp.Graphics.Commands;

/// <summary>
/// A command list to set and execute operations on the GPU.
/// </summary>
internal unsafe ref struct CommandList
{
    /// <summary>
    /// The <see cref="GraphicsDevice"/> instance associated with the current command list.
    /// </summary>
    private readonly GraphicsDevice device;

    /// <summary>
    /// The command list type being used by the current instance.
    /// </summary>
    private readonly D3D12_COMMAND_LIST_TYPE d3D12CommandListType;

    /// <summary>
    /// The <see cref="ID3D12GraphicsCommandList"/> object in use by the current instance.
    /// </summary>
    private ComPtr<ID3D12GraphicsCommandList> d3D12GraphicsCommandList;

    /// <summary>
    /// The <see cref="ID3D12CommandAllocator"/> object in use by the current instance.
    /// </summary>
    private ComPtr<ID3D12CommandAllocator> d3D12CommandAllocator;

    /// <summary>
    /// Creates a new <see cref="CommandList"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to use.</param>
    /// <param name="d3D12CommandListType">The type of command list to create.</param>
    public CommandList(GraphicsDevice device, D3D12_COMMAND_LIST_TYPE d3D12CommandListType)
    {
        this.device = device;
        this.d3D12CommandListType = d3D12CommandListType;

        Unsafe.SkipInit(out this.d3D12GraphicsCommandList);
        Unsafe.SkipInit(out this.d3D12CommandAllocator);

        device.GetCommandListAndAllocator(
            d3D12CommandListType,
            out *(ID3D12GraphicsCommandList**)Unsafe.AsPointer(ref this.d3D12GraphicsCommandList),
            out *(ID3D12CommandAllocator**)Unsafe.AsPointer(ref this.d3D12CommandAllocator));

        // Set the heap descriptor if the command list is not for copy operations
        if (d3D12CommandListType is not D3D12_COMMAND_LIST_TYPE_COPY)
        {
            device.SetDescriptorHeapForCommandList(this.d3D12GraphicsCommandList);
        }
    }

    /// <summary>
    /// Creates a new <see cref="CommandList"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to use.</param>
    /// <param name="d3D12PipelineState">The <see cref="ID3D12PipelineState"/> instance to use for the new command list.</param>
    public CommandList(GraphicsDevice device, ID3D12PipelineState* d3D12PipelineState)
    {
        this.device = device;
        this.d3D12CommandListType = D3D12_COMMAND_LIST_TYPE_COMPUTE;

        Unsafe.SkipInit(out this.d3D12GraphicsCommandList);
        Unsafe.SkipInit(out this.d3D12CommandAllocator);

        device.GetCommandListAndAllocator(
            d3D12PipelineState,
            out *(ID3D12GraphicsCommandList**)Unsafe.AsPointer(ref this.d3D12GraphicsCommandList),
            out *(ID3D12CommandAllocator**)Unsafe.AsPointer(ref this.d3D12CommandAllocator));

        // Set the heap descriptor for the command list
        device.SetDescriptorHeapForCommandList(this.d3D12GraphicsCommandList);
    }

    /// <summary>
    /// Gets the command list type being used by the current instance.
    /// </summary>
    public readonly D3D12_COMMAND_LIST_TYPE D3D12CommandListType => this.d3D12CommandListType;

    /// <summary>
    /// Gets the <see cref="ID3D12GraphicsCommandList"/> object in use by the current instance.
    /// </summary>
    public readonly ID3D12GraphicsCommandList* D3D12GraphicsCommandList => this.d3D12GraphicsCommandList.Get();

    /// <summary>
    /// Detaches the <see cref="ID3D12CommandAllocator"/> object in use by the current instance.
    /// </summary>
    /// <returns>The <see cref="ID3D12CommandAllocator"/> object in use, with ownership.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ID3D12CommandAllocator* DetachD3D12CommandAllocator()
    {
        return this.d3D12CommandAllocator.Detach();
    }

    /// <summary>
    /// Detaches the <see cref="ID3D12GraphicsCommandList"/> object in use by the current instance.
    /// </summary>
    /// <returns>The <see cref="ID3D12GraphicsCommandList"/> object in use, with ownership.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ID3D12GraphicsCommandList* DetachD3D12CommandList()
    {
        return this.d3D12GraphicsCommandList.Detach();
    }

    /// <summary>
    /// Gets a pointer to the <see cref="ID3D12CommandList"/> object in use by the current instance.
    /// </summary>
    /// <returns>A double pointer to the current <see cref="ID3D12CommandList"/> object to execute.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ID3D12CommandList** GetD3D12CommandListAddressOf()
    {
        return (ID3D12CommandList**)this.d3D12GraphicsCommandList.GetAddressOf();
    }

    /// <summary>
    /// Executes the commands in the current commands list, and waits for completion.
    /// </summary>
    public void ExecuteAndWaitForCompletion()
    {
        this.d3D12GraphicsCommandList.Get()->Close();

        this.device.ExecuteCommandList(ref this);
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        this.d3D12CommandAllocator.Dispose();
        this.d3D12GraphicsCommandList.Dispose();
    }
}
