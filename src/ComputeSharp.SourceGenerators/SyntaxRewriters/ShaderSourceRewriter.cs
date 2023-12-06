using System;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <inheritdoc/>
partial class ShaderSourceRewriter
{
    /// <summary>
    /// Gets whether or not the shader uses <see cref="GroupIds"/> at least once (except <see cref="GroupIds.Index"/>).
    /// </summary>
    public bool IsGroupIdsUsed { get; private set; }

    /// <summary>
    /// Gets whether or not the shader uses <see cref="GroupIds.Index"/> at least once.
    /// </summary>
    public bool IsGroupIdsIndexUsed { get; private set; }

    /// <summary>
    /// Gets whether or not the shader uses <see cref="GridIds"/> at least once.
    /// </summary>
    public bool IsGridIdsUsed { get; set; }

    /// <summary>
    /// Gets whether or not the shader uses a texture sampler at least once.
    /// </summary>
    public bool IsSamplerUsed { get; private set; }

    /// <inheritdoc/>
    private partial SyntaxNode RewriteSampledTextureAccess(IInvocationOperation operation, ExpressionSyntax expression, ArgumentSyntax arguments)
    {
        IsSamplerUsed = true;

        // Transform a method invocation syntax into a sampling call with the implicit static linear sampler.
        // For instance: texture.Sample(uv) will be rewritten as texture.SampleLevel(__sampler, uv, 0).
        return
            InvocationExpression(((MemberAccessExpressionSyntax)expression).WithName(IdentifierName("SampleLevel")))
            .AddArgumentListArguments(
                Argument(IdentifierName("__sampler")),
                arguments,
                Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))));
    }

    /// <inheritdoc/>
    partial void TrackKnownPropertyAccess(IMemberReferenceOperation operation, MemberAccessExpressionSyntax node, string mappedName)
    {
        // Mark which dispatch properties have been used, to optimize the declaration afterwards
        if (operation.Member.IsStatic)
        {
            string typeName = operation.Member.ContainingType.GetFullyQualifiedMetadataName();

            if (mappedName == $"__{nameof(GroupIds)}__get_Index")
            {
                IsGroupIdsIndexUsed = true;
            }
            else if (typeName == typeof(GroupIds).FullName)
            {
                IsGroupIdsUsed = true;
            }
            else if (typeName == typeof(GridIds).FullName)
            {
                IsGridIdsUsed = true;
            }

            // Check that the dispatch info types are only used from the main shader body
            if (!this.isEntryPoint || this.localFunctionDepth > 0)
            {
                DiagnosticDescriptor? descriptor = typeName switch
                {
                    _ when typeName == typeof(ThreadIds).FullName || typeName == typeof(ThreadIds.Normalized).FullName => InvalidThreadIdsUsage,
                    _ when typeName == typeof(GroupIds).FullName => InvalidGroupIdsUsage,
                    _ when typeName == typeof(GroupSize).FullName => InvalidGroupSizeUsage,
                    _ when typeName == typeof(GridIds).FullName => InvalidGridIdsUsage,
                    _ when typeName == typeof(DispatchSize).FullName => InvalidDispatchSizeUsage,
                    _ => null
                };

                if (descriptor is not null)
                {
                    Diagnostics.Add(descriptor, node);
                }
            }
        }
    }
}