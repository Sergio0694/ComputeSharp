using System;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop.Allocation;

namespace ComputeSharp.Interop;

/// <summary>
/// Provides methods to configure the global allocator factory instance that will be used by all <see cref="GraphicsDevice"/> objects.
/// </summary>
public static unsafe class AllocationServices
{
    /// <summary>
    /// Configures the global <c>ID3D12MemoryAllocatorFactory</c> instance to use for the current process.
    /// </summary>
    /// <param name="allocatorFactory">The input <c>ID3D12MemoryAllocatorFactory</c> instance to use.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="allocatorFactory"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the allocator factory cannot be configured at this time.</exception>
    /// <remarks>
    /// The allocatory factory can only be configured once, and before any <see cref="GraphicsDevice"/> instance has been created.
    /// </remarks>
    public static void ConfigureAllocatorFactory(void* allocatorFactory)
    {
        default(ArgumentNullException).ThrowIfNull(allocatorFactory);

        DeviceHelper.ConfigureAllocatorFactory((ID3D12MemoryAllocatorFactory*)allocatorFactory);
    }
}