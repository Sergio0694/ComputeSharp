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
    /// <inheritdoc cref="Bool3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct Bool3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool3), sizeof(Bool4));

        [FieldOffset(0)]
        private bool x;

        [FieldOffset(4)]
        private bool y;

        [FieldOffset(8)]
        private bool z;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Bool3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref bool this[int i] => ref *(bool*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>X</c> component.
        /// </summary>
        public readonly ref bool X => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>Y</c> component.
        /// </summary>
        public readonly ref bool Y => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>Z</c> component.
        /// </summary>
        public readonly ref bool Z => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 XX => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 XY => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 XZ => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 YX => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 YY => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 YZ => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 ZX => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 ZY => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 ZZ => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XXX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XXY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XXZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XYX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XYY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 XYZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XZX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 XZY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XZZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YXX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YXY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 YXZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YYX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YYY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YYZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 YZX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YZY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YZZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZXX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ZXY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZXZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ZYX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZYY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZYZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZZX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZZY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZZZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>R</c> component.
        /// </summary>
        public readonly ref bool R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>G</c> component.
        /// </summary>
        public readonly ref bool G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>B</c> component.
        /// </summary>
        public readonly ref bool B => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.z), 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 RR => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 RG => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 RB => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 GR => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 GG => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 GB => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 BR => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 BG => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 BB => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RRR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RRG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RRB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RGR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RGG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 RGB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RBR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 RBG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RBB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GRR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GRG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 GRB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GGR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GGG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GGB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 GBR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GBG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GBB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BRR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 BRG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BRB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 BGR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BGG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BGB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BBR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BBG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BBB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBBB => ref *(Bool4*)UndefinedData;

        /// <inheritdoc/>
        public override readonly string ToString()
        {
            StringBuilder sb = new();

            sb.Append('<');
            sb.Append(this.x);
            sb.Append(", ");
            sb.Append(this.y);
            sb.Append(", ");
            sb.Append(this.z);
            sb.Append('>');

            return sb.ToString();
        }

        /// <summary>
        /// Negates a <see cref="Bool3"/> value.
        /// </summary>
        /// <param name="xyz">The <see cref="Bool3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool3 operator !(Bool3 xyz) => default;
    }
}
