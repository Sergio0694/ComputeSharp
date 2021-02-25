namespace ComputeSharp.Interop
{
    /// <summary>
    /// A model representing reflection info for a given compute shader.
    /// </summary>
    public readonly struct ShaderInfo
    {
        /// <summary>
        /// The compiler version used to build the shader.
        /// </summary>
        public readonly string CompilerVersion;

        /// <summary>
        /// The HLSL source code the shader was built from.
        /// </summary>
        public readonly string HlslSource;

        /// <summary>
        /// The number of shader constant buffers.
        /// </summary>
        public readonly uint ConstantBufferCount;

        /// <summary>
        /// The number of resource (textures and buffers) bound to a shader.
        /// </summary>
        public readonly uint BoundResourceCount;

        /// <summary>
        /// The number of intermediate-language instructions in the compiled shader.
        /// </summary>
        public readonly uint InstructionCount;

        /// <summary>
        /// The number of temporary registers in the compiled shader.
        /// </summary>
        public readonly uint TemporaryRegisterCount;

        /// <summary>
        /// The number of temporary arrays used.
        /// </summary>
        public readonly uint TemporaryArrayCount;

        /// <summary>
        /// The number of constant defines.
        /// </summary>
        public readonly uint ConstantDefineCount;

        /// <summary>
        /// The number of declarations (input + output).
        /// </summary>
        public readonly uint DeclarationCount;

        /// <summary>
        /// The number of non-categorized texture instructions.
        /// </summary>
        public readonly uint TextureNormalInstructions;

        /// <summary>
        /// The number of texture load instructions.
        /// </summary>
        public readonly uint TextureLoadInstructionCount;

        /// <summary>
        /// The number of texture write instructions.
        /// </summary>
        public readonly uint TextureStoreInstructionCount;

        /// <summary>
        /// The number of floating point arithmetic instructions used.
        /// </summary>
        public readonly uint FloatInstructionCount;

        /// <summary>
        /// The number of signed integer arithmetic instructions used.
        /// </summary>
        public readonly uint IntInstructionCount;

        /// <summary>
        /// The number of unsigned integer arithmetic instructions used.
        /// </summary>
        public readonly uint UIntInstructionCount;

        /// <summary>
        /// The number of static flow control instructions used.
        /// </summary>
        public readonly uint StaticFlowControlInstructionCount;

        /// <summary>
        /// The number of dynamic flow control instructions used.
        /// </summary>
        public readonly uint DynamicFlowControlInstructionCount;

        /// <summary>
        /// The number of emit instructions used.
        /// </summary>
        public readonly uint EmitInstructionCount;

        /// <summary>
        /// The number of barrier instructions used.
        /// </summary>
        public readonly uint BarrierInstructionCount;

        /// <summary>
        /// The number of interlocked instructions used.
        /// </summary>
        public readonly uint InterlockedInstructionCount;

        /// <summary>
        /// The number of bitwise instructions used.
        /// </summary>
        public readonly uint BitwiseInstructionCount;

        /// <summary>
        /// The number of <c>movc</c> instructions used.
        /// </summary>
        public readonly uint MovcInstructionCount;

        /// <summary>
        /// The number of <c>mov</c> instructions used.
        /// </summary>
        public readonly uint MovInstructionCount;

        /// <summary>
        /// The number of interface slots used.
        /// </summary>
        public readonly uint InterfaceSlotCount;
    }
}
