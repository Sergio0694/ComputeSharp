using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute that indicates the compile options that should be used when compiling a given D2D1 shader.
/// This applies when the shader is precompiled (through <see cref="D2DShaderProfileAttribute"/>) as well.
/// <para>
/// This attribute can be used to annotate shader types as follows:
/// <code>
/// [D2DCompileOptions(D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel3)]
/// struct MyShader : ID2D1PixelShader
/// {
/// }
/// </code>
/// </para>
/// <para>
/// Note that the <see cref="D2D1CompileOptions.PackMatrixRowMajor"/> is always enabled automatically and does not need to be
/// specified. This option is mandatory, as the generated code to load the constant buffer from a shader assumes the layout
/// for matrix types is row major. For the same reason, using <see cref="D2D1CompileOptions.PackMatrixColumnMajor"/> is not
/// allowed, as it would cause the constant buffer retrieved from a shader to be potentially incorrect.
/// </para>
/// <para>
/// This attribute can also be added to a whole assembly, and will be used by default if not overridden by a shader type.
/// </para>
/// </summary>
/// <param name="options">The compile options to use to compile the shader.</param>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
public sealed class D2DCompileOptionsAttribute(D2D1CompileOptions options) : Attribute
{
    /// <summary>
    /// Gets the compile options to use to compile the shader.
    /// </summary>
    public D2D1CompileOptions Options { get; } = options;
}