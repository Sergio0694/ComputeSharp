using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TerraFX.Interop;

namespace ComputeSharp
{
    /// <summary>
    /// A locally unique identifier for a graphics device.
    /// </summary>
    public readonly struct Luid : IEquatable<Luid>
    {
        #pragma warning disable CS0649

        private readonly uint lowPart;
        private readonly int highPart;

        #pragma warning restore

        /// <summary>
        /// Creates a new <see cref="Luid"/> instance from a raw <see cref="LUID"/> value.
        /// </summary>
        /// <param name="luid">The input <see cref="LUID"/> value.</param>
        /// <returns>A <see cref="Luid"/> instance with the same value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Luid FromLUID(LUID luid)
        {
            return *(Luid*)&luid;
        }

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
