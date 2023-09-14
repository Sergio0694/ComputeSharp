using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>EffectId</c> property.
    /// </summary>
    private static partial class EffectId
    {
        /// <summary>
        /// The <see cref="IncrementalHash"/> instance currently in use for the running thread, if any.
        /// </summary>
        [ThreadStatic]
        private static IncrementalHash? incrementalHash;

        /// <summary>
        /// Extracts the effect id info for the current shader.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type in use.</param>
        /// <returns>The resulting effect id.</returns>
        public static ImmutableArray<byte> GetInfo(ImmutableArrayBuilder<DiagnosticInfo> diagnostics, INamedTypeSymbol structDeclarationSymbol)
        {
            // Initialize an instance using the MD5 algorithm. We use this for several reasons:
            //   - We don't really need security, this is just to uniquely identify types
            //   - The hash size is 128 bits, which is exactly the size of a GUID.
            IncrementalHash incrementalHash = EffectId.incrementalHash ??= IncrementalHash.CreateHash(HashAlgorithmName.MD5);

            string assemblyName = structDeclarationSymbol.ContainingAssembly?.Name ?? string.Empty;

            using ImmutableArrayBuilder<byte> byteBuffer = ImmutableArrayBuilder<byte>.Rent();
            using ImmutableArrayBuilder<char> charBuffer = ImmutableArrayBuilder<char>.Rent();

            // Format the fully qualified name into a pooled builder to avoid the string allocation
            structDeclarationSymbol.AppendFullyQualifiedMetadataName(in charBuffer);

            int maxTypeNameCharsLength = Encoding.UTF8.GetMaxByteCount(charBuffer.Count);
            int maxAssemblyNameCharsLength = Encoding.UTF8.GetMaxByteCount(assemblyName.Length);
            int maxEncodedCharsLength = Math.Max(maxTypeNameCharsLength, maxAssemblyNameCharsLength);

            byteBuffer.EnsureCapacity(maxEncodedCharsLength);

            // UTF8 encode the fully qualified name first
            int typeNameCharsLength = Encoding.UTF8.GetBytes(charBuffer.WrittenSpan, byteBuffer.DangerousGetArray().AsSpan());

            byteBuffer.Advance(typeNameCharsLength);

            // Append the UTF8 fully qualified name to the MD5 hash
            incrementalHash.AppendData(byteBuffer.DangerousGetArray(), 0, byteBuffer.Count);

            // UTF8 encode the assembly name as well
            int assemblyNameCharsLength = Encoding.UTF8.GetBytes(assemblyName.AsSpan(), byteBuffer.DangerousGetArray().AsSpan());

            byteBuffer.Advance(assemblyNameCharsLength);

            // Append the UTF8 assembly name to the MD5 hash
            incrementalHash.AppendData(byteBuffer.DangerousGetArray(), 0, byteBuffer.Count);

            // Get the resulting MD5 hash (128 bits)
            byte[] hash = incrementalHash.GetHashAndReset();

            // We own this buffer, so we can just reinterpret to an immutable array
            return Unsafe.As<byte[], ImmutableArray<byte>>(ref hash);
        }
    }
}