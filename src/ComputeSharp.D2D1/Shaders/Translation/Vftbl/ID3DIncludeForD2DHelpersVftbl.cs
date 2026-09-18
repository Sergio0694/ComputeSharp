using System.Runtime.InteropServices;
#if SOURCE_GENERATOR
using Windows.Win32.Graphics.Direct3D;
#else
using ComputeSharp.Win32;
#endif

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <summary>
/// The vtable for <see cref="ID3DInclude"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID3DIncludeForD2DHelpersVftbl
{
    public delegate* unmanaged[MemberFunction]<D3DCompiler.ID3DIncludeForD2DHelpers*, D3D_INCLUDE_TYPE, sbyte*, void*, void**, uint*, int> Open;
    public delegate* unmanaged[MemberFunction]<D3DCompiler.ID3DIncludeForD2DHelpers*, void*, int> Close;
}