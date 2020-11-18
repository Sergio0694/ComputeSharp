using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ComputeSharp.Graphics;
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
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown when a default device is not available.</exception>
        /// <remarks>Make sure to check <see cref="IsSupported"/> before accessing this property.</remarks>
        public static GraphicsDevice Default => DefaultFactory.Value;

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
