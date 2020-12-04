using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Graphics.Buffers.Enums;
using ComputeSharp.Graphics.Buffers.Interop;
using ComputeSharp.Graphics.Buffers.Views;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Graphics.Helpers;
using Microsoft.Toolkit.Diagnostics;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;

namespace ComputeSharp
{
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
        /// Creates a new <see cref="ConstantBuffer{T}"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> associated with the current instance.</param>
        /// <param name="length">The number of items to store in the current buffer.</param>
        internal ConstantBuffer(GraphicsDevice device, int length)
            : base(device, length, (uint)GetPaddedSize(), ResourceType.Constant)
        {
        }

        /// <summary>
        /// Gets a single <typeparamref name="T"/> value from the current constant buffer.
        /// </summary>
        /// <param name="i">The index of the value to get.</param>
        /// <remarks>This API can only be used from a compute shader, and will always throw if used anywhere else.</remarks>
        public T this[int i] => throw new InvalidExecutionContextException($"{nameof(ConstantBuffer<T>)}<T>[int]");

        /// <summary>
        /// Gets the right padded size for <typeparamref name="T"/> elements to store in the current instance.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetPaddedSize()
        {
            return (Unsafe.SizeOf<T>() + 15) & ~15;
        }

        /// <inheritdoc/>
        public override unsafe void GetData(Span<T> destination, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo((uint)offset + destination.Length, (uint)Length, nameof(destination));

            using ID3D12ResourceMap resource = D3D12Resource->Map();

            if (IsPaddingPresent)
            {
                int count = destination.Length;
                ref T spanRef = ref MemoryMarshal.GetReference(destination);
                ref byte resourceRef = ref Unsafe.AsRef<byte>(resource.Pointer);

                // Move to the initial ref
                resourceRef = ref Unsafe.Add(ref resourceRef, offset * GetPaddedSize());

                for (var i = 0; i < count; i++)
                {
                    ref byte targetRef = ref Unsafe.Add(ref resourceRef, i * GetPaddedSize());

                    Unsafe.Add(ref spanRef, i) = Unsafe.As<byte, T>(ref targetRef);
                }
            }
            else MemoryHelper.Copy(resource.Pointer, offset, destination);
        }

        /// <inheritdoc/>
        public override unsafe void SetData(ReadOnlySpan<T> source, int offset)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDisposed();

            Guard.IsInRange(offset, 0, Length, nameof(offset));
            Guard.IsLessThanOrEqualTo((uint)offset + source.Length, (uint)Length, nameof(source));

            using ID3D12ResourceMap resource = D3D12Resource->Map();

            if (IsPaddingPresent)
            {
                int count = source.Length;
                ref T spanRef = ref MemoryMarshal.GetReference(source);
                ref byte resourceRef = ref Unsafe.AsRef<byte>(resource.Pointer);

                // Move to the initial offset
                resourceRef = ref Unsafe.Add(ref resourceRef, offset * GetPaddedSize());

                for (var i = 0; i < count; i++)
                {
                    ref byte targetRef = ref Unsafe.Add(ref resourceRef, i * GetPaddedSize());

                    Unsafe.As<byte, T>(ref targetRef) = Unsafe.Add(ref spanRef, i);
                }
            }
            else MemoryHelper.Copy(source, resource.Pointer, offset);
        }

        /// <inheritdoc/>
        public override unsafe void SetData(Buffer<T> source)
        {
            GraphicsDevice.ThrowIfDisposed();

            ThrowIfDeviceMismatch(source.GraphicsDevice);
            ThrowIfDisposed();
            source.ThrowIfDisposed();

            if (source is ConstantBuffer<T> &&
                source.GraphicsDevice == GraphicsDevice)
            {
                // Directly copy the input buffer, if possible
                using CommandList copyCommandList = new CommandList(GraphicsDevice, D3D12_COMMAND_LIST_TYPE_COPY);

                copyCommandList.CopyBufferRegion(D3D12Resource, 0, source.D3D12Resource, 0,(ulong)SizeInBytes);
                copyCommandList.ExecuteAndWaitForCompletion();
            }
            else SetDataWithCpuBuffer(source);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"ComputeSharp.ConstantBuffer<{typeof(T)}>[{Length}]";
        }
    }
}
