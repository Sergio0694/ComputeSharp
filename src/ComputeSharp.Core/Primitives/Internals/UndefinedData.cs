using System.Runtime.CompilerServices;

#pragma warning disable CS0649

namespace ComputeSharp;

/// <summary>
/// A helper class with shared undefined data for all HLSL primitive types.
/// This reduces allocations and minimizes reflection metadata kept per type.
/// </summary>
internal static class UndefinedData
{
    /// <summary>
    /// The shared memory with undefined data (has size of <see cref="Double4"/>, as it's the maximum needed at once).
    /// </summary>
    /// <remarks>
    /// This field is intentionally never assigned, as the data it holds is undefined by definition. It is also
    /// intentionally not <see langword="readonly"/>: callers can get a writeable reference into this memory (eg.
    /// from a writeable swizzled property), so marking it as readonly would make writing to it undefined behavior.
    /// </remarks>
    [FixedAddressValueType]
    private static Double4 sharedMemory;

    /// <summary>
    /// Gets a pointer to the shared memory with undefined data.
    /// </summary>
    public static unsafe void* Memory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Unsafe.AsPointer(ref sharedMemory);
    }
}