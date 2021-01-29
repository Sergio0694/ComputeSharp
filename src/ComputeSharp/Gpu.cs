using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ComputeSharp.Graphics.Helpers;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that acts as an entry-point for all the library APIs, exposing the available GPU devices.
    /// </summary>
    public static class Gpu
    {
        /// <summary>
        /// The <see cref="Lazy{T}"/> instance used to produce the default device for <see cref="Default"/>.
        /// </summary>
        private static readonly Lazy<GraphicsDevice> DefaultFactory = new(DeviceHelper.GetDefaultDevice);

        /// <summary>
        /// Gets whether or not the <see cref="Gpu"/> APIs can be used on the
        /// current machine (ie. if there is at least a supported GPU device).
        /// </summary>
        public static bool IsSupported => DeviceHelper.IsDefaultDeviceAvailable();

        /// <summary>
        /// Gets the default <see cref="GraphicsDevice"/> instance for the current machine.
        /// This instance cannot be manually disposed - attempting to do so is safe and it will
        /// not cause an exception, but it will simply do nothing and not dispose the device.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown when a default device is not available.</exception>
        /// <remarks>Make sure to check <see cref="IsSupported"/> before accessing this property.</remarks>
        public static GraphicsDevice Default => DefaultFactory.Value;

        /// <summary>
        /// Enumerates all the currently available DX12.0 devices. Note that creating a device is a relatively
        /// expensive operation, so consider using <see cref="QueryDevices"/> to be able to filter the available
        /// devices before creating them, to reduce the system overhead and avoiding creating unnecessary devices.
        /// </summary>
        /// <returns>A sequence of <see cref="GraphicsDevice"/> instances.</returns>
        [Pure]
        public static IEnumerable<GraphicsDevice> EnumerateDevices()
        {
            return new DeviceHelper.DeviceQuery(static _ => true);
        }

        /// <summary>
        /// Executes a query on the currently available DX12.0 devices matching a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to use to select the devices to create.</param>
        /// <returns>A sequence of <see cref="GraphicsDevice"/> instances matching <paramref name="predicate"/>.</returns>
        [Pure]
        public static IEnumerable<GraphicsDevice> QueryDevices(Predicate<GraphicsDeviceInfo> predicate)
        {
            return new DeviceHelper.DeviceQuery(predicate);
        }
    }
}
