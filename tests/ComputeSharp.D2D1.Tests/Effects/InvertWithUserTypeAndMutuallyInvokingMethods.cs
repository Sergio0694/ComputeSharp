#pragma warning disable IDE0290, CA1822

namespace ComputeSharp.D2D1.Tests.Effects;

public struct ColorWrapperUsingExternalType
{
    private float4 value;

    public ColorWrapperUsingExternalType(float4 value)
    {
        this.value = value;
    }

    public readonly float4 Invert()
    {
        ColorProcessor processor = default;

        return processor.Invert(this.value);
    }

    public readonly float4 Invert(float4 value)
    {
        return new(Hlsl.Saturate(1.0f - value.RGB), 1);
    }
}

public struct ColorProcessor
{
    public readonly float4 Invert(float4 value)
    {
        ColorWrapperUsingExternalType dummy = default;

        return dummy.Invert(value);
    }
}

[D2DInputCount(1)]
[D2DInputSimple(0)]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[D2DGeneratedPixelShaderDescriptor]
public partial struct InvertWithUserTypeAndMutuallyInvokingMethods : ID2D1PixelShader
{
    /// <inheritdoc/>
    public float4 Execute()
    {
        ColorWrapperUsingExternalType wrapper = new(D2D.GetInput(0));

        return wrapper.Invert();
    }
}