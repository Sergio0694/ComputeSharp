using System;
using System.Collections.Generic;
using ComputeSharp.Graphics.Helpers;

namespace ComputeSharp;

/// <inheritdoc/>
partial class GraphicsDevice
{
    /// <summary>
    /// Gets or creates the default <see cref="GraphicsDevice"/> instance for the current machine.
    /// <para>
    /// The default define is the first device supporting the required feature level that could
    /// be created when enumerating the available adapters in performance order. That is, it is
    /// conceptually equivalent to the first successfully created device from <see cref="EnumerateDevices"/>.
    /// </para>
    /// </summary>
    /// <returns>The default <see cref="GraphicsDevice"/> instance for the current machine.</returns>
    /// <remarks>
    /// The returned <see cref="GraphicsDevice"/> is cached across multiple invocations. In order to
    /// support device lost scenarios (see <see cref="DeviceLost"/>) and due to how the DirectX 12
    /// runtime handles caching of device instances, this method has the following behavior:
    /// <list type="bullet">
    ///   <item>If no device has been created, create the default one, cache it, and return it.</item>
    ///   <item>If a cached device is available, return that.</item>
    ///   <item>If the returned device is disposed, the cache is reset.</item>
    ///   <item>
    ///     If <see cref="GetDefault"/> is called again after that, the returned device might map to a different adapter.
    ///     This would be the case if the first device was lost due to the adapter being removed from the system.
    ///   </item>
    /// </list>
    /// There is one additional caveat to the list above: if <see cref="GetDefault"/> is called, then that device is lost, then
    /// the returned <see cref="GraphicsDevice"/> instance is disposed incorrectly (eg. with some existing resources not being
    /// disposed, which would keep the underlying device object alive), calling <see cref="GetDefault"/> again will result in
    /// an exception instead of a different device being returned. That is, if enumerating adapters and trying to create a device
    /// fails due to device lost (as opposed to other valid failures when trying to normally create a device), an exception will be
    /// thrown instead of just skipping that adapter, because that would mean a device had been created but incorrectly disposed.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown if the target device was lost and incorrectly disposed (see remarks).</exception>
    /// <exception cref="NotSupportedException">Thrown if no adapter could be created (this should never be the case).</exception>
    public static GraphicsDevice GetDefault()
    {
        return DeviceHelper.GetDefaultDeviceFromCacheOrCreateInstance();
    }

    /// <summary>
    /// Enumerates all the currently available devices supporting the minimum necessary feature level.
    /// Physical devices and integrated GPUs will be enumerated first, and the WARP device will always be last.
    /// </summary>
    /// <returns>A sequence of <see cref="GraphicsDevice"/> instances.</returns>
    /// <remarks>
    /// Creating a device is a relatively expensive operation, so consider using <see cref="QueryDevices"/> to be
    /// able to filter the existing adapters before creating a device from them, to reduce the system overhead.
    /// </remarks>
    public static IEnumerable<GraphicsDevice> EnumerateDevices()
    {
        return new DeviceHelper.DeviceQuery(null);
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
    public static IEnumerable<GraphicsDevice> QueryDevices(Predicate<GraphicsDeviceInfo> predicate)
    {
        default(ArgumentNullException).ThrowIfNull(predicate);

        return new DeviceHelper.DeviceQuery(predicate);
    }
}