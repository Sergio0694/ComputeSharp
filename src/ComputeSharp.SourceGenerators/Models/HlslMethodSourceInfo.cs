using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGenerators.Models;

/// <summary>
/// A model for extracted info on a processed HLSL method.
/// </summary>
/// <param name="MetadataName">The metadata name for the method.</param>
/// <param name="EntryPoint">The meethod entry point.</param>
/// <param name="DefinedTypes">The discovered declared types.</param>
/// <param name="DefinedConstants">The discovered defined constants.</param>
/// <param name="DependentMethods">The dependent captured methods.</param>
internal sealed record HlslMethodSourceInfo(
    string MetadataName,
    string EntryPoint,
    EquatableArray<(string Name, string Definition)> DefinedTypes,
    EquatableArray<(string Name, string Value)> DefinedConstants,
    EquatableArray<(string Signature, string Definition)> DependentMethods);