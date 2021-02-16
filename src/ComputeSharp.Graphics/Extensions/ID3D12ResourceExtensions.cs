using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Resources.Interop;
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
        /// <param name="d3D12Resource">The target <see cref="ID3D12Resource"/> to map.</param>
        /// <returns>A <see cref="ID3D12ResourceMap"/> instance representing the mapped resource.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ID3D12ResourceMap Map(this ref ID3D12Resource d3D12Resource)
        {
            void* pointer;

            d3D12Resource.Map(0, null, &pointer).Assert();

            return new((ID3D12Resource*)Unsafe.AsPointer(ref d3D12Resource), pointer);
        }

        /// <summary>
        /// Maps a given <see cref="ID3D12Resource"/> memory area.
        /// </summary>
        /// <param name="d3D12Resource">The target <see cref="ID3D12Resource"/> to map.</param>
        /// <returns>A pointer to the mapped area for <paramref name="d3D12Resource"/>.</returns>
        /// <exception cref="Exception">Thrown when the mapping operation fails.</exception>
        /// <remarks>Only use this method to unmap resources that were mapped with <see cref="Map(ref ID3D12Resource)"/>.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unmap(this ref ID3D12Resource d3D12Resource)
        {
            d3D12Resource.Unmap(0, null);
        }

        /// <summary>
        /// Assigns a name to a given <see cref="ID3D12Resource"/>.
        /// </summary>
        /// <param name="d3D12Resource">The target <see cref="ID3D12Resource"/> to map.</param>
        /// <param name="wrapper">The wrapper object to get the name from.</param>
        /// <exception cref="Exception">Thrown when the mapping operation fails.</exception>
        [Conditional("DEBUG")]
        public static void SetName(this ref ID3D12Resource d3D12Resource, object wrapper)
        {
            string name = wrapper.ToString()!;

            fixed (char* p = name)
            {
                d3D12Resource.SetName((ushort*)p).Assert();
            }
        }
    }
}
