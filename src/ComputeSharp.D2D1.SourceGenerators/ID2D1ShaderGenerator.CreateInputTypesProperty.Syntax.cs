using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
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
            writer.Write("readonly global::System.ReadOnlyMemory<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> global::ComputeSharp.D2D1.__Internals.ID2D1Shader.InputTypes => ");

            // If there are no inputs, simply return a default expression. Otherwise, create
            // a ReadOnlyMemory<D2D1PixelShaderInputType> instance from the memory manager.
            if (info.InputTypes.IsEmpty)
            {
                writer.WriteLine("default;");
            }
            else
            {
                writer.WriteLine("InputTypesMemoryManager.Instance.Memory;");
            }
        }

        /// <summary>
        /// Registers a callback to generate an additional type, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional types.</param>
        public static void RegisterAdditionalTypeSyntax(D2D1ShaderInfo info, ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks)
        {
            // If there are no inputs, no memory manager is needed
            if (info.InputTypes.IsEmpty)
            {
                return;
            }

            // Declare the InputTypesMemoryManager custom memory manager type
            static void Callback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                writer.WriteLine($$"""/// <summary>""");
                writer.WriteLine($$"""/// A <see cref="global::System.Buffers.MemoryManager{T}"/> implementation to get the input types.""");
                writer.WriteLine($$"""/// </summary>""");
                writer.WriteLine($$"""[global::System.CodeDom.Compiler.GeneratedCode("{{typeof(ID2D1ShaderGenerator).FullName}}", "{{typeof(ID2D1ShaderGenerator).Assembly.GetName().Version}}")]""");
                writer.WriteLine($$"""[global::System.Diagnostics.DebuggerNonUserCode]""");
                writer.WriteLine($$"""[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]""");
                writer.WriteLine($$"""file sealed class InputTypesMemoryManager : global::System.Buffers.MemoryManager<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType>""");

                using (writer.WriteBlock())
                {
                    // Static singleton instance (the object goes in the frozen heap, so there's no static constructor)
                    writer.WriteLine($$"""/// <summary>The singleton <see cref="InputTypesMemoryManager"/> instance to use.</summary>""");
                    writer.WriteLine($$"""public static readonly InputTypesMemoryManager Instance = new();""");

                    // RVA field
                    writer.WriteLine();
                    writer.WriteLine("/// <summary>The RVA data with the input type info.</summary>");
                    writer.WriteLine("private static global::System.ReadOnlySpan<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> Data => new[]");
                    writer.WriteLine("{");
                    writer.IncreaseIndent();

                    // Input types, one per line in the RVA field initializer
                    for (int i = 0; i < info.InputTypes.Length; i++)
                    {
                        writer.Write("global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType.");
                        writer.Write(info.InputTypes[i] == 0 ? "Simple" : "Complex");

                        if (i < info.InputTypes.Length - 1)
                        {
                            writer.WriteLine(",");
                        }
                    }

                    writer.DecreaseIndent();
                    writer.WriteLine();
                    writer.WriteLine("};");
                    writer.WriteLine();

                    // Add the remaining members for the memory manager
                    writer.WriteLine("""
                        /// <inheritdoc/>
                        public override unsafe global::System.Span<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> GetSpan()
                        {
                            return new(global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(Data)), Data.Length);
                        }

                        /// <inheritdoc/>
                        public override global::System.Memory<global::ComputeSharp.D2D1.Interop.D2D1PixelShaderInputType> Memory
                        {
                            [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
                            get => CreateMemory(Data.Length);
                        }

                        /// <inheritdoc/>
                        public override unsafe global::System.Buffers.MemoryHandle Pin(int elementIndex)
                        {
                            return new(global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref global::System.Runtime.CompilerServices.Unsafe.AsRef(in Data[elementIndex])), pinnable: this);
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