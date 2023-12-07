using System;

namespace ComputeSharp;

/// <summary>
/// Flags to control the options that can be used to compile a compute shader.
/// </summary>
[Flags]
public enum CompileOptions
{
    /// <summary>
    /// Enables agressive flattening.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-all-resources-bound</c>.
    /// </remarks>
    AllResourcesBound = 1 << 0,

    /// <summary>
    /// Directs the compiler not to validate the generated code against known capabilities and constraints.
    /// This option should only be used with shaders that have been successfully compiled in the past.
    /// DirectX always validates shaders before it sets them to a device.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-Vd</c>.
    /// </remarks>
    DisableValidation = 1 << 1,

    /// <summary>
    /// Directs the compiler to skip optimization steps during code generation.
    /// This option should only be used for debug purposes.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-Od</c>.
    /// </remarks>
    DisableOptimization = 1 << 2,

    /// <summary>
    /// Directs the compiler to not use flow-control constructs where possible.	
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-Gfa</c>.
    /// </remarks>
    AvoidFlowControl = 1 << 3,

    /// <summary>
    /// Directs the compiler to prefer using flow-control constructs where possible.	
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-Gfp</c>.
    /// </remarks>
    PreferFlowControl = 1 << 4,

    /// <summary>
    /// Forces strict compile, which might not allow for legacy syntax.
    /// By default, the compiler disables strictness on deprecated syntax.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-Ges</c>.
    /// </remarks>
    EnableStrictness = 1 << 5,

    /// <summary>
    /// Forces the IEEE strict compile which avoids optimizations that may break IEEE rules.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-Gis</c>.
    /// </remarks>
    IeeeStrictness = 1 << 6,

    /// <summary>
    /// Enables backward compatibility mode.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-Gec</c>.
    /// </remarks>
    EnableBackwardsCompatibility = 1 << 7,

    /// <summary>
    /// Directs the compiler to use the lowest optimization level. If this option is set, the compiler
    /// might produce slower code but produces the code more quickly. This option should be set when
    /// shaders are developed iteratively, as it can reduce compilation times.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-O0</c>.
    /// </remarks>
    OptimizationLevel0 = 1 << 8,

    /// <summary>
    /// Directs the compiler to use the second lowest optimization level.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-O1</c>.
    /// </remarks>
    OptimizationLevel1 = OptimizationLevel0 | (1 << 9),

    /// <summary>
    /// Directs the compiler to use the second highest optimization level.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-O2</c>.
    /// </remarks>
    OptimizationLevel2 = OptimizationLevel1 | (1 << 10),

    /// <summary>
    /// Directs the compiler to use the highest optimization level. If this option is set, the compiler
    /// produces the best possible code but might take significantly longer to do so. This option should
    /// be set for final builds of an application when performance is the most important factor.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-O3</c>.
    /// </remarks>
    OptimizationLevel3 = OptimizationLevel2 | (1 << 11),

    /// <summary>
    /// Directs the compiler to treat all warnings as errors when it compiles the shader code. This 
    /// option is recommended for new shader code, so that warnings can be resolved and the
    /// number of hard-to-find code defects can be minimized.	
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-WX</c>.
    /// </remarks>
    WarningsAreErrors = 1 << 12,

    /// <summary>
    /// Directs the compiler that UAVs (uniform access views) and SRVs (shader resource views) may alias.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-res-may-alias</c>.
    /// </remarks>
    ResourcesMayAlias = 1 << 13,

    /// <summary>
    /// Strips the reflection data from the generated shader bytecode. The bytecode size will be significantly smaller, but
    /// trying to perform reflection on the shader will return inaccurate results. It is recommend if not reflection is used.
    /// </summary>
    /// <remarks>
    /// This flag maps to <c>-Qstrip_reflect</c>.
    /// </remarks>
    StripReflectionData = 1 << 14,

    /// <summary>
    /// The default options for shaders compiled by ComputeSharp.
    /// </summary>
    /// <remarks>
    /// This option does not include a flag to pack matrices in column-major order on input and output from the shader,
    /// as that is always enabled by default (it is necessary to ensure the constant buffer matches the managed layout).
    /// </remarks>
    Default = OptimizationLevel3 | WarningsAreErrors
}