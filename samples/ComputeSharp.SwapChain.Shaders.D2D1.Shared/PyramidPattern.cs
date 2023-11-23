using ComputeSharp.D2D1;

namespace ComputeSharp.SwapChain.Shaders.D2D1;

/// <summary>
/// An offset grid of square-based pyramids whose tips have been offset according to an underlying directional noise field.
/// The faux lighting is provided via bump mapping. No raymarching was harmed during the making of this example.
/// Ported from <see href="https://www.shadertoy.com/view/wlscWX"/>.
/// <para>Created by Shane.</para>
/// </summary>
/// <param name="time">The current time since the start of the application.</param>
/// <param name="dispatchSize">The dispatch size for the current output.</param>
[D2DInputCount(0)]
[D2DRequiresScenePosition]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[D2DGeneratedPixelShaderDescriptor]
internal readonly partial struct PyramidPattern(float time, int2 dispatchSize) : ID2D1PixelShader
{
    // Standard 2D rotation formula.
    private static float2x2 Rotate2x2(in float a)
    {
        float c = Hlsl.Cos(a);
        float s = Hlsl.Sin(a);

        return new(c, -s, s, c);
    }

    // IQ's float2 to float hash.
    private static float Hash21(float2 p)
    {
        return Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(p, new float2(27.619f, 57.583f))) * 43758.5453f);
    }

    // float2 to float2 hash.
    private static float2 Hash22B(float2 p)
    {
        float n = Hlsl.Sin(Hlsl.Dot(p, new float2(41, 289)));

        return (Hlsl.Frac(new float2(262144, 32768) * n) * 2.0f) - 1.0f;
    }

    // Based on IQ's gradient noise formula.
    private static float N2D3G(float2 p)
    {
        float2 i = Hlsl.Floor(p);

        p -= i;

        float4 v = default;

        v.X = Hlsl.Dot(Hash22B(i), p);
        v.Y = Hlsl.Dot(Hash22B(i + float2.UnitX), p - float2.UnitX);
        v.Z = Hlsl.Dot(Hash22B(i + float2.UnitY), p - float2.UnitY);
        v.W = Hlsl.Dot(Hash22B(i + 1.0f), p - 1.0f);

        p = p * p * (3.0f - (2.0f * p));

        return Hlsl.Lerp(Hlsl.Lerp(v.X, v.Y, p.X), Hlsl.Lerp(v.Z, v.W, p.X), p.Y);
    }

    // Two layers of noise.
    private static float FBM(float2 p)
    {
        return (N2D3G(p) * 0.66f) + (N2D3G(p * 2.0f) * 0.34f);
    }

    // HLSL mod from GLSL
    private static float Mod(float x, float y)
    {
        return x - (y * Hlsl.Floor(x / y));
    }

    private float BMap(float2 p)
    {
        p = Hlsl.Mul(p, Rotate2x2(-3.14159f / 5.0f));

        if (Mod(Hlsl.Floor(p.Y), 2.0f) < 0.5f)
        {
            p.X += 0.5f;
        }

        float2 ip = Hlsl.Floor(p);

        p -= ip + 0.5f;

        float ang = (-3.14159f * 3.0f / 5.0f) + (FBM((ip / 8.0f) + (time / 3.0f)) * 6.2831f * 2.0f);

        float2 offs = new float2(Hlsl.Cos(ang), Hlsl.Sin(ang)) * 0.35f;

        if (p.X < offs.X)
        {
            p.X = 1.0f - ((p.X + 0.5f) / Hlsl.Abs(offs.X + 0.5f));
        }
        else
        {
            p.X = (p.X - offs.X) / (0.5f - offs.X);
        }

        if (p.Y < offs.Y)
        {
            p.Y = 1.0f - ((p.Y + 0.5f) / Hlsl.Abs(offs.Y + 0.5f));
        }
        else
        {
            p.Y = (p.Y - offs.Y) / (0.5f - offs.Y);
        }

        return 1.0f - Hlsl.Max(p.X, p.Y);
    }

    // Standard function-based bump mapping function, with an edge value  included for good measure
    private float3 DoBumpMap(in float2 p, in float3 n, float bumpfactor, ref float edge)
    {
        float2 e = new(0.025f, 0);

        float f = BMap(p);
        float fx = BMap(p - e.XY);
        float fy = BMap(p - e.YX);
        float fx2 = BMap(p + e.XY);
        float fy2 = BMap(p + e.YX);

        float3 grad = new float3(fx - fx2, fy - fx2, 0) / e.X / 2.0f;

        edge = Hlsl.Length(new float2(fx, fy) + new float2(fx2, fy2) - (f * 2.0f));
        edge = Hlsl.SmoothStep(0.0f, 1.0f, edge / e.X);

        grad -= n * Hlsl.Dot(n, grad);

        return Hlsl.Normalize(n + (grad * bumpfactor));
    }

    // A hatch-like algorithm, or a stipple... or some kind of textured pattern
    private float DoHatch(float2 p, float res)
    {
        p *= res / 16.0f;

        float hatch = Hlsl.Clamp((Hlsl.Sin((p.X - p.Y) * 3.14159f * 200.0f) * 2.0f) + 0.5f, 0.0f, 1.0f);

        float hRnd = Hash21(Hlsl.Floor(p * 6.0f) + 0.73f);
        if (hRnd > 0.66f)
        {
            hatch = hRnd;
        }

        return hatch;
    }

    /// <inheritdoc/>
    public float4 Execute()
    {
        int2 xy = (int2)D2D.GetScenePosition().XY;
        int2 coordinate = new(xy.X, dispatchSize.Y - xy.Y);
        float iRes = Hlsl.Min(dispatchSize.Y, 800.0f);
        float2 uv = (coordinate - ((float2)dispatchSize * 0.5f)) / iRes;
        float3 rd = Hlsl.Normalize(new float3(uv, 0.5f));
        const float gSc = 10.0f;
        float2 p = (uv * gSc) + new float2(0, time / 2.0f);
        float2 oP = p;
        float m = BMap(p);
        float3 n = new(0, 0, -1);
        float edge = 0.0f;
        float bumpFactor = 0.25f;

        n = DoBumpMap(p, n, bumpFactor, ref edge);

        float3 lp = new float3(-0.0f + (Hlsl.Sin(time) * 0.3f), 0.0f + (Hlsl.Cos(time * 1.3f) * 0.3f), -1) - new float3(uv, 0);
        float lDist = Hlsl.Max(Hlsl.Length(lp), 0.001f);
        float3 ld = lp / lDist;
        float diff = Hlsl.Max(Hlsl.Dot(n, ld), 0.0f);

        diff = Hlsl.Pow(diff, 4.0f);

        float spec = Hlsl.Pow(Hlsl.Max(Hlsl.Dot(Hlsl.Reflect(-ld, n), -rd), 0.0f), 16.0f);
        float fre = Hlsl.Min(Hlsl.Pow(Hlsl.Max(1.0f + Hlsl.Dot(n, rd), 0.0f), 4.0f), 3.0f);
        float3 col = 0.15f * (diff + 0.251f + (spec * new float3(1, 0.7f, 0.3f) * 9.0f) + (fre * new float3(0.1f, 0.3f, 1) * 12.0f));
        float rf = Hlsl.SmoothStep(0.0f, 0.35f, (BMap(Hlsl.Reflect(rd, n).XY * 2.0f) * FBM(Hlsl.Reflect(rd, n).XY * 3.0f)) + 0.1f);

        col += col * col * rf * rf * new float3(1, 0.1f, 0.1f) * 15.0f;

        float shade = (m * 0.83f) + 0.17f;

        col *= shade;
        col *= 1.0f - (edge * 0.8f);

        float hatch = DoHatch(oP / gSc, iRes);

        col *= (hatch * 0.5f) + 0.7f;

        return new(Hlsl.Sqrt(Hlsl.Max(col, 0.0f)), 1);
    }
}