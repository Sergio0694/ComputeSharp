using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGenerators.Dxc;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
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
                    byte[] bytecode = DxcShaderCompiler.Instance.Compile(key.HlslSource.AsSpan(), key.CompileOptions, token);

                    token.ThrowIfCancellationRequested();

                    return new HlslBytecodeInfo.Success(Unsafe.As<byte[], ImmutableArray<byte>>(ref bytecode));
                }
                catch (Win32Exception e)
                {
                    return new HlslBytecodeInfo.Win32Error(e.NativeErrorCode, FixupExceptionMessage(e.Message));
                }
                catch (DxcCompilationException e)
                {
                    return new HlslBytecodeInfo.CompilerError(FixupExceptionMessage(e.Message));
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
                    EmbeddedBytecodeFailedWithWin32Exception,
                    structDeclarationSymbol,
                    [structDeclarationSymbol, win32Error.HResult, win32Error.Message]);
            }
            else if (info is HlslBytecodeInfo.CompilerError dxcError)
            {
                diagnostic = DiagnosticInfo.Create(
                    EmbeddedBytecodeFailedWithDxcCompilationException,
                    structDeclarationSymbol,
                    [structDeclarationSymbol, dxcError.Message]);
            }

            if (diagnostic is not null)
            {
                diagnostics.Add(diagnostic);
            }
        }

        /// <summary>
        /// Fixes up an exception message to improve the way it's displayed in VS.
        /// </summary>
        /// <param name="message">The input exception message.</param>
        /// <returns>The updated exception message.</returns>
        private static string FixupExceptionMessage(string message)
        {
            // Add square brackets around error headers
            message = Regex.Replace(message, @"^(error|warning):", static m => $"[{m.Groups[1].Value}]:", RegexOptions.Multiline);

            // Remove lines with notes
            message = Regex.Replace(message, @"^note:.+", string.Empty, RegexOptions.Multiline);

            // Remove syntax error indicators
            message = Regex.Replace(message, @"^ +\^", string.Empty, RegexOptions.Multiline);

            return message.NormalizeToSingleLine();
        }
    }
}