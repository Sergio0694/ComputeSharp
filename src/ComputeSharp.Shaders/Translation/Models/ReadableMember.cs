using System;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="class"/> that wraps a readable member of another <see langword="class"/> and provides access to it
    /// </summary>
    internal sealed class ReadableMember
    {
        /// <summary>
        /// The <see cref="FieldInfo"/> object wrapped by the current instance, if present
        /// </summary>
        private readonly FieldInfo? Field;

        /// <summary>
        /// The <see cref="PropertyInfo"/> object wrapped by the current instance, if present
        /// </summary>
        private readonly PropertyInfo? Property;

        /// <summary>
        /// Creates a new <see cref="ReadableMember"/> instance with the given parameters
        /// </summary>
        /// <param name="field">The optional <see cref="FieldInfo"/> object wrapped by the current instance</param>
        /// <param name="property">The optional <see cref="PropertyInfo"/> object wrapped by the current instance</param>
        private ReadableMember(FieldInfo? field, PropertyInfo? property)
        {
            Field = field;
            Property = property;
        }

        /// <summary>
        /// Returns the value of the wrapped member for the current instance
        /// </summary>
        /// <param name="instance">The target instance to use to read the value from</param>
        [Pure]
        public object GetValue(object instance)
        {
            if (Field != null) return Field.GetValue(instance);
            if (Property != null) return Property.GetValue(instance);

            throw new InvalidOperationException("Field and property can't both be null at the same time");
        }

        /// <summary>
        /// Converts a <see cref="FieldInfo"/> object into a <see cref="ReadableMember"/> instance
        /// </summary>
        /// <param name="field">The input <see cref="FieldInfo"/> object to wrap</param>
        public static implicit operator ReadableMember(FieldInfo field) => new ReadableMember(field, null);

        /// <summary>
        /// Converts a <see cref="PropertyInfo"/> object into a <see cref="ReadableMember"/> instance
        /// </summary>
        /// <param name="property">The input <see cref="PropertyInfo"/> object to wrap</param>
        public static implicit operator ReadableMember(PropertyInfo property) => new ReadableMember(null, property);
    }
}
