using SharpDX.Direct3D;
using SharpDX.Direct3D12;
using SharpDX.DXGI;

namespace DirectX12GameEngine.Graphics
{
    public sealed class PipelineState
    {
        public PipelineState(GraphicsDevice device, ComputePipelineStateDescription pipelineStateDescription)
        {
            IsCompute = true;
            RootSignature = pipelineStateDescription.RootSignaturePointer;
            NativePipelineState = device.NativeDevice.CreateComputePipelineState(pipelineStateDescription);
        }

        public PipelineState(GraphicsDevice device, RootSignature rootSignature, ShaderBytecode computeShader)
            : this(device, CreateComputePipelineStateDescription(rootSignature, computeShader))
        {
        }

        public PipelineState(GraphicsDevice device, GraphicsPipelineStateDescription pipelineStateDescription)
        {
            IsCompute = false;
            RootSignature = pipelineStateDescription.RootSignature;
            NativePipelineState = device.NativeDevice.CreateGraphicsPipelineState(pipelineStateDescription);
        }

        public PipelineState(GraphicsDevice device, InputElement[] inputElements, RootSignature rootSignature, ShaderBytecode vertexShader, ShaderBytecode pixelShader, ShaderBytecode hullShader = default, ShaderBytecode domainShader = default, ShaderBytecode geometryShader = default, RasterizerStateDescription? rasterizerStateDescription = null)
            : this(device, CreateGraphicsPipelineStateDescription(device, inputElements, rootSignature, vertexShader, pixelShader, hullShader, domainShader, geometryShader, rasterizerStateDescription))
        {
        }

        public RootSignature RootSignature { get; }

        internal bool IsCompute { get; }

        internal SharpDX.Direct3D12.PipelineState NativePipelineState { get; }

        private static ComputePipelineStateDescription CreateComputePipelineStateDescription(RootSignature rootSignature, ShaderBytecode computeShader)
        {
            return new ComputePipelineStateDescription
            {
                RootSignaturePointer = rootSignature,
                ComputeShader = computeShader
            };
        }

        private static GraphicsPipelineStateDescription CreateGraphicsPipelineStateDescription(GraphicsDevice device, InputElement[] inputElements, RootSignature rootSignature, ShaderBytecode vertexShader, ShaderBytecode pixelShader, ShaderBytecode hullShader, ShaderBytecode domainShader, ShaderBytecode geometryShader, RasterizerStateDescription? rasterizerStateDescription)
        {
            RasterizerStateDescription rasterizerDescription = rasterizerStateDescription ?? RasterizerStateDescription.Default();
            rasterizerDescription.IsFrontCounterClockwise = true;
            rasterizerDescription.CullMode = CullMode.None;

            BlendStateDescription blendStateDescription = BlendStateDescription.Default();
            RenderTargetBlendDescription[] renderTargetDescriptions = blendStateDescription.RenderTarget;

            for (int i = 0; i < renderTargetDescriptions.Length; i++)
            {
                renderTargetDescriptions[i].IsBlendEnabled = true;
                renderTargetDescriptions[i].SourceBlend = BlendOption.SourceAlpha;
                renderTargetDescriptions[i].DestinationBlend = BlendOption.InverseSourceAlpha;
            }

            DepthStencilStateDescription depthStencilStateDescription = DepthStencilStateDescription.Default();

            GraphicsPipelineStateDescription pipelineStateDescription = new GraphicsPipelineStateDescription
            {
                InputLayout = new InputLayoutDescription(inputElements),
                RootSignature = rootSignature,
                VertexShader = vertexShader,
                PixelShader = pixelShader,
                HullShader = hullShader,
                DomainShader = domainShader,
                GeometryShader = geometryShader,
                RasterizerState = rasterizerDescription,
                BlendState = blendStateDescription,
                DepthStencilState = depthStencilStateDescription,
                SampleMask = int.MaxValue,
                PrimitiveTopologyType = PrimitiveTopologyType.Triangle,
                RenderTargetCount = 1,
                SampleDescription = new SampleDescription(1, 0),
                StreamOutput = new StreamOutputDescription()
            };

            return pipelineStateDescription;
        }
    }
}
