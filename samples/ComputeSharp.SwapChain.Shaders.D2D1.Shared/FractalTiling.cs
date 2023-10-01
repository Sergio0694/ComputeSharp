using ComputeSharp.D2D1;

namespace ComputeSharp.SwapChain.Shaders.D2D1;

/// <summary>
/// A shader creating a fractal tiling animation.
/// Ported from <see href="https://www.shadertoy.com/view/Ml2GWy"/>.
/// <para>Created by Inigo Quilez.</para>
/// <para>License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.</para>
/// </summary>
[D2DInputCount(0)]
[D2DRequiresScenePosition]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[D2DGeneratedPixelShaderDescriptor]
[AutoConstructor]
internal readonly partial struct FractalTiling : ID2D1PixelShader
{
    /// <summary>
    /// The current time since the start of the application.
    /// </summary>
    private readonly float time;

    /// <summary>
    /// The dispatch size for the current output.
    /// </summary>
    private readonly int2 dispatchSize;

    /// <inheritdoc/>
    public float4 Execute()
    {
        int2 xy = (int2)D2D.GetScenePosition().XY;
        float2 position = (((float2)(256 * xy)) / this.dispatchSize.X) + this.time;
        float4 color = 0;

        for (int i = 0; i < 6; i++)
        {
            float2 a = Hlsl.Floor(position);
            float2 b = Hlsl.Frac(position);
            float4 w = Hlsl.Frac(
                (Hlsl.Sin((a.X * 7) + (31.0f * a.Y) + (0.01f * this.time)) +
                 new float4(0.035f, 0.01f, 0, 0.7f))
                 * 13.545317f);

            color.XYZ += w.XYZ *
                   2.0f * Hlsl.SmoothStep(0.45f, 0.55f, w.W) *
                   Hlsl.Sqrt(16.0f * b.X * b.Y * (1.0f - b.X) * (1.0f - b.Y));

            position /= 2.0f;
            color /= 2.0f;
        }

        color.XYZ = Hlsl.Pow(color.XYZ, new float3(0.7f, 0.8f, 0.5f));
        color.W = 1.0f;

        return color;
    }
}