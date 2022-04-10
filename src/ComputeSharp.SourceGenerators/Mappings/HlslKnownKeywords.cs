using System.Collections.Generic;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <inheritdoc/>
internal static partial class HlslKnownKeywords
{
    /// <inheritdoc/>
    static partial void AddKnownKeyword(ICollection<string> knownKeywords)
    {
        // Dispatch type names
        knownKeywords.Add(nameof(ThreadIds));
        knownKeywords.Add(nameof(GroupIds));
        knownKeywords.Add(nameof(GroupSize));
        knownKeywords.Add(nameof(GridIds));
    }
}
