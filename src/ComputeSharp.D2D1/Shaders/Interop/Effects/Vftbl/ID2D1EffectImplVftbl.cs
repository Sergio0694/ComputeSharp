using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <summary>
/// The vtable for <see cref="ID2D1EffectImpl"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID2D1EffectImplVftbl
{
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint> Release;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, ID2D1EffectContext*, ID2D1TransformGraph*, int> Initialize;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, D2D1_CHANGE_TYPE, int> PrepareForRender;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, ID2D1TransformGraph*, int> SetGraph;
}