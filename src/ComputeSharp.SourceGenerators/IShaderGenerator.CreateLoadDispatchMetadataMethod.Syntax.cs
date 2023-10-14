using System.Globalization;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class IShaderGenerator
{
    /// <inheritdoc/>
    partial class LoadDispatchMetadata
    {
        /// <summary>
        /// Writes the <c>ConstantBufferSize</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteConstantBufferSizeSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly int global::ComputeSharp.__Internals.IShader.ConstantBufferSize => {info.ConstantBufferSizeInBytes};");
        }

        /// <summary>
        /// Writes the <c>IsStaticSamplerRequired</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteIsStaticSamplerRequiredSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"readonly bool global::ComputeSharp.__Internals.IShader.IsStaticSamplerRequired => {info.IsSamplerUsed.ToString().ToLowerInvariant()};");
        }

        /// <summary>
        /// Writes the <c>LoadDispatchMetadata</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine("[global::System.Runtime.CompilerServices.SkipLocalsInit]");
            writer.WriteLine("readonly void global::ComputeSharp.__Internals.IShader.LoadDispatchMetadata<TLoader>(ref TLoader loader, out global::System.IntPtr result)");

            using (writer.WriteBlock())
            {
                // Compute the total number of resources
                int totalResourcesCount = info.ResourceDescriptors.Length;

                writer.WriteLine("global::System.Span<byte> span0 = stackalloc byte[5];");
                writer.WriteLine($"global::System.Span<global::ComputeSharp.__Internals.ResourceDescriptor> span1 = stackalloc global::ComputeSharp.__Internals.ResourceDescriptor[{totalResourcesCount}];");
                writer.WriteLine("ref byte r0 = ref span0[0];");
                writer.WriteLine("ref global::ComputeSharp.__Internals.ResourceDescriptor r1 = ref span1[0];");

                // Write the serialized shader metadata
                writer.WriteLine($"global::System.Runtime.CompilerServices.Unsafe.WriteUnaligned<int>(ref global::System.Runtime.CompilerServices.Unsafe.Add(ref r0, 0), {info.ConstantBufferSizeInBytes});");
                writer.WriteLine($"global::System.Runtime.CompilerServices.Unsafe.WriteUnaligned<bool>(ref global::System.Runtime.CompilerServices.Unsafe.Add(ref r0, 4), {info.IsSamplerUsed.ToString(CultureInfo.InvariantCulture).ToLowerInvariant()});");

                // Populate the sequence of resource descriptors
                foreach (ResourceDescriptor descriptor in info.ResourceDescriptors)
                {
                    writer.WriteLine($"global::ComputeSharp.__Internals.ResourceDescriptor.Create({descriptor.TypeId}, {descriptor.RegisterOffset}, out global::System.Runtime.CompilerServices.Unsafe.Add(ref r1, {descriptor.Offset}));");
                }

                // Invoke the value delegate to create the opaque root signature handle
                writer.WriteLine("loader.LoadMetadataHandle(span0, span1, out result);");
            }
        }
    }
}