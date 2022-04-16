using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating that an input resource at a specified index uses simple
/// sampling. This attribute is optional, and not using it will cause inputs to default to complex sampling.
/// </summary>
/// <remarks>
/// <para>This attribute directly maps to <c>#define D2D_INPUT&lt;N&gt;_SIMPLE</c>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct2d/hlsl-helpers"/>.</para></remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
public sealed class D2DInputSimpleAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DInputSimpleAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="index">The index of the resource to declare as using simple sampling.</param>
    public D2DInputSimpleAttribute(int index)
    {
        Index = index;
    }

    /// <summary>
    /// Gets the index of the resource declared as using simple sampling.
    /// </summary>
    public int Index { get; }
}
