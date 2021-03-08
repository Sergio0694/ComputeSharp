namespace ComputeSharp.SwapChain.Shaders
{
    /// <summary>
    /// A shader creating an animated two-tiles Truchet arrangement.
    /// Ported from <see href="https://www.shadertoy.com/view/tsSfWK"/>.
    /// <para>Created by Shane.</para>
    /// </summary>
    [AutoConstructor]
    internal readonly partial struct TwoTiledTruchet : IComputeShader
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
        /// Calculates the Truchet distance field.
        /// </summary>
        /// <param name="p">The input value to compute the distance field for.</param>
        /// <param name="ang">The resulting Truchet angle.</param>
        /// <returns>The distance field for the input value.</returns>
        private Float2 DistanceField(Float2 p, out Float2 ang)
        {
            // Fast hash for a pair of floats
            static float Hash21(Float2 p)
            {
                return Hlsl.Frac(Hlsl.Sin(Hlsl.Dot(p, new Float2(27.619f, 57.583f))) * 43758.5453f);
            }

            // HLSL's port of the GLSL mod intrinsic
            static float Mod(float x, float y)
            {
                return x - y * Hlsl.Floor(x / y);
            }

            Float2 ip2 = Hlsl.Floor(p / 2.0f);
            Float2 ip = Hlsl.Floor(p);
            float rnd = Hash21(ip);
            float rnd3 = Hash21(ip + 0.57f);
            Float2 d = 1e5f;

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
                    p = p.YX * new Float2(1, -1);
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

            ang = Hlsl.Frac(ang + time / 4.0f);

            return d;
        }

        /// <inheritdoc/>
        public void Execute()
        {
            Float2 uv = (ThreadIds.XY - (Float2)DispatchSize.XY * 0.5f) / DispatchSize.Y;
            float gSc = 7.0f;
            Float2 p = uv * gSc - new Float2(-1, -0.5f) * time / 2.0f;
            float sf = 2.0f / DispatchSize.Y * gSc;
            float lSc = 6.0f;
            float lw = 1.0f / lSc / gSc;
            Float2 d = DistanceField(p, out Float2 ang) - 2.5f / lSc;
            Float3 col = new(1.0f, 0.9f, 0.8f);

            for (int i = 0; i < 2; i++)
            {
                float di = d[i] - lw / 4.0f;
                float tracks = Hlsl.Clamp(Hlsl.Sin(ang[i] * 6.2831f + time * 6.0f) * 4.0f, 0.0f, 1.0f);
                float gap = 1.0f + lw;

                col = Hlsl.Lerp(col, 0, (1.0f - Hlsl.SmoothStep(0.0f, sf * 6.0f, di)) * 0.35f);
                col = Hlsl.Lerp(col, 0, 1.0f - Hlsl.SmoothStep(0.0f, sf, di));
                col = Hlsl.Lerp(col, new(1.0f, 0.9f, 0.8f), 1.0f - Hlsl.SmoothStep(0.0f, sf, di + lw * 2.0f));
                col = Hlsl.Lerp(col, 0, 1.0f - Hlsl.SmoothStep(0.0f, sf, di + gap / lSc));
                col = Hlsl.Lerp(col, 1, 1.0f - Hlsl.SmoothStep(0.0f, sf, di + gap / lSc + lw));
                col = Hlsl.Lerp(col, 0, 1.0f - Hlsl.SmoothStep(0.0f, sf, di + 2.0f * gap / lSc));
                col = Hlsl.Lerp(col, 1 * tracks, 1.0f - Hlsl.SmoothStep(0.0f, sf, di + 2.0f * gap / lSc + lw));
            }

            Float3 rgb = Hlsl.Sqrt(Hlsl.Max(col, 0.0f));
            Float4 color = new(rgb.X, rgb.Y, rgb.Z, 1.0f);

            texture[ThreadIds.XY] = color;
        }
    }
}
