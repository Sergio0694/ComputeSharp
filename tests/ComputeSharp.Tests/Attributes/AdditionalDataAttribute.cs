using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialTestMethodAttribute"/> adding additional arbitrary input data.
/// </summary>
/// <param name="data">The additional input data.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class AdditionalDataAttribute(params object[] data) : Attribute
{
    /// <summary>
    /// Gets the additional input data for the current attribute.
    /// </summary>
    public object[] Data => data;
}