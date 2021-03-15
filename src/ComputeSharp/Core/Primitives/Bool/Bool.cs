using System.Runtime.InteropServices;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that can be used in place of the <see cref="bool"/> type in HLSL shaders.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int), Pack = 4)]
    public readonly struct Bool
    {
        /// <summary>
        /// The wrapped <see cref="bool"/> value for the current instance.
        /// </summary>
        [FieldOffset(0)]
        private readonly bool Value;

        /// <summary>
        /// Creates a new <see cref="Bool"/> instance for a given <see cref="bool"/> value.
        /// </summary>
        /// <param name="value">.</param>
        private Bool(bool value)
        {
            Value = value;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is Bool x && Value == x.Value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Checks whether or not two <see cref="Bool"/> instances represent the same <see cref="bool"/> value.
        /// </summary>
        /// <param name="left">The left <see cref="Bool"/> instance.</param>
        /// <param name="right">The right <see cref="Bool"/> instance.</param>
        public static bool operator ==(Bool left, Bool right) => left.Value == right.Value;

        /// <summary>
        /// Checks whether or not two <see cref="Bool"/> instances represent a different <see cref="bool"/> value.
        /// </summary>
        /// <param name="left">The left <see cref="Bool"/> instance.</param>
        /// <param name="right">The right <see cref="Bool"/> instance.</param>
        public static bool operator !=(Bool left, Bool right) => left.Value != right.Value;

        /// <summary>
        /// Inverts the <see cref="bool"/> value represented by a given <see cref="Bool"/> instance.
        /// </summary>
        /// <param name="x">The input <see cref="Bool"/> instance.</param>
        public static Bool operator !(Bool x) => new(!x.Value);

        /// <summary>
        /// Converts a given <see cref="Bool"/> instance to its corresponding <see cref="bool"/> value.
        /// </summary>
        /// <param name="x">The input <see cref="Bool"/> instance.</param>
        public static implicit operator bool(Bool x) => x.Value;

        /// <summary>
        /// Converts a <see cref="bool"/> value to a corresponding <see cref="Bool"/> instance.
        /// </summary>
        /// <param name="x">The input <see cref="bool"/> value.</param>
        public static implicit operator Bool(bool x) => new(x);
    }
}
