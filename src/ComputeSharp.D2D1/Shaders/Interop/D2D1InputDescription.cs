using System.Runtime.InteropServices;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A type containing info on a description for a given D2D1 pixel shader input resource.
/// </summary>
/// <remarks>
/// <para>This type exposes the values that can be set via <c>ID2D1RenderInfo::SetInputDescription</c>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1renderinfo-setinputdescription"/>.</para>
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly struct D2D1InputDescription
{
    /// <summary>
    /// The index of the resource the description belongs to.
    /// </summary>
    /// <remarks>
    /// This field and the ones below are explicity used because this type is in the type layout of <see cref="Effects.PixelShaderEffect"/>.
    /// Since that type is used in an unmanaged context, MCG will produce a shadow copy to inform the runtime marshaller. Such a shallow copy
    /// cannot process the type if it has unspeakable names in any of its members, which is the case for the generated fields. As such, this
    /// type has to use explicit fields here and not readonly autoproperties, or compiling a project using ComputeSharp.D2D1 would fail on UWP.
    /// </remarks>
    private readonly int index;

    /// <summary>
    /// The type of filter to apply to the input texture.
    /// </summary>
    private readonly D2D1Filter filter;

    /// <summary>
    /// The mip level to retrieve from the upstream transform, if specified.
    /// </summary>
    private readonly int levelOfDetailCount;

    /// <summary>
    /// Creates a new <see cref="D2D1InputDescription"/> instance with the specified parameters.
    /// </summary>
    /// <param name="index">The index of the resource the description belongs to.</param>
    /// <param name="filter">The type of filter to apply to the input texture.</param>
    public D2D1InputDescription(int index, D2D1Filter filter)
    {
        this.index = index;
        this.filter = filter;
    }

    /// <summary>
    /// Gets the index of the resource the description belongs to.
    /// </summary>
    public int Index => this.index;

    /// <summary>
    /// Gets the type of filter to apply to the input texture.
    /// </summary>
    public D2D1Filter Filter => this.filter;

    /// <summary>
    /// Gets the mip level to retrieve from the upstream transform, if specified.
    /// </summary>
    public int LevelOfDetailCount
    {
        get => this.levelOfDetailCount;
        init => this.levelOfDetailCount = value;
    }
}