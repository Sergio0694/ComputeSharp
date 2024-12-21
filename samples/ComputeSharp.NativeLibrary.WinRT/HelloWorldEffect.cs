using ComputeSharp.D2D1;
using ComputeSharp.D2D1.WinUI;
using Windows.Foundation;

namespace ComputeSharp.NativeLibrary;

/// <summary>
/// A hello world effect that displays a color gradient.
/// </summary>
public sealed partial class HelloWorldEffect : CanvasEffect
{
    /// <summary>
    /// The <see cref="PixelShaderEffect{T}"/> node in use.
    /// </summary>
    private static readonly CanvasEffectNode<PixelShaderEffect<Shader>> EffectNode = new();

    /// <summary>
    /// Gets or sets the current time since the start of the application.
    /// </summary>
    [GeneratedCanvasEffectProperty]
    public partial float Time { get; set; }

    /// <summary>
    /// Gets or sets the dispatch area for the current output.
    /// </summary>
    [GeneratedCanvasEffectProperty]
    public partial Rect DispatchArea { get; set; }

    /// <inheritdoc/>
    protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
    {
        effectGraph.RegisterOutputNode(EffectNode, new PixelShaderEffect<Shader>());
    }

    /// <inheritdoc/>
    protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
    {
        effectGraph.GetNode(EffectNode).ConstantBuffer = new Shader(
            time: Time,
            dispatchSize: new int2(
                x: (int)double.Round(DispatchArea.Width),
                y: (int)double.Round(DispatchArea.Height)));
    }

    /// <summary>
    /// A hello world effect that displays a color gradient.
    /// </summary>
    /// <param name="time">The current time since the start of the application.</param>
    /// <param name="dispatchSize">The dispatch size for the current output.</param>
    [D2DEffectDisplayName(nameof(HelloWorldEffect))]
    [D2DEffectDescription("A hello world effect that displays a color gradient.")]
    [D2DEffectCategory("Render")]
    [D2DEffectAuthor("ComputeSharp.D2D1")]
    [D2DInputCount(0)]
    [D2DRequiresScenePosition]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct Shader(float time, int2 dispatchSize) : ID2D1PixelShader
    {
        /// <inheritdoc/>
        public float4 Execute()
        {
            int2 xy = (int2)D2D.GetScenePosition().XY;
            float2 uv = xy / (float2)dispatchSize;
            float3 color = 0.5f + (0.5f * Hlsl.Cos(time + new float3(uv, uv.X) + new float3(0, 2, 4)));

            return new(color, 1f);
        }
    }
}
