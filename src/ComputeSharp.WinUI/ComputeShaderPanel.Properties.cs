using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ComputeSharp.WinUI
{
    /// <inheritdoc cref="ComputeShaderPanel"/>
    public sealed unsafe partial class ComputeShaderPanel : SwapChainPanel
    {
        /// <summary>
        /// Gets or sets the <see cref="IShaderRunner"/> instance to use to render content.
        /// </summary>
        public IShaderRunner? ShaderRunner
        {
            get => (IShaderRunner?)GetValue(ShaderFactoryProperty);
            set => SetValue(ShaderFactoryProperty, value);
        }

        /// <summary>
        /// The <see cref="DependencyProperty"/> backing <see cref="ShaderRunner"/>.
        /// </summary>
        public static readonly DependencyProperty ShaderFactoryProperty = DependencyProperty.Register(
            nameof(ShaderRunner),
            typeof(IShaderRunner),
            typeof(ComputeShaderPanel),
            new PropertyMetadata(null, static (d, e) => ((ComputeShaderPanel)d).shaderRunner = (IShaderRunner?)e.NewValue));
    }
}
