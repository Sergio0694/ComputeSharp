using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.Core.SourceGenerators.Models;

/// <summary>
/// A model containiing information on a constructor to generate.
/// </summary>
/// <param name="Parameters">The <see cref="ParameterInfo"/> values for the constructor.</param>
/// <param name="DefaultedFields">The names of ignored fields that should be defaulted.</param>
internal sealed record ConstructorInfo(EquatableArray<ParameterInfo> Parameters, EquatableArray<string> DefaultedFields);