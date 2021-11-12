using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Helpers;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Graphics.Resources.Interop;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using ResourceType = ComputeSharp.Graphics.Resources.Enums.ResourceType;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> representing a typed read write buffer stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the buffer.</typeparam>
[DebuggerTypeProxy(typeof(BufferDebugView<>))]
[DebuggerDisplay("{ToString(),raw}")]
public sealed class ConstantBuffer<T> : Buffer<T>
    where T : unmanaged
{
    /// <summary>
    /// The alignment boundary for elements in a constant buffer.
    /// </summary>
    private const int ElementAlignment = Windows.D3D12_COMMONSHADER_CONSTANT_BUFFER_PARTIAL_UPDATE_EXTENTS_BYTE_ALIGNMENT;

    /// <summary>
    /// Creates a new <see cref="ConstantBuffer{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="length">The number of items to store in the current buffer.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    internal ConstantBuffer(GraphicsDevice device, int length, AllocationMode allocationMode)
        : base(device, length, (uint)GetPaddedSize(), ResourceType.Constant, allocationMode)
    {
    }

    /// <summary>
    /// Gets a single <typeparamref name="T"/> value from the current constant buffer.
    /// </summary>
    /// <param name="i">The index of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public T this[int i] => throw new InvalidExecutionContextException($"{typeof(ConstantBuffer<T>)}[{typeof(int)}]");

    /// <summary>
    /// Gets the right padded size for <typeparamref name="T"/> elements to store in the current instance.
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe int GetPaddedSize()
    {
        return AlignmentHelper.Pad(sizeof(T), ElementAlignment);
    }

    /// <inheritdoc/>
    internal override unsafe void CopyTo(ref T destination, int offset, int length)
    {
        GraphicsDevice.ThrowIfDisposed();

        ThrowIfDisposed();

        Guard.IsBetweenOrEqualTo(length, 0, Length, nameof(length));
        Guard.IsInRange(offset, 0, Length, nameof(offset));
        Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(offset));

        using ID3D12ResourceMap resource = D3D12Resource->Map();

        fixed (void* destinationPointer = &destination)
        {
            MemoryHelper.Copy<T>(
                source: resource.Pointer,
                destination: destinationPointer,
                sourceElementOffset: (uint)offset,
                destinationElementOffset: 0,
                sourceElementPitchInBytes: (uint)GetPaddedSize(),
                destinationElementPitchInBytes: (uint)sizeof(T),
                count: (uint)length);
        }
    }

    /// <inheritdoc/>
    internal override unsafe void CopyTo(Buffer<T> destination, int sourceOffset, int destinationOffset, int length)
    {
        GraphicsDevice.ThrowIfDisposed();

        ThrowIfDisposed();

        destination.ThrowIfDeviceMismatch(GraphicsDevice);
        destination.ThrowIfDisposed();

        Guard.IsBetweenOrEqualTo(length, 0, Length, nameof(length));
        Guard.IsBetweenOrEqualTo(length, 0, destination.Length, nameof(length));
        Guard.IsInRange(sourceOffset, 0, Length, nameof(sourceOffset));
        Guard.IsLessThanOrEqualTo(sourceOffset + length, Length, nameof(sourceOffset));
        Guard.IsInRange(destinationOffset, 0, destination.Length, nameof(destinationOffset));
        Guard.IsLessThanOrEqualTo(destinationOffset + length, destination.Length, nameof(destinationOffset));

        if (destination is ConstantBuffer<T> buffer)
        {
            using ID3D12ResourceMap sourceMap = D3D12Resource->Map();
            using ID3D12ResourceMap destinationMap = buffer.D3D12Resource->Map();

            MemoryHelper.Copy<T>(
                source: sourceMap.Pointer,
                destination: destinationMap.Pointer,
                sourceElementOffset: (uint)sourceOffset,
                destinationElementOffset: (uint)destinationOffset,
                sourceElementPitchInBytes: (uint)GetPaddedSize(),
                destinationElementPitchInBytes: (uint)GetPaddedSize(),
                count: (uint)length);
        }
        else CopyToWithCpuBuffer(destination, sourceOffset, destinationOffset, length);
    }

    /// <inheritdoc/>
    internal override unsafe void CopyFrom(ref T source, int offset, int length)
    {
        GraphicsDevice.ThrowIfDisposed();

        ThrowIfDisposed();

        Guard.IsBetweenOrEqualTo(length, 0, Length, nameof(length));
        Guard.IsInRange(offset, 0, Length, nameof(offset));
        Guard.IsLessThanOrEqualTo(offset + length, Length, nameof(offset));

        using ID3D12ResourceMap resource = D3D12Resource->Map();

        fixed (void* sourcePointer = &source)
        {
            MemoryHelper.Copy<T>(
                source: sourcePointer,
                destination: resource.Pointer,
                sourceElementOffset: 0,
                destinationElementOffset: (uint)offset,
                sourceElementPitchInBytes: (uint)sizeof(T),
                destinationElementPitchInBytes: (uint)GetPaddedSize(),
                count: (uint)length);
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.ConstantBuffer<{typeof(T)}>[{Length}]";
    }
}
