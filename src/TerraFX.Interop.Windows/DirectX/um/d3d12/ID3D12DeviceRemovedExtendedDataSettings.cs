// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from d3d12.h in Microsoft.Direct3D.D3D12 v1.600.10
// Original source is Copyright © Microsoft. Licensed under the MIT license

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("82BC481C-6B9B-4030-AEDB-7EE3D1DF1E63")]
    [NativeTypeName("struct ID3D12DeviceRemovedExtendedDataSettings : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ID3D12DeviceRemovedExtendedDataSettings
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DeviceRemovedExtendedDataSettings*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12DeviceRemovedExtendedDataSettings*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DeviceRemovedExtendedDataSettings*, uint>)(lpVtbl[1]))((ID3D12DeviceRemovedExtendedDataSettings*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12DeviceRemovedExtendedDataSettings*, uint>)(lpVtbl[2]))((ID3D12DeviceRemovedExtendedDataSettings*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public void SetAutoBreadcrumbsEnablement(D3D12_DRED_ENABLEMENT Enablement)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12DeviceRemovedExtendedDataSettings*, D3D12_DRED_ENABLEMENT, void>)(lpVtbl[3]))((ID3D12DeviceRemovedExtendedDataSettings*)Unsafe.AsPointer(ref this), Enablement);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public void SetPageFaultEnablement(D3D12_DRED_ENABLEMENT Enablement)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12DeviceRemovedExtendedDataSettings*, D3D12_DRED_ENABLEMENT, void>)(lpVtbl[4]))((ID3D12DeviceRemovedExtendedDataSettings*)Unsafe.AsPointer(ref this), Enablement);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public void SetWatsonDumpEnablement(D3D12_DRED_ENABLEMENT Enablement)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12DeviceRemovedExtendedDataSettings*, D3D12_DRED_ENABLEMENT, void>)(lpVtbl[5]))((ID3D12DeviceRemovedExtendedDataSettings*)Unsafe.AsPointer(ref this), Enablement);
        }
    }
}