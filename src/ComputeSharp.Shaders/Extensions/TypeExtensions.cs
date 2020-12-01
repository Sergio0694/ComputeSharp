using System;
using System.Diagnostics.Contracts;

namespace ComputeSharp.Shaders.Extensions
{
    /// <summary>
    /// A <see langword="class"/> that provides extension methods for the <see cref="Type"/> type.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Gets whether or not the input <see cref="Type"/> represents a <see cref="Delegate"/>.
        /// </summary>
        /// <param name="type">The input type to analyze.</param>
        [Pure]
        public static bool IsDelegate(this Type type)
        {
            return type.IsSubclassOf(typeof(Delegate));
        }
    }
}
