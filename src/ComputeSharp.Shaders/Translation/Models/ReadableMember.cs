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
        /// <param name="memberInfo">The target <see cref="MemberInfo"/> object wrapped by the current instance</param>
        private ReadableMember(MemberInfo memberInfo)
        {
            // General properties
            Name = memberInfo.Name;
            Id = $"{memberInfo.DeclaringType.FullName}{Type.Delimiter}{Name}";
            DeclaringType = memberInfo.DeclaringType;

            // Type specific properties
            if (memberInfo is FieldInfo fieldInfo)
            {
                Field = fieldInfo;
                IsStatic = Field.IsStatic;
                MemberType = Field.FieldType;
            }
            else if (memberInfo is PropertyInfo propertyInfo)
            {
                Property = propertyInfo;
                IsStatic = Property.GetMethod.IsStatic;
                MemberType = Property.PropertyType;
            }
            else throw new InvalidOperationException("Field and property can't both be null at the same time");
        }

        /// <summary>
        /// Gets the name of the wrapped member for the current instance
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets whether or not the current member is not an instance member
        /// </summary>
        public bool IsStatic { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> where the current member is declared in
        /// </summary>
        public Type DeclaringType { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> of the wrapped member for the current instance
        /// </summary>
        public Type MemberType { get; }

        /// <summary>
        /// Converts a <see cref="FieldInfo"/> object into a <see cref="ReadableMember"/> instance
        /// </summary>
        /// <param name="field">The input <see cref="FieldInfo"/> object to wrap</param>
        public static implicit operator ReadableMember(FieldInfo field) => new ReadableMember(field);

        /// <summary>
        /// Converts a <see cref="PropertyInfo"/> object into a <see cref="ReadableMember"/> instance
        /// </summary>
        /// <param name="property">The input <see cref="PropertyInfo"/> object to wrap</param>
        public static implicit operator ReadableMember(PropertyInfo property) => new ReadableMember(property);
    }
}
