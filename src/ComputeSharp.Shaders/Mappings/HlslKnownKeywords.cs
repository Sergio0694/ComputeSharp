using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace ComputeSharp.Shaders.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL method names to common .NET method
    /// </summary>
    internal static class HlslKnownKeywords
    {
        /// <summary>
        /// The mapping of supported known methods to HLSL methods
        /// </summary>
        private static readonly IReadOnlyCollection<string> KnownKeywords = new[]
        {
            "asm", "asm_fragment", "cbuffer", "centroid", "column_major",
            "compile", "discard", "dword", "export", "fxgroup", "groupshared",
            "half", "inline", "inout", "line", "lineadj", "linear", "matrix",
            "nointerpolation", "noperspective", "NULL", "packoffset", "pass",
            "pixelfragment", "point", "precise", "register", "row_major", "sample",
            "sampler", "shared", "snorm", "stateblock", "stateblock_state", "tbuffer",
            "technique", "typedef", "triangle", "triangleadj", "uniform", "unorm",
            "unsigned", "vector", "vertexfragment", "zero"
        };

        private static Regex? _KeywordsRegex;

        /// <summary>
        /// The <see cref="Regex"/> to use to match variable names
        /// </summary>
        private static Regex KeywordsRegex
        {
            get
            {
                if (_KeywordsRegex == null)
                {
                    string
                        keywords = string.Join("|", KnownKeywords),
                        pattern = $@"(?<![A-Za-z])(?:{keywords})(?!\w)";
                    _KeywordsRegex = new Regex(pattern, RegexOptions.Compiled);
                }

                return _KeywordsRegex;
            }
        }

        /// <summary>
        /// Gets the mapped HLSL-compatible variable name for the input identifier name
        /// </summary>
        /// <param name="name">The input variable name to map</param>
        /// <returns>The HLSL-compatible variable name name that can be used in an HLSL shader</returns>
        [Pure]
        public static string GetMappedName(string name) => KeywordsRegex.IsMatch(name) ? $"_{name}" : name;

        /// <summary>
        /// Processes an input text and replaces all the occurrences of reserved keywords
        /// </summary>
        /// <param name="text">The input text to process</param>
        /// <returns>A valid shader body source code that can be used in HLSL</returns>
        [Pure]
        public static string GetMappedText(string text) => KeywordsRegex.Replace(text, m => $"_{m.Value}");
    }
}
