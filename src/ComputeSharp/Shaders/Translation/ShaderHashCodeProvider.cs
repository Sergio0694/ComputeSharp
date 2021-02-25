using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="static"/> <see langword="class"/> that handles the cache system for the generated shaders.
    /// </summary>
    internal static class ShaderHashCodeProvider
    {
        /// <summary>
        /// A type containing static info for a given shader type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of compute shader currently in use.</typeparam>
        private static class TypeInfo<T>
            where T : struct, IComputeShader
        {
            /// <summary>
            /// The id of the compute shader type in use.
            /// </summary>
            public static readonly int Id;

            /// <summary>
            /// The optional <see cref="Hasher{T}"/> instance, if needed.
            /// </summary>
            public static readonly Hasher<T>? Hasher;

            /// <summary>
            /// Initializes the static members of the <see cref="TypeInfo{T}"/> type.
            /// </summary>
            static TypeInfo()
            {
                Id = HashCode.Combine(typeof(T));

                // Get the generated hasher, if one if present
                if (typeof(T).Assembly.GetType("ComputeSharp.__Internals.HashCodeProvider") is Type type &&
                    type.GetMethod("CombineHash", new[] { typeof(int), typeof(T).MakeByRefType() }) is MethodInfo method)
                {
                    Hasher = method.CreateDelegate<Hasher<T>>();
                }
            }
        }

        /// <summary>
        /// Gets a unique hashcode for the input compute shader instance.
        /// </summary>
        /// <typeparam name="T">The type of compute shader currently in use.</typeparam>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the shader to run.</param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCode<T>(in T shader)
            where T : struct, IComputeShader
        {
            int hash = TypeInfo<T>.Id;

            Hasher<T>? hasher = TypeInfo<T>.Hasher;

            if (hasher != null) hash = hasher(hash, in shader);

            return hash;
        }

        /// <summary>
        /// A <see langword="delegate"/> that represents an aggregate hash function for a given <typeparamref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of compute shader currently in use.</typeparam>
        /// <param name="hash">The initial hash value.</param>
        /// <param name="shader">The compute shader instance to use to compute the final hash value.</param>
        /// <returns>The final hash value for the input closure.</returns>
        private delegate int Hasher<T>(int hash, in T shader)
            where T : struct, IComputeShader;
    }
}
