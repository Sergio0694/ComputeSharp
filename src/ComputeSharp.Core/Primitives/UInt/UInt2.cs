using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="uint2"/> HLSL type
    /// </summary>
    [DebuggerDisplay("({X}, {Y})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint) * 2)]
    public struct UInt2
    {
        /// <summary>
        /// Gets an <see cref="UInt2"/> value with all components set to 0
        /// </summary>
        public static UInt2 Zero => 0;

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with all components set to 1
        /// </summary>
        public static UInt2 One => 1;

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="X"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt2 UnitX => new UInt2(1, 0);

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="Y"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt2 UnitY => new UInt2(0, 1);

        /// <summary>
        /// Creates a new <see cref="UInt2"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        public UInt2(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component
        /// </summary>
        public uint X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component
        /// </summary>
        public uint Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component
        /// </summary>
        public uint R
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public uint G
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="UInt2"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public float this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public UInt2 XX => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public UInt2 YY => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(YY)}");

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public UInt2 RR => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public UInt2 GG => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(GG)}");

        /// <summary>
        /// Creates a new <see cref="UInt2"/> value with the same value for all its components
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt2"/> instance</param>
        public static implicit operator UInt2(uint x) => new UInt2(x, x);

        /// <summary>
        /// Sums two <see cref="UInt2"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to sum</param>
        /// <param name="right">The second <see cref="UInt2"/> value to sum</param>
        public static UInt2 operator +(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.+");

        /// <summary>
        /// Divides two <see cref="UInt2"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to divide</param>
        /// <param name="right">The second <see cref="UInt2"/> value to divide</param>
        public static UInt2 operator /(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}./");

        /// <summary>
        /// Multiplies two <see cref="UInt2"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to multiply</param>
        /// <param name="right">The second <see cref="UInt2"/> value to multiply</param>
        public static UInt2 operator *(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.*");

        /// <summary>
        /// Subtracts two <see cref="UInt2"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to subtract</param>
        /// <param name="right">The second <see cref="UInt2"/> value to subtract</param>
        public static UInt2 operator -(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.-");
    }
}
