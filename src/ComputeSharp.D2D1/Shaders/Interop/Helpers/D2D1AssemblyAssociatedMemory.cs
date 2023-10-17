#if SOURCE_GENERATOR
using System.Runtime.InteropServices;
#else
using System.Runtime.CompilerServices;
#endif

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A helper type to allocate memory that's associated with the current assembly.
/// </summary>
internal static unsafe class D2D1AssemblyAssociatedMemory
{
    /// <summary>
    /// Allocate memory that is associated with the current assembly, and it will be freed if and when it is unloaded.
    /// </summary>
    /// <param name="size">The amount of memory in bytes to allocate.</param>
    /// <returns>The allocated memory.</returns>
    public static unsafe void* Allocate(int size)
    {
        // This is a size optimization specifically for AOT scenarios. The RuntimeHelpers.AllocateTypeAssociatedMemory API
        // requires a Type instance being passed to it, and doing typeof(T) ends up rooting a fair amount of extra metadata
        // and code for those types. This is especially the case for value types, as the reflection stack assumea that the
        // type T might be boxed, and therefore allocates code to handle equality on boxed instances, which also causes
        // implemented interfaces (eg. IEquatable<T>) to be rooted, if present. This method works around this issue by
        // only invoking this API on an empty type, which minimizes the codegen increase. This is still safe, as types are
        // not unloaded one by one. Rather, the entire assembly they belong to is unloaded. So given all uses of this API
        // are from within this same assembly, there is no functional difference than associating memory to the various types.
#if SOURCE_GENERATOR
        return (void*)Marshal.AllocHGlobal(size);
#else
        return (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(D2D1AssemblyAssociatedMemory), size);
#endif
    }
}