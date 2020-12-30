using ComputeSharp.Interop;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Commands
{
    /// <summary>
    /// A <see langword="class"/> representing a custom pipeline state for a compute operation.
    /// </summary>
    internal sealed unsafe class PipelineData : NativeObject
    {
        /// <summary>
        /// The <see cref="ID3D12RootSignature"/> instance for the current <see cref="PipelineData"/> object.
        /// </summary>
        private ComPtr<ID3D12RootSignature> d3D12RootSignature;

        /// <summary>
        /// The <see cref="ID3D12PipelineState"/> instance for the current <see cref="PipelineData"/> object.
        /// </summary>
        private ComPtr<ID3D12PipelineState> d3D12PipelineState;

        /// <summary>
        /// Creates a new <see cref="PipelineState"/> instance with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use.</param>
        /// <param name="rootSignature">The <see cref="ID3D12RootSignature"/> value for the current shader.</param>
        /// <param name="computeShader">The bytecode for the compute shader to run.</param>
        public PipelineData(ComPtr<ID3D12RootSignature> d3D12RootSignature, ComPtr<ID3D12PipelineState> d3D12PipelineState)
        {
            this.d3D12RootSignature = d3D12RootSignature;
            this.d3D12PipelineState = d3D12PipelineState;
        }

        /// <summary>
        /// Gets the <see cref="ID3D12RootSignature"/> instance for the current <see cref="PipelineData"/> object.
        /// </summary>
        public ID3D12RootSignature* D3D12RootSignature => this.d3D12RootSignature;

        /// <summary>
        /// Gets the <see cref="ID3D12PipelineState"/> instance for the current <see cref="PipelineData"/> object.
        /// </summary>
        public ID3D12PipelineState* D3D12PipelineState => this.d3D12PipelineState;

        /// <inheritdoc/>
        protected override bool OnDispose()
        {
            this.d3D12RootSignature.Dispose();
            this.d3D12PipelineState.Dispose();

            return true;
        }
    }
}
