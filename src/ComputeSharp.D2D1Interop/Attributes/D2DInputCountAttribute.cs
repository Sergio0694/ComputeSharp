using System;

namespace ComputeSharp.D2D1Interop;

/// <summary>
/// An attribute for a D2D1 shader indicating the number of texture inputs for the shader.
/// Using this attribute is mandatory when defining a D2D1 shader.
/// </summary>
/// <remarks>
/// <para>This attribute directly maps to <c>#define D2D_INPUT_COUNT &lt;N&gt;</c>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct2d/hlsl-helpers"/>.</para></remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DInputCountAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DInputCountAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="count">The number of texture inputs for the shader.</param>
    public D2DInputCountAttribute(int count)
    {
        Count = count;
    }

    /// <summary>
    /// Gets the number of texture inputs for the shader.
    /// </summary>
    public int Count { get; }
}
