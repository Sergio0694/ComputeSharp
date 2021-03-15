namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="int2"/> HLSL type.
    /// </summary>
    public partial struct Int2
    {
        /// <summary>
        /// Gets an <see cref="Int2"/> value with all components set to 0.
        /// </summary>
        public static Int2 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Int2"/> value with all components set to 1.
        /// </summary>
        public static Int2 One => 1;

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="X"/> component set to 1, and the others to 0.
        /// </summary>
        public static Int2 UnitX => new(1, 0);

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
        /// </summary>
        public static Int2 UnitY => new(0, 1);

        /// <summary>
        /// Creates a new <see cref="Int2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Creates a new <see cref="Int2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Int2"/> instance.</param>
        public static implicit operator Int2(int x) => new(x, x);

        /// <summary>
        /// Casts a <see cref="Int2"/> value to a <see cref="UInt2"/> one.
        /// </summary>
        /// <param name="xy">The input <see cref="Int2"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static explicit operator UInt2(Int2 xy) => default;

        /// <summary>
        /// Casts a <see cref="Int2"/> value to a <see cref="Float2"/> one.
        /// </summary>
        /// <param name="xy">The input <see cref="Int2"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static implicit operator Float2(Int2 xy) => default;

        /// <summary>
        /// Casts a <see cref="Int2"/> value to a <see cref="Double2"/> one.
        /// </summary>
        /// <param name="xy">The input <see cref="Int2"/> value to cast.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static implicit operator Double2(Int2 xy) => default;
    }
}
