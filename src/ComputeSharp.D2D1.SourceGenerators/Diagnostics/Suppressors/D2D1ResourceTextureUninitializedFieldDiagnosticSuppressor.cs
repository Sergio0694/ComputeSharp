using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using static ComputeSharp.D2D1.SourceGenerators.Diagnostics.SuppressionDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <summary>
/// <para>
/// A diagnostic suppressor to suppress CS0649 warnings for uninitialized D2D1 resource texture fields.
/// </para>
/// <para>
/// That is, this diagnostic suppressor will suppress the following diagnostic:
/// <code>
/// public struct MyShader : ID2D1PixelShader
/// {
///     [D2DResourceTextureIndex(0)]
///     private D2D1ResourceTexture1D&lt;float4&gt; texture;
/// 
///     public float4 Execute()
///     {
///         return texture[0];
///     }
/// }
/// </code>
/// </para>
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class D2D1ResourceTextureUninitializedFieldDiagnosticSuppressor : DiagnosticSuppressor
{
    /// <inheritdoc/>
    public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions => [UninitializedD2D1ResourceTextureField];

    /// <inheritdoc/>
    public override void ReportSuppressions(SuppressionAnalysisContext context)
    {
        foreach (Diagnostic diagnostic in context.ReportedDiagnostics)
        {
            SyntaxNode? syntaxNode = diagnostic.Location.SourceTree?.GetRoot(context.CancellationToken).FindNode(diagnostic.Location.SourceSpan);

            if (syntaxNode is not null)
            {
                SemanticModel semanticModel = context.GetSemanticModel(syntaxNode.SyntaxTree);

                // Get the field symbol
                ISymbol? declaredSymbol = semanticModel.GetDeclaredSymbol(syntaxNode, context.CancellationToken);

                // Check if the field is in a struct and it's of a D2D1 resource texture type
                if (declaredSymbol is IFieldSymbol { ContainingType: { TypeKind: TypeKind.Struct } structSymbol } fieldSymbol &&
                    HlslKnownTypes.IsResourceTextureType(fieldSymbol.Type.GetFullyQualifiedMetadataName()))
                {
                    // Get the ID2D1PixelShader interface symbol to the check the containing type of the field
                    if (semanticModel.Compilation.GetTypeByMetadataName("ComputeSharp.D2D1.ID2D1PixelShader") is not { } d2D1PixelShaderSymbol)
                    {
                        continue;
                    }

                    // Also check if the containing type is in fact a D2D1 pixel shader type
                    if (structSymbol.HasInterfaceWithType(d2D1PixelShaderSymbol))
                    {
                        context.ReportSuppression(Suppression.Create(UninitializedD2D1ResourceTextureField, diagnostic));
                    }
                }
            }
        }
    }
}