using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using ComputeSharp.Shaders.Extensions;

namespace ComputeSharp.Shaders.Translation.Models
{
    internal sealed partial class ReadableMember
    {
        /// <summary>
        /// The mapping of members to getter delegates
        /// </summary>
        private static readonly Dictionary<string, Getter> GettersMapping = new Dictionary<string, Getter>();

        /// <summary>
        /// The local <see cref="Getter"/> instance, if already loaded
        /// </summary>
        private Getter? _Getter;

        /// <summary>
        /// Ensures the dynamic accessor for the current instance is loaded and ready to use
        /// </summary>
        public void PreloadAccessor()
        {
            if (_Getter == null)
            {
                /* If the local delegate is available, use it and save the dictionary
                 * access entirely. If it's not, try to get the delegate from the dictionary
                 * first. This can save unnecessary overhead if a delegate for the same member has
                 * already been built, eg. if it belongs to a captured field that is used by two different
                 * shaders in the same closure. Once the getter is built, cache it and invoke it */
                if (GettersMapping.TryGetValue(Id, out Getter getter)) _Getter = getter;
                else
                {
                    _Getter = getter = BuildDynamicGetter();
                    GettersMapping.Add(Id, getter);
                }
            }
        }

        /// <summary>
        /// Returns the value of the wrapped member for the current instance
        /// </summary>
        /// <param name="instance">The target instance to use to read the value from</param>
        /// <remarks>This method doesn't ensure that the accessor is available</remarks>
        /// <exception cref="NullReferenceException">The dynamic accessor is not available</exception>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object DangerousGetValue(object? instance) => _Getter!(instance);

        /// <summary>
        /// Returns the value of the wrapped member for the current instance
        /// </summary>
        /// <param name="instance">The target instance to use to read the value from</param>
        [Pure]
        public object GetValue(object? instance)
        {
            PreloadAccessor();
            return _Getter!(instance);
        }

        /// <summary>
        /// A <see langword="delegate"/> that represents a getter for a specific member
        /// </summary>
        /// <param name="obj">The source object to get the member from</param>
        /// <returns>The value of the member, upcast to <see cref="object"/></returns>
        private delegate object Getter(object? obj);

        /// <summary>
        /// Gets or sets the list of parent members to access the current one from the root instance
        /// </summary>
        public IEnumerable<ReadableMember>? Parents { get; set; }

        /// <summary>
        /// Builds a dynamic IL method to retrieve the value of the current member
        /// </summary>
        [Pure]
        private Getter BuildDynamicGetter()
        {
            return DynamicMethod<Getter>.New(il =>
            {
                // Info for hierarchical access
                ReadableMember[] hierarchy = (Parents ?? Array.Empty<ReadableMember>()).Concat(new[] { this }).ToArray();

                // Load the argument (the object instance) and cast it to the right type, if needed
                if (!IsStatic)
                {
                    il.Emit(OpCodes.Ldarg_0);

                    Type unboxType = hierarchy[0].DeclaringType;
                    il.Emit(unboxType.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, unboxType);
                }

                // Unroll the member access
                foreach (ReadableMember member in hierarchy)
                {
                    // Get the member value with the appropriate method
                    if (member.Property != null) il.EmitCall(member.IsStatic ? OpCodes.Call : OpCodes.Callvirt, member.Property.GetMethod, null);
                    else if (member.Field != null) il.Emit(member.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, member.Field);
                    else throw new InvalidOperationException("Field and property can't both be null at the same time");
                }

                // Box the value, if needed
                if (MemberType.IsValueType) il.Emit(OpCodes.Box, MemberType);

                // Return the member value from the top of the evaluation stack
                il.Emit(OpCodes.Ret);
            });
        }
    }
}
