using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RuntimeHelpers = ComputeSharp.D2D1.NetStandard.System.Runtime.CompilerServices.RuntimeHelpers;
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <summary>
/// An implementation of the <c>ID2D1ResourceTextureManager</c> and <c>ID2D1ResourceTextureManagerInternal</c> interfaces.
/// </summary>
internal unsafe struct D2D1ResourceTextureManager
{
    /// <summary>
    /// Gets the <see cref="Guid"/> for <c>ID2D1ResourceTextureManager</c> (<c>3C4FC7E4-A419-46CA-B5F6-66EB4FF18D64</c>).
    /// </summary>
    private static ref readonly Guid IID_ID2D1ResourceTextureManager
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0x3C, 0x4F, 0xC7, 0xE4, 0xA4,
                0x19, 0x46,
                0xCA, 0xB5,
                0xF6, 0x66,
                0xEB,
                0x4F,
                0xF1,
                0x8D,
                0x64
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// Gets the <see cref="Guid"/> for <c>ID2D1ResourceTextureManagerInternal</c> (<c>5CBB1024-8EA1-4689-81BF-8AD190B5EF5D</c>).
    /// </summary>
    private static ref readonly Guid IID_ID2D1ResourceTextureManagerInternal
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0x5C, 0xBB, 0x10, 0x24,
                0x8E, 0xA1,
                0x46, 0x89,
                0x81, 0xBF,
                0x8A,
                0xD1,
                0x90,
                0xB5,
                0xEF,
                0x5D
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <inheritdoc cref="QueryInterface"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int QueryInterfaceDelegate(D2D1ResourceTextureManager* @this, Guid* riid, void** ppvObject);

    /// <inheritdoc cref="AddRef"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint AddRefDelegate(D2D1ResourceTextureManager* @this);

    /// <inheritdoc cref="Release"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate uint ReleaseDelegate(D2D1ResourceTextureManager* @this);

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1ResourceTextureManager"/> instance, for <c>ID2D1ResourceTextureManager</c>.
    /// </summary>
    private static readonly void** VtblForID2D1EffectImpl;

    /// <summary>
    /// The shared vtable pointer for <see cref="D2D1ResourceTextureManager"/> instance, for <c>ID2D1ResourceTextureManagerInternal</c>.
    /// </summary>
    private static readonly void** VtblForID2D1DrawTransform;

    /// <summary>
    /// Initializes the shared state for <see cref="D2D1ResourceTextureManager"/>.
    /// </summary>
    static D2D1ResourceTextureManager()
    {
        void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(D2D1ResourceTextureManager), sizeof(void*) * 6);

        // ID2D1ResourceTextureManager
#if NET6_0_OR_GREATER
        lpVtbl[0] = (delegate* unmanaged<D2D1ResourceTextureManager*, Guid*, void**, int>)&ID2D1ResourceTextureManagerMethods.QueryInterface;
        lpVtbl[1] = (delegate* unmanaged<D2D1ResourceTextureManager*, uint>)&ID2D1ResourceTextureManagerMethods.AddRef;
        lpVtbl[2] = (delegate* unmanaged<D2D1ResourceTextureManager*, uint>)&ID2D1ResourceTextureManagerMethods.Release;
#else
        lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerMethods.QueryInterfaceWrapper);
        lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerMethods.AddRefWrapper);
        lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerMethods.ReleaseWrapper);
#endif

        // ID2D1ResourceTextureManagerInternal
#if NET6_0_OR_GREATER
        lpVtbl[3 + 0] = (delegate* unmanaged<D2D1ResourceTextureManager*, Guid*, void**, int>)&ID2D1ResourceTextureManagerInternalMethods.QueryInterface;
        lpVtbl[3 + 1] = (delegate* unmanaged<D2D1ResourceTextureManager*, uint>)&ID2D1ResourceTextureManagerInternalMethods.AddRef;
        lpVtbl[3 + 2] = (delegate* unmanaged<D2D1ResourceTextureManager*, uint>)&ID2D1ResourceTextureManagerInternalMethods.Release;
#else
        lpVtbl[3 + 0] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerInternalMethods.QueryInterfaceWrapper);
        lpVtbl[3 + 1] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerInternalMethods.AddRefWrapper);
        lpVtbl[3 + 2] = (void*)Marshal.GetFunctionPointerForDelegate(ID2D1ResourceTextureManagerInternalMethods.ReleaseWrapper);
#endif

        VtblForID2D1EffectImpl = lpVtbl;
        VtblForID2D1DrawTransform = &lpVtbl[6];
    }

    /// <summary>
    /// The vtable pointer for the current instance, for <c>ID2D1ResourceTextureManager</c>.
    /// </summary>
    private void** lpVtblForID2D1ResourceTextureManager;

    /// <summary>
    /// The vtable pointer for the current instance, for <c>ID2D1ResourceTextureManagerInternal</c>.
    /// </summary>
    private void** lpVtblForID2D1ResourceTextureManagerInternal;

    /// <summary>
    /// The current reference count for the object (from <c>IUnknown</c>).
    /// </summary>
    private volatile int referenceCount;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    private int QueryInterface(Guid* riid, void** ppvObject)
    {
        if (ppvObject is null)
        {
            return E.E_POINTER;
        }

        // ID2D1ResourceTextureManager
        if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
            riid->Equals(IID_ID2D1ResourceTextureManager))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = Unsafe.AsPointer(ref this);

            return S.S_OK;
        }

        // ID2D1ResourceTextureManagerInternal
        if (riid->Equals(IID_ID2D1ResourceTextureManagerInternal))
        {
            _ = Interlocked.Increment(ref this.referenceCount);

            *ppvObject = (void**)Unsafe.AsPointer(ref this) + 1;

            return S.S_OK;
        }

        *ppvObject = null;

        return E.E_NOINTERFACE;
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    private uint AddRef()
    {
        return (uint)Interlocked.Increment(ref this.referenceCount);
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    private uint Release()
    {
        uint referenceCount = (uint)Interlocked.Decrement(ref this.referenceCount);

        if (referenceCount == 0)
        {
            // TODO: release resources
        }

        return referenceCount;
    }

    /// <summary>
    /// The implementation for <c>ID2D1ResourceTextureManager</c>.
    /// </summary>
    private static class ID2D1ResourceTextureManagerMethods
    {
#if !NET6_0_OR_GREATER
        /// <summary>
        /// A cached <see cref="QueryInterfaceDelegate"/> instance wrapping <see cref="QueryInterface"/>.
        /// </summary>
        public static readonly QueryInterfaceDelegate QueryInterfaceWrapper = QueryInterface;

        /// <summary>
        /// A cached <see cref="AddRefDelegate"/> instance wrapping <see cref="AddRef"/>.
        /// </summary>
        public static readonly AddRefDelegate AddRefWrapper = AddRef;

        /// <summary>
        /// A cached <see cref="ReleaseDelegate"/> instance wrapping <see cref="Release"/>.
        /// </summary>
        public static readonly ReleaseDelegate ReleaseWrapper = Release;
#endif

        /// <inheritdoc cref="D2D1ResourceTextureManager.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(D2D1ResourceTextureManager* @this, Guid* riid, void** ppvObject)
        {
            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="D2D1ResourceTextureManager.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(D2D1ResourceTextureManager* @this)
        {
            return @this->AddRef();
        }

        /// <inheritdoc cref="D2D1ResourceTextureManager.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(D2D1ResourceTextureManager* @this)
        {
            return @this->Release();
        }
    }

    /// <summary>
    /// The implementation for <c>ID2D1ResourceTextureManagerInternal</c>.
    /// </summary>
    private unsafe static class ID2D1ResourceTextureManagerInternalMethods
    {
#if !NET6_0_OR_GREATER
        /// <summary>
        /// A cached <see cref="QueryInterfaceDelegate"/> instance wrapping <see cref="QueryInterface"/>.
        /// </summary>
        public static readonly QueryInterfaceDelegate QueryInterfaceWrapper = QueryInterface;

        /// <summary>
        /// A cached <see cref="AddRefDelegate"/> instance wrapping <see cref="AddRef"/>.
        /// </summary>
        public static readonly AddRefDelegate AddRefWrapper = AddRef;

        /// <summary>
        /// A cached <see cref="ReleaseDelegate"/> instance wrapping <see cref="Release"/>.
        /// </summary>
        public static readonly ReleaseDelegate ReleaseWrapper = Release;
#endif

        /// <inheritdoc cref="D2D1ResourceTextureManager.QueryInterface"/>
        [UnmanagedCallersOnly]
        public static int QueryInterface(D2D1ResourceTextureManager* @this, Guid* riid, void** ppvObject)
        {
            @this = (D2D1ResourceTextureManager*)&((void**)@this)[-1];

            return @this->QueryInterface(riid, ppvObject);
        }

        /// <inheritdoc cref="D2D1ResourceTextureManager.AddRef"/>
        [UnmanagedCallersOnly]
        public static uint AddRef(D2D1ResourceTextureManager* @this)
        {
            @this = (D2D1ResourceTextureManager*)&((void**)@this)[-1];

            return @this->AddRef();
        }

        /// <inheritdoc cref="D2D1ResourceTextureManager.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(D2D1ResourceTextureManager* @this)
        {
            @this = (D2D1ResourceTextureManager*)&((void**)@this)[-1];

            return @this->Release();
        }
    }
}
