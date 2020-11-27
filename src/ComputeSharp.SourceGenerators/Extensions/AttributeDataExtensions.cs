using System.Diagnostics.Contracts;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for the <see cref="AttributeData"/> type.
    /// </summary>
    internal static class AttributeDataExtension
    {
        /// <summary>
        /// Tries to get a named argument from a given <see cref="AttributeData"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of value to retrieve.</typeparam>
        /// <param name="attributeData">The input <see cref="AttributeData"/> instance to get the value from.</param>
        /// <param name="key">The name of the argument to retrieve.</param>
        /// <param name="value">The resulting value, if found.</param>
        /// <returns>Whether or not the requested argument was found.</returns>
        [Pure]
        public static bool TryGetNamedArgument<T>(this AttributeData attributeData, string key, out T? value)
        {
            foreach (var argument in attributeData.NamedArguments)
            {
                if (argument.Key.Equals(key))
                {
                    value = (T?)argument.Value.Value;

                    return true;
                }
            }

            value = default;

            return false;
        }
    }
}
