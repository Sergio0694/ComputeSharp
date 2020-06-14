using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="static"/> <see langword="class"/> that handles the cache system for the generated shaders
    /// </summary>
    internal static class ShaderHashCodeProvider
    {
        /// <summary>
        /// A type containing static info for a given shader type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of compute shader currently in use</typeparam>
        private static class TypeInfo<T>
            where T : struct, IComputeShader
        {
            /// <summary>
            /// The id of the compute shader type in use
            /// </summary>
            public static readonly int Id;

            /// <summary>
            /// The optional <see cref="Hasher{T}"/> instance, if needed
            /// </summary>
            public static readonly Hasher<T>? Hasher;

            /// <summary>
            /// Initializes the static members of the <see cref="TypeInfo{T}"/> type
            /// </summary>
            static TypeInfo()
            {
                Id = HashCode.Combine(typeof(T));

                // Get the valid delegates for delegate checking
                FieldInfo[] delegateFieldInfos = (
                    from fieldInfo in typeof(T).GetFields()
                    where fieldInfo.FieldType.IsDelegate()
                    select fieldInfo).ToArray();

                // If at least one delegate is present, build the hasher method
                if (delegateFieldInfos.Length > 0)
                {
                    Hasher = BuildDynamicHasher<T>(delegateFieldInfos);
                }
            }
        }

        /// <summary>
        /// Gets a unique hashcode for the input compute shader instance
        /// </summary>
        /// <typeparam name="T">The type of compute shader currently in use</typeparam>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the shader to run</param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCode<T>(in T shader)
            where T : struct, IComputeShader
        {
            int hash = TypeInfo<T>.Id;

            Hasher<T>? hasher = TypeInfo<T>.Hasher;

            if (hasher != null) hash = hasher(hash, shader);

            return hash;
        }

        /// <summary>
        /// A <see langword="delegate"/> that represents an aggregate hash function for a given <typeparamref name="T"/> instance
        /// </summary>
        /// <typeparam name="T">The type of compute shader currently in use</typeparam>
        /// <param name="hash">The initial hash value</param>
        /// <param name="shader">The compute shader instance to use to compute the final hash value</param>
        /// <returns>The final hash value for the input closure</returns>
        private delegate int Hasher<T>(int hash, in T shader)
            where T : struct, IComputeShader;

        /// <summary>
        /// Builds a new <see cref="Hasher{T}"/> instance for the target <see cref="Type"/> and sequence of <see cref="FieldInfo"/> values
        /// </summary>
        /// <typeparam name="T">The type of compute shader currently in use</typeparam>
        /// <param name="fieldInfos">The list of captured fields to inspect</param>
        [Pure]
        private static Hasher<T> BuildDynamicHasher<T>(IReadOnlyCollection<FieldInfo> fieldInfos)
            where T : struct, IComputeShader
        {
            return DynamicMethod<Hasher<T>>.New(il =>
            {
                MethodInfo
                    getMethodInfo = typeof(Delegate).GetProperty(nameof(Delegate.Method)).GetMethod,
                    getHashCodeInfo = typeof(object).GetMethod(nameof(object.GetHashCode));
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    // (hashcode << 5) + hashcode
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldc_I4_5);
                    il.Emit(OpCodes.Shl);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Add);

                    // hashcode += shader.Function[#].Method.GetHashCode();
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Ldfld, fieldInfo);
                    il.EmitCall(OpCodes.Callvirt, getMethodInfo, null);
                    il.EmitCall(OpCodes.Callvirt, getHashCodeInfo, null);
                    il.Emit(OpCodes.Add);
                    il.Emit(OpCodes.Starg_S, (byte)0);
                }

                // Return the computed hash
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ret);
            });
        }
    }
}
