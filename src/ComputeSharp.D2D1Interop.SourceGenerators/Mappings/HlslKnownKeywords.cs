using System.Collections.Generic;
using System.Reflection;
using ComputeSharp.Core.Intrinsics.Attributes;
using ComputeSharp.D2D1Interop;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownKeywords
{
    /// <inheritdoc/>
    static partial void AddKnownKeywords(ICollection<string> knownKeywords)
    {
        // D2D1 intrinsics method names
        foreach (var method in typeof(D2D1).GetMethods(BindingFlags.Public | BindingFlags.Static))
        {
            string name = method.GetCustomAttribute<HlslIntrinsicNameAttribute>()?.Name ?? method.Name;

            knownKeywords.Add(name);
        }
    }
}
