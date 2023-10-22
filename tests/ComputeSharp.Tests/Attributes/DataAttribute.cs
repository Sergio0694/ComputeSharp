using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialTestMethodAttribute"/> adding arbitrary input data.
/// </summary>
/// <param name="data">The input data.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class DataAttribute(params object[] data) : Attribute
{
    /// <summary>
    /// Gets the input data for the current attribute.
    /// </summary>
    public object[] Data => data;
}