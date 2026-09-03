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
    internal unsafe partial struct IBufferWriterWrapper
    {
        /// <summary>
        /// The shared vtable for <see cref="IBufferWriterWrapper"/> instances.
        /// </summary>
        [FixedAddressValueType]
        private static readonly IBufferWriterWrapperVftbl SharedVftbl;

        /// <summary>
        /// Initializes <see cref="SharedVftbl"/>.
        /// </summary>
        static IBufferWriterWrapper()
        {
            SharedVftbl.QueryInterface = &QueryInterface;
            SharedVftbl.AddRef = &AddRef;
            SharedVftbl.Release = &Release;
            SharedVftbl.Read = &Read;
            SharedVftbl.Write = &Write;
            SharedVftbl.Seek = &Seek;
            SharedVftbl.SetSize = &SetSize;
            SharedVftbl.CopyTo = &CopyTo;
            SharedVftbl.Commit = &Commit;
            SharedVftbl.Revert = &Revert;
            SharedVftbl.LockRegion = &LockRegion;
            SharedVftbl.UnlockRegion = &UnlockRegion;
            SharedVftbl.Stat = &Stat;
            SharedVftbl.Clone = &Clone;
        }

        /// <summary>
        /// Gets the shared vtable pointer for <see cref="IBufferWriterWrapper"/> instances.
        /// </summary>
        private static void** Vtbl
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (void**)Unsafe.AsPointer(in SharedVftbl);
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
        private readonly IBufferWriter<byte> GetWriter()
        {
            return Unsafe.As<IBufferWriter<byte>>(this.writerHandle.Target!);
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