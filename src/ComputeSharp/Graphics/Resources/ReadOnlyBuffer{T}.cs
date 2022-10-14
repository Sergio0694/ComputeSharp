using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Resources.Enums;
using ComputeSharp.Resources;
using ComputeSharp.Resources.Debug;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> representing a typed readonly buffer stored on GPU memory.
/// </summary>
/// <typeparam name="T">The type of items stored on the buffer.</typeparam>
[DebuggerTypeProxy(typeof(BufferDebugView<>))]
[DebuggerDisplay("{ToString(),raw}")]
public sealed class ReadOnlyBuffer<T> : StructuredBuffer<T>
    where T : unmanaged
{
    /// <summary>
    /// Creates a new <see cref="ReadOnlyBuffer{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
    /// <param name="length">The number of items to store in the current buffer.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    internal ReadOnlyBuffer(GraphicsDevice device, int length, AllocationMode allocationMode)
        : base(device, length, ResourceType.ReadOnly, allocationMode)
    {
    }

    /// <summary>
    /// Gets a single <typeparamref name="T"/> value from the current readonly buffer.
    /// </summary>
    /// <param name="i">The index of the value to get.</param>
    /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
    public ref readonly T this[int i] => throw new InvalidExecutionContextException($"{typeof(ReadOnlyBuffer<T>)}[{typeof(int)}]");

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"ComputeSharp.ReadOnlyBuffer<{typeof(T)}>[{Length}]";
    }
}