using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating that an input resource at a specified index uses complex sampling.
/// This attribute is optional, and not using it will cause inputs to default to complex sampling as well.
/// </summary>
/// <remarks>
/// <para>This attribute directly maps to <c>#define D2D_INPUT&lt;N&gt;_COMPLEX</c>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct2d/hlsl-helpers"/>.</para>
/// </remarks>
/// <param name="index">The index of the resource to declare as using complex sampling.</param>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
public sealed class D2DInputComplexAttribute(int index) : Attribute
{
    /// <summary>
    /// Gets the index of the resource declared as using complex sampling.
    /// </summary>
    public int Index => index;
}