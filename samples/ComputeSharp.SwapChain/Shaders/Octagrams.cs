namespace ComputeSharp.SwapChain.Shaders
{
    /// <summary>
    /// A shader creating an octagram animation, inspired by arabesque.
    /// Ported from <see href="https://www.shadertoy.com/view/tlVGDt"/>.
    /// <para>Created by whisky_shusuky.</para>
    /// </summary>
    [AutoConstructor]
    internal readonly partial struct Octagrams : IComputeShader
    {
        /// <summary>
        /// The target texture.
        /// </summary>
        public readonly IReadWriteTexture2D<Float4> texture;

        /// <summary>
        /// The current time since the start of the application.
        /// </summary>
        public readonly float time;

        private static Float2x2 Rotate(float a)
        {
            float c = Hlsl.Cos(a), s = Hlsl.Sin(a);

            return new(c, s, -s, c);
        }

        private static float Box(Float3 pos, float scale)
        {
            static float SDBox(Float3 p, Float3 b)
            {
                Float3 q = Hlsl.Abs(p) - b;

                return Hlsl.Length(Hlsl.Max(q, 0.0f)) +
                       Hlsl.Min(Hlsl.Max(q.X, Hlsl.Max(q.Y, q.Z)), 0.0f);
            }

            pos *= scale;

            float b = SDBox(pos, new(0.4f, 0.4f, 0.1f)) / 1.5f;

            pos.XY *= 5.0f;
            pos.Y -= 3.5f;
            pos.XY = Hlsl.Mul(pos.XY, Rotate(0.75f));

            return -b;
        }

        private static float BoxSet(Float3 pos, float gTime)
        {
            Float3 pos_origin = pos;

            pos = pos_origin;
            pos.Y += Hlsl.Sin(gTime * 0.4f) * 2.5f;
            pos.XY = Hlsl.Mul(pos.XY, Rotate(0.8f));

            float box1 = Box(pos, 2.0f - Hlsl.Abs(Hlsl.Sin(gTime * 0.4f)) * 1.5f);

            pos = pos_origin;
            pos.Y -= Hlsl.Sin(gTime * 0.4f) * 2.5f;
            pos.XY = Hlsl.Mul(pos.XY, Rotate(0.8f));

            float box2 = Box(pos, 2.0f - Hlsl.Abs(Hlsl.Sin(gTime * 0.4f)) * 1.5f);

            pos = pos_origin;
            pos.X += Hlsl.Sin(gTime * 0.4f) * 2.5f;
            pos.XY = Hlsl.Mul(pos.XY, Rotate(0.8f));

            float box3 = Box(pos, 2.0f - Hlsl.Abs(Hlsl.Sin(gTime * 0.4f)) * 1.5f);

            pos = pos_origin;
            pos.X -= Hlsl.Sin(gTime * 0.4f) * 2.5f;
            pos.XY = Hlsl.Mul(pos.XY, Rotate(0.8f));

            float box4 = Box(pos, 2.0f - Hlsl.Abs(Hlsl.Sin(gTime * 0.4f)) * 1.5f);

            pos = pos_origin;
            pos.XY = Hlsl.Mul(pos.XY, Rotate(0.8f));

            float box5 = Box(pos, 0.5f) * 6.0f;

            pos = pos_origin;

            float box6 = Box(pos, 0.5f) * 6.0f;
            float result = Hlsl.Max(Hlsl.Max(Hlsl.Max(Hlsl.Max(Hlsl.Max(box1, box2), box3), box4), box5), box6);

            return result;
        }

        /// <inheritdoc/>
        public void Execute()
        {
            Float2 p = ((Float2)ThreadIds.XY * 2.0f - DispatchSize.XY) / Hlsl.Min(DispatchSize.X, DispatchSize.Y);
            Float3 ro = new(0.0f, -0.2f, time * 4.0f);
            Float3 ray = Hlsl.Normalize(new Float3(p, 1.5f));

            ray.XY = Hlsl.Mul(ray.XY, Rotate(Hlsl.Sin(time * 0.03f) * 5.0f));
            ray.YZ = Hlsl.Mul(ray.YZ, Rotate(Hlsl.Sin(time * 0.05f) * 0.2f));

            float t = 0.1f;
            float ac = 0.0f;

            for (int i = 0; i < 99; i++)
            {
                static Float3 Mod(Float3 x, float y)
                {
                    return x - y * Hlsl.Floor(x / y);
                }

                Float3 pos = ro + ray * t;

                pos = Mod(pos - 2.0f, 4.0f) - 2.0f;

                float gTime = time - i * 0.01f;
                float d = BoxSet(pos, gTime);

                d = Hlsl.Max(Hlsl.Abs(d), 0.01f);
                ac += Hlsl.Exp(-d * 23.0f);
                t += d * 0.55f;
            }

            Float3 col = ac * 0.02f + new Float3(0.0f, 0.2f * Hlsl.Abs(Hlsl.Sin(time)), 0.5f + Hlsl.Sin(time) * 0.2f);
            Float4 color = new(col, 1.0f - t * (0.02f + 0.02f * Hlsl.Sin(time)));

            texture[ThreadIds.XY] = color;
        }
    }
}
