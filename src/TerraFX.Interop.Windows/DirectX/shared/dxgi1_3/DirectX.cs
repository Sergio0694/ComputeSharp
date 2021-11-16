// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from shared/dxgi1_3.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    public static unsafe partial class DirectX
    {
        [SupportedOSPlatform("windows8.1")]
        [DllImport("dxgi", ExactSpelling = true)]
        public static extern HRESULT CreateDXGIFactory2(uint Flags, [NativeTypeName("const IID &")] Guid* riid, void** ppFactory);

        [SupportedOSPlatform("windows8.1")]
        [DllImport("dxgi", ExactSpelling = true)]
        public static extern HRESULT DXGIGetDebugInterface1(uint Flags, [NativeTypeName("const IID &")] Guid* riid, void** pDebug);
    }
}
