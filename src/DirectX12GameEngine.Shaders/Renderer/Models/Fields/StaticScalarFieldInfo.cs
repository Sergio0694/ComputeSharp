using DirectX12GameEngine.Shaders.Renderer.Models.Abstract;

namespace DirectX12GameEngine.Shaders.Renderer.Models.Fields
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a static scalar field
    /// </summary>
    public sealed class StaticScalarFieldInfo : FieldInfo
    {
        /// <summary>
        /// Gets whether or not the current <see cref="FieldInfo"/> instance represents a static scalar value (always <see langword="true"/>)
        /// </summary>
        public bool IsStaticScalarField { get; } = true;

        /// <summary>
        /// Gets or sets the value to set for the current field
        /// </summary>
        public object FieldValue { get; set; }
    }
}
