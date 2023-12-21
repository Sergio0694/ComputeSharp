#pragma warning disable IDE0290

namespace ComputeSharp.D2D1.Tests.Effects;

public struct ColorWrapper
{
    private float4 value;

    public ColorWrapper(float4 value)
    {
        this.value = value;
    }

    public float4 Invert()
    {
        return new(Hlsl.Saturate(1.0f - this.value.RGB), 1);
    }
}

[D2DInputCount(1)]
[D2DInputSimple(0)]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[D2DGeneratedPixelShaderDescriptor]
public partial struct InvertWithUserTypeWithConstructorEffect : ID2D1PixelShader
{
    /// <inheritdoc/>
    public float4 Execute()
    {
        ColorWrapper wrapper = new(D2D.GetInput(0));

        return wrapper.Invert();
    }
}