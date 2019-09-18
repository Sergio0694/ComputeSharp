using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// A <see langword="class"/> that provides extension methods for the <see langword="string"/> type
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Removes the left padding for an input multiline <see langword="string"/>
        /// </summary>
        /// <param name="text">The input <see langword="string"/> to process</param>
        /// <returns>A left-aligned <see langword="string"/> with the same text as the input</returns>
        [Pure]
        public static string RemoveLeftPadding(this string text)
        {
            string[] lines = text.Split(Environment.NewLine);
            int spaces = text.TakeWhile(c => c == ' ').Count();
            Regex regex = new Regex($"^{new string(Enumerable.Repeat(' ', spaces).ToArray())}");

            return lines.Aggregate(string.Empty, (result, line) => $"{result}{Environment.NewLine}{regex.Replace(line, string.Empty)}");
        }
    }
}
