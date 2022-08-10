using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ComputeSharp.D2D1.Exceptions;
using ComputeSharp.D2D1.Shaders.Translation;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

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
        /// Extracts the HLSL source from a method with the <see cref="D2DPixelShaderSourceAttribute"/> annotation.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="methodSymbol">The input <see cref="IMethodSymbol"/> instance to process.</param>
        /// <returns>The HLSL source to compile, if present.</returns>
        public static string? GetHlslSource(ImmutableArray<Diagnostic>.Builder diagnostics, IMethodSymbol methodSymbol)
        {
            _ = methodSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DPixelShaderSourceAttribute", out AttributeData? attributeData);

            if (!attributeData!.TryGetConstructorArgument(0, out string? hlslSource))
            {
                // TODO: no string diagnostic
            }

            return hlslSource;
        }

        /// <summary>
        /// Extracts the shader profile for a target method, if present.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="methodSymbol">The input <see cref="IMethodSymbol"/> instance to process.</param>
        /// <returns>The shader profile to use to compile the shader, if present.</returns>
        public static D2D1ShaderProfile? GetShaderProfile(ImmutableArray<Diagnostic>.Builder diagnostics, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DEmbeddedBytecodeAttribute", out AttributeData? attributeData))
            {
                return (D2D1ShaderProfile)attributeData!.ConstructorArguments[0].Value!;
            }

            if (methodSymbol.ContainingAssembly.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DEmbeddedBytecodeAttribute", out attributeData))
            {
                return (D2D1ShaderProfile)attributeData!.ConstructorArguments[0].Value!;
            }

            // TODO: diagnostics

            return null;
        }

        /// <summary>
        /// Extracts the compile options for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="Diagnostic"/> instances.</param>
        /// <param name="methodSymbol">The input <see cref="IMethodSymbol"/> instance to process.</param>
        /// <returns>The compile options to use to compile the shader, if present.</returns>
        public static D2D1CompileOptions? GetCompileOptions(ImmutableArray<Diagnostic>.Builder diagnostics, IMethodSymbol methodSymbol)
        {
            if (methodSymbol.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out AttributeData? attributeData))
            {
                return (D2D1CompileOptions)attributeData!.ConstructorArguments[0].Value!;
            }

            if (methodSymbol.ContainingAssembly.TryGetAttributeWithFullMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out attributeData))
            {
                return (D2D1CompileOptions)attributeData!.ConstructorArguments[0].Value!;
            }

            // TODO: diagnostics

            return null;
        }

        /// <summary>
        /// Gets an <see cref="ImmutableArray{T}"/> instance with the compiled bytecode for the current shader.
        /// </summary>
        /// <param name="sourceInfo">The source info for the shader to compile.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <param name="diagnostic">The resulting diagnostic from the processing operation, if any.</param>
        /// <returns>The <see cref="ImmutableArray{T}"/> instance with the compiled shader bytecode.</returns>
        public static unsafe ImmutableArray<byte> GetBytecode(HlslShaderSourceInfo sourceInfo, CancellationToken token, out DiagnosticInfo? diagnostic)
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
                    sourceInfo.ShaderProfile!.Value,
                    sourceInfo.CompileOptions!.Value);

                token.ThrowIfCancellationRequested();

                byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);
                diagnostic = null;
            }
            catch (Win32Exception e)
            {
                diagnostic = new DiagnosticInfo(
                    null!, // TODO
                    e.HResult,
                    ID2D1ShaderGenerator.LoadBytecode.FixupExceptionMessage(e.Message));
            }
            catch (FxcCompilationException e)
            {
                diagnostic = new DiagnosticInfo(
                    null!, // TODO
                    ID2D1ShaderGenerator.LoadBytecode.FixupExceptionMessage(e.Message));
            }

            End:
            return bytecode;
        }
    }
}
