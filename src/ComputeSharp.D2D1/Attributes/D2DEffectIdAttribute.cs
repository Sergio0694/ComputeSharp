using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating the id of the shader effect to create.
/// </summary>
/// <remarks>
/// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DEffectIdAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DEffectIdAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="value">The value for the effect id.</param>
    public D2DEffectIdAttribute(string value)
    {
        Value = Guid.Parse(value);
    }

    /// <summary>
    /// Gets the number of texture inputs for the shader.
    /// </summary>
    public Guid Value { get; }
}