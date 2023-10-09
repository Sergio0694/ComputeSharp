using System;
using System.ComponentModel;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates that a target shader type should get an automatic constructor for all fields.
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete(
    "This API (and its accompanying source generator) is being removed in a future version of ComputeSharp. " +
    "If you're relying on this functionality, consider switching to primary constructors, once they are supported. " +
    "Otherwise, consider using alternative source generators providing similar functionality available on NuGet.")]
public sealed class AutoConstructorAttribute : Attribute
{
}