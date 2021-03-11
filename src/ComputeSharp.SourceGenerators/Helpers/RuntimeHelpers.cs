using System;

namespace ComputeSharp.SourceGenerators.Helpers
{
    /// <summary>
    /// A polyfill type that mirrors some methods from <see cref="System.Runtime.CompilerServices.RuntimeHelpers"/> on .NET 5.
    /// </summary>
    internal static class RuntimeHelpers
    {
        /// <summary>
        /// Allocates some memory tied to the lifetime of a specific type.
        /// </summary>
        /// <param name="type">The type to associate the memory to.</param>
        /// <param name="size">The size in byte of the memory to allocate.</param>
        /// <returns>A pointer to the allocated memory.</returns>
        public static IntPtr AllocateTypeAssociatedMemory(Type type, int size) => default;
    }
}
