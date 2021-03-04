using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Double2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    public partial struct Double2
    {
        [FieldOffset(0)]
        private double x;

        [FieldOffset(8)]
        private double y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Double2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref double this[int i] => throw new InvalidExecutionContextException($"{nameof(Double2)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>X</c> component.
        /// </summary>
        public ref double X => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref double Y => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref double Z => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>W</c> component.
        /// </summary>
        public ref double W => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(W)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double2 XX => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 XY => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 XZ => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 XW => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(XW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 YX => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double2 YY => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 YZ => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 YW => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(YW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 ZX => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 ZY => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double2 ZZ => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 ZW => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(ZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 WX => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(WX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 WY => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(WY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 WZ => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(WZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double2 WW => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(WW)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>R</c> component.
        /// </summary>
        public ref double R => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>G</c> component.
        /// </summary>
        public ref double G => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>B</c> component.
        /// </summary>
        public ref double B => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>A</c> component.
        /// </summary>
        public ref double A => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double2 RR => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 RG => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 RB => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 RA => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(RA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 GR => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double2 GG => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 GB => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 GA => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(GA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 BR => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 BG => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double2 BB => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(BB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 BA => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(BA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 AR => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(AR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 AG => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(AG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 AB => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(AB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double2 AA => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(AA)}");

        /// <summary>
        /// Negates a <see cref="Double2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Double2"/> value to negate.</param>
        public static Double2 operator -(Double2 xy) => throw new InvalidExecutionContextException($"{nameof(Double2)}.-");

        /// <summary>
        /// Sums two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2"/> value to sum.</param>
        public static Double2 operator +(Double2 left, Double2 right) => throw new InvalidExecutionContextException($"{nameof(Double2)}.+");

        /// <summary>
        /// Divides two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2"/> value to divide.</param>
        public static Double2 operator /(Double2 left, Double2 right) => throw new InvalidExecutionContextException($"{nameof(Double2)}./");

        /// <summary>
        /// Multiplies two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2"/> value to multiply.</param>
        public static Double2 operator *(Double2 left, Double2 right) => throw new InvalidExecutionContextException($"{nameof(Double2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2"/> value to subtract.</param>
        public static Double2 operator -(Double2 left, Double2 right) => throw new InvalidExecutionContextException($"{nameof(Double2)}.-");
    }
}
