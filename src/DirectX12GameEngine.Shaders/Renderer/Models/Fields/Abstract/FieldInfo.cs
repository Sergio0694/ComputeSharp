#pragma warning disable CS8618 // Non-nullable field is uninitialized

namespace DirectX12GameEngine.Shaders.Renderer.Models.Abstract
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a shader field
    /// </summary>
    public abstract class FieldInfo
    {
        /// <summary>
        /// Gets or sets the type of the current field
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// Gets or sets the name to use for the current field
        /// </summary>
        public string FieldName { get; set; }
    }
}
