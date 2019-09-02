using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using ComputeSharp.Shaders.Renderer.Models;
using Stubble.Core;

namespace ComputeSharp.Shaders.Renderer
{
    /// <summary>
    /// A <see langword="class"/> that uses the mustache templating system to render HLSL shaders
    /// </summary>
    internal sealed class ShaderRenderer
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
            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = assembly.GetManifestResourceNames().First(name => name.EndsWith("ShaderTemplate.mustache"));

            using Stream stream = assembly.GetManifestResourceStream(filename);
            using StreamReader reader = new StreamReader(stream);

            Template = reader.ReadToEnd();
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
        public string Render(ShaderInfo info) => StaticStubbleRenderer.Render(Template, info);
    }
}
