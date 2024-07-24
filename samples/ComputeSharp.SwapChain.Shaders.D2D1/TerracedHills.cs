using ComputeSharp.D2D1;

namespace ComputeSharp.SwapChain.Shaders.D2D1;

/// <summary>
/// An abstract representation of some terraced hills. In essence, just a very basic terrain layout with a small code footprint.
/// Ported from <see href="https://www.shadertoy.com/view/MtdSRn"/>.
/// <para>Created by Shane.</para>
/// </summary>
/// <param name="time">The current time since the start of the application.</param>
/// <param name="dispatchSize">The dispatch size for the current output.</param>
[D2DInputCount(0)]
[D2DRequiresScenePosition]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[D2DGeneratedPixelShaderDescriptor]
internal readonly partial struct TerracedHills(float time, int2 dispatchSize) : ID2D1PixelShader
{
    // 2x2 matrix rotation. Angle vector, courtesy of Fabrice
    private static float2x2 Rot2(float th)
    {
        float2 a = Hlsl.Sin(new float2(1.5707963f, 0) + th);

        return new(a.X, -a.Y, a.Y, a.X);
    }

    // The triangle function that Shadertoy user Nimitz has used in various triangle noise demonstrations
    private static float Tri(in float x)
    {
        return Hlsl.Abs(x - Hlsl.Floor(x) - 0.5f);
    }

    // 2D variant of the triangle function
    private static float2 Tri(in float2 x)
    {
        return Hlsl.Abs(x - Hlsl.Floor(x) - 0.5f);
    }

    // A simple noisey layer made up of a sawtooth combination
    private static float HLayer(float2 p)
    {
        return Hlsl.Dot(Tri((p / 1.5f) + Tri((p.YX / 3.0f) + 0.25f)), 1);
    }

    // Combine layers to create surfaces to render
    private static float HMap(float2 p)
    {
        float ret = 0, a = 1;

        for (int i = 0; i < 3; i++)
        {
            ret += Hlsl.Abs(a) * HLayer(p / a);
            p = new float2x2(0.866025f, -0.57735f, 0.57735f, 0.866025f) * p;
            a *= -0.3f;
        }

        ret = Hlsl.SmoothStep(-0.2f, 1.0f, ret * ret / 1.39f / 1.39f);

        return (ret * 0.975f) + (Tri(ret * 12.0f) * 0.05f);
    }

    // Distance function. A flat plane perturbed by a height function of some kind.
    private static float Map(float3 p)
    {
        return (p.Y - (HMap(p.XZ) * 0.35f)) * 0.75f;
    }

    // Tetrahedral normal - courtesy of IQ
    private static float3 Normal(in float3 p, ref float edge, float dispatchHeight)
    {
        float2 e = new float2(-1.0f, 1.0f) * 0.5f / dispatchHeight;
        float d1 = Map(p + e.YXX), d2 = Map(p + e.XXY);
        float d3 = Map(p + e.XYX), d4 = Map(p + e.YYY);
        float d = Map(p);

        edge = Hlsl.Abs(d1 + d2 + d3 + d4 - (d * 4.0f));
        edge = Hlsl.SmoothStep(0.0f, 1.0f, Hlsl.Sqrt(edge / e.Y * 2.0f));
        e = new float2(-1.0f, 1.0f) * 0.001f;
        d1 = Map(p + e.YXX);
        d2 = Map(p + e.XXY);
        d3 = Map(p + e.XYX);
        d4 = Map(p + e.YYY);

        return Hlsl.Normalize((e.YXX * d1) + (e.XXY * d2) + (e.XYX * d3) + (e.YYY * d4));
    }

    /// <inheritdoc/>
    public float4 Execute()
    {
        int2 xy = (int2)D2D.GetScenePosition().XY;
        int2 coordinate = new(xy.X, dispatchSize.Y - xy.Y);

        float3 rd = Hlsl.Normalize(new float3((float2)coordinate - (dispatchSize.Y * 0.5f), dispatchSize.Y));

        rd.YZ = Rot2(0.35f) * rd.YZ;

        float3 ro = new(time * 0.4f, 0.5f, time * 0.2f);

        float t = 0, d;
        for (int i = 0; i < 96; i++)
        {
            d = Map(ro + (rd * t));

            if (Hlsl.Abs(d) < 0.001f * ((t * 0.125f) + 1.0f) || t > 20.0f)
            {
                break;
            }

            t += ((Hlsl.Step(1.0f, t) * 0.3f) + 0.7f) * d;
        }

        float3 sp = ro + (rd * t);
        float3 ld = new(-0.676f, 0.408f, 0.613f);
        float edge = 0;
        float3 n = Normal(sp, ref edge, dispatchSize.Y);
        float dif = Hlsl.Max(Hlsl.Dot(ld, n), 0.0f);
        float spe = Hlsl.Pow(Hlsl.Max(Hlsl.Dot(Hlsl.Reflect(rd, n), ld), 0.0f), 16.0f);
        float sh = HMap(sp.XZ);
        float rnd = (Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(Hlsl.Floor(sp.XZ * 512.0f), new float2(41.73f, 289.67f))) * 43758.5453f) * 0.5f) + 0.5f;
        float3 fog = Hlsl.Lerp(new float3(0.75f, 0.77f, 0.78f), new float3(1.04f, 0.95f, 0.87f), Hlsl.Pow(1.0f + Hlsl.Dot(rd, ld), 3.0f) * 0.35f);
        float3 c = Hlsl.Lerp(((new float3(1.1f, 1.05f, 1) * rnd * (dif + 0.1f) * sh) + (fog * spe)) * (1.0f - (edge * 0.7f)), fog * fog, Hlsl.Min(1.0f, t * 0.3f));

        return new(c, 1);
    }
}