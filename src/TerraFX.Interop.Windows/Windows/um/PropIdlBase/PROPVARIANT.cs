// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/PropIdlBase.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TerraFX.Interop.Windows
{
    public unsafe partial struct PROPVARIANT
    {
        [NativeTypeName("tagPROPVARIANT::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/PropIdlBase.h:303:3)")]
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

        public ref sbyte cVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cVal, 1));
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

        public ref ushort uiVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.uiVal, 1));
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

        public ref uint ulVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.ulVal, 1));
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

        public ref LARGE_INTEGER hVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.hVal, 1));
            }
        }

        public ref ULARGE_INTEGER uhVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.uhVal, 1));
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

        public ref FILETIME filetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.filetime, 1));
            }
        }

        public ref Guid* puuid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.puuid;
            }
        }

        public ref CLIPDATA* pclipdata
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pclipdata;
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

        public ref BSTRBLOB bstrblobVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.bstrblobVal, 1));
            }
        }

        public ref BLOB blob
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.blob, 1));
            }
        }

        public ref sbyte* pszVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pszVal;
            }
        }

        public ref ushort* pwszVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pwszVal;
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

        public ref IStream* pStream
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pStream;
            }
        }

        public ref IStorage* pStorage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pStorage;
            }
        }

        public ref VERSIONEDSTREAM* pVersionedStream
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pVersionedStream;
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

        public ref CAC cac
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cac, 1));
            }
        }

        public ref CAUB caub
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.caub, 1));
            }
        }

        public ref CAI cai
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cai, 1));
            }
        }

        public ref CAUI caui
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.caui, 1));
            }
        }

        public ref CAL cal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cal, 1));
            }
        }

        public ref CAUL caul
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.caul, 1));
            }
        }

        public ref CAH cah
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cah, 1));
            }
        }

        public ref CAUH cauh
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cauh, 1));
            }
        }

        public ref CAFLT caflt
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.caflt, 1));
            }
        }

        public ref CADBL cadbl
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cadbl, 1));
            }
        }

        public ref CABOOL cabool
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cabool, 1));
            }
        }

        public ref CASCODE cascode
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cascode, 1));
            }
        }

        public ref CACY cacy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cacy, 1));
            }
        }

        public ref CADATE cadate
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cadate, 1));
            }
        }

        public ref CAFILETIME cafiletime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cafiletime, 1));
            }
        }

        public ref CACLSID cauuid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cauuid, 1));
            }
        }

        public ref CACLIPDATA caclipdata
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.caclipdata, 1));
            }
        }

        public ref CABSTR cabstr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cabstr, 1));
            }
        }

        public ref CABSTRBLOB cabstrblob
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.cabstrblob, 1));
            }
        }

        public ref CALPSTR calpstr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.calpstr, 1));
            }
        }

        public ref CALPWSTR calpwstr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.calpwstr, 1));
            }
        }

        public ref CAPROPVARIANT capropvar
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous.Anonymous.capropvar, 1));
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

        public ref ushort* puiVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.puiVal;
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

        public ref uint* pulVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pulVal;
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

        public ref DECIMAL* pdecVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pdecVal;
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

        public ref PROPVARIANT* pvarVal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous.Anonymous.pvarVal;
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
            [NativeTypeName("tagPROPVARIANT::(anonymous struct at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/PropIdlBase.h:305:1)")]
            public _Anonymous_e__Struct Anonymous;

            [FieldOffset(0)]
            public DECIMAL decVal;

            public unsafe partial struct _Anonymous_e__Struct
            {
                [NativeTypeName("VARTYPE")]
                public ushort vt;

                [NativeTypeName("PROPVAR_PAD1")]
                public ushort wReserved1;

                [NativeTypeName("PROPVAR_PAD2")]
                public ushort wReserved2;

                [NativeTypeName("PROPVAR_PAD3")]
                public ushort wReserved3;

                [NativeTypeName("tagPROPVARIANT::(anonymous union at C:/Program Files (x86)/Windows Kits/10/Include/10.0.20348.0/um/PropIdlBase.h:311:43)")]
                public _Anonymous_e__Union Anonymous;

                [StructLayout(LayoutKind.Explicit)]
                public unsafe partial struct _Anonymous_e__Union
                {
                    [FieldOffset(0)]
                    [NativeTypeName("CHAR")]
                    public sbyte cVal;

                    [FieldOffset(0)]
                    [NativeTypeName("UCHAR")]
                    public byte bVal;

                    [FieldOffset(0)]
                    public short iVal;

                    [FieldOffset(0)]
                    public ushort uiVal;

                    [FieldOffset(0)]
                    [NativeTypeName("LONG")]
                    public int lVal;

                    [FieldOffset(0)]
                    [NativeTypeName("ULONG")]
                    public uint ulVal;

                    [FieldOffset(0)]
                    public int intVal;

                    [FieldOffset(0)]
                    public uint uintVal;

                    [FieldOffset(0)]
                    public LARGE_INTEGER hVal;

                    [FieldOffset(0)]
                    public ULARGE_INTEGER uhVal;

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
                    public FILETIME filetime;

                    [FieldOffset(0)]
                    [NativeTypeName("CLSID *")]
                    public Guid* puuid;

                    [FieldOffset(0)]
                    public CLIPDATA* pclipdata;

                    [FieldOffset(0)]
                    [NativeTypeName("BSTR")]
                    public ushort* bstrVal;

                    [FieldOffset(0)]
                    public BSTRBLOB bstrblobVal;

                    [FieldOffset(0)]
                    public BLOB blob;

                    [FieldOffset(0)]
                    [NativeTypeName("LPSTR")]
                    public sbyte* pszVal;

                    [FieldOffset(0)]
                    [NativeTypeName("LPWSTR")]
                    public ushort* pwszVal;

                    [FieldOffset(0)]
                    public IUnknown* punkVal;

                    [FieldOffset(0)]
                    public IDispatch* pdispVal;

                    [FieldOffset(0)]
                    public IStream* pStream;

                    [FieldOffset(0)]
                    public IStorage* pStorage;

                    [FieldOffset(0)]
                    [NativeTypeName("LPVERSIONEDSTREAM")]
                    public VERSIONEDSTREAM* pVersionedStream;

                    [FieldOffset(0)]
                    [NativeTypeName("LPSAFEARRAY")]
                    public SAFEARRAY* parray;

                    [FieldOffset(0)]
                    public CAC cac;

                    [FieldOffset(0)]
                    public CAUB caub;

                    [FieldOffset(0)]
                    public CAI cai;

                    [FieldOffset(0)]
                    public CAUI caui;

                    [FieldOffset(0)]
                    public CAL cal;

                    [FieldOffset(0)]
                    public CAUL caul;

                    [FieldOffset(0)]
                    public CAH cah;

                    [FieldOffset(0)]
                    public CAUH cauh;

                    [FieldOffset(0)]
                    public CAFLT caflt;

                    [FieldOffset(0)]
                    public CADBL cadbl;

                    [FieldOffset(0)]
                    public CABOOL cabool;

                    [FieldOffset(0)]
                    public CASCODE cascode;

                    [FieldOffset(0)]
                    public CACY cacy;

                    [FieldOffset(0)]
                    public CADATE cadate;

                    [FieldOffset(0)]
                    public CAFILETIME cafiletime;

                    [FieldOffset(0)]
                    public CACLSID cauuid;

                    [FieldOffset(0)]
                    public CACLIPDATA caclipdata;

                    [FieldOffset(0)]
                    public CABSTR cabstr;

                    [FieldOffset(0)]
                    public CABSTRBLOB cabstrblob;

                    [FieldOffset(0)]
                    public CALPSTR calpstr;

                    [FieldOffset(0)]
                    public CALPWSTR calpwstr;

                    [FieldOffset(0)]
                    public CAPROPVARIANT capropvar;

                    [FieldOffset(0)]
                    [NativeTypeName("CHAR *")]
                    public sbyte* pcVal;

                    [FieldOffset(0)]
                    [NativeTypeName("UCHAR *")]
                    public byte* pbVal;

                    [FieldOffset(0)]
                    public short* piVal;

                    [FieldOffset(0)]
                    public ushort* puiVal;

                    [FieldOffset(0)]
                    [NativeTypeName("LONG *")]
                    public int* plVal;

                    [FieldOffset(0)]
                    [NativeTypeName("ULONG *")]
                    public uint* pulVal;

                    [FieldOffset(0)]
                    public int* pintVal;

                    [FieldOffset(0)]
                    public uint* puintVal;

                    [FieldOffset(0)]
                    public float* pfltVal;

                    [FieldOffset(0)]
                    public double* pdblVal;

                    [FieldOffset(0)]
                    [NativeTypeName("VARIANT_BOOL *")]
                    public short* pboolVal;

                    [FieldOffset(0)]
                    public DECIMAL* pdecVal;

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
                    [NativeTypeName("LPSAFEARRAY *")]
                    public SAFEARRAY** pparray;

                    [FieldOffset(0)]
                    public PROPVARIANT* pvarVal;
                }
            }
        }
    }
}
