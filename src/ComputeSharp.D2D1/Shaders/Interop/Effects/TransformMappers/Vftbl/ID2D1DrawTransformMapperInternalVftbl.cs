using System;
using System.Runtime.InteropServices;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// The vtable for <see cref="ID2D1DrawTransformMapperInternal"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID2D1DrawTransformMapperInternalVftbl
{
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, uint> Release;
    public delegate* unmanaged[MemberFunction]<D2D1DrawTransformMapperImpl*, void**, int> GetManagedWrapperHandle;
}