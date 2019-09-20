using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="int2"/> HLSL type
    /// </summary>
    [DebuggerDisplay("({X}, {Y})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(int) * 2)]
    public struct Int2
    {
        /// <summary>
        /// Gets an <see cref="Int2"/> value with all components set to 0
        /// </summary>
        public static Int2 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Int2"/> value with all components set to 1
        /// </summary>
        public static Int2 One => 1;

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="X"/> component set to 1, and the others to 0
        /// </summary>
        public static Int2 UnitX => new Int2(1, 0);

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Y"/> component set to 1, and the others to 0
        /// </summary>
        public static Int2 UnitY => new Int2(0, 1);

        /// <summary>
        /// Creates a new <see cref="Int2"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        public Int2(int x, int y)
        {
            X = x;
            Y = y;
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
        /// Gets or sets the value of the first color component
        /// </summary>
        public int R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public int G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Int2"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public int this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Int2 XX => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Int2 YY => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(YY)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public Int2 RR => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Int2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Int2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public Int2 GG => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(GG)}");

        /// <summary>
        /// Creates a new <see cref="Int2"/> value with the same value for all its components
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Int2"/> instance</param>
        public static implicit operator Int2(int x) => new Int2(x, x);

        /// <summary>
        /// Sums two <see cref="Int2"/> values
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to sum</param>
        /// <param name="right">The second <see cref="Int2"/> value to sum</param>
        public static Int2 operator +(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{nameof(Int2)}.+");

        /// <summary>
        /// Divides two <see cref="Int2"/> values
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to divide</param>
        /// <param name="right">The second <see cref="Int2"/> value to divide</param>
        public static Int2 operator /(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{nameof(Int2)}./");

        /// <summary>
        /// Multiplies two <see cref="Int2"/> values
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to multiply</param>
        /// <param name="right">The second <see cref="Int2"/> value to multiply</param>
        public static Int2 operator *(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{nameof(Int2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int2"/> values
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to subtract</param>
        /// <param name="right">The second <see cref="Int2"/> value to subtract</param>
        public static Int2 operator -(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{nameof(Int2)}.-");
    }
}
