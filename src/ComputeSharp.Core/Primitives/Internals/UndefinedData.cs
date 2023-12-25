#if !SOURCE_GENERATOR
using System.Runtime.CompilerServices;
#endif

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
    public static readonly unsafe void* Memory =
#if SOURCE_GENERATOR
        null;
#else
        (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UndefinedData), sizeof(Double4));
#endif
}