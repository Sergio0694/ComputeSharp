using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection.Emit;

namespace ComputeSharp.Shaders.Translation.Models
{
    internal sealed partial class ReadableMember
    {
        /// <summary>
        /// The mapping of members to getter delegates
        /// </summary>
        private static readonly Dictionary<string, Getter> GettersMapping = new Dictionary<string, Getter>();

        /// <summary>
        /// Returns the value of the wrapped member for the current instance
        /// </summary>
        /// <param name="instance">The target instance to use to read the value from</param>
        [Pure]
        public object GetValue(object? instance)
        {
            if (!GettersMapping.TryGetValue(Id, out Getter getter))
            {
                getter = BuildDynamicGetter();
                GettersMapping.Add(Id, getter);
            }

            return getter(instance);
        }

        /// <summary>
        /// A <see langword="delegate"/> that represents a getter for a specific member
        /// </summary>
        /// <param name="obj">The source object to get the member from</param>
        /// <returns>The value of the member, upcast to <see cref="object"/></returns>
        private delegate object Getter(object? obj);

        /// <summary>
        /// Builds a dynamic IL method to retrieve the value of the current member
        /// </summary>
        [Pure]
        private Getter BuildDynamicGetter()
        {
            // Create a new dynamic method for the current member
            DynamicMethod method = new DynamicMethod($"Get{Name}", typeof(object), new[] { typeof(object) }, DeclaringType);
            ILGenerator il = method.GetILGenerator();

            // Load the argument (the object instance) and cast it to the right type, if needed
            if (!IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(DeclaringType.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, DeclaringType);
            }

            // Get the member value with the appropriate method
            if (Property != null) il.EmitCall(IsStatic ? OpCodes.Call : OpCodes.Callvirt, Property.GetMethod, null);
            else if (Field != null) il.Emit(IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, Field);
            else throw new InvalidOperationException("Field and property can't both be null at the same time");

            // Box the value, if needed
            if (MemberType.IsValueType) il.Emit(OpCodes.Box, MemberType);

            // Return the member value from the top of the evaluation stack
            il.Emit(OpCodes.Ret);

            // Build and return the delegate
            return (Getter)method.CreateDelegate(typeof(Getter));
        }
    }
}
