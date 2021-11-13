using System;
using ComputeSharp.Tests.Extensions;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialAttribute"/> adding a specific <see cref="Extensions.Device"/> input.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class DeviceAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="DeviceAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The target device type to use..</param>
    public DeviceAttribute(Device device)
    {
        Device = device;
    }

    /// <summary>
    /// Gets the <see cref="Extensions.Device"/> target to use.
    /// </summary>
    public Device Device { get; }
}
