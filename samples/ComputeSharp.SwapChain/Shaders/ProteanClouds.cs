namespace ComputeSharp.SwapChain.Shaders
{
    /// <summary>
    /// Fully procedural 3D animated volume with three evaluations per step (for shading).
    /// Ported from <see href="https://www.shadertoy.com/view/3l23Rh"/>.
    /// <para>Created by nimitz (twitter: @stormoid).</para>
    /// <para>License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.</para>
    /// </summary>
    [AutoConstructor]
    internal readonly partial struct ProteanClouds : IComputeShader
    {
        /// <summary>
        /// The target texture.
        /// </summary>
        public readonly IReadWriteTexture2D<Float4> texture;

        /// <summary>
        /// The current time Hlsl.Since the start of the application.
        /// </summary>
        public readonly float time;

        private static readonly Float3x3 m3 = new Float3x3(0.33338f, 0.56034f, -0.71817f, -0.87887f, 0.32651f, -0.15323f, 0.15162f, 0.69596f, 0.61339f) * 1.93f;

        private static Float2x2 Rotate(in float a)
        {
            float c = Hlsl.Cos(a), s = Hlsl.Sin(a);
            
            return new(c, s, -s, c);
        }

        private static float LinStep(in float mn, in float mx, in float x)
        {
            return Hlsl.Clamp((x - mn) / (mx - mn), 0.0f, 1.0f);
        }

        private static Float2 Disp(float t)
        {
            return new Float2(Hlsl.Sin(t * 0.22f) * 1.0f, Hlsl.Cos(t * 0.175f) * 1.0f) * 2.0f;
        }

        private Float2 Map(Float3 p, float prm1, Float2 bsMo)
        {
            Float3 p2 = p;

            p2.XY -= Disp(p.Z).XY;
            p.XY = Hlsl.Mul(p.XY, Rotate(Hlsl.Sin(p.Z + time) * (0.1f + prm1 * 0.05f) + time * 0.09f));

            float cl = Hlsl.Dot(p2.XY, p2.XY);
            float d = 0.0f;

            p *= 0.61f;

            float z = 1.0f;
            float trk = 1.0f;
            float dspAmp = 0.1f + prm1 * 0.2f;

            for (int i = 0; i < 5; i++)
            {
                p += Hlsl.Sin(p.ZXY * 0.75f * trk + time * trk * 0.8f) * dspAmp;
                d -= Hlsl.Abs(Hlsl.Dot(Hlsl.Cos(p), Hlsl.Sin(p.YZX)) * z);
                z *= 0.57f;
                trk *= 1.4f;

                p = Hlsl.Mul(p, m3);
            }

            d = Hlsl.Abs(d + prm1 * 3.0f) + prm1 * 0.3f - 2.5f + bsMo.Y;

            return new Float2(d + cl * 0.2f + 0.25f, cl);
        }

        private Float4 Render(Float3 ro, Float3 rd, float time, float prm1, Float2 bsMo)
        {
            Float4 rez = 0;
            float t = 1.5f;
            float fogT = 0;

            for (int i = 0; i < 130; i++)
            {
                if (rez.A > 0.99f) break;

                Float3 pos = ro + t * rd;
                Float2 mpv = Map(pos, prm1, bsMo);
                float den = Hlsl.Clamp(mpv.X - 0.3f, 0.0f, 1.0f) * 1.12f;
                float dn = Hlsl.Clamp((mpv.X + 2.0f), 0.0f, 3.0f);
                Float4 col = 0f;

                if (mpv.X > 0.6f)
                {

                    col = new Float4(Hlsl.Sin(new Float3(5.0f, 0.4f, 0.2f) + mpv.Y * 0.1f + Hlsl.Sin(pos.Z * 0.4f) * 0.5f + 1.8f) * 0.5f + 0.5f, 0.08f);
                    col *= den * den * den;
                    col.RGB *= LinStep(4.0f, -2.5f, mpv.X) * 2.3f;

                    float dif = Hlsl.Clamp((den - Map(pos + 0.8f, prm1, bsMo).X) / 9.0f, 0.001f, 1.0f);

                    dif += Hlsl.Clamp((den - Map(pos + 0.35f, prm1, bsMo).X) / 2.5f, 0.001f, 1.0f);
                    col.XYZ *= den * (new Float3(0.005f, 0.045f, 0.075f) + 1.5f * new Float3(0.033f, 0.07f, 0.03f) * dif);
                }

                float fogC = Hlsl.Exp(t * 0.2f - 2.2f);

                col += new Float4(0.06f, 0.11f, 0.11f, 0.1f) * Hlsl.Clamp(fogC - fogT, 0.0f, 1.0f);
                fogT = fogC;
                rez += col * (1.0f - rez.A);
                t += Hlsl.Clamp(0.5f - dn * dn * 0.05f, 0.09f, 0.3f);
            }

            return Hlsl.Clamp(rez, 0.0f, 1.0f);
        }

        private static float GetSaturation(Float3 c)
        {
            float mi = Hlsl.Min(Hlsl.Min(c.X, c.Y), c.Z);
            float ma = Hlsl.Max(Hlsl.Max(c.X, c.Y), c.Z);

            return (ma - mi) / (ma + (float)1e-7);
        }

        private static Float3 ILerp(in Float3 a, in Float3 b, in float x)
        {
            Float3 ic = Hlsl.Lerp(a, b, x) + new Float3((float)1e-6, 0.0f, 0.0f);
            float sd = Hlsl.Abs(GetSaturation(ic) - Hlsl.Lerp(GetSaturation(a), GetSaturation(b), x));
            Float3 dir = Hlsl.Normalize(new Float3(2.0f * ic.X - ic.Y - ic.Z, 2.0f * ic.Y - ic.X - ic.Z, 2.0f * ic.Z - ic.Y - ic.X));
            float lgt = Hlsl.Dot(1.0f, ic);
            float ff = Hlsl.Dot(dir, Hlsl.Normalize(ic));

            ic += 1.5f * dir * sd * ff * lgt;

            return Hlsl.Clamp(ic, 0.0f, 1.0f);
        }

        /// <inheritdoc/>
        public void Execute()
        {
            Float2 q = (Float2)ThreadIds.XY / DispatchSize.XY;
            Float2 p = (ThreadIds.XY - 0.5f * (Float2)DispatchSize.XY) / DispatchSize.Y;
            Float2 bsMo = -0.5f * (Float2)DispatchSize.XY / DispatchSize.Y;
            float scaledTime = time * 3.0f;
            Float3 ro = new(0, 0, scaledTime);

            ro += new Float3(Hlsl.Sin(time) * 0.5f, Hlsl.Sin(time * 1.0f) * 0.0f, 0);

            float dspAmp = 0.85f;

            ro.XY += Disp(ro.Z) * dspAmp;

            float tgtDst = 3.5f;
            Float3 target = Hlsl.Normalize(ro - new Float3(Disp(scaledTime + tgtDst) * dspAmp, scaledTime + tgtDst));

            ro.X -= bsMo.X * 2.0f;

            Float3 rightdir = Hlsl.Normalize(Hlsl.Cross(target, new Float3(0, 1, 0)));
            Float3 updir = Hlsl.Normalize(Hlsl.Cross(rightdir, target));

            rightdir = Hlsl.Normalize(Hlsl.Cross(updir, target));

            Float3 rd = Hlsl.Normalize((p.X * rightdir + p.Y * updir) * 1.0f - target);

            rd.XY = Hlsl.Mul(rd.XY, Rotate(-Disp(scaledTime + 3.5f).X * 0.2f + bsMo.X));

            float prm1 = Hlsl.SmoothStep(-0.4f, 0.4f, Hlsl.Sin(time * 0.3f));
            Float4 scn = Render(ro, rd, scaledTime, prm1, bsMo);
            Float3 col = scn.RGB;

            col = ILerp(col.BGR, col.RGB, Hlsl.Clamp(1.0f - prm1, 0.05f, 1.0f));
            col = Hlsl.Pow(col, new Float3(0.55f, 0.65f, 0.6f)) * new Float3(1.0f, 0.97f, 0.9f);
            col *= Hlsl.Pow(16.0f * q.X * q.Y * (1.0f - q.X) * (1.0f - q.Y), 0.12f) * 0.7f + 0.3f;

            texture[ThreadIds.XY] = new Float4(col, 1.0f);
        }
    }
}
