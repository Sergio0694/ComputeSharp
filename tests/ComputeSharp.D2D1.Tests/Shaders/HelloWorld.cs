namespace ComputeSharp.D2D1.Tests.Shaders;

/// <summary>
/// A simple shader to get started with based on shadertoy new shader template.
/// Ported from <see href="https://www.shadertoy.com/new"/>.
/// </summary>
[D2DInputCount(0)]
[D2DRequiresScenePosition]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[AutoConstructor]
internal readonly partial struct HelloWorld : ID2D1PixelShader
{
    /// <summary>
    /// The current time since the start of the application.
    /// </summary>
    public readonly float time;

    /// <summary>
    /// The dispatch size for the current output.
    /// </summary>
    public readonly int2 dispatchSize;

    /// <inheritdoc/>
    public float4 Execute()
    {
        Int2 xy = (int2)D2D.GetScenePosition().XY;

        // Normalized screen space UV coordinates from 0.0 to 1.0
        float2 uv = xy / (float2)this.dispatchSize;

        // Time varying pixel color
        float3 col = 0.5f + (0.5f * Hlsl.Cos(this.time + new float3(uv, uv.X) + new float3(0, 2, 4)));

        // Output to screen
        return new(col, 1f);
    }
}