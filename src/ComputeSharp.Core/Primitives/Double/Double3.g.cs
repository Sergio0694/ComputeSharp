using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Double3"/>
    public partial struct Double3
    {
        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Double3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref double this[int i] => throw new InvalidExecutionContextException($"{nameof(Double3)}[int]");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double2 XX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 XY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 YX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double2 YY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double2 RR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 RG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 GR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double2 GG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 XXX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 XXY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 XXZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 XYX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 XYY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 XYZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 XZX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 XZY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 XZZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 YXX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 YXY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 YXZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 YYX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 YYY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 YYZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 YZX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 YZY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 YZZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 ZXX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 ZXY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 ZXZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 ZYX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 ZYY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 ZYZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 ZZX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 ZZY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 ZZZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 RRR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 RRG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 RRB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 RGR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 RGG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 RGB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 RBR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 RBG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 RBB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 GRR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 GRG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 GRB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 GGR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 GGG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 GGB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 GBR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 GBG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 GBB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 BRR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 BRG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 BRB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 BGR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 BGG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 BGB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 BBR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 BBG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 BBB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BBB)}");

        /// <summary>
        /// Negates a <see cref="Double3"/> value.
        /// </summary>
        /// <param name="xyz">The <see cref="Double3"/> value to negate.</param>
        public static Double3 operator -(Double3 xyz) => throw new InvalidExecutionContextException($"{nameof(Double3)}.-");

        /// <summary>
        /// Sums two <see cref="Double3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3"/> value to sum.</param>
        public static Double3 operator +(Double3 left, Double3 right) => throw new InvalidExecutionContextException($"{nameof(Double3)}.+");

        /// <summary>
        /// Divides two <see cref="Double3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3"/> value to divide.</param>
        public static Double3 operator /(Double3 left, Double3 right) => throw new InvalidExecutionContextException($"{nameof(Double3)}./");

        /// <summary>
        /// Multiplies two <see cref="Double3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3"/> value to multiply.</param>
        public static Double3 operator *(Double3 left, Double3 right) => throw new InvalidExecutionContextException($"{nameof(Double3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3"/> value to subtract.</param>
        public static Double3 operator -(Double3 left, Double3 right) => throw new InvalidExecutionContextException($"{nameof(Double3)}.-");
    }
}
