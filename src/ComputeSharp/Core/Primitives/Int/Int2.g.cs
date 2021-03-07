using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Int2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public partial struct Int2
    {
        [FieldOffset(0)]
        private int x;

        [FieldOffset(4)]
        private int y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Int2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref int this[int i] => throw new InvalidExecutionContextException($"{typeof(Int2)}[{typeof(int)}]");

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>X</c> component.
        /// </summary>
        public ref int X => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref int Y => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(Y)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Int2 XX => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Int2 XY => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Int2 YX => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Int2 YY => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>R</c> component.
        /// </summary>
        public ref int R => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="int"/> value representing the <c>G</c> component.
        /// </summary>
        public ref int G => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(G)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Int2 RR => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Int2 RG => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Int2 GR => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Int2 GG => throw new InvalidExecutionContextException($"{typeof(Int2)}.{nameof(GG)}");

        /// <summary>
        /// Negates a <see cref="Int2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Int2"/> value to negate.</param>
        public static Int2 operator -(Int2 xy) => throw new InvalidExecutionContextException($"{typeof(Int2)}.-");

        /// <summary>
        /// Sums two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int2"/> value to sum.</param>
        public static Int2 operator +(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{typeof(Int2)}.+");

        /// <summary>
        /// Divides two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int2"/> value to divide.</param>
        public static Int2 operator /(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{typeof(Int2)}./");

        /// <summary>
        /// Multiplies two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int2"/> value to multiply.</param>
        public static Int2 operator *(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{typeof(Int2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int2"/> value to subtract.</param>
        public static Int2 operator -(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{typeof(Int2)}.-");
    }
}
