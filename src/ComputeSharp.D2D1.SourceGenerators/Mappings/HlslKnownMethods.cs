using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Intrinsics;
using ComputeSharp.D2D1;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownMethods
{
    /// <summary>
    /// Checks whether or not a method name (previous matched with <see cref="TryGetMappedName(string, out string?)"/>
    /// needs the <c>[D2DRequiresScenePosition]</c> annotation on its containing shader in order to be used.
    /// </summary>
    /// <param name="name">The fully qualified metadata name.</param>
    /// <returns>Whether the method needs the <c>[D2DRequiresScenePosition]</c> annotation.</returns>
    public static bool NeedsD2DRequiresScenePositionAttribute(string name)
    {
        return name is "ComputeSharp.D2D1.D2D.GetScenePosition" or "ComputeSharp.D2D1.D2D.SampleInputAtPosition";
    }

    /// <inheritdoc/>
    private static partial Dictionary<string, string?> BuildKnownResourceSamplers()
    {
        return new()
        {
            [$"ComputeSharp.D2D1.D2D1ResourceTexture1D`1.Sample({typeof(float).FullName})"] = null,
            [$"ComputeSharp.D2D1.D2D1ResourceTexture2D`1.Sample({typeof(float).FullName}, {typeof(float).FullName})"] = "float2",
            [$"ComputeSharp.D2D1.D2D1ResourceTexture2D`1.Sample({typeof(Float2).FullName})"] = null,
            [$"ComputeSharp.D2D1.D2D1ResourceTexture3D`1.Sample({typeof(float).FullName}, {typeof(float).FullName}, {typeof(float).FullName})"] = "float3",
            [$"ComputeSharp.D2D1.D2D1ResourceTexture3D`1.Sample({typeof(Float3).FullName})"] = null
        };
    }

    /// <inheritdoc/>

    static partial void AddKnownMethods(IDictionary<string, string> knownMethods)
    {
        // Programmatically load mappings from the D2D1 class as well
        foreach (MethodInfo? method in
            from method in typeof(D2D).GetMethods(BindingFlags.Public | BindingFlags.Static)
            group method by method.Name
            into groups
            select groups.First())
        {
            string hlslName = method.GetCustomAttribute<HlslIntrinsicNameAttribute>()?.Name ?? method.Name;

            knownMethods.Add($"{typeof(D2D).FullName}{Type.Delimiter}{method.Name}", hlslName);
        }
    }
}