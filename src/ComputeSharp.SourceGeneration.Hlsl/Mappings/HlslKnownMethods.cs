using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Intrinsics;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <summary>
/// A <see langword="class"/> that contains and maps known HLSL method names to common .NET methods.
/// </summary>
internal static partial class HlslKnownMethods
{
    /// <summary>
    /// The mapping of supported known methods to HLSL names.
    /// </summary>
    private static readonly Dictionary<string, string> KnownMethods = BuildKnownMethodsMap();

    /// <summary>
    /// The mapping of supported known methods that require parameters mapping to HLSL names.
    /// </summary>
    private static readonly Dictionary<string, string> KnownMethodsParametersMapping = BuildKnownMethodsParametersMappingMap();

    /// <summary>
    /// The mapping of supported known samplers to HLSL resource type names.
    /// </summary>
    private static readonly Dictionary<string, string?> KnownResourceSamplers = BuildKnownResourceSamplers();

    /// <summary>
    /// Builds the mapping of supported known methods to HLSL names.
    /// </summary>
    private static Dictionary<string, string> BuildKnownMethodsMap()
    {
        Dictionary<string, string> knownMethods = [];

        // Programmatically load mappings from the Hlsl class as well (the ones with no parameter matching)
        foreach (MethodInfo? method in
            from method in typeof(Hlsl).GetMethods(BindingFlags.Public | BindingFlags.Static)
            group method by method.Name
            into groups
            select groups.First())
        {
            HlslIntrinsicNameAttribute? attribute = method.GetCustomAttribute<HlslIntrinsicNameAttribute>();

            // If the intrinsic requires parameters matching, skip it. It will be tracked separately.
            // To avoid processing all types again, add a special identifier for it to detect it.
            if (attribute is { RequiresParametersMatching: true })
            {
                knownMethods.Add($"{typeof(Hlsl).FullName}{Type.Delimiter}{method.Name}", nameof(HlslIntrinsicNameAttribute.RequiresParametersMatching));

                continue;
            }
            else
            {
                // Check whether the current method should be translated with the original name
                // or with the lowercase version. This is needed because all C# methods are exposed
                // with the upper camel case format, while HLSL intrinsics use multiple different formats.
                string hlslName = attribute?.Name ?? method.Name;

                knownMethods.Add($"{typeof(Hlsl).FullName}{Type.Delimiter}{method.Name}", hlslName);
            }
        }

        // Let other types inject additional methods
        AddKnownMethods(knownMethods);

        return knownMethods;
    }

    /// <summary>
    /// Builds the mapping of supported known methods that require parameters mapping to HLSL names.
    /// </summary>
    private static Dictionary<string, string> BuildKnownMethodsParametersMappingMap()
    {
        ILookup<string, MethodInfo> methodsLookup = typeof(Hlsl).GetMethods(BindingFlags.Public | BindingFlags.Static).ToLookup(static method => method.Name);

        Dictionary<string, string> knownMethods = [];

        // Go through the previously discovered methods to find the ones requiring parameters mapping
        foreach (KeyValuePair<string, string> pair in KnownMethods)
        {
            // Only process methods marked as requiring parameters mapping
            if (pair.Value != nameof(HlslIntrinsicNameAttribute.RequiresParametersMatching))
            {
                continue;
            }

            // Go through the intrinsics with the current name, and track the method name and parameters. We don't need
            // to also track the fully qualified type name, as that part has already been matched previously at this point.
            foreach (MethodInfo method in methodsLookup[pair.Key.Split('.').Last()])
            {
                // The key is in the format "<METHOD_NAME>(<PARAMETER_TYPES>)", and the intrinsic name is guaranteed to be available
                knownMethods.Add(
                    $"{method.Name}({string.Join(", ", method.GetParameters().Select(static p => p.ParameterType.Name))})",
                    method.GetCustomAttribute<HlslIntrinsicNameAttribute>()!.Name);
            }
        }

        return knownMethods;
    }

    /// <summary>
    /// Builds the mapping of supported known samplers to HLSL resource type names.
    /// </summary>
    /// <returns>The mapping of supported known samplers to HLSL resource type names.</returns>
    private static partial Dictionary<string, string?> BuildKnownResourceSamplers();

    /// <summary>
    /// Adds more known methods to the mapping to use.
    /// </summary>
    /// <param name="knownMethods">The mapping of known methods being built.</param>
    static partial void AddKnownMethods(IDictionary<string, string> knownMethods);

    /// <summary>
    /// Tries to get the mapped HLSL-compatible sampler resource type name for the input indexer name.
    /// </summary>
    /// <param name="name">The input fully qualified indexer name.</param>
    /// <param name="mapped">The mapped type name, if one is found.</param>
    /// <returns>The HLSL-compatible type name that can be used in an HLSL shader for the given sampler.</returns>
    public static bool TryGetMappedResourceSamplerAccessType(string name, out string? mapped)
    {
        return KnownResourceSamplers.TryGetValue(name, out mapped);
    }

    /// <summary>
    /// Tries to get the mapped HLSL-compatible method name for the input method name.
    /// </summary>
    /// <param name="name">The input fully qualified method name.</param>
    /// <param name="mapped">The mapped name, if one is found.</param>
    /// <param name="requiresParametersMapping">Whether the method is an intrinsic, but it requires matching parameters too.</param>
    /// <returns>Whether the input method was an intrinsic.</returns>
    public static bool TryGetMappedName(string name, out string? mapped, out bool requiresParametersMapping)
    {
        if (KnownMethods.TryGetValue(name, out string? value))
        {
            if (value == nameof(HlslIntrinsicNameAttribute.RequiresParametersMatching))
            {
                mapped = null;
                requiresParametersMapping = true;
            }
            else
            {
                mapped = value;
                requiresParametersMapping = false;
            }

            return true;
        }

        mapped = null;
        requiresParametersMapping = false;

        return false;
    }

    /// <summary>
    /// Gets the mapped HLSL-compatible method name for the input method name and parameters.
    /// </summary>
    /// <param name="name">The method name (previously validated with <see cref="TryGetMappedName"/>).</param>
    /// <param name="parameterTypeNames">The sequence of type names for the method.</param>
    /// <returns>The intrinsic name for the current method.</returns>
    public static string GetMappedNameWithParameters(string name, IEnumerable<string> parameterTypeNames)
    {
        return KnownMethodsParametersMapping[$"{name}({string.Join(", ", parameterTypeNames)})"];
    }
}