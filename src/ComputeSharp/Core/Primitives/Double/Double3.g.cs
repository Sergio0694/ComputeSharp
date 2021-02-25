using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Double3"/>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct Double3
    {
        [FieldOffset(0)]
        private double x;

        [FieldOffset(8)]
        private double y;

        [FieldOffset(16)]
        private double z;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Double3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref double this[int i] => throw new InvalidExecutionContextException($"{nameof(Double3)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>X</c> component.
        /// </summary>
        public ref double X => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref double Y => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref double Z => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>W</c> component.
        /// </summary>
        public ref double W => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(W)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double2 XX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 XY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 XZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 XW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 YX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double2 YY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 YZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 YW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 ZX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 ZY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double2 ZZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 ZW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 WX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 WY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 WZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double2 WW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WW)}");

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 XXW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XXW)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 XYW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XYW)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 XZW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 XWX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 XWY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 XWZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 XWW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(XWW)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 YXW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YXW)}");

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 YYW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YYW)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 YZW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 YWX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 YWY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 YWZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 YWW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(YWW)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 ZXW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZXW)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 ZYW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZYW)}");

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 ZZW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 ZWX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 ZWY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 ZWZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 ZWW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 WXX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 WXY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 WXZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 WXW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 WYX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 WYY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 WYZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 WYW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 WZX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 WZY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 WZZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 WZW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 WWX => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 WWY => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 WWZ => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 WWW => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(WWW)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>R</c> component.
        /// </summary>
        public ref double R => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>G</c> component.
        /// </summary>
        public ref double G => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>B</c> component.
        /// </summary>
        public ref double B => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>A</c> component.
        /// </summary>
        public ref double A => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double2 RR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 RG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 RB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 RA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 GR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double2 GG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 GB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 GA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 BR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 BG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double2 BB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 BA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 AR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 AG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 AB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double2 AA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AA)}");

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 RRA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RRA)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 RGA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RGA)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 RBA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 RAR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 RAG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 RAB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 RAA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(RAA)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 GRA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GRA)}");

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 GGA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GGA)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 GBA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 GAR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 GAG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 GAB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 GAA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(GAA)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 BRA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BRA)}");

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
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 BGA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BGA)}");

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
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 BBA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 BAR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 BAG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 BAB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 BAA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(BAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 ARR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ARR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 ARG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ARG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 ARB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 ARA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ARA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 AGR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 AGG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 AGB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 AGA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 ABR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ABR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 ABG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 ABB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 ABA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(ABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 AAR => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 AAG => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 AAB => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 AAA => throw new InvalidExecutionContextException($"{nameof(Double3)}.{nameof(AAA)}");

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
