using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Float4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public partial struct Float4
    {
        [FieldOffset(0)]
        private float x;

        [FieldOffset(4)]
        private float y;

        [FieldOffset(8)]
        private float z;

        [FieldOffset(12)]
        private float w;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Float4"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref float this[int i] => throw new InvalidExecutionContextException($"{nameof(Float4)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>X</c> component.
        /// </summary>
        public ref float X => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref float Y => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref float Z => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>W</c> component.
        /// </summary>
        public ref float W => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(W)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float2 XX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 XY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 XZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 XW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 YX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float2 YY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 YZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 YW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 ZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 ZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float2 ZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float2 ZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float2 WX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float2 WY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float2 WZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float2 WW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 XXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 XXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 XXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 XYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 XYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 XYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 XZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 XZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 XZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 XWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 XWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 XWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 XWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 YXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 YXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 YXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 YYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 YYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 YYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 YZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 YZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 YZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 YWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 YWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 YWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 YWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 ZXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 ZXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 ZXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 ZYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 ZYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float3 ZYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 ZZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 ZZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 ZZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 ZWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 ZWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 ZWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 ZWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 WXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 WXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 WXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 WXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 WYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 WYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float3 WYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 WYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float3 WZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float3 WZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 WZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 WZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float3 WWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float3 WWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float3 WWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float3 WWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XXXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XXXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XXXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XXXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XXYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XXYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XXYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XXYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XXZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XXZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XXZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XXZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XXWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XXWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XXWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XXWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XYXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XYXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XYXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XYXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XYYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XYYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XYYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XYYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XYZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XYZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XYZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float4 XYZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XYWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XYWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float4 XYWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XYWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XZXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XZXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XZXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XZXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XZYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XZYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XZYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float4 XZYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XZZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XZZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XZZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XZZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XZWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float4 XZWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XZWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XZWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XWXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XWXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XWXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XWXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XWYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XWYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float4 XWYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XWYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XWZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float4 XWZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XWZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XWZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 XWWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 XWWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 XWWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 XWWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(XWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YXXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YXXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YXXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YXXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YXYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YXYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YXYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YXYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YXZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YXZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YXZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Float4 YXZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YXWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YXWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXWY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float4 YXWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YXWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YYXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YYXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YYXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YYXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YYYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YYYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YYYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YYYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YYZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YYZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YYZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YYZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YYWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YYWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YYWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YYWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YZXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YZXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YZXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float4 YZXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YZYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YZYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YZYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YZYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YZZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YZZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YZZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YZZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float4 YZWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YZWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YZWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YZWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YWXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YWXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float4 YWXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YWXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YWYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YWYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YWYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YWYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float4 YWZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YWZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YWZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YWZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 YWWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 YWWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 YWWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 YWWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(YWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZXXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZXXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZXXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZXXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZXYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZXYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZXYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXYZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Float4 ZXYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZXZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZXZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZXZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZXZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZXWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXWX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float4 ZXWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZXWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZXWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZYXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZYXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZYXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYXZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Float4 ZYXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZYYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZYYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZYYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZYYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZYZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZYZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZYZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZYZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Float4 ZYWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZYWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZYWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZYWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZZXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZZXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZZXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZZXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZZYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZZYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZZYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZZYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZZZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZZZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZZZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZZZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZZWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZZWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZZWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZZWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZWXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float4 ZWXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZWXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZWXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float4 ZWYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZWYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZWYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZWYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZWZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZWZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZWZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZWZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 ZWWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 ZWWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 ZWWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 ZWWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ZWWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WXXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WXXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WXXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WXXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WXYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WXYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXYY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float4 WXYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WXYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WXZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float4 WXZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WXZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WXZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WXWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WXWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WXWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WXWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WXWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WYXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WYXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYXY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Float4 WYXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WYXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WYYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WYYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WYYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WYYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYYW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Float4 WYZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WYZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WYZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WYZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WYWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WYWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WYWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WYWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WYWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WZXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZXX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Float4 WZXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WZXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WZXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZXW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Float4 WZYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WZYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WZYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WZYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WZZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WZZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WZZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WZZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WZWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WZWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WZWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WZWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WZWW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WWXX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWXX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WWXY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWXY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WWXZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWXZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WWXW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWXW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WWYX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWYX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WWYY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWYY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WWYZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWYZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WWYW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWYW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WWZX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWZX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WWZY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WWZZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWZZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WWZW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWZW)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Float4 WWWX => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWWX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Float4 WWWY => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWWY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Float4 WWWZ => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWWZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Float4 WWWW => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(WWWW)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
        /// </summary>
        public ref float R => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
        /// </summary>
        public ref float G => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>B</c> component.
        /// </summary>
        public ref float B => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="float"/> value representing the <c>A</c> component.
        /// </summary>
        public ref float A => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float2 RR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 RG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 RB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 RA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 GR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float2 GG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 GB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 GA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 BR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 BG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float2 BB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float2 BA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float2 AR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float2 AG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float2 AB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float2 AA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 RRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 RRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 RRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 RGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 RGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 RGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 RBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 RBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 RBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 RAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 RAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 RAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 RAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 GRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 GRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 GRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 GGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 GGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 GGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 GBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 GBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 GBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 GAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 GAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 GAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 GAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 BRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 BRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 BRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 BGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 BGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float3 BGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 BBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 BBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 BBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 BAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 BAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 BAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 BAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 ARR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 ARG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 ARB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 ARA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 AGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 AGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float3 AGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 AGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float3 ABR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float3 ABG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 ABB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 ABA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float3 AAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float3 AAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float3 AAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float3 AAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RRRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RRRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RRRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RRRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RRGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RRGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RRGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RRGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RRBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RRBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RRBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RRBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RRAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RRAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RRAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RRAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RGRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RGRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RGRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RGRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RGGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RGGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RGGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RGGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RGBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RGBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RGBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float4 RGBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RGAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RGAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float4 RGAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RGAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RBRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RBRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RBRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RBRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RBGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RBGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RBGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float4 RBGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RBBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RBBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RBBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RBBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RBAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float4 RBAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RBAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RBAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RARR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RARG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RARB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RARA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RAGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RAGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float4 RAGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RAGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RABR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RABR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float4 RABG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RABB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RABA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 RAAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 RAAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 RAAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 RAAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(RAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GRRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GRRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GRRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GRRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GRGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GRGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GRGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GRGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GRBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GRBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GRBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRBB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Float4 GRBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GRAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GRAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRAG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Float4 GRAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GRAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GGRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GGRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GGRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GGRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GGGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GGGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GGGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GGGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GGBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GGBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GGBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GGBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GGAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GGAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GGAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GGAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GBRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GBRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GBRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float4 GBRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GBGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GBGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GBGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GBGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GBBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GBBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GBBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GBBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float4 GBAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GBAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GBAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GBAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GARR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GARG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GARG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float4 GARB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GARA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GAGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GAGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GAGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GAGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float4 GABR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GABG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GABB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GABA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 GAAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 GAAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 GAAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 GAAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(GAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BRRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BRRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BRRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BRRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BRGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BRGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BRGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRGB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Float4 BRGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BRBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BRBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BRBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BRBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BRAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRAR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Float4 BRAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BRAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BRAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BRAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BGRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BGRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BGRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGRB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Float4 BGRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BGGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BGGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BGGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BGGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BGBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BGBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BGBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BGBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGBA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Float4 BGAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BGAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BGAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BGAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BBRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BBRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BBRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BBRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BBGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BBGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BBGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BBGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BBBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BBBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BBBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BBBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BBAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BBAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BBAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BBAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BBAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BARR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BARR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float4 BARG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BARB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BARA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BARA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float4 BAGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BAGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BAGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BAGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BABR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BABG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BABB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BABA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 BAAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 BAAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 BAAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 BAAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(BAAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 ARRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 ARRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 ARRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 ARRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 ARGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 ARGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARGG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Float4 ARGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 ARGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 ARBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARBR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Float4 ARBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 ARBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 ARBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 ARAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 ARAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 ARAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 ARAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ARAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 AGRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGRR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 AGRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGRG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Float4 AGRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 AGRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGRA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 AGGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 AGGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 AGGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 AGGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGGA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Float4 AGBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 AGBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 AGBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 AGBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 AGAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 AGAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 AGAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 AGAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AGAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 ABRR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABRR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Float4 ABRG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABRG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 ABRB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABRB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 ABRA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABRA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Float4 ABGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 ABGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 ABGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 ABGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 ABBR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABBR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 ABBG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABBG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 ABBB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABBB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 ABBA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABBA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 ABAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 ABAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 ABAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 ABAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(ABAA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 AARR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AARR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 AARG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AARG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 AARB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AARB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 AARA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AARA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 AAGR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAGR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 AAGG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAGG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 AAGB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAGB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 AAGA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAGA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 AABR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AABR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 AABG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AABG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 AABB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AABB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 AABA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AABA)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Float4 AAAR => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAAR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Float4 AAAG => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAAG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Float4 AAAB => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAAB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Float4 AAAA => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(AAAA)}");

        /// <summary>
        /// Negates a <see cref="Float4"/> value.
        /// </summary>
        /// <param name="xyzw">The <see cref="Float4"/> value to negate.</param>
        public static Float4 operator -(Float4 xyzw) => throw new InvalidExecutionContextException($"{nameof(Float4)}.-");

        /// <summary>
        /// Sums two <see cref="Float4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float4"/> value to sum.</param>
        public static Float4 operator +(Float4 left, Float4 right) => throw new InvalidExecutionContextException($"{nameof(Float4)}.+");

        /// <summary>
        /// Divides two <see cref="Float4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float4"/> value to divide.</param>
        public static Float4 operator /(Float4 left, Float4 right) => throw new InvalidExecutionContextException($"{nameof(Float4)}./");

        /// <summary>
        /// Multiplies two <see cref="Float4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float4"/> value to multiply.</param>
        public static Float4 operator *(Float4 left, Float4 right) => throw new InvalidExecutionContextException($"{nameof(Float4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float4"/> value to subtract.</param>
        public static Float4 operator -(Float4 left, Float4 right) => throw new InvalidExecutionContextException($"{nameof(Float4)}.-");
    }
}
