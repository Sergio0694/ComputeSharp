using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Extensions;

/// <summary>
/// A helper class for testing <see cref="GraphicsDevice"/> APIs.
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
            Device.Discrete => GraphicsDevice.QueryDevices(info => info.IsHardwareAccelerated).FirstOrDefault(),
            Device.Warp => GraphicsDevice.QueryDevices(info => !info.IsHardwareAccelerated).FirstOrDefault(),
            _ => throw new ArgumentException("Invalid device type.", nameof(type))
        };

        if (device is null)
        {
            // If the device is null, fail only if the device was WARP (as it should always be available).
            // If a dedicated device was requested instead, just report the test as inconclusive (no GPU).
            switch (type)
            {
                case Device.Discrete: Assert.Inconclusive(); break;
                case Device.Warp: Assert.Fail(); break;
            }
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