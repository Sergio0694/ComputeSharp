using System;
#if SOURCE_GENERATOR
using D3DCOMPILE = Windows.Win32.PInvoke;
#else
using ComputeSharp.D2D1.Interop;
using ComputeSharp.Win32;

#pragma warning disable IDE0004
#endif

namespace ComputeSharp.D2D1;

/// <summary>
/// Flags to control the options that can be used to compile a D2D1 shader.
/// </summary>
/// <remarks>
/// For more info, see For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/d3dcompile-constants"/>.
/// </remarks>
[Flags]
public enum D2D1CompileOptions
{
    /// <summary>
    /// Directs the compiler to insert debug file/line/type/symbol information into the output code.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_DEBUG</c> and <c>/Zi</c>.
    /// </remarks>
    Debug = (int)D3DCOMPILE.D3DCOMPILE_DEBUG,

    /// <summary>
    /// Directs the compiler not to validate the generated code against known capabilities and constraints.
    /// This option should only be used with shaders that have been successfully compiled in the past.
    /// DirectX always validates shaders before it sets them to a device.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_SKIP_VALIDATION</c> and <c>/Vd</c>.
    /// </remarks>
    SkipValidation = (int)D3DCOMPILE.D3DCOMPILE_SKIP_VALIDATION,

    /// <summary>
    /// Directs the compiler to skip optimization steps during code generation.
    /// This option should only be used for debug purposes.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_SKIP_OPTIMIZATION</c> and <c>/Od</c>.
    /// </remarks>
    SkipOptimization = (int)D3DCOMPILE.D3DCOMPILE_SKIP_OPTIMIZATION,

    /// <summary>
    /// Directs the compiler to pack matrices in row-major order on input and output from the shader.
    /// </summary>
    /// <remarks>
    /// <para>This value is needed to have the constant buffer loading from generated shaders map to compiled shaders.</para>
    /// <para>This flag maps to <c>D3DCOMPILE_PACK_MATRIX_ROW_MAJOR</c> and <c>/Zpr</c>.</para>
    /// </remarks>
    PackMatrixRowMajor = (int)D3DCOMPILE.D3DCOMPILE_PACK_MATRIX_ROW_MAJOR,

    /// <summary>
    /// Directs the compiler to pack matrices in column-major order on input and output from the shader.
    /// This type of packing is generally more efficient because a series of dot-products can then
    /// perform vector-matrix multiplication.
    /// </summary>
    /// <remarks>
    /// <para>Using this flag makes shaders incompatible with the constant buffer loading code produced by ComputeSharp.D2D1.</para>
    /// <para>That is, trying to load a constant buffer from a shader processed by the source generator is undefined behavior.</para>
    /// <para>This flag maps to <c>D3DCOMPILE_PACK_MATRIX_COLUMN_MAJOR</c> and <c>/Zpc</c>.</para>
    /// </remarks>
    PackMatrixColumnMajor = (int)D3DCOMPILE.D3DCOMPILE_PACK_MATRIX_COLUMN_MAJOR,

    /// <summary>
    /// Directs the compiler to perform all computations with partial precision.
    /// If this flag is set, the compiled code might run faster on some hardware.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_PARTIAL_PRECISION</c> and <c>/Gpp</c>.
    /// </remarks>
    PartialPrecision = (int)D3DCOMPILE.D3DCOMPILE_PARTIAL_PRECISION,

    /// <summary>
    /// Directs the compiler to not use flow-control constructs where possible.	
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_AVOID_FLOW_CONTROL</c> and <c>/Gfa</c>.
    /// </remarks>
    AvoidFlowControl = (int)D3DCOMPILE.D3DCOMPILE_AVOID_FLOW_CONTROL,

    /// <summary>
    /// Forces strict compile, which might not allow for legacy syntax.
    /// By default, the compiler disables strictness on deprecated syntax.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_ENABLE_STRICTNESS</c> and <c>/Ges</c>.
    /// </remarks>
    EnableStrictness = (int)D3DCOMPILE.D3DCOMPILE_ENABLE_STRICTNESS,

    /// <summary>
    /// Forces the IEEE strict compile which avoids optimizations that may break IEEE rules.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_IEEE_STRICTNESS</c> and <c>/Gis</c>.
    /// </remarks>
    IeeeStrictness = (int)D3DCOMPILE.D3DCOMPILE_IEEE_STRICTNESS,

    /// <summary>
    /// Directs the compiler to enable older shaders to compile to 5_0 targets.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_ENABLE_BACKWARDS_COMPATIBILITY</c> and <c>/Gec</c>.
    /// </remarks>
    EnableBackwardsCompatibility = (int)D3DCOMPILE.D3DCOMPILE_ENABLE_BACKWARDS_COMPATIBILITY,

    /// <summary>
    /// Directs the compiler to use the lowest optimization level. If this option is set, the compiler
    /// might produce slower code but produces the code more quickly. This option should be set when
    /// shaders are developed iteratively, as it can reduce compilation times.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_OPTIMIZATION_LEVEL0</c> and <c>/O0</c>.
    /// </remarks>
    OptimizationLevel0 = (int)D3DCOMPILE.D3DCOMPILE_OPTIMIZATION_LEVEL0,

    /// <summary>
    /// Directs the compiler to use the second lowest optimization level.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_OPTIMIZATION_LEVEL1</c> and <c>/O1</c>.
    /// </remarks>
    OptimizationLevel1 = (int)D3DCOMPILE.D3DCOMPILE_OPTIMIZATION_LEVEL1,

    /// <summary>
    /// Directs the compiler to use the second highest optimization level.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_OPTIMIZATION_LEVEL2</c> and <c>/O2</c>.
    /// </remarks>
    OptimizationLevel2 = (int)D3DCOMPILE.D3DCOMPILE_OPTIMIZATION_LEVEL2,

    /// <summary>
    /// Directs the compiler to use the highest optimization level. If this option is set, the compiler
    /// produces the best possible code but might take significantly longer to do so. This option should
    /// be set for final builds of an application when performance is the most important factor.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_OPTIMIZATION_LEVEL3</c> and <c>/O3</c>.
    /// </remarks>
    OptimizationLevel3 = (int)D3DCOMPILE.D3DCOMPILE_OPTIMIZATION_LEVEL3,

    /// <summary>
    /// Directs the compiler to treat all warnings as errors when it compiles the shader code. This 
    /// option is recommended for new shader code, so that warnings can be resolved and the
    /// number of hard-to-find code defects can be minimized.	
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILE_WARNINGS_ARE_ERRORS</c> and <c>/WX</c>.
    /// </remarks>
    WarningsAreErrors = (int)D3DCOMPILE.D3DCOMPILE_WARNINGS_ARE_ERRORS,

    /// <summary>
    /// Strips the reflection data from the generated shader bytecode. The bytecode size will be smaller, but trying
    /// to perform reflection on the shader will return inaccurate results. It is recommend if not reflection is used.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>D3DCOMPILER_STRIP_REFLECTION_DATA</c>, when passed as input to <c>D3DStripShader</c>.
    /// For more info, see <see href="https://learn.microsoft.com/en-us/windows/win32/api/d3dcompiler/nf-d3dcompiler-d3dstripshader"/>.
    /// </remarks>
    StripReflectionData = 1 << 30,

    /// <summary>
    /// This flag enables the support for shader linking. Specifically, when set, this flag causes the APIs from
    /// <see cref="D2D1ShaderCompiler"/> to compile the input shader twice: once as a full D2D1 shader, and once
    /// as an export function, which is then set as private blob data into the final bytecode that is returned.
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct2d/effect-shader-linking"/>.
    /// </remarks>
    EnableLinking = 1 << 31,

    /// <summary>
    /// The default options for shaders compiled by ComputeSharp.D2D1. Specifically, this combination
    /// of options is the one used by precompiled shaders (ie. when using <see cref="D2DShaderProfileAttribute"/>),
    /// and when using <see cref="D2D1PixelShader.LoadBytecode{T}()"/> or any of the overloads.
    /// </summary>
    /// <remarks>
    /// This option does not include <see cref="EnableLinking"/>, which is instead automatically enabled by shaders
    /// compiled using any of the APIs mentioned above. When manually compiling shaders from source using the APIs
    /// from <see cref="D2D1ShaderCompiler"/> instance, <see cref="EnableLinking"/> should be manually used when needed.
    /// </remarks>
    Default = OptimizationLevel3 | WarningsAreErrors | PackMatrixRowMajor
}