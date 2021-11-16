// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d2d1_1.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D2D1_PROPERTY_TYPE;

namespace TerraFX.Interop.DirectX
{
    [Guid("483473D7-CD46-4F9D-9D3A-3112AA80159D")]
    [NativeTypeName("struct ID2D1Properties : IUnknown")]
    [NativeInheritance("IUnknown")]
    public unsafe partial struct ID2D1Properties : ID2D1Properties.Interface
    {
        public void** lpVtbl;

        public HRESULT SetValueByName([NativeTypeName("PCWSTR")] ushort* name, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return SetValueByName(name, D2D1_PROPERTY_TYPE_UNKNOWN, data, dataSize);
        }

        public HRESULT SetValue([NativeTypeName("UINT32")] uint index, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return SetValue(index, D2D1_PROPERTY_TYPE_UNKNOWN, data, dataSize);
        }

        public HRESULT GetValueByName([NativeTypeName("PCWSTR")] ushort* name, byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return GetValueByName(name, D2D1_PROPERTY_TYPE_UNKNOWN, data, dataSize);
        }

        public HRESULT GetValue([NativeTypeName("UINT32")] uint index, byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return GetValue(index, D2D1_PROPERTY_TYPE_UNKNOWN, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<ID2D1Properties*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1Properties*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint>)(lpVtbl[1]))((ID2D1Properties*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint>)(lpVtbl[2]))((ID2D1Properties*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        [return: NativeTypeName("UINT32")]
        public uint GetPropertyCount()
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint>)(lpVtbl[3]))((ID2D1Properties*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetPropertyName([NativeTypeName("UINT32")] uint index, [NativeTypeName("PWSTR")] ushort* name, [NativeTypeName("UINT32")] uint nameCount)
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint, ushort*, uint, int>)(lpVtbl[4]))((ID2D1Properties*)Unsafe.AsPointer(ref this), index, name, nameCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        [return: NativeTypeName("UINT32")]
        public uint GetPropertyNameLength([NativeTypeName("UINT32")] uint index)
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint, uint>)(lpVtbl[5]))((ID2D1Properties*)Unsafe.AsPointer(ref this), index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public D2D1_PROPERTY_TYPE GetType([NativeTypeName("UINT32")] uint index)
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint, D2D1_PROPERTY_TYPE>)(lpVtbl[6]))((ID2D1Properties*)Unsafe.AsPointer(ref this), index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        [return: NativeTypeName("UINT32")]
        public uint GetPropertyIndex([NativeTypeName("PCWSTR")] ushort* name)
        {
            return ((delegate* unmanaged<ID2D1Properties*, ushort*, uint>)(lpVtbl[7]))((ID2D1Properties*)Unsafe.AsPointer(ref this), name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT SetValueByName([NativeTypeName("PCWSTR")] ushort* name, D2D1_PROPERTY_TYPE type, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return ((delegate* unmanaged<ID2D1Properties*, ushort*, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[8]))((ID2D1Properties*)Unsafe.AsPointer(ref this), name, type, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT SetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[9]))((ID2D1Properties*)Unsafe.AsPointer(ref this), index, type, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT GetValueByName([NativeTypeName("PCWSTR")] ushort* name, D2D1_PROPERTY_TYPE type, byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return ((delegate* unmanaged<ID2D1Properties*, ushort*, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[10]))((ID2D1Properties*)Unsafe.AsPointer(ref this), name, type, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT GetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[11]))((ID2D1Properties*)Unsafe.AsPointer(ref this), index, type, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        [return: NativeTypeName("UINT32")]
        public uint GetValueSize([NativeTypeName("UINT32")] uint index)
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint, uint>)(lpVtbl[12]))((ID2D1Properties*)Unsafe.AsPointer(ref this), index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT GetSubProperties([NativeTypeName("UINT32")] uint index, ID2D1Properties** subProperties)
        {
            return ((delegate* unmanaged<ID2D1Properties*, uint, ID2D1Properties**, int>)(lpVtbl[13]))((ID2D1Properties*)Unsafe.AsPointer(ref this), index, subProperties);
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            [return: NativeTypeName("UINT32")]
            uint GetPropertyCount();

            [VtblIndex(4)]
            HRESULT GetPropertyName([NativeTypeName("UINT32")] uint index, [NativeTypeName("PWSTR")] ushort* name, [NativeTypeName("UINT32")] uint nameCount);

            [VtblIndex(5)]
            [return: NativeTypeName("UINT32")]
            uint GetPropertyNameLength([NativeTypeName("UINT32")] uint index);

            [VtblIndex(6)]
            D2D1_PROPERTY_TYPE GetType([NativeTypeName("UINT32")] uint index);

            [VtblIndex(7)]
            [return: NativeTypeName("UINT32")]
            uint GetPropertyIndex([NativeTypeName("PCWSTR")] ushort* name);

            [VtblIndex(8)]
            HRESULT SetValueByName([NativeTypeName("PCWSTR")] ushort* name, D2D1_PROPERTY_TYPE type, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize);

            [VtblIndex(9)]
            HRESULT SetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize);

            [VtblIndex(10)]
            HRESULT GetValueByName([NativeTypeName("PCWSTR")] ushort* name, D2D1_PROPERTY_TYPE type, byte* data, [NativeTypeName("UINT32")] uint dataSize);

            [VtblIndex(11)]
            HRESULT GetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, byte* data, [NativeTypeName("UINT32")] uint dataSize);

            [VtblIndex(12)]
            [return: NativeTypeName("UINT32")]
            uint GetValueSize([NativeTypeName("UINT32")] uint index);

            [VtblIndex(13)]
            HRESULT GetSubProperties([NativeTypeName("UINT32")] uint index, ID2D1Properties** subProperties);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint> Release;

            [NativeTypeName("UINT32 () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint> GetPropertyCount;

            [NativeTypeName("HRESULT (UINT32, PWSTR, UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint, ushort*, uint, int> GetPropertyName;

            [NativeTypeName("UINT32 (UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint, uint> GetPropertyNameLength;

            [NativeTypeName("D2D1_PROPERTY_TYPE (UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public new delegate* unmanaged<ID2D1Properties*, uint, D2D1_PROPERTY_TYPE> GetType;

            [NativeTypeName("UINT32 (PCWSTR) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, ushort*, uint> GetPropertyIndex;

            [NativeTypeName("HRESULT (PCWSTR, D2D1_PROPERTY_TYPE, const BYTE *, UINT32) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, ushort*, D2D1_PROPERTY_TYPE, byte*, uint, int> SetValueByName1;

            [NativeTypeName("HRESULT (UINT32, D2D1_PROPERTY_TYPE, const BYTE *, UINT32) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int> SetValue1;

            [NativeTypeName("HRESULT (PCWSTR, D2D1_PROPERTY_TYPE, BYTE *, UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, ushort*, D2D1_PROPERTY_TYPE, byte*, uint, int> GetValueByName1;

            [NativeTypeName("HRESULT (UINT32, D2D1_PROPERTY_TYPE, BYTE *, UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int> GetValue1;

            [NativeTypeName("UINT32 (UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint, uint> GetValueSize;

            [NativeTypeName("HRESULT (UINT32, ID2D1Properties **) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Properties*, uint, ID2D1Properties**, int> GetSubProperties;
        }
    }
}
