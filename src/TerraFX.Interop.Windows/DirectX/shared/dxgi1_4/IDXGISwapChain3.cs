// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/dxgi1_4.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [SupportedOSPlatform("windows10.0")]
    [Guid("94D99BDB-F1F8-4AB0-B236-7DA0170EDAB1")]
    [NativeTypeName("struct IDXGISwapChain3 : IDXGISwapChain2")]
    [NativeInheritance("IDXGISwapChain2")]
    internal unsafe partial struct IDXGISwapChain3
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint>)(lpVtbl[1]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint>)(lpVtbl[2]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(36)]
        public uint GetCurrentBackBufferIndex()
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain3*, uint>)(lpVtbl[36]))((IDXGISwapChain3*)Unsafe.AsPointer(ref this));
        }
    }
}
