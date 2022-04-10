﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Intrinsics.Attributes;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <summary>
/// A <see langword="class"/> that contains and maps known HLSL identifier names valid HLSL names.
/// </summary>
internal static partial class HlslKnownKeywords
{
    /// <summary>
    /// The mapping of known HLSL keywords.
    /// </summary>
    private static readonly IReadOnlyCollection<string> KnownKeywords = BuildKnownKeywordsMap();

    /// <summary>
    /// Builds the mapping of all known HLSL keywords.
    /// </summary>
    private static IReadOnlyCollection<string> BuildKnownKeywordsMap()
    {
        // HLSL keywords
        HashSet<string> knownKeywords = new(new[]
        {
            "asm", "asm_fragment", "cbuffer", "centroid", "column_major",
            "compile", "discard", "dword", "export", "fxgroup", "groupshared",
            "half", "inline", "inout", "line", "lineadj", "linear", "matrix",
            "nointerpolation", "noperspective", "NULL", "packoffset", "pass",
            "pixelfragment", "point", "precise", "register", "row_major", "sample",
            "sampler", "shared", "snorm", "stateblock", "stateblock_state", "tbuffer",
            "technique", "typedef", "triangle", "triangleadj", "uniform", "unorm",
            "unsigned", "vector", "vertexfragment", "zero"
        });

        // HLSL intrinsics method names
        foreach (var method in typeof(Hlsl).GetMethods(BindingFlags.Public | BindingFlags.Static))
        {
            string name = method.GetCustomAttribute<HlslIntrinsicNameAttribute>()?.Name ?? method.Name;

            knownKeywords.Add(name);
        }

        // Let other types inject additional keywords
        AddKnownKeyword(knownKeywords);

        return knownKeywords;
    }

    /// <summary>
    /// Adds more known keywords to the collection to use.
    /// </summary>
    /// <param name="knownKeywords">The collection of known keywords being built.</param>
    static partial void AddKnownKeyword(ICollection<string> knownKeywords);

    /// <summary>
    /// Tries to get the mapped HLSL-compatible identifier name for the input identifier name.
    /// </summary>
    /// <param name="name">The input identifier name.</param>
    /// <param name="mapped">The mapped identifier name, if a replacement is needed.</param>
    /// <returns>The HLSL-compatible identifier name that can be used in an HLSL shader.</returns>
    public static bool TryGetMappedName(string name, out string? mapped)
    {
        mapped = KnownKeywords.Contains(name) switch
        {
            true => $"__reserved__{name}",
            false => null
        };

        return mapped is not null;
    }
}
