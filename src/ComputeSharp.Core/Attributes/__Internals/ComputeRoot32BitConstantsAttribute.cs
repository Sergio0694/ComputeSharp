using System;
using System.ComponentModel;

namespace ComputeSharp.__Internals
{
    /// <summary>
    /// An attribute that contains info on the number of 32 bit constants for a given shader.
    /// </summary>
    /// <remarks>This attribute is not meant to be directly used by applications using ComputeSharp.</remarks>
    [AttributeUsage(AttributeTargets.ReturnValue, AllowMultiple = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This attribute is not intended to be used directly by user code")]
    public sealed class ComputeRoot32BitConstantsAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="ComputeRoot32BitConstantsAttribute"/> instance with the specified parameters.
        /// </summary>
        /// <param name="count">The number of 32 bit constants for the annotated method.</param>
        public ComputeRoot32BitConstantsAttribute(int count)
        {
            Count = count;
        }

        /// <summary>
        /// Gets the number of 32 bit constants for the annotated method.
        /// </summary>
        internal int Count { get; }
    }
}