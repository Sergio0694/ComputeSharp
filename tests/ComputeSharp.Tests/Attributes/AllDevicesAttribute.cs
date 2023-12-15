using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialTestMethodAttribute"/> targeting all supported devices.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class AllDevicesAttribute : Attribute;