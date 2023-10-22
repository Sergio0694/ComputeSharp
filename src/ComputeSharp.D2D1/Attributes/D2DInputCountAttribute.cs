using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating the number of texture inputs for the shader.
/// Using this attribute is mandatory when defining a D2D1 shader.
/// </summary>
/// <remarks>
/// <para>This attribute directly maps to <c>#define D2D_INPUT_COUNT &lt;N&gt;</c>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct2d/hlsl-helpers"/>.</para>
/// </remarks>
/// <param name="count">The number of texture inputs for the shader.</param>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DInputCountAttribute(int count) : Attribute
{
    /// <summary>
    /// Gets the number of texture inputs for the shader.
    /// </summary>
    public int Count => count;
}