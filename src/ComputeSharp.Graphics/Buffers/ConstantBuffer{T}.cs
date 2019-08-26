using System;
using System.Buffers;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Helpers;
using SharpDX.Direct3D12;
using CommandList = ComputeSharp.Graphics.Commands.CommandList;

namespace ComputeSharp.Graphics.Buffers
{
    /// <summary>
    /// A <see langword="class"/> representing a typed read write buffer stored on GPU memory
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer</typeparam>
    public sealed class ConstantBuffer<T> : HlslBuffer<T> where T : unmanaged
    {
        /// <summary>
        /// Creates a new <see cref="ConstantBuffer{T}"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance</param>
        /// <param name="size">The number of items to store in the current buffer</param>
        internal ConstantBuffer(GraphicsDevice device, int size) : base(device, size, size * GetPaddedSize() * 16, BufferType.Constant) { }

        /// <summary>
        /// Gets the right padded size for <typeparamref name="T"/> elements to store in the current instance
        /// </summary>
        [Pure]
        private static int GetPaddedSize()
        {
            int size = Unsafe.SizeOf<T>();
            return size % 16 == 0 ? 1 : size / 16 + 1;
        }

        /// <summary>
        /// Gets a single <typeparamref name="T"/> value from the current constant buffer
        /// </summary>
        /// <param name="i">The index of the value to get</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else</remarks>
        public T this[int i] => throw new InvalidExecutionContextException($"{nameof(ConstantBuffer<T>)}<T>[int]");

        /// <inheritdoc/>
        public override void GetData(Span<T> span, int offset, int count)
        {
            if (IsPaddingPresent)
            {
                // Create the temporary array
                byte[] temporaryArray = ArrayPool<byte>.Shared.Rent(count * PaddedElementSizeInBytes);
                Span<byte> temporarySpan = temporaryArray.AsSpan(0, count * PaddedElementSizeInBytes);

                // Copy the padded data to the temporary array
                Map(0);
                MemoryHelper.Copy(MappedResource, offset * PaddedElementSizeInBytes, temporarySpan, 0, count * PaddedElementSizeInBytes);
                Unmap(0);

                ref byte tin = ref temporarySpan.GetPinnableReference();
                ref T tout = ref span.GetPinnableReference();

                // Copy the padded data to the target span, removing the padding
                for (int i = 0; i < count; i++)
                {
                    ref byte rsource = ref Unsafe.Add(ref tin, i * PaddedElementSizeInBytes);
                    Unsafe.Add(ref tout, i) = Unsafe.As<byte, T>(ref rsource);
                }

                ArrayPool<byte>.Shared.Return(temporaryArray);
            }
            else
            {
                // Directly copy the data back if there is no padding
                Map(0);
                MemoryHelper.Copy(MappedResource, offset, span, 0, count);
                Unmap(0);
            }
        }

        /// <inheritdoc/>
        public override void SetData(Span<T> span, int offset, int count)
        {
            if (IsPaddingPresent)
            {
                // Create the temporary array
                byte[] temporaryArray = ArrayPool<byte>.Shared.Rent(count * PaddedElementSizeInBytes);
                Span<byte> temporarySpan = temporaryArray.AsSpan(0, count * PaddedElementSizeInBytes); // Array pool arrays can be longer
                ref T tin = ref span.GetPinnableReference();
                ref byte tout = ref temporarySpan.GetPinnableReference();

                // Copy the input data to the temporary array and add the padding
                for (int i = 0; i < count; i++)
                {
                    ref byte rtarget = ref Unsafe.Add(ref tout, i * PaddedElementSizeInBytes);
                    Unsafe.As<byte, T>(ref rtarget) = Unsafe.Add(ref tin, i);
                }

                // Copy the padded data to the GPU
                Map(0);
                MemoryHelper.Copy(temporarySpan, 0, MappedResource, offset * PaddedElementSizeInBytes, count * PaddedElementSizeInBytes);
                Unmap(0);

                ArrayPool<byte>.Shared.Return(temporaryArray);
            }
            else
            {
                // Directly copy the input span if there is no padding
                Map(0);
                MemoryHelper.Copy(span, 0, MappedResource, offset, count);
                Unmap(0);
            }
        }

        /// <inheritdoc/>
        public override void SetData(HlslBuffer<T> buffer)
        {
            if (buffer is ConstantBuffer<T>)
            {
                // Directly copy the input buffer, if possible
                using CommandList copyCommandList = new CommandList(GraphicsDevice, CommandListType.Copy);

                copyCommandList.CopyBufferRegion(buffer, 0, this, 0, SizeInBytes);
                copyCommandList.Flush();
            }
            else SetDataWithCpuBuffer(buffer);
        }
    }
}
