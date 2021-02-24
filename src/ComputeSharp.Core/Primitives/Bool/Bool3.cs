using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="bool3"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 3)]
    public partial struct Bool3
    {
        /// <summary>
        /// Gets an <see cref="Bool3"/> value with all components set to <see langword="false"/>.
        /// </summary>
        public static Bool3 False => false;

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with all components set to <see langword="true"/>.
        /// </summary>
        public static Bool3 True => true;

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool3 TrueX => new(true, false, false);

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool3 TrueY => new(false, true, false);

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool3 TrueZ => new(false, false, true);

        /// <summary>
        /// Creates a new <see cref="Bool3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        public Bool3(bool x, bool y, bool z)
        {
            _X = x;
            _Y = y;
            _Z = z;
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

        /// <summary>
        /// Gets or sets the value of the first color component.
        /// </summary>
        public bool R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public bool G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component.
        /// </summary>
        public bool B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(B)}");
        }

        /// <summary>
        /// Creates a new <see cref="Bool3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool3"/> instance.</param>
        public static implicit operator Bool3(bool x) => new(x, x, x);
    }
}
