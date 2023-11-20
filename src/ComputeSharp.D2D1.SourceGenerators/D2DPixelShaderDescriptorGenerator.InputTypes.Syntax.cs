using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class InputTypes
    {
        /// <summary>
        /// Writes the <c>InputTypes</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.Write($"static global::System.ReadOnlyMemory<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.InputTypes => ");

            // If there are no inputs, simply return a default expression. Otherwise, create
            // a ReadOnlyMemory<D2D1PixelShaderInputType> instance from the memory manager.
            if (info.InputTypes.IsEmpty)
            {
                writer.WriteLine("default;");
            }
            else
            {
                writer.WriteLine("global::ComputeSharp.D2D1.Generated.InputTypesMemoryManager.Instance.Memory;");
            }
        }

        /// <summary>
        /// Registers a callback to generate additional types, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional types.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        public static void RegisterAdditionalTypesSyntax(
            D2D1ShaderInfo info,
            ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks,
            ImmutableHashSetBuilder<string> usingDirectives)
        {
            // If there are no inputs, no memory manager is needed
            if (info.InputTypes.IsEmpty)
            {
                return;
            }

            _ = usingDirectives.Add("global::System");
            _ = usingDirectives.Add("global::System.Buffers");
            _ = usingDirectives.Add("global::System.CodeDom.Compiler");
            _ = usingDirectives.Add("global::System.Diagnostics");
            _ = usingDirectives.Add("global::System.Diagnostics.CodeAnalysis");
            _ = usingDirectives.Add("global::System.Runtime.CompilerServices");
            _ = usingDirectives.Add("global::System.Runtime.InteropServices");
            _ = usingDirectives.Add("global::ComputeSharp.D2D1.Interop");

            // Declare the InputTypesMemoryManager custom memory manager type
            static void Callback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                writer.WriteLine($$"""/// <summary>""");
                writer.WriteLine($$"""/// A <see cref="MemoryManager{T}"/> implementation to get the input types.""");
                writer.WriteLine($$"""/// </summary>""");
                writer.WriteGeneratedAttributes(GeneratorName, useFullyQualifiedTypeNames: false);
                writer.WriteLine($$"""file sealed class InputTypesMemoryManager : MemoryManager<D2D1PixelShaderInputType>""");

                using (writer.WriteBlock())
                {
                    // Static singleton instance (the object goes in the frozen heap, so there's no static constructor)
                    writer.WriteLine($$"""/// <summary>The singleton <see cref="InputTypesMemoryManager"/> instance to use.</summary>""");
                    writer.WriteLine($$"""public static readonly InputTypesMemoryManager Instance = new();""");

                    // RVA field
                    writer.WriteLine();
                    writer.WriteLine("/// <summary>The RVA data with the input type info.</summary>");
                    writer.WriteLine("private static ReadOnlySpan<D2D1PixelShaderInputType> Data =>");
                    writer.WriteLine("[");
                    writer.IncreaseIndent();

                    // Input types, one per line in the RVA field initializer
                    writer.WriteInitializationExpressions(info.InputTypes.AsSpan(), static (type, writer) =>
                    {
                        writer.Write("D2D1PixelShaderInputType.");
                        writer.Write(type == 0 ? "Simple" : "Complex");
                    });

                    writer.DecreaseIndent();
                    writer.WriteLine();
                    writer.WriteLine("];");
                    writer.WriteLine();

                    // Add the remaining members for the memory manager
                    writer.WriteLine("""
                        /// <inheritdoc/>
                        public override unsafe Span<D2D1PixelShaderInputType> GetSpan()
                        {
                            return new(Unsafe.AsPointer(ref MemoryMarshal.GetReference(Data)), Data.Length);
                        }

                        /// <inheritdoc/>
                        public override Memory<D2D1PixelShaderInputType> Memory
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