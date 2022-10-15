using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a shader.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="DispatchId">The gathered dispatch id for the shader type.</param>
/// <param name="DispatchData">The gathered shader dispatch data.</param>
/// <param name="DispatchMetadata">The gathered shader dispatch metadata.</param>
/// <param name="HlslShaderSource">The processed HLSL source for the shader.</param>
/// <param name="ThreadIds">The thread ids for the shader, if compilation is requested.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record ShaderInfo(
    HierarchyInfo Hierarchy,
    DispatchIdInfo DispatchId,
    DispatchDataInfo DispatchData,
    DispatchMetadataInfo DispatchMetadata,
    HlslShaderSourceInfo HlslShaderSource,
    ThreadIdsInfo ThreadIds,
    ImmutableArray<DiagnosticInfo> Diagnostcs)
{
    /// <inheritdoc/>
    public bool Equals(ShaderInfo? obj)
    {
        return Comparer.Default.Equals(this, obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Comparer.Default.GetHashCode(this);
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="ShaderInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<ShaderInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, ShaderInfo obj)
        {
            hashCode.Add(obj.Hierarchy);
            hashCode.Add(obj.DispatchId);
            hashCode.Add(obj.DispatchData);
            hashCode.Add(obj.DispatchMetadata);
            hashCode.Add(obj.HlslShaderSource);
            hashCode.Add(obj.ThreadIds);
            hashCode.AddRange(obj.Diagnostcs);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(ShaderInfo x, ShaderInfo y)
        {
            return
                x.Hierarchy == y.Hierarchy &&
                x.DispatchId == y.DispatchId &&
                x.DispatchData == y.DispatchData &&
                x.DispatchMetadata == y.DispatchMetadata &&
                x.HlslShaderSource == y.HlslShaderSource &&
                x.ThreadIds == y.ThreadIds &&
                x.Diagnostcs.SequenceEqual(y.Diagnostcs);
        }
    }
}