using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Helpers;
using Vortice.Direct3D12;
using Vortice.DXGI;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that acts as an entry-point for all the library APIs, exposing the available GPU devices
    /// </summary>
    public static class Gpu
    {
        /// <summary>
        /// Initializes the <see cref="Devices"/> field and <see cref="IsSupported"/> property
        /// </summary>
        static Gpu()
        {
            Devices = DeviceHelper.QueryAllSupportedDevices();
            IsSupported = Devices.Any();

            if (IsSupported)
            {
                _Default = new GraphicsDevice(Devices[0].Device, Devices[0].Description);
            }
        }

        /// <summary>
        /// The loaded collection of supported devices and descriptions
        /// </summary>
        private static readonly IReadOnlyList<(ID3D12Device Device, AdapterDescription Description)> Devices;

        private static readonly GraphicsDevice? _Default;

        /// <summary>
        /// Gets the default <see cref="GraphicsDevice"/> instance for the current machine
        /// </summary>
        public static GraphicsDevice Default => _Default ?? throw new NotSupportedException("There isn't a supported GPU device on the current machine");

        /// <summary>
        /// Gets whether or not the <see cref="Gpu"/> APIs can be used on the current machine (ie. if there is at least a supported GPU device)
        /// </summary>
        public static bool IsSupported { get; }

        /// <summary>
        /// Enumerates all the available <see cref="GraphicsDevice"/> objects on the current machine
        /// </summary>
        /// <returns>A sequence of supported <see cref="GraphicsDevice"/> objects that can be used to run compute shaders</returns>
        [Pure]
        public static IEnumerable<GraphicsDevice> EnumerateDevices() => Devices.Select(device => new GraphicsDevice(device.Device, device.Description));
    }
}
