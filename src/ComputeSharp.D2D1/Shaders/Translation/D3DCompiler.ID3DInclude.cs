using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    internal unsafe struct ID3DIncludeForD2DHelpers
    {
        /// <summary>
        /// The shared method table for all <see cref="ID3DIncludeForD2DHelpers"/> instances.
        /// </summary>
        [FixedAddressValueType]
        private static readonly ID3DIncludeForD2DHelpersVftbl Vftbl;

        /// <summary>
        /// Initializes <see cref="Vftbl"/>.
        /// </summary>
        static ID3DIncludeForD2DHelpers()
        {
            Vftbl.Open = &Open;
            Vftbl.Close = &Close;
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
            ID3DIncludeForD2DHelpers* @this = (ID3DIncludeForD2DHelpers*)NativeMemory.Alloc((nuint)sizeof(ID3DIncludeForD2DHelpers));

            @this->lpVtbl = (void**)Unsafe.AsPointer(in Vftbl);

            return (ID3DInclude*)@this;
        }

        /// <inheritdoc cref="ID3DInclude.Open"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int Open(ID3DIncludeForD2DHelpers* @this, D3D_INCLUDE_TYPE IncludeType, sbyte* pFileName, void* pParentData, void** ppData, uint* pBytes)
        {
            if (MemoryMarshal.CreateReadOnlySpanFromNullTerminated((byte*)pFileName).SequenceEqual("d2d1effecthelpers.hlsli"u8))
            {
                *ppData = (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(D2D1EffectHelpers.TextUtf8));
                *pBytes = (uint)D2D1EffectHelpers.TextUtf8.Length;

                return S_OK;
            }

            return S_FALSE;
        }

        /// <inheritdoc cref="ID3DInclude.Close"/>
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvMemberFunction)])]
        public static int Close(ID3DIncludeForD2DHelpers* @this, void* pData)
        {
            return S_OK;
        }
    }
}