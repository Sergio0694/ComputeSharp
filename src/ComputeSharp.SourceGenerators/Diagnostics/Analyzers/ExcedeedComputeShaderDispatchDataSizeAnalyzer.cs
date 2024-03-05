using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates errors when a compute shader exceeds the maximum root descriptor size.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ExcedeedComputeShaderDispatchDataSizeAnalyzer : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [ShaderDispatchDataSizeExceeded];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Get the IComputeShader and IComputeShader<TPixel> symbols
            if (context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader") is not { } computeShaderSymbol ||
                context.Compilation.GetTypeByMetadataName("ComputeSharp.IComputeShader`1") is not { } pixelShaderSymbol)
            {
                return;
            }

            context.RegisterSymbolAction(context =>
            {
                // Only struct types are possible targets
                if (context.Symbol is not INamedTypeSymbol { TypeKind: TypeKind.Struct } typeSymbol)
                {
                    return;
                }

                // If the type is not a compute shader type, immediately bail out
                if (!TryGetIsPixelShaderLike(typeSymbol, computeShaderSymbol, pixelShaderSymbol, out bool isPixelShaderLike))
                {
                    return;
                }

                // Setup the resource and byte offsets for tracking. This is the same logic as
                // in ComputeShaderDescriptorGenerator.ConstantBuffer, see there for more info.
                int constantBufferSizeInBytes = sizeof(int) * (isPixelShaderLike ? 2 : 3);

                // Run the fast-path constant buffer processor logic (same as in the equivalent D2D analyzer)
                ConstantBufferSyntaxProcessor.GetInfo(
                    context.Compilation,
                    typeSymbol,
                    ref constantBufferSizeInBytes);

                // Pad the size to get the number of root DWORD constants (see more notes in the same method)
                constantBufferSizeInBytes = AlignmentHelper.Pad(constantBufferSizeInBytes, sizeof(int));

                // Also get the number of captured resources
                int resourceCount = GetNumberOfResources(typeSymbol);

                // A shader root signature has a maximum size of 64 DWORDs, so 256 bytes.
                // Loaded values in the root signature have the following costs:
                //  - Root constants cost 1 DWORD each, since they are 32-bit values.
                //  - Descriptor tables cost 1 DWORD each.
                //  - Root descriptors (64-bit GPU virtual addresses) cost 2 DWORDs each.
                // So here we check whether the current signature respects that constraint,
                // and emit a build error otherwise. For more info on this, see the docs here:
                // https://docs.microsoft.com/windows/win32/direct3d12/root-signature-limits.
                int numberOfDwordConstants = (constantBufferSizeInBytes / sizeof(int)) + resourceCount;

                // Emit an error in case we have in fact exceeded that limit
                if (numberOfDwordConstants > 64)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        ShaderDispatchDataSizeExceeded,
                        typeSymbol.Locations.First(),
                        typeSymbol,
                        numberOfDwordConstants));
                }
            }, SymbolKind.NamedType);
        });
    }

    /// <summary>
    /// Gets whether a given type is a compute shader type (ie. implements any of the interfaces).
    /// </summary>
    /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance to check.</param>
    /// <param name="computeShaderSymbol">The <see cref="INamedTypeSymbol"/> for <c>"ComputeSharp.IComputeShader"</c>.</param>
    /// <param name="pixelShaderSymbol">The <see cref="INamedTypeSymbol"/> for <c>"ComputeSharp.IComputeShader`1"</c>.</param>
    /// <param name="isPixelShaderLike">Whether <paramref name="typeSymbol"/> is a "pixel shader like" type.</param>
    /// <returns>Whether <paramref name="typeSymbol"/> is a compute shader type at all.</returns>
    private static bool TryGetIsPixelShaderLike(
        INamedTypeSymbol typeSymbol,
        INamedTypeSymbol computeShaderSymbol,
        INamedTypeSymbol pixelShaderSymbol,
        out bool isPixelShaderLike)
    {
        foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.AllInterfaces)
        {
            if (SymbolEqualityComparer.Default.Equals(interfaceSymbol, computeShaderSymbol))
            {
                isPixelShaderLike = false;

                return true;
            }
            else if (SymbolEqualityComparer.Default.Equals(interfaceSymbol.ConstructedFrom, pixelShaderSymbol))
            {
                isPixelShaderLike = true;

                return true;
            }
        }

        isPixelShaderLike = false;

        return false;
    }

    /// <summary>
    /// Gets the number of captured resources in a given compute shader type.
    /// </summary>
    /// <param name="typeSymbol">The shader type to inspect.</param>
    /// <returns>The number of captured resources in the given shader type.</returns>
    private static int GetNumberOfResources(ITypeSymbol typeSymbol)
    {
        int resourceCount = 0;

        foreach (ISymbol memberSymbol in typeSymbol.GetMembers())
        {
            // Only process instance fields
            if (memberSymbol is not IFieldSymbol { Type: INamedTypeSymbol, IsConst: false, IsStatic: false, IsFixedSizeBuffer: false } fieldSymbol)
            {
                continue;
            }

            string typeName = fieldSymbol.Type.GetFullyQualifiedMetadataName();

            // Check if the field is a resource
            if (HlslKnownTypes.IsTypedResourceType(typeName))
            {
                resourceCount++;
            }
        }

        return resourceCount;
    }
}