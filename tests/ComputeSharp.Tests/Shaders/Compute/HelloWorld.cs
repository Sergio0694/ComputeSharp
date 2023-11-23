#pragma warning disable IDE0048, IDE0009

namespace ComputeSharp.SwapChain.Shaders.Compute;

/// <summary>
/// A simple shader to get started with based on shadertoy new shader template.
/// Ported from <see href="https://www.shadertoy.com/new"/>.
/// </summary>
[AutoConstructor]
[ThreadGroupSize(DefaultThreadGroupSizes.XY)]
[GeneratedComputeShaderDescriptor]
internal readonly partial struct HelloWorld : IComputeShader
{
    /// <summary>
    /// The target texture.
    /// </summary>
    private readonly IReadWriteNormalizedTexture2D<float4> texture;

    /// <summary>
    /// The current time since the start of the application.
    /// </summary>
    private readonly float time;

    /// <inheritdoc/>
    public void Execute()
    {
        // Normalized screen space UV coordinates from 0.0 to 1.0
        float2 uv = ThreadIds.Normalized.XY;

        // Time varying pixel color
        float3 col = 0.5f + 0.5f * Hlsl.Cos(time + new float3(uv, uv.X) + new float3(0, 2, 4));

        // Output to screen
        texture[ThreadIds.XY] = new float4(col, 1f);
    }
}