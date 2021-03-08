namespace ComputeSharp.SwapChain.Shaders
{
    /// <summary>
    /// A shader creating a fractal tiling animation.
    /// Ported from <see href="https://www.shadertoy.com/view/Ml2GWy"/>.
    /// <para>Created by Inigo Quilez.</para>
    /// <para>License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.</para>
    /// </summary>
    [AutoConstructor]
    internal readonly partial struct FractalTiling : IComputeShader
    {
        /// <summary>
        /// The target texture.
        /// </summary>
        public readonly IReadWriteTexture2D<Float4> texture;

        /// <summary>
        /// The current time since the start of the application.
        /// </summary>
        public readonly float time;

        /// <inheritdoc/>
        public void Execute()
        {
            Float2 position = ((Float2)(256 * ThreadIds.XY)) / DispatchSize.X + time;
            Float4 color = 0;

            for (int i = 0; i < 6; i++)
            {
                Float2 a = Hlsl.Floor(position);
                Float2 b = Hlsl.Frac(position);
                Float4 w = Hlsl.Frac(
                    (Hlsl.Sin(a.X * 7 + 31.0f * a.Y + 0.01f * time) +
                     new Float4(0.035f, 0.01f, 0, 0.7f))
                     * 13.545317f);

                color.XYZ += w.XYZ *
                       2.0f * Hlsl.SmoothStep(0.45f, 0.55f, w.W) *
                       Hlsl.Sqrt(16.0f * b.X * b.Y * (1.0f - b.X) * (1.0f - b.Y));

                position /= 2.0f;
                color /= 2.0f;
            }

            color.XYZ = Hlsl.Pow(color.XYZ, new Float3(0.7f, 0.8f, 0.5f));
            color.W = 1.0f;

            texture[ThreadIds.XY] = color;
        }
    }
}
