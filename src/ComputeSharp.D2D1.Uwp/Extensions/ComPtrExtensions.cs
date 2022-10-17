using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Uwp.Extensions;

/// <summary>
/// Helper methods for working with the <see cref="ComPtr{T}"/> type.
/// </summary>
internal static class ComPtrExtensions
{
    /// <summary>
    /// Checks whether two <see cref="ComPtr{T}"/> values have the same underlying COM object.
    /// </summary>
    /// <typeparam name="T">The type of the first value to check.</typeparam>
    /// <typeparam name="U">The type of the second value to check.</typeparam>
    /// <param name="left">The first <see cref="ComPtr{T}"/> object to compare.</param>
    /// <param name="right">The second <see cref="ComPtr{T}"/> object to compare.</param>
    /// <returns>Whether <paramref name="left"/> and <paramref name="right"/> point to the same COM object.</returns>
    public static unsafe bool IsSameInstance<T, U>(this in ComPtr<T> left, in ComPtr<U> right)
        where T : unmanaged
        where U : unmanaged
    {
        if (left.Get() == right.Get())
        {
            return true;
        }

        if (left.Get() is null || right.Get() is null)
        {
            return false;
        }

        using ComPtr<IUnknown> leftUnknown = default;
        using ComPtr<IUnknown> rightUnknown = default;

        left.CopyTo(leftUnknown.GetAddressOf()).Assert();
        right.CopyTo(rightUnknown.GetAddressOf()).Assert();

        return leftUnknown.Get() == rightUnknown.Get();
    }
}