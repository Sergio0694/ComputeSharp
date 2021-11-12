using System;

namespace ComputeSharp.Tests.Attributes;

/// <summary>
/// An attribute to use with <see cref="CombinatorialAttribute"/> targeting an additional specific resource type.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class AdditionalResourceAttribute : Attribute
{
    public AdditionalResourceAttribute(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}
