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
    [Guid("28211A43-7D89-476F-8181-2D6159B220AD")]
    [NativeTypeName("struct ID2D1Effect : ID2D1Properties")]
    [NativeInheritance("ID2D1Properties")]
    public unsafe partial struct ID2D1Effect : ID2D1Effect.Interface
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

        public void SetInputEffect([NativeTypeName("UINT32")] uint index, ID2D1Effect* inputEffect, [Optional, DefaultParameterValue(1)] BOOL invalidate)
        {
            ID2D1Image* output = null;

            if (inputEffect != null)
            {
                inputEffect->GetOutput(&output);
            }

            SetInput(index, output, invalidate);
            if (output != null)
            {
                output->Release();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged<ID2D1Effect*, Guid*, void**, int>)(lpVtbl[0]))((ID2D1Effect*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint>)(lpVtbl[1]))((ID2D1Effect*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint>)(lpVtbl[2]))((ID2D1Effect*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        [return: NativeTypeName("UINT32")]
        public uint GetPropertyCount()
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint>)(lpVtbl[3]))((ID2D1Effect*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public HRESULT GetPropertyName([NativeTypeName("UINT32")] uint index, [NativeTypeName("PWSTR")] ushort* name, [NativeTypeName("UINT32")] uint nameCount)
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint, ushort*, uint, int>)(lpVtbl[4]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, name, nameCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        [return: NativeTypeName("UINT32")]
        public uint GetPropertyNameLength([NativeTypeName("UINT32")] uint index)
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint, uint>)(lpVtbl[5]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        public D2D1_PROPERTY_TYPE GetType([NativeTypeName("UINT32")] uint index)
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE>)(lpVtbl[6]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        [return: NativeTypeName("UINT32")]
        public uint GetPropertyIndex([NativeTypeName("PCWSTR")] ushort* name)
        {
            return ((delegate* unmanaged<ID2D1Effect*, ushort*, uint>)(lpVtbl[7]))((ID2D1Effect*)Unsafe.AsPointer(ref this), name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        public HRESULT SetValueByName([NativeTypeName("PCWSTR")] ushort* name, D2D1_PROPERTY_TYPE type, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return ((delegate* unmanaged<ID2D1Effect*, ushort*, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[8]))((ID2D1Effect*)Unsafe.AsPointer(ref this), name, type, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        public HRESULT SetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, [NativeTypeName("const BYTE *")] byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[9]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, type, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        public HRESULT GetValueByName([NativeTypeName("PCWSTR")] ushort* name, D2D1_PROPERTY_TYPE type, byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return ((delegate* unmanaged<ID2D1Effect*, ushort*, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[10]))((ID2D1Effect*)Unsafe.AsPointer(ref this), name, type, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        public HRESULT GetValue([NativeTypeName("UINT32")] uint index, D2D1_PROPERTY_TYPE type, byte* data, [NativeTypeName("UINT32")] uint dataSize)
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int>)(lpVtbl[11]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, type, data, dataSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        [return: NativeTypeName("UINT32")]
        public uint GetValueSize([NativeTypeName("UINT32")] uint index)
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint, uint>)(lpVtbl[12]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT GetSubProperties([NativeTypeName("UINT32")] uint index, ID2D1Properties** subProperties)
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint, ID2D1Properties**, int>)(lpVtbl[13]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, subProperties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public void SetInput([NativeTypeName("UINT32")] uint index, ID2D1Image* input, [Optional, DefaultParameterValue(1)] BOOL invalidate)
        {
            ((delegate* unmanaged<ID2D1Effect*, uint, ID2D1Image*, BOOL, void>)(lpVtbl[14]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, input, invalidate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(15)]
        public HRESULT SetInputCount([NativeTypeName("UINT32")] uint inputCount)
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint, int>)(lpVtbl[15]))((ID2D1Effect*)Unsafe.AsPointer(ref this), inputCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(16)]
        public void GetInput([NativeTypeName("UINT32")] uint index, ID2D1Image** input)
        {
            ((delegate* unmanaged<ID2D1Effect*, uint, ID2D1Image**, void>)(lpVtbl[16]))((ID2D1Effect*)Unsafe.AsPointer(ref this), index, input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(17)]
        [return: NativeTypeName("UINT32")]
        public uint GetInputCount()
        {
            return ((delegate* unmanaged<ID2D1Effect*, uint>)(lpVtbl[17]))((ID2D1Effect*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(18)]
        public void GetOutput(ID2D1Image** outputImage)
        {
            ((delegate* unmanaged<ID2D1Effect*, ID2D1Image**, void>)(lpVtbl[18]))((ID2D1Effect*)Unsafe.AsPointer(ref this), outputImage);
        }

        public interface Interface : ID2D1Properties.Interface
        {
            [VtblIndex(14)]
            void SetInput([NativeTypeName("UINT32")] uint index, ID2D1Image* input, [Optional, DefaultParameterValue(1)] BOOL invalidate);

            [VtblIndex(15)]
            HRESULT SetInputCount([NativeTypeName("UINT32")] uint inputCount);

            [VtblIndex(16)]
            void GetInput([NativeTypeName("UINT32")] uint index, ID2D1Image** input);

            [VtblIndex(17)]
            [return: NativeTypeName("UINT32")]
            uint GetInputCount();

            [VtblIndex(18)]
            void GetOutput(ID2D1Image** outputImage);
        }

        public partial struct Vtbl
        {
            [NativeTypeName("HRESULT (const IID &, void **) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, Guid*, void**, int> QueryInterface;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint> AddRef;

            [NativeTypeName("ULONG () __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint> Release;

            [NativeTypeName("UINT32 () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint> GetPropertyCount;

            [NativeTypeName("HRESULT (UINT32, PWSTR, UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, ushort*, uint, int> GetPropertyName;

            [NativeTypeName("UINT32 (UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, uint> GetPropertyNameLength;

            [NativeTypeName("D2D1_PROPERTY_TYPE (UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public new delegate* unmanaged<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE> GetType;

            [NativeTypeName("UINT32 (PCWSTR) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, ushort*, uint> GetPropertyIndex;

            [NativeTypeName("HRESULT (PCWSTR, D2D1_PROPERTY_TYPE, const BYTE *, UINT32) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, ushort*, D2D1_PROPERTY_TYPE, byte*, uint, int> SetValueByName1;

            [NativeTypeName("HRESULT (UINT32, D2D1_PROPERTY_TYPE, const BYTE *, UINT32) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int> SetValue1;

            [NativeTypeName("HRESULT (PCWSTR, D2D1_PROPERTY_TYPE, BYTE *, UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, ushort*, D2D1_PROPERTY_TYPE, byte*, uint, int> GetValueByName1;

            [NativeTypeName("HRESULT (UINT32, D2D1_PROPERTY_TYPE, BYTE *, UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, D2D1_PROPERTY_TYPE, byte*, uint, int> GetValue1;

            [NativeTypeName("UINT32 (UINT32) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, uint> GetValueSize;

            [NativeTypeName("HRESULT (UINT32, ID2D1Properties **) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, ID2D1Properties**, int> GetSubProperties;

            [NativeTypeName("void (UINT32, ID2D1Image *, BOOL) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, ID2D1Image*, BOOL, void> SetInput;

            [NativeTypeName("HRESULT (UINT32) __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, int> SetInputCount;

            [NativeTypeName("void (UINT32, ID2D1Image **) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint, ID2D1Image**, void> GetInput;

            [NativeTypeName("UINT32 () const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, uint> GetInputCount;

            [NativeTypeName("void (ID2D1Image **) const __attribute__((nothrow)) __attribute__((stdcall))")]
            public delegate* unmanaged<ID2D1Effect*, ID2D1Image**, void> GetOutput;
        }
    }
}
