using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A locally unique identifier for a graphics device.
    /// </summary>
    public readonly struct Luid : IEquatable<Luid>
    {
        private readonly uint lowPart;
        private readonly int highPart;

        /// <inheritdoc/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Luid other)
        {
            return
                this.lowPart == other.lowPart &&
                this.highPart == other.highPart;
        }

        /// <inheritdoc/>
        [Pure]
        public override bool Equals(object? other)
        {
            return other is Luid luid && Equals(luid);
        }

        /// <inheritdoc/>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(this.lowPart, this.highPart);
        }

        /// <inheritdoc/>
        [Pure]
        public override string ToString()
        {
            return (((long)this.highPart) << 32 | this.lowPart).ToString();
        }

        /// <summary>
        /// Check whether two <see cref="Luid"/> values are equal.
        /// </summary>
        /// <param name="a">The first <see cref="Luid"/> value to compare.</param>
        /// <param name="b">The second <see cref="Luid"/> value to compare.</param>
        /// <returns>Whether <paramref name="a"/> and <paramref name="b"/> are the same.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Luid a, Luid b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Check whether two <see cref="Luid"/> values are different.
        /// </summary>
        /// <param name="a">The first <see cref="Luid"/> value to compare.</param>
        /// <param name="b">The second <see cref="Luid"/> value to compare.</param>
        /// <returns>Whether <paramref name="a"/> and <paramref name="b"/> are different.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Luid a, Luid b)
        {
            return !a.Equals(b);
        }
    }
}
