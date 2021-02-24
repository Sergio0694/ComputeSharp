using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="float4"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z}, {W})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(float) * 4)]
    public partial struct Float4
    {
        /// <summary>
        /// Gets an <see cref="Float4"/> value with all components set to 0.
        /// </summary>
        public static Float4 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Float4"/> value with all components set to 1.
        /// </summary>
        public static Float4 One => 1;

        /// <summary>
        /// Gets an <see cref="Float4"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static Float4 UnitX => new(1, 0, 0, 0);

        /// <summary>
        /// Gets an <see cref="Float4"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static Float4 UnitY => new(0, 1, 0, 0);

        /// <summary>
        /// Gets an <see cref="Float4"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
        /// </summary>
        public static Float4 UnitZ => new(0, 0, 1, 0);

        /// <summary>
        /// Gets an <see cref="Float4"/> value with the <see cref="W"/> component set to 1, and the others to 0.
        /// </summary>
        public static Float4 UnitW => new(0, 0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="Float4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public Float4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the third vector component.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Gets or sets the value of the fourth vector component.
        /// </summary>
        public float W { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component.
        /// </summary>
        public float R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public float G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component.
        /// </summary>
        public float B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets the value of the fourth color component.
        /// </summary>
        public float A
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(A)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float4)}.{nameof(A)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Float4"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public float this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float4)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Float4)}[int]");
        }

        /// <summary>
        /// Creates a new <see cref="Float4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float4"/> instance.</param>
        public static implicit operator Float4(float x) => new(x, x, x, x);

        /// <summary>
        /// Casts a <see cref="Float4"/> value to a <see cref="Vector4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Float4"/> value to cast.</param>
        public static unsafe implicit operator Vector4(Float4 xyzw) => *(Vector4*)&xyzw;

        /// <summary>
        /// Casts a <see cref="Vector4"/> value to a <see cref="Float4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Vector4"/> value to cast.</param>
        public static unsafe implicit operator Float4(Vector4 xyzw) => *(Float4*)&xyzw;

        /// <summary>
        /// Casts a <see cref="Float4"/> value to a <see cref="Double4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Float4"/> value to cast.</param>
        public static implicit operator Double4(Float4 xyzw) => throw new InvalidExecutionContextException($"{nameof(Float4)}.({nameof(Double4)})");

        /// <summary>
        /// Casts a <see cref="Float4"/> value to a <see cref="Int4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Float4"/> value to cast.</param>
        public static explicit operator Int4(Float4 xyzw) => throw new InvalidExecutionContextException($"{nameof(Float4)}.({nameof(Int4)})");

        /// <summary>
        /// Casts a <see cref="Float4"/> value to a <see cref="UInt4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Float4"/> value to cast.</param>
        public static explicit operator UInt4(Float4 xyzw) => throw new InvalidExecutionContextException($"{nameof(Float4)}.({nameof(UInt4)})");
    }
}
