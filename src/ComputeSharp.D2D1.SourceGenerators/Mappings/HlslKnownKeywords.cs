using System.Collections.Generic;
using System.Reflection;
using ComputeSharp.Core.Intrinsics;
using ComputeSharp.D2D1;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownKeywords
{
    /// <inheritdoc/>
    static partial void AddKnownKeywords(ICollection<string> knownKeywords)
    {
        // D2D1 intrinsics method names
        foreach (MethodInfo? method in typeof(D2D).GetMethods(BindingFlags.Public | BindingFlags.Static))
        {
            string name = method.GetCustomAttribute<HlslIntrinsicNameAttribute>()?.Name ?? method.Name;

            knownKeywords.Add(name);
        }
    }
}