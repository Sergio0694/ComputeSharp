using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownProperties
{
    /// <inheritdoc/>
    private static partial Dictionary<string, string> BuildKnownResourceIndexers()
    {
        return new()
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
    private static partial Dictionary<string, (int Rank, int Axis)> BuildKnownSizeAccessors()
    {
        return new()
        {
            ["ComputeSharp.Resources.Buffer`1.Length"] = (2, 0),
            ["ComputeSharp.Resources.Texture1D`1.Width"] = (1, 0),
            ["ComputeSharp.Resources.Texture2D`1.Width"] = (2, 0),
            ["ComputeSharp.Resources.Texture2D`1.Height"] = (2, 1),
            ["ComputeSharp.Resources.Texture3D`1.Width"] = (3, 0),
            ["ComputeSharp.Resources.Texture3D`1.Height"] = (3, 1),
            ["ComputeSharp.Resources.Texture3D`1.Depth"] = (3, 2),
            ["ComputeSharp.IReadOnlyTexture1D`1.Width"] = (1, 0),
            ["ComputeSharp.IReadOnlyTexture2D`1.Width"] = (2, 0),
            ["ComputeSharp.IReadOnlyTexture2D`1.Height"] = (2, 1),
            ["ComputeSharp.IReadOnlyTexture3D`1.Width"] = (3, 0),
            ["ComputeSharp.IReadOnlyTexture3D`1.Height"] = (3, 1),
            ["ComputeSharp.IReadOnlyTexture3D`1.Depth"] = (3, 2),
            ["ComputeSharp.IReadOnlyNormalizedTexture1D`1.Width"] = (1, 0),
            ["ComputeSharp.IReadOnlyNormalizedTexture2D`1.Width"] = (2, 0),
            ["ComputeSharp.IReadOnlyNormalizedTexture2D`1.Height"] = (2, 1),
            ["ComputeSharp.IReadOnlyNormalizedTexture3D`1.Width"] = (3, 0),
            ["ComputeSharp.IReadOnlyNormalizedTexture3D`1.Height"] = (3, 1),
            ["ComputeSharp.IReadOnlyNormalizedTexture3D`1.Depth"] = (3, 2),
            ["ComputeSharp.IReadWriteNormalizedTexture1D`1.Width"] = (1, 0),
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
        foreach ((Type Type, PropertyInfo Property) item in
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
        foreach (PropertyInfo? property in typeof(ThreadIds.Normalized).GetProperties(BindingFlags.Static | BindingFlags.Public))
        {
            string key = $"{typeof(ThreadIds).FullName}{Type.Delimiter}{nameof(ThreadIds.Normalized)}{Type.Delimiter}{property.Name}";

            // The normalized value must be in the [0, 1] range, inclusive. As such, the expression is rewritten to be:
            //   1D: (float)ThreadIds.<XYZ> / (float)(max(1, __<xyz> - 1));
            //   2D: float2(ThreadIds.<XYZ>, ThreadIds.<XYZ>) / float2(max(1, int2(__<xyz>, __<xyz>) -1));
            //   3D: float3(ThreadIds.<XYZ>, ThreadIds.<XYZ>, ThreadIds.<XYZ>) / float3(max(1, int3(__<xyz>, __<xyz>, __<xyz>) - 1));
            //
            // All these expressions will result in the values going in the target [0, 1] range, with the values at the two ends being
            // exactly 0 and 1. This logic assumes that the __<xyz> values are always > 0, which is guaranteed by the fact those are the
            // dispatching upper bounds, which is validated whenever a shader is run (as there can never be 0 iterations on any given axis).
            switch (property.Name)
            {
                case string name when name.Length == 1:
                    knownProperties.Add(key, $"(float){nameof(ThreadIds)}.{char.ToLowerInvariant(name[0])} / (float)(max(1, __{char.ToLowerInvariant(name[0])} - 1))");
                    break;
                case string name when name.Length == 2:
                {
                    string numerator = $"float2({nameof(ThreadIds)}.{char.ToLowerInvariant(name[0])}, {nameof(ThreadIds)}.{char.ToLowerInvariant(name[1])})";
                    string denominator = $"float2(max(1, int2(__{char.ToLowerInvariant(name[0])}, __{char.ToLowerInvariant(name[1])}) - 1))";

                    knownProperties.Add(key, $"{numerator} / {denominator}");
                    break;
                }
                case string name when name.Length == 3:
                {
                    string numerator = $"float3({nameof(ThreadIds)}.{char.ToLowerInvariant(name[0])}, {nameof(ThreadIds)}.{char.ToLowerInvariant(name[1])}, {nameof(ThreadIds)}.{char.ToLowerInvariant(name[2])})";
                    string denominator = $"float3(max(1, int3(__{char.ToLowerInvariant(name[0])}, __{char.ToLowerInvariant(name[1])}, __{char.ToLowerInvariant(name[2])}) - 1))";

                    knownProperties.Add(key, $"{numerator} / {denominator}");
                    break;
                }
            }
        }

        // Programmatically load mappings for the group size
        foreach (PropertyInfo? property in typeof(GroupSize).GetProperties(BindingFlags.Static | BindingFlags.Public))
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
        foreach (PropertyInfo? property in typeof(DispatchSize).GetProperties(BindingFlags.Static | BindingFlags.Public))
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