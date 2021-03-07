using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.__Internals;

#pragma warning disable CS0618

namespace ComputeSharp
{
    /// <summary>
    /// Packed pixel type containing a single 16-bit unsigned normalized channel value.
    /// <para>
    /// Ranges from [0, 0, 0, 0] to [1, 0, 0, 0] in vector form.
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct R16 : IEquatable<R16>, IUnorm<float>
    {
        /// <summary>
        /// The red component.
        /// </summary>
        public ushort R;

        /// <summary>
        /// Initializes a new instance of the <see cref="R16"/> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        public R16(ushort r)
        {
            R = r;
        }

        /// <summary>
        /// Gets or sets the packed representation of the <see cref="R16"/> struct.
        /// </summary>
        public ushort PackedValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => Unsafe.As<R16, ushort>(ref Unsafe.AsRef(this));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Unsafe.As<R16, ushort>(ref this) = value;
        }

        /// <summary>
        /// Compares two <see cref="R16"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="R16"/> on the left side of the operand.</param>
        /// <param name="right">The <see cref="R16"/> on the right side of the operand.</param>
        /// <returns>
        /// True if the <paramref name="left"/> parameter is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(R16 left, R16 right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="R16"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="R16"/> on the left side of the operand.</param>
        /// <param name="right">The <see cref="R16"/> on the right side of the operand.</param>
        /// <returns>
        /// True if the <paramref name="left"/> parameter is not equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(R16 left, R16 right) => !left.Equals(right);

        /// <inheritdoc/>
        public override readonly bool Equals(object? obj) => obj is R16 R16 && Equals(R16);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(R16 other) => PackedValue.Equals(other.PackedValue);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly int GetHashCode() => PackedValue.GetHashCode();

        /// <inheritdoc/>
        public override readonly string ToString() => $"{nameof(R16)}({this.R})";
    }
}
