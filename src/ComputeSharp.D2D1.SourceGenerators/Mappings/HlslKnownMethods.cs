using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Intrinsics.Attributes;
using ComputeSharp.D2D1;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
partial class HlslKnownMethods
{
    /// <summary>
    /// Checks whether or not a method name (previous matched with <see cref="TryGetMappedName(string, out string?)"/>
    /// needs the <c>[D2DRequiresPosition]</c> annotation on its containing shader in order to be used.
    /// </summary>
    /// <param name="name">The fully qualified metadata name.</param>
    /// <returns>Whether the method needs the <c>[D2DRequiresPosition]</c> annotation.</returns>
    public static bool NeedsD2DRequiresPositionAttribute(string name)
    {
        return name == "ComputeSharp.D2D1.D2D.GetScenePosition";
    }

    /// <inheritdoc/>

    static partial void AddKnownMethods(IDictionary<string, string> knownMethods)
    {
        // Programmatically load mappings from the D2D1 class as well
        foreach (var method in
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
