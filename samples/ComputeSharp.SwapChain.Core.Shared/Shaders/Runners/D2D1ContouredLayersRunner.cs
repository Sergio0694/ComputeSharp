using System.IO;
using CommunityToolkit.Diagnostics;
using ComputeSharp.D2D1;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Uwp;
using ComputeSharp.SwapChain.Shaders.D2D1;
#if WINDOWS_UWP
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
#else
using ComputeSharp.WinUI;
#endif
using Windows.ApplicationModel;

#nullable enable

namespace ComputeSharp.SwapChain.Core.Shaders.Runners;

/// <summary>
/// A specialized <see cref="ID2D1ShaderRunner"/> for <see cref="ContouredLayers"/>.
/// </summary>
public sealed class D2D1ContouredLayersRunner : ID2D1ShaderRunner
{
    /// <summary>
    /// The reusable <see cref="PixelShaderEffect{T}"/> instance to use to render frames.
    /// </summary>
    private PixelShaderEffect<ContouredLayers>? pixelShaderEffect;

    /// <inheritdoc/>
    public unsafe void Execute(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
    {
        if (this.pixelShaderEffect is null)
        {
            D2D1ResourceTextureManager resourceTextureManager;

            string filename = Path.Combine(Package.Current.InstalledLocation.Path, "Assets", "Textures", "RustyMetal.1024x1024rgba32");

            // Open a file to read from the file (we're doing synchronous IO here by design, doesn't really matter)
            using (FileStream fileStream = new(filename, FileMode.Open, FileAccess.Read))
            {
                byte[] pixelData = new byte[(int)fileStream.Length];
                int totalBytesRead = 0;

                // Read the raw image data
                while (totalBytesRead < pixelData.Length)
                {
                    int bytesRead = fileStream.Read(pixelData, 0, pixelData.Length);

                    Guard.IsGreaterThan(bytesRead, 0);

                    totalBytesRead += bytesRead;
                }

                // Create the resource texture manager to use in the shader
                resourceTextureManager = new D2D1ResourceTextureManager(
                    extents: stackalloc uint[] { 1024, 1024 },
                    bufferPrecision: D2D1BufferPrecision.UInt8Normalized,
                    channelDepth: D2D1ChannelDepth.Four,
                    filter: D2D1Filter.MinMagMipLinear,
                    extendModes: stackalloc D2D1ExtendMode[] { D2D1ExtendMode.Mirror, D2D1ExtendMode.Mirror },
                    data: pixelData,
                    strides: stackalloc uint[] { (uint)(1024 * sizeof(Bgra32)) });
            }

            // Create the new pixel shader effect
            this.pixelShaderEffect = new PixelShaderEffect<ContouredLayers>() { ResourceTextureManagers = { [0] = resourceTextureManager } };
        }

        int widthInPixels = sender.ConvertDipsToPixels((float)sender.Size.Width, CanvasDpiRounding.Round);
        int heightInPixels = sender.ConvertDipsToPixels((float)sender.Size.Height, CanvasDpiRounding.Round);

        // Set the constant buffer
        this.pixelShaderEffect.ConstantBuffer = new ContouredLayers((float)args.Timing.TotalTime.TotalSeconds, new int2(widthInPixels, heightInPixels));

        // Draw the shader
        args.DrawingSession.DrawImage(this.pixelShaderEffect);
    }
}