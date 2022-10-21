namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A model representing reflection info for a given D2D1 shader.
/// </summary>
/// <param name="CompilerVersion">The compiler version used to build the shader.</param>
/// <param name="HlslSource">The HLSL source code the shader was built from.</param>
/// <param name="ConstantBufferCount">The number of shader constant buffers.</param>
/// <param name="BoundResourceCount">The number of resource (textures and buffers) bound to a shader.</param>
/// <param name="InstructionCount">The number of intermediate-language instructions in the compiled shader.</param>
/// <param name="TemporaryRegisterCount">The number of temporary registers in the compiled shader.</param>
/// <param name="TemporaryArrayCount">The number of temporary arrays used.</param>
/// <param name="ConstantDefineCount">The number of constant defines.</param>
/// <param name="DeclarationCount">The number of declarations (input + output).</param>
/// <param name="TextureNormalInstructions">The number of non-categorized texture instructions.</param>
/// <param name="TextureLoadInstructionCount"> The number of texture load instructions.</param>
/// <param name="TextureStoreInstructionCount">The number of texture write instructions.</param>
/// <param name="FloatInstructionCount">The number of floating point arithmetic instructions used.</param>
/// <param name="IntInstructionCount">The number of signed integer arithmetic instructions used.</param>
/// <param name="UIntInstructionCount">The number of unsigned integer arithmetic instructions used.</param>
/// <param name="StaticFlowControlInstructionCount">The number of static flow control instructions used.</param>
/// <param name="DynamicFlowControlInstructionCount">The number of dynamic flow control instructions used.</param>
/// <param name="EmitInstructionCount">The number of emit instructions used.</param>
/// <param name="BarrierInstructionCount">The number of barrier instructions used.</param>
/// <param name="InterlockedInstructionCount">The number of interlocked instructions used.</param>
/// <param name="BitwiseInstructionCount">The number of bitwise instructions used.</param>
/// <param name="MovcInstructionCount">The number of <c>movc</c> instructions used.</param>
/// <param name="MovInstructionCount">The number of <c>mov</c> instructions used.</param>
/// <param name="InterfaceSlotCount">The number of interface slots used.</param>
/// <param name="RequiresDoublePrecisionSupport">Indicates whether support for double precision floating point numbers is required.</param>
/// <param name="MinimumFeatureLevel">The minimum feature level for the shader.</param>
public sealed record D2D1ShaderInfo(
    string CompilerVersion,
    string HlslSource,
    uint ConstantBufferCount,
    uint BoundResourceCount,
    uint InstructionCount,
    uint TemporaryRegisterCount,
    uint TemporaryArrayCount,
    uint ConstantDefineCount,
    uint DeclarationCount,
    uint TextureNormalInstructions,
    uint TextureLoadInstructionCount,
    uint TextureStoreInstructionCount,
    uint FloatInstructionCount,
    uint IntInstructionCount,
    uint UIntInstructionCount,
    uint StaticFlowControlInstructionCount,
    uint DynamicFlowControlInstructionCount,
    uint EmitInstructionCount,
    uint BarrierInstructionCount,
    uint InterlockedInstructionCount,
    uint BitwiseInstructionCount,
    uint MovcInstructionCount,
    uint MovInstructionCount,
    uint InterfaceSlotCount,
    bool RequiresDoublePrecisionSupport,
    D3DFeatureLevel MinimumFeatureLevel);