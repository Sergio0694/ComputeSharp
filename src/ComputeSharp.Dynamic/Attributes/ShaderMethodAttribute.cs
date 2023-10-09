using System;
using System.ComponentModel;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates that a method will be indirectly used in a compute shader.
/// This method is necessary for methods that are captured and used within a given shader
/// through a <see cref="Delegate"/> field (of some custom type). In order to be processed
/// correctly, wrapped methods need to have this attribute manually applied to them.
/// Methods also need to be static, though this can only be tested at runtime.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete(
    "This API (and shader metaprogramming in general) is being removed in a future version of ComputeSharp. " +
    "If you're relying on this functionality, consider manually implementing different shader variants. " +
    "It is possible to still reuse code by moving it to helper methods that are used by multiple shaders.")]
public sealed class ShaderMethodAttribute : Attribute
{
}