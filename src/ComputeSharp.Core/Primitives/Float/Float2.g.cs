using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Float2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Float2
    {
        [FieldOffset(0)]
        private float x;

        [FieldOffset(4)]
        private float y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Float2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref float this[int i] => throw new InvalidExecutionContextException($"{nameof(Float2)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>X</c> component.
        /// </summary>
        public ref float X => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref float Y => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
        /// </summary>
        public ref float R => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
        /// </summary>
        public ref float G => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(G)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float2 XX => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 XY => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 YX => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float2 YY => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float2 RR => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 RG => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 GR => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float2 GG => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GG)}");

        /// <summary>
        /// Negates a <see cref="Float2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Float2"/> value to negate.</param>
        public static Float2 operator -(Float2 xy) => throw new InvalidExecutionContextException($"{nameof(Float2)}.-");

        /// <summary>
        /// Sums two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float2"/> value to sum.</param>
        public static Float2 operator +(Float2 left, Float2 right) => throw new InvalidExecutionContextException($"{nameof(Float2)}.+");

        /// <summary>
        /// Divides two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float2"/> value to divide.</param>
        public static Float2 operator /(Float2 left, Float2 right) => throw new InvalidExecutionContextException($"{nameof(Float2)}./");

        /// <summary>
        /// Multiplies two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float2"/> value to multiply.</param>
        public static Float2 operator *(Float2 left, Float2 right) => throw new InvalidExecutionContextException($"{nameof(Float2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float2"/> value to subtract.</param>
        public static Float2 operator -(Float2 left, Float2 right) => throw new InvalidExecutionContextException($"{nameof(Float2)}.-");
    }
}
