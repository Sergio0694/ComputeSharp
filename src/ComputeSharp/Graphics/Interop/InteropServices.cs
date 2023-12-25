using System;
using System.ComponentModel;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Resources;

namespace ComputeSharp.Interop;

/// <summary>
/// Provides methods to interoperate with the native APIs and the managed types in this library.
/// <para>
/// None of the APIs in <see cref="InteropServices"/> perform input validation, and it is responsibility
/// of consumers to ensure the input arguments are in a correct state to be used (eg. not disposed).
/// </para>
/// <para>
/// Manually interfering with the underlying COM objects for any of these types can result in issues if
/// the operations are not done correctly, which can prevent other APIs from functioning as expected.
/// Consumers should ensure the executed operations do not result in any errors. Furthermore, even when
/// the reference count to the returned COM object is incremented, consumers should ensure that the owning
/// objects will remain alive as long as the returned pointers are in use, to avoid unexpected issues. This
/// is because the lifecycle of certain COM objects (eg. resources) is delegated to an internal allocator
/// that relies on resources being disposed as soon as the relative allocation object is disposed.
/// </para>
/// </summary>
public static unsafe class InteropServices
{
    /// <summary>
    /// Gets the underlying COM object for a given device, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <param name="device">The input <see cref="GraphicsDevice"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the device interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <exception cref="ObjectDisposedException">The <paramref name="device"/> instance has been disposed.</exception>
    /// <exception cref="Win32Exception">Thrown if the <c>IUnknown::QueryInterface</c> call doesn't return <c>S_OK</c>.</exception>
    public static void GetID3D12Device(GraphicsDevice device, Guid* riid, void** ppvObject)
    {
        using ReferenceTracker.Lease _0 = device.GetReferenceTracker().GetLease();

        device.D3D12Device->QueryInterface(riid, ppvObject).Assert();
    }

    /// <summary>
    /// Gets the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    /// <param name="buffer">The input <see cref="Buffer{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <exception cref="ObjectDisposedException">The <paramref name="buffer"/> instance has been disposed.</exception>
    /// <exception cref="Win32Exception">Thrown if the <c>IUnknown::QueryInterface</c> call doesn't return <c>S_OK</c>.</exception>
    public static void GetID3D12Resource<T>(Buffer<T> buffer, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = buffer.GetReferenceTracker().GetLease();

        buffer.D3D12Resource->QueryInterface(riid, ppvObject).Assert();
    }

    /// <summary>
    /// Gets the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="Texture1D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    /// <exception cref="Win32Exception">Thrown if the <c>IUnknown::QueryInterface</c> call doesn't return <c>S_OK</c>.</exception>
    public static void GetID3D12Resource<T>(Texture1D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        texture.D3D12Resource->QueryInterface(riid, ppvObject).Assert();
    }

    /// <summary>
    /// Gets the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="Texture2D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    /// <exception cref="Win32Exception">Thrown if the <c>IUnknown::QueryInterface</c> call doesn't return <c>S_OK</c>.</exception>
    public static void GetID3D12Resource<T>(Texture2D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        texture.D3D12Resource->QueryInterface(riid, ppvObject).Assert();
    }

    /// <summary>
    /// Gets the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="Texture3D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    /// <exception cref="Win32Exception">Thrown if the <c>IUnknown::QueryInterface</c> call doesn't return <c>S_OK</c>.</exception>
    public static void GetID3D12Resource<T>(Texture3D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        texture.D3D12Resource->QueryInterface(riid, ppvObject).Assert();
    }

    /// <summary>
    /// Gets the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    /// <param name="buffer">The input <see cref="TransferBuffer{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <exception cref="ObjectDisposedException">The <paramref name="buffer"/> instance has been disposed.</exception>
    /// <exception cref="Win32Exception">Thrown if the <c>IUnknown::QueryInterface</c> call doesn't return <c>S_OK</c>.</exception>
    public static void GetID3D12Resource<T>(TransferBuffer<T> buffer, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = buffer.GetReferenceTracker().GetLease();

        buffer.D3D12Resource->QueryInterface(riid, ppvObject).Assert();
    }

    /// <summary>
    /// Gets the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="TransferTexture2D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    /// <exception cref="Win32Exception">Thrown if the <c>IUnknown::QueryInterface</c> call doesn't return <c>S_OK</c>.</exception>
    public static void GetID3D12Resource<T>(TransferTexture2D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        texture.D3D12Resource->QueryInterface(riid, ppvObject).Assert();
    }

    /// <summary>
    /// Gets the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="TransferTexture3D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    /// <exception cref="Win32Exception">Thrown if the <c>IUnknown::QueryInterface</c> call doesn't return <c>S_OK</c>.</exception>
    public static void GetID3D12Resource<T>(TransferTexture3D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        texture.D3D12Resource->QueryInterface(riid, ppvObject).Assert();
    }

    /// <summary>
    /// Tries to get the underlying COM object for a given device, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <param name="device">The input <see cref="GraphicsDevice"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the device interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <returns>
    /// <c>S_OK</c> if the interface is supported, and <c>E_NOINTERFACE</c> otherwise.
    /// If <paramref name="ppvObject"/> (the address) is <see langword="null"/>, then this method returns <c>E_POINTER</c>.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The <paramref name="device"/> instance has been disposed.</exception>
    public static int TryGetID3D12Device(GraphicsDevice device, Guid* riid, void** ppvObject)
    {
        using ReferenceTracker.Lease _0 = device.GetReferenceTracker().GetLease();

        return device.D3D12Device->QueryInterface(riid, ppvObject);
    }

    /// <summary>
    /// Tries to get the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    /// <param name="buffer">The input <see cref="Buffer{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <returns>
    /// <c>S_OK</c> if the interface is supported, and <c>E_NOINTERFACE</c> otherwise.
    /// If <paramref name="ppvObject"/> (the address) is <see langword="null"/>, then this method returns <c>E_POINTER</c>.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The <paramref name="buffer"/> instance has been disposed.</exception>
    public static int TryGetID3D12Resource<T>(Buffer<T> buffer, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = buffer.GetReferenceTracker().GetLease();

        return buffer.D3D12Resource->QueryInterface(riid, ppvObject);
    }

    /// <summary>
    /// Tries to get the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="Texture1D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <returns>
    /// <c>S_OK</c> if the interface is supported, and <c>E_NOINTERFACE</c> otherwise.
    /// If <paramref name="ppvObject"/> (the address) is <see langword="null"/>, then this method returns <c>E_POINTER</c>.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    public static int TryGetID3D12Resource<T>(Texture1D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        return texture.D3D12Resource->QueryInterface(riid, ppvObject);
    }

    /// <summary>
    /// Tries to get the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="Texture2D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <returns>
    /// <c>S_OK</c> if the interface is supported, and <c>E_NOINTERFACE</c> otherwise.
    /// If <paramref name="ppvObject"/> (the address) is <see langword="null"/>, then this method returns <c>E_POINTER</c>.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    public static int TryGetID3D12Resource<T>(Texture2D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        return texture.D3D12Resource->QueryInterface(riid, ppvObject);
    }

    /// <summary>
    /// Tries to get the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="Texture3D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <returns>
    /// <c>S_OK</c> if the interface is supported, and <c>E_NOINTERFACE</c> otherwise.
    /// If <paramref name="ppvObject"/> (the address) is <see langword="null"/>, then this method returns <c>E_POINTER</c>.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    public static int TryGetID3D12Resource<T>(Texture3D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        return texture.D3D12Resource->QueryInterface(riid, ppvObject);
    }

    /// <summary>
    /// Tries to get the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the buffer.</typeparam>
    /// <param name="buffer">The input <see cref="TransferBuffer{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <returns>
    /// <c>S_OK</c> if the interface is supported, and <c>E_NOINTERFACE</c> otherwise.
    /// If <paramref name="ppvObject"/> (the address) is <see langword="null"/>, then this method returns <c>E_POINTER</c>.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The <paramref name="buffer"/> instance has been disposed.</exception>
    public static int TryGetID3D12Resource<T>(TransferBuffer<T> buffer, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = buffer.GetReferenceTracker().GetLease();

        return buffer.D3D12Resource->QueryInterface(riid, ppvObject);
    }

    /// <summary>
    /// Tries to get the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="TransferTexture2D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <returns>
    /// <c>S_OK</c> if the interface is supported, and <c>E_NOINTERFACE</c> otherwise.
    /// If <paramref name="ppvObject"/> (the address) is <see langword="null"/>, then this method returns <c>E_POINTER</c>.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    public static int TryGetID3D12Resource<T>(TransferTexture2D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        return texture.D3D12Resource->QueryInterface(riid, ppvObject);
    }

    /// <summary>
    /// Tries to get the underlying COM object for a given resource, as a specified interface. This method invokes
    /// <see href="https://docs.microsoft.com/windows/win32/api/unknwn/nf-unknwn-iunknown-queryinterface(refiid_void)">IUnknown::QueryInterface</see>.
    /// </summary>
    /// <typeparam name="T">The type of items stored on the texture.</typeparam>
    /// <param name="texture">The input <see cref="TransferTexture3D{T}"/> instance in use.</param>
    /// <param name="riid">A reference to the interface identifier (IID) of the resource interface being queried for.</param>
    /// <param name="ppvObject">The address of a pointer to an interface with the IID specified in <paramref name="riid"/>.</param>
    /// <returns>
    /// <c>S_OK</c> if the interface is supported, and <c>E_NOINTERFACE</c> otherwise.
    /// If <paramref name="ppvObject"/> (the address) is <see langword="null"/>, then this method returns <c>E_POINTER</c>.
    /// </returns>
    /// <exception cref="ObjectDisposedException">The <paramref name="texture"/> instance has been disposed.</exception>
    public static int TryGetID3D12Resource<T>(TransferTexture3D<T> texture, Guid* riid, void** ppvObject)
        where T : unmanaged
    {
        using ReferenceTracker.Lease _0 = texture.GetReferenceTracker().GetLease();

        return texture.D3D12Resource->QueryInterface(riid, ppvObject);
    }
}