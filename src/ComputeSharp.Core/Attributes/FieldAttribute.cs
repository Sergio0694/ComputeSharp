using System;

namespace ComputeSharp
{
    /// <summary>
    /// An attribute that indicates a field to declare in a custom struct type to use in a shader.
    /// This is necessary to be used instead of manually declare fields, so that the fields can be
    /// declared with the right offset into the type data, as HLSL requires precise alignment for values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class FieldAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="FieldAttribute"/> instance with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the field to declare.</param>
        /// <param name="type">The type of field to declare.</param>
        public FieldAttribute(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Gets the name of the field to declare.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the field to declare.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the optional summary for the field to declare.
        /// </summary>
        public string? Summary { get; init; }

        /// <summary>
        /// Gets the optional remarks for the field to declare.
        /// </summary>
        public string? Remarks { get; init; }
    }
}