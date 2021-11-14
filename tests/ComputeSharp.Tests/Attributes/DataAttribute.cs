using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialAttribute"/> adding arbitrary input data.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class DataAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="DataAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="data">The input data.</param>
    public DataAttribute(params object[] data)
    {
        Data = data;
    }

    /// <summary>
    /// Gets the input data for the current attribute.
    /// </summary>
    public object[] Data { get; }
}
