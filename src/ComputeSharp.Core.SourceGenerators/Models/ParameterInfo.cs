namespace ComputeSharp.Core.SourceGenerators.Models;

/// <summary>
/// A model describing info on a constructor parameter.
/// </summary>
/// <param name="Type">The fully qualified type name.</param>
/// <param name="Name">The parameter name.</param>
internal sealed record ParameterInfo(string Type, string Name);
