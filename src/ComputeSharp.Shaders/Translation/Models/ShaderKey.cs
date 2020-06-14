using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="struct"/> representing a key for a given shader
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal readonly struct ShaderKey : IEquatable<ShaderKey>
    {
        /// <summary>
        /// The hashcode of the current shader type
        /// </summary>
        [FieldOffset(0)]
        private readonly int Id;

        /// <summary>
        /// The number of iterations to run on the X axis
        /// </summary>
        [FieldOffset(4)]
        private readonly int ThreadsX;

        /// <summary>
        /// The number of iterations to run on the Y axis
        /// </summary>
        [FieldOffset(8)]
        private readonly int ThreadsY;

        /// <summary>
        /// The number of iterations to run on the Z axis
        /// </summary>
        [FieldOffset(12)]
        private readonly int ThreadsZ;

        /// <summary>
        /// Creates a new <see cref="ShaderKey"/> instance with the specified parameters
        /// </summary>
        /// <param name="id">The hashcode of the current shader type</param>
        /// <param name="threadsX">The number of iterations to run on the X axis</param>
        /// <param name="threadsY">The number of iterations to run on the Y axis</param>
        /// <param name="threadsZ">The number of iterations to run on the Z axis</param>
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
                Unsafe.As<int, ulong>(ref Unsafe.AsRef(Id))
                == Unsafe.As<int, ulong>(ref Unsafe.AsRef(other.Id)) &&
                Unsafe.As<int, ulong>(ref Unsafe.AsRef(ThreadsY))
                == Unsafe.As<int, ulong>(ref Unsafe.AsRef(other.ThreadsY));
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
            ulong ul0 = Unsafe.As<int, ulong>(ref Unsafe.AsRef(Id));

            int hash = unchecked((int)ul0) ^ (int)(ul0 >> 32);

            ulong ul1 = Unsafe.As<int, ulong>(ref Unsafe.AsRef(ThreadsY));

            hash = (hash << 5) + hash + unchecked((int)ul1) ^ ((int)(ul1 >> 32));

            return hash;
        }
    }
}
