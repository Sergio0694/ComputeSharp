using System;
using System.Threading.Tasks;
using ComputeSharp.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Tests.DeviceLost;

[TestClass]
[TestCategory("GetDefaultDevice")]
public class GetDefaultDeviceTests
{
    [TestMethod]
    public async Task GetDefault_VerifyContract()
    {
        GraphicsDevice device1 = GraphicsDevice.GetDefault();

        // The default device is never null (or the previous call would've thrown an exception)
        Assert.IsNotNull(device1);

        GraphicsDevice device2 = GraphicsDevice.GetDefault();

        // Assuming the device hasn't been disposed (tests don't run concurrently), calling
        // GraphicsDevice.GetDefault() again will always return the same cached instance.
        Assert.AreSame(device1, device2);

        await DeviceLostTests.RemoveDeviceAsync(device1);

        // The device is lost but not disposed, so the same instance should be returned yet again
        GraphicsDevice device3 = GraphicsDevice.GetDefault();

        Assert.AreSame(device1, device3);

        device1.Dispose();

        // Calling GraphicsDevice.GetDefault() after disposing it should always succeed...
        GraphicsDevice device4 = GraphicsDevice.GetDefault();

        // But it will return a new instance (assuming the device has been disposed properly).
        // That is, the caller has to ensure that no other object is keeping the device alive.
        Assert.AreNotSame(device1, device4);

        // The LUID should match though, as the same adapter should be returned
        Assert.AreEqual(device1.Luid, device4.Luid);

        unsafe void GetDefault_VerifyContract_Part2()
        {
            using ComPtr<ID3D12Device> d3D12Device = default;

            // Increment the reference count on the internal device, causing it to remain alive
            InteropServices.GetID3D12Device(device4, Windows.__uuidof<ID3D12Device>(), (void**)d3D12Device.GetAddressOf());

            device4.Dispose();

            _ = d3D12Device.Get()->AddRef();

            uint refCount = d3D12Device.Get()->Release();

            // Sanity check that this is in fact the only reference keeping the device alive
            Assert.AreEqual(refCount, 1u);

            HRESULT removalReason = d3D12Device.Get()->GetDeviceRemovedReason();

            // Sanity check that the device is in fact removed
            Assert.AreEqual((int)removalReason, DXGI.DXGI_ERROR_DEVICE_REMOVED);

            // Calling GraphicsDevice.GetDefault() will now throw, because the native device has not been disposed properly
            Assert.ThrowsException<InvalidOperationException>(() => GraphicsDevice.GetDefault());
        }

        await DeviceLostTests.RemoveDeviceAsync(device4);

        GetDefault_VerifyContract_Part2();
    }
}
