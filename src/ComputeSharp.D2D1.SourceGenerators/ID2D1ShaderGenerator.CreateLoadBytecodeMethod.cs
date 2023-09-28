using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
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
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>LoadBytecode</c> method.
    /// </summary>
    internal static partial class LoadBytecode
    {
        /// <summary>
        /// Extracts the requested shader profile for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The requested shader profile to use to compile the shader, if present.</returns>
        public static D2D1ShaderProfile? GetRequestedShaderProfile(INamedTypeSymbol structDeclarationSymbol)
        {
            if (structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute", out AttributeData? attributeData))
            {
                return (D2D1ShaderProfile)attributeData.ConstructorArguments[0].Value!;
            }

            if (structDeclarationSymbol.ContainingAssembly.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DShaderProfileAttribute", out attributeData))
            {
                return (D2D1ShaderProfile)attributeData.ConstructorArguments[0].Value!;
            }

            return null;
        }

        /// <summary>
        /// Extracts the requested compile options for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The requested compile options to use to compile the shader, if present.</returns>
        public static D2D1CompileOptions? GetRequestedCompileOptions(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, INamedTypeSymbol structDeclarationSymbol)
        {
            if (structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out AttributeData? attributeData))
            {
                D2D1CompileOptions options = (D2D1CompileOptions)attributeData.ConstructorArguments[0].Value!;

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

            if (structDeclarationSymbol.ContainingAssembly.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DCompileOptionsAttribute", out attributeData))
            {
                // No need to validate against PackMatrixColumnMajor as that's checked separately
                return (D2D1CompileOptions)attributeData.ConstructorArguments[0].Value! | D2D1CompileOptions.PackMatrixRowMajor;
            }

            return null;
        }

        /// <summary>
        /// Gets the effective shader profile to use.
        /// </summary>
        /// <param name="shaderProfile">The requested shader profile.</param>
        /// <returns>The effective shader profile.</returns>
        public static D2D1ShaderProfile GetEffectiveShaderProfile(D2D1ShaderProfile? shaderProfile)
        {
            // The effective shader profile is either be the requested one, or the default value (which maps to PS5.0)
            return shaderProfile ?? D2D1ShaderProfile.PixelShader50;
        }

        /// <summary>
        /// Gets the effective compile options to use.
        /// </summary>
        /// <param name="compileOptions">The requested compile options.</param>
        /// <param name="isLinkingSupported">Whether the input shader supports linking.</param>
        /// <returns>The effective compile options.</returns>
        public static D2D1CompileOptions GetEffectiveCompileOptions(D2D1CompileOptions? compileOptions, bool isLinkingSupported)
        {
            // If an explicit set of compile options is provided, use that directly. Otherwise, use the default
            // options plus the option to enable linking only if the shader can potentially support it.
            return compileOptions ?? (D2D1CompileOptions.Default | (isLinkingSupported ? D2D1CompileOptions.EnableLinking : 0));
        }

        /// <summary>
        /// Extracts the metadata definition for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <returns>Whether the shader only has simple inputs.</returns>
        public static bool IsSimpleInputShader(INamedTypeSymbol structDeclarationSymbol, int inputCount)
        {
            // If there are no inputs, the shader is as if only had simple inputs
            if (inputCount == 0)
            {
                return true;
            }

            // Build a map of all simple inputs (unmarked inputs default to being complex)
            bool[] simpleInputsMap = new bool[inputCount];

            foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
            {
                switch (attributeData.AttributeClass?.GetFullyQualifiedMetadataName())
                {
                    // Only retrieve indices of simple inputs that are in range. If an input is out of
                    // range, the diagnostic for it will already be emitted by a previous generator step.
                    case "ComputeSharp.D2D1.D2DInputSimpleAttribute"
                    when attributeData.ConstructorArguments[0].Value is int index && index < inputCount:
                        simpleInputsMap[index] = true;
                        break;
                }
            }

            return simpleInputsMap.All(static x => x);
        }

        /// <summary>
        /// Gets the <see cref="HlslBytecodeInfo"/> instance for the input shader info.
        /// </summary>
        /// <param name="hlslSource">The input HLSL source code.</param>
        /// <param name="requestedShaderProfile">The requested shader profile, if available.</param>
        /// <param name="requestedCompileOptions">The requested compile options, if available.</param>
        /// <param name="effectiveShaderProfile">The effective shader profile.</param>
        /// <param name="effectiveCompileOptions">The effective compile options.</param>
        /// <param name="hasErrors">Whether any errors were produced when analyzing the shader.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns></returns>
        public static unsafe HlslBytecodeInfo GetInfo(
            string hlslSource,
            D2D1ShaderProfile? requestedShaderProfile,
            D2D1CompileOptions? requestedCompileOptions,
            D2D1ShaderProfile effectiveShaderProfile,
            D2D1CompileOptions effectiveCompileOptions,
            bool hasErrors,
            CancellationToken token)
        {
            // No embedded shader was requested, or there were some errors earlier in the pipeline.
            // In this case, skip the compilation, as diagnostic will be emitted for those anyway.
            // Compiling would just add overhead and result in more errors, as the HLSL would be invalid.
            // We also skip compilation if no shader profile has been requested (we never just assume one).
            if (hasErrors || requestedShaderProfile is null)
            {
                return HlslBytecodeInfo.Missing.Instance;
            }

            try
            {
                token.ThrowIfCancellationRequested();

                // Compile the shader bytecode using the effective parameters
                using ComPtr<ID3DBlob> dxcBlobBytecode = D3DCompiler.Compile(
                    hlslSource.AsSpan(),
                    effectiveShaderProfile,
                    effectiveCompileOptions);

                token.ThrowIfCancellationRequested();

                byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                ImmutableArray<byte> bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);

                return new HlslBytecodeInfo.Success(bytecode);
            }
            catch (Win32Exception e)
            {
                return new HlslBytecodeInfo.Win32Error(D3DCompiler.PrettifyFxcErrorMessage(e.Message));
            }
            catch (FxcCompilationException e)
            {
                return new HlslBytecodeInfo.FxcError(D3DCompiler.PrettifyFxcErrorMessage(e.Message));
            }
        }
    }
}