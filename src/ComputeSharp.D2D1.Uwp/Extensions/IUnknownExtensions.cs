using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.Windows;
using Win32 = TerraFX.Interop.Windows.Windows;

namespace ComputeSharp.D2D1.Uwp.Extensions;

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

        ((IUnknown*)Unsafe.AsPointer(ref left))->QueryInterface(Win32.__uuidof<T>(), leftUnknown.GetVoidAddressOf()).Assert();
        ((IUnknown*)right)->QueryInterface(Win32.__uuidof<T>(), rightUnknown.GetVoidAddressOf()).Assert();

        return leftUnknown.Get() == rightUnknown.Get();
    }

    /// <summary>
    /// Copies the source <see cref="IUnknown"/> object onto a target <see cref="ComPtr{T}"/> destination.
    /// </summary>
    /// <typeparam name="T">The type of object being copied.</typeparam>
    /// <param name="source">The source <see cref="IUnknown"/> object.</param>
    /// <param name="destination">The destination <see cref="ComPtr{T}"/> location.</param>
    public static void CopyTo<T>(this ref T source, ref ComPtr<T> destination)
        where T : unmanaged // IUnknown
    {
        destination.Dispose();

        destination = new ComPtr<T>((T*)Unsafe.AsPointer(ref source));
    }
}