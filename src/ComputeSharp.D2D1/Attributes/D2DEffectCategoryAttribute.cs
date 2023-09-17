using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating the category of the shader effect to create.
/// </summary>
/// <remarks>
/// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Assembly, AllowMultiple = false)]
public sealed class D2DEffectCategoryAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DEffectCategoryAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="value">The value for the category.</param>
    public D2DEffectCategoryAttribute(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the category value.
    /// </summary>
    public string Value { get; }
}