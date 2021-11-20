using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialTestMethodAttribute"/> targeting an additional specific resource type.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class AdditionalResourceAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="AdditionalResourceAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="type">The additional resource type.</param>
    public AdditionalResourceAttribute(Type type)
    {
        Type = type;
    }

    /// <summary>
    /// Gets the additional resource type for the current attribute.
    /// </summary>
    public Type Type { get; }
}
