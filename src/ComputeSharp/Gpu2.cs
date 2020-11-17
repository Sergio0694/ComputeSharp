using System;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Helpers;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that acts as an entry-point for all the library APIs, exposing the available GPU devices.
    /// </summary>
    public static class Gpu2
    {
        /// <summary>
        /// The <see cref="Lazy{T}"/> instance used to produce the default device for <see cref="Default"/>.
        /// </summary>
        private static readonly Lazy<GraphicsDevice2> DefaultFactory = new(DeviceHelper2.GetDefaultDevice);

        /// <summary>
        /// Gets whether or not the <see cref="Gpu"/> APIs can be used on the
        /// current machine (ie. if there is at least a supported GPU device).
        /// </summary>
        public static bool IsSupported => DeviceHelper2.IsDefaultDeviceAvailable();

        /// <summary>
        /// Gets the default <see cref="GraphicsDevice2"/> instance for the current machine.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown when a default device is not available.</exception>
        /// <remarks>Make sure to check <see cref="IsSupported"/> before accessing this property.</remarks>
        public static GraphicsDevice2 Default => DefaultFactory.Value;
    }
}
