using System.Diagnostics;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="double4"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z}, {W})")]
    public partial struct Double4
    {
        /// <summary>
        /// Gets an <see cref="Double4"/> value with all components set to 0.
        /// </summary>
        public static Double4 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Double4"/> value with all components set to 1.
        /// </summary>
        public static Double4 One => 1;

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double4 UnitX => new(1, 0, 0, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double4 UnitY => new(0, 1, 0, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double4 UnitZ => new(0, 0, 1, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double4 UnitW => new(0, 0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="Double4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public Double4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Creates a new <see cref="Double4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double4"/> instance.</param>
        public static implicit operator Double4(double x) => new(x, x, x, x);

        /// <summary>
        /// Casts a <see cref="Double4"/> value to a <see cref="Int4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Double4"/> value to cast.</param>
        public static explicit operator Int4(Double4 xyzw) => throw new InvalidExecutionContextException($"{nameof(Double4)}.({nameof(Int4)})");

        /// <summary>
        /// Casts a <see cref="Double4"/> value to a <see cref="UInt4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Double4"/> value to cast.</param>
        public static explicit operator UInt4(Double4 xyzw) => throw new InvalidExecutionContextException($"{nameof(Double4)}.({nameof(UInt4)})");

        /// <summary>
        /// Casts a <see cref="Double4"/> value to a <see cref="Float4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Double4"/> value to cast.</param>
        public static explicit operator Float4(Double4 xyzw) => throw new InvalidExecutionContextException($"{nameof(Double4)}.({nameof(Float4)})");
    }
}
