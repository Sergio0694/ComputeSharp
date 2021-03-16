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
    /// <inheritdoc cref="Double2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    public unsafe partial struct Double2 : IFormattable
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double2), sizeof(Double4));

        [FieldOffset(0)]
        private double x;

        [FieldOffset(8)]
        private double y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Double2"/> instance.
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
        /// Gets a reference to the <see cref="double"/> value representing the <c>R</c> component.
        /// </summary>
        public readonly ref double R => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.x), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>G</c> component.
        /// </summary>
        public readonly ref double G => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.y), 1));

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
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGGR => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref readonly Double4 GGGG => ref *(Double4*)UndefinedData;

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
            sb.Append('>');

            return sb.ToString();
        }

        /// <summary>
        /// Negates a <see cref="Double2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Double2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2 operator -(Double2 xy) => default;

        /// <summary>
        /// Sums two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2 operator +(Double2 left, Double2 right) => default;

        /// <summary>
        /// Divides two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2 operator /(Double2 left, Double2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2 operator *(Double2 left, Double2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2 operator -(Double2 left, Double2 right) => default;
    }
}
