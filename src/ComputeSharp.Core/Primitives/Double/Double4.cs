using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="double4"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z}, {W})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(double) * 4)]
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
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the third vector component.
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// Gets or sets the value of the fourth vector component.
        /// </summary>
        public double W { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component.
        /// </summary>
        public double R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public double G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component.
        /// </summary>
        public double B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets the value of the fourth color component.
        /// </summary>
        public double A
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(A)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(A)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Double4"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public double this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}[int]");
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
