using System;
using ComputeSharp.SwapChain.WinUI.Models;

namespace ComputeSharp.SwapChain.WinUI.Converters
{
    /// <summary>
    /// A class with some static converters for <see cref="ComputeShader"/>.
    /// </summary>
    public static class ComputeShaderConverter
    {
        /// <summary>
        /// Converts a <see cref="ComputeShader"/> instance to a <see cref="Uri"/> with the thumbnail image.
        /// </summary>
        /// <param name="value">The input <see cref="ComputeShader"/> instance.</param>
        /// <returns>A <see cref="Uri"/> with the thumbnail image for <paramref name="value"/>.</returns>
        public static Uri ConvertComputeShaderToThumbnailUri(ComputeShader value)
        {
            return new($"ms-appx:///Images/Shaders/{value.ShaderRunner.GetType().GenericTypeArguments[0].Name}.png");
        }

        /// <summary>
        /// Converts a <see cref="ComputeShader"/> instance to a <see cref="Uri"/> with the background image.
        /// </summary>
        /// <param name="value">The input <see cref="ComputeShader"/> instance.</param>
        /// <returns>A <see cref="Uri"/> with the background image for <paramref name="value"/>.</returns>
        public static Uri ConvertComputeShaderToBackgroundUri(ComputeShader value)
        {
            return new($"ms-appx:///Images/Shaders/Blurred/{value.ShaderRunner.GetType().GenericTypeArguments[0].Name}.png");
        }
    }
}
