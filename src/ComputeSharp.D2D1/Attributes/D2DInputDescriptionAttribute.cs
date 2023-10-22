using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating the description of a given input resource.
/// This attribute is optional, and not using it will cause no description to be declared.
/// </summary>
/// <remarks>
/// <para>This attribute exposes the options that can be set via <c>D2D1_INPUT_DESCRIPTION</c>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/ns-d2d1effectauthor-d2d1_input_description"/>.</para>
/// </remarks>
/// <param name="index">The index of the resource to declare the description for.</param>
/// <param name="filter">The type of filter to apply to the input texture.</param>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = true)]
public sealed class D2DInputDescriptionAttribute(int index, D2D1Filter filter) : Attribute
{
    /// <summary>
    /// Gets the index of the resource to declare the description for.
    /// </summary>
    public int Index => index;

    /// <summary>
    /// Gets the type of filter to apply to the input texture.
    /// </summary>
    public D2D1Filter Filter => filter;

    /// <summary>
    /// Gets the mip level to retrieve from the upstream transform, if specified.
    /// </summary>
    public int LevelOfDetailCount { get; init; }
}