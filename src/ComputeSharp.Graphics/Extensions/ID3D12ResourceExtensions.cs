using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Buffers.Interop;
using ComputeSharp.Graphics.Helpers;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extensions for the <see cref="ID3D12Resource"/> type.
    /// </summary>
    internal static unsafe class ID3D12ResourceExtensions
    {
        /// <summary>
        /// Maps the current resource to a specified subresource.
        /// </summary>
        /// <param name="d3d12resource">The target <see cref="ID3D12Resource"/> to map.</param>
        /// <returns>A <see cref="ID3D12ResourceMap"/> instance representing the mapped resource.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ID3D12ResourceMap Map(this ref ID3D12Resource d3d12resource)
        {
            void* pointer;

            d3d12resource.Map(0, null, &pointer).Assert();

            return new((ID3D12Resource*)Unsafe.AsPointer(ref d3d12resource), pointer);
        }

        /// <summary>
        /// Maps a given <see cref="ID3D12Resource"/> memory area.
        /// </summary>
        /// <param name="d3d12resource">The target <see cref="ID3D12Resource"/> to map.</param>
        /// <returns>A pointer to the mapped area for <paramref name="d3d12resource"/>.</returns>
        /// <exception cref="Exception">Thrown when the mapping operation fails.</exception>
        /// <remarks>Only use this method to unmap resources that were mapped with <see cref="Map(ref ID3D12Resource)"/>.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unmap(this ref ID3D12Resource d3d12resource)
        {
            d3d12resource.Unmap(0, null);
        }
    }
}
