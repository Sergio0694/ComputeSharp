#pragma warning disable CS1572, CS1573

namespace ComputeSharp.Shaders.Renderer.Models
{
    /// <summary>
    /// A model that contains info on a shader field.
    /// </summary>
    /// <param name="FieldType">The type of the current field in the HLSL shader.</param>
    /// <param name="FieldName">The name to use for the current field.</param>
    internal record CapturedFieldInfo(string FieldType, string FieldName);
}
