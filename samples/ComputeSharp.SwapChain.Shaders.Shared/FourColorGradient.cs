namespace ComputeSharp.SwapChain.Shaders;

/// <summary>
/// A shader which interpolates four different colors into a rotating gradient pattern.
/// Ported from <see href="https://www.shadertoy.com/view/7lc3R4"/>.
/// <para>Created by Inigo Quilez.</para>
/// <para>License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.</para>
/// </summary>
[AutoConstructor]
#if SAMPLE_APP
[EmbeddedBytecode(DispatchAxis.XY)]
#endif
internal readonly partial struct FourColorGradient : IPixelShader<float4>
{
    /// <summary>
    /// The current time since the start of the application.
    /// </summary>
    public readonly float time;

    // Colors to blend together
    private static readonly float3 colorOne = new(0.999f, 0.999f, 0.999f);
    private static readonly float3 colorTwo = new(0.999f, 0f, 0f);
    private static readonly float3 colorThree = new(0f, 0.999f, 0f);
    private static readonly float3 colorFour = new(0f, 0f, 0.999f);

    /// <summary>
    /// Standard 2D rotation formula.
    /// </summary>
    private static float2x2 Rotate(float a)
    {
        float c = Hlsl.Cos(a);
        float s = Hlsl.Sin(a);

        return new(c, s, -s, c);
    }

    /// <summary>
    /// Generate a hash from a vector.
    /// </summary>
    /// <param name="p">Vector to hash.</param>
    /// <returns>Hashed vector.</returns>
    private static float2 Hash(float2 p)
    {
        p = new float2(Hlsl.Dot(p, new float2(2127.1f, 81.17f)), Hlsl.Dot(p, new float2(1269.5f, 283.37f)));

        return Hlsl.Frac(Hlsl.Sin(p) * 43758.5453f);
    }

    /// <summary>
    /// Generate a smoothed noise pattern.
    /// </summary>
    /// <param name="p">Position vector.</param>
    /// <returns>Depth value.</returns>
    private static float Noise(float2 p)
    {
        float2 i = Hlsl.Floor(p);
        float2 f = Hlsl.Frac(p);
        float2 u = f * f * (3.0f - 2.0f * f);

        // Mix -> Lerp
        float n = Hlsl.Lerp(
            Hlsl.Lerp(
                Hlsl.Dot(-1.0f + 2.0f * Hash(i + new float2(0.0f, 0.0f)), f - new float2(0.0f, 0.0f)),
                Hlsl.Dot(-1.0f + 2.0f * Hash(i + new float2(1.0f, 0.0f)), f - new float2(1.0f, 0.0f)),
                u.X),
            Hlsl.Lerp(
                Hlsl.Dot(-1.0f + 2.0f * Hash(i + new float2(0.0f, 1.0f)), f - new float2(0.0f, 1.0f)),
                Hlsl.Dot(-1.0f + 2.0f * Hash(i + new float2(1.0f, 1.0f)), f - new float2(1.0f, 1.0f)),
                u.X),
            u.Y);

        return 0.5f + 0.5f * n;
    }


    /// <inheritdoc/>
    public float4 Execute()
    {
        float2 fragCoord = new(ThreadIds.X, DispatchSize.Y - ThreadIds.Y);
        float2 uv = fragCoord / DispatchSize.XY;
        float ratio = DispatchSize.X / DispatchSize.Y;
        float2 tuv = uv;

        tuv -= 0.5f;

        // Rotate with noise
        float degree = Noise(new float2(time * 0.1f, tuv.X * tuv.Y));

        tuv.Y *= 1.0f / ratio;
        tuv.XY = Hlsl.Mul(tuv.XY, Rotate(Hlsl.Radians((degree - 0.5f) * 720.0f + 180.0f)));
        tuv.Y *= ratio;

        // Wave warp with sin
        float frequency = 5.0f;
        float amplitude = 30.0f;
        float speed = time * 2.0f;

        tuv.X += Hlsl.Sin(tuv.Y * frequency + speed) / amplitude;
        tuv.Y += Hlsl.Sin(tuv.X * frequency * 1.5f + speed) / (amplitude * 0.5f);

        // Draw the image
        float3 layer1 = Hlsl.Lerp(colorOne, colorTwo, Hlsl.SmoothStep(-0.3f, 0.2f, Hlsl.Mul(tuv.XY, Rotate(Hlsl.Radians(-5.0f))).X));
        float3 layer2 = Hlsl.Lerp(colorThree, colorFour, Hlsl.SmoothStep(-0.3f, 0.2f, Hlsl.Mul(tuv.XY, Rotate(Hlsl.Radians(-5.0f))).X));
        float3 col = Hlsl.Lerp(layer1, layer2, Hlsl.SmoothStep(0.5f, -0.3f, tuv.Y));

        return new(col, 1.0f);
    }
}