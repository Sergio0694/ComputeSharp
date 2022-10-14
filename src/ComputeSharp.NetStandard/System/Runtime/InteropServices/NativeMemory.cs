namespace System.Runtime.InteropServices;

/// <summary>
/// This class contains methods that are mainly used to manage native memory.
/// </summary>
internal static unsafe class NativeMemory
{
    /// <summary>
    /// Allocates a block of memory of the specified size, in bytes.
    /// </summary>
    /// <param name="byteCount">The size, in bytes, of the block to allocate.</param>
    /// <returns>A pointer to the allocated block of memory.</returns>
    /// <exception cref="OutOfMemoryException">Thrown when allocating <paramref name="byteCount"/> of memory failed.</exception>
    /// <remarks>
    /// <para>This method allows <paramref name="byteCount" /> to be <c>0</c> and will return a valid pointer that should not be dereferenced and that should be passed to free to avoid memory leaks.</para>
    /// <para>This method is a thin wrapper over the C <c>malloc</c> API.</para>
    /// </remarks>
    public static void* Alloc(nuint byteCount)
    {
        return (void*)Marshal.AllocHGlobal(checked((int)byteCount));
    }

    /// <summary>
    /// Frees a block of memory.
    /// </summary>
    /// <param name="ptr">A pointer to the block of memory that should be freed.</param>
    /// <remarks>
    /// <para>This method does nothing if <paramref name="ptr" /> is <see langword="null"/>.</para>
    /// <para>This method is a thin wrapper over the C <c>free</c> API.</para>
    /// </remarks>
    public static void Free(void* ptr)
    {
        Marshal.FreeHGlobal((IntPtr)ptr);
    }
}