using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using TerraFX.Interop;

namespace ComputeSharp.__Internals
{
    /// <summary>
    /// A helper class with some proxy methods to expose to generated code in external projects.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This type is not intended to be used directly by user code")]
    public static class GraphicsResourceHelper
    {
        /// <summary>
        /// Validates the given buffer for usage with a specified device, and retrieves its GPU descriptor handle.
        /// </summary>
        /// <param name="buffer">The input <see cref="Buffer{T}"/> instance to check.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is not intended to be called directly by user code")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ValidateAndGetGpuDescriptorHandle<T>(Buffer<T> buffer, GraphicsDevice device)
            where T : unmanaged
        {
            buffer.ThrowIfDisposed();
            buffer.ThrowIfDeviceMismatch(device);

            return Unsafe.As<D3D12_GPU_DESCRIPTOR_HANDLE, ulong>(ref Unsafe.AsRef(in buffer.D3D12GpuDescriptorHandle));
        }

        /// <summary>
        /// Validates the given buffer for usage with a specified device, and retrieves its GPU descriptor handle.
        /// </summary>
        /// <param name="buffer">The input <see cref="Texture2D{T}"/> instance to check.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is not intended to be called directly by user code")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ValidateAndGetGpuDescriptorHandle<T>(Texture2D<T> buffer, GraphicsDevice device)
            where T : unmanaged
        {
            buffer.ThrowIfDisposed();
            buffer.ThrowIfDeviceMismatch(device);

            return Unsafe.As<D3D12_GPU_DESCRIPTOR_HANDLE, ulong>(ref Unsafe.AsRef(in buffer.D3D12GpuDescriptorHandle));
        }

        /// <summary>
        /// Validates the given buffer for usage with a specified device, and retrieves its GPU descriptor handle.
        /// </summary>
        /// <param name="buffer">The input <see cref="Texture3D{T}"/> instance to check.</param>
        /// <param name="device">The target <see cref="GraphicsDevice"/> instance in use.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is not intended to be called directly by user code")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ValidateAndGetGpuDescriptorHandle<T>(Texture3D<T> buffer, GraphicsDevice device)
            where T : unmanaged
        {
            buffer.ThrowIfDisposed();
            buffer.ThrowIfDeviceMismatch(device);

            return Unsafe.As<D3D12_GPU_DESCRIPTOR_HANDLE, ulong>(ref Unsafe.AsRef(in buffer.D3D12GpuDescriptorHandle));
        }
    }
}
