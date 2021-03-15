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
    /// <inheritdoc cref="Bool2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public unsafe partial struct Bool2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool2), sizeof(Bool2));

        [FieldOffset(0)]
        private bool x;

        [FieldOffset(4)]
        private bool y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Bool2"/> instance.
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
        /// Gets a reference to the <see cref="bool"/> value representing the <c>R</c> component.
        /// </summary>
        public readonly ref bool R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>G</c> component.
        /// </summary>
        public readonly ref bool G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

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
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 GR => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Bool2 GG => ref *(Bool2*)UndefinedData;

        /// <inheritdoc/>
        public override readonly string ToString()
        {
            StringBuilder sb = new();

            sb.Append('<');
            sb.Append(this.x);
            sb.Append(", ");
            sb.Append(this.y);
            sb.Append('>');

            return sb.ToString();
        }

        /// <summary>
        /// Negates a <see cref="Bool2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Bool2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool2 operator !(Bool2 xy) => default;
    }
}
