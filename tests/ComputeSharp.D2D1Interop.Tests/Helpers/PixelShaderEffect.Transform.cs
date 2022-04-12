using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1Interop.Tests.Helpers;

/// <inheritdoc/>
partial struct PixelShaderEffect
{
    /// <summary>
    /// A simple <see cref="ID2D1DrawTransform"/> implementation.
    /// </summary>
    private unsafe struct PixelShaderTransform
    {
        /// <summary>
        /// The shared vtable pointer for <see cref="PixelShaderTransform"/> instances.
        /// </summary>
        private static readonly void** Vtbl = InitVtbl();

        /// <summary>
        /// Setups the vtable pointer for <see cref="PixelShaderTransform"/>.
        /// </summary>
        /// <returns>The initialized vtable pointer for <see cref="PixelShaderTransform"/>.</returns>
        private static void** InitVtbl()
        {
            void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(PixelShaderTransform), sizeof(void*) * 8);

            lpVtbl[0] = (delegate* unmanaged<PixelShaderTransform*, Guid*, void**, int>)&QueryInterface;
            lpVtbl[1] = (delegate* unmanaged<PixelShaderTransform*, uint>)&AddRef;
            lpVtbl[2] = (delegate* unmanaged<PixelShaderTransform*, uint>)&Release;
            lpVtbl[3] = (delegate* unmanaged<PixelShaderTransform*, uint>)&GetInputCount;
            lpVtbl[4] = (delegate* unmanaged<PixelShaderTransform*, RECT*, RECT*, uint, int>)&MapOutputRectToInputRects;
            lpVtbl[5] = (delegate* unmanaged<PixelShaderTransform*, RECT*, RECT*, uint, RECT*, RECT*, int>)&MapInputRectsToOutputRect;
            lpVtbl[6] = (delegate* unmanaged<PixelShaderTransform*, uint, RECT, RECT*, int>)&MapInvalidRect;
            lpVtbl[7] = (delegate* unmanaged<PixelShaderTransform*, ID2D1DrawInfo*, int>)&SetDrawInfo;

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
        /// The current input rectangle.
        /// </summary>
        private RECT inputRect;

        /// <summary>
        /// The <see cref="ID2D1DrawInfo"/> instance currently in use.
        /// </summary>
        private ID2D1DrawInfo* d2D1DrawInfo;

        /// <summary>
        /// Gets the <see cref="ID2D1DrawInfo"/> instance currently in use.
        /// </summary>
        public ID2D1DrawInfo* D2D1DrawInfo => this.d2D1DrawInfo;

        /// <summary>
        /// Creates and initializes a new <see cref="PixelShaderTransform"/> instance.
        /// </summary>
        /// <returns>A new <see cref="PixelShaderTransform"/> instance.</returns>
        public static PixelShaderTransform* Create()
        {
            PixelShaderTransform* @this = (PixelShaderTransform*)NativeMemory.Alloc((nuint)sizeof(PixelShaderTransform));

            @this->lpVtbl = Vtbl;
            @this->referenceCount = 1;
            @this->inputRect = default;
            @this->d2D1DrawInfo = null;

            return @this;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.QueryInterface"/>
        [UnmanagedCallersOnly]
        private static int QueryInterface(PixelShaderTransform* @this, Guid* riid, void** ppvObject)
        {
            if (ppvObject is null)
            {
                return E.E_POINTER;
            }

            if (riid->Equals(Windows.__uuidof<IUnknown>()) ||
                riid->Equals(Windows.__uuidof<ID2D1TransformNode>()) ||
                riid->Equals(Windows.__uuidof<ID2D1Transform>()) ||
                riid->Equals(Windows.__uuidof<ID2D1DrawTransform>()))
            {
                _ = Interlocked.Increment(ref @this->referenceCount);

                *ppvObject = @this;

                return S.S_OK;
            }

            *ppvObject = null;

            return E.E_NOINTERFACE;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.AddRef"/>
        [UnmanagedCallersOnly]
        private static uint AddRef(PixelShaderTransform* @this)
        {
            return (uint)Interlocked.Increment(ref @this->referenceCount);
        }

        /// <inheritdoc cref="ID2D1DrawTransform.Release"/>
        [UnmanagedCallersOnly]
        private static uint Release(PixelShaderTransform* @this)
        {
            uint referenceCount = (uint)Interlocked.Decrement(ref @this->referenceCount);

            if (referenceCount == 0)
            {
                NativeMemory.Free(@this);
            }

            return referenceCount;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.GetInputCount"/>
        [UnmanagedCallersOnly]
        private static uint GetInputCount(PixelShaderTransform* @this)
        {
            return 1;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapOutputRectToInputRects"/>
        [UnmanagedCallersOnly]
        private static int MapOutputRectToInputRects(PixelShaderTransform* @this, RECT* outputRect, RECT* inputRects, uint inputRectsCount)
        {
            if (inputRectsCount != 1)
            {
                return E.E_INVALIDARG;
            }

            inputRects[0] = *outputRect;

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInputRectsToOutputRect"/>
        [UnmanagedCallersOnly]
        private static int MapInputRectsToOutputRect(PixelShaderTransform* @this, RECT* inputRects, RECT* inputOpaqueSubRects, uint inputRectCount, RECT* outputRect, RECT* outputOpaqueSubRect)
        {
            if (inputRectCount != 1)
            {
                return E.E_INVALIDARG;
            }

            *outputRect = inputRects[0];

            @this->inputRect = inputRects[0];

            *outputOpaqueSubRect = default;

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.MapInvalidRect"/>
        [UnmanagedCallersOnly]
        private static int MapInvalidRect(PixelShaderTransform* @this, uint inputIndex, RECT invalidInputRect, RECT* invalidOutputRect)
        {
            *invalidOutputRect = @this->inputRect;

            return S.S_OK;
        }

        /// <inheritdoc cref="ID2D1DrawTransform.SetDrawInfo"/>
        [UnmanagedCallersOnly]
        private static int SetDrawInfo(PixelShaderTransform* @this, ID2D1DrawInfo* drawInfo)
        {
            @this->d2D1DrawInfo = drawInfo;

            return drawInfo->SetPixelShader((Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(typeof(InvertShader).GUID)));
        }
    }
}