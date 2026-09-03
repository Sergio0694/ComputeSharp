using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.Graphics.Extensions;

/// <summary>
/// The vtable for <see cref="IWICStreamExtensions.IStreamWrapper"/>, matching the layout of <see cref="IStream"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct IStreamWrapperVftbl
{
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, uint> Release;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, void*, uint, uint*, int> Read;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, void*, uint, uint*, int> Write;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, LARGE_INTEGER, uint, ULARGE_INTEGER*, int> Seek;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, ULARGE_INTEGER, int> SetSize;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*, int> CopyTo;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, uint, int> Commit;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, int> Revert;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int> LockRegion;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int> UnlockRegion;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, STATSTG*, uint, int> Stat;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IStreamWrapper*, IStream**, int> Clone;
}