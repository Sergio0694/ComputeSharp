using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_COMMAND_LIST_TYPE;

namespace ComputeSharp.Graphics.Commands;

/// <summary>
/// A command list to set and execute operations on the GPU.
/// </summary>
internal unsafe struct CommandList : IDisposable
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
            out this.d3D12GraphicsCommandList.GetPinnableReference(),
            out this.d3D12CommandAllocator.GetPinnableReference());

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
            out this.d3D12GraphicsCommandList.GetPinnableReference(),
            out this.d3D12CommandAllocator.GetPinnableReference());

        // Set the heap descriptor for the command list
        device.SetDescriptorHeapForCommandList(this.d3D12GraphicsCommandList);
    }

    /// <summary>
    /// Gets whether or not the curreent <see cref="CommandList"/> instance is allocated.
    /// </summary>
    public readonly bool IsAllocated => this.device is not null;

    /// <summary>
    /// Gets the <see cref="GraphicsDevice"/> instance associated with the current command list.
    /// </summary>
    public GraphicsDevice GraphicsDevice => this.device;

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
    public readonly ref ID3D12CommandList* GetD3D12CommandListPinnableAddressOf()
    {
        // This method should return a ref ID3D12CommandList*, so it can't just call this.d3D12GraphicsCommandList.GetPinnableReference().
        // That would return a ref ID3D12GraphicsCommandList*, and there's currently no way to cast that without wasting performance (ie.
        // a function pointer cast and calli is needed). Instead, we can reinterpret the reference to the field, and then just call
        // GetPinnableReference() on the new reference, which this time will return the ref ID3D12CommandList* we wanted.
        return ref Unsafe.As<ComPtr<ID3D12GraphicsCommandList>, ComPtr<ID3D12CommandList>>(ref Unsafe.AsRef(in this.d3D12GraphicsCommandList)).GetPinnableReference();
    }

    /// <summary>
    /// Executes the commands in the current commands list, and waits for completion.
    /// </summary>
    public void ExecuteAndWaitForCompletion()
    {
        this.d3D12GraphicsCommandList.Get()->Close().Assert();

        this.device.ExecuteCommandList(ref this);
    }

    /// <summary>
    /// Executes the commands in the current commands list, returns a <see cref="ValueTask"/>.
    /// </summary>
    /// <returns>The <see cref="ValueTask"/> to await for the operations to complete.</returns>
    public ValueTask ExecuteAndWaitForCompletionAsync()
    {
        this.d3D12GraphicsCommandList.Get()->Close().Assert();

        return this.device.ExecuteCommandListAsync(ref this);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.d3D12CommandAllocator.Dispose();
        this.d3D12GraphicsCommandList.Dispose();
    }
}
