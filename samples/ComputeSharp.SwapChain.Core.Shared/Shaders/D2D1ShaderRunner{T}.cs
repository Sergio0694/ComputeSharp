using System;
using CommunityToolkit.Diagnostics;
using ComputeSharp.D2D1;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp;
#else
using ComputeSharp.D2D1.WinUI;
#endif
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace ComputeSharp.SwapChain.Core.Shaders;

/// <summary>
/// A simple <see cref="ID2D1ShaderRunner"/> implementation powered by a supplied shader type.
/// </summary>
/// <typeparam name="T">The type of shader to use to render frames.</typeparam>
public sealed class D2D1ShaderRunner<T> : ID2D1ShaderRunner, IDisposable
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// The <see cref="Func{T1,T2,T3TResult}"/> instance used to create shaders to run.
    /// </summary>
    private readonly Func<TimeSpan, int, int, T> shaderFactory;

    /// <summary>
    /// The reusable <see cref="PixelShaderEffect{T}"/> instance to use to render frames.
    /// </summary>
    private readonly PixelShaderEffect<T> pixelShaderEffect;

    /// <summary>
    /// Creates a new <see cref="D2D1ShaderRunner{T}"/> instance.
    /// </summary>
    /// <param name="shaderFactory">The <see cref="Func{T1,TResult}"/> instance used to create shaders to run.</param>
    public D2D1ShaderRunner(Func<TimeSpan, int, int, T> shaderFactory)
    {
        Guard.IsNotNull(shaderFactory);

        this.shaderFactory = shaderFactory;
        this.pixelShaderEffect = new PixelShaderEffect<T>();
    }

    /// <inheritdoc/>
    public void Execute(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
    {
        int widthInPixels = sender.ConvertDipsToPixels((float)sender.Size.Width, CanvasDpiRounding.Round);
        int heightInPixels = sender.ConvertDipsToPixels((float)sender.Size.Height, CanvasDpiRounding.Round);

        // Set the constant buffer
        this.pixelShaderEffect.ConstantBuffer = this.shaderFactory(args.Timing.TotalTime, widthInPixels, heightInPixels);

        // Draw the shader
        args.DrawingSession.DrawImage(this.pixelShaderEffect);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.pixelShaderEffect.Dispose();
    }
}