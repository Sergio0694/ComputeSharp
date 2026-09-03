using System;
using System.Runtime.InteropServices;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// The vtable for <see cref="ID2D1DrawInfoUpdateContext"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID2D1DrawInfoUpdateContextVftbl
{
    public delegate* unmanaged[MemberFunction]<D2D1DrawInfoUpdateContextImpl*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<D2D1DrawInfoUpdateContextImpl*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<D2D1DrawInfoUpdateContextImpl*, uint> Release;
    public delegate* unmanaged[MemberFunction]<D2D1DrawInfoUpdateContextImpl*, uint*, int> GetConstantBufferSize;
    public delegate* unmanaged[MemberFunction]<D2D1DrawInfoUpdateContextImpl*, byte*, uint, int> GetConstantBuffer;
    public delegate* unmanaged[MemberFunction]<D2D1DrawInfoUpdateContextImpl*, byte*, uint, int> SetConstantBuffer;
}