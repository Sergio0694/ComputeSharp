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
        /// Gets the default <see cref="GraphicsDevice"/> instance for the current machine.
        /// This instance cannot be manually disposed - attempting to do so is safe and it will
        /// not cause an exception, but it will simply do nothing and not dispose the device.
        /// </summary>
        /// <remarks>
        /// This device will always be available, even when there isn't a compatible physical GPU
        /// or integrated GPU in the system in use. In that case, the WARP device will be used.
        /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3darticles/directx-warp"/>.
        /// </remarks>
        public static GraphicsDevice Default => DeviceHelper.DefaultFactory.Value;

        /// <summary>
        /// Enumerates all the currently available devices supporting the minimum necessary feature level.
        /// Physical devices and integrated GPUs will be enumerated first, and the WARP device will always be last.
        /// </summary>
        /// <returns>A sequence of <see cref="GraphicsDevice"/> instances.</returns>
        /// <remarks>
        /// Creating a device is a relatively expensive operation, so consider using <see cref="QueryDevices"/> to be
        /// able to filter the existing adapters before creating a device from them, to reduce the system overhead.
        /// </remarks>
        [Pure]
        public static IEnumerable<GraphicsDevice> EnumerateDevices()
        {
            return new DeviceHelper.DeviceQuery(static _ => true);
        }

        /// <summary>
        /// Executes a query on the currently available devices matching a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to use to select the devices to create.</param>
        /// <returns>A sequence of <see cref="GraphicsDevice"/> instances matching <paramref name="predicate"/>.</returns>
        /// <remarks>
        /// Note that only devices matching the minimum necessary feature level will actually be instantiated and returned.
        /// This means that <paramref name="predicate"/> might not actually be used to match against all existing adapters on
        /// the current system, if any of them doesn't meet the minimum criteria, and that additional filtering might be done
        /// after the input predicate is invoked, so a match doesn't necessarily guarantee that that device will be returned.
        /// </remarks>
        [Pure]
        public static IEnumerable<GraphicsDevice> QueryDevices(Predicate<GraphicsDeviceInfo> predicate)
        {
            return new DeviceHelper.DeviceQuery(predicate);
        }
    }
}
