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
        /// Creates a new <see cref="PipelineData"/> instance with the specified parameters.
        /// </summary>
        /// <param name="d3D12RootSignature">The <see cref="ID3D12RootSignature"/> value for the current shader.</param>
        /// <param name="d3D12PipelineState">The compiled pipeline state to reuse for the current shader.</param>
        public PipelineData(ID3D12RootSignature* d3D12RootSignature, ID3D12PipelineState* d3D12PipelineState)
        {
            this.d3D12RootSignature = new ComPtr<ID3D12RootSignature>(d3D12RootSignature);
            this.d3D12PipelineState = new ComPtr<ID3D12PipelineState>(d3D12PipelineState);
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
