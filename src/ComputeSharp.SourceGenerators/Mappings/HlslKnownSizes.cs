using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ComputeSharp.SourceGenerators.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL types to their alignment sizes.
    /// </summary>
    internal static class HlslKnownSizes
    {
        /// <summary>
        /// The mapping of supported known sizes to HLSL type names.
        /// </summary>
        private static IReadOnlyDictionary<string, (int, int)> KnownSizes = BuildKnownSizesMap();

        /// <summary>
        /// Builds the mapping of known type sizes and alignments.
        /// </summary>
        [Pure]
        private static IReadOnlyDictionary<string, (int, int)> BuildKnownSizesMap()
        {
            Dictionary<string, (int, int)> knownSizes = new()
            {
                [typeof(bool).FullName] = (4, 4),
                [typeof(int).FullName] = (4, 4),
                [typeof(uint).FullName] = (4, 4),
                [typeof(float).FullName] = (4, 4),
                [typeof(double).FullName] = (8, 8),

                [typeof(Vector2).FullName] = (8, 4),
                [typeof(Vector3).FullName] = (12, 4),
                [typeof(Vector4).FullName] = (16, 4)
            };

            foreach (
                var type in
                from type in Assembly.GetExecutingAssembly().ExportedTypes
                where Regex.IsMatch(type.FullName, @"^ComputeSharp\.(Bool|Double|Float|Int|UInt)")
                select type)
            {
                knownSizes.Add(type.FullName, (type.StructLayoutAttribute.Size, type.StructLayoutAttribute.Pack));
            }

            return knownSizes;
        }

        /// <summary>
        /// Gets the size and alignment info for a specified primitive type.
        /// </summary>
        /// <param name="typeName">The type name to get info for.</param>
        /// <returns>The size and and alignment for the input type.</returns>
        [Pure]
        public static (int Size, int Pack) GetTypeInfo(string typeName)
        {
            return KnownSizes[typeName];
        }
    }
}
