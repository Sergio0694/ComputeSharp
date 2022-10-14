using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.Windows.E;
using static TerraFX.Interop.Windows.IID;
using static TerraFX.Interop.Windows.S;
using static TerraFX.Interop.Windows.STATFLAG;
using static TerraFX.Interop.Windows.STG;
using static TerraFX.Interop.Windows.STGM;
using static TerraFX.Interop.Windows.STGTY;
#if NET6_0_OR_GREATER
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RuntimeHelpers = ComputeSharp.NetStandard.System.Runtime.CompilerServices.RuntimeHelpers;
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.Graphics.Extensions;

/// <inheritdoc/>
unsafe partial class IWICStreamExtensions
{
    /// <summary>
    /// Initializes an input <see cref="IWICStream"/> wrapping a given <see cref="IBufferWriter{T}"/> instance.
    /// </summary>
    /// <param name="stream">The target <see cref="IWICStream"/> object to initialize.</param>
    /// <param name="destination">The input <see cref="IBufferWriter{T}"/> instance to wrap.</param>
    /// <returns>An <see cref="HRESULT"/> value indicating the operation result.</returns>
    public static HRESULT InitializeFromBufferWriter(this ref IWICStream stream, IBufferWriter<byte> destination)
    {
        using ComPtr<IBufferWriterWrapper> streamWrapper = default;

        IBufferWriterWrapper.Create(destination, streamWrapper.GetAddressOf());

        return stream.InitializeFromIStream((IStream*)streamWrapper.Get());
    }

    /// <summary>
    /// A manual CCW implementation for an <see cref="IStream"/> object wrapping an <see cref="IBufferWriter{T}"/> instance.
    /// </summary>
    internal unsafe partial struct IBufferWriterWrapper
#if NET6_0_OR_GREATER
        : IUnknown.Interface
#endif
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="QueryInterface"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int QueryInterfaceDelegate(IBufferWriterWrapper* @this, Guid* riid, void** ppvObject);

        /// <inheritdoc cref="AddRef"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate uint AddRefDelegate(IBufferWriterWrapper* @this);

        /// <inheritdoc cref="Release"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate uint ReleaseDelegate(IBufferWriterWrapper* @this);

        /// <inheritdoc cref="Read"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT ReadDelegate(IBufferWriterWrapper* @this, void* pv, uint cb, uint* pcbRead);

        /// <inheritdoc cref="Write"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT WriteDelegate(IBufferWriterWrapper* @this, void* pv, uint cb, uint* pcbWritten);

        /// <inheritdoc cref="Seek"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT SeekDelegate(IBufferWriterWrapper* @this, LARGE_INTEGER dlibMove, uint dwOrigin, ULARGE_INTEGER* plibNewPosition);

        /// <inheritdoc cref="SetSize"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT SetSizeDelegate(IBufferWriterWrapper* @this, ULARGE_INTEGER libNewSize);

        /// <inheritdoc cref="CopyTo"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT CopyToDelegate(IBufferWriterWrapper* @this, IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten);

        /// <inheritdoc cref="Commit"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT CommitDelegate(IBufferWriterWrapper* @this, uint grfCommitFlags);

        /// <inheritdoc cref="Revert"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT RevertDelegate(IBufferWriterWrapper* @this);

        /// <inheritdoc cref="LockRegion"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT LockRegionDelegate(IBufferWriterWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType);

        /// <inheritdoc cref="UnlockRegion"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT UnlockRegionDelegate(IBufferWriterWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType);

        /// <inheritdoc cref="Stat"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT StatDelegate(IBufferWriterWrapper* @this, STATSTG* pstatstg, uint grfStatFlag);

        /// <inheritdoc cref="Clone"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate HRESULT CloneDelegate(IBufferWriterWrapper* @this, IStream** ppstm);

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
        /// The shared vtable pointer for <see cref="IBufferWriterWrapper"/> instances.
        /// </summary>
        private static readonly void** Vtbl = InitVtbl();

        /// <summary>
        /// Setups the vtable pointer for <see cref="IBufferWriterWrapper"/>.
        /// </summary>
        /// <returns>The initialized vtable pointer for <see cref="IBufferWriterWrapper"/>.</returns>
        private static void** InitVtbl()
        {
            void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(IBufferWriterWrapper), sizeof(void*) * 14);

#if NET6_0_OR_GREATER
            lpVtbl[0] = (delegate* unmanaged<IBufferWriterWrapper*, Guid*, void**, int>)&QueryInterface;
            lpVtbl[1] = (delegate* unmanaged<IBufferWriterWrapper*, uint>)&AddRef;
            lpVtbl[2] = (delegate* unmanaged<IBufferWriterWrapper*, uint>)&Release;
            lpVtbl[3] = (delegate* unmanaged<IBufferWriterWrapper*, void*, uint, uint*, HRESULT>)&Read;
            lpVtbl[4] = (delegate* unmanaged<IBufferWriterWrapper*, void*, uint, uint*, HRESULT>)&Write;
            lpVtbl[5] = (delegate* unmanaged<IBufferWriterWrapper*, LARGE_INTEGER, uint, ULARGE_INTEGER*, HRESULT>)&Seek;
            lpVtbl[6] = (delegate* unmanaged<IBufferWriterWrapper*, ULARGE_INTEGER, HRESULT>)&SetSize;
            lpVtbl[7] = (delegate* unmanaged<IBufferWriterWrapper*, IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*, HRESULT>)&CopyTo;
            lpVtbl[8] = (delegate* unmanaged<IBufferWriterWrapper*, uint, HRESULT>)&Commit;
            lpVtbl[9] = (delegate* unmanaged<IBufferWriterWrapper*, HRESULT>)&Revert;
            lpVtbl[10] = (delegate* unmanaged<IBufferWriterWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, HRESULT>)&LockRegion;
            lpVtbl[11] = (delegate* unmanaged<IBufferWriterWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, HRESULT>)&UnlockRegion;
            lpVtbl[12] = (delegate* unmanaged<IBufferWriterWrapper*, STATSTG*, uint, HRESULT>)&Stat;
            lpVtbl[13] = (delegate* unmanaged<IBufferWriterWrapper*, IStream**, HRESULT>)&Clone;
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
        public void** lpVtbl;

        /// <summary>
        /// The current reference count for the object (from <c>IUnknown</c>).
        /// </summary>
        public volatile int referenceCount;

        /// <summary>
        /// The <see cref="GCHandle"/> to the captured <see cref="IBufferWriter{T}"/>.
        /// </summary>
        public GCHandle writerHandle;

        /// <summary>
        /// Creates and initializes a new <see cref="IBufferWriterWrapper"/> instance.
        /// </summary>
        /// <param name="writer">The input <see cref="IBufferWriter{T}"/> instance to wrap.</param>
        /// <param name="streamWrapper">The target <see cref="IBufferWriterWrapper"/> instance to initialize.</param>
        public static void Create(IBufferWriter<byte> writer, IBufferWriterWrapper** streamWrapper)
        {
            IBufferWriterWrapper* @this = (IBufferWriterWrapper*)NativeMemory.Alloc((nuint)sizeof(IBufferWriterWrapper));

            @this->lpVtbl = Vtbl;
            @this->referenceCount = 1;
            @this->writerHandle = GCHandle.Alloc(writer);

            *streamWrapper = @this;
        }

        /// <summary>
        /// Gets the captured <see cref="IBufferWriter{T}"/> instance.
        /// </summary>
        /// <returns>The captured <see cref="IBufferWriter{T}"/> instance</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IBufferWriter<byte> GetWriter()
        {
            return Unsafe.As<IBufferWriter<byte>>(this.writerHandle.Target!);
        }

#if NET6_0_OR_GREATER
        /// <inheritdoc/>
        public HRESULT QueryInterface(Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IBufferWriterWrapper*, Guid*, void**, HRESULT>)lpVtbl[0])((IBufferWriterWrapper*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        /// <inheritdoc/>
        public uint AddRef()
        {
            return ((delegate* unmanaged<IBufferWriterWrapper*, uint>)lpVtbl[1])((IBufferWriterWrapper*)Unsafe.AsPointer(ref this));
        }

        /// <inheritdoc/>
        public uint Release()
        {
            return ((delegate* unmanaged<IBufferWriterWrapper*, uint>)lpVtbl[2])((IBufferWriterWrapper*)Unsafe.AsPointer(ref this));
        }
#endif

        /// <inheritdoc cref="IStream.QueryInterface(Guid*, void**)"/>
        [UnmanagedCallersOnly]
        private static int QueryInterface(IBufferWriterWrapper* @this, Guid* riid, void** ppvObject)
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
        private static uint AddRef(IBufferWriterWrapper* @this)
        {
            return (uint)Interlocked.Increment(ref @this->referenceCount);
        }

        /// <inheritdoc cref="IStream.Release"/>
        [UnmanagedCallersOnly]
        private static uint Release(IBufferWriterWrapper* @this)
        {
            uint referenceCount = (uint)Interlocked.Decrement(ref @this->referenceCount);

            if (referenceCount == 0)
            {
                @this->writerHandle.Free();

                NativeMemory.Free(@this);
            }

            return referenceCount;
        }

        /// <inheritdoc cref="IStream.Read(void*, uint, uint*)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Read(IBufferWriterWrapper* @this, void* pv, uint cb, uint* pcbRead)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.Write(void*, uint, uint*)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Write(IBufferWriterWrapper* @this, void* pv, uint cb, uint* pcbWritten)
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
                @this->GetWriter().Write(new ReadOnlySpan<byte>(pv, (int)cb));

                if (pcbWritten != null)
                {
                    *pcbWritten = cb;
                }

                return S_OK;
            }
            catch (Exception ex)
            {
                return ex.HResult;
            }
        }

        /// <inheritdoc cref="IStream.Seek(LARGE_INTEGER, uint, ULARGE_INTEGER*)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Seek(IBufferWriterWrapper* @this, LARGE_INTEGER dlibMove, uint dwOrigin, ULARGE_INTEGER* plibNewPosition)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.SetSize(ULARGE_INTEGER)"/>
        [UnmanagedCallersOnly]
        private static HRESULT SetSize(IBufferWriterWrapper* @this, ULARGE_INTEGER libNewSize)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.CopyTo(IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*)"/>
        [UnmanagedCallersOnly]
        private static HRESULT CopyTo(IBufferWriterWrapper* @this, IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten)
        {
            return E_NOTIMPL;
        }

        /// <inheritdoc cref="IStream.Commit(uint)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Commit(IBufferWriterWrapper* @this, uint grfCommitFlags)
        {
            return S_OK;
        }

        /// <inheritdoc cref="IStream.Revert"/>
        [UnmanagedCallersOnly]
        private static HRESULT Revert(IBufferWriterWrapper* @this)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.LockRegion(ULARGE_INTEGER, ULARGE_INTEGER, uint)"/>
        [UnmanagedCallersOnly]
        private static HRESULT LockRegion(IBufferWriterWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.UnlockRegion(ULARGE_INTEGER, ULARGE_INTEGER, uint)"/>
        [UnmanagedCallersOnly]
        private static HRESULT UnlockRegion(IBufferWriterWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.Stat(STATSTG*, uint)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Stat(IBufferWriterWrapper* @this, STATSTG* pstatstg, uint grfStatFlag)
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
                pstatstg->grfMode = STGM_WRITE;
                pstatstg->type = (uint)STGTY_STREAM;
                pstatstg->cbSize.QuadPart = ulong.MaxValue;

                return S_OK;
            }
            catch (Exception ex)
            {
                return ex.HResult;
            }
        }

        /// <inheritdoc cref="IStream.Clone(IStream**)"/>
        [UnmanagedCallersOnly]
        private static HRESULT Clone(IBufferWriterWrapper* @this, IStream** ppstm)
        {
            return STG_E_INVALIDFUNCTION;
        }
    }
}