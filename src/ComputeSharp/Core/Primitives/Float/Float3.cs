using System.Numerics;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="float3"/> HLSL type.
    /// </summary>
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
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Creates a new <see cref="Float3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xy">The value to assign to the first and second vector components.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        public Float3(Float2 xy, float z)
        {
            this.x = xy.X;
            this.y = xy.Y;
            this.z = z;
        }

        /// <summary>
        /// Creates a new <see cref="Float3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="yz">The value to assign to the second and thirt vector components.</param>
        public Float3(float x, Float2 yz)
        {
            this.x = x;
            this.y = yz.X;
            this.z = yz.Y;
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
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static implicit operator Double3(Float3 xyz) => default;

        /// <summary>
        /// Casts a <see cref="Float3"/> value to a <see cref="Int3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static explicit operator Int3(Float3 xyz) => default;

        /// <summary>
        /// Casts a <see cref="Float3"/> value to a <see cref="UInt3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Float3"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static explicit operator UInt3(Float3 xyz) => default;
    }
}
