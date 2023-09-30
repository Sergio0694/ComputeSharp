using System;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.SourceGeneration.Extensions;

/// <summary>
/// Extension methods for the <see cref="IndentedTextWriter"/> type.
/// </summary>
internal static class IndentedTextWriterExtensions
{
    /// <summary>
    /// Writes the following attributes into a target writer:
    /// <code>
    /// [global::System.CodeDom.Compiler.GeneratedCode("...", "...")]
    /// [global::System.Diagnostics.DebuggerNonUserCode]
    /// [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    /// </code>
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="generatorType">The type of the running generator.</param>
    /// <param name="useFullyQualifiedTypeNames">Whether to use fully qualified type names or not.</param>
    public static void WriteGeneratedAttributes(this IndentedTextWriter writer, Type generatorType, bool useFullyQualifiedTypeNames = true)
    {
        if (useFullyQualifiedTypeNames)
        {
            writer.WriteLine($$"""[global::System.CodeDom.Compiler.GeneratedCode("{{generatorType.FullName}}", "{{generatorType.Assembly.GetName().Version}}")]""");
            writer.WriteLine($$"""[global::System.Diagnostics.DebuggerNonUserCode]""");
            writer.WriteLine($$"""[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]""");
        }
        else
        {
            writer.WriteLine($$"""[GeneratedCode("{{generatorType.FullName}}", "{{generatorType.Assembly.GetName().Version}}")]""");
            writer.WriteLine($$"""[DebuggerNonUserCode]""");
            writer.WriteLine($$"""[ExcludeFromCodeCoverage]""");
        }
    }

    /// <summary>
    /// Writes a new line into the target writer depending on an input condition.
    /// </summary>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="condition">The condition to use to decide whether or not to write a new line.</param>
    public static void WriteLineIf(this IndentedTextWriter writer, bool condition)
    {
        if (condition)
        {
            writer.WriteLine();
        }
    }

    /// <summary>
    /// Writes a series of members separated by one line between each of them.
    /// </summary>
    /// <typeparam name="T">The type of input items to process.</typeparam>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="items">The input items to process.</param>
    /// <param name="callback">The <see cref="IndentedTextWriter.Callback{T}"/> instance to invoke for each item.</param>
    public static void WriteLineSeparatedMembers<T>(
        this IndentedTextWriter writer,
        ReadOnlySpan<T> items,
        IndentedTextWriter.Callback<T> callback)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (i > 0)
            {
                writer.WriteLine();
            }

            callback(items[i], writer);
        }
    }

    /// <summary>
    /// Writes a series of initialization expressions separated by a comma between each of them.
    /// </summary>
    /// <typeparam name="T">The type of input items to process.</typeparam>
    /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
    /// <param name="items">The input items to process.</param>
    /// <param name="callback">The <see cref="IndentedTextWriter.Callback{T}"/> instance to invoke for each item.</param>
    public static void WriteInitializationExpressions<T>(
        this IndentedTextWriter writer,
        ReadOnlySpan<T> items,
        IndentedTextWriter.Callback<T> callback)
    {
        for (int i = 0; i < items.Length; i++)
        {
            callback(items[i], writer);

            if (i < items.Length - 1)
            {
                writer.WriteLine(",");
            }
        }
    }
}