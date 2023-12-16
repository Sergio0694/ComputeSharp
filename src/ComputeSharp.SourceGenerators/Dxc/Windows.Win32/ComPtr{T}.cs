// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from winrt/wrl/client.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32;

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

    /// <summary>Increments the reference count for the current COM object, if any, and copies its address to a target raw pointer.</summary>
    /// <param name="ptr">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>This method always returns <see cref="HRESULT.S_OK"/>.</returns>
    public readonly HRESULT CopyTo(T** ptr)
    {
        _ = ((IUnknown*)this.ptr)->AddRef();

        *ptr = this.ptr;

        return HRESULT.S_OK;
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

    /// <summary>
    /// Moves the current <see cref="ComPtr{T}"/> instance and resets it without releasing the reference.
    /// </summary>
    /// <returns>The moved <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComPtr<T> Move()
    {
        ComPtr<T> other = default;

        other.ptr = this.ptr;

        this.ptr = null;

        return other;
    }
}