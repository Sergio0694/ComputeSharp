using System;
using System.Buffers;
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
    private unsafe partial struct IBufferWriterWrapper //: IUnknown.Interface
    {
        ///// <inheritdoc/>
        //static Guid* INativeGuid.NativeGuid => (Guid*)default(NotSupportedException).Throw<nint>();

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
        /// The <see cref="GCHandle"/> to the captured <see cref="IBufferWriter{T}"/>.
        /// </summary>
        private GCHandle writerHandle;

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

        /// <inheritdoc/>
        private HRESULT QueryInterface(Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IBufferWriterWrapper*, Guid*, void**, HRESULT>)this.lpVtbl[0])((IBufferWriterWrapper*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        /// <inheritdoc/>
        private uint AddRef()
        {
            return ((delegate* unmanaged<IBufferWriterWrapper*, uint>)this.lpVtbl[1])((IBufferWriterWrapper*)Unsafe.AsPointer(ref this));
        }

        /// <inheritdoc/>
        private uint Release()
        {
            return ((delegate* unmanaged<IBufferWriterWrapper*, uint>)this.lpVtbl[2])((IBufferWriterWrapper*)Unsafe.AsPointer(ref this));
        }

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
                return Marshal.GetHRForException(ex);
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
                return Marshal.GetHRForException(ex);
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