using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Intrinsics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace ComputeSharp.SourceGeneration.Mappings;

/// <summary>
/// A <see langword="class"/> that contains and maps known HLSL operators names to common .NET methods.
/// </summary>
internal static partial class HlslKnownOperators
{
    /// <summary>
    /// The mapping of supported known operators to HLSL names.
    /// </summary>
    private static readonly Dictionary<string, string> KnownOperators = BuildKnownOperatorsMap();

    /// <summary>
    /// Builds the mapping of supported known operators to HLSL names.
    /// </summary>
    /// <returns>The mapping of supported known operators to HLSL names.</returns>
    private static Dictionary<string, string> BuildKnownOperatorsMap()
    {
        Dictionary<string, string> knownOperators = [];

        // Programmatically load mappings for the intrinsic operators on each HLSL primitive type
        foreach ((Type Type, MethodInfo Operator, string IntrinsicName, bool RequiresParametersMatching) item in
            from type in HlslKnownTypes.EnumerateKnownVectorTypes().Concat(HlslKnownTypes.EnumerateKnownMatrixTypes())
            from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public)
            where method.IsSpecialName && method.Name.StartsWith("op_", StringComparison.InvariantCulture)
            let attribute = method.GetCustomAttribute<HlslIntrinsicNameAttribute>()
            where attribute is not null
            select (Type: type, Operator: method, IntrinsicName: attribute.Name, attribute.RequiresParametersMatching))
        {
            if (!item.RequiresParametersMatching)
            {
                throw new NotSupportedException("All intrinsic operators must require parameter matching.");
            }

            // The key is in the format "<TYPE_NAME>.<METHOD_NAME>(<PARAMETER_TYPES>)"
            knownOperators.Add(
                $"{item.Type.FullName}{Type.Delimiter}{item.Operator.Name}({string.Join(", ", item.Operator.GetParameters().Select(static p => p.ParameterType.Name))})",
                item.IntrinsicName);
        }

        return knownOperators;
    }

    /// <summary>
    /// Checks whether a given operator kind represents a candidate special intrinsic that can handle.
    /// </summary>
    /// <param name="operatorKind">The input operator kind to check.</param>
    /// <returns>Whether <paramref name="operatorKind"/> is a candidate operator for a special intrinsic to rewrite.</returns>
    public static bool IsCandidateOperator(BinaryOperatorKind operatorKind)
    {
        return operatorKind is BinaryOperatorKind.Multiply or BinaryOperatorKind.UnsignedRightShift;
    }

    /// <summary>
    /// Tries to get the mapped HLSL-compatible method name for the input operator name.
    /// </summary>
    /// <param name="name">The input fully qualified operator name.</param>
    /// <param name="parameterTypeNames">The sequence of type names for the method.</param>
    /// <param name="mapped">The mapped name, if one is found.</param>
    /// <returns>The HLSL-compatible method name that can be used in an HLSL shader.</returns>
    public static bool TryGetMappedName(string name, IEnumerable<string> parameterTypeNames, out string? mapped)
    {
        return KnownOperators.TryGetValue($"{name}({string.Join(", ", parameterTypeNames)})", out mapped);
    }
}