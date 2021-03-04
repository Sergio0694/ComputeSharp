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
        public ref float this[int i] => throw new InvalidExecutionContextException($"{nameof(Float3)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>X</c> component.
        /// </summary>
        public ref float X => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref float Y => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref float Z => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>W</c> component.
        /// </summary>
        public ref float W => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(W)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float2 XX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 XY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 XZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 XW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 YX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float2 YY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 YZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 YW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 ZX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 ZY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float2 ZZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 ZW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 WX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 WY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 WZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float2 WW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XXX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 XXY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 XXZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 XXW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XYX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 XYY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 XYZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 XYW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XZX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 XZY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 XZZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 XZW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XWX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 XWY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 XWZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 XWW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(XWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 YXX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YXY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 YXZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 YXW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 YYX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YYY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 YYZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 YYW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 YZX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YZY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 YZZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 YZW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 YWX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YWY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 YWZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 YWW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(YWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 ZXX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 ZXY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZXZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 ZXW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 ZYX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 ZYY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZYZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 ZYW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 ZZX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 ZZY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZZZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 ZZW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 ZWX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 ZWY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZWZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 ZWW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 WXX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 WXY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 WXZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 WXW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 WYX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 WYY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 WYZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 WYW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 WZX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 WZY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 WZZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 WZW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 WWX => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 WWY => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 WWZ => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 WWW => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(WWW)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
        /// </summary>
        public ref float R => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
        /// </summary>
        public ref float G => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>B</c> component.
        /// </summary>
        public ref float B => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>A</c> component.
        /// </summary>
        public ref float A => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float2 RR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 RG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 RB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 RA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 GR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float2 GG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 GB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 GA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 BR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 BG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float2 BB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 BA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 AR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 AG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 AB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float2 AA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RRR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 RRG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 RRB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 RRA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RGR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 RGG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 RGB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 RGA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RBR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 RBG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 RBB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 RBA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RAR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 RAG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 RAB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 RAA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(RAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 GRR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GRG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 GRB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 GRA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 GGR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GGG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 GGB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 GGA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 GBR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GBG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 GBB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 GBA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 GAR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GAG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 GAB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 GAA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(GAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 BRR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 BRG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BRB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 BRA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BRA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 BGR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 BGG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BGB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 BGA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 BBR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 BBG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BBB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 BBA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 BAR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 BAG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BAB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 BAA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(BAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 ARR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ARR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 ARG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ARG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 ARB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 ARA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ARA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 AGR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 AGG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 AGB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 AGA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 ABR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ABR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 ABG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 ABB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 ABA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(ABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 AAR => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 AAG => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 AAB => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 AAA => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(AAA)}");

        /// <summary>
        /// Negates a <see cref="Float3"/> value.
        /// </summary>
        /// <param name="xyz">The <see cref="Float3"/> value to negate.</param>
        public static Float3 operator -(Float3 xyz) => throw new InvalidExecutionContextException($"{nameof(Float3)}.-");

        /// <summary>
        /// Sums two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float3"/> value to sum.</param>
        public static Float3 operator +(Float3 left, Float3 right) => throw new InvalidExecutionContextException($"{nameof(Float3)}.+");

        /// <summary>
        /// Divides two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float3"/> value to divide.</param>
        public static Float3 operator /(Float3 left, Float3 right) => throw new InvalidExecutionContextException($"{nameof(Float3)}./");

        /// <summary>
        /// Multiplies two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float3"/> value to multiply.</param>
        public static Float3 operator *(Float3 left, Float3 right) => throw new InvalidExecutionContextException($"{nameof(Float3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float3"/> value to subtract.</param>
        public static Float3 operator -(Float3 left, Float3 right) => throw new InvalidExecutionContextException($"{nameof(Float3)}.-");
    }
}
