// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/oaidl.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct VARIANT
    {
        [NativeTypeName("tagVARIANT::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/oaidl.h:478:5)")]
        public _Anonymous_e__Union Anonymous;

        public ref ushort vt
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.vt, 1));
            }
        }

        public ref ushort wReserved1
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.wReserved1, 1));
            }
        }

        public ref ushort wReserved2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.wReserved2, 1));
            }
        }

        public ref ushort wReserved3
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.wReserved3, 1));
            }
        }

        public ref long llVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.llVal, 1));
            }
        }

        public ref int lVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.lVal, 1));
            }
        }

        public ref byte bVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.bVal, 1));
            }
        }

        public ref short iVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.iVal, 1));
            }
        }

        public ref float fltVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.fltVal, 1));
            }
        }

        public ref double dblVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.dblVal, 1));
            }
        }

        public ref short boolVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.boolVal, 1));
            }
        }

        public ref short __OBSOLETE__VARIANT_BOOL
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.__OBSOLETE__VARIANT_BOOL, 1));
            }
        }

        public ref int scode
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.scode, 1));
            }
        }

        public ref CY cyVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cyVal, 1));
            }
        }

        public ref double date
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.date, 1));
            }
        }

        public ref ushort* bstrVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.bstrVal;
            }
        }

        public ref IUnknown* punkVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.punkVal;
            }
        }

        public ref IDispatch* pdispVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pdispVal;
            }
        }

        public ref SAFEARRAY* parray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.parray;
            }
        }

        public ref byte* pbVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pbVal;
            }
        }

        public ref short* piVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.piVal;
            }
        }

        public ref int* plVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.plVal;
            }
        }

        public ref long* pllVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pllVal;
            }
        }

        public ref float* pfltVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pfltVal;
            }
        }

        public ref double* pdblVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pdblVal;
            }
        }

        public ref short* pboolVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pboolVal;
            }
        }

        public ref short* __OBSOLETE__VARIANT_PBOOL
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.__OBSOLETE__VARIANT_PBOOL;
            }
        }

        public ref int* pscode
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pscode;
            }
        }

        public ref CY* pcyVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pcyVal;
            }
        }

        public ref double* pdate
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pdate;
            }
        }

        public ref ushort** pbstrVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pbstrVal;
            }
        }

        public ref IUnknown** ppunkVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.ppunkVal;
            }
        }

        public ref IDispatch** ppdispVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.ppdispVal;
            }
        }

        public ref SAFEARRAY** pparray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pparray;
            }
        }

        public ref VARIANT* pvarVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pvarVal;
            }
        }

        public ref void* byref
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.byref;
            }
        }

        public ref sbyte cVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cVal, 1));
            }
        }

        public ref ushort uiVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.uiVal, 1));
            }
        }

        public ref uint ulVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.ulVal, 1));
            }
        }

        public ref ulong ullVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.ullVal, 1));
            }
        }

        public ref int intVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.intVal, 1));
            }
        }

        public ref uint uintVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.uintVal, 1));
            }
        }

        public ref DECIMAL* pdecVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pdecVal;
            }
        }

        public ref sbyte* pcVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pcVal;
            }
        }

        public ref ushort* puiVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.puiVal;
            }
        }

        public ref uint* pulVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pulVal;
            }
        }

        public ref ulong* pullVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pullVal;
            }
        }

        public ref int* pintVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pintVal;
            }
        }

        public ref uint* puintVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.puintVal;
            }
        }

        public ref void* pvRecord
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.Anonymous.pvRecord;
            }
        }

        public ref IRecordInfo* pRecInfo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.Anonymous.pRecInfo;
            }
        }

        public ref DECIMAL decVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.decVal, 1));
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe partial struct _Anonymous_e__Union
        {
            [FieldOffset(0)]
            [NativeTypeName("tagVARIANT::(anonymous struct at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/oaidl.h:480:9)")]
            public _Anonymous_e__Struct Anonymous;

            [FieldOffset(0)]
            public DECIMAL decVal;

            public unsafe partial struct _Anonymous_e__Struct
            {
                [NativeTypeName("VARTYPE")]
                public ushort vt;

                [NativeTypeName("WORD")]
                public ushort wReserved1;

                [NativeTypeName("WORD")]
                public ushort wReserved2;

                [NativeTypeName("WORD")]
                public ushort wReserved3;

                [NativeTypeName("tagVARIANT::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/oaidl.h:486:13)")]
                public _Anonymous_e__Union Anonymous;

                [StructLayout(LayoutKind.Explicit)]
                public unsafe partial struct _Anonymous_e__Union
                {
                    [FieldOffset(0)]
                    [NativeTypeName("LONGLONG")]
                    public long llVal;

                    [FieldOffset(0)]
                    [NativeTypeName("LONG")]
                    public int lVal;

                    [FieldOffset(0)]
                    public byte bVal;

                    [FieldOffset(0)]
                    public short iVal;

                    [FieldOffset(0)]
                    public float fltVal;

                    [FieldOffset(0)]
                    public double dblVal;

                    [FieldOffset(0)]
                    [NativeTypeName("VARIANT_BOOL")]
                    public short boolVal;

                    [FieldOffset(0)]
                    [NativeTypeName("VARIANT_BOOL")]
                    public short __OBSOLETE__VARIANT_BOOL;

                    [FieldOffset(0)]
                    [NativeTypeName("SCODE")]
                    public int scode;

                    [FieldOffset(0)]
                    public CY cyVal;

                    [FieldOffset(0)]
                    [NativeTypeName("DATE")]
                    public double date;

                    [FieldOffset(0)]
                    [NativeTypeName("BSTR")]
                    public ushort* bstrVal;

                    [FieldOffset(0)]
                    public IUnknown* punkVal;

                    [FieldOffset(0)]
                    public IDispatch* pdispVal;

                    [FieldOffset(0)]
                    public SAFEARRAY* parray;

                    [FieldOffset(0)]
                    public byte* pbVal;

                    [FieldOffset(0)]
                    public short* piVal;

                    [FieldOffset(0)]
                    [NativeTypeName("LONG *")]
                    public int* plVal;

                    [FieldOffset(0)]
                    [NativeTypeName("LONGLONG *")]
                    public long* pllVal;

                    [FieldOffset(0)]
                    public float* pfltVal;

                    [FieldOffset(0)]
                    public double* pdblVal;

                    [FieldOffset(0)]
                    [NativeTypeName("VARIANT_BOOL *")]
                    public short* pboolVal;

                    [FieldOffset(0)]
                    [NativeTypeName("VARIANT_BOOL *")]
                    public short* __OBSOLETE__VARIANT_PBOOL;

                    [FieldOffset(0)]
                    [NativeTypeName("SCODE *")]
                    public int* pscode;

                    [FieldOffset(0)]
                    public CY* pcyVal;

                    [FieldOffset(0)]
                    [NativeTypeName("DATE *")]
                    public double* pdate;

                    [FieldOffset(0)]
                    [NativeTypeName("BSTR *")]
                    public ushort** pbstrVal;

                    [FieldOffset(0)]
                    public IUnknown** ppunkVal;

                    [FieldOffset(0)]
                    public IDispatch** ppdispVal;

                    [FieldOffset(0)]
                    public SAFEARRAY** pparray;

                    [FieldOffset(0)]
                    public VARIANT* pvarVal;

                    [FieldOffset(0)]
                    [NativeTypeName("PVOID")]
                    public void* byref;

                    [FieldOffset(0)]
                    [NativeTypeName("CHAR")]
                    public sbyte cVal;

                    [FieldOffset(0)]
                    public ushort uiVal;

                    [FieldOffset(0)]
                    [NativeTypeName("ULONG")]
                    public uint ulVal;

                    [FieldOffset(0)]
                    [NativeTypeName("ULONGLONG")]
                    public ulong ullVal;

                    [FieldOffset(0)]
                    public int intVal;

                    [FieldOffset(0)]
                    public uint uintVal;

                    [FieldOffset(0)]
                    public DECIMAL* pdecVal;

                    [FieldOffset(0)]
                    [NativeTypeName("CHAR *")]
                    public sbyte* pcVal;

                    [FieldOffset(0)]
                    public ushort* puiVal;

                    [FieldOffset(0)]
                    [NativeTypeName("ULONG *")]
                    public uint* pulVal;

                    [FieldOffset(0)]
                    [NativeTypeName("ULONGLONG *")]
                    public ulong* pullVal;

                    [FieldOffset(0)]
                    public int* pintVal;

                    [FieldOffset(0)]
                    public uint* puintVal;

                    [FieldOffset(0)]
                    [NativeTypeName("tagVARIANT::(anonymous struct at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/oaidl.h:533:17)")]
                    public _Anonymous_e__Struct Anonymous;

                    public unsafe partial struct _Anonymous_e__Struct
                    {
                        [NativeTypeName("PVOID")]
                        public void* pvRecord;

                        public IRecordInfo* pRecInfo;
                    }
                }
            }
        }
    }
}
