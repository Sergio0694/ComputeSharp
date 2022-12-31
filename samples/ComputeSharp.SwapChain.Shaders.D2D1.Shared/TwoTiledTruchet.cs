using ComputeSharp.D2D1;

namespace ComputeSharp.SwapChain.Shaders.D2D1;

/// <summary>
/// A shader creating an animated two-tiles Truchet arrangement.
/// Ported from <see href="https://www.shadertoy.com/view/tsSfWK"/>.
/// <para>Created by Shane.</para>
/// </summary>
[D2DInputCount(0)]
[D2DRequiresScenePosition]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[AutoConstructor]
internal readonly partial struct TwoTiledTruchet : ID2D1PixelShader
{
    /// <summary>
    /// The current time since the start of the application.
    /// </summary>
    private readonly float time;

    /// <summary>
    /// The dispatch size for the current output.
    /// </summary>
    private readonly int2 dispatchSize;

    /// <summary>
    /// Calculates the Truchet distance field.
    /// </summary>
    /// <param name="p">The input value to compute the distance field for.</param>
    /// <param name="ang">The resulting Truchet angle.</param>
    /// <returns>The distance field for the input value.</returns>
    private float2 DistanceField(float2 p, out float2 ang)
    {
        // Fast hash for a pair of floats
        static float Hash21(float2 p)
        {
            return Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(p, new float2(27.619f, 57.583f))) * 43758.5453f);
        }

        // HLSL's port of the GLSL mod intrinsic
        static float Mod(float x, float y)
        {
            return x - (y * Hlsl.Floor(x / y));
        }

        float2 ip2 = Hlsl.Floor(p / 2.0f);
        float2 ip = Hlsl.Floor(p);
        float rnd = Hash21(ip);
        float rnd3 = Hash21(ip + 0.57f);
        float2 d = 1e5f;

        p -= ip + 0.5f;

        if (Mod(ip2.X + ip2.Y, 2.0f) < 0.5f)
        {
            d = Hlsl.Abs(p);
            ang = p.YX;

            if (Mod(ip.X + 1.0f, 2.0f) < 0.5f)
            {
                ang.X *= -1.0f;

            }

            if (Mod(ip.Y + 1.0f, 2.0f) > 0.5f)
            {
                ang.Y *= -1.0f;
            }

            if (rnd3 < .5)
            {
                d = d.YX;
                ang = ang.YX;
            }

            ang += 0.5f;
            ang *= 3.0f;

        }
        else
        {
            if (rnd < .5)
            {
                p = p.YX * new float2(1, -1);
            }

            d.X = Hlsl.Length(p - 0.5f) - 0.5f;
            d.Y = Hlsl.Length(p + 0.5f) - 0.5f;

            d = Hlsl.Abs(d);

            ang = 0;
            ang.X = -Hlsl.Atan((p.Y - 0.5f) / (p.X - 0.5f));
            ang.Y = -Hlsl.Atan((p.Y + 0.5f) / (p.X + 0.5f));

            if (Mod(ip.X + ip.Y, 2.0f) < 0.5f)
            {
                ang *= -1.0f;
            }

            if (rnd < 0.5f)
            {
                ang *= -1.0f;
            }

            if (rnd3 < 0.5f)
            {
                d = d.YX;
                ang = ang.YX;
            }

            ang *= 4.0f / 6.2831853f;
            ang *= 2.0f;
        }

        ang = Hlsl.Frac(ang + (this.time / 4.0f));

        return d;
    }

    /// <inheritdoc/>
    public float4 Execute()
    {
        int2 xy = (int2)D2D.GetScenePosition().XY;
        float2 uv = (xy - ((float2)this.dispatchSize * 0.5f)) / this.dispatchSize.Y;
        float gSc = 7.0f;
        float2 p = (uv * gSc) - (new float2(-1, -0.5f) * this.time / 2.0f);
        float sf = 2.0f / this.dispatchSize.Y * gSc;
        float lSc = 6.0f;
        float lw = 1.0f / lSc / gSc;
        float2 d = DistanceField(p, out float2 ang) - (2.5f / lSc);
        float3 col = new(1.0f, 0.9f, 0.8f);

        for (int i = 0; i < 2; i++)
        {
            float di = d[i] - (lw / 4.0f);
            float tracks = Hlsl.Clamp(Hlsl.Sin((ang[i] * 6.2831f) + (this.time * 6.0f)) * 4.0f, 0.0f, 1.0f);
            float gap = 1.0f + lw;

            col = Hlsl.Lerp(col, 0, (1.0f - Hlsl.SmoothStep(0.0f, sf * 6.0f, di)) * 0.35f);
            col = Hlsl.Lerp(col, 0, 1.0f - Hlsl.SmoothStep(0.0f, sf, di));
            col = Hlsl.Lerp(col, new(1.0f, 0.9f, 0.8f), 1.0f - Hlsl.SmoothStep(0.0f, sf, di + (lw * 2.0f)));
            col = Hlsl.Lerp(col, 0, 1.0f - Hlsl.SmoothStep(0.0f, sf, di + (gap / lSc)));
            col = Hlsl.Lerp(col, 1, 1.0f - Hlsl.SmoothStep(0.0f, sf, di + (gap / lSc) + lw));
            col = Hlsl.Lerp(col, 0, 1.0f - Hlsl.SmoothStep(0.0f, sf, di + (2.0f * gap / lSc)));
            col = Hlsl.Lerp(col, 1 * tracks, 1.0f - Hlsl.SmoothStep(0.0f, sf, di + (2.0f * gap / lSc) + lw));
        }

        float3 rgb = Hlsl.Sqrt(Hlsl.Max(col, 0.0f));
        float4 color = new(rgb.X, rgb.Y, rgb.Z, 1.0f);

        return color;
    }
}