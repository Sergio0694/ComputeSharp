#if !SOURCE_GENERATOR
using System;
#endif
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop;
#if SOURCE_GENERATOR
using Windows.Win32.Graphics.Direct3D;
using static Windows.Win32.Foundation.HRESULT;
#else
using ComputeSharp.Win32;
using static ComputeSharp.Win32.S;
#endif

namespace ComputeSharp.D2D1.Shaders.Translation;

/// <inheritdoc/>
partial class D3DCompiler
{
    /// <summary>
    /// A custom <see cref="ID3DInclude"/> fallback implementation to use on systems with no support for it.
    /// </summary>
    private unsafe struct ID3DIncludeForD2DHelpers
    {
#if SOURCE_GENERATOR
        /// <inheritdoc cref="Open"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int OpenDelegate(ID3DIncludeForD2DHelpers* @this, D3D_INCLUDE_TYPE IncludeType, sbyte* pFileName, void* pParentData, void** ppData, uint* pBytes);

        /// <inheritdoc cref="Close"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int CloseDelegate(ID3DIncludeForD2DHelpers* @this, void* pData);

        /// <summary>
        /// A cached <see cref="OpenDelegate"/> instance wrapping <see cref="Open"/>.
        /// </summary>
        private static readonly OpenDelegate OpenWrapper = Open;

        /// <summary>
        /// A cached <see cref="CloseDelegate"/> instance wrapping <see cref="Close"/>.
        /// </summary>
        private static readonly CloseDelegate CloseWrapper = Close;
#endif

        /// <summary>
        /// The shared method table pointer for all <see cref="ID3DIncludeForD2DHelpers"/> instances.
        /// </summary>
        private static readonly void** Vtbl = InitVtbl();

        /// <summary>
        /// Builds the custom method table pointer for <see cref="ID3DIncludeForD2DHelpers"/>.
        /// </summary>
        /// <returns>The method table pointer for <see cref="ID3DIncludeForD2DHelpers"/>.</returns>
        private static void** InitVtbl()
        {
            void** lpVtbl = (void**)D2D1AssemblyAssociatedMemory.Allocate(sizeof(void*) * 2);

#if SOURCE_GENERATOR
            lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(OpenWrapper);
            lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(CloseWrapper);
#else
            lpVtbl[0] = (delegate* unmanaged<ID3DIncludeForD2DHelpers*, D3D_INCLUDE_TYPE, sbyte*, void*, void**, uint*, int>)&Open;
            lpVtbl[1] = (delegate* unmanaged<ID3DIncludeForD2DHelpers*, void*, int>)&Close;
#endif

            return lpVtbl;
        }

        /// <summary>
        /// The method table pointer for the current instance.
        /// </summary>
        private void** lpVtbl;

        /// <summary>
        /// Creates and initializes a new <see cref="ID3DIncludeForD2DHelpers"/> instance.
        /// </summary>
        /// <returns>A pointer to a new <see cref="ID3DIncludeForD2DHelpers"/> instance.</returns>
        public static ID3DInclude* Create()
        {
            ID3DIncludeForD2DHelpers* @this =
#if SOURCE_GENERATOR
                (ID3DIncludeForD2DHelpers*)Marshal.AllocHGlobal(sizeof(ID3DIncludeForD2DHelpers));
#else
                (ID3DIncludeForD2DHelpers*)NativeMemory.Alloc((nuint)sizeof(ID3DIncludeForD2DHelpers));
#endif

            @this->lpVtbl = Vtbl;

            return (ID3DInclude*)@this;
        }

        /// <inheritdoc cref="ID3DInclude.Open"/>
        [UnmanagedCallersOnly]
        public static int Open(ID3DIncludeForD2DHelpers* @this, D3D_INCLUDE_TYPE IncludeType, sbyte* pFileName, void* pParentData, void** ppData, uint* pBytes)
        {
#if SOURCE_GENERATOR
            if (new string(pFileName) == "d2d1effecthelpers.hlsli")
#else
            if (MemoryMarshal.CreateReadOnlySpanFromNullTerminated((byte*)pFileName).SequenceEqual("d2d1effecthelpers.hlsli"u8))
#endif
            {
                *ppData = (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(D2D1EffectHelpers.TextUtf8));
                *pBytes = (uint)D2D1EffectHelpers.TextUtf8.Length;

                return S_OK;
            }

            return S_FALSE;
        }

        /// <inheritdoc cref="ID3DInclude.Close"/>
        [UnmanagedCallersOnly]
        public static int Close(ID3DIncludeForD2DHelpers* @this, void* pData)
        {
            return S_OK;
        }
    }
}