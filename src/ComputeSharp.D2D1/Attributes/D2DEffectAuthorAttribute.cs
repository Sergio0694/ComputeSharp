using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating the author of the shader effect to create.
/// </summary>
/// <remarks>
/// This only applies to effects created from <see cref="Interop.D2D1PixelShaderEffect"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Assembly, AllowMultiple = false)]
public sealed class D2DEffectAuthorAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DEffectAuthorAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="value">The value for the author.</param>
    public D2DEffectAuthorAttribute(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the author value.
    /// </summary>
    public string Value { get; }
}