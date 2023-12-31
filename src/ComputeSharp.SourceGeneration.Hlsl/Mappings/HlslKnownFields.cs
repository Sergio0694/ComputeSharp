using System.Collections.Generic;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <summary>
/// A <see langword="class"/> that contains and maps known HLSL field names to common .NET fields.
/// </summary>
internal static partial class HlslKnownFields
{
    /// <summary>
    /// The mapping of supported known fields to HLSL names.
    /// </summary>
    private static readonly Dictionary<string, string> KnownFields = BuildKnownFieldsMap();

    /// <summary>
    /// Builds the mapping of supported known fields to HLSL names.
    /// </summary>
    private static Dictionary<string, string> BuildKnownFieldsMap()
    {
        return new()
        {
            [$"float.{nameof(float.NaN)}"] = "asfloat(0xFFC00000)",
            [$"float.{nameof(float.PositiveInfinity)}"] = "asfloat(0x7F800000)",
            [$"float.{nameof(float.NegativeInfinity)}"] = "asfloat(0xFF800000)",

            [$"double.{nameof(double.NaN)}"] = "asdouble(0x00000000, 0xFFF80000)",
            [$"double.{nameof(double.PositiveInfinity)}"] = "asdouble(0x00000000, 0x7FF00000)",
            [$"double.{nameof(double.NegativeInfinity)}"] = "asdouble(0x00000000, 0xFFF00000)"
        };
    }

    /// <summary>
    /// Tries to get the mapped HLSL-compatible field name for the input field name.
    /// </summary>
    /// <param name="name">The input fully qualified field name.</param>
    /// <param name="mapped">The mapped name, if one is found.</param>
    /// <returns>The HLSL-compatible field name that can be used in an HLSL shader.</returns>
    public static bool TryGetMappedName(string name, out string? mapped)
    {
        return KnownFields.TryGetValue(name, out mapped);
    }
}