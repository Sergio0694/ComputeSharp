using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownProperties
{
    /// <inheritdoc/>
    private static partial IReadOnlyDictionary<string, string> BuildKnownResourceIndexers()
    {
        return new Dictionary<string, string>
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
    }

    /// <inheritdoc/>
    private static partial IReadOnlyDictionary<string, string?> BuildKnownResourceSamplers()
    {
        return new Dictionary<string, string?>
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
    }

    /// <inheritdoc/>
    private static partial IReadOnlyDictionary<string, (int Rank, int Axis)> BuildKnownSizeAccessors()
    {
        return new Dictionary<string, (int, int)>
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
    }

    /// <inheritdoc/>
    static partial void AddKnownProperties(IDictionary<string, string> knownProperties)
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
                knownProperties.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"__{nameof(GroupIds)}__get_Index");
            }
            else
            {
                knownProperties.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $"{item.Type.Name}.{item.Property.Name.ToLowerInvariant()}");
            }
        }

        // Programmatically load mappings for the normalized thread ids
        foreach (var property in typeof(ThreadIds.Normalized).GetProperties(BindingFlags.Static | BindingFlags.Public))
        {
            string key = $"{typeof(ThreadIds).FullName}{Type.Delimiter}{typeof(ThreadIds.Normalized).Name}{Type.Delimiter}{property.Name}";

            switch (property.Name)
            {
                case string name when name.Length == 1:
                    knownProperties.Add(key, $"{typeof(ThreadIds).Name}.{char.ToLowerInvariant(name[0])} / (float)__{char.ToLowerInvariant(name[0])}");
                    break;
                case string name when name.Length == 2:
                {
                    string numerator = $"float2({typeof(ThreadIds).Name}.{char.ToLowerInvariant(name[0])}, {typeof(ThreadIds).Name}.{char.ToLowerInvariant(name[1])})";
                    string denominator = $"float2(__{char.ToLowerInvariant(name[0])}, __{char.ToLowerInvariant(name[1])})";

                    knownProperties.Add(key, $"{numerator} / {denominator}");
                    break;
                }
                case string name when name.Length == 3:
                {
                    string numerator = $"float3({typeof(ThreadIds).Name}.{char.ToLowerInvariant(name[0])}, {typeof(ThreadIds).Name}.{char.ToLowerInvariant(name[1])}, {typeof(ThreadIds).Name}.{char.ToLowerInvariant(name[2])})";
                    string denominator = $"float3(__{char.ToLowerInvariant(name[0])}, __{char.ToLowerInvariant(name[1])}, __{char.ToLowerInvariant(name[2])})";

                    knownProperties.Add(key, $"{numerator} / {denominator}");
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
                    knownProperties.Add(key, "__GroupSize__get_X * __GroupSize__get_Y * __GroupSize__get_Z");
                    break;
                case string name when name.Length == 1:
                    knownProperties.Add(key, $"__GroupSize__get_{name}");
                    break;
                case string name when name.Length == 2:
                    knownProperties.Add(key, $"int2(__GroupSize__get_{name[0]}, __GroupSize__get_{name[1]})");
                    break;
                case string name when name.Length == 3:
                    knownProperties.Add(key, $"int3(__GroupSize__get_{name[0]}, __GroupSize__get_{name[1]}, __GroupSize__get_{name[2]})");
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
                    knownProperties.Add(key, "__x * __y * __z");
                    break;
                case string name when name.Length == 1:
                    knownProperties.Add(key, $"__{char.ToLowerInvariant(name[0])}");
                    break;
                case string name when name.Length == 2:
                    knownProperties.Add(key, $"int2(__{char.ToLowerInvariant(name[0])}, __{char.ToLowerInvariant(name[1])})");
                    break;
                case string name when name.Length == 3:
                    knownProperties.Add(key, $"int3(__{char.ToLowerInvariant(name[0])}, __{char.ToLowerInvariant(name[1])}, __{char.ToLowerInvariant(name[2])})");
                    break;
            }
        }
    }
}
