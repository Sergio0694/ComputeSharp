using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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
    /// Compiles the shaders for all input keys in parallel, warming up the shared cache.
    /// After this call, <see cref="GetInfo"/> calls for any of the input keys will be cache hits.
    /// </summary>
    /// <param name="keys">The <see cref="HlslBytecodeInfoKey"/> instances for the shaders to compile.</param>
    /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
    public static void CompileAllInParallel(ImmutableArray<HlslBytecodeInfoKey> keys, CancellationToken token)
    {
        static void Compile(HlslBytecodeInfoKey key, CancellationToken token)
        {
            _ = GetInfo(ref key, token);
        }

        // Skip the parallel dispatch entirely if there are less than two keys to process
        if (keys.Length == 0)
        {
            return;
        }

        if (keys.Length == 1)
        {
            Compile(keys[0], token);

            return;
        }

        try
        {
            // Compile all shaders in parallel (each compilation is independent, and both the shared cache and
            // the native compilers support concurrent use). Duplicate keys are filtered out first: concurrent
            // requests for the same key would be benign (one result would just be discarded), but there is no
            // reason to schedule them at all. The order of compilations does not matter, as the results are
            // only published to the cache here (callers will then retrieve them via cache hits afterwards).
            _ = Parallel.ForEach(
                keys.Distinct(),
                new ParallelOptions { CancellationToken = token },
                key => Compile(key, token));
        }
        catch (AggregateException)
        {
            // If cancellation is requested, normalize to an OperationCanceledException for the incremental
            // driver (a cancellation from the callbacks may be wrapped, depending on interleaving). Other
            // exceptions cannot really occur, as the compilation callback catches all expected exceptions.
            token.ThrowIfCancellationRequested();

            throw;
        }
    }

    /// <summary>
    /// Gets the <see cref="HlslBytecodeDiagnosticsInfo"/> instance for a given shader type.
    /// This captures all info needed to synthesize compile diagnostics after the transform
    /// node has completed (which is required, as symbols cannot be used past that point).
    /// </summary>
    /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
    /// <returns>The <see cref="HlslBytecodeDiagnosticsInfo"/> instance for the current shader.</returns>
    public static HlslBytecodeDiagnosticsInfo GetDiagnosticsInfo(INamedTypeSymbol structDeclarationSymbol)
    {
        bool hasRequiresDoublePrecisionSupportAttribute = structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName(
            GetRequiresDoublePrecisionSupportAttributeName(),
            out AttributeData? attributeData);

        return new HlslBytecodeDiagnosticsInfo(
            TypeName: structDeclarationSymbol.ToString(),
            TypeLocation: LocationInfo.From(structDeclarationSymbol),
            HasRequiresDoublePrecisionSupportAttribute: hasRequiresDoublePrecisionSupportAttribute,
            RequiresDoublePrecisionSupportAttributeLocation: LocationInfo.From(attributeData?.GetLocation()));
    }

    /// <summary>
    /// Gets any diagnostics from a processed <see cref="HlslBytecodeInfo"/> instance.
    /// </summary>
    /// <param name="diagnosticsInfo">The <see cref="HlslBytecodeDiagnosticsInfo"/> instance for the current shader.</param>
    /// <param name="info">The source <see cref="HlslBytecodeInfo"/> instance.</param>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    public static void GetInfoDiagnostics(
        HlslBytecodeDiagnosticsInfo diagnosticsInfo,
        HlslBytecodeInfo info,
        ImmutableArrayBuilder<DiagnosticInfo> diagnostics)
    {
        DiagnosticInfo? diagnostic = null;

        if (info is HlslBytecodeInfo.Win32Error win32Error)
        {
            diagnostic = DiagnosticInfo.Create(
                HlslBytecodeFailedWithWin32Exception,
                diagnosticsInfo.TypeLocation?.ToLocation(),
                diagnosticsInfo.TypeName,
                win32Error.HResult,
                win32Error.Message);
        }
        else if (info is HlslBytecodeInfo.CompilerError fxcError)
        {
            diagnostic = DiagnosticInfo.Create(
                HlslBytecodeFailedWithCompilationException,
                diagnosticsInfo.TypeLocation?.ToLocation(),
                diagnosticsInfo.TypeName,
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
    /// <param name="diagnosticsInfo">The <see cref="HlslBytecodeDiagnosticsInfo"/> instance for the current shader.</param>
    /// <param name="info">The source <see cref="HlslBytecodeInfo"/> instance.</param>
    /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
    public static void GetDoublePrecisionSupportDiagnostics(
        HlslBytecodeDiagnosticsInfo diagnosticsInfo,
        HlslBytecodeInfo info,
        ImmutableArrayBuilder<DiagnosticInfo> diagnostics)
    {
        // If we have no compiled HLSL bytecode, there is nothing more to do
        if (info is not HlslBytecodeInfo.Success success)
        {
            return;
        }

        // Check the two cases where diagnostics are necessary:
        //   - The shader does not have [[D2D]RequiresDoublePrecisionSupport], but it needs it
        //   - The shader has [[D2D]RequiresDoublePrecisionSupport], but it does not need it
        if (!diagnosticsInfo.HasRequiresDoublePrecisionSupportAttribute && success.RequiresDoublePrecisionSupport)
        {
            diagnostics.Add(DiagnosticInfo.Create(
                MissingRequiresDoublePrecisionSupportAttribute,
                diagnosticsInfo.TypeLocation?.ToLocation(),
                diagnosticsInfo.TypeName));
        }
        else if (diagnosticsInfo.HasRequiresDoublePrecisionSupportAttribute && !success.RequiresDoublePrecisionSupport)
        {
            diagnostics.Add(DiagnosticInfo.Create(
                UnnecessaryRequiresDoublePrecisionSupportAttribute,
                (diagnosticsInfo.RequiresDoublePrecisionSupportAttributeLocation ?? diagnosticsInfo.TypeLocation)?.ToLocation(),
                diagnosticsInfo.TypeName));
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