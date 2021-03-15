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
    /// <inheritdoc cref="UInt4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public unsafe partial struct UInt4 : IFormattable
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt4), sizeof(UInt4));

        [FieldOffset(0)]
        private uint x;

        [FieldOffset(4)]
        private uint y;

        [FieldOffset(8)]
        private uint z;

        [FieldOffset(12)]
        private uint w;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="UInt4"/> instance.
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
        /// Gets a reference to the <see cref="uint"/> value representing the <c>W</c> component.
        /// </summary>
        public readonly ref uint W => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.w), 1));

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
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 XW => ref *(UInt2*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 YW => ref *(UInt2*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 ZW => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 WX => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 WY => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 WZ => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt2 WW => ref *(UInt2*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XXW => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 XYW => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 XZW => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XWX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 XWY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 XWZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 XWW => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 YXW => ref *(UInt3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YYW => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 YZW => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 YWX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YWY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 YWZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 YWW => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ZXW => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ZYW => ref *(UInt3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZZW => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ZWX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ZWY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZWZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ZWW => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WXX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 WXY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 WXZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WXW => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 WYX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WYY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 WYZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WYW => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 WZX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 WZY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WZZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WZW => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WWX => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WWY => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WWZ => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 WWW => ref *(UInt3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XXWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 XYZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 XYWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XYWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 XZYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 XZWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XZWW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWXW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 XWYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWYW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 XWZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 XWWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 YXZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 YXWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YXWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YYWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 YZXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 YZWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YZWW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 YWXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWXW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWYW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 YWZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 YWWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ZXYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ZXWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZXWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ZYXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ZYWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZYWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZXW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZYW => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZZWW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ZWXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWXW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ZWYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWYW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ZWWW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXXW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 WXYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXYW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 WXZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WXWW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 WYXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYXW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYYW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 WYZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WYWW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 WZXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZXW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 WZYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZYW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WZWW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWXX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWXY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWXZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWXW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWYX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWYY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWYZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWYW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWZX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWZY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWZZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWZW => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWWX => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWWY => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWWZ => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 WWWW => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="uint"/> value representing the <c>A</c> component.
        /// </summary>
        public readonly ref uint A => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.w), 1));

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
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 RA => ref *(UInt2*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 GA => ref *(UInt2*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 BA => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 AR => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 AG => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 AB => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt2 AA => ref *(UInt2*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RRA => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 RGA => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 RBA => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RAR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 RAG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 RAB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 RAA => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 GRA => ref *(UInt3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GGA => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 GBA => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 GAR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GAG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 GAB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 GAA => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 BRA => ref *(UInt3*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 BGA => ref *(UInt3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BBA => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 BAR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 BAG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BAB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 BAA => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ARR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ARG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ARB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ARA => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 AGR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 AGG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 AGB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 AGA => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ABR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 ABG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ABB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 ABA => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 AAR => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 AAG => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 AAB => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt3 AAA => ref *(UInt3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRGA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RRAA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGGA => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 RGBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 RGAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RGAA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 RBGA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 RBAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RBAA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RARR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RARG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RARB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RARA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RAGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RAGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 RAGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RAGA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RABR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 RABG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RABB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RABA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RAAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RAAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RAAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 RAAA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRGA => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 GRBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 GRAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GRAA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGGA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GGAA => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 GBRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBGA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 GBAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GBAA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GARR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GARG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 GARB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GARA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GAGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GAGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GAGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GAGA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 GABR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GABG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GABB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GABA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GAAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GAAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GAAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 GAAA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 BRGA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 BRAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BRAA => ref *(UInt4*)UndefinedData;

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
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 BGRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGGA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 BGAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BGAA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBRA => ref *(UInt4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBGA => ref *(UInt4*)UndefinedData;

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

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BBAA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BARR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 BARG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BARB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BARA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 BAGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BAGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BAGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BAGA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BABR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BABG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BABB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BABA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BAAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BAAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BAAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 BAAA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARRA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ARGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARGA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ARBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ARAA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 AGRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGRA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGGA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 AGBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AGAA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABRR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ABRG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABRB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABRA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 ABGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABGA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABBR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABBG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABBB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABBA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 ABAA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AARR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AARG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AARB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AARA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AAGR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AAGG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AAGB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AAGA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AABR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AABG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AABB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AABA => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AAAR => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AAAG => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AAAB => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly UInt4 AAAA => ref *(UInt4*)UndefinedData;

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
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(this.w.ToString(format, formatProvider));
            sb.Append('>');

            return sb.ToString();
        }

        /// <summary>
        /// Sums two <see cref="UInt4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4 operator +(UInt4 left, UInt4 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4 operator /(UInt4 left, UInt4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4 operator *(UInt4 left, UInt4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4 operator -(UInt4 left, UInt4 right) => default;
    }
}
