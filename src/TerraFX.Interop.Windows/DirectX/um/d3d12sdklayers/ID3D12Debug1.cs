// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12sdklayers.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("AFFAA4CA-63FE-4D8E-B8AD-159000AF4304")]
    [NativeTypeName("struct ID3D12Debug1 : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ID3D12Debug1 : ID3D12Debug1.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12Debug1*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, uint>)(lpVtbl[1]))((ID3D12Debug1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, uint>)(lpVtbl[2]))((ID3D12Debug1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public void EnableDebugLayer()
        {
            ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, void>)(lpVtbl[3]))((ID3D12Debug1*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public void SetEnableGPUBasedValidation(BOOL Enable)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, BOOL, void>)(lpVtbl[4]))((ID3D12Debug1*)Unsafe.AsPointer(ref this), Enable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public void SetEnableSynchronizedCommandQueueValidation(BOOL Enable)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12Debug1*, BOOL, void>)(lpVtbl[5]))((ID3D12Debug1*)Unsafe.AsPointer(ref this), Enable);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            void EnableDebugLayer();

            [VtblIndex(4)]
            void SetEnableGPUBasedValidation(BOOL Enable);

            [VtblIndex(5)]
            void SetEnableSynchronizedCommandQueueValidation(BOOL Enable);
        }
    }
}
