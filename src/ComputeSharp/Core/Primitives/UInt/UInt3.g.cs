using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="UInt3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public partial struct UInt3
    {
        [FieldOffset(0)]
        private uint x;

        [FieldOffset(4)]
        private uint y;

        [FieldOffset(8)]
        private uint z;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="UInt3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref uint this[int i] => throw new InvalidExecutionContextException($"{typeof(UInt3)}[{typeof(int)}]");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>X</c> component.
        /// </summary>
        public ref uint X => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref uint Y => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref uint Z => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(Z)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt2 XX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt2 XY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt2 XZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt2 YX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt2 YY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt2 YZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt2 ZX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt2 ZY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt2 ZZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XXX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 XXY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 XXZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XYX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 XYY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 XYZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XZX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 XZY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 XZZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 YXX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YXY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 YXZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 YYX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YYY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 YYZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 YZX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YZY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 YZZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 ZXX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 ZXY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZXZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 ZYX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 ZYY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZYZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 ZZX => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 ZZY => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZZZ => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>R</c> component.
        /// </summary>
        public ref uint R => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>G</c> component.
        /// </summary>
        public ref uint G => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>B</c> component.
        /// </summary>
        public ref uint B => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(B)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt2 RR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt2 RG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt2 RB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt2 GR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt2 GG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt2 GB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt2 BR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt2 BG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt2 BB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RRR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 RRG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 RRB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RGR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 RGG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 RGB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RBR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 RBG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 RBB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(RBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 GRR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GRG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 GRB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 GGR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GGG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 GGB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 GBR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GBG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 GBB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(GBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 BRR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 BRG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BRB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 BGR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 BGG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BGB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 BBR => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 BBG => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BBB => throw new InvalidExecutionContextException($"{typeof(UInt3)}.{nameof(BBB)}");

        /// <summary>
        /// Sums two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to sum.</param>
        public static UInt3 operator +(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{typeof(UInt3)}.+");

        /// <summary>
        /// Divides two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to divide.</param>
        public static UInt3 operator /(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{typeof(UInt3)}./");

        /// <summary>
        /// Multiplies two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to multiply.</param>
        public static UInt3 operator *(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{typeof(UInt3)}.*");

        /// <summary>
        /// Subtracts two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to subtract.</param>
        public static UInt3 operator -(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{typeof(UInt3)}.-");
    }
}
