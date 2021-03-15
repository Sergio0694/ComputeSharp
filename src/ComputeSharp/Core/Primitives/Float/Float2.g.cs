using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if !NET5_0
using RuntimeHelpers = ComputeSharp.SourceGenerators.Helpers.RuntimeHelpers;
using MemoryMarshal = ComputeSharp.SourceGenerators.Helpers.MemoryMarshal;
#endif

namespace ComputeSharp
{
    /// <inheritdoc cref="Float2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public unsafe partial struct Float2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Float2), sizeof(Float2));

        [FieldOffset(0)]
        private float x;

        [FieldOffset(4)]
        private float y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Float2"/> instance.
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
        /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
        /// </summary>
        public readonly ref float R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
        /// </summary>
        public readonly ref float G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

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
        /// Negates a <see cref="Float2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Float2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float2 operator -(Float2 xy) => default;

        /// <summary>
        /// Sums two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float2 operator +(Float2 left, Float2 right) => default;

        /// <summary>
        /// Divides two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float2 operator /(Float2 left, Float2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float2 operator *(Float2 left, Float2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Float2 operator -(Float2 left, Float2 right) => default;
    }
}
