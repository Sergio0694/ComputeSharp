using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;
using WinRT;
using WinRT.Interop;

#pragma warning disable CS0649, IDE0055, IDE1006

namespace ABI.Microsoft.Graphics.Canvas
{
    /// <summary>
    /// An interop Win2D interface for all images and effects that can be drawn.
    /// </summary>
    [NativeTypeName("class ICanvasImageInterop : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe struct ICanvasImageInterop : IComObject
    {
        /// <inheritdoc/>
        public static Guid* IID
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ReadOnlySpan<byte> data =
                [
                    0xF7, 0xD1, 0x42, 0xE0,
                    0xAD, 0xF9,
                    0x79, 0x44,
                    0xA7, 0x13,
                    0x67,
                    0x62,
                    0x7E,
                    0xA3,
                    0x18,
                    0x63
                ];

                return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
            }
        }

        public void** lpVtbl;

        /// <summary>
        /// Gets the device that the effect is currently realized on, if any.
        /// </summary>
        /// <param name="device">The resulting device, if available (as a marshalled <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>).</param>
        /// <param name="type">The <see cref="WIN2D_GET_DEVICE_ASSOCIATION_TYPE"/> value describing the returned instance.</param>
        /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetDevice(ICanvasDevice** device, WIN2D_GET_DEVICE_ASSOCIATION_TYPE* type)
        {
            return ((delegate* unmanaged[MemberFunction]<ICanvasImageInterop*, ICanvasDevice**, WIN2D_GET_DEVICE_ASSOCIATION_TYPE*, int>)this.lpVtbl[3])((ICanvasImageInterop*)Unsafe.AsPointer(ref this), device, type);
        }

        /// <summary>
        /// Gets an <see cref="ID2D1Image"/> from an <see cref="ICanvasImageInterop"/> instance.
        /// </summary>
        /// <param name="device">The input canvas device (as a marshalled <see cref="global::Microsoft.Graphics.Canvas.CanvasDevice"/>).</param>
        /// <param name="deviceContext">
        /// The device context in use. This value is is optional (but recommended), except when the
        /// <see cref="WIN2D_GET_D2D_IMAGE_FLAGS.WIN2D_GET_D2D_IMAGE_FLAGS_READ_DPI_FROM_DEVICE_CONTEXT"/> flag is specified. This is because
        /// not all callers of <see cref="GetD2DImage"/> have easy access to a context. It is always
        /// possible to get a resource creation context from the device, but the context is only
        /// actually necessary if a new effect realization needs to be created, so it is more efficient
        /// to have the implementation do this lookup only if/when it turns out to be needed.
        /// </param>
        /// <param name="flags">The flags to use to get the image.</param>
        /// <param name="targetDpi">
        /// The DPI of the target device context. This is used to determine when a <c>D2D1DpiCompensation</c>
        /// effect needs to be inserted. Behavior of this parameter can be overridden by the flag values
        /// <see cref="WIN2D_GET_D2D_IMAGE_FLAGS.WIN2D_GET_D2D_IMAGE_FLAGS_READ_DPI_FROM_DEVICE_CONTEXT"/>,
        /// <see cref="WIN2D_GET_D2D_IMAGE_FLAGS.WIN2D_GET_D2D_IMAGE_FLAGS_ALWAYS_INSERT_DPI_COMPENSATION"/>
        /// or <see cref="WIN2D_GET_D2D_IMAGE_FLAGS.WIN2D_GET_D2D_IMAGE_FLAGS_NEVER_INSERT_DPI_COMPENSATION"/>
        /// </param>
        /// <param name="realizeDpi">
        /// The DPI of a source bitmap, or zero if the image does not have a fixed DPI. A <c>D2D1DpiCompensation</c> effect
        /// will be inserted if <paramref name="targetDpi"/> and <paramref name="realizeDpi"/> are different (flags permitting).
        /// </param>
        /// <param name="ppImage">The resulting <see cref="ID2D1Image"/> for the effect.</param>
        /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetD2DImage(
            ICanvasDevice* device,
            ID2D1DeviceContext* deviceContext,
            WIN2D_GET_D2D_IMAGE_FLAGS flags,
            float targetDpi,
            float* realizeDpi,
            ID2D1Image** ppImage)
        {
            return ((delegate* unmanaged[MemberFunction]<ICanvasImageInterop*, ICanvasDevice*, ID2D1DeviceContext*, WIN2D_GET_D2D_IMAGE_FLAGS, float, float*, ID2D1Image**, int>)this.lpVtbl[4])(
                (ICanvasImageInterop*)Unsafe.AsPointer(ref this),
                device,
                deviceContext,
                flags,
                targetDpi,
                realizeDpi,
                ppImage);
        }
    }

    /// <summary>
    /// The ABI methods for <see cref="ICanvasImageInterop"/>.
    /// </summary>
    /// <remarks>
    /// This type has public accessibility to allow the AOT generator in CsWinRT to reference
    /// the <see cref="IID"/> and <see cref="AbiToProjectionVftablePtr"/> properties to make
    /// marshalling of CCW types AOT-safe. It is not meant to be used directly by developers.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static unsafe class ICanvasImageInteropMethods
    {
        /// <inheritdoc cref="ICanvasImageInterop.IID"/>
        public static Guid IID => *ICanvasImageInterop.IID;

        /// <inheritdoc cref="global::Microsoft.Graphics.Canvas.ICanvasImageInterop.Vftbl.AbiToProjectionVftablePtr"/>
        public static IntPtr AbiToProjectionVftablePtr => global::Microsoft.Graphics.Canvas.ICanvasImageInterop.Vftbl.AbiToProjectionVftablePtr;
    }
}

namespace Microsoft.Graphics.Canvas
{
    using ABI.Microsoft.Graphics.Canvas;

    /// <summary>
    /// The managed implementation of <see cref="ABI.Microsoft.Graphics.Canvas.ICanvasImageInterop"/>.
    /// </summary>
    [Guid("E042D1F7-F9AD-4479-A713-67627EA31863")]
    [WindowsRuntimeType]
    [WindowsRuntimeHelperType(typeof(ICanvasImageInterop))]
    internal unsafe interface ICanvasImageInterop
    {
        /// <inheritdoc cref="ABI.Microsoft.Graphics.Canvas.ICanvasImageInterop.GetDevice"/>
        [return: NativeTypeName("HRESULT")]
        int GetDevice(ICanvasDevice** device, WIN2D_GET_DEVICE_ASSOCIATION_TYPE* type);

        /// <inheritdoc cref="ABI.Microsoft.Graphics.Canvas.ICanvasImageInterop.GetD2DImage"/>
        [return: NativeTypeName("HRESULT")]
        int GetD2DImage(
            ICanvasDevice* device,
            ID2D1DeviceContext* deviceContext,
            WIN2D_GET_D2D_IMAGE_FLAGS flags,
            float targetDpi,
            float* realizeDpi,
            ID2D1Image** ppImage);

        /// <summary>
        /// The vtable type for <see cref="ICanvasImageInterop"/>.
        /// </summary>
        public struct Vftbl
        {
            /// <summary>
            /// Allows CsWinRT to retrieve a pointer to the projection vtable (the name is hardcoded by convention).
            /// </summary>
            public static readonly IntPtr AbiToProjectionVftablePtr = InitVtbl();

            /// <summary>
            /// Builds the custom method table pointer for <see cref="ICanvasImageInterop"/>.
            /// </summary>
            /// <returns>The method table pointer for <see cref="ICanvasImageInterop"/>.</returns>
            private static IntPtr InitVtbl()
            {
                Vftbl* lpVtbl = (Vftbl*)ComWrappersSupport.AllocateVtableMemory(typeof(Vftbl), sizeof(Vftbl));

                lpVtbl->IUnknownVftbl = IUnknownVftbl.AbiToProjectionVftbl;
                lpVtbl->GetDevice = &GetDeviceFromAbi;
                lpVtbl->GetD2DImage = &GetD2DImageFromAbi;

                return (IntPtr)lpVtbl;
            }

            /// <summary>
            /// The IUnknown vtable.
            /// </summary>
            private IUnknownVftbl IUnknownVftbl;

            /// <summary>
            /// Function pointer for <see cref="GetDeviceFromAbi"/>.
            /// </summary>
            private delegate* unmanaged[MemberFunction]<IntPtr, ICanvasDevice**, WIN2D_GET_DEVICE_ASSOCIATION_TYPE*, int> GetDevice;

            /// <summary>
            /// Function pointer for <see cref="GetD2DImageFromAbi"/>.
            /// </summary>
            private delegate* unmanaged[MemberFunction]<IntPtr, ICanvasDevice*, ID2D1DeviceContext*, WIN2D_GET_D2D_IMAGE_FLAGS, float, float*, ID2D1Image**, int> GetD2DImage;

            /// <inheritdoc cref="ICanvasImageInterop.GetDevice"/>
            [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
            [return: NativeTypeName("HRESULT")]
            private static int GetDeviceFromAbi(IntPtr thisPtr, ICanvasDevice** device, WIN2D_GET_DEVICE_ASSOCIATION_TYPE* type)
            {
                try
                {
                    return ComWrappersSupport.FindObject<ICanvasImageInterop>(thisPtr).GetDevice(device, type);
                }
                catch (Exception e)
                {
                    ExceptionHelpers.SetErrorInfo(e);

                    return ExceptionHelpers.GetHRForException(e);
                }
            }

            /// <inheritdoc cref="ICanvasImageInterop.GetDevice"/>
            [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
            [return: NativeTypeName("HRESULT")]
            private static int GetD2DImageFromAbi(
                IntPtr thisPtr,
                ICanvasDevice* device,
                ID2D1DeviceContext* deviceContext,
                WIN2D_GET_D2D_IMAGE_FLAGS flags,
                float targetDpi,
                float* realizeDpi,
                ID2D1Image** ppImage)
            {
                try
                {
                    return ComWrappersSupport.FindObject<ICanvasImageInterop>(thisPtr).GetD2DImage(device, deviceContext, flags, targetDpi, realizeDpi, ppImage);
                }
                catch (Exception e)
                {
                    ExceptionHelpers.SetErrorInfo(e);

                    return ExceptionHelpers.GetHRForException(e);
                }
            }
        }
    }
}