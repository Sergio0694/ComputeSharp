using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.Graphics.Extensions;

/// <summary>
/// The vtable for <see cref="IWICStreamExtensions.IBufferWriterWrapper"/>, matching the layout of <see cref="IStream"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct IBufferWriterWrapperVftbl
{
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, Guid*, void**, int> QueryInterface;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, uint> AddRef;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, uint> Release;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, void*, uint, uint*, int> Read;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, void*, uint, uint*, int> Write;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, LARGE_INTEGER, uint, ULARGE_INTEGER*, int> Seek;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, ULARGE_INTEGER, int> SetSize;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*, int> CopyTo;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, uint, int> Commit;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, int> Revert;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int> LockRegion;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int> UnlockRegion;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, STATSTG*, uint, int> Stat;
    public delegate* unmanaged[MemberFunction]<IWICStreamExtensions.IBufferWriterWrapper*, IStream**, int> Clone;
}