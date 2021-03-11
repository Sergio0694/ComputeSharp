using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp
{
    /// <inheritdoc cref="UInt3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct UInt3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt3), sizeof(UInt3));

        [FieldOffset(0)]
        private uint x;

        [FieldOffset(4)]
        private uint y;

        [FieldOffset(8)]
        private uint z;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="UInt3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref uint this[int i] => ref *(uint*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>X</c> component.
        /// </summary>
        public ref uint X => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.x, 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref uint Y => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.y, 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref uint Z => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.z, 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt2 XX => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 XY => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 XZ => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 YX => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt2 YY => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 YZ => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 ZX => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 ZY => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt2 ZZ => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 XXX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 XXY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 XXZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 XYX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 XYY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 XYZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 XZX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 XZY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 XZZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 YXX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 YXY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 YXZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 YYX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 YYY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 YYZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 YZX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 YZY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 YZZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 ZXX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 ZXY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 ZXZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 ZYX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 ZYY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 ZYZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 ZZX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 ZZY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 ZZZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>R</c> component.
        /// </summary>
        public ref uint R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.x, 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>G</c> component.
        /// </summary>
        public ref uint G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.y, 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>B</c> component.
        /// </summary>
        public ref uint B => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.z, 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt2 RR => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 RG => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 RB => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 GR => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt2 GG => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 GB => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 BR => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt2 BG => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt2 BB => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 RRR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 RRG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 RRB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 RGR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 RGG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 RGB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 RBR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 RBG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 RBB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 GRR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 GRG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 GRB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 GGR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 GGG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 GGB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 GBR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 GBG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 GBB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 BRR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 BRG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 BRB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref UInt3 BGR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 BGG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 BGB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 BBR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 BBG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly UInt3 BBB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Sums two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3 operator +(UInt3 left, UInt3 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3 operator /(UInt3 left, UInt3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3 operator *(UInt3 left, UInt3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3 operator -(UInt3 left, UInt3 right) => default;
    }
}
