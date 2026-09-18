using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// The vtable for <see cref="ID2D1DrawTransformMapper"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID2D1DrawTransformMapperVftbl
{
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint> Release;
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, ID2D1DrawInfoUpdateContext*, RECT*, RECT*, uint, RECT*, RECT*, int> MapInputRectsToOutputRect;
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, RECT*, RECT*, uint, int> MapOutputRectToInputRects;
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint, RECT, RECT*, int> MapInvalidRect;
}