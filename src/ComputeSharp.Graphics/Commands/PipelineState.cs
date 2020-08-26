using Vortice.Direct3D12;

namespace ComputeSharp.Graphics.Commands
{
    /// <summary>
    /// A <see langword="class"/> representing a custom pipeline state for a compute operation
    /// </summary>
    internal sealed class PipelineState : ComputePipelineStateDescription
    {
        /// <summary>
        /// Creates a new <see cref="PipelineState"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use</param>
        /// <param name="rootSignature">The <see cref="ID3D12RootSignature"/> value for the current shader</param>
        /// <param name="computeShader">The bytecode for the compute shader to run</param>
        public PipelineState(GraphicsDevice device, ID3D12RootSignature rootSignature, ShaderBytecode computeShader)
        {
            RootSignature = rootSignature;
            ComputeShader = computeShader;

            NativePipelineState = device.NativeDevice.CreateComputePipelineState<ID3D12PipelineState>(this);
        }

        /// <summary>
        /// Gets the <see cref="ID3D12PipelineState"/> instance for the current <see cref="PipelineState"/> object
        /// </summary>
        public ID3D12PipelineState NativePipelineState { get; }
    }
}
