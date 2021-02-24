using System.Diagnostics;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="bool4"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z}, {W})")]
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
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Creates a new <see cref="Bool4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool4"/> instance.</param>
        public static implicit operator Bool4(bool x) => new(x, x, x, x);
    }
}
