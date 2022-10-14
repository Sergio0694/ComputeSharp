using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;

namespace ComputeSharp.D2D1.SourceGenerators.Models;

/// <summary>
/// A model representing all necessary info for a full generation pass for a D2D1 shader.
/// </summary>
/// <param name="Hierarchy">The hierarchy info for the shader type.</param>
/// <param name="DispatchData">The gathered shader dispatch data.</param>
/// <param name="InputTypes">The gathered input types for the shader.</param>
/// <param name="ResourceTextureDescriptions">The gathered resource texture descriptions for the shader.</param>
/// <param name="HlslShaderSource">The processed HLSL source for the shader.</param>
/// <param name="OutputBuffer">The output buffer info for the shader.</param>
/// <param name="InputDescriptions">The gathered input descriptions for the shader.</param>
/// <param name="PixelOptions">The pixel options used by the shader.</param>
/// <param name="Diagnostcs">The discovered diagnostics, if any.</param>
internal sealed record D2D1ShaderInfo(
    HierarchyInfo Hierarchy,
    DispatchDataInfo DispatchData,
    InputTypesInfo InputTypes,
    ResourceTextureDescriptionsInfo ResourceTextureDescriptions,
    HlslShaderSourceInfo HlslShaderSource,
    OutputBufferInfo OutputBuffer,
    InputDescriptionsInfo InputDescriptions,
    D2D1PixelOptions PixelOptions,
    ImmutableArray<DiagnosticInfo> Diagnostcs)
{
    /// <inheritdoc/>
    public bool Equals(D2D1ShaderInfo? obj) => Comparer.Default.Equals(this, obj);

    /// <inheritdoc/>
    public override int GetHashCode() => Comparer.Default.GetHashCode(this);

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> implementation for <see cref="D2D1ShaderInfo"/>.
    /// </summary>
    private sealed class Comparer : Comparer<D2D1ShaderInfo, Comparer>
    {
        /// <inheritdoc/>
        protected override void AddToHashCode(ref HashCode hashCode, D2D1ShaderInfo obj)
        {
            hashCode.Add(obj.Hierarchy);
            hashCode.Add(obj.DispatchData);
            hashCode.Add(obj.InputTypes);
            hashCode.Add(obj.ResourceTextureDescriptions);
            hashCode.Add(obj.HlslShaderSource);
            hashCode.Add(obj.OutputBuffer);
            hashCode.Add(obj.InputDescriptions);
            hashCode.Add(obj.PixelOptions);
            hashCode.AddRange(obj.Diagnostcs);
        }

        /// <inheritdoc/>
        protected override bool AreEqual(D2D1ShaderInfo x, D2D1ShaderInfo y)
        {
            return
                x.Hierarchy == y.Hierarchy &&
                x.DispatchData == y.DispatchData &&
                x.InputTypes == y.InputTypes &&
                x.ResourceTextureDescriptions == y.ResourceTextureDescriptions &&
                x.HlslShaderSource == y.HlslShaderSource &&
                x.OutputBuffer == y.OutputBuffer &&
                x.InputDescriptions == y.InputDescriptions &&
                x.PixelOptions == y.PixelOptions &&
                x.Diagnostcs.SequenceEqual(y.Diagnostcs);
        }
    }
}