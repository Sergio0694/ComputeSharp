// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from winrt/wrl/client.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using Windows.Win32.System.Com;

namespace ComputeSharp.SourceGenerators.Dxc.Interop;

/// <summary>A type that allows working with pointers to COM objects more securely.</summary>
/// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
internal unsafe struct ComPtr<T> : IDisposable
    where T : unmanaged
{
    /// <summary>The raw pointer to a COM object, if existing.</summary>
    private T* ptr;

    /// <summary>Creates a new <see cref="ComPtr{T}"/> instance from a raw pointer and increments the ref count.</summary>
    /// <param name="other">The raw pointer to wrap.</param>
    public ComPtr(T* other)
    {
        this.ptr = other;

        if (other is not null)
        {
            _ = ((IUnknown*)other)->AddRef();
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        T* pointer = this.ptr;

        if (pointer is not null)
        {
            this.ptr = null;

            _ = ((IUnknown*)pointer)->Release();
        }
    }

    /// <summary>Gets the currently wrapped raw pointer to a COM object.</summary>
    /// <returns>The raw pointer wrapped by the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* Get()
    {
        return this.ptr;
    }

    /// <summary>Gets the address of the current <see cref="ComPtr{T}"/> instance as a raw <typeparamref name="T"/> double pointer. This method is only valid when the current <see cref="ComPtr{T}"/> instance is on the stack or pinned.
    /// </summary>
    /// <returns>The raw pointer to the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T** GetAddressOf()
    {
        return (T**)Unsafe.AsPointer(ref Unsafe.AsRef(in this));
    }
}