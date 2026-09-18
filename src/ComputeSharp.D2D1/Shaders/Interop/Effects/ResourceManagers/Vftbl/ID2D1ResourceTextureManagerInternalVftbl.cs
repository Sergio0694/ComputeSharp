using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <summary>
/// The vtable for <see cref="ID2D1ResourceTextureManagerInternal"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ID2D1ResourceTextureManagerInternalVftbl
{
    public delegate* unmanaged[MemberFunction]<D2D1ResourceTextureManagerImpl*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<D2D1ResourceTextureManagerImpl*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<D2D1ResourceTextureManagerImpl*, uint> Release;
    public delegate* unmanaged[MemberFunction]<D2D1ResourceTextureManagerImpl*, ID2D1EffectContext*, uint*, int> Initialize;
    public delegate* unmanaged[MemberFunction]<D2D1ResourceTextureManagerImpl*, ID2D1ResourceTexture**, int> GetResourceTexture;
}