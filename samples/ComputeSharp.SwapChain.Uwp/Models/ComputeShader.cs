using Microsoft.Toolkit.Mvvm.ComponentModel;
using ComputeSharp.Uwp;

namespace ComputeSharp.SwapChain.Uwp.Models
{
    /// <summary>
    /// A model for a compute shader.
    /// </summary>
    /// <remarks>This can be converted to a record when bindings are fixed, see notes in the XAML code.</remarks>
    public sealed class ComputeShader : ObservableObject
    {
        /// <summary>
        /// Creates a new <see cref="ComputeShader"/> instance.
        /// </summary>
        /// <param name="name">The name of the shader.</param>
        /// <param name="shaderRunner">The <see cref="IShaderRunner"/> instance to execute.</param>
        public ComputeShader(string name, IShaderRunner shaderRunner)
        {
            Name = name;
            ShaderRunner = shaderRunner;
        }

        /// <summary>
        /// Gets the name of the shader.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the <see cref="IShaderRunner"/> instance to execute.
        /// </summary>
        public IShaderRunner ShaderRunner { get; }

        private bool isSelected;

        /// <summary>
        /// Gets or sets whether the current shader is selected.
        /// </summary>
        public bool IsSelected
        {
            get => this.isSelected;
            set => SetProperty(ref this.isSelected, value);
        }
    }
}