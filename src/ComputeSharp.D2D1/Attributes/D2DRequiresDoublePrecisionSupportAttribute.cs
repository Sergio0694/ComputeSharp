using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating that the shader requires support for double precision operations.
/// </summary>
/// <remarks>
/// <para>
/// This attribute does not map to any HLSL feature or compile option directly, but it is needed to make using double
/// precision operations opt-in. It is easy to accidentally introduce them in a shader when that is not intented, and
/// doing so can make the shader run slower and not being compatible with some GPUs (eg. many arm64 GPUs lack support
/// for double precision operations). To avoid this, support is disabled by default, and it is necessary to add this
/// attribute over a shader type to explicitly allow using these operations.
/// </para>
/// <para>
/// Validation can only be performed when the shader is being precompiled. If that is not the case (ie. if the shader
/// is using <see cref="D2DEnableRuntimeCompilationAttribute"/> and not <see cref="D2DShaderProfileAttribute"/>), then
/// no build time check for double precision operations can be done. Using this attribute in that scenarios is not valid.
/// </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DRequiresDoublePrecisionSupportAttribute : Attribute;