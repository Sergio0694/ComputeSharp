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
        /// The <see cref="MemberInfo"/> object wrapped by the current instance
        /// </summary>
        private readonly MemberInfo Member;

        /// <summary>
        /// Creates a new <see cref="ReadableMember"/> instance with the given parameters
        /// </summary>
        /// <param name="memberInfo">The target <see cref="MemberInfo"/> object wrapped by the current instance</param>
        private ReadableMember(MemberInfo memberInfo)
        {
            // General properties
            Id = $"{memberInfo.DeclaringType.FullName}{Type.Delimiter}{memberInfo.Name}";
            Member = memberInfo;
            Name = memberInfo.Name;
            DeclaringType = memberInfo.DeclaringType;

            // Type specific properties
            if (memberInfo is FieldInfo fieldInfo)
            {
                Member = fieldInfo;
                IsStatic = fieldInfo.IsStatic;
                MemberType = fieldInfo.FieldType;
            }
            else if (memberInfo is PropertyInfo propertyInfo)
            {
                Member = propertyInfo;
                IsStatic = propertyInfo.GetMethod.IsStatic;
                MemberType = propertyInfo.PropertyType;
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

        /// <summary>
        /// Converts a <see cref="ReadableMember"/> object into a <see cref="MemberInfo"/> instance
        /// </summary>
        /// <param name="member">The input <see cref="ReadableMember"/> object to unwrap</param>
        public static implicit operator MemberInfo(ReadableMember member) => member.Member;

        /// <summary>
        /// Converts a <see cref="ReadableMember"/> object into a <see cref="FieldInfo"/> instance
        /// </summary>
        /// <param name="member">The input <see cref="ReadableMember"/> object to unwrap</param>
        public static implicit operator FieldInfo(ReadableMember member) => (FieldInfo)member.Member;

        /// <summary>
        /// Converts a <see cref="ReadableMember"/> object into a <see cref="PropertyInfo"/> instance
        /// </summary>
        /// <param name="member">The input <see cref="ReadableMember"/> object to unwrap</param>
        public static implicit operator PropertyInfo(ReadableMember member) => (PropertyInfo)member.Member;
    }
}
