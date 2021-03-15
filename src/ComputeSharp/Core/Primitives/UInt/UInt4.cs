namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="uint4"/> HLSL type.
    /// </summary>
    public partial struct UInt4
    {
        /// <summary>
        /// Gets an <see cref="UInt4"/> value with all components set to 0.
        /// </summary>
        public static UInt4 Zero => 0;

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with all components set to 1.
        /// </summary>
        public static UInt4 One => 1;

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static UInt4 UnitX => new(1, 0, 0, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static UInt4 UnitY => new(0, 1, 0, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
        /// </summary>
        public static UInt4 UnitZ => new(0, 0, 1, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/> component set to 1, and the others to 0.
        /// </summary>
        public static UInt4 UnitW => new(0, 0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="UInt4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public UInt4(uint x, uint y, uint z, uint w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Creates a new <see cref="UInt4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xy">The value to assign to the first and second vector components.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public UInt4(UInt2 xy, uint z, uint w)
        {
            this.x = xy.X;
            this.y = xy.Y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Creates a new <see cref="UInt4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="zw">The value to assign to the third and fourth vector components.</param>
        public UInt4(uint x, uint y, UInt2 zw)
        {
            this.x = x;
            this.y = y;
            this.z = zw.X;
            this.w = zw.Y;
        }

        /// <summary>
        /// Creates a new <see cref="UInt4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xy">The value to assign to the first and second vector components.</param>
        /// <param name="zw">The value to assign to the third and fourth vector components.</param>
        public UInt4(UInt2 xy, UInt2 zw)
        {
            this.x = xy.X;
            this.y = xy.Y;
            this.z = zw.X;
            this.w = zw.Y;
        }

        /// <summary>
        /// Creates a new <see cref="UInt4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
        /// <param name="w">The value to assign to the fourth vector component.</param>
        public UInt4(UInt3 xyz, uint w)
        {
            this.x = xyz.X;
            this.y = xyz.Y;
            this.z = xyz.Z;
            this.w = w;
        }

        /// <summary>
        /// Creates a new <see cref="UInt4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="yzw">The value to assign to the second, third and fourth vector components.</param>
        public UInt4(uint x, UInt3 yzw)
        {
            this.x = x;
            this.y = yzw.X;
            this.z = yzw.Y;
            this.w = yzw.Z;
        }

        /// <summary>
        /// Creates a new <see cref="UInt4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt4"/> instance.</param>
        public static implicit operator UInt4(uint x) => new(x, x, x, x);

        /// <summary>
        /// Casts a <see cref="UInt4"/> value to a <see cref="Int4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static explicit operator Int4(UInt4 xyzw) => default;

        /// <summary>
        /// Casts a <see cref="UInt4"/> value to a <see cref="Float4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static implicit operator Float4(UInt4 xyzw) => default;

        /// <summary>
        /// Casts a <see cref="UInt4"/> value to a <see cref="Double4"/> one.
        /// </summary>
        /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static implicit operator Double4(UInt4 xyzw) => default;
    }
}
