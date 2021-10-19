namespace ComputeSharp.SwapChain.Shaders
{
    /// <summary>
    /// A simple shader to get started with based on shadertoy new shader template.
    /// Ported from <see href="https://www.shadertoy.com/new"/>.
    /// </summary>
    /// 
    [AutoConstructor]
    internal readonly partial struct HelloWorld : IPixelShader<Float4>
    {
        /// <summary>
        /// The current time since the start of the application.
        /// </summary>
        public readonly float time;

        /// <inheritdoc/>
        public Float4 Execute()
        {
            // Normalized screen space UV coordinates from 0.0f to 1.0f
            Float2 uv = ThreadIds.Normalized.XY;

            // Time varying pixel color
            Float3 col = 0.5f + 0.5f * Hlsl.Cos(time + new Float3(uv, uv.X) + new Float3(0, 2, 4));

            // Output to screen
            return new(col, 1f);
        }
    }
}
