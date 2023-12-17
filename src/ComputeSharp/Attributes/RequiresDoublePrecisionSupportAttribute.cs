using System;

namespace ComputeSharp;

/// <summary>
/// An attribute for a compute shader indicating that the shader requires support for double precision operations.
/// </summary>
/// <remarks>
/// This attribute does not map to any HLSL feature or compile option directly, but it is needed to make using double
/// precision operations opt-in. It is easy to accidentally introduce them in a shader when that is not intented, and
/// doing so can make the shader run slower and not being compatible with some GPUs (eg. many arm64 GPUs lack support
/// for double precision operations). To avoid this, support is disabled by default, and it is necessary to add this
/// attribute over a shader type to explicitly allow using these operations.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class RequiresDoublePrecisionSupportAttribute : Attribute;