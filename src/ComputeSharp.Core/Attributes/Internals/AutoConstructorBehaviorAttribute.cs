using System;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates that a target shader type should get an automatic constructor for all fields.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
internal sealed class AutoConstructorBehaviorAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="AutoConstructorBehaviorAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="behavior">The <see cref="AutoConstructorBehavior"/> value for the current instance.</param>
    public AutoConstructorBehaviorAttribute(AutoConstructorBehavior behavior)
    {
        Behavior = behavior;
    }

    /// <summary>
    /// Gets the <see cref="AutoConstructorBehavior"/> value for the current instance.
    /// </summary>
    public AutoConstructorBehavior Behavior { get; }
}