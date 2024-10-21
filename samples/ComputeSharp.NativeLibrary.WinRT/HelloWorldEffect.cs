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

    /// <summary><inheritdoc cref="Shader(float, Int2)" path="/param[@name='time']/node()"/></summary>
    private float time;

    /// <summary><inheritdoc cref="Shader(float, Int2)" path="/param[@name='dispatchSize']/node()"/></summary>
    private Rect dispatchArea;

    /// <summary>
    /// Gets or sets the current time since the start of the application.
    /// </summary>
    public float Time
    {
        get => this.time;
        set => SetPropertyAndInvalidateEffectGraph(ref this.time, value);
    }

    /// <summary>
    /// Gets or sets the dispatch area for the current output.
    /// </summary>
    public Rect DispatchArea
    {
        get => this.dispatchArea;
        set => SetPropertyAndInvalidateEffectGraph(ref this.dispatchArea, value);
    }

    /// <inheritdoc/>
    protected override void BuildEffectGraph(CanvasEffectGraph effectGraph)
    {
        effectGraph.RegisterOutputNode(EffectNode, new PixelShaderEffect<Shader>());
    }

    /// <inheritdoc/>
    protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
    {
        effectGraph.GetNode(EffectNode).ConstantBuffer = new Shader(
            time: this.time,
            dispatchSize: new int2(
                x: (int)double.Round(this.dispatchArea.Width),
                y: (int)double.Round(this.dispatchArea.Height)));
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
