using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;
using Windows.Win32;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;
#if D2D1_SOURCE_GENERATOR
using HlslBytecodeInfoKey = ComputeSharp.D2D1.SourceGenerators.Models.HlslBytecodeInfoKey;
using HlslCompilationException = ComputeSharp.D2D1.FxcCompilationException;
using ID3DBlob = Windows.Win32.Graphics.Direct3D.ID3DBlob;
#else
using HlslBytecodeInfoKey = ComputeSharp.SourceGenerators.Models.HlslBytecodeInfoKey;
using HlslCompilationException = ComputeSharp.SourceGenerators.Dxc.DxcCompilationException;
using ID3DBlob = Windows.Win32.Graphics.Direct3D.Dxc.IDxcBlob;
#endif

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <summary>
/// A processor responsible for extracting info about compiled HLSL bytecode.
/// </summary>
internal static partial class HlslBytecodeSyntaxProcessor
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
                using ComPtr<ID3DBlob> d3DBlob = Compile(key, token);

                token.ThrowIfCancellationRequested();

                // Check whether double precision operations are required
                bool requiresDoublePrecisionSupport = IsDoublePrecisionSupportRequired(d3DBlob.Get());

                token.ThrowIfCancellationRequested();

                byte* buffer = (byte*)d3DBlob.Get()->GetBufferPointer();
                int length = checked((int)d3DBlob.Get()->GetBufferSize());

                byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                ImmutableArray<byte> bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);

                return new HlslBytecodeInfo.Success(bytecode, requiresDoublePrecisionSupport);
            }
            catch (Win32Exception e)
            {
                return new HlslBytecodeInfo.Win32Error(e.NativeErrorCode, FixupErrorMessage(e.Message));
            }
            catch (HlslCompilationException e)
            {
                return new HlslBytecodeInfo.CompilerError(FixupErrorMessage(e.Message));
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
                HlslBytecodeFailedWithWin32Exception,
                structDeclarationSymbol,
                structDeclarationSymbol,
                win32Error.HResult,
                win32Error.Message);
        }
        else if (info is HlslBytecodeInfo.CompilerError fxcError)
        {
            diagnostic = DiagnosticInfo.Create(
                HlslBytecodeFailedWithCompilationException,
                structDeclarationSymbol,
                structDeclarationSymbol,
                fxcError.Message);
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

        bool hasRequiresDoublePrecisionSupportAttribute = structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName(
            GetRequiresDoublePrecisionSupportAttributeName(),
            out AttributeData? attributeData);

        // Check the two cases where diagnostics are necessary:
        //   - The shader does not have [[D2D]RequiresDoublePrecisionSupport], but it needs it
        //   - The shader has [[D2D]RequiresDoublePrecisionSupport], but it does not need it
        if (!hasRequiresDoublePrecisionSupportAttribute && success.RequiresDoublePrecisionSupport)
        {
            diagnostics.Add(DiagnosticInfo.Create(
                MissingRequiresDoublePrecisionSupportAttribute,
                structDeclarationSymbol,
                structDeclarationSymbol));
        }
        else if (hasRequiresDoublePrecisionSupportAttribute && !success.RequiresDoublePrecisionSupport)
        {
            diagnostics.Add(DiagnosticInfo.Create(
                UnnecessaryRequiresDoublePrecisionSupportAttribute,
                attributeData!.GetLocation(),
                structDeclarationSymbol));
        }
    }

    /// <summary>
    /// Gets the type name for the attribute to indicate that double precision support is required.
    /// </summary>
    /// <returns>The type name for the attribute to indicate that double precision support is required.</returns>
    private static partial string GetRequiresDoublePrecisionSupportAttributeName();

    /// <summary>
    /// Compiles the input HLSL source into bytecode.
    /// </summary>
    /// <param name="key">The <see cref="HlslBytecodeInfoKey"/> instance for the shader to compile.</param>
    /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
    /// <returns>The resulting HLSL bytecode.</returns>
    private static partial ComPtr<ID3DBlob> Compile(HlslBytecodeInfoKey key, CancellationToken token);

    /// <summary>
    /// Checks whether double precision support is required.
    /// </summary>
    /// <param name="d3DBlob">The input HLSL bytecode to inspect.</param>
    /// <returns>Whether double precision support is required for <paramref name="d3DBlob"/>.</returns>
    private static unsafe partial bool IsDoublePrecisionSupportRequired(ID3DBlob* d3DBlob);

    /// <summary>
    /// Fixes up an exception message to improve the way it's displayed in VS.
    /// </summary>
    /// <param name="message">The input exception message.</param>
    /// <returns>The updated exception message.</returns>
    /// <returns></returns>
    private static partial string FixupErrorMessage(string message);
}