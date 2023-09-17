using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating the description of the shader effect to create.
/// </summary>
/// <remarks>
/// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DEffectDescriptionAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DEffectDescriptionAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="value">The value for the description.</param>
    public D2DEffectDescriptionAttribute(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the description value.
    /// </summary>
    public string Value { get; }
}