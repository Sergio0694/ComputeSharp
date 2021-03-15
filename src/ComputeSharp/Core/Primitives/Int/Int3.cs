namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="int3"/> HLSL type.
    /// </summary>
    public partial struct Int3
    {
        /// <summary>
        /// Gets an <see cref="Int3"/> value with all components set to 0.
        /// </summary>
        public static Int3 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Int3"/> value with all components set to 1.
        /// </summary>
        public static Int3 One => 1;

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static Int3 UnitX => new(1, 0, 0);

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static Int3 UnitY => new(0, 1, 0);

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
        /// </summary>
        public static Int3 UnitZ => new(0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="Int3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        public Int3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Creates a new <see cref="Int3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xy">The value to assign to the first and second vector components.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        public Int3(Int2 xy, int z)
        {
            this.x = xy.X;
            this.y = xy.Y;
            this.z = z;
        }

        /// <summary>
        /// Creates a new <see cref="Int3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="yz">The value to assign to the second and thirt vector components.</param>
        public Int3(int x, Int2 yz)
        {
            this.x = x;
            this.y = yz.X;
            this.z = yz.Y;
        }

        /// <summary>
        /// Creates a new <see cref="Int3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Int3"/> instance.</param>
        public static implicit operator Int3(int x) => new(x, x, x);

        /// <summary>
        /// Casts a <see cref="Int3"/> value to a <see cref="UInt3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Int3"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static explicit operator UInt3(Int3 xyz) => default;

        /// <summary>
        /// Casts a <see cref="Int3"/> value to a <see cref="Float3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Int3"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static implicit operator Float3(Int3 xyz) => default;

        /// <summary>
        /// Casts a <see cref="Int3"/> value to a <see cref="Double3"/> one.
        /// </summary>
        /// <param name="xyz">The input <see cref="Int3"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static implicit operator Double3(Int3 xyz) => default;
    }
}
