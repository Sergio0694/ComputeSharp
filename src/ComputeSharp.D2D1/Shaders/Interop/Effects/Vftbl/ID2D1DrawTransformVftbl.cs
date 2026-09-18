using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <summary>
/// The vtable for <see cref="ID2D1DrawTransform"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID2D1DrawTransformVftbl
{
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint> Release;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint> GetInputCount;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, RECT*, RECT*, uint, int> MapOutputRectToInputRects;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, RECT*, RECT*, uint, RECT*, RECT*, int> MapInputRectsToOutputRect;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, uint, RECT, RECT*, int> MapInvalidRect;
    public delegate* unmanaged[MemberFunction]<PixelShaderEffect*, ID2D1DrawInfo*, int> SetDrawInfo;
}