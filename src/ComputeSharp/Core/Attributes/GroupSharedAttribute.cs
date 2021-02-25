using System;

namespace ComputeSharp
{
    /// <summary>
    /// An attribute that indicates that an array static field in a shader type will be shared in a thread group.
    /// <para>
    /// This attribute can be used to declare fields as follows:
    /// <code>
    /// struct MyShader : IComputeShader
    /// {
    ///     [GroupShared(32)]
    ///     static float[] MyBuffer;
    /// }
    /// </code>
    /// </para>
    /// The buffer shouldn't be manually initialized, as each shader invocation will automatically receive the
    /// right buffer in use for the thread group it belongs to. If there is an assignment for the field, it
    /// will just be ignored in the shader. Additionally, buffer types are not allowed as array items.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class GroupSharedAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="GroupSharedAttribute"/> instance with the specified parameters.
        /// </summary>
        public GroupSharedAttribute()
        {
        }

        /// <summary>
        /// Creates a new <see cref="GroupSharedAttribute"/> instance with the specified parameters.
        /// </summary>
        /// <param name="size">The size of the shared buffer to declare.</param>
        public GroupSharedAttribute(int size)
        {
            Size = size;
        }

        /// <summary>
        /// Gets the size of the shared buffer being declared.
        /// If <see langword="null"/>, the thread group size will be used.
        /// </summary>
        public int? Size { get; }
    }
}