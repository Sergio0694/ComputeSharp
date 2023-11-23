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
    {
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

        /// <inheritdoc/>
        private HRESULT QueryInterface(Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<IStreamWrapper*, Guid*, void**, HRESULT>)this.lpVtbl[0])((IStreamWrapper*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        /// <inheritdoc/>
        private uint AddRef()
        {
            return ((delegate* unmanaged<IStreamWrapper*, uint>)this.lpVtbl[1])((IStreamWrapper*)Unsafe.AsPointer(ref this));
        }

        /// <inheritdoc/>
        private uint Release()
        {
            return ((delegate* unmanaged<IStreamWrapper*, uint>)this.lpVtbl[2])((IStreamWrapper*)Unsafe.AsPointer(ref this));
        }

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