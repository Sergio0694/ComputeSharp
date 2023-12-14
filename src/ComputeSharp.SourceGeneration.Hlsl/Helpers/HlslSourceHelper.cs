using System.Collections.Immutable;

namespace ComputeSharp.SourceGeneration.Helpers;

/// <summary>
/// A helper type to write HLSL source.
/// </summary>
internal static class HlslSourceHelper
{
    /// <summary>
    /// Writes the header included at the top of each generated HLSL shader.
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    public static void WriteHeader(IndentedTextWriter writer)
    {
        writer.WriteLine("""
            // ================================================
            //                  AUTO GENERATED
            // ================================================
            // This shader was created by ComputeSharp.
            // See: https://github.com/Sergio0694/ComputeSharp.
            """, isMultiline: true);

        writer.WriteLine();
    }

    /// <summary>
    /// Writes the top declarations in each generated HLSL shader:
    /// <list type="bullet">
    ///   <item>Defined constants.</item>
    ///   <item>Type forward declarations (optional).</item>
    ///   <item>Method forward declarations.</item>
    ///   <item>Static fields.</item>
    ///   <item>Type declarations.</item>
    /// </list>
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="definedConstants">The sequence of defined constants for the shader.</param>
    /// <param name="staticFields">The sequence of static fields referenced by the shader.</param>
    /// <param name="processedMethods">The sequence of processed methods used by the shader.</param>
    /// <param name="typeDeclarations">The collection of declarations of all custom types.</param>
    /// <param name="includeTypeForwardDeclarations">Whether to include type forward declarations (if supported).</param>
    public static void WriteTopDeclarations(
        IndentedTextWriter writer,
        ImmutableArray<HlslConstant> definedConstants,
        ImmutableArray<HlslStaticField> staticFields,
        ImmutableArray<HlslMethod> processedMethods,
        ImmutableArray<HlslUserType> typeDeclarations,
        bool includeTypeForwardDeclarations)
    {
        // Define declarations
        foreach (HlslConstant constant in definedConstants)
        {
            writer.WriteLine($"#define {constant.Name} {constant.Value}");
        }

        writer.WriteLine(skipIfPresent: true);

        // Forward declarations of discovered types, only if supported
        if (includeTypeForwardDeclarations)
        {
            foreach (HlslUserType userType in typeDeclarations)
            {
                writer.WriteLine($"struct {userType.Name};");
            }

            writer.WriteLine(skipIfPresent: true);
        }

        // Forward declarations of shader/static methods
        foreach (HlslMethod method in processedMethods)
        {
            writer.WriteLine(skipIfPresent: true);
            writer.WriteLine(method.Signature);
        }

        writer.WriteLine(skipIfPresent: true);

        // Static fields
        foreach (HlslStaticField field in staticFields)
        {
            if (field.Assignment is string assignment)
            {
                writer.WriteLine($"{field.TypeDeclaration} {field.Name} = {assignment};");
            }
            else
            {
                writer.WriteLine($"{field.TypeDeclaration} {field.Name};");
            }
        }

        writer.WriteLine(skipIfPresent: true);

        // Declared types
        foreach (HlslUserType userType in typeDeclarations)
        {
            writer.WriteLine(skipIfPresent: true);
            writer.WriteLine(userType.Definition);
        }

        writer.WriteLine(skipIfPresent: true);
    }

    /// <summary>
    /// Writes the declarations for captured shader fields.
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="valueFields">The sequence of value instance fields for the current shader.</param>
    public static void WriteCapturedFields(
        IndentedTextWriter writer,
        ImmutableArray<HlslValueField> valueFields)
    {
        foreach (HlslValueField valueField in valueFields)
        {
            writer.WriteLine($"{valueField.Type} {valueField.Name};");
        }
    }

    /// <summary>
    /// Writes all method declarations (ie. implementations of previous forward declarations).
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="processedMethods">The sequence of processed methods used by the shader.</param>
    /// <param name="typeMethodDeclarations">The collection of implementations of all methods in all custom types.</param>
    public static void WriteMethodDeclarations(
        IndentedTextWriter writer,
        ImmutableArray<HlslMethod> processedMethods,
        ImmutableArray<string> typeMethodDeclarations)
    {
        // Method declarations for discovered types
        foreach (string method in typeMethodDeclarations)
        {
            writer.WriteLine(skipIfPresent: true);
            writer.WriteLine(method);
        }

        // Captured methods
        foreach (HlslMethod method in processedMethods)
        {
            writer.WriteLine(skipIfPresent: true);
            writer.WriteLine(method.Declaration);
        }

        writer.WriteLine(skipIfPresent: true);
    }
}
