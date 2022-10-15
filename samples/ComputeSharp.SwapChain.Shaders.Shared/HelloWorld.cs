namespace ComputeSharp.SwapChain.Shaders;

/// <summary>
/// A simple shader to get started with based on shadertoy new shader template.
/// Ported from <see href="https://www.shadertoy.com/new"/>.
/// </summary>
[AutoConstructor]
#if SAMPLE_APP
[EmbeddedBytecode(DispatchAxis.XY)]
#endif
internal readonly partial struct HelloWorld : IPixelShader<float4>
{
    /// <summary>
    /// The current time since the start of the application.
    /// </summary>
    private readonly float time;

    /// <inheritdoc/>
    public float4 Execute()
    {
        // Normalized screen space UV coordinates from 0.0 to 1.0
        float2 uv = ThreadIds.Normalized.XY;

        // Time varying pixel color
        float3 col = 0.5f + (0.5f * Hlsl.Cos(time + new float3(uv, uv.X) + new float3(0, 2, 4)));

        // Output to screen
        return new(col, 1f);
    }
}