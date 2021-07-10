using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Interop;
using TerraFX.Interop;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A class that contains info on a cached shader.
    /// </summary>
    internal sealed unsafe class CachedShader : NativeObject
    {
        /// <summary>
        /// The <see cref="IDxcBlob"/> instance currently in use.
        /// </summary>
        private ComPtr<IDxcBlob> dxcBlobBytecode;

        /// <summary>
        /// Creates a new <see cref="CachedShader"/> instance with the specified parameters.
        /// </summary>
        /// <param name="dxcBlobBytecode">The <see cref="IDxcBlob"/> bytecode instance to wrap.</param>
        public CachedShader(IDxcBlob* dxcBlobBytecode)
        {
            this.dxcBlobBytecode = new ComPtr<IDxcBlob>(dxcBlobBytecode);
        }

        /// <summary>
        /// Gets the map of cached <see cref="PipelineData"/> instances for each GPU in use.
        /// </summary>
        public ConditionalWeakTable<GraphicsDevice, PipelineData> CachedPipelines { get; } = new();

        /// <summary>
        /// Gets a raw pointer to the <see cref="IDxcBlob"/> instance in use.
        /// </summary>
        public D3D12_SHADER_BYTECODE D3D12ShaderBytecode => new((ID3DBlob*)this.dxcBlobBytecode.Get());

        /// <inheritdoc/>
        protected override bool OnDispose()
        {
            this.dxcBlobBytecode.Dispose();

            return true;
        }
    }
}
