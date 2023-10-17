using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ComputeSharp.Win32;
using static ComputeSharp.Win32.E;
using static ComputeSharp.Win32.IID;
using static ComputeSharp.Win32.S;
using static ComputeSharp.Win32.STATFLAG;
using static ComputeSharp.Win32.STG;
using static ComputeSharp.Win32.STGM;
using static ComputeSharp.Win32.STGTY;

namespace ComputeSharp.Graphics.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="IWICStream"/> type.
/// </summary>
internal static unsafe partial class IWICStreamExtensions
{
    /// <summary>
    /// Initializes an input <see cref="IWICStream"/> wrapping a given <see cref="Stream"/> instance.
    /// </summary>
    /// <param name="stream">The target <see cref="IWICStream"/> object to initialize.</param>
    /// <param name="source">The input <see cref="Stream"/> instance to wrap.</param>
    /// <returns>An <see cref="HRESULT"/> value indicating the operation result.</returns>
    public static HRESULT InitializeFromStream(this ref IWICStream stream, Stream source)
    {
        using ComPtr<IStreamWrapper> streamWrapper = default;

        IStreamWrapper.Create(source, streamWrapper.GetAddressOf());

        return stream.InitializeFromIStream((IStream*)streamWrapper.Get());
    }

    /// <summary>
    /// A manual CCW implementation for an <see cref="IStream"/> object wrapping a <see cref="Stream"/> instance.
    /// </summary>
    private unsafe partial struct IStreamWrapper
#if NET6_0_OR_GREATER
    //: IUnknown.Interface
#endif
    {
#if NET6_0_OR_GREATER
        ///// <inheritdoc/>
        //static Guid* INativeGuid.NativeGuid => (Guid*)default(NotSupportedException).Throw<nint>();
#else
        /// <inheritdoc cref="QueryInterface"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int QueryInterfaceDelegate(IStreamWrapper* @this, Guid* riid, void** ppvObject);

        /// <inheritdoc cref="AddRef"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate uint AddRefDelegate(IStreamWrapper* @this);

        /// <inheritdoc cref="Release"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate uint ReleaseDelegate(IStreamWrapper* @this);

        /// <inheritdoc cref="Read"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT ReadDelegate(IStreamWrapper* @this, void* pv, uint cb, uint* pcbRead);

        /// <inheritdoc cref="Write"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT WriteDelegate(IStreamWrapper* @this, void* pv, uint cb, uint* pcbWritten);

        /// <inheritdoc cref="Seek"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT SeekDelegate(IStreamWrapper* @this, LARGE_INTEGER dlibMove, uint dwOrigin, ULARGE_INTEGER* plibNewPosition);

        /// <inheritdoc cref="SetSize"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT SetSizeDelegate(IStreamWrapper* @this, ULARGE_INTEGER libNewSize);

        /// <inheritdoc cref="CopyTo"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT CopyToDelegate(IStreamWrapper* @this, IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten);

        /// <inheritdoc cref="Commit"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT CommitDelegate(IStreamWrapper* @this, uint grfCommitFlags);

        /// <inheritdoc cref="Revert"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT RevertDelegate(IStreamWrapper* @this);

        /// <inheritdoc cref="LockRegion"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT LockRegionDelegate(IStreamWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType);

        /// <inheritdoc cref="UnlockRegion"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT UnlockRegionDelegate(IStreamWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType);

        /// <inheritdoc cref="Stat"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT StatDelegate(IStreamWrapper* @this, STATSTG* pstatstg, uint grfStatFlag);

        /// <inheritdoc cref="Clone"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT CloneDelegate(IStreamWrapper* @this, IStream** ppstm);

        /// <summary>
        /// A cached <see cref="QueryInterfaceDelegate"/> instance wrapping <see cref="QueryInterface"/>.
        /// </summary>
        private static readonly QueryInterfaceDelegate QueryInterfaceWrapper = QueryInterface;

        /// <summary>
        /// A cached <see cref="AddRefDelegate"/> instance wrapping <see cref="AddRef"/>.
        /// </summary>
        private static readonly AddRefDelegate AddRefWrapper = AddRef;

        /// <summary>
        /// A cached <see cref="ReleaseDelegate"/> instance wrapping <see cref="Release"/>.
        /// </summary>
        private static readonly ReleaseDelegate ReleaseWrapper = Release;

        /// <summary>
        /// A cached <see cref="ReadDelegate"/> instance wrapping <see cref="Read"/>.
        /// </summary>
        private static readonly ReadDelegate ReadWrapper = Read;

        /// <summary>
        /// A cached <see cref="WriteDelegate"/> instance wrapping <see cref="Write"/>.
        /// </summary>
        private static readonly WriteDelegate WriteWrapper = Write;

        /// <summary>
        /// A cached <see cref="SeekDelegate"/> instance wrapping <see cref="Seek"/>.
        /// </summary>
        private static readonly SeekDelegate SeekWrapper = Seek;

        /// <summary>
        /// A cached <see cref="SetSizeDelegate"/> instance wrapping <see cref="SetSize"/>.
        /// </summary>
        private static readonly SetSizeDelegate SetSizeWrapper = SetSize;

        /// <summary>
        /// A cached <see cref="CopyToDelegate"/> instance wrapping <see cref="CopyTo"/>.
        /// </summary>
        private static readonly CopyToDelegate CopyToWrapper = CopyTo;

        /// <summary>
        /// A cached <see cref="CommitDelegate"/> instance wrapping <see cref="Commit"/>.
        /// </summary>
        private static readonly CommitDelegate CommitWrapper = Commit;

        /// <summary>
        /// A cached <see cref="RevertDelegate"/> instance wrapping <see cref="Revert"/>.
        /// </summary>
        private static readonly RevertDelegate RevertWrapper = Revert;

        /// <summary>
        /// A cached <see cref="LockRegionDelegate"/> instance wrapping <see cref="LockRegion"/>.
        /// </summary>
        private static readonly LockRegionDelegate LockRegionWrapper = LockRegion;

        /// <summary>
        /// A cached <see cref="UnlockRegionDelegate"/> instance wrapping <see cref="UnlockRegion"/>.
        /// </summary>
        private static readonly UnlockRegionDelegate UnlockRegionWrapper = UnlockRegion;

        /// <summary>
        /// A cached <see cref="StatDelegate"/> instance wrapping <see cref="Stat"/>.
        /// </summary>
        private static readonly StatDelegate StatWrapper = Stat;

        /// <summary>
        /// A cached <see cref="CloneDelegate"/> instance wrapping <see cref="Clone"/>.
        /// </summary>
        private static readonly CloneDelegate CloneWrapper = Clone;
#endif

        /// <summary>
        /// The shared vtable pointer for <see cref="IStreamWrapper"/> instances.
        /// </summary>
        private static readonly void** Vtbl = InitVtbl();

        /// <summary>
        /// Setups the vtable pointer for <see cref="IStreamWrapper"/>.
        /// </summary>
        /// <returns>The initialized vtable pointer for <see cref="IStreamWrapper"/>.</returns>
        private static void** InitVtbl()
        {
            void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(IStreamWrapper), sizeof(void*) * 14);

#if NET6_0_OR_GREATER
            lpVtbl[0] = (delegate* unmanaged<IStreamWrapper*, Guid*, void**, int>)&QueryInterface;
            lpVtbl[1] = (delegate* unmanaged<IStreamWrapper*, uint>)&AddRef;
            lpVtbl[2] = (delegate* unmanaged<IStreamWrapper*, uint>)&Release;
            lpVtbl[3] = (delegate* unmanaged<IStreamWrapper*, void*, uint, uint*, HRESULT>)&Read;
            lpVtbl[4] = (delegate* unmanaged<IStreamWrapper*, void*, uint, uint*, HRESULT>)&Write;
            lpVtbl[5] = (delegate* unmanaged<IStreamWrapper*, LARGE_INTEGER, uint, ULARGE_INTEGER*, HRESULT>)&Seek;
            lpVtbl[6] = (delegate* unmanaged<IStreamWrapper*, ULARGE_INTEGER, HRESULT>)&SetSize;
            lpVtbl[7] = (delegate* unmanaged<IStreamWrapper*, IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*, HRESULT>)&CopyTo;
            lpVtbl[8] = (delegate* unmanaged<IStreamWrapper*, uint, HRESULT>)&Commit;
            lpVtbl[9] = (delegate* unmanaged<IStreamWrapper*, HRESULT>)&Revert;
            lpVtbl[10] = (delegate* unmanaged<IStreamWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, HRESULT>)&LockRegion;
            lpVtbl[11] = (delegate* unmanaged<IStreamWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, HRESULT>)&UnlockRegion;
            lpVtbl[12] = (delegate* unmanaged<IStreamWrapper*, STATSTG*, uint, HRESULT>)&Stat;
            lpVtbl[13] = (delegate* unmanaged<IStreamWrapper*, IStream**, HRESULT>)&Clone;
#else
            lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(QueryInterfaceWrapper);
            lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(AddRefWrapper);
            lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ReleaseWrapper);
            lpVtbl[3] = (void*)Marshal.GetFunctionPointerForDelegate(ReadWrapper);
            lpVtbl[4] = (void*)Marshal.GetFunctionPointerForDelegate(WriteWrapper);
            lpVtbl[5] = (void*)Marshal.GetFunctionPointerForDelegate(SeekWrapper);
            lpVtbl[6] = (void*)Marshal.GetFunctionPointerForDelegate(SetSizeWrapper);
            lpVtbl[7] = (void*)Marshal.GetFunctionPointerForDelegate(CopyToWrapper);
            lpVtbl[8] = (void*)Marshal.GetFunctionPointerForDelegate(CommitWrapper);
            lpVtbl[9] = (void*)Marshal.GetFunctionPointerForDelegate(RevertWrapper);
            lpVtbl[10] = (void*)Marshal.GetFunctionPointerForDelegate(LockRegionWrapper);
            lpVtbl[11] = (void*)Marshal.GetFunctionPointerForDelegate(UnlockRegionWrapper);
            lpVtbl[12] = (void*)Marshal.GetFunctionPointerForDelegate(StatWrapper);
            lpVtbl[13] = (void*)Marshal.GetFunctionPointerForDelegate(CloneWrapper);
#endif

            return lpVtbl;
        }

        /// <summary>
        /// The vtable pointer for the current instance.
        /// </summary>
        private void** lpVtbl;

        /// <summary>
        /// The current reference count for the object (from <c>IUnknown</c>).
        /// </summary>
        private volatile int referenceCount;

        /// <summary>
        /// The <see cref="GCHandle"/> to the captured <see cref="Stream"/>.
        /// </summary>
        private GCHandle streamHandle;

        /// <summary>
        /// Creates and initializes a new <see cref="IStreamWrapper"/> instance.
        /// </summary>
        /// <param name="stream">The input <see cref="Stream"/> instance to wrap.</param>
        /// <param name="streamWrapper">The target <see cref="IStreamWrapper"/> instance to initialize.</param>
        public static void Create(Stream stream, IStreamWrapper** streamWrapper)
        {
            IStreamWrapper* @this = (IStreamWrapper*)NativeMemory.Alloc((nuint)sizeof(IStreamWrapper));

            @this->lpVtbl = Vtbl;
            @this->referenceCount = 1;
            @this->streamHandle = GCHandle.Alloc(stream);

            *streamWrapper = @this;
        }

        /// <summary>
        /// Gets the captured <see cref="Stream"/> instance.
        /// </summary>
        /// <returns>The captured <see cref="Stream"/> instance</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Stream GetStream()
        {
            return Unsafe.As<Stream>(this.streamHandle.Target!);
        }

#if NET6_0_OR_GREATER
        /// <inheritdoc/>
        public HRESULT QueryInterface(Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IStreamWrapper*, Guid*, void**, HRESULT>)this.lpVtbl[0])((IStreamWrapper*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        /// <inheritdoc/>
        public uint AddRef()
        {
            return ((delegate* unmanaged<IStreamWrapper*, uint>)this.lpVtbl[1])((IStreamWrapper*)Unsafe.AsPointer(ref this));
        }

        /// <inheritdoc/>
        public uint Release()
        {
            return ((delegate* unmanaged<IStreamWrapper*, uint>)this.lpVtbl[2])((IStreamWrapper*)Unsafe.AsPointer(ref this));
        }
#endif

        /// <inheritdoc cref="IStream.QueryInterface(Guid*, void**)"/>
        [UnmanagedCallersOnly]
        private static int QueryInterface(IStreamWrapper* @this, Guid* riid, void** ppvObject)
        {
            if (ppvObject is null)
            {
                return E_POINTER;
            }

            if (riid->Equals(IID_IUnknown) ||
                riid->Equals(IID_ISequentialStream) ||
                riid->Equals(IID_IStream))
            {
                _ = Interlocked.Increment(ref @this->referenceCount);

                *ppvObject = @this;

                return S_OK;
            }

            *ppvObject = null;

            return E_NOINTERFACE;
        }

        /// <inheritdoc cref="IStream.AddRef"/>
        [UnmanagedCallersOnly]
        private static uint AddRef(IStreamWrapper* @this)
        {
            return (uint)Interlocked.Increment(ref @this->referenceCount);
        }

        /// <inheritdoc cref="IStream.Release"/>
        [UnmanagedCallersOnly]
        private static uint Release(IStreamWrapper* @this)
        {
            uint referenceCount = (uint)Interlocked.Decrement(ref @this->referenceCount);

            if (referenceCount == 0)
            {
                @this->streamHandle.Free();

                NativeMemory.Free(@this);
            }

            return referenceCount;
        }

        /// <inheritdoc cref="IStream.Read(void*, uint, uint*)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Read(IStreamWrapper* @this, void* pv, uint cb, uint* pcbRead)
        {
            if (pv == null)
            {
                return E_POINTER;
            }

            if (cb > int.MaxValue)
            {
                return E_INVALIDARG;
            }

            if (pcbRead != null)
            {
                *pcbRead = 0;
            }

            try
            {
                Span<byte> dst = new(pv, (int)cb);

                while (dst.Length > 0)
                {
                    int bytesRead = @this->GetStream().Read(dst);

                    if (bytesRead == 0)
                    {
                        return S_FALSE;
                    }

                    if (pcbRead != null)
                    {
                        *pcbRead += (uint)bytesRead;
                    }

                    dst = dst.Slice(bytesRead);
                }

                return S_OK;
            }
            catch (Exception ex)
            {
                return Marshal.GetHRForException(ex);
            }
        }

        /// <inheritdoc cref="IStream.Write(void*, uint, uint*)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Write(IStreamWrapper* @this, void* pv, uint cb, uint* pcbWritten)
        {
            if (pv == null)
            {
                return E_POINTER;
            }

            if (cb > int.MaxValue)
            {
                return E_INVALIDARG;
            }

            if (pcbWritten != null)
            {
                *pcbWritten = 0;
            }

            try
            {
                @this->GetStream().Write(new ReadOnlySpan<byte>(pv, (int)cb));

                if (pcbWritten != null)
                {
                    *pcbWritten = cb;
                }

                return S_OK;
            }
            catch (Exception ex)
            {
                return Marshal.GetHRForException(ex);
            }
        }

        /// <inheritdoc cref="IStream.Seek(LARGE_INTEGER, uint, ULARGE_INTEGER*)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Seek(IStreamWrapper* @this, LARGE_INTEGER dlibMove, uint dwOrigin, ULARGE_INTEGER* plibNewPosition)
        {
            SeekOrigin origin = (SeekOrigin)dwOrigin;

            switch (origin)
            {
                case SeekOrigin.Begin:
                case SeekOrigin.Current:
                case SeekOrigin.End:
                    break;
                default:
                    return E_INVALIDARG;
            }

            if (plibNewPosition != null)
            {
                *plibNewPosition = default;
            }

            try
            {
                long newPosition = @this->GetStream().Seek(dlibMove.QuadPart, origin);

                if (plibNewPosition != null)
                {
                    plibNewPosition->QuadPart = (ulong)newPosition;
                }

                return S_OK;
            }
            catch (Exception ex)
            {
                return Marshal.GetHRForException(ex);
            }
        }

        /// <inheritdoc cref="IStream.SetSize(ULARGE_INTEGER)"/>
        [UnmanagedCallersOnly]
        private static HRESULT SetSize(IStreamWrapper* @this, ULARGE_INTEGER libNewSize)
        {
            if (libNewSize.QuadPart > long.MaxValue)
            {
                return E_INVALIDARG;
            }

            try
            {
                @this->GetStream().SetLength((long)libNewSize.QuadPart);

                return S_OK;
            }
            catch (Exception ex)
            {
                return Marshal.GetHRForException(ex);
            }
        }

        /// <inheritdoc cref="IStream.CopyTo(IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*)"/>
        [UnmanagedCallersOnly]
        private static HRESULT CopyTo(IStreamWrapper* @this, IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten)
        {
            return E_NOTIMPL;
        }

        /// <inheritdoc cref="IStream.Commit(uint)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Commit(IStreamWrapper* @this, uint grfCommitFlags)
        {
            try
            {
                @this->GetStream().Flush();

                return S_OK;
            }
            catch (Exception ex)
            {
                return Marshal.GetHRForException(ex);
            }
        }

        /// <inheritdoc cref="IStream.Revert"/>
        [UnmanagedCallersOnly]
        private static HRESULT Revert(IStreamWrapper* @this)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.LockRegion(ULARGE_INTEGER, ULARGE_INTEGER, uint)"/>
        [UnmanagedCallersOnly]
        private static HRESULT LockRegion(IStreamWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.UnlockRegion(ULARGE_INTEGER, ULARGE_INTEGER, uint)"/>
        [UnmanagedCallersOnly]
        private static HRESULT UnlockRegion(IStreamWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.Stat(STATSTG*, uint)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Stat(IStreamWrapper* @this, STATSTG* pstatstg, uint grfStatFlag)
        {
            if (pstatstg == null)
            {
                return E_POINTER;
            }

            *pstatstg = default;

            STATFLAG statFlag = (STATFLAG)grfStatFlag;

            if ((statFlag & (STATFLAG_DEFAULT | STATFLAG_NONAME | STATFLAG_NOOPEN)) != statFlag)
            {
                return E_INVALIDARG;
            }

            try
            {
                Stream stream = @this->GetStream();

                if (!statFlag.HasFlag(STATFLAG_NONAME))
                {
                    if (stream is FileStream asFileStream)
                    {
                        pstatstg->pwcsName = (ushort*)Marshal.StringToCoTaskMemUni(asFileStream.Name);
                    }
                }

                bool canRead = stream.CanRead;
                bool canWrite = stream.CanWrite;

                if (canRead && !canWrite)
                {
                    pstatstg->grfMode = STGM_READ;
                }
                else if (canRead && canWrite)
                {
                    pstatstg->grfMode = STGM_READWRITE;
                }
                else if (!canRead && canWrite)
                {
                    pstatstg->grfMode = STGM_WRITE;
                }
                else
                {
                    default(NotSupportedException).Throw();
                }

                pstatstg->type = (uint)STGTY_STREAM;
                pstatstg->cbSize.QuadPart = (ulong)stream.Length;

                return S_OK;
            }
            catch (Exception ex)
            {
                if (pstatstg->pwcsName != null)
                {
                    Marshal.FreeCoTaskMem((IntPtr)pstatstg->pwcsName);

                    pstatstg->pwcsName = null;
                }

                return Marshal.GetHRForException(ex);
            }
        }

        /// <inheritdoc cref="IStream.Clone(IStream**)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Clone(IStreamWrapper* @this, IStream** ppstm)
        {
            return STG_E_INVALIDFUNCTION;
        }
    }
}