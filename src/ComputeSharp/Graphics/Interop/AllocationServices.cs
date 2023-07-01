using System;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Interop;

/// <summary>
/// Provides methods to configure the global allocator factory instance that will be used by all <see cref="GraphicsDevice"/> objects.
/// </summary>
public static unsafe class AllocationServices
{
    /// <summary>
    /// Configures the global <c>ID3D12MemoryAllocatorFactory</c> instance to use for the current process.
    /// </summary>
    /// <param name="allocatorFactory">The input <see cref="ICustomQueryInterface"/> instance to use (implementing <c>ID3D12MemoryAllocatorFactory</c>).</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="allocatorFactory"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the allocator factory cannot be configured at this time.</exception>
    /// <remarks>
    /// The allocatory factory can only be configured once, and before any <see cref="GraphicsDevice"/> instance has been created.
    /// </remarks>
    public static void ConfigureAllocatorFactory(ICustomQueryInterface allocatorFactory)
    {
        default(ArgumentNullException).ThrowIfNull(allocatorFactory);

        using ComPtr<ID3D12MemoryAllocatorFactory> allocatorFactoryAbi = default;

        // Unwrap the underlying ID3D12MemoryAllocatorFactory object
        CustomQueryInterfaceResult result = allocatorFactory.GetInterface(
            iid: ref *(Guid*)Windows.__uuidof<ID3D12MemoryAllocatorFactory>(),
            ppv: out *(IntPtr*)allocatorFactoryAbi.GetAddressOf());

        default(InvalidOperationException).ThrowIf(result != CustomQueryInterfaceResult.Handled, "Failed to retrieve the underlying ID3D12MemoryAllocatorFactory object.");

        DeviceHelper.ConfigureAllocatorFactory(allocatorFactoryAbi.Get());
    }
}