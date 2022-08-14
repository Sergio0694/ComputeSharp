using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using ComputeSharp.Exceptions;
using ComputeSharp.Shaders.Translation;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>LoadBytecode</c> method.
    /// </summary>
    private static partial class LoadBytecode
    {
        /// <summary>
        /// Gets the thread ids values for a given shader type, if available.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="isDynamicShader">Indicates whether or not the shader is dynamic (ie. captures delegates).</param>
        /// <param name="supportsDynamicShaders">Indicates whether or not dynamic shaders are supported.</param>
        /// <returns>The thread ids for the precompiled shader, if available.</returns>
        public static ThreadIdsInfo GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            INamedTypeSymbol structDeclarationSymbol,
            bool isDynamicShader,
            bool supportsDynamicShaders)
        {
            // Try to get the attribute that controls shader precompilation (has to be present when ComputeSharp.Dynamic is not referenced)
            if (!structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName(typeof(EmbeddedBytecodeAttribute).FullName, out AttributeData? attribute))
            {
                // Emit the diagnostics if dynamic shaders are not supported
                if (!supportsDynamicShaders)
                {
                    diagnostics.Add(MissingEmbeddedBytecodeAttributeWhenDynamicShaderCompilationIsNotSupported, structDeclarationSymbol, structDeclarationSymbol);
                }

                return new(true, 0, 0, 0);
            }

            int threadsX;
            int threadsY;
            int threadsZ;

            // Check for a dispatch axis argument first
            if (attribute!.ConstructorArguments.Length == 1)
            {
                int dispatchAxis = (int)attribute.ConstructorArguments[0].Value!;

                (threadsX, threadsY, threadsZ) = (DispatchAxis)dispatchAxis switch
                {
                    DispatchAxis.X => (64, 1, 1),
                    DispatchAxis.Y => (1, 64, 1),
                    DispatchAxis.Z => (1, 1, 64),
                    DispatchAxis.XY => (8, 8, 1),
                    DispatchAxis.XZ => (8, 1, 8),
                    DispatchAxis.YZ => (1, 8, 8),
                    DispatchAxis.XYZ => (4, 4, 4),
                    _ => (0, 0, 0)
                };

                // Validate the dispatch axis argument
                if ((threadsX, threadsY, threadsZ) is (0, 0, 0))
                {
                    diagnostics.Add(InvalidEmbeddedBytecodeDispatchAxis, structDeclarationSymbol, structDeclarationSymbol);

                    return new(true, 0, 0, 0);
                }
            }
            else if (
                attribute.ConstructorArguments.Length != 3 ||
                attribute.ConstructorArguments[0].Value is not int explicitThreadsX ||
                attribute.ConstructorArguments[1].Value is not int explicitThreadsY ||
                attribute.ConstructorArguments[2].Value is not int explicitThreadsZ ||
                explicitThreadsX is < 1 or > 1024 ||
                explicitThreadsY is < 1 or > 1024 ||
                explicitThreadsZ is < 1 or > 64)
            {
                // Failed to validate the thread number arguments
                diagnostics.Add(InvalidEmbeddedBytecodeThreadIds, structDeclarationSymbol, structDeclarationSymbol);

                return new(true, 0, 0, 0);
            }
            else
            {
                threadsX = explicitThreadsX;
                threadsY = explicitThreadsY;
                threadsZ = explicitThreadsZ;
            }

            // If the current shader is dynamic, emit a diagnostic error
            if (isDynamicShader)
            {
                diagnostics.Add(EmbeddedBytecodeWithDynamicShader, structDeclarationSymbol, structDeclarationSymbol);

                return new(true, 0, 0, 0);
            }

            // Ensure the bytecode generation is disabled if any errors are present. This step is done last to ensure that
            // all available diagnostics are still emitted correctly before reaching this point, instead of being disabled.
            if (diagnostics.Count > 0)
            {
                return new(true, 0, 0, 0);
            }

            return new(false, threadsX, threadsY, threadsZ);
        }

        /// <summary>
        /// Indicates whether the required <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries have been loaded.
        /// </summary>
        private static volatile bool areDxcLibrariesLoaded;

        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
        /// </summary>
        /// <param name="threadIds">The input <see cref="ThreadIdsInfo"/> instance to process.</param>
        /// <param name="hlslSource">The generated HLSL source code (ignoring captured delegates, if present).</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <param name="diagnostic">The resulting diagnostic from the processing operation, if any.</param>
        /// <returns>The <see cref="ImmutableArray{T}"/> instance with the compiled shader bytecode.</returns>
        public static unsafe ImmutableArray<byte> GetBytecode(
            ThreadIdsInfo threadIds,
            string hlslSource,
            CancellationToken token,
            out DeferredDiagnosticInfo? diagnostic)
        {
            ImmutableArray<byte> bytecode = ImmutableArray<byte>.Empty;

            // No embedded shader was requested
            if (threadIds.IsDefault)
            {
                diagnostic = null;

                goto End;
            }

            try
            {
                // Try to load dxcompiler.dll and dxil.dll
                LoadNativeDxcLibraries();

                token.ThrowIfCancellationRequested();

                // Replace the actual thread num values
                hlslSource = hlslSource
                    .Replace("<THREADSX>", threadIds.X.ToString(CultureInfo.InvariantCulture))
                    .Replace("<THREADSY>", threadIds.Y.ToString(CultureInfo.InvariantCulture))
                    .Replace("<THREADSZ>", threadIds.Z.ToString(CultureInfo.InvariantCulture));

                // Compile the shader bytecode
                using ComPtr<IDxcBlob> dxcBlobBytecode = ShaderCompiler.Instance.Compile(hlslSource.AsSpan());

                token.ThrowIfCancellationRequested();

                byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);
                diagnostic = null;
            }
            catch (Win32Exception e)
            {
                diagnostic = DeferredDiagnosticInfo.Create(EmbeddedBytecodeFailedWithWin32Exception, e.HResult, FixupExceptionMessage(e.Message));
            }
            catch (DxcCompilationException e)
            {
                diagnostic = DeferredDiagnosticInfo.Create(EmbeddedBytecodeFailedWithDxcCompilationException, FixupExceptionMessage(e.Message));
            }

            End:
            return bytecode;
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

                string folder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Libraries", rid);

                LoadLibrary(Path.Combine(folder, "dxil.dll"));
                LoadLibrary(Path.Combine(folder, "dxcompiler.dll"));

                areDxcLibrariesLoaded = true;
            }
        }
    }
}