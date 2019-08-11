using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using DirectX12GameEngine.Shaders.Renderer.Models;

namespace DirectX12GameEngine.Shaders.Renderer
{
    /// <summary>
    /// A <see langword="class"/> that uses the mustache templating system to render HLSL shaders
    /// </summary>
    public sealed class ShaderRenderer
    {
        /// <summary>
        /// Gets the singleton <see cref="ShaderRenderer"/> instance to use
        /// </summary>
        public static ShaderRenderer Instance { get; } = new ShaderRenderer();

        /// <summary>
        /// Creates a new <see cref="ShaderRenderer"/> instance and loads the mustache template to use
        /// </summary>
        private ShaderRenderer()
        {
            string
                assemblyPath = Assembly.GetExecutingAssembly().Location,
                assemblyDirectory = Path.GetDirectoryName(assemblyPath) ?? throw new InvalidOperationException("Can't find the assembly directory"),
                templatePath = Path.Combine(assemblyDirectory, "Assets", "ShaderTemplate.mustache");
            Template = File.ReadAllText(templatePath);
        }

        /// <summary>
        /// Gets the mustache template used by the shader renderer
        /// </summary>
        public string Template { get; }

        /// <summary>
        /// Renders a new HLSL shader source with the given parameters
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with the shader information</param>
        /// <returns>The source code for the new HLSL shader</returns>
        [Pure]
        public string Render(ShaderInfo info) => Nustache.Core.Render.StringToString(Template, info);
    }
}
