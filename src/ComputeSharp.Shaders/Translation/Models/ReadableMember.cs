using System;
using System.Reflection;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="class"/> that wraps a readable member of another <see langword="class"/> and provides access to it
    /// </summary>
    internal sealed partial class ReadableMember
    {
        /// <summary>
        /// The id of the current member
        /// </summary>
        private readonly string Id;

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
            Id = $"{DeclaringType.FullName}{Type.Delimiter}{Name}".Replace(Type.Delimiter, '_');
        }

        /// <summary>
        /// Gets the <see cref="Type"/> where the current member is declared in
        /// </summary>
        public Type DeclaringType
        {
            get
            {
                if (Field != null) return Field.DeclaringType;
                if (Property != null) return Property.DeclaringType;

                throw new InvalidOperationException("Field and property can't both be null at the same time");
            }
        }

        /// <summary>
        /// Gets the <see cref="Type"/> of the wrapped member for the current instance
        /// </summary>
        public Type MemberType
        {
            get
            {
                if (Field != null) return Field.FieldType;
                if (Property != null) return Property.PropertyType;

                throw new InvalidOperationException("Field and property can't both be null at the same time");
            }
        }

        /// <summary>
        /// Gets the name of the wrapped member for the current instance
        /// </summary>
        public string Name => (Field?.Name ?? Property?.Name) ?? throw new InvalidOperationException("Field and property can't both be null at the same time");

        /// <summary>
        /// Gets whether or not the current member is not an instance member
        /// </summary>
        public bool IsStatic
        {
            get
            {
                if (Field != null) return Field.IsStatic;
                if (Property != null) return Property.GetMethod.IsStatic;

                throw new InvalidOperationException("Field and property can't both be null at the same time");
            }
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
