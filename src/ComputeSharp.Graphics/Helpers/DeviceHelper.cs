using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Vortice.Direct3D;
using Vortice.Direct3D12;
using Vortice.DXGI;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A <see langword="class"/> with methods to inspect the available devices on the current machine
    /// </summary>
    internal static class DeviceHelper
    {
        /// <summary>
        /// Gets a collection of all the supported devices on the current machine
        /// </summary>
        /// <returns>The collection of <see cref="ID3D12Device"/> objects with support for DX12.1</returns>
        [Pure]
        public static IReadOnlyList<(ID3D12Device, AdapterDescription)> QueryAllSupportedDevices()
        {
            if (!DXGI.CreateDXGIFactory1(out IDXGIFactory1 factory).Success) return new (ID3D12Device, AdapterDescription)[0];
            try
            {
                List<(ID3D12Device, AdapterDescription)> devices = new List<(ID3D12Device, AdapterDescription)>();

                int i = 0;

                while (factory.EnumAdapters1(i++, out IDXGIAdapter1? adapter).Success && !(adapter is null))
                {
                    if (adapter.Description.DedicatedVideoMemory == 0) continue;

                    if (D3D12.D3D12CreateDevice(adapter, FeatureLevel.Level_12_0, out ID3D12Device device).Success)
                    {
                        devices.Add((device, adapter.Description));
                    }
                }

                return devices;
            }
            finally
            {
                // Explicit using statement
                factory?.Dispose();
            }
        }
    }
}
