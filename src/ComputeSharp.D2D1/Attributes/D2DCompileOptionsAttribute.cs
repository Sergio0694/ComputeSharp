using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute that indicates the compile options that should be used when compiling a given D2D1 shader.
/// This applies when the shader is precompiled (through <see cref="D2DEmbeddedBytecodeAttribute"/>) as well.
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
/// </para>
/// Note that the <see cref="D2D1CompileOptions.PackMatrixRowMajor"/> is always enabled automatically and does not need to be
/// specified. This option is mandatory, as the generated code to load the constant buffer from a shader assumes the layout
/// for matrix types is row major. For the same reason, using <see cref="D2D1CompileOptions.PackMatrixColumnMajor"/> is not
/// allowed, as it would cause the constant buffer retrieved from a shader to be potentially incorrect.
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DCompileOptionsAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="D2DCompileOptionsAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="options">The compiler options to use to compile the shader.</param>
    public D2DCompileOptionsAttribute(D2D1CompileOptions options)
    {
        Options = options;
    }

    /// <summary>
    /// Gets the number of threads in each thread group for the X axis
    /// </summary>
    public D2D1CompileOptions Options { get; }
}
