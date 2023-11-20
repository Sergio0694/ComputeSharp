using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialTestMethodAttribute"/> targeting an additional specific resource type.
/// </summary>
/// <param name="type">The additional resource type.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class AdditionalResourceAttribute(Type type) : Attribute
{
    /// <summary>
    /// Gets the additional resource type for the current attribute.
    /// </summary>
    public Type Type { get; } = type;
}