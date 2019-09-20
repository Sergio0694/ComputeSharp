using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="bool2"/> HLSL type
    /// </summary>
    [DebuggerDisplay("({X}, {Y})")]
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 2)]
    public struct Bool2
    {
        /// <summary>
        /// Gets an <see cref="Bool2"/> value with all components set to <see langword="false"/>
        /// </summary>
        public static Bool2 False => false;

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with all components set to <see langword="true"/>
        /// </summary>
        public static Bool2 True => true;

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="X"/> component set to <see langword="true"/>, and the others to <see langword="false"/>
        /// </summary>
        public static Bool2 TrueX => new Bool2(true, false);

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="Y"/> component set to <see langword="true"/>, and the others to <see langword="false"/>
        /// </summary>
        public static Bool2 TrueY => new Bool2(false, true);

        /// <summary>
        /// Creates a new <see cref="Bool2"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        public Bool2(bool x, bool y)
        {
            _X = x;
            _Y = y;
        }

        [FieldOffset(0)]
        private bool _X;

        /// <summary>
        /// Gets or sets the value of the first vector component
        /// </summary>
        public bool X
        {
            get => _X;
            set => _X = value;
        }

        [FieldOffset(4)]
        private bool _Y;

        /// <summary>
        /// Gets or sets the value of the second vector component
        /// </summary>
        public bool Y
        {
            get => _Y;
            set => _Y = value;
        }

        /// <summary>
        /// Gets or sets the value of the first color component
        /// </summary>
        public bool R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public bool G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Bool2"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public bool this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Bool2 XX => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Bool2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Bool2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Bool2 YY => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YY)}");

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public Bool2 RR => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Bool2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Bool2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public Bool2 GG => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GG)}");

        /// <summary>
        /// Creates a new <see cref="Bool2"/> value with the same value for all its components
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool2"/> instance</param>
        public static implicit operator Bool2(bool x) => new Bool2(x, x);
    }
}
