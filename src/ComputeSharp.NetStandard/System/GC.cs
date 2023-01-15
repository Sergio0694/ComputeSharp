namespace ComputeSharp.NetStandard;

/// <summary>
/// A polyfill type that mirrors some methods from <see cref="global::System.GC"/> on .NET 6.
/// </summary>
internal static class GC
{
    /// <summary>
    /// Allocate an array while skipping zero-initialization if possible.
    /// </summary>
    /// <typeparam name="T">Specifies the type of the array element.</typeparam>
    /// <param name="length">Specifies the length of the array.</param>
    /// <param name="pinned">Specifies whether the allocated array must be pinned.</param>
    /// <remarks>
    /// If pinned is set to true, <typeparamref name="T"/> must not be a reference type or a type that contains object references.
    /// </remarks>
    public static T[] AllocateUninitializedArray<T>(int length, bool pinned = false)
    {
        return new T[length];
    }
}