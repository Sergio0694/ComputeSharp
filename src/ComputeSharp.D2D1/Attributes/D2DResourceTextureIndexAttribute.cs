using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for resource fields in a D2D1 shader indicating the index of the D2D1 resource texture.
/// This is needed to bind an input resource in a shader to the correct input. Using this attribute is mandatory.
/// </summary>
/// <remarks>
/// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1drawinfo-setresourcetexture"/>.</para>
/// </remarks>
/// <param name="index">The index of the annotated resource field.</param>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class D2DResourceTextureIndexAttribute(int index) : Attribute
{
    /// <summary>
    /// Gets the index of the annotated resource field.
    /// </summary>
    public int Index { get; } = index;
}