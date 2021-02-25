using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="UInt3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
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
        public ref uint this[int i] => throw new InvalidExecutionContextException($"{nameof(UInt3)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>X</c> component.
        /// </summary>
        public ref uint X => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref uint Y => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref uint Z => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>W</c> component.
        /// </summary>
        public ref uint W => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(W)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt2 XX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt2 XY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt2 XZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt2 XW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt2 YX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt2 YY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt2 YZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt2 YW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt2 ZX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt2 ZY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt2 ZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt2 ZW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt2 WX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt2 WY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt2 WZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt2 WW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XXX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 XXY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 XXZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 XXW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XYX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 XYY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 XYZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt3 XYW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XZX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 XZY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 XZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt3 XZW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 XWX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 XWY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 XWZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 XWW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 YXX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YXY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 YXZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt3 YXW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 YYX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YYY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 YYZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 YYW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 YZX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YZY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 YZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt3 YZW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 YWX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 YWY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 YWZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 YWW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 ZXX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 ZXY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZXZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt3 ZXW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 ZYX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 ZYY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZYZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref UInt3 ZYW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 ZZX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 ZZY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 ZZW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 ZWX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 ZWY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 ZWZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 ZWW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 WXX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 WXY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 WXZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 WXW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 WYX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 WYY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref UInt3 WYZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 WYW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref UInt3 WZX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref UInt3 WZY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 WZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 WZW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly UInt3 WWX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly UInt3 WWY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly UInt3 WWZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly UInt3 WWW => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(WWW)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>R</c> component.
        /// </summary>
        public ref uint R => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>G</c> component.
        /// </summary>
        public ref uint G => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>B</c> component.
        /// </summary>
        public ref uint B => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the <c>A</c> component.
        /// </summary>
        public ref uint A => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt2 RR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt2 RG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt2 RB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt2 RA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt2 GR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt2 GG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt2 GB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt2 GA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt2 BR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt2 BG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt2 BB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt2 BA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt2 AR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt2 AG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt2 AB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt2 AA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RRR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 RRG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 RRB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 RRA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RGR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 RGG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 RGB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt3 RGA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RBR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 RBG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 RBB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt3 RBA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 RAR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 RAG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 RAB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 RAA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 GRR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GRG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 GRB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt3 GRA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 GGR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GGG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 GGB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 GGA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 GBR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GBG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 GBB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt3 GBA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 GAR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 GAG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 GAB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 GAA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 BRR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 BRG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BRB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt3 BRA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BRA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 BGR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 BGG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BGB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref UInt3 BGA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 BBR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 BBG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BBB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 BBA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 BAR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 BAG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 BAB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 BAA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 ARR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ARR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 ARG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ARG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 ARB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 ARA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ARA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 AGR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 AGG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref UInt3 AGB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 AGA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref UInt3 ABR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ABR)}");

        /// <summary>
        /// Gets a reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref UInt3 ABG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 ABB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 ABA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly UInt3 AAR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly UInt3 AAG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly UInt3 AAB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="UInt3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly UInt3 AAA => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(AAA)}");

        /// <summary>
        /// Sums two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to sum.</param>
        public static UInt3 operator +(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{nameof(UInt3)}.+");

        /// <summary>
        /// Divides two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to divide.</param>
        public static UInt3 operator /(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{nameof(UInt3)}./");

        /// <summary>
        /// Multiplies two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to multiply.</param>
        public static UInt3 operator *(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{nameof(UInt3)}.*");

        /// <summary>
        /// Subtracts two <see cref="UInt3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt3"/> value to subtract.</param>
        public static UInt3 operator -(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{nameof(UInt3)}.-");
    }
}
