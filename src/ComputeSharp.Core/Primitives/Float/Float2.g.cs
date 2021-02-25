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
        /// Gets a reference to the <see cref="float"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref float Z => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>W</c> component.
        /// </summary>
        public ref float W => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(W)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float2 XX => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 XY => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 XZ => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 XW => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 YX => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float2 YY => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 YZ => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 YW => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 ZX => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 ZY => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float2 ZZ => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 ZW => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(ZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 WX => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(WX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 WY => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(WY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 WZ => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(WZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float2 WW => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(WW)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
        /// </summary>
        public ref float R => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
        /// </summary>
        public ref float G => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>B</c> component.
        /// </summary>
        public ref float B => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>A</c> component.
        /// </summary>
        public ref float A => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float2 RR => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 RG => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 RB => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 RA => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 GR => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float2 GG => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 GB => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 GA => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 BR => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 BG => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float2 BB => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(BB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 BA => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(BA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 AR => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(AR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 AG => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(AG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 AB => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(AB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float2 AA => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(AA)}");

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
