using ComputeSharp.Graphics.Helpers;
using System;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extensions for the <see cref="ID3D12Resource"/> type.
    /// </summary>
    internal static unsafe class ID3D12ResourceExtensions
    {
        /// <summary>
        /// Maps a given <see cref="ID3D12Resource"/> memory area.
        /// </summary>
        /// <param name="d3d12resource">The target <see cref="ID3D12Resource"/> to map.</param>
        /// <returns>A pointer to the mapped area for <paramref name="d3d12resource"/>.</returns>
        /// <exception cref="Exception">Thrown when the mapping operation fails.</exception>
        public static IntPtr Map(this ref ID3D12Resource d3d12resource)
        {
            IntPtr ptr;

            int result = d3d12resource.Map(0, null, (void**)&ptr);

            ThrowHelper.ThrowIfFailed(result);

            return ptr;
        }

        /// <summary>
        /// Maps a given <see cref="ID3D12Resource"/> memory area.
        /// </summary>
        /// <param name="d3d12resource">The target <see cref="ID3D12Resource"/> to map.</param>
        /// <returns>A pointer to the mapped area for <paramref name="d3d12resource"/>.</returns>
        /// <exception cref="Exception">Thrown when the mapping operation fails.</exception>
        /// <remarks>Only use this method to unmap resources that were mapped with <see cref="Map(ref ID3D12Resource)"/>.</remarks>
        public static void Unmap(this ref ID3D12Resource d3d12resource)
        {
            d3d12resource.Unmap(0, null);
        }
    }
}
