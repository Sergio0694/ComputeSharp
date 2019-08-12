using System.Runtime.CompilerServices;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that indicates the number of threads to use to run a given compute shader
    /// </summary>
    public readonly struct NumThreads
    {
        /// <summary>
        /// The X id of the current thread
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The Y id of the current thread
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// The Z id of the current thread
        /// </summary>
        public readonly int Z;

        /// <summary>
        /// Creates a new <see cref="NumThreads"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The number of threads in the X axis</param>
        /// <param name="y">The number of threads in the Y axis</param>
        public NumThreads(int x, int y)
        {
            X = x;
            Y = y;
            Z = 1;
        }

        /// <summary>
        /// Creates a new <see cref="NumThreads"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The number of threads in the X axis</param>
        /// <param name="y">The number of threads in the Y axis</param>
        /// <param name="z">The number of threads in the Z axis</param>
        public NumThreads(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Creates a new <see cref="NumThreads"/> instance from a value tuple
        /// </summary>
        /// <param name="xy">The tuple with the thread X and Y counts</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NumThreads((int X, int Y) xy) => new NumThreads(xy.X, xy.Y);

        /// <summary>
        /// Creates a new <see cref="NumThreads"/> instance from a value tuple
        /// </summary>
        /// <param name="xy">The tuple with the thread X, Y and Z counts</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NumThreads((int X, int Y, int Z) xyz) => new NumThreads(xyz.X, xyz.Y, xyz.Z);
    }
}
