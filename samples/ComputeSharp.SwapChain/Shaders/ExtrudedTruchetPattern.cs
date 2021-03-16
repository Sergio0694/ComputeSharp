namespace ComputeSharp.SwapChain.Shaders
{
    /// <summary>
    /// A basic extruded square grid-based blobby Truchet pattern.
    /// Ported from <see href="https://www.shadertoy.com/view/ttVBzd"/>.
    /// <para>Created by Shane.</para>
    /// </summary>
    [AutoConstructor]
    internal readonly partial struct ExtrudedTruchetPattern : IComputeShader
    {
        /// <summary>
        /// The target texture.
        /// </summary>
        public readonly IReadWriteTexture2D<Float4> texture;

        /// <summary>
        /// The current time since the start of the application.
        /// </summary>
        public readonly float time;

        /// <summary>
        /// Curve shape - Round: 0, Semi-round: 1, Octagonal: 2, Superellipse: 3, Straight: 4.
        /// </summary>
        private static readonly int SHAPE = 0;

        /// <summary>
        /// Object ID: Either the back plane or the metaballs.
        /// </summary>
        private static int objID;

        // Standard 2D rotation formula.
        private static Float2x2 Rotate2x2(float a)
        {
            float c = Hlsl.Cos(a), s = Hlsl.Sin(a);

            return new(c, -s, s, c);
        }

        // IQ's Float2 to float hash.
        private static float Hash21(Float2 p)
        {
            return Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(p, new Float2(27.619f, 57.583f))) * 43758.5453f);
        }

        // Various distance metrics.
        private static float Distance(Float2 p)
        {
            if (SHAPE == 0)
            {
                return Hlsl.Length(p);
            }
            else p = Hlsl.Abs(p);

            switch (SHAPE)
            {
                case 1: return Hlsl.Max(Hlsl.Length(p), (p.X + p.Y) * 0.7071f + 0.015f);
                case 2: return Hlsl.Max((p.X + p.Y) * 0.7071f, Hlsl.Max(p.X, p.Y));
                case 3: return Hlsl.Pow(Hlsl.Dot(Hlsl.Pow(p, 3), 1), 1.0f / 3.0f);
                default: return (p.X + p.Y) * 0.7071f;
            }
        }

        // GLSL's mod function
        private static float Mod(float x, float y)
        {
            return x - y * Hlsl.Floor(x / y);
        }

        // A standard square grid 2D blobby Truchet routine: Render circles
        // in opposite corners of a tile, reverse the pattern on alternate
        // checker tiles, and randomly rotate.
        private static float Truchet(Float2 p)
        {
            const float sc = 0.5f;
            Float2 ip = Hlsl.Floor(p / sc) + 0.5f;

            p -= ip * sc;

            float rnd = Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(ip, new Float2(1, 113))) * 45758.5453f);

            if (rnd < .5) p.Y = -p.Y;

            float d = Hlsl.Min(Distance(p - 0.5f * sc), Distance(p + 0.5f * sc)) - 0.5f * sc;

            if (SHAPE == 4)
            {
                d += (0.5f - 0.5f / Hlsl.Sqrt(2.0f)) * sc;
            }

            if (rnd < 0.5f) d = -d;
            if (Mod(ip.X + ip.Y, 2.0f) < 0.5f) d = -d;

            return d - 0.03f;
        }

        // The scene's distance function: There'd be faster ways to do this, but it's
        // more readable this way. Plus, this  is a pretty simple scene, so it's 
        // efficient enough.
        private static float M(Float3 p)
        {
            float fl = -p.Z;
            float obj = Truchet(p.XY);

            obj = Hlsl.Max(obj, Hlsl.Abs(p.Z) - 0.125f) - Hlsl.SmoothStep(0.03f, 0.25f, -obj) * 0.1f;

            float studs = 1e5f;
            const float sc = 0.5f;
            Float2 q = p.XY + 0.5f * sc;
            Float2 iq = Hlsl.Floor(q / sc) + 0.5f;

            q -= iq * sc;

            if (Mod(iq.X + iq.Y, 2.0f) > 0.5f)
            {
                studs = Hlsl.Max(Hlsl.Length(q) - 0.1f * sc - 0.02f, Hlsl.Abs(p.Z) - 0.26f);
            }

            objID = fl < obj && fl < studs ? 0 : obj < studs ? 1 : 2;

            return Hlsl.Min(Hlsl.Min(fl, obj), studs);
        }

        // Cheap shadows are hard. In fact, I'd almost say, shadowing particular scenes with limited 
        // iterations is impossible... However, I'd be very grateful if someone could prove me wrong. :)
        private static float SoftShadow(Float3 ro, Float3 lp, Float3 n, float k)
        {
            const int iter = 24;

            ro += n * 0.0015f;

            Float3 rd = lp - ro;
            float shade = 1.0f;
            float t = 0;
            float end = Hlsl.Max(Hlsl.Length(rd), 0.0001f);

            rd /= end;

            for (int i = 0; i < iter; i++)
            {
                float d = M(ro + rd * t);

                shade = Hlsl.Min(shade, k * d / t);
                t += Hlsl.Clamp(d, 0.01f, 0.25f);

                if (d < 0.0f || t > end) break;
            }

            return Hlsl.Max(shade, 0.0f);
        }


        // I keep a collection of occlusion routines... OK, that sounded really nerdy. :)
        // Anyway, I like this one. I'm assuHlsl.Ming it's based on IQ's original.
        private float CalculateAO(in Float3 p, in Float3 n)
        {
            float sca = 2.0f, occ = 0.0f;

            for (int i = (int)Hlsl.Min(time, 0f); i < 5; i++)
            {
                float hr = (i + 1f) * 0.15f / 5.0f;
                float d = M(p + n * hr);
                occ += (hr - d) * sca;
                sca *= 0.7f;

                if (sca > 1e5f) break;
            }

            return Hlsl.Clamp(1.0f - occ, 0.0f, 1.0f);
        }

        // Standard normal function.
        private static Float3 Normal(in Float3 p)
        {
            Float2 e = new(0.001f, 0);

            return Hlsl.Normalize(new Float3(
                M(p + e.XYY) - M(p - e.XYY),
                M(p + e.YXY) - M(p - e.YXY),
                M(p + e.YYX) - M(p - e.YYX)));
        }

        /// <inheritdoc/>
        public void Execute()
        {
            Int2 coords = new(ThreadIds.X, DispatchSize.Y - ThreadIds.Y);
            Float2 u = (coords - (Float2)DispatchSize.XY * 0.5f) / DispatchSize.Y;
            Float3
                r = Hlsl.Normalize(new Float3(u, 1)),
                o = new(0, time / 2.0f, -3),
                l = o + new Float3(0.25f, 0.25f, 2.0f);

            r.YZ = Hlsl.Mul(Rotate2x2(0.15f), r.YZ);
            r.XZ = Hlsl.Mul(Rotate2x2(-Hlsl.Cos(time * 3.14159f / 32.0f) / 8.0f), r.XZ);
            r.XY = Hlsl.Mul(Rotate2x2(Hlsl.Sin(time * 3.14159f / 32.0f) / 8.0f), r.XY);

            float d, t = Hash21(r.XY * 57.0f + Hlsl.Frac(time)) * 0.5f, glow = 0;

            for (int i = 0; i < 96; i++)
            {
                d = M(o + r * t);

                if (Hlsl.Abs(d) < .001) break;

                t += d * 0.7f;
                glow += 0.2f / (1.0f + Hlsl.Abs(d) * 5.0f);
            }

            int gObjID = objID;
            Float3 p = o + r * t, n = Normal(p);
            Float2 uv = p.XY;
            float sc = 0.5f;
            Float2 iuv = Hlsl.Floor(uv / sc) + 0.5f;

            uv -= iuv * sc;

            Float2 uv2 = p.XY + 0.5f * sc;
            Float2 iuv2 = Hlsl.Floor(uv2 / sc) + 0.5f;

            uv2 -= iuv2 * sc;

            float bord = Hlsl.Max(Hlsl.Abs(uv.X), Hlsl.Abs(uv.Y)) - 0.5f * sc;

            bord = Hlsl.Abs(bord) - 0.002f;
            d = Truchet(p.XY);

            float lSc = 20.0f;
            float pat = (Hlsl.Abs(Hlsl.Frac((uv.X - uv.Y) * lSc - 0.5f) - 0.5f) * 2.0f - 0.5f) / lSc;
            float pat2 = (Hlsl.Abs(Hlsl.Frac((uv.X + uv.Y) * lSc + 0.5f) - 0.5f) * 2.0f - 0.5f) / lSc;
            float sf = Hlsl.Dot(Hlsl.Sin(p.XY - Hlsl.Cos(p.YX * 2.0f)), 0.5f);
            float sf2 = Hlsl.Dot(Hlsl.Sin(p.XY * 1.5f - Hlsl.Cos(p.YX * 3.0f)), 0.5f);
            Float4 col1 = Hlsl.Lerp(new Float4(1, 0.75f, 0.6f, 0), new Float4(1, 0.85f, 0.65f, 0), Hlsl.SmoothStep(-0.5f, 0.5f, sf));
            Float4 col2 = Hlsl.Lerp(new Float4(0.4f, 0.7f, 1, 0), new Float4(0.3f, 0.85f, 0.95f, 0), Hlsl.SmoothStep(-0.5f, 0.5f, sf2) * 0.5f);

            col1 = Hlsl.Pow(col1, 1.6f);
            col2 = Hlsl.Lerp(col1.ZYXW, Hlsl.Pow(col2, 1.4f), 0.666f);

            Float4 oCol;

            if (gObjID == 0)
            {
                oCol = Hlsl.Lerp(col2, 0, (1.0f - Hlsl.SmoothStep(0, 0.01f, pat2)) * 0.35f);
                oCol = Hlsl.Lerp(oCol, 0, (1.0f - Hlsl.SmoothStep(0, 0.01f, bord)) * 0.8f);

                if (Mod(iuv.X + iuv.Y, 2.0f) > 0.5f) oCol *= 0.8f;

                oCol = Hlsl.Lerp(oCol, 0, (1.0f - Hlsl.SmoothStep(0, 0.01f, d - 0.015f)) * 0.8f);
            }
            else if (gObjID == 1)
            {
                oCol = Hlsl.Lerp(1, 0, 1.0f - Hlsl.SmoothStep(0, 0.01f, d + 0.05f));
                Float4 fCol = Hlsl.Lerp(col1, 0, (1.0f - Hlsl.SmoothStep(0, 0.01f, pat)) * 0.35f);
                fCol = Hlsl.Lerp(fCol, 0, (1.0f - Hlsl.SmoothStep(0, 0.01f, bord)) * 0.8f);

                if (Mod(iuv.X + iuv.Y, 2.0f) < 0.5f) fCol *= 0.8f;

                oCol = Hlsl.Lerp(oCol, fCol, 1 - Hlsl.SmoothStep(0, 0.01f, d + 0.08f));
                oCol = Hlsl.Lerp(oCol, 0, (1 - Hlsl.SmoothStep(0, 0.01f, Hlsl.Length(uv2) - 0.08f)) * 0.8f);
            }
            else
            {
                oCol = col1;

                float ht = 0.26f;

                if (Mod(iuv2.X + iuv2.Y + 1.0f, 2.0f) > 0.5f)
                {
                    float tmp = pat; pat = pat2; pat2 = tmp;

                    oCol = col2;
                    ht = 0.05f;
                }

                float mark = Hlsl.Length(uv2);
                float markRim = Hlsl.Max(Hlsl.Abs(mark - 0.07f), Hlsl.Abs(p.Z + ht)) - 0.003f;

                oCol = Hlsl.Lerp(oCol, 0, (1 - Hlsl.SmoothStep(0, 0.01f, markRim)) * 0.8f);
                oCol = Hlsl.Lerp(oCol, 0, (1 - Hlsl.SmoothStep(0, 0.01f, mark - 0.015f)) * 0.8f);
            }

            Float3 ld = l - p;
            float lDist = Hlsl.Length(ld);

            ld /= lDist;

            float at = 1.0f / (1.0f + lDist * lDist * 0.125f);
            float sh = SoftShadow(p, l, n, 8);
            float ao = CalculateAO(p, n);
            float df = Hlsl.Max(Hlsl.Dot(n, ld), 0);
            float sp = Hlsl.Pow(Hlsl.Max(Hlsl.Dot(Hlsl.Reflect(r, n), ld), 0), 32);

            Float4 c = oCol * (df * sh + sp * sh + 0.5f) * at * ao;

            c = Hlsl.Sqrt(Hlsl.Max(c, 0));

            texture[ThreadIds.XY] = c;
        }
    }
}
