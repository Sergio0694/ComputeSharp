using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Float3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public partial struct Float3
    {
        [FieldOffset(0)]
        private float x;

        [FieldOffset(4)]
        private float y;

        [FieldOffset(8)]
        private float z;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Float3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref float this[int i] => throw new InvalidExecutionContextException($"{typeof(Float3)}[{typeof(int)}]");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>X</c> component.
        /// </summary>
        public ref float X => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref float Y => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref float Z => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(Z)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float2 XX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 XY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 XZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 YX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float2 YY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 YZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 ZX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 ZY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float2 ZZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XXX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 XXY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 XXZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XYX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 XYY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 XYZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XZX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 XZY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 XZZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 YXX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YXY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 YXZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 YYX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YYY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 YYZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 YZX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YZY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 YZZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 ZXX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 ZXY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZXZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 ZYX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 ZYY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZYZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 ZZX => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 ZZY => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZZZ => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
        /// </summary>
        public ref float R => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
        /// </summary>
        public ref float G => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>B</c> component.
        /// </summary>
        public ref float B => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(B)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float2 RR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 RG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 RB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 GR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float2 GG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 GB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 BR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 BG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float2 BB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RRR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 RRG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 RRB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RGR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 RGG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 RGB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RBR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 RBG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 RBB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(RBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 GRR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GRG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 GRB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 GGR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GGG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 GGB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 GBR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GBG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 GBB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(GBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 BRR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 BRG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BRB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 BGR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 BGG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BGB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 BBR => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 BBG => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BBB => throw new InvalidExecutionContextException($"{typeof(Float3)}.{nameof(BBB)}");

        /// <summary>
        /// Negates a <see cref="Float3"/> value.
        /// </summary>
        /// <param name="xyz">The <see cref="Float3"/> value to negate.</param>
        public static Float3 operator -(Float3 xyz) => throw new InvalidExecutionContextException($"{typeof(Float3)}.-");

        /// <summary>
        /// Sums two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float3"/> value to sum.</param>
        public static Float3 operator +(Float3 left, Float3 right) => throw new InvalidExecutionContextException($"{typeof(Float3)}.+");

        /// <summary>
        /// Divides two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float3"/> value to divide.</param>
        public static Float3 operator /(Float3 left, Float3 right) => throw new InvalidExecutionContextException($"{typeof(Float3)}./");

        /// <summary>
        /// Multiplies two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float3"/> value to multiply.</param>
        public static Float3 operator *(Float3 left, Float3 right) => throw new InvalidExecutionContextException($"{typeof(Float3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float3"/> value to subtract.</param>
        public static Float3 operator -(Float3 left, Float3 right) => throw new InvalidExecutionContextException($"{typeof(Float3)}.-");
    }
}
