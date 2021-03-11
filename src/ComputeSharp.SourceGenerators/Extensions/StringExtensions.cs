using System.Diagnostics.Contracts;

namespace ComputeSharp.SourceGenerators.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="string"/> types.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Converts a given <see cref="string"/> instance to a valid HLSL identifier name.
        /// </summary>
        /// <param name="text">The input <see cref="string"/> instance to convert.</param>
        /// <returns>An valid HLSL identifier name from <paramref name="text"/>.</returns>
        [Pure]
        public static string ToHlslIdentifierName(this string text)
        {
            return text.Replace('.', '_').Replace('+', '_');
        }
    }
}
