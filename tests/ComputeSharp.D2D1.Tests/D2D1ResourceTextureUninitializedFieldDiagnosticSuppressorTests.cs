namespace ComputeSharp.D2D1.Tests;

/// <summary>
/// A test class for the diagnostic suppressor for D2D1 resource texture fields being left uninitialized.
/// </summary>
internal sealed partial class D2D1ResourceTextureUninitializedFieldDiagnosticSuppressorTests
{
    [D2DInputCount(0)]
    [D2DGeneratedShaderMarshaller]
    public readonly partial struct MyShader : ID2D1PixelShader
    {
        // This test just needs to validate the project builds fine with this shader.
        // If the diagnostic suppressor wasn't working, it'd fail to build because
        // Roslyn would emit a CS0649 warning, which would be treated as an error.
        [D2DResourceTextureIndex(0)]
        private readonly D2D1ResourceTexture1D<float4> texture;

        public float4 Execute()
        {
            return this.texture[0];
        }
    }
}