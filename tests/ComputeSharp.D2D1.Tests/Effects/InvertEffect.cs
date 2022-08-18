namespace ComputeSharp.D2D1.Tests.Effects;

[D2DInputCount(1)]
[D2DInputSimple(0)]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
public partial struct InvertEffect : ID2D1PixelShader
{
    /// <inheritdoc/>
    public float4 Execute()
    {
        float4 color = D2D.GetInput(0);
        float3 rgb = Hlsl.Saturate(1.0f - color.RGB);

        return new(rgb, 1);
    }
}
