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

        /// <summary>
        /// Creates a new <see cref="StaticScalarFieldInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="fieldType">The type of the current field</param>
        /// <param name="fieldName">The name of the current field</param>
        /// <param name="fieldValue">The static value to assign to the current field</param>
        public StaticScalarFieldInfo(string fieldType, string fieldName, object fieldValue) : base(fieldType, fieldName)
        {
            FieldValue = fieldValue;
        }
    }
}
