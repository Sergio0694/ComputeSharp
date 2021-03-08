namespace ComputeSharp.SwapChain.Shaders
{
    /// <summary>
    /// A shader creating an abstract and colorful animation.
    /// Ported from <see href="https://www.shadertoy.com/view/WtjyzR"/>.
    /// <para>Created by Benoit Marini.</para>
    /// <para>License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.</para>
    /// </summary>
    [AutoConstructor]
    internal readonly partial struct ColorfulInfinity : IComputeShader
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
        /// The total number of layers for the final animation.
        /// </summary>
        private const float NumberOfLayers = 16.0f;

        /// <summary>
        /// The number of iterations to calculate a texel value.
        /// </summary>
        private const int NumberOfIterations = 23;

        /// <summary>
        /// Makes some magic happen.
        /// </summary>
        private Float4 Tex(Float3 p)
        {
            float t = time + 78.0f;
            Float4 o = new(p.X, p.Y, p.Z, 3.0f * Hlsl.Sin(t * 0.1f));
            Float4 dec =
                new Float4(1.0f, 0.9f, 0.1f, 0.15f) +
                new Float4(0.06f * Hlsl.Cos(t * 0.1f), 0, 0, 0.14f * Hlsl.Cos(t * 0.23f));

            for (int i = 0; i++ < NumberOfIterations;)
            {
                o.XZYW = Hlsl.Abs(o / Hlsl.Dot(o, o) - dec);
            }

            return o;
        }

        /// <inheritdoc/>
        public void Execute()
        {
            Float2 uv = (ThreadIds.XY - (Float2)DispatchSize.XY * 0.5f) / DispatchSize.Y;
            Float3 col = 0;
            float t = time * 0.3f;

            for (float i = 0.0f; i <= 1.0f; i += 1.0f / NumberOfLayers)
            {
                float d = Hlsl.Frac(i + t);
                float s = Hlsl.Lerp(5.0f, 0.5f, d);
                float f = d * Hlsl.SmoothStep(1.0f, 0.9f, d);

                col += Tex(new Float3(uv.X * s, uv.Y * s, i * 4.0f)).XYZ * f;
            }

            col /= NumberOfLayers;
            col *= new Float3(2, 1.0f, 2.0f);
            col = Hlsl.Pow(col, 0.5f);

            Float4 color = new(col.X, col.Y, col.Z, 1.0f);

            texture[ThreadIds.XY] = color;
        }
    }
}
