using System.Collections.Generic;
using System.Diagnostics.Contracts;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D12.Device;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A <see langword="class"/> with methods to inspect the available devices on the current machine
    /// </summary>
    public static class DeviceHelper
    {
        /// <summary>
        /// Gets a collection of all the supported devices on the current machine
        /// </summary>
        /// <returns>The collection of <see cref="Device"/> objects with support for DX12.1</returns>
        [Pure]
        public static IReadOnlyList<Device> QueryAllSupportedDevices()
        {
            using Factory factory = new Factory1();
            List<Device> devices = new List<Device>();

            foreach (Adapter adapter in factory.Adapters)
            {
                if (adapter.Description.DedicatedVideoMemory == 0) continue;
                try
                {
                    Device device = new Device(adapter, GraphicsDevice.FeatureLevel);
                    devices.Add(device);
                }
                catch
                {
                    // Unsupported device
                }
            }

            return devices;
        }
    }
}
