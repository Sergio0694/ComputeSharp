using System.Linq;
using Microsoft.Toolkit.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Extensions;

/// <summary>
/// A helper class for testing <see cref="Gpu"/> APIs.
/// </summary>
public static class DeviceExtensions
{
    /// <summary>
    /// Gets a <see cref="GraphicsDevice"/> instance matching a specified type.
    /// </summary>
    /// <param name="type">The device to retrieve.</param>
    /// <returns>The device of the requested type.</returns>
    /// <exception cref="AssertInconclusiveException">Thrown when the requested device is not present.</exception>
    public static GraphicsDevice Get(this Device type)
    {
        GraphicsDevice? device = type switch
        {
            Device.Discrete => Gpu.QueryDevices(info => info.IsHardwareAccelerated).FirstOrDefault(),
            Device.Warp => Gpu.QueryDevices(info => !info.IsHardwareAccelerated).First(),
            _ => ThrowHelper.ThrowArgumentException<GraphicsDevice>("Invalid device")
        };

        if (device is null)
        {
            Assert.Inconclusive();
        }

        return device!;
    }
}

/// <summary>
/// Indicates a specific compute device.
/// </summary>
public enum Device
{
    /// <summary>
    /// A discrete device.
    /// </summary>
    Discrete,

    /// <summary>
    /// A WARP device (software fallback).
    /// </summary>
    Warp
}
