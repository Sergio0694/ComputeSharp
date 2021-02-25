using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Extensions;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Resources.Interop
{
    /// <summary>
    /// A type representing a mapped memory resource.
    /// </summary>
    internal readonly unsafe ref struct ID3D12ResourceMap
    {
        /// <summary>
        /// The target <see cref="ID3D12Resource"/> to map.
        /// </summary>
        private readonly ID3D12Resource* d3D12Resource;

        /// <summary>
        /// The pointer to the mapped resource.
        /// </summary>
        public readonly void* Pointer;

        /// <summary>
        /// Creates a new <see cref="ID3D12ResourceMap"/> instance for a given <see cref="ID3D12Resource"/> value.
        /// </summary>
        /// <param name="d3D12Resource">The input <see cref="ID3D12Resource"/> instance to map.</param>
        /// <param name="pointer">The pointer to the mapped resource.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ID3D12ResourceMap(ID3D12Resource* d3D12Resource, void* pointer)
        {
            this.d3D12Resource = d3D12Resource;

            Pointer = pointer;
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            this.d3D12Resource->Unmap();
        }
    }
}
