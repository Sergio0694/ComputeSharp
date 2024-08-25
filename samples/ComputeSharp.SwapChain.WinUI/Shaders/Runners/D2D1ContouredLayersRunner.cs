using System;
using System.IO;
using ComputeSharp.D2D1;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.WinUI;
using ComputeSharp.SwapChain.Shaders.D2D1;
using Windows.ApplicationModel;

namespace ComputeSharp.SwapChain.Core.Shaders.Runners;

/// <summary>
/// A specialized <see cref="PixelShaderEffect"/> for <see cref="ContouredLayers"/>.
/// </summary>
public sealed partial class D2D1ContouredLayersEffect : PixelShaderEffect
{
    /// <summary>
    /// The reusable <see cref="PixelShaderEffect{T}"/> node to use to render frames.
    /// </summary>
    private static readonly CanvasEffectNode<PixelShaderEffect<ContouredLayers>> PixelShaderEffect = new();

    /// <inheritdoc/>
    protected override unsafe void BuildEffectGraph(CanvasEffectGraph effectGraph)
    {
        string filename = Path.Combine(Package.Current.InstalledLocation.Path, "Assets", "Textures", "RustyMetal.png");

        // As a temporary workaround for the lack of image decoding helpers for D2D1ResourceTextureManager, and in order to
        // keep the code here synchronous and simple, we can just leverage the DirectX 12 APIs to load textures. That is, we
        // first load a texture (which will use WIC behind the scenes) to get the decoded image data in GPU memory, and then
        // copy the data in a readback texture we can read from on the CPU. We can't just load an upload texture and read
        // from it, as that type of texture can only be written to from the CPU. From there, we create a D2D resource texture.
        using ReadOnlyTexture2D<Rgba32, float4> readOnlyTexture = GraphicsDevice.GetDefault().LoadReadOnlyTexture2D<Rgba32, float4>(filename);
        using ReadBackTexture2D<Rgba32> readBackTexture = GraphicsDevice.GetDefault().AllocateReadBackTexture2D<Rgba32>(readOnlyTexture.Width, readOnlyTexture.Height);

        readOnlyTexture.CopyTo(readBackTexture);

        // Get the buffer pointer, the stride, and calculate the buffer size without the trailing padding.
        // That is, the area between the end of the data in the last row and the stride is not included.
        Rgba32* dataBuffer = readBackTexture.View.DangerousGetAddressAndByteStride(out int strideInBytes);
        int bufferSize = ((readBackTexture.Height - 1) * strideInBytes) + (readBackTexture.View.Width * sizeof(Rgba32));

        // Create the resource texture manager to use in the shader
        D2D1ResourceTextureManager resourceTextureManager = new(
            extents: [(uint)readBackTexture.Height, (uint)readBackTexture.Width],
            bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
            channelDepth: D2D1ChannelDepth.Four,
            filter: D2D1Filter.MinMagMipLinear,
            extendModes: [D2D1ExtendMode.Mirror, D2D1ExtendMode.Mirror],
            data: new ReadOnlySpan<byte>(dataBuffer, bufferSize),
            strides: [(uint)strideInBytes]);

        // Create the new pixel shader effect
        PixelShaderEffect<ContouredLayers> pixelShaderEffect = new() { ResourceTextureManagers = { [0] = resourceTextureManager } };

        // Register the pixel shader effect as the output node in the effect graph
        effectGraph.RegisterOutputNode(PixelShaderEffect, pixelShaderEffect);
    }

    /// <inheritdoc/>
    protected override void ConfigureEffectGraph(CanvasEffectGraph effectGraph)
    {
        effectGraph.GetNode(PixelShaderEffect).ConstantBuffer = new ContouredLayers((float)ElapsedTime.TotalSeconds, new int2(ScreenWidth, ScreenHeight));
    }
}