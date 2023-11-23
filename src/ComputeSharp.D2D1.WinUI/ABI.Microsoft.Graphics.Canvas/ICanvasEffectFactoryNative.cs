using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;
using WinRT;
using WinRT.Interop;
using IInspectable = ComputeSharp.Win32.IInspectable;

#pragma warning disable CS0649, IDE0055, IDE1006

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// The interop Win2D interface for factories of external effects.
/// </summary>
[NativeTypeName("class ICanvasEffectFactoryNative : IUnknown")]
[NativeInheritance("IUnknown")]
internal unsafe struct ICanvasEffectFactoryNative : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x1F, 0x1A, 0xBA, 0x29,
                0xFE, 0x1C,
                0xC3, 0x44,
                0x98, 0x4D,
                0x42,
                0x6D,
                0x61,
                0xB5,
                0x14,
                0x27
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    public void** lpVtbl;

    /// <inheritdoc cref="IUnknown.QueryInterface"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(0)]
    public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffectFactoryNative*, Guid*, void**, int>)this.lpVtbl[0])((ICanvasEffectFactoryNative*)Unsafe.AsPointer(ref this), riid, ppvObject);
    }

    /// <inheritdoc cref="IUnknown.AddRef"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(1)]
    [return: NativeTypeName("ULONG")]
    public uint AddRef()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffectFactoryNative*, uint>)this.lpVtbl[1])((ICanvasEffectFactoryNative*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IUnknown.Release"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(2)]
    [return: NativeTypeName("ULONG")]
    public uint Release()
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffectFactoryNative*, uint>)this.lpVtbl[2])((ICanvasEffectFactoryNative*)Unsafe.AsPointer(ref this));
    }

    /// <summary>
    /// Creates a new inspectable wrapper for an input D2D effect previosly registered through <see cref="ICanvasFactoryNative.RegisterEffectFactory"/>.
    /// </summary>
    /// <param name="device">The input canvas device.</param>
    /// <param name="resource">The input native effect to create a wrapper for.</param>
    /// <param name="dpi">The realization DPIs for <paramref name="resource"/>.</param>
    /// <param name="wrapper">The resulting wrapper for <paramref name="resource"/>.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    /// <remarks>
    /// All parameters are directly forwarded from the ones the caller passed to <see cref="ICanvasFactoryNative.GetOrCreate"/>. The returned
    /// wrapper should implement <see cref="global::Microsoft.Graphics.Canvas.ICanvasImage"/> to be returned correctly from Win2D after this call.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [VtblIndex(3)]
    public HRESULT CreateWrapper(ICanvasDevice* device, ID2D1Effect* resource, float dpi, IInspectable** wrapper)
    {
        return ((delegate* unmanaged[Stdcall]<ICanvasEffectFactoryNative*, ICanvasDevice*, ID2D1Effect*, float, IInspectable**, int>)this.lpVtbl[3])(
            (ICanvasEffectFactoryNative*)Unsafe.AsPointer(ref this),
            device,
            resource,
            dpi,
            wrapper);
    }

    /// <summary>
    /// The managed implementation of <see cref="ICanvasEffectFactoryNative"/>.
    /// </summary>
    [Guid("29BA1A1F-1CFE-44C3-984D-426D61B51427")]
    [WindowsRuntimeType]
    [WindowsRuntimeHelperType(typeof(Interface))]
    public interface Interface
    {
        /// <inheritdoc cref="ICanvasEffectFactoryNative.CreateWrapper"/>
        [return: NativeTypeName("HRESULT")]
        int CreateWrapper(ICanvasDevice* device, ID2D1Effect* resource, float dpi, IInspectable** wrapper);

        /// <summary>
        /// The vtable type for <see cref="Interface"/>.
        /// </summary>
        [Guid("29BA1A1F-1CFE-44C3-984D-426D61B51427")]
        public struct Vftbl
        {
            /// <summary>
            /// Allows CsWinRT to retrieve a pointer to the projection vtable (the name is hardcoded by convention).
            /// </summary>
            public static readonly IntPtr AbiToProjectionVftablePtr = InitVtbl();

            /// <summary>
            /// Builds the custom method table pointer for <see cref="Interface"/>.
            /// </summary>
            /// <returns>The method table pointer for <see cref="Interface"/>.</returns>
            private static IntPtr InitVtbl()
            {
                Vftbl* lpVtbl = (Vftbl*)ComWrappersSupport.AllocateVtableMemory(typeof(Vftbl), sizeof(Vftbl));

                lpVtbl->IUnknownVftbl = IUnknownVftbl.AbiToProjectionVftbl;
                lpVtbl->CreateWrapper = &CreateWrapperFromAbi;

                return (IntPtr)lpVtbl;
            }

            /// <summary>
            /// The IUnknown vtable.
            /// </summary>
            private IUnknownVftbl IUnknownVftbl;

            /// <summary>
            /// Function pointer for <see cref="CreateWrapperFromAbi"/>.
            /// </summary>
            private delegate* unmanaged[Stdcall]<IntPtr, ICanvasDevice*, ID2D1Effect*, float, IInspectable**, int> CreateWrapper;

            /// <inheritdoc cref="Interface.CreateWrapper"/>
            [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
            [return: NativeTypeName("HRESULT")]
            private static int CreateWrapperFromAbi(IntPtr thisPtr, ICanvasDevice* device, ID2D1Effect* resource, float dpi, IInspectable** wrapper)
            {
                try
                {
                    return ComWrappersSupport.FindObject<Interface>(thisPtr).CreateWrapper(device, resource, dpi, wrapper);
                }
                catch (Exception e)
                {
                    ExceptionHelpers.SetErrorInfo(e);

                    return Marshal.GetHRForException(e);
                }
            }
        }
    }
}