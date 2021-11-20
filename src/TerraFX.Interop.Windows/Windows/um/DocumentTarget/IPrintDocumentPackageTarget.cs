// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/DocumentTarget.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace TerraFX.Interop.Windows
{
    [SupportedOSPlatform("windows8.0")]
    [Guid("1B8EFEC4-3019-4C27-964E-367202156906")]
    [NativeTypeName("struct IPrintDocumentPackageTarget : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct IPrintDocumentPackageTarget
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<IPrintDocumentPackageTarget*, Guid*, void**, int>)(lpVtbl[0]))((IPrintDocumentPackageTarget*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<IPrintDocumentPackageTarget*, uint>)(lpVtbl[1]))((IPrintDocumentPackageTarget*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<IPrintDocumentPackageTarget*, uint>)(lpVtbl[2]))((IPrintDocumentPackageTarget*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT GetPackageTargetTypes([NativeTypeName("UINT32 *")] uint* targetCount, Guid** targetTypes)
        {
            return ((delegate* unmanaged[Stdcall]<IPrintDocumentPackageTarget*, uint*, Guid**, int>)(lpVtbl[3]))((IPrintDocumentPackageTarget*)Unsafe.AsPointer(ref this), targetCount, targetTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetPackageTarget([NativeTypeName("const GUID &")] Guid* guidTargetType, [NativeTypeName("const IID &")] Guid* riid, void** ppvTarget)
        {
            return ((delegate* unmanaged[Stdcall]<IPrintDocumentPackageTarget*, Guid*, Guid*, void**, int>)(lpVtbl[4]))((IPrintDocumentPackageTarget*)Unsafe.AsPointer(ref this), guidTargetType, riid, ppvTarget);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT Cancel()
        {
            return ((delegate* unmanaged[Stdcall]<IPrintDocumentPackageTarget*, int>)(lpVtbl[5]))((IPrintDocumentPackageTarget*)Unsafe.AsPointer(ref this));
        }
    }
}
