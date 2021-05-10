namespace ComputeSharp.SwapChain.Shaders
{
    /// <summary>
    /// Constructing some concise contoured layers, then applying various edge and shading effects to produce some faux depth.
    /// Ported from <see href="https://www.shadertoy.com/view/3lj3zt"/>.
    /// <para>Created by Shane.</para>
    /// </summary>
    [AutoConstructor]
    internal readonly partial struct ContouredLayers : IPixelShader<Float4>
    {
        /// <summary>
        /// The current time Hlsl.Since the start of the application.
        /// </summary>
        public readonly float time;

        /// <summary>
        /// The background texture to sample.
        /// </summary>
        public readonly IReadOnlyTexture2D<Float4> texture;

        // Float3 to float hash.
        private static float Hash21(Float2 p)
        {
            return Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(p, new Float2(41, 289))) * 45758.5453f);
        }

        // Float2 to Float2 hash.
        private Float2 Hash22(Float2 p)
        {
            float n = Hlsl.Sin(Hlsl.Dot(p, new Float2(1, 113)));

            p = Hlsl.Frac(new Float2(262144, 32768) * n);

            return Hlsl.Sin(p * 6.2831853f + time);
        }

        // Float2 to Float2 hash.
        private static Float2 Hash22B(Float2 p)
        {
            float n = Hlsl.Sin(Hlsl.Dot(p, new Float2(41, 289)));

            return Hlsl.Frac(new Float2(262144, 32768) * n) * 2.0f - 1.0f;
        }

        // Smooth 2D noise
        private static float Noise2D(Float2 p)
        {
            Float2 i = Hlsl.Floor(p);

            p -= i;

            Float4 v = default;

            v.X = Hlsl.Dot(Hash22B(i), p);
            v.Y = Hlsl.Dot(Hash22B(i + Float2.UnitX), p - Float2.UnitX);
            v.Z = Hlsl.Dot(Hash22B(i + Float2.UnitY), p - Float2.UnitY);
            v.W = Hlsl.Dot(Hash22B(i + 1.0f), p - 1.0f);

            p = p * p * (3.0f - 2.0f * p);

            return Hlsl.Lerp(Hlsl.Lerp(v.X, v.Y, p.X), Hlsl.Lerp(v.Z, v.W, p.X), p.Y);
        }

        // Based on IQ's gradient noise formula.
        private float Noise2D3G(Float2 p)
        {
            Float2 i = Hlsl.Floor(p);
            
            p -= i;

            Float4 v = default;

            v.X = Hlsl.Dot(Hash22(i), p);
            v.Y = Hlsl.Dot(Hash22(i + Float2.UnitX), p - Float2.UnitX);
            v.Z = Hlsl.Dot(Hash22(i + Float2.UnitY), p - Float2.UnitY);
            v.W = Hlsl.Dot(Hash22(i + 1.0f), p - 1.0f);

            p = p * p * p * (p * (p * 6.0f - 15.0f) + 10.0f);

            return Hlsl.Lerp(Hlsl.Lerp(v.X, v.Y, p.X), Hlsl.Lerp(v.Z, v.W, p.X), p.Y);
        }

        // Map function with noise layers
        private float MapNoise(Float3 p, float i)
        {
            return Noise2D3G(p.XY * 3.0f) * 0.66f + Noise2D3G(p.XY * 6.0f) * 0.34f + i / 10.0f * 1.0f - 0.15f;
        }

        // 2D derivative function.
        private Float3 GetNormal(in Float3 p, float m, float i)
        {
            Float2 e = new(0.001f, 0);

            return new Float3(m - MapNoise(p - e.XYY, i), m - MapNoise(p - e.YXY, i), 0.0f) / e.X * 1.4142f;
        }

        // The map layer and its derivative
        private Float4 MapLayer(in Float3 p, float i)
        {
            Float4 d = default;

            d.X = MapNoise(p, i);
            d.YZW = GetNormal(p, d.X, i);

            return d;
        }

        // Layer color. Based on the shade, layer number and smoothing factor.
        private Float3 GetColor(Float2 p, float sh, float fi)
        {
            Float3 tx = texture[p + Hash21(new Float2(sh, fi))].XYZ;
            
            tx *= tx;

            Float3 col;

            col = (Float3)1.0 * (1.0f - 0.75f / (1.0f + sh * sh * 2.0f));
            col = Hlsl.Min(col * tx * 3.0f, 1.0f);

            return col;
        }

        // A hatch-like algorithm, or a stipple... or some kind of textured pattern.
        private float Hatch(Float2 p, float res)
        {
            p *= res / 16.0f;

            float hatch = Hlsl.Clamp(Hlsl.Sin((p.X - p.Y) * 3.14159f * 200.0f) * 2.0f + 0.5f, 0.0f, 1.0f);

            float hRnd = Hash21(Hlsl.Floor(p * 6.0f) + 0.73f);
            if (hRnd > 0.66f) hatch = hRnd;

            hatch = hatch * 0.75f + 0.5f;

            return hatch;
        }

        /// <inheritdoc/>
        public Float4 Execute()
        {
            Float2 fragCoord = new(ThreadIds.X, DispatchSize.Y - ThreadIds.Y);
            float res = Hlsl.Min(DispatchSize.Y, 700.0f);
            Float2 uv = (fragCoord - (Float2)DispatchSize.XY * 0.5f) / res;
            float sf = 1.0f / DispatchSize.Y;
            Float3 col = GetColor(uv, 0.0f, 0.0f);
            float pL = 0.0f;
            float hatch = Hatch(uv, res);

            col *= hatch;

            const int lNum = 5;
            float flNum = lNum;

            for (int i = 0; i < lNum; i++)
            {
                float fi = i;

                hatch = Hatch(uv + Hlsl.Sin(new Float2(41.0f, 289.0f) * (fi + 1.0f)), res);

                Float4 c = MapLayer(new Float3(uv, 1.0f), fi);
                Float4 cSh = MapLayer(new Float3(uv - new Float2(0.03f, -0.03f) * ((flNum - fi) / flNum * 0.5f + 0.5f), 1.0f), fi);
                float sh = (fi + 1.0f) / (flNum);
                Float3 lCol = GetColor(uv, sh, fi + 1.0f);
                Float3 ld = Hlsl.Normalize(new Float3(-1, 1, -0.25f));
                Float3 n = Hlsl.Normalize(new Float3(0.0f, 0.0f, -1.0f) + c.YZW);
                float diff = Hlsl.Max(Hlsl.Dot(ld, n), 0.0f);

                diff *= 2.5f;

                Float3 eCol = lCol * (diff + 1.0f);
                float sfL = sf * Hlsl.Length(c.YZX) * 2.0f;
                float sfLSh = sf * Hlsl.Length(cSh.YZX) * 6.0f;
                const float shF = 0.5f;

                col = Hlsl.Lerp(col, Float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sfLSh, Hlsl.Max(cSh.X, pL))) * shF);
                col = Hlsl.Lerp(col, Float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sfL * 3.0f, c.X)) * 0.25f);
                col = Hlsl.Lerp(col, Float3.Zero, (1.0f - Hlsl.SmoothStep(0.0f, sfL, c.X)) * 0.85f);
                col = Hlsl.Lerp(col, eCol * hatch, (1.0f - Hlsl.SmoothStep(0.0f, sfL, c.X + Hlsl.Length(c.YZX) * 0.003f)));
                col = Hlsl.Lerp(col, lCol * hatch, (1.0f - Hlsl.SmoothStep(0.0f, sfL, c.X + Hlsl.Length(c.YZX) * 0.006f)));

                pL = c.X;
            }

            col *= Hlsl.Lerp(new Float3(1.8f, 1, 0.7f).ZYX, new Float3(1.8f, 1, 0.7f).XZY, Noise2D(uv * 2.0f));

            Float3 rn3 = Noise2D(uv * DispatchSize.Y / 1.0f + 1.7f) - Noise2D(uv * DispatchSize.Y / 1.0f + 3.4f);

            col *= 0.93f + 0.07f * rn3.XYZ + 0.07f * Hlsl.Dot(rn3, new Float3(0.299f, 0.587f, 0.114f));

            uv = fragCoord / DispatchSize.XY;

            col *= Hlsl.Pow(16.0f * uv.X * uv.Y * (1.0f - uv.X) * (1.0f - uv.Y), 0.0625f);

            return new Float4(Hlsl.Sqrt(Hlsl.Max(col, 0.0f)), 1);
        }
    }
}
