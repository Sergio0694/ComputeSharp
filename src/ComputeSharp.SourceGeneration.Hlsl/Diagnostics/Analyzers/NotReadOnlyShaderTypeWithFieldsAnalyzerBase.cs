using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ComputeSharp.SourceGeneration.Diagnostics;

/// <summary>
/// A diagnostic analyzer that generates a warning if a shader type is not readonly.
/// </summary>
/// <param name="diagnosticDescriptor">The <see cref="DiagnosticDescriptor"/> instance to use.</param>
/// <param name="shaderInterfaceTypeFullyQualifiedMetadataNames">The fully qualified metadata names of all candidate shader interface types.</param>
public abstract class NotReadOnlyShaderTypeWithFieldsAnalyzerBase(
    DiagnosticDescriptor diagnosticDescriptor,
    ImmutableArray<string> shaderInterfaceTypeFullyQualifiedMetadataNames) : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [diagnosticDescriptor];

    /// <inheritdoc/>
    public sealed override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(context =>
        {
            // Get the candidate type symbols
            if (!context.Compilation.TryBuildNamedTypeSymbolSet(shaderInterfaceTypeFullyQualifiedMetadataNames, out ImmutableHashSet<INamedTypeSymbol>? typeSymbols))
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // Only struct types that are not readonly are possible targets
                if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Struct, IsReadOnly: false } typeSymbol)
                {
                    return;
                }

                // In order to trigger the analyzer, we must have at least one field.
                // If not (eg. shaders that just return a constant color), no need.
                if (!typeSymbol.GetMembers().Any(static m => m is IFieldSymbol { IsStatic: false, IsConst: false, IsFixedSizeBuffer: false }))
                {
                    return;
                }

                // Emit a diagnostic if the type is a shader type
                if (IsShaderType(typeSymbol, typeSymbols))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        diagnosticDescriptor,
                        typeSymbol.Locations.FirstOrDefault(),
                        typeSymbol));
                }
            }, SymbolKind.NamedType);
        });
    }

    /// <summary>
    /// Checks whether a given type is a shader type.
    /// </summary>
    /// <param name="typeSymbol">The type to check.</param>
    /// <param name="typeSymbols">The list of candidate type symbols for shader interface types.</param>
    /// <returns></returns>
    private static bool IsShaderType(INamedTypeSymbol typeSymbol, ImmutableHashSet<INamedTypeSymbol> typeSymbols)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            // Look for either the interface itself, if not generic, or the unbounded type if it's generic
            if ((!interfaceSymbol.IsGenericType && typeSymbols.Contains(interfaceSymbol)) ||
                (interfaceSymbol.IsGenericType && typeSymbols.Contains(interfaceSymbol.ConstructedFrom)))
            {
                return true;
            }
        }

        return false;
    }
}