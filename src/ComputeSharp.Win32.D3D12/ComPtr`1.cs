// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from winrt/wrl/client.h in the Windows SDK for Windows 10.0.20348.0
// Original source is Copyright © Microsoft. All rights reserved.

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static TerraFX.Interop.Windows.Windows;
using static TerraFX.Interop.Windows.S;

namespace TerraFX.Interop.Windows;

/// <summary>
/// Extensions for <see cref="ComPtr{T}"/> to operate on raw pointers as well.
/// </summary>
internal static unsafe class ComPtr
{
    /// <summary>
    /// Disposes a target COM object, if it's not <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The type of COM objects to work with.</typeparam>
    /// <param name="other">The COM object to potentially release.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Dispose<T>(T* other)
        where T : unmanaged
    {
        if (other is not null)
        {
            _ = ((IUnknown*)other)->Release();
        }
    }

    /// <summary>
    /// Copies a given COM object into a target destination, incrementing reference counts where needed.
    /// </summary>
    /// <typeparam name="T">The type of COM objects to work with.</typeparam>
    /// <param name="obj">The input COM object to copy.</param>
    /// <param name="other">The target destination for the COM object.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyTo<T>(T* obj, ref T* other)
        where T : unmanaged
    {
        // We specifically need to increment the reference count on the source object before releasing the
        // target object, to avoid issues in case source and destination were pointers to the same object
        // (which can be the case even if the two pointers are not identical). To double check, a full check
        // through an IUnknown* cast would be needed, but to avoid the extra QueryInterface call on both objects,
        // we can just always do one AddRef call which avoids issues in all cases as well. The situation we need
        // to avoid is that without a temporary copy, releasing the target object might actually destroy the
        // object, causing the following AddRef call to AV.
        if (obj is not null)
        {
            _ = ((IUnknown*)obj)->AddRef();
        }

        T* pointer = other;

        if (pointer is not null)
        {
            _ = ((IUnknown*)pointer)->Release();
        }

        other = obj;
    }

    /// <summary>
    /// Writes a given COM object into a target (assumed uninitialized) destination, incrementing reference counts where needed.
    /// </summary>
    /// <typeparam name="T">The type of COM objects to work with.</typeparam>
    /// <param name="obj">The input COM object to copy.</param>
    /// <param name="other">The target destination for the COM object.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteTo<T>(T* obj, out T* other)
        where T : unmanaged
    {
        _ = ((IUnknown*)obj)->AddRef();

        other = obj;
    }
}

/// <summary>A type that allows working with pointers to COM objects more securely.</summary>
/// <typeparam name="T">The type to wrap in the current <see cref="ComPtr{T}"/> instance.</typeparam>
/// <remarks>While this type is not marked as <see langword="ref"/> so that it can also be used in fields, make sure to keep the reference counts properly tracked if you do store <see cref="ComPtr{T}"/> instances on the heap.</remarks>
internal unsafe struct ComPtr<T> : IDisposable
    where T : unmanaged
{
    /// <summary>The raw pointer to a COM object, if existing.</summary>
    private T* ptr_;

    /// <summary>Creates a new <see cref="ComPtr{T}"/> instance from a raw pointer and increments the ref count.</summary>
    /// <param name="other">The raw pointer to wrap.</param>
    public ComPtr(T* other)
    {
        ptr_ = other;
        InternalAddRef();
    }

    /// <summary>Releases the current COM object, if any, and replaces the internal pointer with an input raw pointer.</summary>
    /// <param name="other">The input raw pointer to wrap.</param>
    /// <remarks>This method will release the current raw pointer, if any, but it will not increment the references for <paramref name="other"/>.</remarks>
    public void Attach(T* other)
    {
        if (ptr_ != null)
        {
            var @ref = ((IUnknown*)ptr_)->Release();
            Debug.Assert((@ref != 0) || (ptr_ != other));
        }
        ptr_ = other;
    }

    /// <summary>Returns the raw pointer wrapped by the current instance, and resets the current <see cref="ComPtr{T}"/> value.</summary>
    /// <returns>The raw pointer wrapped by the current <see cref="ComPtr{T}"/> value.</returns>
    /// <remarks>This method will not change the reference count for the COM object in use.</remarks>
    public T* Detach()
    {
        T* ptr = ptr_;
        ptr_ = null;
        return ptr;
    }

    /// <summary>Increments the reference count for the current COM object, if any, and copies its address to a target raw pointer.</summary>
    /// <param name="ptr">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>This method always returns <see cref="S_OK"/>.</returns>
    public readonly HRESULT CopyTo(T** ptr)
    {
        InternalAddRef();
        *ptr = ptr_;
        return S_OK;
    }

    /// <summary>Increments the reference count for the current COM object, if any, and copies its address to a target <see cref="ComPtr{T}"/>.</summary>
    /// <param name="p">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>This method always returns <see cref="S_OK"/>.</returns>
    public readonly HRESULT CopyTo(ComPtr<T>* p)
    {
        InternalAddRef();
        *p->ReleaseAndGetAddressOf() = ptr_;
        return S_OK;
    }

    /// <summary>Increments the reference count for the current COM object, if any, and copies its address to a target <see cref="ComPtr{T}"/>.</summary>
    /// <param name="other">The target reference to copy the address of the current COM object to.</param>
    /// <returns>This method always returns <see cref="S_OK"/>.</returns>
    public readonly HRESULT CopyTo(ref ComPtr<T> other)
    {
        InternalAddRef();
        other.Attach(ptr_);
        return S_OK;
    }

    /// <summary>Converts the current COM object reference to a given interface type and assigns that to a target raw pointer.</summary>
    /// <param name="ptr">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target type <typeparamref name="U"/>.</returns>
    public readonly HRESULT CopyTo<U>(U** ptr)
        where U : unmanaged
    {
        return ((IUnknown*)ptr_)->QueryInterface(__uuidof<U>(), (void**)ptr);
    }

    /// <summary>Converts the current COM object reference to a given interface type and assigns that to a target <see cref="ComPtr{T}"/>.</summary>
    /// <param name="p">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target type <typeparamref name="U"/>.</returns>
    public readonly HRESULT CopyTo<U>(ComPtr<U>* p)
        where U : unmanaged
    {
        return ((IUnknown*)ptr_)->QueryInterface(__uuidof<U>(), (void**)p->ReleaseAndGetAddressOf());
    }

    /// <summary>Converts the current COM object reference to a given interface type and assigns that to a target <see cref="ComPtr{T}"/>.</summary>
    /// <param name="other">The target reference to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target type <typeparamref name="U"/>.</returns>
    public readonly HRESULT CopyTo<U>(ref ComPtr<U> other)
        where U : unmanaged
    {
        U* ptr;
        HRESULT result = ((IUnknown*)ptr_)->QueryInterface(__uuidof<U>(), (void**)&ptr);

        other.Attach(ptr);
        return result;
    }

    /// <summary>Converts the current object reference to a type indicated by the given IID and assigns that to a target address.</summary>
    /// <param name="riid">The IID indicating the interface type to convert the COM object reference to.</param>
    /// <param name="ptr">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target IID.</returns>
    public readonly HRESULT CopyTo(Guid* riid, void** ptr)
    {
        return ((IUnknown*)ptr_)->QueryInterface(riid, ptr);
    }

    /// <summary>Converts the current object reference to a type indicated by the given IID and assigns that to a target <see cref="ComPtr{T}"/> value.</summary>
    /// <param name="riid">The IID indicating the interface type to convert the COM object reference to.</param>
    /// <param name="p">The target raw pointer to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target IID.</returns>
    public readonly HRESULT CopyTo(Guid* riid, ComPtr<IUnknown>* p)
    {
        return ((IUnknown*)ptr_)->QueryInterface(riid, (void**)p->ReleaseAndGetAddressOf());
    }

    /// <summary>Converts the current object reference to a type indicated by the given IID and assigns that to a target <see cref="ComPtr{T}"/> value.</summary>
    /// <param name="riid">The IID indicating the interface type to convert the COM object reference to.</param>
    /// <param name="other">The target reference to copy the address of the current COM object to.</param>
    /// <returns>The result of <see cref="IUnknown.QueryInterface"/> for the target IID.</returns>
    public readonly HRESULT CopyTo(Guid* riid, ref ComPtr<IUnknown> other)
    {
        IUnknown* ptr;
        HRESULT result = ((IUnknown*)ptr_)->QueryInterface(riid, (void**)&ptr);

        other.Attach(ptr);
        return result;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        T* pointer = ptr_;
        if (pointer != null)
        {
            ptr_ = null;

            _ = ((IUnknown*)pointer)->Release();
        }
    }

    /// <summary>Gets the currently wrapped raw pointer to a COM object.</summary>
    /// <returns>The raw pointer wrapped by the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T* Get()
    {
        return ptr_;
    }

    /// <summary>Gets the address of the current <see cref="ComPtr{T}"/> instance as a raw <typeparamref name="T"/> double pointer. This method is only valid when the current <see cref="ComPtr{T}"/> instance is on the stack or pinned.
    /// </summary>
    /// <returns>The raw pointer to the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly T** GetAddressOf()
    {
        return (T**)Unsafe.AsPointer(ref Unsafe.AsRef(in this));
    }

    /// <summary>Gets the address of the current <see cref="ComPtr{T}"/> instance as a raw <typeparamref name="T"/> double pointer.</summary>
    /// <returns>The raw pointer to the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [UnscopedRef]
    public readonly ref T* GetPinnableReference()
    {
        return ref Unsafe.AsRef(in this).ptr_;
    }

    /// <summary>Releases the current COM object in use and gets the address of the <see cref="ComPtr{T}"/> instance as a raw <typeparamref name="T"/> double pointer. This method is only valid when the current <see cref="ComPtr{T}"/> instance is on the stack or pinned.</summary>
    /// <returns>The raw pointer to the current <see cref="ComPtr{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T** ReleaseAndGetAddressOf()
    {
        Dispose();

        return GetAddressOf();
    }

    /// <summary>Swaps the current COM object reference with that of a given <see cref="ComPtr{T}"/> instance.</summary>
    /// <param name="r">The target <see cref="ComPtr{T}"/> instance to swap with the current one.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Swap(ComPtr<T>* r)
    {
        T* tmp = ptr_;
        ptr_ = r->ptr_;
        r->ptr_ = tmp;
    }

    /// <summary>Swaps the current COM object reference with that of a given <see cref="ComPtr{T}"/> instance.</summary>
    /// <param name="other">The target <see cref="ComPtr{T}"/> instance to swap with the current one.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Swap(ref ComPtr<T> other)
    {
        T* tmp = ptr_;
        ptr_ = other.ptr_;
        other.ptr_ = tmp;
    }

    // Increments the reference count for the current COM object, if any
    private readonly void InternalAddRef()
    {
        T* temp = ptr_;

        if (temp != null)
        {
            _ = ((IUnknown*)temp)->AddRef();
        }
    }
}