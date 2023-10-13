using System;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderSourceGenerator
{
    /// <summary>
    /// A container for all the logic for <see cref="D2DPixelShaderSourceGenerator"/>.
    /// </summary>
    private static partial class Execute
    {
        /// <summary>
        /// Validates that the return type of the annotated method is valid and returns the type name.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="methodSymbol">The input <see cref="IMethodSymbol"/> instance to process.</param>
        /// <returns>The HLSL source to compile, if present.</returns>
        public static string? GetInvalidReturnType(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, IMethodSymbol methodSymbol)
        {
            if (!(methodSymbol.ReturnType is INamedTypeSymbol
                {
                    Name: "ReadOnlySpan",
                    ContainingNamespace.Name: "System",
                    IsGenericType: true,
                    TypeParameters.Length: 1
                } returnType && returnType.TypeArguments[0].SpecialType == SpecialType.System_Byte))
            {
                diagnostics.Add(
                    InvalidD2DPixelShaderSourceMethodReturnType,
                    methodSymbol,
                    methodSymbol.Name,
                    methodSymbol.ContainingType,
                    methodSymbol.ReturnType);

                return methodSymbol.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            }

            return null;
        }

        /// <summary>
        /// Extracts the HLSL source from a method with the <see cref="D2DPixelShaderSourceAttribute"/> annotation.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="methodSymbol">The input <see cref="IMethodSymbol"/> instance to process.</param>
        /// <returns>The HLSL source to compile, if present.</returns>
        public static string GetHlslSource(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, IMethodSymbol methodSymbol)
        {
            _ = methodSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DPixelShaderSourceAttribute", out AttributeData? attributeData);

            if (!attributeData!.TryGetConstructorArgument(0, out string? hlslSource))
            {
                diagnostics.Add(
                    InvalidD2DPixelShaderSource,
                    methodSymbol,
                    methodSymbol.Name,
                    methodSymbol.ContainingType);
            }

            return hlslSource ?? "";
        }

        /// <summary>
        /// Extracts the shader profile for a target method, if present.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="methodSymbol">The input <see cref="IMethodSymbol"/> instance to process.</param>
        /// <returns>The shader profile to use to compile the shader, if present.</returns>
        public static D2D1ShaderProfile GetShaderProfile(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute", out AttributeData? attributeData))
            {
                return (D2D1ShaderProfile)attributeData.ConstructorArguments[0].Value!;
            }

            if (methodSymbol.ContainingAssembly.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute", out attributeData))
            {
                return (D2D1ShaderProfile)attributeData.ConstructorArguments[0].Value!;
            }

            diagnostics.Add(
                MissingShaderProfileForD2DPixelShaderSource,
                methodSymbol,
                methodSymbol.Name,
                methodSymbol.ContainingType);

            return D2D1ShaderProfile.PixelShader50;
        }

        /// <summary>
        /// Extracts the compile options for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="methodSymbol">The input <see cref="IMethodSymbol"/> instance to process.</param>
        /// <returns>The compile options to use to compile the shader, if present.</returns>
        public static D2D1CompileOptions GetCompileOptions(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out AttributeData? attributeData))
            {
                return (D2D1CompileOptions)attributeData.ConstructorArguments[0].Value!;
            }

            if (methodSymbol.ContainingAssembly.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out attributeData))
            {
                return (D2D1CompileOptions)attributeData.ConstructorArguments[0].Value!;
            }

            diagnostics.Add(
                MissingCompileOptionsForD2DPixelShaderSource,
                methodSymbol,
                methodSymbol.Name,
                methodSymbol.ContainingType);

            return D2D1CompileOptions.Default;
        }

        /// <summary>
        /// Gets any diagnostics from a processed <see cref="HlslBytecodeInfo"/> instance.
        /// </summary>
        /// <param name="methodSymbol">The input <see cref="IMethodSymbol"/> instance to process.</param>
        /// <param name="info">The source <see cref="HlslBytecodeInfo"/> instance.</param>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        public static void GetInfoDiagnostics(
            IMethodSymbol methodSymbol,
            HlslBytecodeInfo info,
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics)
        {
            DiagnosticInfo? diagnostic = null;

            if (info is HlslBytecodeInfo.Win32Error win32Error)
            {
                diagnostic = DiagnosticInfo.Create(
                    D2DPixelShaderSourceCompilationFailedWithWin32Exception,
                    methodSymbol,
                    new object[] { methodSymbol, methodSymbol.ContainingType, win32Error.HResult, win32Error.Message });
            }
            else if (info is HlslBytecodeInfo.CompilerError fxcError)
            {
                diagnostic = DiagnosticInfo.Create(
                    D2DPixelShaderSourceCompilationFailedWithFxcCompilationException,
                    methodSymbol,
                    new object[] { methodSymbol, methodSymbol.ContainingType, fxcError.Message });
            }

            if (diagnostic is not null)
            {
                diagnostics.Add(diagnostic);
            }
        }

        /// <summary>
        /// Writes the generated method for the compiled HLSL bytecode.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1PixelShaderSourceInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1PixelShaderSourceInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);

            // Write the modifiers, if any
            foreach (uint modifier in info.Modifiers)
            {
                writer.Write(SyntaxFacts.GetText((SyntaxKind)modifier));
                writer.Write(" ");
            }

            // Write the return type
            if (info.InvalidReturnType is string invalidReturnType)
            {
                writer.Write(invalidReturnType);
                writer.Write(" ");
            }
            else
            {
                writer.Write("global::System.ReadOnlySpan<byte> ");
            }

            writer.WriteLine($"{info.MethodName}()");

            // Write the bytecode expression
            using (writer.WriteBlock())
            {
                if (info.HlslInfo is HlslBytecodeInfo.Success success)
                {
                    writer.Write("return new byte[] { ");

                    SyntaxFormattingHelper.WriteByteArrayInitializationExpressions(success.Bytecode.AsSpan(), writer);

                    writer.WriteLine(" };");
                }
                else
                {
                    writer.Write("return default;");
                }
            }
        }
    }
}