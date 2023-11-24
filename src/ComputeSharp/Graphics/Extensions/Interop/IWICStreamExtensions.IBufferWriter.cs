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
    private unsafe partial struct IBufferWriterWrapper
    {
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

            lpVtbl[0] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, Guid*, void**, int>)&QueryInterface;
            lpVtbl[1] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, uint>)&AddRef;
            lpVtbl[2] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, uint>)&Release;
            lpVtbl[3] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, void*, uint, uint*, int>)&Read;
            lpVtbl[4] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, void*, uint, uint*, int>)&Write;
            lpVtbl[5] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, LARGE_INTEGER, uint, ULARGE_INTEGER*, int>)&Seek;
            lpVtbl[6] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, ULARGE_INTEGER, int>)&SetSize;
            lpVtbl[7] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*, int>)&CopyTo;
            lpVtbl[8] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, uint, int>)&Commit;
            lpVtbl[9] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, int>)&Revert;
            lpVtbl[10] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int>)&LockRegion;
            lpVtbl[11] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, ULARGE_INTEGER, ULARGE_INTEGER, uint, int>)&UnlockRegion;
            lpVtbl[12] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, STATSTG*, uint, int>)&Stat;
            lpVtbl[13] = (delegate* unmanaged[MemberFunction]<IBufferWriterWrapper*, IStream**, int>)&Clone;

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
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int QueryInterface(IBufferWriterWrapper* @this, Guid* riid, void** ppvObject)
        {
            if (ppvObject is null)
            {
                return E_POINTER;
            }

            if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
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
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static uint AddRef(IBufferWriterWrapper* @this)
        {
            return (uint)Interlocked.Increment(ref @this->referenceCount);
        }

        /// <inheritdoc cref="IStream.Release"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
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
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int Read(IBufferWriterWrapper* @this, void* pv, uint cb, uint* pcbRead)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.Write(void*, uint, uint*)"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int Write(IBufferWriterWrapper* @this, void* pv, uint cb, uint* pcbWritten)
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
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int Seek(IBufferWriterWrapper* @this, LARGE_INTEGER dlibMove, uint dwOrigin, ULARGE_INTEGER* plibNewPosition)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.SetSize(ULARGE_INTEGER)"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int SetSize(IBufferWriterWrapper* @this, ULARGE_INTEGER libNewSize)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.CopyTo(IStream*, ULARGE_INTEGER, ULARGE_INTEGER*, ULARGE_INTEGER*)"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int CopyTo(IBufferWriterWrapper* @this, IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten)
        {
            return E_NOTIMPL;
        }

        /// <inheritdoc cref="IStream.Commit(uint)"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int Commit(IBufferWriterWrapper* @this, uint grfCommitFlags)
        {
            return S_OK;
        }

        /// <inheritdoc cref="IStream.Revert"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int Revert(IBufferWriterWrapper* @this)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.LockRegion(ULARGE_INTEGER, ULARGE_INTEGER, uint)"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int LockRegion(IBufferWriterWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.UnlockRegion(ULARGE_INTEGER, ULARGE_INTEGER, uint)"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int UnlockRegion(IBufferWriterWrapper* @this, ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, uint dwLockType)
        {
            return STG_E_INVALIDFUNCTION;
        }

        /// <inheritdoc cref="IStream.Stat(STATSTG*, uint)"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int Stat(IBufferWriterWrapper* @this, STATSTG* pstatstg, uint grfStatFlag)
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
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        private static int Clone(IBufferWriterWrapper* @this, IStream** ppstm)
        {
            return STG_E_INVALIDFUNCTION;
        }
    }
}