using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using ComputeSharp.D2D1.Exceptions;
using ComputeSharp.D2D1.Shaders.Translation;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
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
        /// Gets an <see cref="ImmutableArray{T}"/> instance with the compiled bytecode for the current shader.
        /// </summary>
        /// <param name="sourceInfo">The source info for the shader to compile.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <param name="diagnostic">The resulting diagnostic from the processing operation, if any.</param>
        /// <returns>The <see cref="ImmutableArray{T}"/> instance with the compiled shader bytecode.</returns>
        public static unsafe ImmutableArray<byte> GetBytecode(HlslShaderMethodSourceInfo sourceInfo, CancellationToken token, out DeferredDiagnosticInfo? diagnostic)
        {
            ImmutableArray<byte> bytecode = ImmutableArray<byte>.Empty;

            // Skip compilation if there are any errors
            if (sourceInfo is { HasErrors: true })
            {
                diagnostic = null;

                goto End;
            }

            try
            {
                token.ThrowIfCancellationRequested();

                // Compile the shader bytecode using the requested parameters
                using ComPtr<ID3DBlob> dxcBlobBytecode = D3DCompiler.Compile(
                    sourceInfo.HlslSource.AsSpan(),
                    sourceInfo.ShaderProfile,
                    sourceInfo.CompileOptions);

                token.ThrowIfCancellationRequested();

                byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);
                diagnostic = null;
            }
            catch (Win32Exception e)
            {
                diagnostic = DeferredDiagnosticInfo.Create(
                    D2DPixelShaderSourceCompilationFailedWithWin32Exception,
                    sourceInfo.MethodName,
                    e.HResult,
                    D3DCompiler.PrettifyFxcErrorMessage(e.Message));
            }
            catch (FxcCompilationException e)
            {
                diagnostic = DeferredDiagnosticInfo.Create(
                    D2DPixelShaderSourceCompilationFailedWithFxcCompilationException,
                    sourceInfo.MethodName,
                    D3DCompiler.PrettifyFxcErrorMessage(e.Message));
            }

            End:
            return bytecode;
        }
    }
}