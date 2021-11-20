// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from um/d3d12sdklayers.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

namespace TerraFX.Interop.DirectX
{
    [Guid("0742A90B-C387-483F-B946-30A7E4E61458")]
    [NativeTypeName("struct ID3D12InfoQueue : IUnknown")]
    [NativeInheritance("IUnknown")]
    internal unsafe partial struct ID3D12InfoQueue : ID3D12InfoQueue.Interface
    {
        public void** lpVtbl;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(0)]
        public HRESULT QueryInterface([NativeTypeName("const IID &")] Guid* riid, void** ppvObject)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, Guid*, void**, int>)(lpVtbl[0]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), riid, ppvObject);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(1)]
        [return: NativeTypeName("ULONG")]
        public uint AddRef()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, uint>)(lpVtbl[1]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(2)]
        [return: NativeTypeName("ULONG")]
        public uint Release()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, uint>)(lpVtbl[2]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(3)]
        public HRESULT SetMessageCountLimit([NativeTypeName("UINT64")] ulong MessageCountLimit)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, ulong, int>)(lpVtbl[3]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), MessageCountLimit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(4)]
        public void ClearStoredMessages()
        {
            ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, void>)(lpVtbl[4]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(5)]
        public HRESULT GetMessage([NativeTypeName("UINT64")] ulong MessageIndex, D3D12_MESSAGE* pMessage, [NativeTypeName("SIZE_T *")] nuint* pMessageByteLength)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, ulong, D3D12_MESSAGE*, nuint*, int>)(lpVtbl[5]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), MessageIndex, pMessage, pMessageByteLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(6)]
        [return: NativeTypeName("UINT64")]
        public ulong GetNumMessagesAllowedByStorageFilter()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, ulong>)(lpVtbl[6]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(7)]
        [return: NativeTypeName("UINT64")]
        public ulong GetNumMessagesDeniedByStorageFilter()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, ulong>)(lpVtbl[7]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(8)]
        [return: NativeTypeName("UINT64")]
        public ulong GetNumStoredMessages()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, ulong>)(lpVtbl[8]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(9)]
        [return: NativeTypeName("UINT64")]
        public ulong GetNumStoredMessagesAllowedByRetrievalFilter()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, ulong>)(lpVtbl[9]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(10)]
        [return: NativeTypeName("UINT64")]
        public ulong GetNumMessagesDiscardedByMessageCountLimit()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, ulong>)(lpVtbl[10]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(11)]
        [return: NativeTypeName("UINT64")]
        public ulong GetMessageCountLimit()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, ulong>)(lpVtbl[11]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(12)]
        public HRESULT AddStorageFilterEntries(D3D12_INFO_QUEUE_FILTER* pFilter)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_INFO_QUEUE_FILTER*, int>)(lpVtbl[12]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), pFilter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(13)]
        public HRESULT GetStorageFilter(D3D12_INFO_QUEUE_FILTER* pFilter, [NativeTypeName("SIZE_T *")] nuint* pFilterByteLength)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_INFO_QUEUE_FILTER*, nuint*, int>)(lpVtbl[13]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), pFilter, pFilterByteLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(14)]
        public void ClearStorageFilter()
        {
            ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, void>)(lpVtbl[14]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(15)]
        public HRESULT PushEmptyStorageFilter()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, int>)(lpVtbl[15]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(16)]
        public HRESULT PushCopyOfStorageFilter()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, int>)(lpVtbl[16]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(17)]
        public HRESULT PushStorageFilter(D3D12_INFO_QUEUE_FILTER* pFilter)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_INFO_QUEUE_FILTER*, int>)(lpVtbl[17]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), pFilter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(18)]
        public void PopStorageFilter()
        {
            ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, void>)(lpVtbl[18]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(19)]
        public uint GetStorageFilterStackSize()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, uint>)(lpVtbl[19]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(20)]
        public HRESULT AddRetrievalFilterEntries(D3D12_INFO_QUEUE_FILTER* pFilter)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_INFO_QUEUE_FILTER*, int>)(lpVtbl[20]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), pFilter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(21)]
        public HRESULT GetRetrievalFilter(D3D12_INFO_QUEUE_FILTER* pFilter, [NativeTypeName("SIZE_T *")] nuint* pFilterByteLength)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_INFO_QUEUE_FILTER*, nuint*, int>)(lpVtbl[21]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), pFilter, pFilterByteLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(22)]
        public void ClearRetrievalFilter()
        {
            ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, void>)(lpVtbl[22]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(23)]
        public HRESULT PushEmptyRetrievalFilter()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, int>)(lpVtbl[23]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(24)]
        public HRESULT PushCopyOfRetrievalFilter()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, int>)(lpVtbl[24]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(25)]
        public HRESULT PushRetrievalFilter(D3D12_INFO_QUEUE_FILTER* pFilter)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_INFO_QUEUE_FILTER*, int>)(lpVtbl[25]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), pFilter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(26)]
        public void PopRetrievalFilter()
        {
            ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, void>)(lpVtbl[26]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(27)]
        public uint GetRetrievalFilterStackSize()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, uint>)(lpVtbl[27]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(28)]
        public HRESULT AddMessage(D3D12_MESSAGE_CATEGORY Category, D3D12_MESSAGE_SEVERITY Severity, D3D12_MESSAGE_ID ID, [NativeTypeName("LPCSTR")] sbyte* pDescription)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_MESSAGE_CATEGORY, D3D12_MESSAGE_SEVERITY, D3D12_MESSAGE_ID, sbyte*, int>)(lpVtbl[28]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), Category, Severity, ID, pDescription);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(29)]
        public HRESULT AddApplicationMessage(D3D12_MESSAGE_SEVERITY Severity, [NativeTypeName("LPCSTR")] sbyte* pDescription)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_MESSAGE_SEVERITY, sbyte*, int>)(lpVtbl[29]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), Severity, pDescription);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(30)]
        public HRESULT SetBreakOnCategory(D3D12_MESSAGE_CATEGORY Category, BOOL bEnable)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_MESSAGE_CATEGORY, BOOL, int>)(lpVtbl[30]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), Category, bEnable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(31)]
        public HRESULT SetBreakOnSeverity(D3D12_MESSAGE_SEVERITY Severity, BOOL bEnable)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_MESSAGE_SEVERITY, BOOL, int>)(lpVtbl[31]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), Severity, bEnable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(32)]
        public HRESULT SetBreakOnID(D3D12_MESSAGE_ID ID, BOOL bEnable)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_MESSAGE_ID, BOOL, int>)(lpVtbl[32]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), ID, bEnable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(33)]
        public BOOL GetBreakOnCategory(D3D12_MESSAGE_CATEGORY Category)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_MESSAGE_CATEGORY, int>)(lpVtbl[33]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), Category);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(34)]
        public BOOL GetBreakOnSeverity(D3D12_MESSAGE_SEVERITY Severity)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_MESSAGE_SEVERITY, int>)(lpVtbl[34]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), Severity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(35)]
        public BOOL GetBreakOnID(D3D12_MESSAGE_ID ID)
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, D3D12_MESSAGE_ID, int>)(lpVtbl[35]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), ID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(36)]
        public void SetMuteDebugOutput(BOOL bMute)
        {
            ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, BOOL, void>)(lpVtbl[36]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this), bMute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [VtblIndex(37)]
        public BOOL GetMuteDebugOutput()
        {
            return ((delegate* unmanaged[Stdcall]<ID3D12InfoQueue*, int>)(lpVtbl[37]))((ID3D12InfoQueue*)Unsafe.AsPointer(ref this));
        }

        public interface Interface : IUnknown.Interface
        {
            [VtblIndex(3)]
            HRESULT SetMessageCountLimit([NativeTypeName("UINT64")] ulong MessageCountLimit);

            [VtblIndex(4)]
            void ClearStoredMessages();

            [VtblIndex(5)]
            HRESULT GetMessage([NativeTypeName("UINT64")] ulong MessageIndex, D3D12_MESSAGE* pMessage, [NativeTypeName("SIZE_T *")] nuint* pMessageByteLength);

            [VtblIndex(6)]
            [return: NativeTypeName("UINT64")]
            ulong GetNumMessagesAllowedByStorageFilter();

            [VtblIndex(7)]
            [return: NativeTypeName("UINT64")]
            ulong GetNumMessagesDeniedByStorageFilter();

            [VtblIndex(8)]
            [return: NativeTypeName("UINT64")]
            ulong GetNumStoredMessages();

            [VtblIndex(9)]
            [return: NativeTypeName("UINT64")]
            ulong GetNumStoredMessagesAllowedByRetrievalFilter();

            [VtblIndex(10)]
            [return: NativeTypeName("UINT64")]
            ulong GetNumMessagesDiscardedByMessageCountLimit();

            [VtblIndex(11)]
            [return: NativeTypeName("UINT64")]
            ulong GetMessageCountLimit();

            [VtblIndex(12)]
            HRESULT AddStorageFilterEntries(D3D12_INFO_QUEUE_FILTER* pFilter);

            [VtblIndex(13)]
            HRESULT GetStorageFilter(D3D12_INFO_QUEUE_FILTER* pFilter, [NativeTypeName("SIZE_T *")] nuint* pFilterByteLength);

            [VtblIndex(14)]
            void ClearStorageFilter();

            [VtblIndex(15)]
            HRESULT PushEmptyStorageFilter();

            [VtblIndex(16)]
            HRESULT PushCopyOfStorageFilter();

            [VtblIndex(17)]
            HRESULT PushStorageFilter(D3D12_INFO_QUEUE_FILTER* pFilter);

            [VtblIndex(18)]
            void PopStorageFilter();

            [VtblIndex(19)]
            uint GetStorageFilterStackSize();

            [VtblIndex(20)]
            HRESULT AddRetrievalFilterEntries(D3D12_INFO_QUEUE_FILTER* pFilter);

            [VtblIndex(21)]
            HRESULT GetRetrievalFilter(D3D12_INFO_QUEUE_FILTER* pFilter, [NativeTypeName("SIZE_T *")] nuint* pFilterByteLength);

            [VtblIndex(22)]
            void ClearRetrievalFilter();

            [VtblIndex(23)]
            HRESULT PushEmptyRetrievalFilter();

            [VtblIndex(24)]
            HRESULT PushCopyOfRetrievalFilter();

            [VtblIndex(25)]
            HRESULT PushRetrievalFilter(D3D12_INFO_QUEUE_FILTER* pFilter);

            [VtblIndex(26)]
            void PopRetrievalFilter();

            [VtblIndex(27)]
            uint GetRetrievalFilterStackSize();

            [VtblIndex(28)]
            HRESULT AddMessage(D3D12_MESSAGE_CATEGORY Category, D3D12_MESSAGE_SEVERITY Severity, D3D12_MESSAGE_ID ID, [NativeTypeName("LPCSTR")] sbyte* pDescription);

            [VtblIndex(29)]
            HRESULT AddApplicationMessage(D3D12_MESSAGE_SEVERITY Severity, [NativeTypeName("LPCSTR")] sbyte* pDescription);

            [VtblIndex(30)]
            HRESULT SetBreakOnCategory(D3D12_MESSAGE_CATEGORY Category, BOOL bEnable);

            [VtblIndex(31)]
            HRESULT SetBreakOnSeverity(D3D12_MESSAGE_SEVERITY Severity, BOOL bEnable);

            [VtblIndex(32)]
            HRESULT SetBreakOnID(D3D12_MESSAGE_ID ID, BOOL bEnable);

            [VtblIndex(33)]
            BOOL GetBreakOnCategory(D3D12_MESSAGE_CATEGORY Category);

            [VtblIndex(34)]
            BOOL GetBreakOnSeverity(D3D12_MESSAGE_SEVERITY Severity);

            [VtblIndex(35)]
            BOOL GetBreakOnID(D3D12_MESSAGE_ID ID);

            [VtblIndex(36)]
            void SetMuteDebugOutput(BOOL bMute);

            [VtblIndex(37)]
            BOOL GetMuteDebugOutput();
        }
    }
}
