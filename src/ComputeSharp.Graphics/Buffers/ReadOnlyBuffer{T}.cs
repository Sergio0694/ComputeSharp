using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Helpers;
using SharpDX.Direct3D12;

namespace ComputeSharp.Graphics.Buffers
{
    /// <summary>
    /// A <see langword="class"/> representing a typed read write buffer stored on GPU memory
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer</typeparam>
    public sealed class ReadOnlyBuffer<T> : HlslBuffer<T> where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="ReadOnlyBuffer{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        internal ReadOnlyBuffer(GraphicsDevice device, int size) : base(device, size, size * (Unsafe.SizeOf<T>() / 16 + 1) * 16, HeapType.Upload)
        {
            PaddedElementSizeInBytes = SizeInBytes / Size;
        }

        /// <summary>
        /// Gets the size in bytes of the current buffer
        /// </summary>
        private int PaddedElementSizeInBytes { get; }

        /// <summary>
        /// Gets a single <typeparamref name="T"/> value from the current read write buffer
        /// </summary>
        /// <param name="i">The index of the value to get</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else</remarks>
        public T this[uint i] => throw new InvalidOperationException("The indexer APIs can only be used from a compute shader");

        /// <inheritdoc/>
        public override void GetData(Span<T> span)
        {
            byte[] temporaryArray = ArrayPool<byte>.Shared.Rent(SizeInBytes);
            Span<byte> temporarySpan = temporaryArray.AsSpan();

            Map(0);
            MemoryHelper.Copy(MappedResource, temporarySpan);
            Unmap(0);

            ref byte tin = ref temporarySpan.GetPinnableReference();
            ref T tout = ref span.GetPinnableReference();

            for (int i = 0; i < Size; i++)
            {
                ref byte rsource = ref Unsafe.Add(ref tin, i * PaddedElementSizeInBytes);
                Unsafe.Add(ref tout, i) = Unsafe.As<byte, T>(ref rsource);
            }

            ArrayPool<byte>.Shared.Return(temporaryArray);
        }

        /// <inheritdoc/>
        public override void SetData(Span<T> span)
        {
            byte[] temporaryArray = ArrayPool<byte>.Shared.Rent(SizeInBytes);
            Span<byte> temporarySpan = temporaryArray.AsSpan(0, SizeInBytes); // Array pool arrays can be longer
            ref T tin = ref span.GetPinnableReference();
            ref byte tout = ref temporarySpan.GetPinnableReference();

            temporarySpan.Clear();

            for (int i = 0; i < Size; i++)
            {
                ref byte rtarget = ref Unsafe.Add(ref tout, i * PaddedElementSizeInBytes);
                Unsafe.As<byte, T>(ref rtarget) = Unsafe.Add(ref tin, i);
            }

            Map(0);
            MemoryHelper.Copy(temporarySpan, MappedResource);
            Unmap(0);

            ArrayPool<byte>.Shared.Return(temporaryArray);
        }
    }
}
