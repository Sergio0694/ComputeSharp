using System;
using ComputeSharp.Tests.Extensions;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialTestMethodAttribute"/> adding a specific <see cref="Extensions.Device"/> input.
/// </summary>
/// <param name="device">The target device type to use..</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class DeviceAttribute(Device device) : Attribute
{
    /// <summary>
    /// Gets the <see cref="Extensions.Device"/> target to use.
    /// </summary>
    public Device Device => device;
}