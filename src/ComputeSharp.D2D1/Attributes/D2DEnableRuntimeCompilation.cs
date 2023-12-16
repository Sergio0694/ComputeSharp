using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute that indicates that runtime compilation of D2D shaders is enabled.
/// This attribute is required for all shaders that are not compiled at build time.
/// </summary>
/// <remarks>
/// <para>
/// Precompiling shaders at build time is recommended, as it enables additional validation
/// and diagnostics, and provides better performance at runtime when dispatching the shaders.
/// </para>
/// <para>
/// This option can be used in advanced scenarios, for instance when trying to minimize
/// binary size (which should only be done after carefully measuring the actual difference).
/// </para>
/// <para>
/// Note that not using this attribute does not mean that trying to compile shaders at runtime will
/// fail. The purpose of this attribute is to allow declaring shader types that are not precompiled.
/// All other features, such as manually compiling a shader type with specific compile options or
/// with a different shader profile, or manually compiling a shader from source at runtime, are
/// always supported, regardless of whether nor not this attribute is used.
/// </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Assembly, AllowMultiple = false)]
public sealed class D2DEnableRuntimeCompilationAttribute : Attribute;