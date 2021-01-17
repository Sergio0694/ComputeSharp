using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="struct"/> that contains all the captured data to dispatch a shader.
    /// </summary>
    internal readonly ref struct DispatchData
    {
        /// <summary>
        /// The <see cref="ulong"/> array (actually <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> values) with the captured buffers.
        /// </summary>
        private readonly ulong[] resourcesArray;

        /// <summary>
        /// The number of <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> values in <see cref="resourcesArray"/>.
        /// </summary>
        private readonly int resourcesCount;

        /// <summary>
        /// The <see cref="byte"/> array with all the captured variables, with proper padding.
        /// </summary>
        private readonly byte[] variablesArray;

        /// <summary>
        /// The actual size in bytes to use from <see cref="variablesArray"/>.
        /// </summary>
        private readonly int variablesByteSize;

        /// <summary>
        /// Creates a new <see cref="DispatchData"/> instance with the specified parameters.
        /// </summary>
        /// <param name="resourcesArray">The <see cref="ulong"/> array with the captured buffers.</param>
        /// <param name="resourcesCount">The number of <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instances in <see cref="resourcesArray"/>.</param>
        /// <param name="variablesArray">The <see cref="byte"/> array with all the captured variables, with proper padding.</param>
        /// <param name="variablesByteSize">The actual size in bytes to use from <see cref="variablesArray"/>.</param>
        public DispatchData(ulong[] resourcesArray, int resourcesCount, byte[] variablesArray, int variablesByteSize)
        {
            this.resourcesArray = resourcesArray;
            this.variablesArray = variablesArray;
            this.resourcesCount = resourcesCount;
            this.variablesByteSize = variablesByteSize;
        }

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with all the captured buffers.
        /// </summary>
        public ReadOnlySpan<D3D12_GPU_DESCRIPTOR_HANDLE> Resources
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                ref ulong r0 = ref MemoryMarshal.GetArrayDataReference(this.resourcesArray);
                ref D3D12_GPU_DESCRIPTOR_HANDLE r1 = ref Unsafe.As<ulong, D3D12_GPU_DESCRIPTOR_HANDLE>(ref r0);

                return MemoryMarshal.CreateReadOnlySpan(ref r1, this.resourcesCount);
#else
                Span<ulong> span = this.resourcesArray.AsSpan(0, this.resourcesCount);

                return MemoryMarshal.Cast<ulong, D3D12_GPU_DESCRIPTOR_HANDLE>(span);
#endif
            }
        }

        /// <summary>
        /// Gets a <see cref="ReadOnlySpan{T}"/> with the padded data representing all the captured variables.
        /// </summary>
        public unsafe ReadOnlySpan<uint> Variables
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                ref byte r0 = ref MemoryMarshal.GetArrayDataReference(this.variablesArray);
                ref uint r1 = ref Unsafe.As<byte, uint>(ref r0);
                int length = (int)((uint)this.variablesByteSize / sizeof(uint));

                return MemoryMarshal.CreateReadOnlySpan(ref r1, length);
#else
                Span<byte> span = this.variablesArray.AsSpan(0, this.variablesByteSize);

                return MemoryMarshal.Cast<byte, uint>(span);
#endif
            }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            ArrayPool<ulong>.Shared.Return(this.resourcesArray);
            ArrayPool<byte>.Shared.Return(this.variablesArray);
        }
    }
}
