using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="float3"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(float) * 3)]
    public partial struct Float3
    {
        /// <summary>
        /// Gets an <see cref="Float3"/> value with all components set to 0.
        /// </summary>
        public static Float3 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Float3"/> value with all components set to 1.
        /// </summary>
        public static Float3 One => 1;

        /// <summary>
        /// Gets an <see cref="Float3"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static Float3 UnitX => new(1, 0, 0);

        /// <summary>
        /// Gets an <see cref="Float3"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static Float3 UnitY => new(0, 1, 0);

        /// <summary>
        /// Gets an <see cref="Float3"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
        /// </summary>
        public static Float3 UnitZ => new(0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="Float3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        public Float3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
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
        /// Gets or sets the value of the first color component.
        /// </summary>
        public float R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public float G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component.
        /// </summary>
        public float B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float3)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Float3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public float this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float3)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Float3)}[int]");
        }

        /// <summary>
        /// Creates a new <see cref="Float3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float3"/> instance.</param>
        public static implicit operator Float3(float x) => new(x, x, x);

        /// <summary>
        /// Casts a <see cref="Float3"/> value to a <see cref="Vector3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
        public static unsafe implicit operator Vector3(Float3 xyz) => *(Vector3*)&xyz;

        /// <summary>
        /// Casts a <see cref="Vector3"/> value to a <see cref="Float3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Vector3"/> value to cast.</param>
        public static unsafe implicit operator Float3(Vector3 xyz) => *(Float3*)&xyz;

        /// <summary>
        /// Casts a <see cref="Float3"/> value to a <see cref="Double3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
        public static implicit operator Double3(Float3 xyz) => throw new InvalidExecutionContextException($"{nameof(Float3)}.({nameof(Double3)})");

        /// <summary>
        /// Casts a <see cref="Float3"/> value to a <see cref="Int3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
        public static explicit operator Int3(Float3 xyz) => throw new InvalidExecutionContextException($"{nameof(Float3)}.({nameof(Int3)})");

        /// <summary>
        /// Casts a <see cref="Float3"/> value to a <see cref="UInt3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
        public static explicit operator UInt3(Float3 xyz) => throw new InvalidExecutionContextException($"{nameof(Float3)}.({nameof(UInt3)})");
    }
}
