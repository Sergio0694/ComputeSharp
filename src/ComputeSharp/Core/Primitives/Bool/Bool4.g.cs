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
    /// <inheritdoc cref="Bool4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public unsafe partial struct Bool4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool4), sizeof(Bool4));

        [FieldOffset(0)]
        private bool x;

        [FieldOffset(4)]
        private bool y;

        [FieldOffset(8)]
        private bool z;

        [FieldOffset(12)]
        private bool w;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Bool4"/> instance.
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
        /// Gets a reference to the <see cref="bool"/> value representing the <c>W</c> component.
        /// </summary>
        public readonly ref bool W => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.w), 1));

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
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 XW => ref *(Bool2*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 YW => ref *(Bool2*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 ZW => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 WX => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 WY => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 WZ => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 WW => ref *(Bool2*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XXW => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 XYW => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 XZW => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XWX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 XWY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 XWZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 XWW => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 YXW => ref *(Bool3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YYW => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 YZW => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 YWX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YWY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 YWZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 YWW => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ZXW => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ZYW => ref *(Bool3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZZW => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ZWX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ZWY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZWZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ZWW => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WXX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 WXY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 WXZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WXW => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 WYX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WYY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 WYZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WYW => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 WZX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 WZY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WZZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WZW => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WWX => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WWY => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WWZ => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 WWW => ref *(Bool3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XXWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 XYZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 XYWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XYWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 XZYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 XZWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XZWW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWXW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 XWYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWYW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 XWZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 XWWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 YXZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 YXWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YXWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YYWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 YZXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 YZWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YZWW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 YWXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWXW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWYW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 YWZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 YWWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ZXYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ZXWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZXWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ZYXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ZYWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZYWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZXW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZYW => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZZWW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ZWXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWXW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ZWYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWYW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ZWWW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXXW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 WXYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXYW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 WXZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WXWW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 WYXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYXW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYYW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 WYZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WYWW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 WZXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZXW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 WZYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZYW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WZWW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWXX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWXY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWXZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWXW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWYX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWYY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWYZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWYW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWZX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWZY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWZZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWZW => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWWX => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWWY => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWWZ => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 WWWW => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="bool"/> value representing the <c>A</c> component.
        /// </summary>
        public readonly ref bool A => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.w), 1));

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
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 RA => ref *(Bool2*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 GA => ref *(Bool2*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 BA => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 AR => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 AG => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 AB => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 AA => ref *(Bool2*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RRA => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 RGA => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 RBA => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RAR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 RAG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 RAB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 RAA => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 GRA => ref *(Bool3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GGA => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 GBA => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 GAR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GAG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 GAB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 GAA => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 BRA => ref *(Bool3*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 BGA => ref *(Bool3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BBA => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 BAR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 BAG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BAB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 BAA => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ARR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ARG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ARB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ARA => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 AGR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 AGG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 AGB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 AGA => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ABR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 ABG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ABB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 ABA => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 AAR => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 AAG => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 AAB => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool3 AAA => ref *(Bool3*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRGA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RRAA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGGA => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 RGBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 RGAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RGAA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 RBGA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 RBAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RBAA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RARR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RARG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RARB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RARA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RAGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RAGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 RAGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RAGA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RABR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 RABG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RABB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RABA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RAAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RAAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RAAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 RAAA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRGA => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 GRBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 GRAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GRAA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGGA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GGAA => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 GBRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBGA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 GBAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GBAA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GARR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GARG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 GARB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GARA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GAGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GAGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GAGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GAGA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 GABR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GABG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GABB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GABA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GAAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GAAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GAAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 GAAA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 BRGA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 BRAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BRAA => ref *(Bool4*)UndefinedData;

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
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 BGRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGGA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 BGAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BGAA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBRA => ref *(Bool4*)UndefinedData;

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
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBGA => ref *(Bool4*)UndefinedData;

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

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BBAA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BARR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 BARG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BARB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BARA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 BAGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BAGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BAGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BAGA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BABR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BABG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BABB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BABA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BAAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BAAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BAAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 BAAA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARRA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ARGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARGA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ARBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ARAA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 AGRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGRA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGGA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 AGBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AGAA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABRR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ABRG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABRB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABRA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 ABGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABGA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABBR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABBG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABBB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABBA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 ABAA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AARR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AARG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AARB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AARA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AAGR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AAGG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AAGB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AAGA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AABR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AABG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AABB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AABA => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AAAR => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AAAG => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AAAB => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool4 AAAA => ref *(Bool4*)UndefinedData;

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
            sb.Append(", ");
            sb.Append(this.w);
            sb.Append('>');

            return sb.ToString();
        }

        /// <summary>
        /// Negates a <see cref="Bool4"/> value.
        /// </summary>
        /// <param name="xyzw">The <see cref="Bool4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool4 operator !(Bool4 xyzw) => default;
    }
}
