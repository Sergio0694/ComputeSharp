using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if !NET5_0
using RuntimeHelpers = ComputeSharp.SourceGenerators.Helpers.RuntimeHelpers;
using MemoryMarshal = ComputeSharp.SourceGenerators.Helpers.MemoryMarshal;
#endif

namespace ComputeSharp
{
    /// <inheritdoc cref="Double3"/>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    public unsafe partial struct Double3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double3), sizeof(Double3));

        [FieldOffset(0)]
        private double x;

        [FieldOffset(8)]
        private double y;

        [FieldOffset(16)]
        private double z;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Double3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref double this[int i] => ref *(double*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>X</c> component.
        /// </summary>
        public readonly ref double X => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>Y</c> component.
        /// </summary>
        public readonly ref double Y => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>Z</c> component.
        /// </summary>
        public readonly ref double Z => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double2 XX => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 XY => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 XZ => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 YX => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double2 YY => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 YZ => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 ZX => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 ZY => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double2 ZZ => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XXX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XXY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XXZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XYX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XYY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 XYZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XZX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 XZY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XZZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YXX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YXY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 YXZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YYX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YYY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YYZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 YZX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YZY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YZZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZXX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ZXY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZXZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ZYX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZYY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZYZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZZX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZZY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZZZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>R</c> component.
        /// </summary>
        public readonly ref double R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>G</c> component.
        /// </summary>
        public readonly ref double G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>B</c> component.
        /// </summary>
        public readonly ref double B => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double2 RR => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 RG => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 RB => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 GR => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double2 GG => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 GB => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 BR => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 BG => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double2 BB => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RRR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RRG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RRB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RGR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RGG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 RGB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RBR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 RBG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RBB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GRR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GRG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 GRB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GGR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GGG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GGB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 GBR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GBG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GBB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BRR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 BRG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BRB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 BGR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BGG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BGB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BBR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BBG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BBB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Negates a <see cref="Double3"/> value.
        /// </summary>
        /// <param name="xyz">The <see cref="Double3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3 operator -(Double3 xyz) => default;

        /// <summary>
        /// Sums two <see cref="Double3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3 operator +(Double3 left, Double3 right) => default;

        /// <summary>
        /// Divides two <see cref="Double3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3 operator /(Double3 left, Double3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3 operator *(Double3 left, Double3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3 operator -(Double3 left, Double3 right) => default;
    }
}
