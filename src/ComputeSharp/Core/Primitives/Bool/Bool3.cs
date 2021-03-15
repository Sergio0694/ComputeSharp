namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="bool3"/> HLSL type.
    /// </summary>
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
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Creates a new <see cref="Bool3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="xy">The value to assign to the first and second vector components.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        public Bool3(Bool2 xy, bool z)
        {
            this.x = xy.X;
            this.y = xy.Y;
            this.z = z;
        }

        /// <summary>
        /// Creates a new <see cref="Bool3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="yz">The value to assign to the second and thirt vector components.</param>
        public Bool3(bool x, Bool2 yz)
        {
            this.x = x;
            this.y = yz.X;
            this.z = yz.Y;
        }

        /// <summary>
        /// Creates a new <see cref="Bool3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool3"/> instance.</param>
        public static implicit operator Bool3(bool x) => new(x, x, x);
    }
}
