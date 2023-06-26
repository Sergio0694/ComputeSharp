using System;
using TerraFX.Interop.Windows;

namespace ComputeSharp.SwapChain.Backend;

/// <summary>
/// A <see cref="Win32Application"/> factory type that renders an animated shader on a swapchain on a target <see cref="HWND"/>.
/// </summary>
/// <typeparam name="T">The shader type to render.</typeparam>
/// <remarks>
/// The main point of this type is to hide the complexity of <see cref="Win32Application"/> from the non-NativeAOT sample.
/// </remarks>
internal static class Win32ApplicationFactory<T>
    where T : struct, IPixelShader<float4>
{
    /// <summary>
    /// The <see cref="Func{T1, TResult}"/> instance used to create shaders to run.
    /// </summary>
    private static Func<TimeSpan, T>? shaderFactory;

    /// <summary>
    /// Creates a new <see cref="Win32Application"/> from the specified factory.
    /// </summary>
    /// <param name="shaderFactory">The <see cref="Func{T1, TResult}"/> instance used to create shaders to run.</param>
    /// <returns>A <see cref="Win32Application"/> instance to use.</returns>
    public static unsafe Win32Application Create(Func<TimeSpan, T> shaderFactory)
    {
        Win32ApplicationFactory<T>.shaderFactory = shaderFactory;

        return new(&ShaderCallback);
    }

    /// <summary>
    /// The callback function to draw a single frame.
    /// </summary>
    /// <param name="texture">The input <see cref="IReadOnlyNormalizedTexture2D{TPixel}"/> instance to draw on.</param>
    /// <param name="time">The time for the current frame.</param>
    private static void ShaderCallback(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time)
    {
        texture.GraphicsDevice.ForEach(texture, shaderFactory!(time));
    }
}