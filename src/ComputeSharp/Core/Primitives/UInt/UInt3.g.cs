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
    /// <inheritdoc cref="UInt3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct UInt3 : IFormattable
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt3), sizeof(UInt4));

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
        public readonly ref uint this[int i] => ref *(uint*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>X</c> component.
        /// </summary>
        public readonly ref uint X => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Y</c> component.
        /// </summary>
        public readonly ref uint Y => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Z</c> component.
        /// </summary>
        public readonly ref uint Z => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt2 XX => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 XY => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 XZ => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 YX => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt2 YY => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 YZ => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 ZX => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 ZY => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt2 ZZ => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XXX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XXY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XXZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XYX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XYY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 XYZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XZX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 XZY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XZZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YXX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YXY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 YXZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YYX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YYY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YYZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 YZX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YZY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YZZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZXX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ZXY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZXZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ZYX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZYY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZYZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZZX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZZY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZZZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>R</c> component.
        /// </summary>
        public readonly ref uint R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>G</c> component.
        /// </summary>
        public readonly ref uint G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>B</c> component.
        /// </summary>
        public readonly ref uint B => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt2 RR => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 RG => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 RB => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 GR => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt2 GG => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 GB => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 BR => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 BG => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt2 BB => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RRR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RRG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RRB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RGR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RGG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 RGB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RBR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 RBG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RBB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GRR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GRG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 GRB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GGR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GGG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GGB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 GBR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GBG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GBB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BRR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 BRG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BRB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 BGR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BGG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BGB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BBR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BBG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BBB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBBB => ref *(UInt4*)UndefinedData;

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
