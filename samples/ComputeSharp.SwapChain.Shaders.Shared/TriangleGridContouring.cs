namespace ComputeSharp.SwapChain.Shaders;

/// <summary>
/// Utilizing a 2D simplex grid to construct the isolines of a noise function.
/// Ported from <see href="https://www.shadertoy.com/view/WtfGDX"/>.
/// <para>Created by Shane.</para>
/// </summary>
[AutoConstructor]
#if SAMPLE_APP
[EmbeddedBytecode(DispatchAxis.XY)]
#endif
internal readonly partial struct TriangleGridContouring : IPixelShader<float4>
{
    /// <summary>
    /// The current time Hlsl.Since the start of the application.
    /// </summary>
    private readonly float time;

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
    private float2 Hash22(float2 p)
    {
        float n = Hlsl.Sin(Hlsl.Dot(p, new float2(41, 289)));

        p = Hlsl.Frac(new float2(262144, 32768) * n);

        return Hlsl.Sin((p * 6.2831853f) + time);
    }

    // Based on IQ's gradient noise formula.
    private float N2D3G(float2 p)
    {
        float2 i = Hlsl.Floor(p);

        p -= i;

        float4 v = default;

        v.X = Hlsl.Dot(Hash22(i), p);
        v.Y = Hlsl.Dot(Hash22(i + float2.UnitX), p - float2.UnitX);
        v.Z = Hlsl.Dot(Hash22(i + float2.UnitY), p - float2.UnitY);
        v.W = Hlsl.Dot(Hash22(i + 1.0f), p - 1.0f);

        p = p * p * p * ((p * ((p * 6.0f) - 15.0f)) + 10.0f);

        return Hlsl.Lerp(Hlsl.Lerp(v.X, v.Y, p.X), Hlsl.Lerp(v.Z, v.W, p.X), p.Y);
    }

    // The isofunction. Just a Hlsl.Single noise function, but it can be more elaborate.
    private float IsoFunction(in float2 p)
    {
        return N2D3G((p / 4.0f) + 0.07f);
    }

    // Unsigned distance to the segment joining "a" and "b".
    private static float DistLine(float2 a, float2 b)
    {
        b = a - b;

        float h = Hlsl.Clamp(Hlsl.Dot(a, b) / Hlsl.Dot(b, b), 0.0f, 1.0f);

        return Hlsl.Length(a - (b * h));
    }

    // Based on IQ's signed distance to the segment joining "a" and "b".
    private static float DistEdge(float2 a, float2 b)
    {
        return Hlsl.Dot((a + b) * 0.5f, Hlsl.Normalize((b - a).YX * new float2(-1, 1)));
    }

    // Interpolating along the edge connecting vertices v1 and v2 with respect to the isovalue.
    private static float2 Interpolate(in float2 p1, in float2 p2, float v1, float v2, float isovalue)
    {
        return Hlsl.Lerp(p1, p2, ((isovalue - v1) / (v2 - v1) * 0.75f) + (0.25f / 2.0f));
    }

    // Isoline function.
    private static int IsoLine(
        float3 n3,
        float2 ip0,
        float2 ip1,
        float2 ip2,
        float isovalue,
        float i,
        ref float2 p0,
        ref float2 p1)
    {
        p0 = 1e5f;
        p1 = 1e5f;

        int iTh = 0;

        if (n3.X > isovalue) iTh += 4;
        if (n3.Y > isovalue) iTh += 2;
        if (n3.Z > isovalue) iTh += 1;

        if (iTh == 1 || iTh == 6)
        {
            p0 = Interpolate(ip1, ip2, n3.Y, n3.Z, isovalue);
            p1 = Interpolate(ip2, ip0, n3.Z, n3.X, isovalue);

        }
        else if (iTh == 2 || iTh == 5)
        {
            p0 = Interpolate(ip0, ip1, n3.X, n3.Y, isovalue);
            p1 = Interpolate(ip1, ip2, n3.Y, n3.Z, isovalue);

        }
        else if (iTh == 3 || iTh == 4)
        {
            p0 = Interpolate(ip0, ip1, n3.X, n3.Y, isovalue);
            p1 = Interpolate(ip2, ip0, n3.Z, n3.X, isovalue);
        }

        if (iTh >= 4 && iTh <= 6)
        {
            float2 tmp = p0;

            p0 = p1;
            p1 = tmp;
        }

        if (i == 0.0f)
        {
            float2 tmp = p0;

            p0 = p1;
            p1 = tmp;
        }

        return iTh;
    }

    private float3 SimplexContour(float2 p)
    {
        const float gSc = 8.0f;

        p *= gSc;

        float2 oP = p;

        p += new float2(N2D3G(p * 3.5f), N2D3G((p * 3.5f) + 7.3f)) * 0.015f;

        float2 s = Hlsl.Floor(p + ((p.X + p.Y) * 0.36602540378f));

        p -= s - ((s.X + s.Y) * 0.211324865f);

        float i = p.X < p.Y ? 1.0f : 0.0f;
        float2 ioffs = new(1.0f - i, i);
        float2 ip0 = 0;
        float2 ip1 = ioffs - 0.2113248654f;
        float2 ip2 = 0.577350269f;
        float2 ctr = (ip0 + ip1 + ip2) / 3.0f;

        ip0 -= ctr;
        ip1 -= ctr;
        ip2 -= ctr;
        p -= ctr;

        float3 n3 = default;

        n3.X = IsoFunction(s);
        n3.Y = IsoFunction(s + ioffs);
        n3.Z = IsoFunction(s + 1.0f);

        float d = 1e5f;
        float d2 = 1e5f;
        float d3 = 1e5f;
        float d4 = 1e5f;
        float d5 = 1e5f;
        float isovalue = 0;
        float2 p0 = default;
        float2 p1 = default;
        int iTh = IsoLine(n3, ip0, ip1, ip2, isovalue, i, ref p0, ref p1);

        d = Hlsl.Min(d, DistEdge(p - p0, p - p1));

        if (iTh == 7)
        {
            d = 0;
        }

        d3 = Hlsl.Min(d3, DistLine((p - p0), (p - p1)));
        d4 = Hlsl.Min(d4, Hlsl.Min(Hlsl.Length(p - p0), Hlsl.Length(p - p1)));

        float tri = Hlsl.Min(Hlsl.Min(DistLine(p - ip0, p - ip1), DistLine(p - ip1, p - ip2)), DistLine(p - ip2, p - ip0));

        d5 = Hlsl.Min(d5, tri);
        d5 = Hlsl.Min(d5, Hlsl.Length(p) - 0.02f);
        isovalue = -0.15f;

        int iTh2 = IsoLine(n3, ip0, ip1, ip2, isovalue, i, ref p0, ref p1);

        d2 = Hlsl.Min(d2, DistEdge(p - p0, p - p1));

        if (iTh2 == 7) d2 = 0.0f;
        if (iTh == 7) d2 = 1e5f;

        d2 = Hlsl.Max(d2, -d);
        d3 = Hlsl.Min(d3, DistLine((p - p0), (p - p1)));
        d4 = Hlsl.Min(d4, Hlsl.Min(Hlsl.Length(p - p0), Hlsl.Length(p - p1)));
        d4 -= 0.075f;
        d3 -= 0.0125f;
        d /= gSc;
        d2 /= gSc;
        d3 /= gSc;
        d4 /= gSc;
        d5 /= gSc;

        float3 col = new(1, 0.85f, 0.6f);
        float sf = 0.004f;

        if (d > 0.0f && d2 > 0.0f) col = new float3(1, 1.8f, 3) * 0.45f;
        if (d > 0.0f) col = Hlsl.Lerp(col, new float3(1, 1.85f, 3) * 0.3f, (1.0f - Hlsl.SmoothStep(0.0f, sf, d2 - 0.012f)));

        col = Hlsl.Lerp(col, new float3(1.1f, 0.85f, 0.6f), (1.0f - Hlsl.SmoothStep(0.0f, sf, d2)));
        col = Hlsl.Lerp(col, new float3(1.5f, 0.9f, 0.6f) * 0.6f, (1.0f - Hlsl.SmoothStep(0.0f, sf, d - 0.012f)));
        col = Hlsl.Lerp(col, new float3(1, 0.8f, 0.6f) * new float3(0.7f, 1.0f, 0.75f) * 0.95f, (1.0f - Hlsl.SmoothStep(0.0f, sf, d)));

        if (d2 > 0.0f) col *= ((Hlsl.Abs(Hlsl.Dot(n3, float3.One)) * 1.25f) + 1.25f) / 2.0f;
        else col *= Hlsl.Max(2.0f - ((Hlsl.Dot(n3, float3.One) + 1.45f) / 1.25f), 0.0f);

        float pat = Hlsl.Abs(Hlsl.Frac((tri * 12.5f) + 0.4f) - 0.5f) * 2.0f;

        col *= (pat * 0.425f) + 0.75f;
        col = Hlsl.Lerp(col, float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sf, d5)) * 0.95f);
        col = Hlsl.Lerp(col, float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sf, d3)));
        col = Hlsl.Lerp(col, float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sf, d4)));
        col = Hlsl.Lerp(col, float3.One, (1.0f - Hlsl.SmoothStep(0.0f, sf, d4 + 0.005f)));

        float2 q = oP * 1.5f;

        col = Hlsl.Min(col, 1.0f);

        float gr = Hlsl.Sqrt(Hlsl.Dot(col, new float3(0.299f, 0.587f, 0.114f))) * 1.25f;
        float ns = (((N2D3G(q * 4.0f * new float2(1.0f / 3.0f, 3)) * 0.64f) + (N2D3G(q * 8.0f * new float2(1.0f / 3.0f, 3)) * 0.34f)) * 0.5f) + 0.5f;

        ns = gr - ns;
        q = Hlsl.Mul(q, Rotate2x2(3.14159f / 3.0f));

        float ns2 = (((N2D3G(q * 4.0f * new float2(1.0f / 3.0f, 3)) * 0.64f) + (N2D3G(q * 8.0f * new float2(1.0f / 3.0f, 3)) * 0.34f)) * 0.5f) + 0.5f;

        ns2 = gr - ns2;
        ns = Hlsl.SmoothStep(0.0f, 1.0f, Hlsl.Min(ns, ns2));
        col = Hlsl.Lerp(col, col * (ns + 0.35f), 0.4f);

        return col;
    }

    /// <inheritdoc/>
    public float4 Execute()
    {
        int2 coordinate = new(ThreadIds.X, DispatchSize.Y - ThreadIds.Y);
        float2 uv = (coordinate - ((float2)DispatchSize.XY * 0.5f)) / Hlsl.Min(650.0f, DispatchSize.Y);
        float2 p = Hlsl.Mul(Rotate2x2(3.14159f / 12.0f), uv) + (new float2(0.8660254f, 0.5f) * time / 16.0f);
        float3 col = SimplexContour(p);

        uv = coordinate / (float2)DispatchSize.XY;
        col *= Hlsl.Pow(16.0f * uv.X * uv.Y * (1.0f - uv.X) * (1.0f - uv.Y), 0.0625f) + 0.1f;

        return new(Hlsl.Sqrt(Hlsl.Max(col, 0.0f)), 1);
    }
}