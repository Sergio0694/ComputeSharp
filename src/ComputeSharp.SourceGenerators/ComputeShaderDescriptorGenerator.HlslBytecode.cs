using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGenerators.Dxc;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D.Dxc;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>HlslBytecode</c> property.
    /// </summary>
    private static partial class HlslBytecode
    {
        /// <summary>
        /// The shared cache of <see cref="HlslBytecodeInfo"/> values.
        /// </summary>
        private static readonly DynamicCache<HlslBytecodeInfoKey, HlslBytecodeInfo> HlslBytecodeCache = new();

        /// <summary>
        /// Gets the <see cref="HlslBytecodeInfo"/> instance for the input shader info.
        /// </summary>
        /// <param name="key">The <see cref="HlslBytecodeInfoKey"/> instance for the shader to compile.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <returns>The <see cref="HlslBytecodeInfo"/> instance for the current shader.</returns>
        public static unsafe HlslBytecodeInfo GetBytecode(ref HlslBytecodeInfoKey key, CancellationToken token)
        {
            static unsafe HlslBytecodeInfo GetInfo(HlslBytecodeInfoKey key, CancellationToken token)
            {
                // Skip even attempting to compile if compilation is disabled (see comments in D2D1 generator)
                if (!key.IsCompilationEnabled)
                {
                    return HlslBytecodeInfo.Missing.Instance;
                }

                try
                {
                    token.ThrowIfCancellationRequested();

                    // Try to load dxcompiler.dll and dxil.dll
                    DxcLibraryLoader.LoadNativeDxcLibraries();

                    token.ThrowIfCancellationRequested();

                    // Compile the shader bytecode
                    using ComPtr<IDxcBlob> dxcBlob = DxcShaderCompiler.Instance.Compile(
                        key.HlslSource.AsSpan(),
                        key.CompileOptions,
                        token);

                    token.ThrowIfCancellationRequested();

                    // Check whether double precision operations are required
                    bool requiresDoublePrecisionSupport = DxcShaderCompiler.Instance.IsDoublePrecisionSupportRequired(dxcBlob.Get());

                    token.ThrowIfCancellationRequested();

                    byte* buffer = (byte*)dxcBlob.Get()->GetBufferPointer();
                    int length = checked((int)dxcBlob.Get()->GetBufferSize());

                    byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                    ImmutableArray<byte> bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);

                    return new HlslBytecodeInfo.Success(bytecode, requiresDoublePrecisionSupport);
                }
                catch (Win32Exception e)
                {
                    return new HlslBytecodeInfo.Win32Error(e.NativeErrorCode, DxcShaderCompiler.FixupExceptionMessage(e.Message));
                }
                catch (DxcCompilationException e)
                {
                    return new HlslBytecodeInfo.CompilerError(DxcShaderCompiler.FixupExceptionMessage(e.Message));
                }
            }

            return HlslBytecodeCache.GetOrCreate(ref key, GetInfo, token);
        }

        /// <summary>
        /// Extracts the compile options for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <returns>The requested compile options to use to compile the shader, if present.</returns>
        public static CompileOptions GetCompileOptions(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, INamedTypeSymbol structDeclarationSymbol)
        {
            // If a [CompileOptions] annotation is present, return the explicit options
            if (structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.CompileOptionsAttribute", out AttributeData? attributeData) ||
                structDeclarationSymbol.ContainingAssembly.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.CompileOptionsAttribute", out attributeData))
            {
                return (CompileOptions)attributeData.ConstructorArguments[0].Value!;
            }

            return CompileOptions.Default;
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
                    HlslBytecodeFailedWithWin32Exception,
                    structDeclarationSymbol,
                    structDeclarationSymbol,
                    win32Error.HResult,
                    win32Error.Message);
            }
            else if (info is HlslBytecodeInfo.CompilerError dxcError)
            {
                diagnostic = DiagnosticInfo.Create(
                    HlslBytecodeFailedWithCompilationException,
                    structDeclarationSymbol,
                    structDeclarationSymbol,
                    dxcError.Message);
            }

            if (diagnostic is not null)
            {
                diagnostics.Add(diagnostic);
            }
        }

        /// <summary>
        /// Gets the diagnostics for when double precision support is configured incorrectly.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="info">The source <see cref="HlslBytecodeInfo"/> instance.</param>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        public static void GetDoublePrecisionSupportDiagnostics(
            INamedTypeSymbol structDeclarationSymbol,
            HlslBytecodeInfo info,
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics)
        {
            // If we have no compiled HLSL bytecode, there is nothing more to do
            if (info is not HlslBytecodeInfo.Success success)
            {
                return;
            }

            bool hasD2DRequiresDoublePrecisionSupportAttribute = structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName(
                "ComputeSharp.RequiresDoublePrecisionSupportAttribute",
                out AttributeData? attributeData);

            // Check the two cases where diagnostics are necessary (same as the D2D generator)
            if (!hasD2DRequiresDoublePrecisionSupportAttribute && success.RequiresDoublePrecisionSupport)
            {
                diagnostics.Add(DiagnosticInfo.Create(
                    MissingRequiresDoublePrecisionSupportAttribute,
                    structDeclarationSymbol,
                    structDeclarationSymbol));
            }
            else if (hasD2DRequiresDoublePrecisionSupportAttribute && !success.RequiresDoublePrecisionSupport)
            {
                diagnostics.Add(DiagnosticInfo.Create(
                    UnnecessaryRequiresDoublePrecisionSupportAttribute,
                    attributeData!.GetLocation(),
                    structDeclarationSymbol));
            }
        }
    }
}