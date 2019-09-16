using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using ComputeSharp.Shaders.Extensions;
using ComputeSharp.Shaders.Mappings;

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="static"/> <see langword="class"/> that handles the cache system for the generated shaders
    /// </summary>
    internal static class ShaderHashCodeProvider
    {
        /// <summary>
        /// The mapping of hashcodes to aggregate hashing functions
        /// </summary>
        private static readonly Dictionary<int, Hasher?> HasherMapping = new Dictionary<int, Hasher?>();

        /// <summary>
        /// Gets a unique hashcode for the input shader closure
        /// </summary>
        /// <param name="action">The input <see cref="Action{T}"/> representing the shader to run</param>
        [Pure]
        public static int GetHashCode(Action<ThreadIds> action)
        {
            // Get the delegate hashcode
            MethodInfo methodInfo = action.Method;
            int hash = methodInfo.GetHashCode();

            if (!HasherMapping.TryGetValue(hash, out Hasher? hasher))
            {
                // Get the candidate fields for delegate checking
                FieldInfo[] delegateFieldInfos = (
                    from fieldInfo in methodInfo.DeclaringType.GetFields()
                    where fieldInfo.FieldType.IsDelegate() &&
                          fieldInfo.FieldType.GenericTypeArguments.All(type => HlslKnownTypes.IsKnownScalarType(type) || HlslKnownTypes.IsKnownVectorType(type))
                    select fieldInfo).ToArray();

                // If at least one captured delegate is present, build the hasher method
                hasher = delegateFieldInfos.Length == 0 ? null : BuildDynamicHasher(action.Method.DeclaringType, delegateFieldInfos);
                HasherMapping.Add(hash, hasher);
            }

            // Aggregate the hash of the captured delegates, if needed
            if (hasher != null) hash = hasher(hash, action.Target);

            return hash;
        }

        /// <summary>
        /// A <see langword="delegate"/> that represents an aggregate hash function for a given instance
        /// </summary>
        /// <param name="obj">The source object to get the member from</param>
        /// <returns>The value of the member, upcast to <see cref="object"/></returns>
        private delegate int Hasher(int hash, object instance);

        /// <summary>
        /// Builds a new <see cref="Hasher"/> instance for the target <see cref="Type"/> and sequence of <see cref="FieldInfo"/> values
        /// </summary>
        /// <param name="declaringType">The declaring <see cref="Type"/> to analyze</param>
        /// <param name="fieldInfos">The list of captured fields to inspect</param>
        [Pure]
        private static Hasher BuildDynamicHasher(Type declaringType, IReadOnlyCollection<FieldInfo> fieldInfos)
        {
            // Create a new dynamic method for the current member
            DynamicMethod method = new DynamicMethod("GetHashCodeForHlslDelegates", typeof(int), new[] { typeof(int), typeof(object) }, declaringType);
            ILGenerator il = method.GetILGenerator();

            // Declare the local variable to store the target instance
            il.DeclareLocal(declaringType);

            // Load the argument (the object instance)
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Castclass, declaringType);
            il.Emit(OpCodes.Stloc_0);

            // Unroll the member access
            MethodInfo
                validateDelegateInfo = typeof(ShaderHashCodeProvider).GetMethod(nameof(ValidateDelegate), BindingFlags.NonPublic | BindingFlags.Static),
                getMethodInfo = typeof(Delegate).GetProperty(nameof(Delegate.Method)).GetMethod,
                getHashCodeInfo = typeof(object).GetMethod(nameof(GetHashCode));
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Label label = il.DefineLabel();

                // if (fieldInfo.GetValue(obj) != null) {
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldfld, fieldInfo);
                il.Emit(OpCodes.Brfalse_S, label);

                // if (ValidateDelegate(field)) {
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldfld, fieldInfo);
                il.EmitCall(OpCodes.Call, validateDelegateInfo, null);
                il.Emit(OpCodes.Brfalse_S, label);

                // hash = hash * 17 + field.Method.GetHashCode();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldc_I4, 17);
                il.Emit(OpCodes.Mul);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldfld, fieldInfo);
                il.EmitCall(OpCodes.Callvirt, getMethodInfo, null);
                il.EmitCall(OpCodes.Callvirt, getHashCodeInfo, null);
                il.Emit(OpCodes.Add);
                il.Emit(OpCodes.Starg_S, 0);

                il.MarkLabel(label);
            }

            // Return the computed hash
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ret);

            // Build and return the delegate
            return (Hasher)method.CreateDelegate(typeof(Hasher));
        }

        /// <summary>
        /// A method that checks whether an input <see cref="Delegate"/> is supported in HLSL
        /// </summary>
        /// <param name="candidate">The input <see cref="Delegate"/> instance to check</param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ValidateDelegate(Delegate candidate)
        {
            MethodInfo methodInfo = candidate.Method;
            return (methodInfo.IsStatic || methodInfo.DeclaringType.IsStatelessDelegateContainer()) &&
                   (HlslKnownTypes.IsKnownScalarType(methodInfo.ReturnType) || HlslKnownTypes.IsKnownVectorType(methodInfo.ReturnType));
        }
    }
}
