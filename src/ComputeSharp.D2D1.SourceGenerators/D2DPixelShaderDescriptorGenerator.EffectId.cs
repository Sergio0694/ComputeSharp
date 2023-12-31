using System;
using System.Buffers;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
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
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The <see cref="INamedTypeSymbol"/> for the shader type in use.</param>
        /// <returns>The resulting effect id.</returns>
        public static ImmutableArray<byte> GetInfo(Compilation compilation, INamedTypeSymbol structDeclarationSymbol)
        {
            if (TryGetDefinedEffectId(compilation, structDeclarationSymbol, out ImmutableArray<byte> effectId))
            {
                return effectId;
            }

            return CreateDefaultEffectId(structDeclarationSymbol);
        }

        /// <summary>
        /// Tries to get the defined effect id for a given shader type.
        /// </summary>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance.</param>
        /// <param name="effectId">The resulting defined effect id, if found.</param>
        /// <returns>Whether or not a defined effect id could be found.</returns>
        private static bool TryGetDefinedEffectId(Compilation compilation, INamedTypeSymbol typeSymbol, out ImmutableArray<byte> effectId)
        {
            INamedTypeSymbol effectIdAttributeSymbol = compilation.GetTypeByMetadataName("ComputeSharp.D2D1.D2DEffectIdAttribute")!;

            foreach (AttributeData attributeData in typeSymbol.GetAttributes())
            {
                // Check that the attribute is [D2DEffectId] and with a valid parameter
                if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, effectIdAttributeSymbol) &&
                    attributeData.ConstructorArguments is [{ Value: string value }] &&
                    Guid.TryParse(value, out Guid guid))
                {
                    byte[] bytes = guid.ToByteArray();

                    effectId = Unsafe.As<byte[], ImmutableArray<byte>>(ref bytes);

                    return true;
                }
            }

            effectId = default;

            return false;
        }

        /// <summary>
        /// Creates the default effect id for a given type symbol.
        /// </summary>
        /// <param name="typeSymbol">The input <see cref="INamedTypeSymbol"/> instance.</param>
        /// <returns>The resulting effect id.</returns>
        private static unsafe ImmutableArray<byte> CreateDefaultEffectId(INamedTypeSymbol typeSymbol)
        {
            try
            {
                // Initialize an instance using the MD5 algorithm. We use this for several reasons:
                //   - We don't really need security, this is just to uniquely identify types
                //   - The hash size is 128 bits, which is exactly the size of a GUID.
                IncrementalHash incrementalHash = EffectId.incrementalHash ??= IncrementalHash.CreateHash(HashAlgorithmName.MD5);

                string assemblyName = typeSymbol.ContainingAssembly?.Name ?? string.Empty;

                using ImmutableArrayBuilder<char> charBuffer = new();

                // Format the fully qualified name into a pooled builder to avoid the string allocation
                typeSymbol.AppendFullyQualifiedMetadataName(in charBuffer);

                int maxTypeNameByteCount = Encoding.UTF8.GetMaxByteCount(charBuffer.Count);
                int maxAssemblyNameByteCount = Encoding.UTF8.GetMaxByteCount(assemblyName.Length);
                int maxEncodedByteCount = Math.Max(maxTypeNameByteCount, maxAssemblyNameByteCount);

                byte[] bufferUtf8 = ArrayPool<byte>.Shared.Rent(maxEncodedByteCount);

                // UTF8 encode the fully qualified name first
                int typeNameBytesWritten = Encoding.UTF8.GetBytes(charBuffer.WrittenSpan, bufferUtf8);

                // Append the UTF8 fully qualified name to the MD5 hash
                incrementalHash.AppendData(bufferUtf8, 0, typeNameBytesWritten);

                // UTF8 encode the assembly name as well
                int assemblyNameBytesWritten = Encoding.UTF8.GetBytes(assemblyName.AsSpan(), bufferUtf8);

                // Append the UTF8 assembly name to the MD5 hash
                incrementalHash.AppendData(bufferUtf8, 0, assemblyNameBytesWritten);

                // The state is now fully in the incremental hash, we can return the array
                ArrayPool<byte>.Shared.Return(bufferUtf8);

                // Get the resulting MD5 hash (128 bits)
                byte[] hash = incrementalHash.GetHashAndReset();

                // We own this buffer, so we can just reinterpret to an immutable array
                return Unsafe.As<byte[], ImmutableArray<byte>>(ref hash);
            }
            catch
            {
                // Something went wrong, throw away the current incremental hash.
                // Realistically speaking, this should just never happen.
                incrementalHash = null;

                throw;
            }
        }
    }
}