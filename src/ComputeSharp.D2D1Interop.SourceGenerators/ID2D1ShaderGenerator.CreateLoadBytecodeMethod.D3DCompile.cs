using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using ComputeSharp.D2D1Interop.Exceptions;
using ComputeSharp.D2D1Interop.Shaders.Translation;
using ComputeSharp.D2D1Interop.SourceGenerators.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static ComputeSharp.D2D1Interop.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.D2D1Interop.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritodoc/>
    internal static partial class LoadBytecode
    {
        /// <summary>
        /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
        /// </summary>
        /// <param name="hlslSource">The generated HLSL source code (ignoring captured delegates, if present).</param>
        /// <param name="shaderProfile">The shader profile to use to compile the shader, if requested.</param>
        /// <param name="token">The <see cref="CancellationToken"/> used to cancel the operation, if needed.</param>
        /// <param name="diagnostic">The resulting diagnostic from the processing operation, if any.</param>
        /// <returns>The <see cref="ImmutableArray{T}"/> instance with the compiled shader bytecode.</returns>
        public static unsafe ImmutableArray<byte> GetBytecode(
            string hlslSource,
            D2D1ShaderProfile? shaderProfile,
            CancellationToken token,
            out DiagnosticInfo? diagnostic)
        {
            ImmutableArray<byte> bytecode = ImmutableArray<byte>.Empty;

            // No embedded shader was requested
            if (shaderProfile is null)
            {
                diagnostic = null;

                goto End;
            }

            try
            {
                token.ThrowIfCancellationRequested();

                // Compile the shader bytecode
                using ComPtr<ID3DBlob> dxcBlobBytecode = D2D1ShaderCompiler.Compile(hlslSource.AsSpan(), shaderProfile.Value, false);

                token.ThrowIfCancellationRequested();

                byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
                int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());

                byte[] array = new ReadOnlySpan<byte>(buffer, length).ToArray();

                bytecode = Unsafe.As<byte[], ImmutableArray<byte>>(ref array);
                diagnostic = null;
            }
            catch (Win32Exception e)
            {
                diagnostic = new DiagnosticInfo(EmbeddedBytecodeFailedWithWin32Exception, e.HResult, e.Message);
            }
            catch (FxcCompilationException e)
            {
                diagnostic = new DiagnosticInfo(EmbeddedBytecodeFailedWithDxcCompilationException, e.Message);
            }

            End:
            return bytecode;
        }
    }
}
