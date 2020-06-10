using System;
using System.Buffers;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Helpers;
using Vortice.Direct3D12;
using CommandList = ComputeSharp.Graphics.Commands.CommandList;

namespace ComputeSharp
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
        internal ConstantBuffer(GraphicsDevice device, int size) : base(device, size, size * GetPaddedSize() * /* what is the meaning of this magic num? */ 16, BufferType.Constant) { }

        /// <summary>
        /// Gets the right padded size for <typeparamref name="T"/> elements to store in the current instance
        /// </summary>
        [Pure]
        private static int GetPaddedSize()
        {
            int size = Unsafe.SizeOf<T>();

            // Equivalent to rounding up to nearest 256. We add 255 then t
            return (size + 255) & ~255;
        }

        /// <summary>
        /// Gets a single <typeparamref name="T"/> value from the current constant buffer
        /// </summary>
        /// <param name="i">The index of the value to get</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else</remarks>
        public T this[int i] => throw new InvalidExecutionContextException($"{nameof(ConstantBuffer<T>)}<T>[int]");

        /// <inheritdoc/>
        public override unsafe void GetData(Span<T> span, int offset, int count)
        {
            if (IsPaddingPresent)
            {
                Map();

                ref var dest = ref MemoryMarshal.GetReference(span);
                for (var i = offset; i < count; i++)
                {
                    // why is this exposed as an IntPtr? It should really be void*/T*/byte*
                    Unsafe.Add(ref dest, i) = ((T*) MappedResource)[i * GetPaddedSize()];
                }

                Unmap();
            }
            else
            {
                // Directly copy the data back if there is no padding
                Map();
                MemoryHelper.Copy(MappedResource, offset, span, 0, count);
                Unmap();
            }
        }

        /// <inheritdoc/>
        public override unsafe void SetData(Span<T> span, int offset, int count)
        {
            if (IsPaddingPresent)
            {
                Map();

                ref var src = ref MemoryMarshal.GetReference(span);
                for (var i = offset; i < count; i++)
                {
                    // why is this exposed as an IntPtr? It should really be void*/T*/byte*
                    ((T*)MappedResource)[i * GetPaddedSize()] = Unsafe.Add(ref src, i);
                }

                Unmap();
            }
            else
            {
                // Directly copy the input span if there is no padding
                Map();
                MemoryHelper.Copy(span, 0, MappedResource, offset, count);
                Unmap();
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
