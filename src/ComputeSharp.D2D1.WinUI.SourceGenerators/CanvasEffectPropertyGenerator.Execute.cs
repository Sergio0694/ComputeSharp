using System.Threading;
using ComputeSharp.D2D1.WinUI.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.D2D1.WinUI.SourceGenerators;

/// <inheritdoc/>
partial class CanvasEffectPropertyGenerator
{
    /// <summary>
    /// A container for all the logic for <see cref="CanvasEffectPropertyGenerator"/>.
    /// </summary>
    private static partial class Execute
    {
        /// <summary>
        /// Checks whether an input syntax node is a candidate property declaration for the generator.
        /// </summary>
        /// <param name="node">The input syntax node to check.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns>Whether <paramref name="node"/> is a candidate property declaration.</returns>
        public static bool IsCandidatePropertyDeclaration(SyntaxNode node, CancellationToken token)
        {
            // The node must be a property declaration with two accessors
            if (node is not PropertyDeclarationSyntax { AccessorList.Accessors: { Count: 2 } accessors })
            {
                return false;
            }

            // The accessors must be a get and a set (with any accessibility)
            if (accessors[0].Kind() is not (SyntaxKind.GetAccessorDeclaration or SyntaxKind.SetAccessorDeclaration) ||
                accessors[1].Kind() is not (SyntaxKind.GetAccessorDeclaration or SyntaxKind.SetAccessorDeclaration))
            {
                return false;
            }

            // The property must be in a type with a base type (as it must derive from CanvasEffect)
            return node.Parent?.IsTypeDeclarationWithOrPotentiallyWithBaseTypes<ClassDeclarationSyntax>() == true;
        }

        /// <summary>
        /// Gets the invalidation type to use for the generated effect property.
        /// </summary>
        /// <param name="attributeData">The <see cref="AttributeData"/> instance for the processed attribute.</param>
        /// <returns>The resulting <see cref="CanvasEffectInvalidationType"/> to use for the generated property.</returns>
        public static CanvasEffectInvalidationType GetCanvasEffectInvalidationType(AttributeData attributeData)
        {
            if (attributeData.ConstructorArguments is [{ Kind: TypedConstantKind.Enum, Value: byte enumValue }])
            {
                return (CanvasEffectInvalidationType)enumValue;
            }

            // No constructor parameter, or an invalid one. In this case we either just use the default
            // invalidation mode, or let the analyzer emit a diagnostic to let the user know.
            return CanvasEffectInvalidationType.Update;
        }

        /// <summary>
        /// Checks whether the generated code has to directly reference the old property value.
        /// </summary>
        /// <param name="propertySymbol">The input <see cref="IPropertySymbol"/> instance to process.</param>
        /// <returns>Whether the generated code needs direct access to the old property value.</returns>
        public static bool IsOldPropertyValueDirectlyReferenced(IPropertySymbol propertySymbol)
        {
            // Check On<PROPERTY_NAME>Changing(<PROPERTY_TYPE> oldValue, <PROPERTY_TYPE> newValue) first
            foreach (ISymbol symbol in propertySymbol.ContainingType.GetMembers($"On{propertySymbol.Name}Changing"))
            {
                // No need to be too specific as we're not expecting false positives (which also wouldn't really
                // cause any problems anyway, just produce slightly worse codegen). Just checking the number of
                // parameters is good enough, and keeps the code very simple and cheap to run.
                if (symbol is IMethodSymbol { Parameters.Length: 2 })
                {
                    return true;
                }
            }

            // Do the same for On<PROPERTY_NAME>Changed(<PROPERTY_TYPE> oldValue, <PROPERTY_TYPE> newValue)
            foreach (ISymbol symbol in propertySymbol.ContainingType.GetMembers($"On{propertySymbol.Name}Changed"))
            {
                if (symbol is IMethodSymbol { Parameters.Length: 2 })
                {
                    return true;
                }
            }

            return false;
        }

        public static void WritePropertyDeclarations(EquatableArray<CanvasEffectPropertyInfo> properties, IndentedTextWriter writer)
        {
            // First, generate all partial property implementations at the top of the partial type declaration
            foreach (CanvasEffectPropertyInfo propertyInfo in properties)
            {
                writer.WriteLine(skipIfPresent: true);
                writer.WriteLine("/// <inheritdoc/>");
                writer.WriteGeneratedAttributes(GeneratorName);
                writer.WriteLine($$"""                    
                    public partial {{propertyInfo.TypeNameWithNullabilityAnnotations}} {{propertyInfo.PropertyName}}
                    {
                        get => field;
                        set
                        {
                            if (global::System.Collections.Generic.EqualityComparer<{{propertyInfo.TypeNameWithNullabilityAnnotations}}>.Default.Equals(field, value))
                            {
                                return;
                            }
                
                            int oldValue = field;
                    
                            On{{propertyInfo.PropertyName}}Changing(value);
                            On{{propertyInfo.PropertyName}}Changing(oldValue, value);
                
                            field = value;
                
                            On{{propertyInfo.PropertyName}}Changed(value);
                            On{{propertyInfo.PropertyName}}Changed(oldValue, value);
                    
                            InvalidateEffectGraph(global::ComputeSharp.D2D1.WinUI.CanvasEffectInvalidationType.{{propertyInfo.InvalidationType}});
                        }
                    }
                    """, isMultiline: true);
            }

            foreach (CanvasEffectPropertyInfo propertyInfo in properties)
            {
                // On<PROPERTY_NAME>Changing, only with new value
                writer.WriteLine();
                writer.WriteLine($"""
                    /// <summary>Executes the logic for when <see cref="{propertyInfo.PropertyName}"/> is changing.</summary>
                    /// <param name="value">The new property value being set.</param>
                    /// <remarks>This method is invoked right before the value of <see cref="{propertyInfo.PropertyName}"/> is changed.</remarks>
                    """, isMultiline: true);
                writer.WriteGeneratedAttributes(GeneratorName, includeNonUserCodeAttributes: false);
                writer.WriteLine($"partial void On{propertyInfo.PropertyName}Changing({propertyInfo.TypeNameWithNullabilityAnnotations} newValue);");

                // Prepare the nullable type for the previous property value. This is needed because if the type is a reference
                // type, the previous value might be null even if the property type is not nullable, as the first invocation would
                // happen when the property is first set to some value that is not null (but the backing field would still be so).
                // As a cheap way to check whether we need to add nullable, we can simply check whether the type name with nullability
                // annotations ends with a '?'. If it doesn't and the type is a reference type, we add it. Otherwise, we keep it.
                string oldValueTypeNameAsNullable = propertyInfo.IsReferenceTypeOrUnconstraindTypeParameter switch
                {
                    true when !propertyInfo.TypeNameWithNullabilityAnnotations.EndsWith("?")
                        => $"{propertyInfo.TypeNameWithNullabilityAnnotations}?",
                    _ => propertyInfo.TypeNameWithNullabilityAnnotations
                };

                // On<PROPERTY_NAME>Changing, with both values
                writer.WriteLine();
                writer.WriteLine($"""
                    /// <summary>Executes the logic for when <see cref="{propertyInfo.PropertyName}"/> is changing.</summary>
                    /// <param name="oldValue">The previous property value that is being replaced.</param>
                    /// <param name="newValue">The new property value being set.</param>
                    /// <remarks>This method is invoked right before the value of <see cref="{propertyInfo.PropertyName}"/> is changed.</remarks>
                    """, isMultiline: true);
                writer.WriteGeneratedAttributes(GeneratorName, includeNonUserCodeAttributes: false);
                writer.WriteLine($"partial void On{propertyInfo.PropertyName}Changing({oldValueTypeNameAsNullable} oldValue, {propertyInfo.TypeNameWithNullabilityAnnotations} newValue);");

                // On<PROPERTY_NAME>Changed, only with new value
                writer.WriteLine();
                writer.WriteLine($"""
                    /// <summary>Executes the logic for when <see cref="{propertyInfo.PropertyName}"/> has just changed.</summary>
                    /// <param name="value">The new property value that has been set.</param>
                    /// <remarks>This method is invoked right after the value of <see cref="{propertyInfo.PropertyName}"/> is changed.</remarks>
                    """, isMultiline: true);
                writer.WriteGeneratedAttributes(GeneratorName, includeNonUserCodeAttributes: false);
                writer.WriteLine($"partial void On{propertyInfo.PropertyName}Changed({propertyInfo.TypeNameWithNullabilityAnnotations} newValue);");

                // On<PROPERTY_NAME>Changed, with both values
                writer.WriteLine();
                writer.WriteLine($"""
                    /// <summary>Executes the logic for when <see cref="{propertyInfo.PropertyName}"/> has just changed.</summary>
                    /// <param name="oldValue">The previous property value that has been replaced.</param>
                    /// <param name="newValue">The new property value that has been set.</param>
                    /// <remarks>This method is invoked right after the value of <see cref="{propertyInfo.PropertyName}"/> is changed.</remarks>
                    """, isMultiline: true);
                writer.WriteGeneratedAttributes(GeneratorName, includeNonUserCodeAttributes: false);
                writer.WriteLine($"partial void On{propertyInfo.PropertyName}Changed({oldValueTypeNameAsNullable} oldValue, {propertyInfo.TypeNameWithNullabilityAnnotations} newValue);");
            }
        }
    }
}