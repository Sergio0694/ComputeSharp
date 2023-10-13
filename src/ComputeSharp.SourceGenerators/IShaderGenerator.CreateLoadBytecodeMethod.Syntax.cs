using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadBytecode
    {
        /// <summary>
        /// Writes the <c>LoadBytecode</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine("readonly void global::ComputeSharp.__Internals.IShader.LoadBytecode<TLoader>(ref TLoader loader, int threadsX, int threadsY, int threadsZ)");

            using (writer.WriteBlock())
            {
                // If there is no HLSL bytecode, there's nothing left to do
                if (info.EmbeddedBytecode.Bytecode.IsEmpty)
                {
                    return;
                }

                // Check that the [numthreads] values match
                writer.WriteLine($"if (threadsX == {info.ThreadIds.X} && threadsY == {info.ThreadIds.Y} && threadsZ == {info.ThreadIds.Z})");

                using (writer.WriteBlock())
                {
                    writer.Write("global::System.ReadOnlySpan<byte> bytecode = new byte[] { ");

                    SyntaxFormattingHelper.WriteByteArrayInitializationExpressions(info.EmbeddedBytecode.Bytecode.AsSpan(), writer);

                    writer.WriteLine(" };");
                    writer.WriteLine();
                    writer.WriteLine("loader.LoadEmbeddedBytecode(bytecode);");
                }
            }
        }
    }
}