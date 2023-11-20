using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialTestMethodAttribute"/> targeting a specific resource type.
/// </summary>
/// <param name="type">The resource type.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ResourceAttribute(Type type) : Attribute
{
    /// <summary>
    /// Gets the resource type for the current attribute.
    /// </summary>
    public Type Type { get; } = type;
}