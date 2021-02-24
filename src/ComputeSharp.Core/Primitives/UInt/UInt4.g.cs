using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="UInt4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct UInt4
    {
        [FieldOffset(0)]
        private uint x;

        [FieldOffset(4)]
        private uint y;

        [FieldOffset(8)]
        private uint z;

        [FieldOffset(12)]
        private uint w;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="UInt4"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref uint this[int i] => throw new InvalidExecutionContextException($"{nameof(UInt4)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>X</c> component.
        /// </summary>
        public ref uint X => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref uint Y => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref uint Z => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>W</c> component.
        /// </summary>
        public ref uint W => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(W)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>R</c> component.
        /// </summary>
        public ref uint R => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>G</c> component.
        /// </summary>
        public ref uint G => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>B</c> component.
        /// </summary>
        public ref uint B => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>A</c> component.
        /// </summary>
        public ref uint A => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt2 XX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt2 XY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt2 YX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt2 YY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt2 RR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt2 RG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt2 GR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt2 GG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 XXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 XXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 XYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 XYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 XZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 XZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 YXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 YXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 YYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 YYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 YZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 YZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 ZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 ZXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 ZYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 ZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 ZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 ZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 RRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 RRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 RGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 RGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 RBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 RBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 GRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 GRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 GGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 GGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 GBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 GBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 BRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 BRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 BGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 BGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 BBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 BBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XXXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XXXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XXXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XXXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XXYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XXYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XXYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XXYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XXZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XXZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XXZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XXZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XXWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XXWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XXWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XXWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XYXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XYXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XYXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XYXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XYYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XYYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XYYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XYYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XYZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XYZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XYZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt4 XYZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XYWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XYWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt4 XYWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XYWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XZXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XZXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XZYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt4 XZYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XZWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt4 XZWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XWXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XWXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XWXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XWXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XWYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XWYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt4 XWYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XWYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XWZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt4 XWZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XWZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XWZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 XWWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 XWWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 XWWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 XWWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YXXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YXXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YXXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YXXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YXYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YXYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YXYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YXYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YXZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YXZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YXZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt4 YXZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YXWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YXWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt4 YXWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YXWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YYXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YYXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YYXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YYXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YYYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YYYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YYYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YYYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YYZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YYZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YYZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YYZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YYWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YYWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YYWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YYWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YZXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt4 YZXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YZYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YZYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt4 YZWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YZWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YWXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YWXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt4 YWXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YWXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YWYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YWYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YWYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YWYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt4 YWZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YWZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YWZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YWZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 YWWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 YWWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 YWWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 YWWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZXXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZXXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZXXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZXXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZXYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZXYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZXYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt4 ZXYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZXZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZXZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZXZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZXZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZXWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt4 ZXWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZXWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZXWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZYXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZYXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZYXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt4 ZYXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZYYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZYYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZYYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZYYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZYZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZYZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZYZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZYZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt4 ZYWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZYWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZYWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZYWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZZXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZZXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZZYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZZYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZZWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZZWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZWXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt4 ZWXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZWXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZWXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt4 ZWYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZWYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZWYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZWYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZWZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZWZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZWZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZWZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 ZWWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 ZWWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 ZWWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 ZWWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WXXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WXXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WXXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WXXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WXYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WXYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt4 WXYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WXYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WXZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt4 WXZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WXZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WXZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WXWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WXWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WXWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WXWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WYXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WYXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt4 WYXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WYXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WYYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WYYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WYYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WYYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt4 WYZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WYZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WYZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WYZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WYWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WYWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WYWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WYWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt4 WZXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WZXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt4 WZYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WZYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WZWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WZWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WWXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WWXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WWXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WWXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WWYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WWYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WWYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WWYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WWZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WWZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WWZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WWZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt4 WWWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt4 WWWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt4 WWWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt4 WWWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RRRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RRRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RRRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RRRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RRGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RRGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RRGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RRGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RRBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RRBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RRBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RRBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RRAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RRAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RRAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RRAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RGRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RGRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RGRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RGRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RGGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RGGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RGGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RGGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RGBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RGBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RGBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt4 RGBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RGAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RGAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt4 RGAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RGAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RBRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RBRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RBRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RBRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RBGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RBGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RBGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt4 RBGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RBBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RBBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RBBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RBBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RBAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt4 RBAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RBAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RBAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RARG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RARB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RAGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RAGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt4 RAGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RAGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RABR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt4 RABG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 RAAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 RAAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 RAAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 RAAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GRRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GRRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GRRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GRRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GRGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GRGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GRGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GRGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GRBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GRBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GRBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt4 GRBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GRAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GRAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt4 GRAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GRAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GGRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GGRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GGRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GGRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GGGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GGGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GGGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GGGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GGBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GGBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GGBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GGBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GGAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GGAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GGAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GGAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GBRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GBRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GBRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt4 GBRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GBGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GBGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GBGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GBGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GBBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GBBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GBBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GBBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt4 GBAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GBAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GBAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GBAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GARG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt4 GARB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GAGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GAGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GAGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GAGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt4 GABR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GABG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 GAAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 GAAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 GAAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 GAAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BRRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BRRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BRRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BRRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BRGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BRGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BRGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt4 BRGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BRBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BRBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BRBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BRBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BRAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt4 BRAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BRAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BRAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BGRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BGRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BGRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt4 BGRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BGGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BGGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BGGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BGGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BGBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BGBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BGBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BGBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt4 BGAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BGAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BGAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BGAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BBRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BBRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BBRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BBRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BBGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BBGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BBGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BBGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BBBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BBBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BBBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BBBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BBAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BBAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BBAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BBAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt4 BARG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BARB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt4 BAGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BAGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BAGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BAGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BABR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BABG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 BAAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 BAAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 BAAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 BAAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 ARRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 ARRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 ARRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 ARRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 ARGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 ARGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt4 ARGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 ARGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 ARBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt4 ARBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 ARBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 ARBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 ARAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 ARAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 ARAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 ARAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 AGRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 AGRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt4 AGRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 AGRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 AGGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 AGGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 AGGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 AGGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt4 AGBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 AGBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 AGBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 AGBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 AGAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 AGAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 AGAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 AGAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 ABRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt4 ABRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 ABRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 ABRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt4 ABGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 ABGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 ABGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 ABGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 ABBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 ABBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 ABBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 ABBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 ABAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 ABAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 ABAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 ABAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 AARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 AARG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 AARB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 AARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 AAGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 AAGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 AAGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 AAGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 AABR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 AABG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 AABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 AABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt4 AAAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt4 AAAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt4 AAAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt4 AAAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAAA)}");

        /// <summary>
        /// Sums two <see cref="UInt4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt4"/> value to sum.</param>
        public static UInt4 operator +(UInt4 left, UInt4 right) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.+");

        /// <summary>
        /// Divides two <see cref="UInt4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt4"/> value to divide.</param>
        public static UInt4 operator /(UInt4 left, UInt4 right) => throw new InvalidExecutionContextException($"{nameof(UInt4)}./");

        /// <summary>
        /// Multiplies two <see cref="UInt4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt4"/> value to multiply.</param>
        public static UInt4 operator *(UInt4 left, UInt4 right) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.*");

        /// <summary>
        /// Subtracts two <see cref="UInt4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt4"/> value to subtract.</param>
        public static UInt4 operator -(UInt4 left, UInt4 right) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.-");
    }
}
