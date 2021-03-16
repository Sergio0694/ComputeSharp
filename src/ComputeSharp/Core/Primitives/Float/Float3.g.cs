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
    /// <inheritdoc cref="Float3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct Float3 : IFormattable
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Float3), sizeof(Float4));

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
        public readonly ref float this[int i] => ref *(float*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>X</c> component.
        /// </summary>
        public readonly ref float X => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Y</c> component.
        /// </summary>
        public readonly ref float Y => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Z</c> component.
        /// </summary>
        public readonly ref float Z => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float2 XX => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 XY => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 XZ => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 YX => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float2 YY => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 YZ => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 ZX => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 ZY => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float2 ZZ => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 XXX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 XXY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 XXZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 XYX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 XYY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 XYZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 XZX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 XZY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 XZZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 YXX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 YXY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 YXZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 YYX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 YYY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 YYZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 YZX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 YZY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 YZZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 ZXX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 ZXY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 ZXZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 ZYX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 ZYY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 ZYZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 ZZX => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 ZZY => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 ZZZ => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XXZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XYZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 XZZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YXZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YYZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 YZZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZXZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZYZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZXX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZXY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZXZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZYX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZYY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZYZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZZX => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZZY => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 ZZZZ => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
        /// </summary>
        public readonly ref float R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
        /// </summary>
        public readonly ref float G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>B</c> component.
        /// </summary>
        public readonly ref float B => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float2 RR => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 RG => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 RB => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 GR => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float2 GG => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 GB => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 BR => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float2 BG => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float2 BB => ref *(Float2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 RRR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 RRG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 RRB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 RGR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 RGG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 RGB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 RBR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 RBG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 RBB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 GRR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 GRG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 GRB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 GGR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 GGG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 GGB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 GBR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 GBG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 GBB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 BRR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 BRG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 BRB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Float3 BGR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 BGG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 BGB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 BBR => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 BBG => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float3 BBB => ref *(Float3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RRBB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RGBB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 RBBB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GRBB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GGBB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 GBBB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BRBB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BGBB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBRR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBRG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBRB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBGR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBGG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBGB => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBBR => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBBG => ref *(Float4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Float4 BBBB => ref *(Float4*)UndefinedData;

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
