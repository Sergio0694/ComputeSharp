using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="double3"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(double) * 3)]
    public partial struct Double3
    {
        /// <summary>
        /// Gets an <see cref="Double3"/> value with all components set to 0.
        /// </summary>
        public static Double3 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Double3"/> value with all components set to 1.
        /// </summary>
        public static Double3 One => 1;

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double3 UnitX => new(1, 0, 0);

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double3 UnitY => new(0, 1, 0);

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double3 UnitZ => new(0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="Double3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        public Double3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
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
        /// Gets or sets the value of the first color component.
        /// </summary>
        public double R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public double G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component.
        /// </summary>
        public double B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(B)}");
        }

        /// <summary>
        /// Creates a new <see cref="Double3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double3"/> instance.</param>
        public static implicit operator Double3(double x) => new(x, x, x);

        /// <summary>
        /// Casts a <see cref="Double3"/> value to a <see cref="Int3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Double3"/> value to cast.</param>
        public static explicit operator Int3(Double3 xyz) => throw new InvalidExecutionContextException($"{nameof(Double3)}.({nameof(Int3)})");

        /// <summary>
        /// Casts a <see cref="Double3"/> value to a <see cref="UInt3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Double3"/> value to cast.</param>
        public static explicit operator UInt3(Double3 xyz) => throw new InvalidExecutionContextException($"{nameof(Double3)}.({nameof(UInt3)})");

        /// <summary>
        /// Casts a <see cref="Double3"/> value to a <see cref="Float3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Double3"/> value to cast.</param>
        public static explicit operator Float3(Double3 xyz) => throw new InvalidExecutionContextException($"{nameof(Double3)}.({nameof(Float3)})");
    }
}
