using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators
{
    /// <inheritdoc/>
    public sealed partial class IShaderGenerator
    {
        /// <inheritdoc/>
        private static partial MethodDeclarationSyntax CreateLoadDispatchMetadataMethod(
            GeneratorExecutionContext context,
            StructDeclarationSyntax structDeclaration,
            INamedTypeSymbol structDeclarationSymbol,
            IEnumerable<string> discoveredResources,
            int root32BitConstantsCount,
            bool isSamplerUsed)
        {
            return MethodDeclaration(IdentifierName("void"), "LoadDispatchMetadataMethod");
        }
    }
}

