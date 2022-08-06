using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a <see cref="D2D1TextureResource"/> field in a D2D1 shader indicating the index of the D2D1 resource.
/// This is needed to bind an input resource in a shader to the correct input. Using this attribute is mandatory.
/// </summary>
/// <remarks>
/// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1drawinfo-setresourcetexture"/>.</para>
/// </remarks>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class D2DResourceIndexAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the <see cref="D2DResourceIndexAttribute"/> type with the specified arguments.
    /// </summary>
    /// <param name="index">The index of the annotated <see cref="D2D1TextureResource"/> field.</param>
    public D2DResourceIndexAttribute(int index)
    {
        Index = index;
    }

    /// <summary>
    /// Gets the index of the annotated <see cref="D2D1TextureResource"/> field.
    /// </summary>
    public int Index { get; }
}
