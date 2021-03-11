using System.Runtime.InteropServices;
#if NET5_0
using System.Runtime.CompilerServices;
#else
using RuntimeHelpers = ComputeSharp.SourceGenerators.Helpers.RuntimeHelpers;
using MemoryMarshal = ComputeSharp.SourceGenerators.Helpers.MemoryMarshal;
#endif

namespace ComputeSharp
{
    /// <inheritdoc cref="Int2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public unsafe partial struct Int2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Int2), sizeof(Int2));

        [FieldOffset(0)]
        private int x;

        [FieldOffset(4)]
        private int y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Int2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref int this[int i] => ref *(int*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>X</c> component.
        /// </summary>
        public ref int X => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.x, 1));

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref int Y => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.y, 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Int2 XX => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Int2 XY => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Int2 YX => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Int2 YY => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>R</c> component.
        /// </summary>
        public ref int R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.x, 1));

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>G</c> component.
        /// </summary>
        public ref int G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this.y, 1));

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Int2 RR => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Int2 RG => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref Int2 GR => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public ref readonly Int2 GG => ref *(Int2*)UndefinedData;

        /// <summary>
        /// Negates a <see cref="Int2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Int2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int2 operator -(Int2 xy) => default;

        /// <summary>
        /// Sums two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int2 operator +(Int2 left, Int2 right) => default;

        /// <summary>
        /// Divides two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int2 operator /(Int2 left, Int2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int2 operator *(Int2 left, Int2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Int2 operator -(Int2 left, Int2 right) => default;
    }
}
