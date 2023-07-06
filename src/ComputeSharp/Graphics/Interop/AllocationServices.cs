using System;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Interop.Allocation;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Interop;

/// <summary>
/// Provides methods to configure the global allocator factory instance that will be used by all <see cref="GraphicsDevice"/> objects.
/// </summary>
/// <remarks>
/// <para>
/// By default, all resources will be created as committed resources. The <see cref="AllocationServices"/> APIs make it possible to change
/// this behavior, and instead use a pluggable memory allocator implementing any desired custom logic to manage how resources are created.
/// </para>
/// <para>
/// Memory allocators are created through a factory object that can be configured via <see cref="ConfigureAllocatorFactory"/>. This object
/// needs to implement the <c>ID3D12MemoryAllocatorFactory</c> COM interface, which has the following definition:
/// <code>
/// [uuid(CC1E74A7-786D-40F4-8AE2-F8B7A255587E)]
/// interface ID3D12MemoryAllocatorFactory : IUnknown
/// {
///     HRESULT CreateAllocator(
///         [in] const ID3D12Device* device,
///         [in] const IDXGIAdapter* adapter,
///         [out] ID3D12MemoryAllocator allocator);
/// };
/// </code>
/// </para>
/// <para>
/// This is a simple, possibly stateless factory, responsible for creating per-device memory allocators. Those use the following interface:
/// <code>
/// [uuid(2D5E55D2-9244-431F-868E-0D90AAB6E575)]
/// interface ID3D12MemoryAllocator : IUnknown
/// {
///     HRESULT AllocateResource(
///         [in] const D3D12_RESOURCE_DESC* resourceDescription,
///         const D3D12_HEAP_TYPE heapType,
///         const D3D12_RESOURCE_STATES resourceState,
///         const BOOL clearAllocation,
///         [out] ID3D12Allocation** allocation);
/// };
/// </code>
/// </para>
/// <para>
/// Finally, each returned allocation object wraps a given native resource, and uses the following interface:
/// <code>
/// [uuid(D42D5782-2DE7-4539-A817-482E3AA01E2E)]
/// interface ID3D12Allocation : IUnknown
/// {
///     HRESULT GetD3D12Resource([out] ID3D12Resource** resource);
/// };
/// </code>
/// </para>
/// <para>
/// The implementation of these types can be done in any language, as long as the COM interfaces defined above are correctly supported.
/// </para>
/// </remarks>
public static class AllocationServices
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
    public static unsafe void ConfigureAllocatorFactory(ICustomQueryInterface allocatorFactory)
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