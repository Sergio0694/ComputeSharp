using SharpDX.Direct3D12;

namespace DirectX12GameEngine.Graphics
{
    /// <summary>
    /// A <see langword="class"/> representing a custom pipeline state for a compute operation
    /// </summary>
    public sealed class PipelineState : ComputePipelineStateDescription
    {
        /// <summary>
        /// Creates a new <see cref="PipelineState"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use</param>
        /// <param name="rootSignature">The <see cref="SharpDX.Direct3D12.RootSignature"/> value for the current shader</param>
        /// <param name="computeShader">The bytecode for the compute shader to run</param>
        public PipelineState(GraphicsDevice device, RootSignature rootSignature, ShaderBytecode computeShader)
        {
            RootSignaturePointer = rootSignature;
            ComputeShader = computeShader;
            RootSignature = RootSignaturePointer;

            NativePipelineState = device.NativeDevice.CreateComputePipelineState(this);
        }

        /// <summary>
        /// Gets the <see cref="SharpDX.Direct3D12.RootSignature"/> value for the current shader
        /// </summary>
        public RootSignature RootSignature { get; }

        /// <summary>
        /// Gets the <see cref="SharpDX.Direct3D12.PipelineState"/> instance for the current <see cref="PipelineState"/> object
        /// </summary>
        public SharpDX.Direct3D12.PipelineState NativePipelineState { get; }
    }
}
