using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.Win32;

namespace ComputeSharp.D2D1.WinUI.Extensions;

/// <summary>
/// Helper methods for working with the <see cref="IUnknown"/> type.
/// </summary>
internal static unsafe class IUnknownExtensions
{
    /// <summary>
    /// Checks whether two <see cref="IUnknown"/> values have the same underlying COM object.
    /// </summary>
    /// <typeparam name="T">The type of the first value to check.</typeparam>
    /// <typeparam name="U">The type of the second value to check.</typeparam>
    /// <param name="left">The first <see cref="IUnknown"/> object to compare.</param>
    /// <param name="right">The second <see cref="IUnknown"/> object to compare.</param>
    /// <returns>Whether <paramref name="left"/> and <paramref name="right"/> point to the same COM object.</returns>
    public static bool IsSameInstance<T, U>(this ref T left, U* right)
        where T : unmanaged // IUnknown
        where U : unmanaged // IUnknown
    {
        if (Unsafe.AreSame(ref left, ref *(T*)right))
        {
            return true;
        }

        if (Unsafe.IsNullRef(ref left) || right is null)
        {
            return false;
        }

        using ComPtr<IUnknown> leftUnknown = default;
        using ComPtr<IUnknown> rightUnknown = default;

        ((IUnknown*)Unsafe.AsPointer(ref left))->QueryInterface(Win32.Windows.__uuidof<IUnknown>(), (void**)leftUnknown.GetAddressOf()).Assert();
        ((IUnknown*)right)->QueryInterface(Win32.Windows.__uuidof<IUnknown>(), (void**)rightUnknown.GetAddressOf()).Assert();

        return leftUnknown.Get() == rightUnknown.Get();
    }

    /// <summary>
    /// Copies the source <see cref="IUnknown"/> object onto a target <see cref="ComPtr{T}"/> destination.
    /// </summary>
    /// <typeparam name="T">The type of object being copied.</typeparam>
    /// <param name="source">The source <see cref="IUnknown"/> object.</param>
    /// <param name="destination">The destination <see cref="ComPtr{T}"/> location.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static HRESULT CopyTo<T>(this ref T source, ref ComPtr<T> destination)
        where T : unmanaged // IUnknown
    {
        // Note: this explicitly needs a temporary copy to avoid issues in case source and destination were
        // pointers to the same object (which can be the case even if the two pointers are not identical).
        // To double check, a full check through an IUnknown* cast would be needed, but to avoid the extra
        // QueryInterface call on both objects, we can just always do one AddRef call which avoids issues in
        // all cases as well. The situation we need to avoid is that without a temporary copy, releasing the
        // target object might actually destroy the object, causing the following AddRef call to AV.
        using ComPtr<T> temporary = new((T*)Unsafe.AsPointer(ref source));

        return temporary.CopyTo(ref destination);
    }

    /// <summary>
    /// Copies the source <see cref="IUnknown"/> object onto a target <typeparamref name="U"/> location.
    /// </summary>
    /// <typeparam name="T">The type of object being copied.</typeparam>
    /// <typeparam name="U">The type of destination object being copied.</typeparam>
    /// <param name="source">The source <see cref="IUnknown"/> object.</param>
    /// <param name="destination">The destination <typeparamref name="U"/> location.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static HRESULT CopyTo<T, U>(this ref T source, U** destination)
        where T : unmanaged // IUnknown
        where U : unmanaged // IUnknown
    {
        using ComPtr<T> temporary = new((T*)Unsafe.AsPointer(ref source));

        return temporary.CopyTo(destination);
    }
}