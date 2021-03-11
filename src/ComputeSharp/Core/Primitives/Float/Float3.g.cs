using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp
{
    /// <inheritdoc cref="Float3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct Float3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Float3), sizeof(Float3));

        [FieldOffset(0)]
        private float x;

        [FieldOffset(4)]
        private float y;

        [FieldOffset(8)]
        private float z;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Float3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref float this[int i] => ref *(float*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>X</c> component.
        /// </summary>
        public ref float X => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.x, 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref float Y => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.y, 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref float Z => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.z, 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float2 XX => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 XY => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 XZ => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 YX => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float2 YY => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 YZ => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 ZX => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 ZY => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float2 ZZ => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 XXX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 XXY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 XXZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 XYX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 XYY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 XYZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 XZX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 XZY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 XZZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 YXX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 YXY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 YXZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 YYX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 YYY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 YYZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 YZX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 YZY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 YZZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 ZXX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 ZXY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 ZXZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 ZYX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 ZYY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 ZYZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 ZZX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 ZZY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 ZZZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
        /// </summary>
        public ref float R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.x, 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
        /// </summary>
        public ref float G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.y, 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>B</c> component.
        /// </summary>
        public ref float B => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.z, 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float2 RR => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 RG => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 RB => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 GR => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float2 GG => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 GB => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 BR => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float2 BG => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float2 BB => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 RRR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 RRG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 RRB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 RGR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 RGG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 RGB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 RBR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 RBG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 RBB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 GRR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 GRG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 GRB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 GGR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 GGG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 GGB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 GBR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 GBG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 GBB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 BRR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 BRG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 BRB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Float3 BGR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 BGG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 BGB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 BBR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 BBG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Float3 BBB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Negates a <see cref="Float3"/> value.
        /// </summary>
        /// <param name="xyz">The <see cref="Float3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float3 operator -(Float3 xyz) => default;

        /// <summary>
        /// Sums two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float3 operator +(Float3 left, Float3 right) => default;

        /// <summary>
        /// Divides two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float3 operator /(Float3 left, Float3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float3 operator *(Float3 left, Float3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float3 operator -(Float3 left, Float3 right) => default;
    }
}
