using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;
using ComputeSharp.Shaders.Translation;
using ComputeSharp.SourceGenerators.Diagnostics;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static ComputeSharp.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
public sealed partial class IShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>TryGetBytecode</c> method.
    /// </summary>
    private static partial class TryGetBytecode
    {
        /// <summary>
        /// Gets the thread ids values for a given shader type, if available.
        /// </summary>
        /// <param name="embeddedBytecodeAttribute">The <see cref="INamedTypeSymbol"/> instance for the attribute.</param>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="isDynamicShader">Indicates whether or not the shader is dynamic (ie. captures delegates).</param>
        /// <param name="diagnostics">The resulting diagnostics from the processing operation.</param>
        /// <returns>The thread ids for the precompiled shader, if available.</returns>
        public static ThreadIdsInfo GetInfo(
            INamedTypeSymbol embeddedBytecodeAttribute,
            INamedTypeSymbol structDeclarationSymbol,
            bool isDynamicShader,
            out ImmutableArray<Diagnostic> diagnostics)
        {
            AttributeData? attribute = structDeclarationSymbol
                .GetAttributes()
                .FirstOrDefault(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, embeddedBytecodeAttribute));

            if (attribute is null)
            {
                diagnostics = ImmutableArray<Diagnostic>.Empty;

                return new(true, 0, 0, 0);
            }

            ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();

            // Validate the thread number arguments
            if (attribute.ConstructorArguments[0].Value is not int threadsX ||
                attribute.ConstructorArguments[1].Value is not int threadsY ||
                attribute.ConstructorArguments[2].Value is not int threadsZ ||
                threadsX is < 1 or > 1024 ||
                threadsY is < 1 or > 1024 ||
                threadsZ is < 1 or > 64)
            {
                builder.Add(InvalidEmbeddedBytecodeThreadIds, structDeclarationSymbol, structDeclarationSymbol);

                diagnostics = builder.ToImmutable();

                return new(true, 0, 0, 0);
            }

            // If the current shader is dynamic, emit a diagnostic error
            if (isDynamicShader)
            {
                builder.Add(EmbeddedBytecodeWithDynamicShader, structDeclarationSymbol, structDeclarationSymbol);

                diagnostics = builder.ToImmutable();

                return new(true, 0, 0, 0);
            }

            diagnostics = builder.ToImmutable();

            return new(false, threadsX, threadsY, threadsZ);
        }

        /// <summary>
        /// Indicates whether the required <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries have been loaded.
        /// </summary>
        private static bool areDxcLibrariesLoaded;

        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
        /// </summary>
        /// <param name="threadIds">The input <see cref="ThreadIdsInfo"/> instance to process.</param>
        /// <param name="hlslSource">The generated HLSL source code (ignoring captured delegates, if present).</param>
        /// <param name="diagnostics">The resulting diagnostics from the processing operation.</param>
        /// <returns>The <see cref="ImmutableArray{T}"/> instance with the compiled shader bytecode.</returns>
        [Pure]
        public static unsafe ImmutableArray<byte> GetBytecode(
            ThreadIdsInfo threadIds,
            string hlslSource,
            out ImmutableArray<Diagnostic> diagnostics)
        {
            ImmutableArray<byte> bytecode = ImmutableArray<byte>.Empty;
            ImmutableArray<Diagnostic>.Builder builder = ImmutableArray.CreateBuilder<Diagnostic>();

            // No embedded shader was requested
            if (threadIds.IsDefault)
            {
                goto End;
            }

            try
            {
                // Try to load dxcompiler.dll and dxil.dll
                LoadNativeDxcLibraries();

                // Replace the actual thread num values
                hlslSource = hlslSource
                    .Replace("<THREADSX>", threadIds.X.ToString())
                    .Replace("<THREADSY>", threadIds.Y.ToString())
                    .Replace("<THREADSZ>", threadIds.Z.ToString());

                // Compile the shader bytecode
                using ComPtr<IDxcBlob> dxcBlobBytecode = ShaderCompiler.Instance.CompileShader(hlslSource.AsSpan());

                byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);
            }
            catch (Win32Exception)
            {
                // TODO
                //builder.Add(
                //    EmbeddedBytecodeFailedWithWin32Exception,
                //    structDeclarationSymbol,
                //    structDeclarationSymbol,
                //    e.HResult,
                //    e.Message);
            }
            catch (HlslCompilationException)
            {
                // TODO
                //builder.Add(
                //    EmbeddedBytecodeFailedWithHlslCompilationException,
                //    structDeclarationSymbol,
                //    structDeclarationSymbol,
                //    e.Message);
            }

            End:
            diagnostics = builder.ToImmutable();

            return bytecode;
        }

        /// <summary>
        /// Extracts and loads the <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown if the CPU architecture is not supported.</exception>
        /// <exception cref="Win32Exception">Thrown if a library fails to load.</exception>
        private static void LoadNativeDxcLibraries()
        {
            // Extracts a specified native library for a given runtime identifier
            static string ExtractLibrary(string rid, string name)
            {
                string sourceFilename = $"ComputeSharp.SourceGenerators.ComputeSharp.Libraries.{rid}.{name}.dll";
                string targetFilename = Path.Combine(Path.GetTempPath(), "ComputeSharp.SourceGenerators", Path.GetRandomFileName(), rid, $"{name}.dll");

                Directory.CreateDirectory(Path.GetDirectoryName(targetFilename));

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

                LoadLibrary(ExtractLibrary(rid, "dxil"));
                LoadLibrary(ExtractLibrary(rid, "dxcompiler"));
            }

            areDxcLibrariesLoaded = true;
        }
    }
}
