using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Intrinsics.Attributes;
using ComputeSharp.D2D1Interop;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownMethods
{
    /// <inheritdoc/>

    static partial void AddKnownMethods(IDictionary<string, string> knownMethods)
    {
        // Programmatically load mappings from the D2D1 class as well
        foreach (var method in
            from method in typeof(D2D1).GetMethods(BindingFlags.Public | BindingFlags.Static)
            group method by method.Name
            into groups
            select groups.First())
        {
            string hlslName = method.GetCustomAttribute<HlslIntrinsicNameAttribute>()?.Name ?? method.Name;

            knownMethods.Add($"{typeof(D2D1).FullName}{Type.Delimiter}{method.Name}", hlslName);
        }
    }
}
