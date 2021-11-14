using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialAttribute"/> adding additional arbitrary input data.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class AdditionalDataAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="AdditionalDataAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="data">The additional input data.</param>
    public AdditionalDataAttribute(params object[] data)
    {
        Data = data;
    }

    /// <summary>
    /// Gets the additional input data for the current attribute.
    /// </summary>
    public object[] Data { get; }
}
