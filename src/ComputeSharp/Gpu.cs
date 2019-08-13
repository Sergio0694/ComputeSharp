using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ComputeSharp.Graphics;
using SharpDX.DXGI;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that acts as an entry-point for all the library APIs, exposing the available GPU devices
    /// </summary>
    public static class Gpu
    {
        private static GraphicsDevice _Default;

        /// <summary>
        /// Gets the default <see cref="GraphicsDevice"/> instance for the current machine
        /// </summary>
        public static GraphicsDevice Default => _Default ??= new GraphicsDevice();

        /// <summary>
        /// Gets a collection of all the supported graphics devices on the current machine
        /// </summary>
        /// <returns>The collection of <see cref="GraphicsDevice"/> objects with support for DX12.1</returns>
        [Pure]
        public static IReadOnlyList<GraphicsDevice> QueryAll()
        {
            using Factory factory = new Factory1();
            List<GraphicsDevice> devices = new List<GraphicsDevice>();

            foreach (Adapter adapter in factory.Adapters)
                if (GraphicsDevice.TryGetFromAdapter(adapter, out GraphicsDevice? device))
                    devices.Add(device!);

            return devices;
        }
    }
}
