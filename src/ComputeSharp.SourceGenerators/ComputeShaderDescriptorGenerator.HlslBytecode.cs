using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using ComputeSharp.Shaders.Translation;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

#pragma warning disable RS1035

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
        /// Indicates whether the required <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries have been loaded.
        /// </summary>
        private static volatile bool areDxcLibrariesLoaded;

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
                    // Try to load dxcompiler.dll and dxil.dll
                    LoadNativeDxcLibraries();

                    token.ThrowIfCancellationRequested();

                    // Compile the shader bytecode
                    using ComPtr<IDxcBlob> dxcBlobBytecode = ShaderCompiler.Instance.Compile(key.HlslSource.AsSpan());

                    token.ThrowIfCancellationRequested();

                    byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                    int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                    byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                    ImmutableArray<byte> bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);

                    return new HlslBytecodeInfo.Success(bytecode);
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
                    new object[] { structDeclarationSymbol, win32Error.HResult, win32Error.Message });
            }
            else if (info is HlslBytecodeInfo.CompilerError dxcError)
            {
                diagnostic = DiagnosticInfo.Create(
                    EmbeddedBytecodeFailedWithDxcCompilationException,
                    structDeclarationSymbol,
                    new object[] { structDeclarationSymbol, dxcError.Message });
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

        /// <summary>
        /// Extracts and loads the <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown if the CPU architecture is not supported.</exception>
        /// <exception cref="Win32Exception">Thrown if a library fails to load.</exception>
        private static void LoadNativeDxcLibraries()
        {
            // Extracts a specified native library for a given runtime identifier
            static string ExtractLibrary(string folder, string rid, string name)
            {
                string sourceFilename = $"ComputeSharp.SourceGenerators.ComputeSharp.Libraries.{rid}.{name}.dll";
                string targetFilename = Path.Combine(folder, rid, $"{name}.dll");

                _ = Directory.CreateDirectory(Path.GetDirectoryName(targetFilename));

                using Stream sourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sourceFilename);

                try
                {
                    using Stream destinationStream = File.Open(targetFilename, FileMode.CreateNew, FileAccess.Write);

                    sourceStream.CopyTo(destinationStream);
                }
                catch (IOException)
                {
                }

                return targetFilename;
            }

            // Loads a target native library
            static unsafe void LoadLibrary(string filename)
            {
                [DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
                static extern void* LoadLibraryW(ushort* lpLibFileName);

                fixed (char* p = filename)
                {
                    if (LoadLibraryW((ushort*)p) is null)
                    {
                        int hresult = Marshal.GetLastWin32Error();

                        throw new Win32Exception(hresult, $"Failed to load {Path.GetFileName(filename)}.");
                    }
                }
            }

            if (!areDxcLibrariesLoaded)
            {
                string rid = RuntimeInformation.ProcessArchitecture switch
                {
                    Architecture.X64 => "x64",
                    Architecture.Arm64 => "arm64",
                    _ => throw new NotSupportedException("Invalid process architecture")
                };

                string folder = Path.Combine(Path.GetTempPath(), "ComputeSharp.SourceGenerators", Path.GetRandomFileName());

                LoadLibrary(ExtractLibrary(folder, rid, "dxil"));
                LoadLibrary(ExtractLibrary(folder, rid, "dxcompiler"));

                areDxcLibrariesLoaded = true;
            }
        }
    }
}