namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="double4"/> HLSL type.
    /// </summary>
    public partial struct Double4
    {
        /// <summary>
        /// Gets an <see cref="Double4"/> value with all components set to 0.
        /// </summary>
        public static Double4 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Double4"/> value with all components set to 1.
        /// </summary>
        public static Double4 One => 1;

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double4 UnitX => new(1, 0, 0, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double4 UnitY => new(0, 1, 0, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double4 UnitZ => new(0, 0, 1, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/> component set to 1, and the others to 0.
        /// </summary>
        public static Double4 UnitW => new(0, 0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="Double4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public Double4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Creates a new <see cref="Double4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xy">The value to assign to the first and second vector components.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public Double4(Double2 xy, double z, double w)
        {
            this.x = xy.X;
            this.y = xy.Y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Creates a new <see cref="Double4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="zw">The value to assign to the third and fourth vector components.</param>
        public Double4(double x, double y, Double2 zw)
        {
            this.x = x;
            this.y = y;
            this.z = zw.X;
            this.w = zw.Y;
        }

        /// <summary>
        /// Creates a new <see cref="Double4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xy">The value to assign to the first and second vector components.</param>
        /// <param name="zw">The value to assign to the third and fourth vector components.</param>
        public Double4(Double2 xy, Double2 zw)
        {
            this.x = xy.X;
            this.y = xy.Y;
            this.z = zw.X;
            this.w = zw.Y;
        }

        /// <summary>
        /// Creates a new <see cref="Double4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public Double4(Double3 xyz, double w)
        {
            this.x = xyz.X;
            this.y = xyz.Y;
            this.z = xyz.Z;
            this.w = w;
        }

        /// <summary>
        /// Creates a new <see cref="Double4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="yzw">The value to assign to the second, third and fourth vector components.</param>
        public Double4(double x, Double3 yzw)
        {
            this.x = x;
            this.y = yzw.X;
            this.z = yzw.Y;
            this.w = yzw.Z;
        }

        /// <summary>
        /// Creates a new <see cref="Double4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double4"/> instance.</param>
        public static implicit operator Double4(double x) => new(x, x, x, x);

        /// <summary>
        /// Casts a <see cref="Double4"/> value to a <see cref="Int4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Double4"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static explicit operator Int4(Double4 xyzw) => default;

        /// <summary>
        /// Casts a <see cref="Double4"/> value to a <see cref="UInt4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Double4"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static explicit operator UInt4(Double4 xyzw) => default;

        /// <summary>
        /// Casts a <see cref="Double4"/> value to a <see cref="Float4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="Double4"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static explicit operator Float4(Double4 xyzw) => default;
    }
}
