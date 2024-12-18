using System.Threading;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp.SourceGenerators.Constants;
using ComputeSharp.D2D1.Uwp.SourceGenerators.Models;
#else
using ComputeSharp.D2D1.WinUI.SourceGenerators.Constants;
using ComputeSharp.D2D1.WinUI.SourceGenerators.Models;
#endif
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.SourceGenerators;
#else
namespace ComputeSharp.D2D1.WinUI.SourceGenerators;
#endif

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
            if (node is not PropertyDeclarationSyntax { AccessorList.Accessors: { Count: 2 } accessors } property)
            {
                return false;
            }

            // The property must be partial (we'll check that it's a declaration from its symbol)
            if (!property.Modifiers.Any(SyntaxKind.PartialKeyword))
            {
                return false;
            }

            // Static properties are not supported
            if (property.Modifiers.Any(SyntaxKind.StaticKeyword))
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
        /// Tries to get the accessibility of the property and accessors, if possible.
        /// </summary>
        /// <param name="node">The input <see cref="PropertyDeclarationSyntax"/> node.</param>
        /// <param name="symbol">The input <see cref="IPropertySymbol"/> instance.</param>
        /// <param name="declaredAccessibility">The accessibility of the property, if available.</param>
        /// <param name="getterAccessibility">The accessibility of the <see langword="get"/> accessor, if available.</param>
        /// <param name="setterAccessibility">The accessibility of the <see langword="set"/> accessor, if available.</param>
        /// <returns>Whether the property was valid and the accessibilities could be retrieved.</returns>
        public static bool TryGetAccessibilityModifiers(
            PropertyDeclarationSyntax node,
            IPropertySymbol symbol,
            out Accessibility declaredAccessibility,
            out Accessibility getterAccessibility,
            out Accessibility setterAccessibility)
        {
            declaredAccessibility = Accessibility.NotApplicable;
            getterAccessibility = Accessibility.NotApplicable;
            setterAccessibility = Accessibility.NotApplicable;

            // Ensure that we have a getter and a setter, and that the setter is not init-only
            if (symbol is not { GetMethod: { } getMethod, SetMethod: { IsInitOnly: false } setMethod })
            {
                return false;
            }

            // Track the property accessibility if explicitly set
            if (node.Modifiers.Count > 0)
            {
                declaredAccessibility = symbol.DeclaredAccessibility;
            }

            // Track the accessors accessibility, if explicitly set
            foreach (AccessorDeclarationSyntax accessor in node.AccessorList?.Accessors ?? [])
            {
                if (accessor.Modifiers.Count == 0)
                {
                    continue;
                }

                switch (accessor.Kind())
                {
                    case SyntaxKind.GetAccessorDeclaration:
                        getterAccessibility = getMethod.DeclaredAccessibility;
                        break;
                    case SyntaxKind.SetAccessorDeclaration:
                        setterAccessibility = setMethod.DeclaredAccessibility;
                        break;
                }
            }

            return true;
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
        /// Writes all implementations of partial effect property declarations.
        /// </summary>
        /// <param name="properties">The input set of declared effect properties.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WritePropertyDeclarations(EquatableArray<CanvasEffectPropertyInfo> properties, IndentedTextWriter writer)
        {
            // Helper to get the nullable type name for the initial property value
            static string GetOldValueTypeNameAsNullable(CanvasEffectPropertyInfo propertyInfo)
            {
                // Prepare the nullable type for the previous property value. This is needed because if the type is a reference
                // type, the previous value might be null even if the property type is not nullable, as the first invocation would
                // happen when the property is first set to some value that is not null (but the backing field would still be so).
                // As a cheap way to check whether we need to add nullable, we can simply check whether the type name with nullability
                // annotations ends with a '?'. If it doesn't and the type is a reference type, we add it. Otherwise, we keep it.
                return propertyInfo.IsReferenceTypeOrUnconstraindTypeParameter switch
                {
                    true when !propertyInfo.TypeNameWithNullabilityAnnotations.EndsWith("?")
                        => $"{propertyInfo.TypeNameWithNullabilityAnnotations}?",
                    _ => propertyInfo.TypeNameWithNullabilityAnnotations
                };
            }

            // Helper to get the accessibility with a trailing space
            static string GetExpressionWithTrailingSpace(Accessibility accessibility)
            {
                return accessibility.GetExpression() switch
                {
                    { Length: > 0 } expression => expression + " ",
                    _ => ""
                };
            }

            // First, generate all partial property implementations at the top of the partial type declaration
            foreach (CanvasEffectPropertyInfo propertyInfo in properties)
            {
                string oldValueTypeNameAsNullable = GetOldValueTypeNameAsNullable(propertyInfo);

                writer.WriteLine("/// <inheritdoc/>");
                writer.WriteGeneratedAttributes(GeneratorName);
                writer.Write(GetExpressionWithTrailingSpace(propertyInfo.DeclaredAccessibility));
                writer.WriteIf(propertyInfo.IsRequired, "required ");
                writer.WriteLine($"partial {propertyInfo.TypeNameWithNullabilityAnnotations} {propertyInfo.PropertyName}");
                writer.WriteLine($$"""
                    {
                        {{GetExpressionWithTrailingSpace(propertyInfo.GetterAccessibility)}}get => field;
                        {{GetExpressionWithTrailingSpace(propertyInfo.SetterAccessibility)}}set
                        {
                            if (global::System.Collections.Generic.EqualityComparer<{{oldValueTypeNameAsNullable}}>.Default.Equals(field, value))
                            {
                                return;
                            }
                
                            {{oldValueTypeNameAsNullable}} oldValue = field;
                    
                            On{{propertyInfo.PropertyName}}Changing(value);
                            On{{propertyInfo.PropertyName}}Changing(oldValue, value);
                
                            field = value;
                
                            On{{propertyInfo.PropertyName}}Changed(value);
                            On{{propertyInfo.PropertyName}}Changed(oldValue, value);
                    
                            InvalidateEffectGraph(global::{{WellKnownTypeNames.CanvasEffectInvalidationType}}.{{propertyInfo.InvalidationType}});
                        }
                    }
                    """, isMultiline: true);
                writer.WriteLine();
            }

            // Next, emit all partial method declarations at the bottom of the file
            foreach (CanvasEffectPropertyInfo propertyInfo in properties)
            {
                // On<PROPERTY_NAME>Changing, only with new value
                writer.WriteLine(skipIfPresent: true);
                writer.WriteLine($"""
                    /// <summary>Executes the logic for when <see cref="{propertyInfo.PropertyName}"/> is changing.</summary>
                    /// <param name="value">The new property value being set.</param>
                    /// <remarks>This method is invoked right before the value of <see cref="{propertyInfo.PropertyName}"/> is changed.</remarks>
                    """, isMultiline: true);
                writer.WriteGeneratedAttributes(GeneratorName, includeNonUserCodeAttributes: false);
                writer.WriteLine($"partial void On{propertyInfo.PropertyName}Changing({propertyInfo.TypeNameWithNullabilityAnnotations} newValue);");

                string oldValueTypeNameAsNullable = GetOldValueTypeNameAsNullable(propertyInfo);

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