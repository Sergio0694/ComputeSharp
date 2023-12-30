using System.Collections.Generic;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownMethods
{
    /// <inheritdoc/>
    private static partial Dictionary<string, string?> BuildKnownResourceSamplers()
    {
        return new()
        {
            [$"ComputeSharp.ReadOnlyTexture1D`2.Sample({typeof(float).FullName})"] = null,
            [$"ComputeSharp.IReadOnlyTexture1D`1.Sample({typeof(float).FullName})"] = null,
            [$"ComputeSharp.IReadOnlyNormalizedTexture1D`1.Sample({typeof(float).FullName})"] = null,
            [$"ComputeSharp.ReadOnlyTexture2D`2.Sample({typeof(float).FullName}, {typeof(float).FullName})"] = "float2",
            [$"ComputeSharp.ReadOnlyTexture2D`2.Sample({typeof(Float2).FullName})"] = null,
            [$"ComputeSharp.IReadOnlyTexture2D`1.Sample({typeof(float).FullName}, {typeof(float).FullName})"] = "float2",
            [$"ComputeSharp.IReadOnlyTexture2D`1.Sample({typeof(Float2).FullName})"] = null,
            [$"ComputeSharp.IReadOnlyNormalizedTexture2D`1.Sample({typeof(float).FullName}, {typeof(float).FullName})"] = "float2",
            [$"ComputeSharp.IReadOnlyNormalizedTexture2D`1.Sample({typeof(Float2).FullName})"] = null,
            [$"ComputeSharp.ReadOnlyTexture3D`2.Sample({typeof(float).FullName}, {typeof(float).FullName}, {typeof(float).FullName})"] = "float3",
            [$"ComputeSharp.ReadOnlyTexture3D`2.Sample({typeof(Float3).FullName})"] = null,
            [$"ComputeSharp.IReadOnlyTexture3D`1.Sample({typeof(float).FullName}, {typeof(float).FullName}, {typeof(float).FullName})"] = "float3",
            [$"ComputeSharp.IReadOnlyTexture3D`1.Sample({typeof(Float3).FullName})"] = null,
            [$"ComputeSharp.IReadOnlyNormalizedTexture3D`1.Sample({typeof(float).FullName}, {typeof(float).FullName}, {typeof(float).FullName})"] = "float3",
            [$"ComputeSharp.IReadOnlyNormalizedTexture3D`1.Sample({typeof(Float3).FullName})"] = null
        };
    }
}