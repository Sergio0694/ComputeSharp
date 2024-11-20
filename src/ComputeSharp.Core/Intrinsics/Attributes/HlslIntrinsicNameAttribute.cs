using System;
using System.Diagnostics;

namespace ComputeSharp.Core.Intrinsics;

/// <summary>
/// An attribute indicating the native member name of a given HLSL intrinsic.
/// </summary>
/// <param name="name">The name of the HLSL intrinsic.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[Conditional("SOURCE_GENERATOR")]
internal sealed class HlslIntrinsicNameAttribute(string name) : Attribute
{
    /// <summary>
    /// Gets the name of the HLSL intrinsic.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets whether or not matching parameters is also required to extract the name of this intrinsic.
    /// </summary>
    public bool RequiresParametersMatching { get; init; }
}