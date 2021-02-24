using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="uint4"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z}, {W})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint) * 4)]
    public partial struct UInt4
    {
        /// <summary>
        /// Gets an <see cref="UInt4"/> value with all components set to 0.
        /// </summary>
        public static UInt4 Zero => 0;

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with all components set to 1.
        /// </summary>
        public static UInt4 One => 1;

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static UInt4 UnitX => new(1, 0, 0, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static UInt4 UnitY => new(0, 1, 0, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
        /// </summary>
        public static UInt4 UnitZ => new(0, 0, 1, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/> component set to 1, and the others to 0.
        /// </summary>
        public static UInt4 UnitW => new(0, 0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="UInt4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public UInt4(uint x, uint y, uint z, uint w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component.
        /// </summary>
        public uint X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component.
        /// </summary>
        public uint Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the third vector component.
        /// </summary>
        public uint Z { get; set; }

        /// <summary>
        /// Gets or sets the value of the fourth vector component.
        /// </summary>
        public uint W { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component.
        /// </summary>
        public uint R
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public uint G
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component.
        /// </summary>
        public uint B
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets the value of the fourth color component.
        /// </summary>
        public uint A
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(A)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(A)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="UInt4"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public uint this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}[uint]");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}[uint]");
        }

        /// <summary>
        /// Creates a new <see cref="UInt4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt4"/> instance.</param>
        public static implicit operator UInt4(uint x) => new(x, x, x, x);

        /// <summary>
        /// Casts a <see cref="UInt4"/> value to a <see cref="Int4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
        public static explicit operator Int4(UInt4 xyzw) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.({nameof(Int4)})");

        /// <summary>
        /// Casts a <see cref="UInt4"/> value to a <see cref="Float4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
        public static implicit operator Float4(UInt4 xyzw) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.({nameof(Float4)})");

        /// <summary>
        /// Casts a <see cref="UInt4"/> value to a <see cref="Double4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
        public static implicit operator Double4(UInt4 xyzw) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.({nameof(Double4)})");
    }
}
