using System;
using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGeneration.SyntaxRewriters;

/// <inheritdoc/>
partial class StaticFieldRewriter
{
    /// <inheritdoc/>
    private partial void TrackKnownPropertyAccess(IMemberReferenceOperation operation, MemberAccessExpressionSyntax node)
    {
        if (operation.Member.IsStatic)
        {
            string typeName = operation.Member.ContainingType.GetFullyQualifiedMetadataName();

            // Special dispatch types are not supported from static constants
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

    /// <inheritdoc/>
    private partial void TrackKnownMethodInvocation(string metadataName)
    {
        // No special tracking is needed for DX12 compute shaders
    }
}