using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp.SourceGenerators.Constants;
#else
using ComputeSharp.D2D1.WinUI.SourceGenerators.Constants;
#endif
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
#if WINDOWS_UWP
using static ComputeSharp.D2D1.Uwp.SourceGenerators.DiagnosticDescriptors;
#else
using static ComputeSharp.D2D1.WinUI.SourceGenerators.DiagnosticDescriptors;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.SourceGenerators;
#else
namespace ComputeSharp.D2D1.WinUI.SourceGenerators;
#endif

/// <summary>
/// A diagnostic analyzer that generates a suggestion whenever <c>[GeneratedCanvasEffectProperty]</c> is used on a semi-auto property when a partial property could be used instead.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseGeneratedCanvasEffectPropertyOnSemiAutoPropertyAnalyzer : DiagnosticAnalyzer
{
    /// <summary>
    /// The number of pooled flags per stack (ie. how many properties we expect on average per type).
    /// </summary>
    private const int NumberOfPooledFlagsPerStack = 20;

    /// <summary>
    /// Shared pool for <see cref="Dictionary{TKey, TValue}"/> instances.
    /// </summary>
    [SuppressMessage("MicrosoftCodeAnalysisPerformance", "RS1008", Justification = "This is a pool of (empty) dictionaries, it is not actually storing compilation data.")]
    private static readonly ObjectPool<Dictionary<IPropertySymbol, byte[]>> PropertyMapPool = new(static () => new Dictionary<IPropertySymbol, byte[]>(SymbolEqualityComparer.Default));

    /// <summary>
    /// Shared pool for <see cref="Stack{T}"/>-s of flags, one per type being processed.
    /// </summary>
    private static readonly ObjectPool<Stack<byte[]>> PropertyFlagsStackPool = new(CreatePropertyFlagsStack);

    /// <summary>
    /// The property name for the serialized invalidation mode.
    /// </summary>
    public const string InvalidationModePropertyName = "InvalidationMode";

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [UseGeneratedCanvasEffectPropertyOnSemiAutoProperty];

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterCompilationStartAction(static context =>
        {
            // Using [GeneratedCanvasEffectProperty] on partial properties is only supported when using C# preview.
            // As such, if that is not the case, return immediately, as no diagnostic should be produced.
            if (!context.Compilation.IsLanguageVersionPreview())
            {
                return;
            }

            // Get the [GeneratedCanvasEffectProperty] and CanvasEffect symbols
            if (context.Compilation.GetTypeByMetadataName(WellKnownTypeNames.GeneratedCanvasEffectPropertyAttribute) is not { } generatedCanvasEffectPropertyAttributeSymbol ||
                context.Compilation.GetTypeByMetadataName(WellKnownTypeNames.CanvasEffect) is not { } canvasEffectSymbol)
            {
                return;
            }

            // Get the symbol for the SetPropertyAndInvalidateEffectGraph<T> method as well
            if (!TryGetSetPropertyMethodSymbol(canvasEffectSymbol, out IMethodSymbol? setPropertySymbol))
            {
                return;
            }

            context.RegisterSymbolStartAction(context =>
            {
                // We only care about types that could derive from ObservableObject
                if (context.Symbol is not INamedTypeSymbol { IsStatic: false, IsReferenceType: true, BaseType.SpecialType: not SpecialType.System_Object } typeSymbol)
                {
                    return;
                }

                // If the type does not derive from ObservableObject, ignore it
                if (!typeSymbol.InheritsFromType(canvasEffectSymbol))
                {
                    return;
                }

                Dictionary<IPropertySymbol, byte[]> propertyMap = PropertyMapPool.Allocate();
                Stack<byte[]> propertyFlagsStack = PropertyFlagsStackPool.Allocate();

                // Crawl all members to discover properties that might be of interest
                foreach (ISymbol memberSymbol in typeSymbol.GetMembers())
                {
                    // We're only looking for properties that might be valid candidates for conversion
                    if (memberSymbol is not IPropertySymbol
                        {
                            IsStatic: false,
                            IsPartialDefinition: false,
                            PartialDefinitionPart: null,
                            PartialImplementationPart: null,
                            ReturnsByRef: false,
                            ReturnsByRefReadonly: false,
                            Type.IsRefLikeType: false,
                            GetMethod: not null,
                            SetMethod.IsInitOnly: false
                        } propertySymbol)
                    {
                        continue;
                    }

                    // We can safely ignore properties that already have [GeneratedCanvasEffectProperty].
                    // This is because in that case, the other analyzer will already emit an error.
                    if (propertySymbol.HasAttributeWithType(generatedCanvasEffectPropertyAttributeSymbol))
                    {
                        continue;
                    }

                    // Take an array from the stack or create a new one otherwise
                    byte[] flags = propertyFlagsStack.Count > 0
                        ? propertyFlagsStack.Pop()
                        : new byte[2];

                    // Track the property for later
                    propertyMap.Add(propertySymbol, flags);
                }

                // We want to process both accessors, where we specifically need both the syntax
                // and their semantic model to verify what they're doing. We can use a code callback.
                context.RegisterOperationBlockAction(context =>
                {
                    // Make sure the current symbol is a property accessor
                    if (context.OwningSymbol is not IMethodSymbol { MethodKind: MethodKind.PropertyGet or MethodKind.PropertySet, AssociatedSymbol: IPropertySymbol propertySymbol })
                    {
                        return;
                    }

                    // If so, check that we are actually processing one of the properties we care about
                    if (!propertyMap.TryGetValue(propertySymbol, out byte[]? validFlags))
                    {
                        return;
                    }

                    // Handle the 'get' logic
                    if (SymbolEqualityComparer.Default.Equals(propertySymbol.GetMethod, context.OwningSymbol))
                    {
                        // We expect a top-level block operation, that immediately returns an expression
                        if (context.OperationBlocks is not [IBlockOperation { Operations: [IReturnOperation returnOperation] }])
                        {
                            return;
                        }

                        // Next, we expect the return to produce a field reference
                        if (returnOperation is not { ReturnedValue: IFieldReferenceOperation fieldReferenceOperation })
                        {
                            return;
                        }

                        // The field has to be implicitly declared and not constant (and not static)
                        if (fieldReferenceOperation.Field is not { IsImplicitlyDeclared: true, IsStatic: false } fieldSymbol)
                        {
                            return;
                        }

                        // Validate tha the field is indeed 'field' (it will be associated with the property)
                        if (!SymbolEqualityComparer.Default.Equals(fieldSymbol.AssociatedSymbol, propertySymbol))
                        {
                            return;
                        }

                        // The 'get' accessor is valid
                        validFlags[0] = 1;
                    }
                    else if (SymbolEqualityComparer.Default.Equals(propertySymbol.SetMethod, context.OwningSymbol))
                    {
                        // We expect a top-level block operation, that immediately performs an invocation
                        if (context.OperationBlocks is not [IBlockOperation { Operations: [IExpressionStatementOperation { Operation: IInvocationOperation invocationOperation }] }])
                        {
                            return;
                        }

                        // Brief filtering of the target method, also get the original definition
                        if (invocationOperation.TargetMethod is not { Name: "SetPropertyAndInvalidateEffectGraph", IsGenericMethod: true, IsStatic: false } methodSymbol)
                        {
                            return;
                        }

                        // First, check that we're calling 'CanvasEffect.SetPropertyAndInvalidateEffectGraph'
                        if (!SymbolEqualityComparer.Default.Equals(methodSymbol.ConstructedFrom, setPropertySymbol))
                        {
                            return;
                        }

                        // We matched the method, now let's validate the arguments
                        if (invocationOperation.Arguments is not [{ } locationArgument, { } valueArgument, { } invalidationModeArgument])
                        {
                            return;
                        }

                        // The field has to be implicitly declared and not constant (and not static)
                        if (locationArgument.Value is not IFieldReferenceOperation { Field: { IsImplicitlyDeclared: true, IsStatic: false } fieldSymbol })
                        {
                            return;
                        }

                        // Validate tha the field is indeed 'field' (it will be associated with the property)
                        if (!SymbolEqualityComparer.Default.Equals(fieldSymbol.AssociatedSymbol, propertySymbol))
                        {
                            return;
                        }

                        // The value is just the 'value' keyword
                        if (valueArgument.Value is not IParameterReferenceOperation { Syntax: IdentifierNameSyntax { Identifier.Text: "value" } })
                        {
                            return;
                        }

                        // The invalidation mode can either be the default value...
                        if (invalidationModeArgument is not { IsImplicit: true, ArgumentKind: ArgumentKind.DefaultValue })
                        {
                            validFlags[1] = 1;
                        }
                        else if (invalidationModeArgument is { ConstantValue: { HasValue: true, Value: byte mode } } && mode is 0 or 1)
                        {
                            // ...Or is has to be set explicitly to one of the two supported values
                            validFlags[1] = (byte)(mode + 1);
                        }
                    }
                });

                // We also need to track getters which have no body, and we need syntax for that
                context.RegisterSyntaxNodeAction(context =>
                {
                    // Let's just make sure we do have a property symbol
                    if (context.ContainingSymbol is not IPropertySymbol { GetMethod: not null } propertySymbol)
                    {
                        return;
                    }

                    // Lookup the property to get its flags
                    if (!propertyMap.TryGetValue(propertySymbol, out byte[]? validFlags))
                    {
                        return;
                    }

                    // We expect two accessors, skip if otherwise (the setter will be validated by the other callback)
                    if (context.Node is not PropertyDeclarationSyntax { AccessorList.Accessors: [{ } firstAccessor, { } secondAccessor] })
                    {
                        return;
                    }

                    // Check that either of them is a semicolon token 'get;' accessor (it can be in either position)
                    if ((firstAccessor.IsKind(SyntaxKind.GetAccessorDeclaration) &&
                         firstAccessor.SemicolonToken.IsKind(SyntaxKind.SemicolonToken) &&
                         firstAccessor.ExpressionBody is null) ||
                        (secondAccessor.IsKind(SyntaxKind.GetAccessorDeclaration) &&
                         secondAccessor.SemicolonToken.IsKind(SyntaxKind.SemicolonToken) &&
                         secondAccessor.ExpressionBody is null))
                    {
                        validFlags[0] = 1;
                    }
                }, SyntaxKind.PropertyDeclaration);

                // Finally, we can consume this information when we finish processing the symbol
                context.RegisterSymbolEndAction(context =>
                {
                    // Emit a diagnostic for each property that was a valid match
                    foreach (KeyValuePair<IPropertySymbol, byte[]> pair in propertyMap)
                    {
                        if (pair.Value is [1, 1 or 2])
                        {
                            // Shift back the index to match the actual enum values, to simplify the code fixer. Here we're adding 1 to
                            // signal "the setter is valid", but we want to hide this implementation detail to downstream consumers.
                            int invalidationType = pair.Value[1] - 1;

                            context.ReportDiagnostic(Diagnostic.Create(
                                UseGeneratedCanvasEffectPropertyOnSemiAutoProperty,
                                pair.Key.Locations.FirstOrDefault(),
                                ImmutableDictionary.Create<string, string?>().Add(InvalidationModePropertyName, invalidationType.ToString()),
                                pair.Key));
                        }
                    }

                    // Before clearing the dictionary, move back all values to the stack
                    foreach (byte[] propertyFlags in propertyMap.Values)
                    {
                        // Make sure the array is cleared before returning it
                        propertyFlags.AsSpan().Clear();

                        propertyFlagsStack.Push(propertyFlags);
                    }

                    // We are now done processing the symbol, we can return the dictionary.
                    // Note that we must clear it before doing so to avoid leaks and issues.
                    propertyMap.Clear();

                    PropertyMapPool.Free(propertyMap);

                    // Also do the same for the stack, except we don't need to clean it (since it roots no compilation objects)
                    PropertyFlagsStackPool.Free(propertyFlagsStack);
                });
            }, SymbolKind.NamedType);
        });
    }

    /// <summary>
    /// Tries to get the symbol for the target <c>SetPropertyAndInvalidateEffectGraph</c> method this analyzer looks for.
    /// </summary>
    /// <param name="canvasEffectSymbol">The symbol for <c>CanvasEffect</c>.</param>
    /// <param name="setPropertySymbol">The resulting method symbol, if found (this should always be the case).</param>
    /// <returns>Whether <paramref name="setPropertySymbol"/> could be resolved correctly.</returns>
    private static bool TryGetSetPropertyMethodSymbol(INamedTypeSymbol canvasEffectSymbol, [NotNullWhen(true)] out IMethodSymbol? setPropertySymbol)
    {
        foreach (ISymbol symbol in canvasEffectSymbol.GetMembers("SetPropertyAndInvalidateEffectGraph"))
        {
            // There's only one method with this name, so we can just return it directly
            setPropertySymbol = (IMethodSymbol)symbol;

            return true;
        }

        setPropertySymbol = null;

        return false;
    }

    /// <summary>
    /// Produces a new <see cref="Stack{T}"/> instance to pool.
    /// </summary>
    /// <returns>The resulting <see cref="Stack{T}"/> instance to use.</returns>
    private static Stack<byte[]> CreatePropertyFlagsStack()
    {
        static IEnumerable<byte[]> EnumerateFlags()
        {
            for (int i = 0; i < NumberOfPooledFlagsPerStack; i++)
            {
                yield return new byte[2];
            }
        }

        return new(EnumerateFlags());
    }
}