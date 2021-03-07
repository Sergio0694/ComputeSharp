using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Double4"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 8)]
    public partial struct Double4
    {
        [FieldOffset(0)]
        private double x;

        [FieldOffset(8)]
        private double y;

        [FieldOffset(16)]
        private double z;

        [FieldOffset(24)]
        private double w;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Double4"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref double this[int i] => throw new InvalidExecutionContextException($"{nameof(Double4)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>X</c> component.
        /// </summary>
        public ref double X => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref double Y => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref double Z => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>W</c> component.
        /// </summary>
        public ref double W => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(W)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double2 XX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 XY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 XZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 XW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 YX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double2 YY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 YZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 YW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 ZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 ZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double2 ZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double2 ZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double2 WX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double2 WY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double2 WZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double2 WW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 XXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 XXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 XXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 XXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 XYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 XYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 XYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 XYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 XZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 XZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 XZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 XZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 XWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 XWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 XWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 XWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 YXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 YXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 YXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 YXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 YYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 YYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 YYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 YYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 YZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 YZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 YZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 YZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 YWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 YWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 YWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 YWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 ZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 ZXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 ZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 ZXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 ZYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 ZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 ZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double3 ZYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 ZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 ZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 ZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 ZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 ZWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 ZWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 ZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 ZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 WXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 WXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 WXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 WXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 WYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 WYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double3 WYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 WYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double3 WZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double3 WZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 WZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 WZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double3 WWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double3 WWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double3 WWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double3 WWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XXXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XXXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XXXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XXXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XXYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XXYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XXYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XXYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XXZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XXZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XXZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XXZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XXWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XXWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XXWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XXWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XYXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XYXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XYXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XYXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XYYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XYYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XYYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XYYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XYZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XYZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XYZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double4 XYZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XYWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XYWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double4 XYWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XYWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XZXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XZXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XZYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double4 XZYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XZWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double4 XZWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XWXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XWXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XWXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XWXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XWYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XWYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double4 XWYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XWYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XWZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double4 XWZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XWZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XWZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 XWWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 XWWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 XWWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 XWWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YXXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YXXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YXXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YXXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YXYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YXYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YXYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YXYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YXZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YXZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YXZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Double4 YXZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YXWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YXWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double4 YXWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YXWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YYXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YYXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YYXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YYXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YYYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YYYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YYYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YYYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YYZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YYZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YYZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YYZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YYWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YYWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YYWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YYWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YZXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double4 YZXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YZYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YZYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double4 YZWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YZWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YWXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YWXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double4 YWXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YWXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YWYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YWYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YWYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YWYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double4 YWZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YWZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YWZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YWZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 YWWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 YWWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 YWWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 YWWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZXXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZXXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZXXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZXXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZXYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZXYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZXYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Double4 ZXYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZXZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZXZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZXZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZXZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZXWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double4 ZXWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZXWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZXWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZYXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZYXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZYXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Double4 ZYXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZYYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZYYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZYYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZYYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZYZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZYZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZYZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZYZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Double4 ZYWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZYWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZYWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZYWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZZXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZZXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZZYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZZYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZZWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZZWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZWXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double4 ZWXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZWXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZWXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double4 ZWYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZWYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZWYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZWYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZWZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZWZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZWZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZWZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 ZWWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 ZWWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 ZWWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 ZWWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WXXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WXXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WXXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WXXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WXYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WXYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double4 WXYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WXYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WXZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double4 WXZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WXZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WXZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WXWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WXWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WXWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WXWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WYXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WYXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Double4 WYXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WYXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WYYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WYYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WYYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WYYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Double4 WYZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WYZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WYZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WYZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WYWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WYWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WYWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WYWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Double4 WZXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WZXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Double4 WZYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WZYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WZWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WZWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WWXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WWXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WWXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WWXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WWYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WWYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WWYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WWYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WWZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WWZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WWZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WWZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Double4 WWWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Double4 WWWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Double4 WWWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Double4 WWWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWWW)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>R</c> component.
        /// </summary>
        public ref double R => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>G</c> component.
        /// </summary>
        public ref double G => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>B</c> component.
        /// </summary>
        public ref double B => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the <c>A</c> component.
        /// </summary>
        public ref double A => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double2 RR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 RG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 RB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 RA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 GR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double2 GG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 GB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 GA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 BR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 BG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double2 BB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double2 BA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double2 AR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double2 AG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double2 AB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double2 AA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 RRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 RRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 RRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 RRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 RGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 RGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 RGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 RGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 RBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 RBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 RBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 RBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 RAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 RAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 RAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 RAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 GRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 GRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 GRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 GRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 GGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 GGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 GGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 GGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 GBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 GBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 GBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 GBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 GAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 GAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 GAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 GAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 BRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 BRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 BRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 BRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 BGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 BGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 BGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double3 BGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 BBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 BBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 BBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 BBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 BAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 BAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 BAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 BAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 ARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 ARG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 ARB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 ARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 AGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 AGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double3 AGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 AGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double3 ABR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double3 ABG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 ABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 ABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double3 AAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double3 AAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double3 AAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double3 AAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RRRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RRRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RRRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RRRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RRGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RRGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RRGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RRGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RRBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RRBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RRBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RRBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RRAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RRAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RRAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RRAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RGRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RGRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RGRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RGRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RGGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RGGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RGGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RGGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RGBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RGBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RGBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double4 RGBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RGAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RGAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double4 RGAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RGAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RBRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RBRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RBRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RBRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RBGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RBGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RBGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double4 RBGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RBBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RBBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RBBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RBBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RBAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double4 RBAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RBAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RBAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RARG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RARB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RAGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RAGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double4 RAGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RAGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RABR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double4 RABG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 RAAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 RAAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 RAAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 RAAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GRRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GRRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GRRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GRRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GRGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GRGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GRGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GRGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GRBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GRBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GRBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Double4 GRBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GRAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GRAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Double4 GRAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GRAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GGRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GGRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GGRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GGRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GGGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GGGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GGGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GGGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GGBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GGBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GGBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GGBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GGAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GGAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GGAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GGAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GBRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GBRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GBRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double4 GBRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GBGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GBGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GBGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GBGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GBBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GBBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GBBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GBBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double4 GBAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GBAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GBAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GBAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GARG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double4 GARB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GAGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GAGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GAGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GAGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double4 GABR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GABG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 GAAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 GAAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 GAAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 GAAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BRRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BRRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BRRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BRRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BRGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BRGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BRGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Double4 BRGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BRBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BRBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BRBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BRBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BRAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Double4 BRAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BRAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BRAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BGRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BGRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BGRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Double4 BGRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BGGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BGGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BGGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BGGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BGBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BGBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BGBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BGBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Double4 BGAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BGAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BGAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BGAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BBRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BBRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BBRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BBRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BBGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BBGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BBGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BBGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BBBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BBBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BBBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BBBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BBAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BBAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BBAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BBAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double4 BARG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BARB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double4 BAGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BAGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BAGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BAGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BABR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BABG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 BAAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 BAAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 BAAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 BAAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 ARRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 ARRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 ARRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 ARRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 ARGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 ARGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Double4 ARGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 ARGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 ARBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Double4 ARBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 ARBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 ARBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 ARAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 ARAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 ARAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 ARAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 AGRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 AGRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Double4 AGRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 AGRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 AGGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 AGGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 AGGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 AGGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Double4 AGBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 AGBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 AGBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 AGBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 AGAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 AGAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 AGAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 AGAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 ABRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Double4 ABRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 ABRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 ABRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Double4 ABGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 ABGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 ABGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 ABGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 ABBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 ABBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 ABBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 ABBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 ABAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 ABAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 ABAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 ABAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 AARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 AARG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 AARB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 AARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 AAGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 AAGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 AAGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 AAGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 AABR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 AABG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 AABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 AABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Double4 AAAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Double4 AAAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Double4 AAAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Double4 AAAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAAA)}");

        /// <summary>
        /// Negates a <see cref="Double4"/> value.
        /// </summary>
        /// <param name="xyzw">The <see cref="Double4"/> value to negate.</param>
        public static Double4 operator -(Double4 xyzw) => throw new InvalidExecutionContextException($"{nameof(Double4)}.-");

        /// <summary>
        /// Sums two <see cref="Double4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4"/> value to sum.</param>
        public static Double4 operator +(Double4 left, Double4 right) => throw new InvalidExecutionContextException($"{nameof(Double4)}.+");

        /// <summary>
        /// Divides two <see cref="Double4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4"/> value to divide.</param>
        public static Double4 operator /(Double4 left, Double4 right) => throw new InvalidExecutionContextException($"{nameof(Double4)}./");

        /// <summary>
        /// Multiplies two <see cref="Double4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4"/> value to multiply.</param>
        public static Double4 operator *(Double4 left, Double4 right) => throw new InvalidExecutionContextException($"{nameof(Double4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4"/> value to subtract.</param>
        public static Double4 operator -(Double4 left, Double4 right) => throw new InvalidExecutionContextException($"{nameof(Double4)}.-");
    }
}
