#pragma warning disable IDE0048

namespace ComputeSharp.SwapChain.Shaders.Compute;

/// <summary>
/// Constructing some concise contoured layers, then applying various edge and shading effects to produce some faux depth.
/// Ported from <see href="https://www.shadertoy.com/view/3lj3zt"/>.
/// <para>Created by Shane.</para>
/// </summary>
[AutoConstructor]
internal readonly partial struct ContouredLayers : IComputeShader
{
    /// <summary>
    /// The target texture.
    /// </summary>
    private readonly IReadWriteNormalizedTexture2D<float4> destination;

    /// <summary>
    /// The current time Hlsl.Since the start of the application.
    /// </summary>
    private readonly float time;

    /// <summary>
    /// The sampling texture.
    /// </summary>
    private readonly IReadOnlyNormalizedTexture2D<float4> texture;

    // float3 to float hash.
    private static float Hash21(float2 p)
    {
        return Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(p, new float2(41, 289))) * 45758.5453f);
    }

    // float2 to float2 hash.
    private float2 Hash22(float2 p)
    {
        float n = Hlsl.Sin(Hlsl.Dot(p, new float2(1, 113)));

        p = Hlsl.Frac(new float2(262144, 32768) * n);

        return Hlsl.Sin(p * 6.2831853f + time);
    }

    // float2 to float2 hash.
    private static float2 Hash22B(float2 p)
    {
        float n = Hlsl.Sin(Hlsl.Dot(p, new float2(41, 289)));

        return Hlsl.Frac(new float2(262144, 32768) * n) * 2.0f - 1.0f;
    }

    // Smooth 2D noise
    private static float Noise2D(float2 p)
    {
        float2 i = Hlsl.Floor(p);

        p -= i;

        float4 v = default;

        v.X = Hlsl.Dot(Hash22B(i), p);
        v.Y = Hlsl.Dot(Hash22B(i + float2.UnitX), p - float2.UnitX);
        v.Z = Hlsl.Dot(Hash22B(i + float2.UnitY), p - float2.UnitY);
        v.W = Hlsl.Dot(Hash22B(i + 1.0f), p - 1.0f);

        p = p * p * (3.0f - 2.0f * p);

        return Hlsl.Lerp(Hlsl.Lerp(v.X, v.Y, p.X), Hlsl.Lerp(v.Z, v.W, p.X), p.Y);
    }

    // Based on IQ's gradient noise formula.
    private float Noise2D3G(float2 p)
    {
        float2 i = Hlsl.Floor(p);

        p -= i;

        float4 v = default;

        v.X = Hlsl.Dot(Hash22(i), p);
        v.Y = Hlsl.Dot(Hash22(i + float2.UnitX), p - float2.UnitX);
        v.Z = Hlsl.Dot(Hash22(i + float2.UnitY), p - float2.UnitY);
        v.W = Hlsl.Dot(Hash22(i + 1.0f), p - 1.0f);

        p = p * p * p * (p * (p * 6.0f - 15.0f) + 10.0f);

        return Hlsl.Lerp(Hlsl.Lerp(v.X, v.Y, p.X), Hlsl.Lerp(v.Z, v.W, p.X), p.Y);
    }

    // Map function with noise layers
    private float MapNoise(float3 p, float i)
    {
        return Noise2D3G(p.XY * 3.0f) * 0.66f + Noise2D3G(p.XY * 6.0f) * 0.34f + i / 10.0f * 1.0f - 0.15f;
    }

    // 2D derivative function.
    private float3 GetNormal(in float3 p, float m, float i)
    {
        float2 e = new(0.001f, 0);

        return new float3(m - MapNoise(p - e.XYY, i), m - MapNoise(p - e.YXY, i), 0.0f) / e.X * 1.4142f;
    }

    // The map layer and its derivative
    private float4 MapLayer(in float3 p, float i)
    {
        float4 d = default;

        d.X = MapNoise(p, i);
        d.YZW = GetNormal(p, d.X, i);

        return d;
    }

    // Layer color. Based on the shade, layer number and smoothing factor.
    private float3 GetColor(float2 p, float sh, float fi)
    {
        float3 tx = texture.Sample(p + Hash21(new float2(sh, fi))).XYZ;

        tx *= tx;

        float3 col;

        col = (float3)1.0 * (1.0f - 0.75f / (1.0f + sh * sh * 2.0f));
        col = Hlsl.Min(col * tx * 3.0f, 1.0f);

        return col;
    }

    // A hatch-like algorithm, or a stipple... or some kind of textured pattern.
    private float Hatch(float2 p, float res)
    {
        p *= res / 16.0f;

        float hatch = Hlsl.Clamp(Hlsl.Sin((p.X - p.Y) * 3.14159f * 200.0f) * 2.0f + 0.5f, 0.0f, 1.0f);

        float hRnd = Hash21(Hlsl.Floor(p * 6.0f) + 0.73f);
        if (hRnd > 0.66f) hatch = hRnd;

        hatch = hatch * 0.75f + 0.5f;

        return hatch;
    }

    /// <inheritdoc/>
    public void Execute()
    {
        float2 fragCoord = new(ThreadIds.X, DispatchSize.Y - ThreadIds.Y);
        float res = Hlsl.Min(DispatchSize.Y, 700.0f);
        float2 uv = (fragCoord - (float2)DispatchSize.XY * 0.5f) / res;
        float sf = 1.0f / DispatchSize.Y;
        float3 col = GetColor(uv, 0.0f, 0.0f);
        float pL = 0.0f;
        float hatch = Hatch(uv, res);

        col *= hatch;

        const int lNum = 5;
        float flNum = lNum;

        for (int i = 0; i < lNum; i++)
        {
            float fi = i;

            hatch = Hatch(uv + Hlsl.Sin(new float2(41.0f, 289.0f) * (fi + 1.0f)), res);

            float4 c = MapLayer(new float3(uv, 1.0f), fi);
            float4 cSh = MapLayer(new float3(uv - new float2(0.03f, -0.03f) * ((flNum - fi) / flNum * 0.5f + 0.5f), 1.0f), fi);
            float sh = (fi + 1.0f) / (flNum);
            float3 lCol = GetColor(uv, sh, fi + 1.0f);
            float3 ld = Hlsl.Normalize(new float3(-1, 1, -0.25f));
            float3 n = Hlsl.Normalize(new float3(0.0f, 0.0f, -1.0f) + c.YZW);
            float diff = Hlsl.Max(Hlsl.Dot(ld, n), 0.0f);

            diff *= 2.5f;

            float3 eCol = lCol * (diff + 1.0f);
            float sfL = sf * Hlsl.Length(c.YZX) * 2.0f;
            float sfLSh = sf * Hlsl.Length(cSh.YZX) * 6.0f;
            const float shF = 0.5f;

            col = Hlsl.Lerp(col, float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sfLSh, Hlsl.Max(cSh.X, pL))) * shF);
            col = Hlsl.Lerp(col, float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sfL * 3.0f, c.X)) * 0.25f);
            col = Hlsl.Lerp(col, float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sfL, c.X)) * 0.85f);
            col = Hlsl.Lerp(col, eCol * hatch, (1.0f - Hlsl.SmoothStep(0.0f, sfL, c.X + Hlsl.Length(c.YZX) * 0.003f)));
            col = Hlsl.Lerp(col, lCol * hatch, (1.0f - Hlsl.SmoothStep(0.0f, sfL, c.X + Hlsl.Length(c.YZX) * 0.006f)));

            pL = c.X;
        }

        col *= Hlsl.Lerp(new float3(1.8f, 1, 0.7f).ZYX, new float3(1.8f, 1, 0.7f).XZY, Noise2D(uv * 2.0f));

        float3 rn3 = Noise2D(uv * DispatchSize.Y / 1.0f + 1.7f) - Noise2D(uv * DispatchSize.Y / 1.0f + 3.4f);

        col *= 0.93f + 0.07f * rn3.XYZ + 0.07f * Hlsl.Dot(rn3, new float3(0.299f, 0.587f, 0.114f));

        uv = fragCoord / DispatchSize.XY;

        col *= Hlsl.Pow(16.0f * uv.X * uv.Y * (1.0f - uv.X) * (1.0f - uv.Y), 0.0625f);

        destination[ThreadIds.XY] = new float4(Hlsl.Sqrt(Hlsl.Max(col, 0.0f)), 1);
    }
}