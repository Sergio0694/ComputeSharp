using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <inheritdoc/>
    partial class Resources
    {
        /// <summary>
        /// Writes the <c>LoadGraphicsResources</c> method.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(ShaderInfo info, IndentedTextWriter writer)
        {
            string typeName = info.Hierarchy.Hierarchy[0].QualifiedName;

            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"static void global::ComputeSharp.Descriptors.IComputeShaderDescriptor<{typeName}>.LoadGraphicsResources<TLoader>(in {typeName} shader, ref TLoader loader)");

            using (writer.WriteBlock())
            {
                // Nothing to do if there are no resources
                if (info.Resources.IsEmpty)
                {
                    return;
                }

                // Delegate to the generated loader type
                writer.WriteLine("global::ComputeSharp.Generated.GraphicsResourcesLoader.LoadGraphicsResources(in shader, ref loader);");
            }
        }

        /// <summary>
        /// Registers a callback to generate additional types, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional types.</param>
        /// <param name="usingDirectives">The using directives needed by the generated code.</param>
        public static void RegisterAdditionalTypesSyntax(
            ShaderInfo info,
            ImmutableArrayBuilder<IndentedTextWriter.Callback<ShaderInfo>> callbacks,
            ImmutableHashSetBuilder<string> usingDirectives)
        {
            // If there are no resources, there's no need for any additional types
            if (info.Resources.IsEmpty)
            {
                return;
            }

            _ = usingDirectives.Add("global::System.CodeDom.Compiler");
            _ = usingDirectives.Add("global::System.Diagnostics");
            _ = usingDirectives.Add("global::System.Diagnostics.CodeAnalysis");
            _ = usingDirectives.Add("global::System.Runtime.CompilerServices");
            _ = usingDirectives.Add("global::ComputeSharp.Descriptors");

            // Declare the GraphicsResourcesLoader type
            static void Callback(ShaderInfo info, IndentedTextWriter writer)
            {
                string fullyQualifiedTypeName = info.Hierarchy.GetFullyQualifiedTypeName();

                writer.WriteLine($"""/// <summary>""");
                writer.WriteLine($"""/// A type containing loading logic for graphics resources in shaders of type <see cref="{fullyQualifiedTypeName}"/>.""");
                writer.WriteLine($"""/// </summary>""");
                writer.WriteGeneratedAttributes(GeneratorName, useFullyQualifiedTypeNames: false);
                writer.WriteLine($"""file static class GraphicsResourcesLoader""");

                using (writer.WriteBlock())
                {
                    // Define the FromManaged method (managed shader type to native constant buffer)
                    writer.WriteLine($$"""/// <inheritdoc cref="IComputeShaderDescriptor{T}.LoadGraphicsResources"/>""");
                    writer.WriteLine($"""[MethodImpl(MethodImplOptions.AggressiveInlining)]""");
                    writer.WriteLine($"public static void LoadGraphicsResources<TLoader>(in {fullyQualifiedTypeName} shader, ref TLoader loader)");
                    writer.WriteLine("    where TLoader : struct, IGraphicsResourceLoader");

                    using (writer.WriteBlock())
                    {
                        // Generate loading statements for each captured resource.
                        // Each loading statement will invoke the generated accessor.
                        for (int i = 0; i < info.Resources.Length; i++)
                        {
                            writer.WriteLine($"loader.LoadGraphicsResource(shader.{info.Resources[i].FieldName}(), {i});");
                        }
                    }

                    // Define all field accessors (always used, just like for constant buffer fields, see remarks there)
                    foreach (ResourceInfo resourceInfo in info.Resources)
                    {
                        writer.WriteLine();

                        // Generate the correct field accessor depending on whether the field can be referenced directly
                        if (resourceInfo.UnspeakableName is null)
                        {
                            writer.WriteLine($"""
                                /// <inheritdoc cref="{fullyQualifiedTypeName}.{resourceInfo.FieldName}"/>
                                /// <param name="value">The input <see cref="{fullyQualifiedTypeName}"/> value.</param>
                                /// <returns>A reference to <see cref="{fullyQualifiedTypeName}.{resourceInfo.FieldName}"/>.</returns>
                                [UnsafeAccessor(UnsafeAccessorKind.Field)]
                                private static extern ref readonly {resourceInfo.FieldType} {resourceInfo.FieldName}(this ref readonly {fullyQualifiedTypeName} value);
                                """, isMultiline: true);
                        }
                        else
                        {
                            writer.WriteLine($"""
                                /// <summary>Gets a reference to the unspeakable field "{resourceInfo.FieldName}" of type <see cref="{fullyQualifiedTypeName}"/>.</summary>
                                /// <param name="value">The input <see cref="{fullyQualifiedTypeName}"/> value.</param>
                                /// <returns>A reference to the unspeakable field "{resourceInfo.FieldName}".</returns>
                                [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "{resourceInfo.UnspeakableName}")]
                                private static extern ref readonly {resourceInfo.FieldType} {resourceInfo.FieldName}(this ref readonly {fullyQualifiedTypeName} value);
                                """, isMultiline: true);
                        }
                    }
                }
            }

            callbacks.Add(Callback);
        }
    }
}