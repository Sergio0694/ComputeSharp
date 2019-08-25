using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Exceptions;

namespace ComputeSharp.Shaders.Primitives.Int
{
    /// <summary>
    /// A <see langword="struct"/> that maps the int3 HLSL type
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = sizeof(int) * 3)]
    public struct Int3
    {
        /// <summary>
        /// Creates a new <see cref="Int3"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        /// <param name="z">The value to assign to the third vector component</param>
        public Int3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the third vector component
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component
        /// </summary>
        public float R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public float G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component
        /// </summary>
        public float B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Int3"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public float this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Int2 XX => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 XZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Int2 YY => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 YZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int2 ZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 ZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public Int2 ZZ => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Int2 XXX => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 XXY => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XXY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 XXZ => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int2 XYX => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 XYY => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 XYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int2 XZX => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 XZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 XZZ => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Int2 YXX => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 YXY => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YXY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 YXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int2 YYX => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Int2 YYY => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 YYZ => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int2 YZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YZX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 YZY => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 YZZ => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 ZXX => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 ZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZXY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 ZXZ => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int2 ZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZYX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 ZYY => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 ZYZ => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int2 ZZX => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 ZZY => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public Int2 ZZZ => throw new InvalidExecutionContextException($"{nameof(Int3)}.{nameof(ZZZ)}");
    }
}
