using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating the display name of the shader effect to create.
/// </summary>
/// <remarks>
/// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DEffectDisplayNameAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DEffectDisplayNameAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="value">The value for the display name.</param>
    public D2DEffectDisplayNameAttribute(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the display name value.
    /// </summary>
    public string Value { get; }
}