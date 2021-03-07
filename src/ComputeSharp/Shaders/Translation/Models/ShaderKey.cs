using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="struct"/> representing a key for a given shader.
    /// </summary>
    internal readonly struct ShaderKey : IEquatable<ShaderKey>
    {
        /// <summary>
        /// The hashcode of the current shader type.
        /// </summary>
        private readonly int Id;

        /// <summary>
        /// The number of iterations to run on the X axis.
        /// </summary>
        private readonly int ThreadsX;

        /// <summary>
        /// The number of iterations to run on the Y axis.
        /// </summary>
        private readonly int ThreadsY;

        /// <summary>
        /// The number of iterations to run on the Z axis.
        /// </summary>
        private readonly int ThreadsZ;

        /// <summary>
        /// Creates a new <see cref="ShaderKey"/> instance with the specified parameters.
        /// </summary>
        /// <param name="id">The hashcode of the current shader type.</param>
        /// <param name="threadsX">The number of iterations to run on the X axis.</param>
        /// <param name="threadsY">The number of iterations to run on the Y axis.</param>
        /// <param name="threadsZ">The number of iterations to run on the Z axis.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ShaderKey(int id, int threadsX, int threadsY, int threadsZ)
        {
            Id = id;
            ThreadsX = threadsX;
            ThreadsY = threadsY;
            ThreadsZ = threadsZ;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ShaderKey other)
        {
            return
                Id == other.Id &&
                ThreadsX == other.ThreadsX &&
                ThreadsY == other.ThreadsY &&
                ThreadsZ == other.ThreadsZ;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is ShaderKey other && Equals(other);
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ThreadsX, ThreadsY, ThreadsZ);
        }
    }
}
