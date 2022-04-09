using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RuntimeHelpers = ComputeSharp.D2D1Interop.NetStandard.System.Runtime.CompilerServices.RuntimeHelpers;
using UnmanagedCallersOnlyAttribute = ComputeSharp.NetStandard.System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute;
#endif

namespace ComputeSharp.D2D1Interop.Shaders.Translation;

/// <inheritdoc/>
partial class D2D1ShaderCompiler
{
    /// <summary>
    /// A custom <see cref="ID3DInclude"/> fallback implementation to use on systems with no support for it.
    /// </summary>
    private unsafe struct ID3DIncludeForD2DHelpers
    {
#if !NET6_0_OR_GREATER
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
        /// A pointer to a buffer with the contents of <c>d2d1effecthelpers.hlsli</c>.
        /// </summary>
        private static void* d2D1EffectHelpersData;

        /// <summary>
        /// The size of the buffer in <see cref="d2D1EffectHelpersData"/>.
        /// </summary>
        private static int d2D1EffectHelpersSize;

        /// <summary>
        /// Builds the custom method table pointer for <see cref="ID3DIncludeForD2DHelpers"/>.
        /// </summary>
        /// <returns>The method table pointer for <see cref="ID3DIncludeForD2DHelpers"/>.</returns>
        private static void** InitVtbl()
        {
            void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(ID3DIncludeForD2DHelpers), sizeof(void*) * 2);

#if NET6_0_OR_GREATER
            lpVtbl[0] = (delegate* unmanaged<ID3DIncludeForD2DHelpers*, D3D_INCLUDE_TYPE, sbyte*, void*, void**, uint*, int>)&Open;
            lpVtbl[1] = (delegate* unmanaged<ID3DIncludeForD2DHelpers*, void*, int>)&Close;
#else
            lpVtbl[0] = (void*)Marshal.GetFunctionPointerForDelegate(OpenWrapper);
            lpVtbl[1] = (void*)Marshal.GetFunctionPointerForDelegate(CloseWrapper);
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
        public static ID3DIncludeForD2DHelpers* Create()
        {
            ID3DIncludeForD2DHelpers* @this = (ID3DIncludeForD2DHelpers*)NativeMemory.Alloc((nuint)sizeof(ID3DIncludeForD2DHelpers));

            @this->lpVtbl = Vtbl;

            return @this;
        }

        /// <inheritdoc cref="ID3DInclude.Open"/>
        [UnmanagedCallersOnly]
        public static int Open(ID3DIncludeForD2DHelpers* @this, D3D_INCLUDE_TYPE IncludeType, sbyte* pFileName, void* pParentData, void** ppData, uint* pBytes)
        {
            if (new string(pFileName) == "d2d1effecthelpers.hlsli")
            {
                // Load the header if needed
                if (d2D1EffectHelpersData is null)
                {
                    d2D1EffectHelpersData = LoadD2D1EffectHelpersHeader(out d2D1EffectHelpersSize);
                }

                *ppData = d2D1EffectHelpersData;
                *pBytes = (uint)d2D1EffectHelpersSize;

                return S.S_OK;
            }

            return S.S_FALSE;
        }

        /// <inheritdoc cref="ID3DInclude.Close"/>
        [UnmanagedCallersOnly]
        public static int Close(ID3DIncludeForD2DHelpers* @this, void* pData)
        {
            return S.S_OK;
        }

        /// <summary>
        /// Loads the <c>d2d1effecthelpers.hlsli</c> header into a persistent buffer.
        /// </summary>
        /// <param name="size">The size of the header.</param>
        /// <returns>A pointer to a buffer with the contents of <c>d2d1effecthelpers.hlsli</c>.</returns>
        private static void* LoadD2D1EffectHelpersHeader(out int size)
        {
#if SOURCE_GENERATOR
            const string headerFilename = "ComputeSharp.D2D1Interop.SourceGenerators.ComputeSharp.D2D1Interop.Shaders.Translation.Headers.d2d1effecthelpers.hlsli";
#else
            const string headerFilename = "ComputeSharp.D2D1Interop.Shaders.Translation.Headers.d2d1effecthelpers.hlsli";
#endif

            using Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream(headerFilename)!;
            using MemoryStream destination = new();

            source.CopyTo(destination);

            byte[] buffer = destination.GetBuffer();

            size = (int)destination.Position;

            void* pointer = NativeMemory.Alloc((nuint)size);

            buffer.AsSpan(0, size).CopyTo(new Span<byte>(pointer, size));

            return pointer;
        }
    }
}
