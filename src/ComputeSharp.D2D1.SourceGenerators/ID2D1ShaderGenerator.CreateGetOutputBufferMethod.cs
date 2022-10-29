using ComputeSharp.SourceGeneration.Extensions;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>GetOutputBuffer</c> method.
    /// </summary>
    private static partial class GetOutputBuffer
    {
        /// <summary>
        /// Extracts the output buffer info for the current shader.
        /// </summary>
        /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
        /// <param name="bufferPrecision">The output buffer precision for the shader.</param>
        /// <param name="channelDepth">The output buffer channel depth for the shader.</param>
        public static void GetInfo(
            INamedTypeSymbol structDeclarationSymbol,
            out D2D1BufferPrecision bufferPrecision,
            out D2D1ChannelDepth channelDepth)
        {
            // Try to get the [D2DOutputBuffer] attribute
            if (!structDeclarationSymbol.TryGetAttributeWithFullyQualifiedMetadataName("ComputeSharp.D2D1.D2DOutputBufferAttribute", out AttributeData? attributeData))
            {
                bufferPrecision = D2D1BufferPrecision.Unknown;
                channelDepth = D2D1ChannelDepth.Default;

                return;
            }

            // Extract the buffer precision and channel depth, if available
            if (attributeData!.ConstructorArguments.Length == 1)
            {
                if (attributeData.AttributeConstructor is IMethodSymbol { Parameters.Length: 1 } constructorSymbol &&
                    constructorSymbol.Parameters[0].Type.HasFullyQualifiedMetadataName("ComputeSharp.D2D1.D2D1BufferPrecision"))
                {
                    bufferPrecision = (D2D1BufferPrecision)attributeData.ConstructorArguments[0].Value!;
                    channelDepth = D2D1ChannelDepth.Default;
                }
                else
                {
                    bufferPrecision = D2D1BufferPrecision.Unknown;
                    channelDepth = (D2D1ChannelDepth)attributeData.ConstructorArguments[0].Value!;
                }
            }
            else if (attributeData.ConstructorArguments.Length == 2)
            {
                bufferPrecision = (D2D1BufferPrecision)attributeData.ConstructorArguments[0].Value!;
                channelDepth = (D2D1ChannelDepth)attributeData.ConstructorArguments[1].Value!;
            }
            else
            {
                // The syntax tree is likely invalid, so just ignore it for now
                bufferPrecision = D2D1BufferPrecision.Unknown;
                channelDepth = D2D1ChannelDepth.Default;
            }
        }
    }
}