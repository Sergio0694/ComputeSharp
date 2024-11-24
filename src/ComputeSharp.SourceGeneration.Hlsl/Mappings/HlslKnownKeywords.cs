using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Intrinsics;

#pragma warning disable IDE0055

namespace ComputeSharp.SourceGeneration.Mappings;

/// <summary>
/// A <see langword="class"/> that contains and maps known HLSL identifier names valid HLSL names.
/// </summary>
internal static partial class HlslKnownKeywords
{
    /// <summary>
    /// The mapping of known HLSL keywords.
    /// </summary>
    private static readonly HashSet<string> KnownKeywords = BuildKnownKeywordsMap();

    /// <summary>
    /// Builds the mapping of all known HLSL keywords.
    /// </summary>
    private static HashSet<string> BuildKnownKeywordsMap()
    {
        // HLSL keywords
        HashSet<string> knownKeywords = new(
        [
            "asm", "asm_fragment", "cbuffer", "buffer", "texture", "centroid",
            "column_major", "compile", "discard", "dword", "export", "fxgroup",
            "groupshared", "half", "inline", "inout", "line", "lineadj", "linear",
            "matrix", "nointerpolation", "noperspective", "NULL", "packoffset", "pass",
            "pixelfragment", "point", "precise", "register", "row_major", "sample",
            "sampler", "shared", "snorm", "stateblock", "stateblock_state", "tbuffer",
            "technique", "typedef", "triangle", "triangleadj", "uniform", "unorm",
            "unsigned", "vector", "vertexfragment", "zero", "float1", "double1",
            "int1", "uint1", "bool1", "fragmentKeyword", "compile_fragment", "shaderProfile",
            "maxvertexcount", "TriangleStream", "Buffer", "ByteAddressBuffer", "ConsumeStructuredBuffer",
            "InputPatch", "OutputPatch", "RWBuffer", "RWByteAddressBuffer", "RWStructuredBuffer",
            "RWTexture1D", "RWTexture1DArray", "RWTexture2D", "RWTexture2DArray", "RWTexture3D",
            "StructuredBuffer", "Texture1D", "Texture1DArray", "Texture2D", "Texture2DArray",
            "Texture3D", "Texture2DMS", "Texture2DMSArray", "TextureCube", "TextureCubeArray",
            "SV_DispatchThreadID", "SV_DomainLocation", "SV_GroupID", "SV_GroupIndex", "SV_GroupThreadID",
            "SV_GSInstanceID", "SV_InsideTessFactor", "SV_OutputControlPointID", "SV_TessFactor",
            "SV_InnerCoverage", "SV_StencilRef", "globallycoherent"

        ]);

        // HLSL primitive names
        foreach (Type? type in HlslKnownTypes.EnumerateKnownVectorTypes().Concat(HlslKnownTypes.EnumerateKnownMatrixTypes()))
        {
            _ = knownKeywords.Add(type.Name.ToLowerInvariant());
        }

        // HLSL intrinsics method names
        foreach (MethodInfo? method in typeof(Hlsl).GetMethods(BindingFlags.Public | BindingFlags.Static))
        {
            string name = method.GetCustomAttribute<HlslIntrinsicNameAttribute>()?.Name ?? method.Name;

            _ = knownKeywords.Add(name);
        }

        // Let other types inject additional keywords
        AddKnownKeywords(knownKeywords);

        return knownKeywords;
    }

    /// <summary>
    /// Adds more known keywords to the collection to use.
    /// </summary>
    /// <param name="knownKeywords">The collection of known keywords being built.</param>
    static partial void AddKnownKeywords(ICollection<string> knownKeywords);

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