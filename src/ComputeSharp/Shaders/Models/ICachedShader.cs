using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Shaders.Translation.Models;

/// <summary>
/// An interface that contains info on a cached shader.
/// </summary>
internal interface ICachedShader
{
    /// <summary>
    /// Gets the map of cached <see cref="PipelineData"/> instances for each GPU in use.
    /// </summary>
    ConditionalWeakTable<GraphicsDevice, PipelineData> CachedPipelines { get; }

    /// <summary>
    /// Gets a <see cref="D3D12_SHADER_BYTECODE"/> value pointing shader bytecode to use.
    /// </summary>
    D3D12_SHADER_BYTECODE D3D12ShaderBytecode { get; }

    /// <summary>
    /// An <see cref="ICachedShader"/> implementation for an embedded shader.
    /// </summary>
    public sealed unsafe class Embedded : ICachedShader
    {
        /// <summary>
        /// The bytecode buffer, if a precompiled shader is used.
        /// </summary>
        private readonly byte* precompiledBytecodeBuffer;

        /// <summary>
        /// The buffer size for <see cref="precompiledBytecodeBuffer"/>, if present.
        /// </summary>
        private readonly uint precompiledBytecodeSize;

        /// <summary>
        /// Creates a new <see cref="Embedded"/> instance with the specified parameters.
        /// </summary>
        /// <param name="precompiledBytecode">The shader bytecode buffer to wrap.</param>
        /// <remarks>
        /// It is assumed that <paramref name="precompiledBytecode"/> is a static binary block in the <c>.text</c> segment of the
        /// PE file, which is what Roslyn generates when directly assigning a new array to a <see cref="ReadOnlySpan{T}"/>. As such,
        /// the underlying buffer is assumed to be pinned, and it will not be properly tracked for GC compactions. If an invalid
        /// buffer is passed and its contents are moved, the behavior is undefined (access violations will most definitely occurr).
        /// </remarks>
        public Embedded(ReadOnlySpan<byte> precompiledBytecode)
        {
            this.precompiledBytecodeBuffer = (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(precompiledBytecode));
            this.precompiledBytecodeSize = (uint)precompiledBytecode.Length;
        }

        /// <inheritdoc/>
        public ConditionalWeakTable<GraphicsDevice, PipelineData> CachedPipelines { get; } = new();

        /// <inheritdoc/>
        public D3D12_SHADER_BYTECODE D3D12ShaderBytecode => new(this.precompiledBytecodeBuffer, this.precompiledBytecodeSize);
    }

    /// <summary>
    /// An <see cref="ICachedShader"/> implementation for a dynamically compiled shader.
    /// </summary>
    public sealed unsafe class Dynamic : NativeObject, ICachedShader
    {
        /// <summary>
        /// The <see cref="IDxcBlob"/> instance currently in use.
        /// </summary>
        private ComPtr<IDxcBlob> dxcBlobBytecode;

        /// <summary>
        /// Creates a new <see cref="Dynamic"/> instance with the specified parameters.
        /// </summary>
        /// <param name="dxcBlobBytecode">The <see cref="IDxcBlob"/> bytecode instance to wrap.</param>
        public Dynamic(IDxcBlob* dxcBlobBytecode)
        {
            this.dxcBlobBytecode = new ComPtr<IDxcBlob>(dxcBlobBytecode);
        }

        /// <inheritdoc/>
        public ConditionalWeakTable<GraphicsDevice, PipelineData> CachedPipelines { get; } = new();

        /// <inheritdoc/>
        public D3D12_SHADER_BYTECODE D3D12ShaderBytecode => new ((ID3DBlob*)this.dxcBlobBytecode.Get());

        /// <inheritdoc/>
        protected override void OnDispose()
        {
            this.dxcBlobBytecode.Dispose();
        }
    }
}
