using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="bool2"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y})")]
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 2)]
    public partial struct Bool2
    {
        /// <summary>
        /// Gets an <see cref="Bool2"/> value with all components set to <see langword="false"/>.
        /// </summary>
        public static Bool2 False => false;

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with all components set to <see langword="true"/>.
        /// </summary>
        public static Bool2 True => true;

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="X"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool2 TrueX => new(true, false);

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="Y"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool2 TrueY => new(false, true);

        /// <summary>
        /// Creates a new <see cref="Bool2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        public Bool2(bool x, bool y)
        {
            _X = x;
            _Y = y;
        }

        [FieldOffset(0)]
        private bool _X;

        /// <summary>
        /// Gets or sets the value of the first vector component.
        /// </summary>
        public bool X
        {
            get => _X;
            set => _X = value;
        }

        [FieldOffset(4)]
        private bool _Y;

        /// <summary>
        /// Gets or sets the value of the second vector component.
        /// </summary>
        public bool Y
        {
            get => _Y;
            set => _Y = value;
        }

        /// <summary>
        /// Gets or sets the value of the first color component.
        /// </summary>
        public bool R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public bool G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(G)}");
        }

        /// <summary>
        /// Creates a new <see cref="Bool2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool2"/> instance.</param>
        public static implicit operator Bool2(bool x) => new(x, x);
    }
}
