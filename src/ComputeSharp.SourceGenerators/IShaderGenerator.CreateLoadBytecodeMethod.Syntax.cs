using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
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
                if (info.HlslInfo is not HlslBytecodeInfo.Success success)
                {
                    return;
                }

                // Check that the [numthreads] values match
                writer.WriteLine($"if (threadsX == {info.ThreadsX} && threadsY == {info.ThreadsY} && threadsZ == {info.ThreadsZ})");

                using (writer.WriteBlock())
                {
                    writer.Write("global::System.ReadOnlySpan<byte> bytecode = new byte[] { ");

                    SyntaxFormattingHelper.WriteByteArrayInitializationExpressions(success.Bytecode.AsSpan(), writer);

                    writer.WriteLine(" };");
                    writer.WriteLine();
                    writer.WriteLine("loader.LoadEmbeddedBytecode(bytecode);");
                }
            }
        }

        /// <summary>
        /// Writes the <c>ThreadsX</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteThreadsXSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly int global::ComputeSharp.__Internals.IShader.ThreadsX => {info.ThreadsX};");
        }

        /// <summary>
        /// Writes the <c>ThreadsY</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteThreadsYSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly int global::ComputeSharp.__Internals.IShader.ThreadsY => {info.ThreadsY};");
        }

        /// <summary>
        /// Writes the <c>ThreadsZ</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteThreadsZSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly int global::ComputeSharp.__Internals.IShader.ThreadsZ => {info.ThreadsZ};");
        }
    }
}