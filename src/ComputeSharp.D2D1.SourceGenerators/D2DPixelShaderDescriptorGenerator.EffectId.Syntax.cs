using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class EffectId
    {
        /// <summary>
        /// Writes the <c>EffectId</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly ref readonly global::System.Guid global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.EffectId");

            using (writer.WriteBlock())
            {
                writer.WriteLine("[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine("get");

                using (writer.WriteBlock())
                {
                    writer.WriteLine("global::System.ReadOnlySpan<byte> bytes = new byte[]");
                    writer.WriteLine("{");
                    writer.IncreaseIndent();

                    // Write the bytes like so:
                    //
                    // b[0], b[1], b[2], b[3],
                    // b[4], b[5],
                    // b[6], b[7],
                    // b[8],
                    // b[9],
                    // b[10],
                    // b[11],
                    // b[12],
                    // b[13],
                    // b[14],
                    // b[15]
                    SyntaxFormattingHelper.WriteByteArrayInitializationExpressions(info.EffectId.AsSpan()[..4], writer);
                    writer.WriteLine(",");
                    SyntaxFormattingHelper.WriteByteArrayInitializationExpressions(info.EffectId.AsSpan()[4..6], writer);
                    writer.WriteLine(",");
                    SyntaxFormattingHelper.WriteByteArrayInitializationExpressions(info.EffectId.AsSpan()[6..8], writer);
                    writer.WriteLine(",");
                    writer.WriteLine($"{SyntaxFormattingHelper.GetByteExpression(info.EffectId[8])},");
                    writer.WriteLine($"{SyntaxFormattingHelper.GetByteExpression(info.EffectId[9])},");
                    writer.WriteLine($"{SyntaxFormattingHelper.GetByteExpression(info.EffectId[10])},");
                    writer.WriteLine($"{SyntaxFormattingHelper.GetByteExpression(info.EffectId[11])},");
                    writer.WriteLine($"{SyntaxFormattingHelper.GetByteExpression(info.EffectId[12])},");
                    writer.WriteLine($"{SyntaxFormattingHelper.GetByteExpression(info.EffectId[13])},");
                    writer.WriteLine($"{SyntaxFormattingHelper.GetByteExpression(info.EffectId[14])},");
                    writer.WriteLine(SyntaxFormattingHelper.GetByteExpression(info.EffectId[15]));
                    writer.DecreaseIndent();
                    writer.WriteLine("};");

                    writer.WriteLine();
                    writer.WriteLine("return ref global::System.Runtime.CompilerServices.Unsafe.As<byte, global::System.Guid>(ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(bytes));");
                }
            }
        }
    }
}