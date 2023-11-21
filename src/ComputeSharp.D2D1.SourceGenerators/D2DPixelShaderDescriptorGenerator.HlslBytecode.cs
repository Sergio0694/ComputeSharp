using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using ComputeSharp.D2D1.Shaders.Translation;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the HLSL bytecode properties.
    /// </summary>
    internal static partial class HlslBytecode
    {
        /// <summary>
        /// The shared cache of <see cref="HlslBytecodeInfo"/> values.
        /// </summary>
        private static readonly DynamicCache<HlslBytecodeInfoKey, HlslBytecodeInfo> HlslBytecodeCache = new();

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
        /// <param name="isCompilationEnabled">Whether compilation should be performed with the input profile.</param>
        /// <returns>The effective shader profile.</returns>
        public static D2D1ShaderProfile GetEffectiveShaderProfile(D2D1ShaderProfile? shaderProfile, out bool isCompilationEnabled)
        {
            // Compilation is only enabled if the user explicitly selected a shader profile
            isCompilationEnabled = shaderProfile is not null;

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
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <returns>Whether the shader only has simple inputs.</returns>
        public static bool IsSimpleInputShader(
            Compilation compilation,
            INamedTypeSymbol structDeclarationSymbol,
            int inputCount)
        {
            return IsSimpleInputShader(
                structDeclarationSymbol,
                compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DInputSimpleAttribute")!,
                inputCount);
        }

        /// <summary>
        /// Extracts the metadata definition for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="d2DInputSimpleSymbolAttributeSymbol">The symbol for the <c>[D2DInputSimple]</c> attribute.</param>
        /// <param name="inputCount">The number of inputs for the shader.</param>
        /// <returns>Whether the shader only has simple inputs.</returns>
        public static bool IsSimpleInputShader(
            INamedTypeSymbol structDeclarationSymbol,
            INamedTypeSymbol d2DInputSimpleSymbolAttributeSymbol,
            int inputCount)
        {
            // We cannot trust the input count to be valid at this point (it may be invalid and
            // with diagnostic already emitted for it). So first, just clamp it in the right range.
            inputCount = Math.Max(0, Math.Min(8, inputCount));

            // If there are no inputs, the shader is as if only had simple inputs
            if (inputCount == 0)
            {
                return true;
            }

            // Build a map of all simple inputs (unmarked inputs default to being complex).
            // We can never have more than 8 inputs, and if there are it means the shader is
            // not valid. Just ignore them, and the generator will emit a separate diagnostic.
            Span<bool> simpleInputsMap = stackalloc bool[8];

            // We first start with all inputs marked as complex (ie. not simple)
            simpleInputsMap.Clear();

            foreach (AttributeData attributeData in structDeclarationSymbol.GetAttributes())
            {
                // Only retrieve indices of simple inputs that are in range. If an input is out of
                // range, the diagnostic for it will already be emitted by a previous generator step.
                if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, d2DInputSimpleSymbolAttributeSymbol) &&
                    attributeData.ConstructorArguments is [{ Value: >= 0 and < 8 and int index }])
                {
                    simpleInputsMap[index] = true;
                }
            }

            bool isSimpleInputShader = true;

            // Validate all inputs in our range (filtered by the allowed one)
            foreach (bool isSimpleInput in simpleInputsMap[..inputCount])
            {
                isSimpleInputShader &= isSimpleInput;
            }

            return isSimpleInputShader;
        }

        /// <summary>
        /// Gets the <see cref="HlslBytecodeInfo"/> instance for the input shader info.
        /// </summary>
        /// <param name="key">The <see cref="HlslBytecodeInfoKey"/> instance for the shader to compile.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns>The <see cref="HlslBytecodeInfo"/> instance for the current shader.</returns>
        public static HlslBytecodeInfo GetInfo(ref HlslBytecodeInfoKey key, CancellationToken token)
        {
            static unsafe HlslBytecodeInfo GetInfo(HlslBytecodeInfoKey key, CancellationToken token)
            {
                // Check if the compilation is not enabled (eg. if there's been errors earlier in the pipeline).
                // In this case, skip the compilation, as diagnostic will be emitted for those anyway.
                // Compiling would just add overhead and result in more errors, as the HLSL would be invalid.
                if (!key.IsCompilationEnabled)
                {
                    return HlslBytecodeInfo.Missing.Instance;
                }

                try
                {
                    token.ThrowIfCancellationRequested();

                    // Compile the shader bytecode using the effective parameters
                    using ComPtr<ID3DBlob> dxcBlobBytecode = D3DCompiler.Compile(
                        key.HlslSource.AsSpan(),
                        key.ShaderProfile,
                        key.CompileOptions);

                    token.ThrowIfCancellationRequested();

                    byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                    int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                    byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                    ImmutableArray<byte> bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);

                    return new HlslBytecodeInfo.Success(bytecode);
                }
                catch (Win32Exception e)
                {
                    return new HlslBytecodeInfo.Win32Error(e.NativeErrorCode, D3DCompiler.PrettifyFxcErrorMessage(e.Message));
                }
                catch (FxcCompilationException e)
                {
                    return new HlslBytecodeInfo.CompilerError(D3DCompiler.PrettifyFxcErrorMessage(e.Message));
                }
            }

            // Get or create the HLSL bytecode compilation result for the input key. The dynamic cache
            // will take care of retrieving an existing cached value if the same shader has been compiled
            // already with the same parameters. After this call, callers must use the updated key value.
            return HlslBytecodeCache.GetOrCreate(ref key, GetInfo, token);
        }

        /// <summary>
        /// Gets any diagnostics from a processed <see cref="HlslBytecodeInfo"/> instance.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="info">The source <see cref="HlslBytecodeInfo"/> instance.</param>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        public static void GetInfoDiagnostics(
            INamedTypeSymbol structDeclarationSymbol,
            HlslBytecodeInfo info,
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics)
        {
            DiagnosticInfo? diagnostic = null;

            if (info is HlslBytecodeInfo.Win32Error win32Error)
            {
                diagnostic = DiagnosticInfo.Create(
                    EmbeddedBytecodeFailedWithWin32Exception,
                    structDeclarationSymbol,
                    [structDeclarationSymbol, win32Error.HResult, win32Error.Message]);
            }
            else if (info is HlslBytecodeInfo.CompilerError fxcError)
            {
                diagnostic = DiagnosticInfo.Create(
                    EmbeddedBytecodeFailedWithFxcCompilationException,
                    structDeclarationSymbol,
                    [structDeclarationSymbol, fxcError.Message]);
            }

            if (diagnostic is not null)
            {
                diagnostics.Add(diagnostic);
            }
        }
    }
}