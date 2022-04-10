using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownMembers
{
    /// <summary>
    /// The mapping of supported known indexers to HLSL resource type names.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, string> KnownResourceIndexers = new Dictionary<string, string>
    {
        [$"ComputeSharp.ReadOnlyTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
        [$"ComputeSharp.ReadOnlyTexture2D`2.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
        [$"ComputeSharp.ReadWriteTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
        [$"ComputeSharp.ReadWriteTexture2D`2.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
        [$"ComputeSharp.IReadOnlyTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
        [$"ComputeSharp.IReadWriteTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
        [$"ComputeSharp.IReadOnlyNormalizedTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
        [$"ComputeSharp.IReadWriteNormalizedTexture2D`1.this[{typeof(int).FullName}, {typeof(int).FullName}]"] = "int2",
        [$"ComputeSharp.ReadOnlyTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
        [$"ComputeSharp.ReadOnlyTexture3D`2.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
        [$"ComputeSharp.ReadWriteTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
        [$"ComputeSharp.ReadWriteTexture3D`2.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
        [$"ComputeSharp.IReadOnlyTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
        [$"ComputeSharp.IReadWriteTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
        [$"ComputeSharp.IReadOnlyNormalizedTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3",
        [$"ComputeSharp.IReadWriteNormalizedTexture3D`1.this[{typeof(int).FullName}, {typeof(int).FullName}, {typeof(int).FullName}]"] = "int3"
    };

    /// <summary>
    /// The mapping of supported known samplers to HLSL resource type names.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, string?> KnownResourceSamplers = new Dictionary<string, string?>
    {
        [$"ComputeSharp.ReadOnlyTexture2D`2.this[{typeof(float).FullName}, {typeof(float).FullName}]"] = "float2",
        [$"ComputeSharp.ReadOnlyTexture2D`2.this[{typeof(Float2).FullName}]"] = null,
        [$"ComputeSharp.IReadOnlyTexture2D`1.this[{typeof(float).FullName}, {typeof(float).FullName}]"] = "float2",
        [$"ComputeSharp.IReadOnlyTexture2D`1.this[{typeof(Float2).FullName}]"] = null,
        [$"ComputeSharp.IReadOnlyNormalizedTexture2D`1.this[{typeof(float).FullName}, {typeof(float).FullName}]"] = "float2",
        [$"ComputeSharp.IReadOnlyNormalizedTexture2D`1.this[{typeof(Float2).FullName}]"] = null,
        [$"ComputeSharp.ReadOnlyTexture3D`2.this[{typeof(float).FullName}, {typeof(float).FullName}, {typeof(float).FullName}]"] = "float3",
        [$"ComputeSharp.ReadOnlyTexture3D`2.this[{typeof(Float3).FullName}]"] = null,
        [$"ComputeSharp.IReadOnlyTexture3D`1.this[{typeof(float).FullName}, {typeof(float).FullName}, {typeof(float).FullName}]"] = "float3",
        [$"ComputeSharp.IReadOnlyTexture3D`1.this[{typeof(Float3).FullName}]"] = null,
        [$"ComputeSharp.IReadOnlyNormalizedTexture3D`1.this[{typeof(float).FullName}, {typeof(float).FullName}, {typeof(float).FullName}]"] = "float3",
        [$"ComputeSharp.IReadOnlyNormalizedTexture3D`1.this[{typeof(Float3).FullName}]"] = null
    };

    /// <summary>
    /// The mapping of supported known size accessors for HLSL resource types.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, (int Rank, int Axis)> KnownSizeAccessors = new Dictionary<string, (int, int)>
    {
        ["ComputeSharp.Resources.Buffer`1.Length"] = (2, 0),
        ["ComputeSharp.Resources.Texture2D`1.Width"] = (2, 0),
        ["ComputeSharp.Resources.Texture2D`1.Height"] = (2, 1),
        ["ComputeSharp.Resources.Texture3D`1.Width"] = (3, 0),
        ["ComputeSharp.Resources.Texture3D`1.Height"] = (3, 1),
        ["ComputeSharp.Resources.Texture3D`1.Depth"] = (3, 2),
        ["ComputeSharp.IReadOnlyTexture2D`1.Width"] = (2, 0),
        ["ComputeSharp.IReadOnlyTexture2D`1.Height"] = (2, 1),
        ["ComputeSharp.IReadOnlyTexture3D`1.Width"] = (3, 0),
        ["ComputeSharp.IReadOnlyTexture3D`1.Height"] = (3, 1),
        ["ComputeSharp.IReadOnlyTexture3D`1.Depth"] = (3, 2),
        ["ComputeSharp.IReadOnlyNormalizedTexture2D`1.Width"] = (2, 0),
        ["ComputeSharp.IReadOnlyNormalizedTexture2D`1.Height"] = (2, 1),
        ["ComputeSharp.IReadOnlyNormalizedTexture3D`1.Width"] = (3, 0),
        ["ComputeSharp.IReadOnlyNormalizedTexture3D`1.Height"] = (3, 1),
        ["ComputeSharp.IReadOnlyNormalizedTexture3D`1.Depth"] = (3, 2),
        ["ComputeSharp.IReadWriteNormalizedTexture2D`1.Width"] = (2, 0),
        ["ComputeSharp.IReadWriteNormalizedTexture2D`1.Height"] = (2, 1),
        ["ComputeSharp.IReadWriteNormalizedTexture3D`1.Width"] = (3, 0),
        ["ComputeSharp.IReadWriteNormalizedTexture3D`1.Height"] = (3, 1),
        ["ComputeSharp.IReadWriteNormalizedTexture3D`1.Depth"] = (3, 2)
    };

    /// <inheritdoc/>
    static partial void AddKnownMembers(IDictionary<string, string> knownMembers)
    {
        // Store GroupIds.Index for a quicker comparison afterwards
        PropertyInfo groupindexProperty = typeof(GroupIds).GetProperty(nameof(GroupIds.Index), BindingFlags.Static | BindingFlags.Public);

        // Programmatically load mappings for the dispatch types
        foreach (var item in
            from type in HlslKnownTypes.HlslDispatchTypes
            from property in type.GetProperties(BindingFlags.Static | BindingFlags.Public)
            select (Type: type, Property: property))
        {
            if (item.Property == groupindexProperty)
            {
                // The thread group index is a standalone parameter, so if this property is used, we
                // just map the access directly to that implicit and hidden parameter name instead.
                knownMembers.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"__{nameof(GroupIds)}__get_Index");
            }
            else
            {
                knownMembers.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"{item.Type.Name}.{item.Property.Name.ToLower()}");
            }
        }

        // Programmatically load mappings for the normalized thread ids
        foreach (var property in typeof(ThreadIds.Normalized).GetProperties(BindingFlags.Static | BindingFlags.Public))
        {
            string key = $"{typeof(ThreadIds).FullName}{Type.Delimiter}{typeof(ThreadIds.Normalized).Name}{Type.Delimiter}{property.Name}";

            switch (property.Name)
            {
                case string name when name.Length == 1:
                    knownMembers.Add(key, $"{typeof(ThreadIds).Name}.{char.ToLower(name[0])} / (float)__{char.ToLower(name[0])}");
                    break;
                case string name when name.Length == 2:
                {
                    string numerator = $"float2({typeof(ThreadIds).Name}.{char.ToLower(name[0])}, {typeof(ThreadIds).Name}.{char.ToLower(name[1])})";
                    string denominator = $"float2(__{char.ToLower(name[0])}, __{char.ToLower(name[1])})";

                    knownMembers.Add(key, $"{numerator} / {denominator}");
                    break;
                }
                case string name when name.Length == 3:
                {
                    string numerator = $"float3({typeof(ThreadIds).Name}.{char.ToLower(name[0])}, {typeof(ThreadIds).Name}.{char.ToLower(name[1])}, {typeof(ThreadIds).Name}.{char.ToLower(name[2])})";
                    string denominator = $"float3(__{char.ToLower(name[0])}, __{char.ToLower(name[1])}, __{char.ToLower(name[2])})";

                    knownMembers.Add(key, $"{numerator} / {denominator}");
                    break;
                }
            }
        }

        // Programmatically load mappings for the group size
        foreach (var property in typeof(GroupSize).GetProperties(BindingFlags.Static | BindingFlags.Public))
        {
            string key = $"{typeof(GroupSize).FullName}{Type.Delimiter}{property.Name}";

            switch (property.Name)
            {
                case nameof(GroupSize.Count):
                    knownMembers.Add(key, "__GroupSize__get_X * __GroupSize__get_Y * __GroupSize__get_Z");
                    break;
                case string name when name.Length == 1:
                    knownMembers.Add(key, $"__GroupSize__get_{name}");
                    break;
                case string name when name.Length == 2:
                    knownMembers.Add(key, $"int2(__GroupSize__get_{name[0]}, __GroupSize__get_{name[1]})");
                    break;
                case string name when name.Length == 3:
                    knownMembers.Add(key, $"int3(__GroupSize__get_{name[0]}, __GroupSize__get_{name[1]}, __GroupSize__get_{name[2]})");
                    break;
            }
        }

        // Programmatically load mappings for the dispatch size
        foreach (var property in typeof(DispatchSize).GetProperties(BindingFlags.Static | BindingFlags.Public))
        {
            string key = $"{typeof(DispatchSize).FullName}{Type.Delimiter}{property.Name}";

            switch (property.Name)
            {
                case nameof(DispatchSize.Count):
                    knownMembers.Add(key, "__x * __y * __z");
                    break;
                case string name when name.Length == 1:
                    knownMembers.Add(key, $"__{char.ToLower(name[0])}");
                    break;
                case string name when name.Length == 2:
                    knownMembers.Add(key, $"int2(__{char.ToLower(name[0])}, __{char.ToLower(name[1])})");
                    break;
                case string name when name.Length == 3:
                    knownMembers.Add(key, $"int3(__{char.ToLower(name[0])}, __{char.ToLower(name[1])}, __{char.ToLower(name[2])})");
                    break;
            }
        }
    }

    /// <summary>
    /// Tries to get the mapped HLSL-compatible indexer resource type name for the input indexer name.
    /// </summary>
    /// <param name="name">The input fully qualified indexer name.</param>
    /// <param name="mapped">The mapped type name, if one is found.</param>
    /// <returns>The HLSL-compatible type name that can be used in an HLSL shader for the given indexer.</returns>
    public static bool TryGetMappedResourceIndexerTypeName(string name, out string? mapped)
    {
        return KnownResourceIndexers.TryGetValue(name, out mapped);
    }

    /// <summary>
    /// Tries to get the mapped HLSL-compatible sampler resource type name for the input indexer name.
    /// </summary>
    /// <param name="name">The input fully qualified indexer name.</param>
    /// <param name="mapped">The mapped type name, if one is found.</param>
    /// <returns>The HLSL-compatible type name that can be used in an HLSL shader for the given sampler.</returns>
    public static bool TryGetMappedResourceSamplerAccessType(string name, out string? mapped)
    {
        return KnownResourceSamplers.TryGetValue(name, out mapped);
    }

    /// <summary>
    /// Tries to get the mapped rank and axis for a given indexer name.
    /// </summary>
    /// <param name="name">The input fully qualified indexer name.</param>
    /// <param name="rank">The resulting indexer rank, if found.</param>
    /// <param name="axis">The resulting indexer axis, if found.</param>
    /// <returns>Whether or not a rank and axis could be resolved by the input indexer name.</returns>
    public static bool TryGetAccessorRankAndAxis(string name, out int rank, out int axis)
    {
        if (KnownSizeAccessors.TryGetValue(name, out var info))
        {
            (rank, axis) = info;

            return true;
        }

        (rank, axis) = default((int, int));

        return false;
    }
}
