using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
public sealed partial class IShaderGenerator
{
    /// <summary>
    /// Extracts the names of all instance fields of a <see cref="System.Delegate"/> type.
    /// </summary>
    /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
    /// <returns>The names of all <see cref="System.Delegate"/> instance fields within <paramref name="structDeclarationSymbol"/>.</returns>
    [Pure]
    private static ImmutableArray<string> GetDelegateFieldNames(INamedTypeSymbol structDeclarationSymbol)
    {
        return
            structDeclarationSymbol
            .GetMembers()
            .OfType<IFieldSymbol>()
            .Where(static member => member.Type is INamedTypeSymbol { TypeKind: TypeKind.Delegate, IsStatic: false })
            .Select(static member => member.Name)
            .ToImmutableArray();
    }
}
