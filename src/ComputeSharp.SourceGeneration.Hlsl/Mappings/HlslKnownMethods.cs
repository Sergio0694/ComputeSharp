using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using ComputeSharp.Core.Intrinsics.Attributes;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <summary>
/// A <see langword="class"/> that contains and maps known HLSL method names to common .NET methods.
/// </summary>
internal static partial class HlslKnownMethods
{
    /// <summary>
    /// The mapping of supported known methods to HLSL names.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, string> KnownMethods = BuildKnownMethodsMap();

    /// <summary>
    /// The mapping of supported known methods that require parameters mapping to HLSL names.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, string> KnownMethodsParametersMapping = BuildKnownMethodsParametersMappingMap();

    /// <summary>
    /// Builds the mapping of supported known methods to HLSL names.
    /// </summary>
    private static IReadOnlyDictionary<string, string> BuildKnownMethodsMap()
    {
        Dictionary<string, string> knownMethods = new()
        {
            [$"{typeof(Math).FullName}.{nameof(Math.Abs)}"] = "abs",
            [$"{typeof(Math).FullName}.{nameof(Math.Max)}"] = "max",
            [$"{typeof(Math).FullName}.{nameof(Math.Min)}"] = "min",
            [$"{typeof(Math).FullName}.{nameof(Math.Pow)}"] = "pow",
            [$"{typeof(Math).FullName}.{nameof(Math.Sin)}"] = "sin",
            [$"{typeof(Math).FullName}.{nameof(Math.Sinh)}"] = "sinh",
            [$"{typeof(Math).FullName}.{nameof(Math.Asin)}"] = "asin",
            [$"{typeof(Math).FullName}.{nameof(Math.Cos)}"] = "cos",
            [$"{typeof(Math).FullName}.{nameof(Math.Cosh)}"] = "cosh",
            [$"{typeof(Math).FullName}.{nameof(Math.Acos)}"] = "acos",
            [$"{typeof(Math).FullName}.{nameof(Math.Tan)}"] = "tan",
            [$"{typeof(Math).FullName}.{nameof(Math.Tanh)}"] = "tanh",
            [$"{typeof(Math).FullName}.{nameof(Math.Atan)}"] = "atan",
            [$"{typeof(Math).FullName}.{nameof(Math.Atan2)}"] = "atan2",
            [$"{typeof(Math).FullName}.{nameof(Math.Ceiling)}"] = "ceil",
            [$"{typeof(Math).FullName}.{nameof(Math.Floor)}"] = "floor",
            [$"{typeof(Math).FullName}.Clamp"] = "clamp",
            [$"{typeof(Math).FullName}.{nameof(Math.Exp)}"] = "exp",
            [$"{typeof(Math).FullName}.{nameof(Math.Log)}"] = "log",
            [$"{typeof(Math).FullName}.{nameof(Math.Log10)}"] = "log10",
            [$"{typeof(Math).FullName}.{nameof(Math.Max)}"] = "max",
            [$"{typeof(Math).FullName}.{nameof(Math.Min)}"] = "min",
            [$"{typeof(Math).FullName}.{nameof(Math.Round)}"] = "round",
            [$"{typeof(Math).FullName}.{nameof(Math.Sqrt)}"] = "sqrt",
            [$"{typeof(Math).FullName}.{nameof(Math.Sign)}"] = "sign",
            [$"{typeof(Math).FullName}.{nameof(Math.Truncate)}"] = "trunc",

            ["System.MathF.Abs"] = "abs",
            ["System.MathF.Max"] = "max",
            ["System.MathF.Min"] = "min",
            ["System.MathF.Pow"] = "pow",
            ["System.MathF.Sin"] = "sin",
            ["System.MathF.Sinh"] = "sinh",
            ["System.MathF.Asin"] = "asin",
            ["System.MathF.Cos"] = "cos",
            ["System.MathF.Cosh"] = "cosh",
            ["System.MathF.Acos"] = "acos",
            ["System.MathF.Tan"] = "tan",
            ["System.MathF.Tanh"] = "tanh",
            ["System.MathF.Atan"] = "atan",
            ["System.MathF.Atan2"] = "atan2",
            ["System.MathF.Ceiling"] = "ceil",
            ["System.MathF.Floor"] = "floor",
            ["System.MathF.Clamp"] = "clamp",
            ["System.MathF.Exp"] = "exp",
            ["System.MathF.Log"] = "log",
            ["System.MathF.Log10"] = "log10",
            ["System.MathF.Max"] = "max",
            ["System.MathF.Min"] = "min",
            ["System.MathF.Round"] = "round",
            ["System.MathF.Sqrt"] = "sqrt",
            ["System.MathF.Sign"] = "sign",
            ["System.MathF.Truncate"] = "trunc",

            [$"{typeof(float).FullName}.IsFinite"] = "isfinite",
            [$"{typeof(float).FullName}.{nameof(float.IsInfinity)}"] = "isinf",
            [$"{typeof(float).FullName}.{nameof(float.IsNaN)}"] = "isnan",

            [$"{typeof(double).FullName}.IsFinite"] = "isfinite",
            [$"{typeof(double).FullName}.{nameof(double.IsInfinity)}"] = "isinf",
            [$"{typeof(double).FullName}.{nameof(double.IsNaN)}"] = "isnan",

            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Abs)}"] = "abs",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Clamp)}"] = "clamp",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Distance)}"] = "distance",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Dot)}"] = "dot",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Lerp)}"] = "lerp",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Max)}"] = "max",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Min)}"] = "min",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Reflect)}"] = "reflect",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Transform)}"] = "mul",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.TransformNormal)}"] = "mul",
            [$"{typeof(Vector2).FullName}.{nameof(Vector2.Normalize)}"] = "normalize",

            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Abs)}"] = "abs",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Clamp)}"] = "clamp",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Cross)}"] = "cross",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Distance)}"] = "distance",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Dot)}"] = "dot",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Lerp)}"] = "lerp",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Max)}"] = "max",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Min)}"] = "min",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Reflect)}"] = "reflect",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Transform)}"] = "mul",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.TransformNormal)}"] = "mul",
            [$"{typeof(Vector3).FullName}.{nameof(Vector3.Normalize)}"] = "normalize",

            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Abs)}"] = "abs",
            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Clamp)}"] = "clamp",
            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Distance)}"] = "distance",
            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Dot)}"] = "dot",
            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Lerp)}"] = "lerp",
            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Max)}"] = "max",
            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Min)}"] = "min",
            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Transform)}"] = "mul",
            [$"{typeof(Vector4).FullName}.{nameof(Vector4.Normalize)}"] = "normalize"
        };

        // Programmatically load mappings from the Hlsl class as well (the ones with no parameter matching)
        foreach (var method in
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
    private static IReadOnlyDictionary<string, string> BuildKnownMethodsParametersMappingMap()
    {
        ILookup<string, MethodInfo> methodsLookup = typeof(Hlsl).GetMethods(BindingFlags.Public | BindingFlags.Static).ToLookup(static method => method.Name);

        Dictionary<string, string> knownMethods = new();

        // Go through the previously discovered methods to find the ones requiring parameters mapping
        foreach (var pair in KnownMethods)
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
    /// Adds more known methods to the mapping to use.
    /// </summary>
    /// <param name="knownMethods">The mapping of known methods being built.</param>
    static partial void AddKnownMethods(IDictionary<string, string> knownMethods);

    /// <summary>
    /// The mapping of supported known samplers to HLSL resource type names.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, string?> KnownResourceSamplers = BuildKnownResourceSamplers();

    /// <summary>
    /// Builds the mapping of supported known samplers to HLSL resource type names.
    /// </summary>
    /// <returns>The mapping of supported known samplers to HLSL resource type names.</returns>
    private static partial IReadOnlyDictionary<string, string?> BuildKnownResourceSamplers();

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