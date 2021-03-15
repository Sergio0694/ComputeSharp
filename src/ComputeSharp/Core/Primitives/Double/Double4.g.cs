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
    /// <inheritdoc cref="Double4"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 8)]
    public unsafe partial struct Double4 : IFormattable
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double4), sizeof(Double4));

        [FieldOffset(0)]
        private double x;

        [FieldOffset(8)]
        private double y;

        [FieldOffset(16)]
        private double z;

        [FieldOffset(24)]
        private double w;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Double4"/> instance.
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
        /// Gets a reference to the <see cref="double"/> value representing the <c>W</c> component.
        /// </summary>
        public readonly ref double W => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.w), 1));

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
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 XW => ref *(Double2*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 YW => ref *(Double2*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 ZW => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 WX => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 WY => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 WZ => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double2 WW => ref *(Double2*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XXW => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 XYW => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 XZW => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XWX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 XWY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 XWZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 XWW => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 YXW => ref *(Double3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YYW => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 YZW => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 YWX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YWY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 YWZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 YWW => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ZXW => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ZYW => ref *(Double3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZZW => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ZWX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ZWY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZWZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ZWW => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WXX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 WXY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 WXZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WXW => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 WYX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WYY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 WYZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WYW => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 WZX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 WZY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WZZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WZW => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WWX => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WWY => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WWZ => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 WWW => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XXWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 XYZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 XYWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XYWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 XZYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 XZWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XZWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 XWYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 XWZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 XWWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 YXZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 YXWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YXWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YYWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 YZXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 YZWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YZWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 YWXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 YWZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 YWWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ZXYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ZXWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZXWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ZYXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ZYWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZYWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZZWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ZWXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ZWYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ZWWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 WXYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 WXZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WXWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 WYXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 WYZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WYWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 WZXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 WZYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WZWW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWXX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWXY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWXZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWXW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWYX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWYY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWYZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWYW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWZX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWZY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWZZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWZW => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWWX => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWWY => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWWZ => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 WWWW => ref *(Double4*)UndefinedData;

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
        /// Gets a reference to the <see cref="double"/> value representing the <c>A</c> component.
        /// </summary>
        public readonly ref double A => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.w), 1));

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
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 RA => ref *(Double2*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 GA => ref *(Double2*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 BA => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 AR => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 AG => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 AB => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double2 AA => ref *(Double2*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RRA => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 RGA => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 RBA => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RAR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 RAG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 RAB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 RAA => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 GRA => ref *(Double3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GGA => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 GBA => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 GAR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GAG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 GAB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 GAA => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 BRA => ref *(Double3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 BGA => ref *(Double3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BBA => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 BAR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 BAG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BAB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 BAA => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ARR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ARG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ARB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ARA => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 AGR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 AGG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 AGB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 AGA => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ABR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 ABG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ABB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 ABA => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 AAR => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 AAG => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 AAB => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double3 AAA => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RRAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 RGBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 RGAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RGAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 RBGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 RBAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RBAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RARR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RARG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RARB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RARA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RAGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RAGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 RAGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RAGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RABR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 RABG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RABB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RABA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RAAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RAAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RAAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 RAAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 GRBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 GRAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GRAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 GBRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 GBAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GBAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GARR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GARG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 GARB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GARA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GAGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GAGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GAGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GAGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 GABR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GABG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GABB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GABA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GAAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GAAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GAAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GAAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 BRGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 BRAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BRAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 BGRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 BGAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BGAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BBAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BARR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 BARG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BARB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BARA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 BAGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BAGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BAGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BAGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BABR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BABG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BABB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BABA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BAAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BAAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BAAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 BAAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ARGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ARBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ARAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 AGRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 AGBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AGAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABRR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ABRG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABRB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABRA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 ABGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABBR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABBG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABBB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABBA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 ABAA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AARR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AARG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AARB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AARA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AAGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AAGG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AAGB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AAGA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AABR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AABG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AABB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AABA => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AAAR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AAAG => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AAAB => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 AAAA => ref *(Double4*)UndefinedData;

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
        /// Negates a <see cref="Double4"/> value.
        /// </summary>
        /// <param name="xyzw">The <see cref="Double4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4 operator -(Double4 xyzw) => default;

        /// <summary>
        /// Sums two <see cref="Double4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4 operator +(Double4 left, Double4 right) => default;

        /// <summary>
        /// Divides two <see cref="Double4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4 operator /(Double4 left, Double4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4 operator *(Double4 left, Double4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4 operator -(Double4 left, Double4 right) => default;
    }
}
