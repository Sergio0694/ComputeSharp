using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="UInt2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public partial struct UInt2
    {
        [FieldOffset(0)]
        private uint x;

        [FieldOffset(4)]
        private uint y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="UInt2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref uint this[int i] => throw new InvalidExecutionContextException($"{nameof(UInt2)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>X</c> component.
        /// </summary>
        public ref uint X => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref uint Y => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(Y)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt2 XX => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt2 XY => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt2 YX => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt2 YY => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>R</c> component.
        /// </summary>
        public ref uint R => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>G</c> component.
        /// </summary>
        public ref uint G => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(G)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt2 RR => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt2 RG => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt2 GR => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt2 GG => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(GG)}");

        /// <summary>
        /// Sums two <see cref="UInt2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt2"/> value to sum.</param>
        public static UInt2 operator +(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.+");

        /// <summary>
        /// Divides two <see cref="UInt2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt2"/> value to divide.</param>
        public static UInt2 operator /(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}./");

        /// <summary>
        /// Multiplies two <see cref="UInt2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt2"/> value to multiply.</param>
        public static UInt2 operator *(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.*");

        /// <summary>
        /// Subtracts two <see cref="UInt2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt2"/> value to subtract.</param>
        public static UInt2 operator -(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.-");
    }
}
