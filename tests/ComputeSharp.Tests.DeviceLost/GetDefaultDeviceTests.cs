using System;
using System.Threading.Tasks;
using ComputeSharp.Tests.DeviceLost.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Tests.DeviceLost;

[TestClass]
public class GetDefaultDeviceTests
{
    [TestMethod]
    public async Task GetDefault_VerifyContract()
    {
        GraphicsDevice device1 = GraphicsDevice.GetDefault();

        // The default device is never null (or the previous call would've thrown an exception)
        Assert.IsNotNull(device1);

        using (ComPtr<ID3D12Device> d3D12Device = default)
        {
            GraphicsDeviceHelper.GetD3D12Device(device1, in d3D12Device);

            GraphicsDevice device2 = GraphicsDevice.GetDefault();

            // Assuming the device hasn't been disposed (tests don't run concurrently), calling
            // GraphicsDevice.GetDefault() again will always return the same cached instance.
            Assert.AreSame(device1, device2);

            await GraphicsDeviceHelper.RemoveDeviceAsync(device1);

            // The device is lost but not disposed, so the same instance should be returned yet again
            GraphicsDevice device3 = GraphicsDevice.GetDefault();

            Assert.AreSame(device1, device3);

            // Dispose the device, which should make its actual release logic execute
            device1.Dispose();

            // The device is correctly marked as disposed
            _ = Assert.ThrowsException<ObjectDisposedException>(() => device1.IsDoublePrecisionSupportAvailable());

            // Now there should just be this one last reference left to the native device
            Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(in d3D12Device));
        }

        // Calling GraphicsDevice.GetDefault() after disposing it should always succeed...
        GraphicsDevice device4 = GraphicsDevice.GetDefault();

        // But it will return a new instance (assuming the device has been disposed properly).
        // That is, the caller has to ensure that no other object is keeping the device alive.
        Assert.AreNotSame(device1, device4);

        // The LUID should match though, as the same adapter should be returned
        Assert.AreEqual(device1.Luid, device4.Luid);

        await GraphicsDeviceHelper.RemoveDeviceAsync(device4);

        using (ComPtr<ID3D12Device> d3D12Device = default)
        {
            GraphicsDeviceHelper.GetD3D12Device(device4, in d3D12Device);

            device4.Dispose();

            // The device is correctly marked as disposed
            _ = Assert.ThrowsException<ObjectDisposedException>(() => device4.IsDoublePrecisionSupportAvailable());

            // Sanity check that this is in fact the only reference keeping the device alive
            Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(in d3D12Device));

            unsafe
            {
                HRESULT removalReason = d3D12Device.Get()->GetDeviceRemovedReason();

                // Sanity check that the device is in fact removed
                Assert.AreEqual(DXGI.DXGI_ERROR_DEVICE_REMOVED, (int)removalReason);
            }

            // Calling GraphicsDevice.GetDefault() will now throw, because the native device has not been disposed properly
            _ = Assert.ThrowsException<InvalidOperationException>(GraphicsDevice.GetDefault);
        }
    }

    [TestMethod]
    public void GetDefault_DisposeAndUseNewInstance()
    {
        GraphicsDevice device1 = GraphicsDevice.GetDefault();

        using (ComPtr<ID3D12Device> d3D12Device = default)
        {
            using (device1.AllocateReadOnlyBuffer<float>(128))
            {
            }

            GraphicsDeviceHelper.GetD3D12Device(device1, in d3D12Device);

            device1.Dispose();

            Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(in d3D12Device));
        }

        // Just same test but twice, just for good measure
        using (ComPtr<ID3D12Device> d3D12Device = default)
        {
            GraphicsDevice device2 = GraphicsDevice.GetDefault();

            Assert.AreNotSame(device1, device2);

            using (device2.AllocateReadOnlyBuffer<float>(128))
            {
            }

            GraphicsDeviceHelper.GetD3D12Device(device2, in d3D12Device);

            device2.Dispose();

            Assert.AreEqual(1u, GraphicsDeviceHelper.GetD3D12DeviceRefCount(in d3D12Device));
        }
    }
}