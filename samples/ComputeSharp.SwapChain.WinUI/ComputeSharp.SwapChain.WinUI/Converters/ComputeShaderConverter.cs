using System;
using ComputeSharp.SwapChain.WinUI.Models;
using ComputeSharp.SwapChain.WinUI.Shaders.Runners;

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
            if (value.ShaderRunner.GetType().IsGenericType)
            {
                return new($"ms-appx:///Images/Shaders/{value.ShaderRunner.GetType().GenericTypeArguments[0].Name}.png");
            }

            return value.ShaderRunner.GetType() switch
            {
                Type t when t == typeof(ContouredLayersRunner) => new($"ms-appx:///Images/Shaders/ContouredLayers.png"),
                _ => throw new ArgumentException($"Invalid shader type: {value.ShaderRunner.GetType()}")
            };
        }

        /// <summary>
        /// Converts a <see cref="ComputeShader"/> instance to a <see cref="Uri"/> with the background image.
        /// </summary>
        /// <param name="value">The input <see cref="ComputeShader"/> instance.</param>
        /// <returns>A <see cref="Uri"/> with the background image for <paramref name="value"/>.</returns>
        public static Uri ConvertComputeShaderToBackgroundUri(ComputeShader value)
        {
            if (value.ShaderRunner.GetType().IsGenericType)
            {
                return new($"ms-appx:///Images/Shaders/Blurred/{value.ShaderRunner.GetType().GenericTypeArguments[0].Name}.png");
            }

            return value.ShaderRunner.GetType() switch
            {
                Type t when t == typeof(ContouredLayersRunner) => new($"ms-appx:///Images/Shaders/Blurred/ContouredLayers.png"),
                _ => throw new ArgumentException($"Invalid shader type: {value.ShaderRunner.GetType()}")
            };
        }
    }
}
