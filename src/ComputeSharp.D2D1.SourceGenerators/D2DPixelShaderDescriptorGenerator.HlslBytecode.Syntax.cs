using System.Linq;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class HlslBytecode
    {
        /// <summary>
        /// Writes the <c>ShaderProfile</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteShaderProfileSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly ComputeSharp.D2D1.D2D1ShaderProfile global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.ShaderProfile => global::ComputeSharp.D2D1.D2D1ShaderProfile.{info.HlslInfoKey.ShaderProfile};");
        }

        /// <summary>
        /// Writes the <c>CompileOptions</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteCompileOptionsSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            // Get a formatted representation of the compile options being used
            string compileOptionsExpression =
                info.HlslInfoKey.CompileOptions
                .ToString()
                .Split(',')
                .Select(static name => $"global::ComputeSharp.D2D1.D2D1CompileOptions.{name.Trim()}")
                .Aggregate("", static (left, right) => left.Length > 0 ? $"{left} | {right}" : right);

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly ComputeSharp.D2D1.D2D1CompileOptions global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.CompileOptions => {compileOptionsExpression};");
        }

        /// <summary>
        /// Writes the <c>HlslBytecode</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteHlslBytecodeSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.Write($"readonly global::System.ReadOnlyMemory<byte> global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.HlslBytecode => ");

            // If there is no bytecode, just return a default expression.
            // Otherwise, return the memory manager backed memory instance.
            if (info.HlslInfo is not HlslBytecodeInfo.Success)
            {
                writer.WriteLine("default;");
            }
            else
            {
                writer.WriteLine("global::ComputeSharp.D2D1.Generated.HlslBytecodeMemoryManager.Instance.Memory;");
            }
        }

        /// <summary>
        /// Registers a callback to generate an additional type, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional types.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        public static void RegisterAdditionalTypeSyntax(
            D2D1ShaderInfo info,
            ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks,
            ImmutableHashSetBuilder<string> usingDirectives)
        {
            // If there is no bytecode, no memory manager is needed
            if (info.HlslInfo is not HlslBytecodeInfo.Success)
            {
                return;
            }

            usingDirectives.Add("global::System");
            usingDirectives.Add("global::System.Buffers");
            usingDirectives.Add("global::System.CodeDom.Compiler");
            usingDirectives.Add("global::System.Diagnostics");
            usingDirectives.Add("global::System.Diagnostics.CodeAnalysis");
            usingDirectives.Add("global::System.Runtime.CompilerServices");
            usingDirectives.Add("global::System.Runtime.InteropServices");

            // Declare the HlslBytecodeMemoryManager custom memory manager type
            static void Callback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                writer.WriteLine($$"""/// <summary>""");
                writer.WriteLine($$"""/// <see cref="MemoryManager{T}"/> implementation to get the HLSL bytecode.""");
                writer.WriteLine($$"""/// </summary>""");
                writer.WriteGeneratedAttributes(GeneratorName, useFullyQualifiedTypeNames: false);
                writer.WriteLine($$"""file sealed class HlslBytecodeMemoryManager : MemoryManager<byte>""");

                using (writer.WriteBlock())
                {
                    // Static singleton instance (to avoid the static constructor, just like with the input types)
                    writer.WriteLine($$"""/// <summary>The singleton <see cref="HlslBytecodeMemoryManager"/> instance to use.</summary>""");
                    writer.WriteLine($$"""public static readonly HlslBytecodeMemoryManager Instance = new();""");

                    // RVA field (with the compiled HLSL bytecode, on a single line)
                    writer.WriteLine();
                    writer.WriteLine("/// <summary>The RVA data with the HLSL bytecode.</summary>");
                    writer.Write("private static ReadOnlySpan<byte> Data => new byte[] { ");

                    SyntaxFormattingHelper.WriteByteArrayInitializationExpressions(((HlslBytecodeInfo.Success)info.HlslInfo).Bytecode.AsSpan(), writer);

                    writer.WriteLine(" };");
                    writer.WriteLine();

                    // Add the remaining members for the memory manager
                    writer.WriteLine("""
                        /// <inheritdoc/>
                        public override unsafe Span<byte> GetSpan()
                        {
                            return new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(Data)), Data.Length);
                        }

                        /// <inheritdoc/>
                        public override Memory<byte> Memory
                        {
                            [MethodImpl(MethodImplOptions.AggressiveInlining)]
                            get => CreateMemory(Data.Length);
                        }

                        /// <inheritdoc/>
                        public override unsafe MemoryHandle Pin(int elementIndex)
                        {
                            return new(Unsafe.AsPointer(ref Unsafe.AsRef(in Data[elementIndex])), pinnable: this);
                        }

                        /// <inheritdoc/>
                        public override void Unpin()
                        {
                        }

                        /// <inheritdoc/>
                        protected override void Dispose(bool disposing)
                        {
                        }
                        """, isMultiline: true);
                }
            }

            callbacks.Add(Callback);
        }
    }
}