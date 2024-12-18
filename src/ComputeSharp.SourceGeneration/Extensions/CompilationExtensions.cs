// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// Extension methods for the <see cref="Compilation"/> type.
/// </summary>
internal static class CompilationExtensions
{
    /// <summary>
    /// Tries to build a set of <see cref="INamedTypeSymbol"/> instances form the input mapping of names.
    /// </summary>
    /// <param name="compilation">The <see cref="Compilation"/> to consider for analysis.</param>
    /// <param name="fullyQualifiedTypeNames">The input fully qualified type names.</param>
    /// <param name="typeSymbols">The resulting mapping of resolved <see cref="INamedTypeSymbol"/> instances.</param>
    /// <returns>Whether all requested <see cref="INamedTypeSymbol"/> instances could be resolved.</returns>
    public static bool TryBuildNamedTypeSymbolSet(
        this Compilation compilation,
        IEnumerable<string> fullyQualifiedTypeNames,
        [NotNullWhen(true)] out ImmutableHashSet<INamedTypeSymbol>? typeSymbols)
    {
        ImmutableHashSet<INamedTypeSymbol>.Builder builder = ImmutableHashSet.CreateBuilder<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        foreach (string fullyQualifiedTypeName in fullyQualifiedTypeNames)
        {
            if (compilation.GetTypeByMetadataName(fullyQualifiedTypeName) is not INamedTypeSymbol typeSymbol)
            {
                typeSymbols = null;

                return false;
            }

            _ = builder.Add(typeSymbol);
        }

        typeSymbols = builder.ToImmutable();

        return true;
    }

    /// <summary>
    /// Checks whether a given compilation (assumed to be for C#) is using the preview language version.
    /// </summary>
    /// <param name="compilation">The <see cref="Compilation"/> to consider for analysis.</param>
    /// <returns>Whether <paramref name="compilation"/> is using the preview language version.</returns>
    public static bool IsLanguageVersionPreview(this Compilation compilation)
    {
        return ((CSharpCompilation)compilation).LanguageVersion == LanguageVersion.Preview;
    }

    /// <summary>
    /// Checks whether the <c>AllowUnsafeBlocks</c> option is set for a given compilation.
    /// </summary>
    /// <param name="compilation">The <see cref="Compilation"/> object to check.</param>
    /// <returns>Whether the <c>AllowUnsafeBlocks</c> option is set for <paramref name="compilation"/>.</returns>
    public static bool IsAllowUnsafeBlocksEnabled(this Compilation compilation)
    {
        return compilation.Options is CSharpCompilationOptions { AllowUnsafe: true };
    }

    /// <summary>
    /// <para>
    /// Checks whether or not a type with a specified metadata name is accessible from a given <see cref="Compilation"/> instance.
    /// </para>
    /// <para>
    /// This method enumerates candidate type symbols to find a match in the following order:
    /// <list type="number">
    ///   <item><description>
    ///     If only one type with the given name is found within the compilation and its referenced assemblies, check its accessibility.
    ///   </description></item>
    ///   <item><description>
    ///     If the current <paramref name="compilation"/> defines the symbol, check its accessibility.
    ///   </description></item>
    ///   <item><description>
    ///     Otherwise, check whether the type exists and is accessible from any of the referenced assemblies.
    ///   </description></item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="compilation">The <see cref="Compilation"/> to consider for analysis.</param>
    /// <param name="fullyQualifiedMetadataName">The fully-qualified metadata type name to find.</param>
    /// <returns>Whether a type with the specified metadata name can be accessed from the given compilation.</returns>
    public static bool HasAccessibleTypeWithMetadataName(this Compilation compilation, string fullyQualifiedMetadataName)
    {
        // If there is only a single matching symbol, check its accessibility
        if (compilation.GetTypeByMetadataName(fullyQualifiedMetadataName) is INamedTypeSymbol typeSymbol)
        {
            return compilation.IsSymbolAccessibleWithin(typeSymbol, compilation.Assembly);
        }

        // Otherwise, check all available types
        foreach (INamedTypeSymbol currentTypeSymbol in compilation.GetTypesByMetadataName(fullyQualifiedMetadataName))
        {
            if (compilation.IsSymbolAccessibleWithin(currentTypeSymbol, compilation.Assembly))
            {
                return true;
            }
        }

        return false;
    }
}