using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="bool4"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z}, {W})")]
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 4)]
    public partial struct Bool4
    {
        /// <summary>
        /// Gets an <see cref="Bool4"/> value with all components set to <see langword="false"/>.
        /// </summary>
        public static Bool4 False => false;

        /// <summary>
        /// Gets an <see cref="Bool4"/> value with all components set to <see langword="true"/>.
        /// </summary>
        public static Bool4 True => true;

        /// <summary>
        /// Gets an <see cref="Bool4"/> value with the <see cref="X"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool4 TrueX => new(true, false, false, false);

        /// <summary>
        /// Gets an <see cref="Bool4"/> value with the <see cref="Y"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool4 TrueY => new(false, true, false, false);

        /// <summary>
        /// Gets an <see cref="Bool4"/> value with the <see cref="Z"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool4 TrueZ => new(false, false, true, false);

        /// <summary>
        /// Gets an <see cref="Bool4"/> value with the <see cref="W"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool4 TrueW => new(false, false, false, true);

        /// <summary>
        /// Creates a new <see cref="Bool4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public Bool4(bool x, bool y, bool z, bool w)
        {
            _X = x;
            _Y = y;
            _Z = z;
            _W = w;
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

        [FieldOffset(8)]
        private bool _Z;

        /// <summary>
        /// Gets or sets the value of the third vector component.
        /// </summary>
        public bool Z
        {
            get => _Z;
            set => _Z = value;
        }

        [FieldOffset(12)]
        private bool _W;

        /// <summary>
        /// Gets or sets the value of the fourth vector component.
        /// </summary>
        public bool W
        {
            get => _W;
            set => _W = value;
        }

        /// <summary>
        /// Gets or sets the value of the first color component.
        /// </summary>
        public bool R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public bool G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component.
        /// </summary>
        public bool B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets the value of the fourth color component.
        /// </summary>
        public bool A
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(A)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(A)}");
        }

        /// <summary>
        /// Creates a new <see cref="Bool4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool4"/> instance.</param>
        public static implicit operator Bool4(bool x) => new(x, x, x, x);
    }
}
