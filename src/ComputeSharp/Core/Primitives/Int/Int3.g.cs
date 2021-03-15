using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
#if !NET5_0
using RuntimeHelpers = ComputeSharp.SourceGenerators.Helpers.RuntimeHelpers;
using MemoryMarshal = ComputeSharp.SourceGenerators.Helpers.MemoryMarshal;
#endif

#nullable enable

namespace ComputeSharp
{
    /// <inheritdoc cref="Int3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct Int3 : IFormattable
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Int3), sizeof(Int3));

        [FieldOffset(0)]
        private int x;

        [FieldOffset(4)]
        private int y;

        [FieldOffset(8)]
        private int z;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Int3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref int this[int i] => ref *(int*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>X</c> component.
        /// </summary>
        public readonly ref int X => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>Y</c> component.
        /// </summary>
        public readonly ref int Y => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>Z</c> component.
        /// </summary>
        public readonly ref int Z => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int2 XX => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 XY => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 XZ => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 YX => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int2 YY => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 YZ => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 ZX => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 ZY => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int2 ZZ => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 XXX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 XXY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 XXZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 XYX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 XYY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 XYZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 XZX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 XZY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 XZZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 YXX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 YXY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 YXZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 YYX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 YYY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 YYZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 YZX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 YZY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 YZZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 ZXX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 ZXY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 ZXZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 ZYX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 ZYY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 ZYZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 ZZX => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 ZZY => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 ZZZ => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>R</c> component.
        /// </summary>
        public readonly ref int R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>G</c> component.
        /// </summary>
        public readonly ref int G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>B</c> component.
        /// </summary>
        public readonly ref int B => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int2 RR => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 RG => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 RB => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 GR => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int2 GG => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 GB => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 BR => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int2 BG => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int2 BB => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 RRR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 RRG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 RRB => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 RGR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 RGG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 RGB => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 RBR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 RBG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 RBB => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 GRR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 GRG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 GRB => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 GGR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 GGG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 GGB => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 GBR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 GBG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 GBB => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 BRR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 BRG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 BRB => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Int3 BGR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 BGG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 BGB => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 BBR => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 BBG => ref *(Int3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Int3 BBB => ref *(Int3*)UndefinedData;

        /// <inheritdoc/>
        public override readonly string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <inheritdoc/>
        public readonly string ToString(string? format, IFormatProvider? formatProvider)
        {
            StringBuilder sb = new();

            string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            sb.Append('<');
            sb.Append(this.x.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(this.y.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(this.z.ToString(format, formatProvider));
            sb.Append('>');

            return sb.ToString();
        }

        /// <summary>
        /// Negates a <see cref="Int3"/> value.
        /// </summary>
        /// <param name="xyz">The <see cref="Int3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int3 operator -(Int3 xyz) => default;

        /// <summary>
        /// Sums two <see cref="Int3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int3 operator +(Int3 left, Int3 right) => default;

        /// <summary>
        /// Divides two <see cref="Int3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int3 operator /(Int3 left, Int3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Int3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int3 operator *(Int3 left, Int3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Int3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int3 operator -(Int3 left, Int3 right) => default;
    }
}
