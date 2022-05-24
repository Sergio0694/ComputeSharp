using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using ComputeSharp.D2D1.Exceptions;
using ComputeSharp.D2D1.Shaders.Translation;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static ComputeSharp.D2D1.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>LoadBytecode</c> method.
    /// </summary>
    private static partial class LoadBytecode
    {
        /// <summary>
        /// Extracts the shader profile for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The shader profile to use to compile the shader, if present.</returns>
        public static D2D1ShaderProfile? GetShaderProfile(INamedTypeSymbol structDeclarationSymbol)
        {
            if (structDeclarationSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DEmbeddedBytecodeAttribute", out AttributeData? attributeData))
            {
                return (D2D1ShaderProfile)attributeData!.ConstructorArguments[0].Value!;
            }

            if (structDeclarationSymbol.ContainingAssembly.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DEmbeddedBytecodeAttribute", out attributeData))
            {
                return (D2D1ShaderProfile)attributeData!.ConstructorArguments[0].Value!;
            }

            return null;
        }

        /// <summary>
        /// Extracts the compile options for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The compile options to use to compile the shader, if present.</returns>
        public static D2D1CompileOptions? GetCompileOptions(ImmutableArray<Diagnostic>.Builder diagnostics, INamedTypeSymbol structDeclarationSymbol)
        {
            if (structDeclarationSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out AttributeData? attributeData))
            {
                D2D1CompileOptions options = (D2D1CompileOptions)attributeData!.ConstructorArguments[0].Value!;

                if ((options & D2D1CompileOptions.PackMatrixColumnMajor) != 0)
                {
                    diagnostics.Add(
                        InvalidPackMatrixColumnMajorOption,
                        structDeclarationSymbol,
                        structDeclarationSymbol);
                }

                // PackMatrixRowMajor is always automatically enabled
                return options | D2D1CompileOptions.PackMatrixRowMajor;
            }

            if (structDeclarationSymbol.ContainingAssembly.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out attributeData))
            {
                // No need to validate against PackMatrixColumnMajor as that's checked separately
                return (D2D1CompileOptions)attributeData!.ConstructorArguments[0].Value! | D2D1CompileOptions.PackMatrixRowMajor;
            }

            return null;
        }

        /// <summary>
        /// Gets an <see cref="IncrementalValuesProvider{TValues}"/> instance producing <see cref="Diagnostic"/>-s for invalid assembly-level <c>[D2D1CompileOptions]</c> attributes.
        /// </summary>
        /// <param name="syntaxProvider">The input <see cref="SyntaxValueProvider"/> instance to use to produce the diagnostics.</param>
        /// <returns>The diagnostic for the attribute, if invalid.</returns>
        public static IncrementalValuesProvider<Diagnostic> GetAssemblyLevelCompileOptionsDiagnostics(SyntaxValueProvider syntaxProvider)
        {
            // In order to emit diagnostics for [D2D1CompileOptions] attributes at the assembly level, the following is needed:
            //   - The type symbol for the attribute, to be able to check it's actually [D2D1CompileOptions].
            //   - The syntax node representing the attribute targeting the assembly, to get a location.
            //   - The input D2D1CompileOptions value, which can be retrieved as a constant argument to the attribute.
            //
            // The first step to do this is to find all AttributeListSyntax values and filter those targeting the assembly.
            static bool IsAssemblyTargetingAttributeNode(SyntaxNode node, CancellationToken token)
            {
                return node is AttributeListSyntax attributeList && attributeList.Target?.Identifier.IsKind(SyntaxKind.AssemblyKeyword) == true;
            }

            // Helper that detects invalid [D2D1CompileOptions] uses and produces a diagnostic
            static Diagnostic? TryGetDiagnosticForAssemblyLevelCompileOptions(GeneratorSyntaxContext context, CancellationToken token)
            {
                Location? location = null;
                D2D1CompileOptions? options = null;

                // The input is an AttributeListSyntax object, so first traverse all its containing attributes
                foreach (AttributeSyntax attributeSyntax in ((AttributeListSyntax)context.Node).Attributes)
                {
                    // Match the symbol (retrieved from the constructor symbol) to check it's a [D2D1CompileOptions] attribute
                    if (context.SemanticModel.GetSymbolInfo(attributeSyntax, token).Symbol is IMethodSymbol attributeConstructorSymbol &&
                        attributeConstructorSymbol.ContainingType.HasFullyQualifiedName("ComputeSharp.D2D1.D2DCompileOptionsAttribute"))
                    {
                        // Get the value from the expression (get the argument expression and the constant value from there)
                        if (attributeSyntax.ArgumentList?.Arguments.FirstOrDefault() is AttributeArgumentSyntax argumentSyntax &&
                            context.SemanticModel.GetConstantValue(argumentSyntax.Expression, token) is { HasValue: true, Value: int rawOptions })
                        {
                            options = (D2D1CompileOptions)rawOptions;
                        }

                        // Also get the location from the attribute syntax node
                        location = attributeSyntax.GetLocation();

                        break;
                    }
                }

                // Check that some options were actually found, and that they were incorrect.
                // The attribute can't be repeated, so checking for multiple values isn't needed.
                if (options is not null &&
                    (options & D2D1CompileOptions.PackMatrixColumnMajor) != 0)
                {
                    IAssemblySymbol assemblySymbol = context.SemanticModel.Compilation.Assembly;

                    // Emit the diagnostic targeting the assembly (instead of a shader type)
                    return Diagnostic.Create(
                        InvalidPackMatrixColumnMajorOption,
                        location,
                        assemblySymbol);
                }

                return null;
            }

            // Create the incremental collection and only retrieve non null items
            return
                syntaxProvider
                .CreateSyntaxProvider(IsAssemblyTargetingAttributeNode, TryGetDiagnosticForAssemblyLevelCompileOptions)
                .Where(static diagnostic => diagnostic is not null)!;
        }

        /// <summary>
        /// Extracts the metadata definition for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <returns>Whether the shader only has simple inputs.</returns>
        public static bool IsSimpleInputShader(INamedTypeSymbol structDeclarationSymbol, int inputCount)
        {
            // Build a map of all simple inputs (unmarked inputs default to being complex)
            bool[] simpleInputsMap = new bool[inputCount];

            foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
            {
                switch (attributeData.AttributeClass?.GetFullMetadataName())
                {
                    case "ComputeSharp.D2D1.D2DInputSimpleAttribute":
                        simpleInputsMap[(int)attributeData.ConstructorArguments[0].Value!] = true;
                        break;
                }
            }

            return simpleInputsMap.All(static x => x);
        }

        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
        /// </summary>
        /// <param name="sourceInfo">The source info for the shader to compile.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <param name="options">The effective compile options used to create the shader bytecode.</param>
        /// <param name="diagnostic">The resulting diagnostic from the processing operation, if any.</param>
        /// <returns>The <see cref="ImmutableArray{T}"/> instance with the compiled shader bytecode.</returns>
        public static unsafe ImmutableArray<byte> GetBytecode(
            HlslShaderSourceInfo sourceInfo,
            CancellationToken token,
            out D2D1CompileOptions options,
            out DiagnosticInfo? diagnostic)
        {
            ImmutableArray<byte> bytecode = ImmutableArray<byte>.Empty;

            // No embedded shader was requested, or there were some errors earlier in the pipeline.
            // In this case, skip the compilation, as diagnostic will be emitted for those anyway.
            // Compiling would just add overhead and result in more errors, as the HLSL would be invalid.
            if (sourceInfo is { HasErrors: true } or { ShaderProfile: null })
            {
                options = default;
                diagnostic = null;

                goto End;
            }

            try
            {
                token.ThrowIfCancellationRequested();

                // If an explicit set of compile options is provided, use that directly. Otherwise, use the default
                // options plus the option to enable linking only if the shader can potentially support it.
                options = sourceInfo.CompileOptions ?? (D2D1CompileOptions.Default | (sourceInfo.IsLinkingSupported ? D2D1CompileOptions.EnableLinking : 0));

                // Compile the shader bytecode using the requested parameters
                using ComPtr<ID3DBlob> dxcBlobBytecode = D3DCompiler.Compile(
                    sourceInfo.HlslSource.AsSpan(),
                    sourceInfo.ShaderProfile.Value,
                    options);

                token.ThrowIfCancellationRequested();

                byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);
                diagnostic = null;
            }
            catch (Win32Exception e)
            {
                options = default;
                diagnostic = new DiagnosticInfo(EmbeddedBytecodeFailedWithWin32Exception, e.HResult, FixupExceptionMessage(e.Message));
            }
            catch (FxcCompilationException e)
            {
                options = default;
                diagnostic = new DiagnosticInfo(EmbeddedBytecodeFailedWithDxcCompilationException, FixupExceptionMessage(e.Message));
            }

            End:
            return bytecode;
        }

        /// <summary>
        /// Fixes up an exception message to improve the way it's displayed in VS.
        /// </summary>
        /// <param name="message">The input exception message.</param>
        /// <returns>The updated exception message.</returns>
        private static string FixupExceptionMessage(string message)
        {
            // Add square brackets around error headers
            message = Regex.Replace(message, @"((?:error|warning) \w+):", static m => $"[{m.Groups[1].Value}]:");

            return message.NormalizeToSingleLine();
        }
    }
}
