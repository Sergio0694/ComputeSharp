using System;
using System.Collections.Immutable;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Mappings;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGeneration.SyntaxProcessors;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using static ComputeSharp.SourceGeneration.Diagnostics.DiagnosticDescriptors;

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
partial class ComputeShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the resources loading code.
    /// </summary>
    private static class Resources
    {
        /// <summary>
        /// Explores a given type hierarchy and generates statements to load fields.
        /// </summary>
        /// <param name="diagnostics">The collection of produced <see cref="DiagnosticInfo"/> instances.</param>
        /// <param name="compilation">The input <see cref="Compilation"/> object currently in use.</param>
        /// <param name="structDeclarationSymbol">The current shader type being explored.</param>
        /// <param name="constantBufferSizeInBytes">The size of the shader constant buffer.</param>
        /// <param name="resources">The sequence of <see cref="ResourceInfo"/> instances for all captured resources.</param>
        public static void GetInfo(
            ImmutableArrayBuilder<DiagnosticInfo> diagnostics,
            Compilation compilation,
            ITypeSymbol structDeclarationSymbol,
            int constantBufferSizeInBytes,
            out ImmutableArray<ResourceInfo> resources)
        {
            using ImmutableArrayBuilder<ResourceInfo> resourceBuilder = new();

            foreach (ISymbol memberSymbol in structDeclarationSymbol.GetMembers())
            {
                // Only process instance fields
                if (memberSymbol is not IFieldSymbol { Type: INamedTypeSymbol { IsStatic: false }, IsConst: false, IsStatic: false, IsFixedSizeBuffer: false, IsImplicitlyDeclared: false } fieldSymbol)
                {
                    continue;
                }

                // Skip fields of not accessible types (the analyzer will handle this just like other fields)
                if (!fieldSymbol.Type.IsAccessibleFromCompilationAssembly(compilation))
                {
                    continue;
                }

                // Try to get the name to use for the field and the accessor
                if (!ConstantBufferSyntaxProcessor.TryGetFieldAccessorName(fieldSymbol, out string? fieldName, out string? unspeakableName))
                {
                    continue;
                }

                string typeName = fieldSymbol.Type.GetFullyQualifiedMetadataName();

                // Check if the field is a resource (note: resources can only be top level fields)
                if (HlslKnownTypes.IsTypedResourceType(typeName))
                {
                    resourceBuilder.Add(new ResourceInfo(fieldName, unspeakableName, typeName));
                }
            }

            resources = resourceBuilder.ToImmutable();

            // A shader root signature has a maximum size of 64 DWORDs, so 256 bytes.
            // Loaded values in the root signature have the following costs:
            //  - Root constants cost 1 DWORD each, since they are 32-bit values.
            //  - Descriptor tables cost 1 DWORD each.
            //  - Root descriptors (64-bit GPU virtual addresses) cost 2 DWORDs each.
            // So here we check whether the current signature respects that constraint,
            // and emit a build error otherwise. For more info on this, see the docs here:
            // https://docs.microsoft.com/windows/win32/direct3d12/root-signature-limits.
            if ((constantBufferSizeInBytes / sizeof(int)) + resourceBuilder.Count > 64)
            {
                diagnostics.Add(ShaderDispatchDataSizeExceeded, structDeclarationSymbol, structDeclarationSymbol);
            }
        }

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
                    writer.WriteLine($"""[SkipLocalsInit]""");
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
                                /// <param name="shader">The input <see cref="{fullyQualifiedTypeName}"/> value.</param>
                                /// <returns>A reference to <see cref="{fullyQualifiedTypeName}.{resourceInfo.FieldName}"/>.</returns>
                                [UnsafeAccessor(UnsafeAccessorKind.Field)]
                                private static extern ref readonly IGraphicsResource {resourceInfo.FieldName}(this ref readonly {fullyQualifiedTypeName} value);
                                """, isMultiline: true);
                        }
                        else
                        {
                            writer.WriteLine($"""
                                /// <summary>Gets a reference to the unspeakable field "{resourceInfo.FieldName}" of type <see cref="{fullyQualifiedTypeName}"/>.</summary>
                                /// <param name="shader">The input <see cref="{fullyQualifiedTypeName}"/> value.</param>
                                /// <returns>A reference to the unspeakable field "{resourceInfo.FieldName}".</returns>
                                [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "{resourceInfo.UnspeakableName}")]
                                private static extern ref readonly IGraphicsResource {resourceInfo.FieldName}(this ref readonly {fullyQualifiedTypeName} value);
                                """, isMultiline: true);
                        }
                    }
                }
            }

            callbacks.Add(Callback);
        }
    }
}