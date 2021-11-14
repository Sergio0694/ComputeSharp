using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialAttribute"/> targeting a specific resource type.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ResourceAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="ResourceAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="type">The resource type.</param>
    public ResourceAttribute(Type type)
    {
        Type = type;
    }

    /// <summary>
    /// Gets the resource type for the current attribute.
    /// </summary>
    public Type Type { get; }
}
