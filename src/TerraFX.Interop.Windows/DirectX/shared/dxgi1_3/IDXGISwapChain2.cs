// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/dxgi1_3.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [SupportedOSPlatform("windows8.1")]
    [Guid("A8BE2AC4-199F-4946-B331-79599FB98DE7")]
    [NativeTypeName("struct IDXGISwapChain2 : IDXGISwapChain1")]
    [NativeInheritance("IDXGISwapChain1")]
    internal unsafe partial struct IDXGISwapChain2
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, Guid*, void**, int>)(lpVtbl[0]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint>)(lpVtbl[1]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint>)(lpVtbl[2]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT Present(uint SyncInterval, uint Flags)
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, uint, int>)(lpVtbl[8]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), SyncInterval, Flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT GetBuffer(uint Buffer, [NativeTypeName("const IID &")] Guid* riid, void** ppSurface)
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, Guid*, void**, int>)(lpVtbl[9]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), Buffer, riid, ppSurface);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT ResizeBuffers(uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, uint, uint, uint, DXGI_FORMAT, uint, int>)(lpVtbl[13]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), BufferCount, Width, Height, NewFormat, SwapChainFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(33)]
        public HANDLE GetFrameLatencyWaitableObject()
        {
            return ((HANDLE)(((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, void*>)(lpVtbl[33]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(34)]
        public HRESULT SetMatrixTransform([NativeTypeName("const DXGI_MATRIX_3X2_F *")] DXGI_MATRIX_3X2_F* pMatrix)
        {
            return ((delegate* unmanaged[Stdcall]<IDXGISwapChain2*, DXGI_MATRIX_3X2_F*, int>)(lpVtbl[34]))((IDXGISwapChain2*)Unsafe.AsPointer(ref this), pMatrix);
        }
    }
}
