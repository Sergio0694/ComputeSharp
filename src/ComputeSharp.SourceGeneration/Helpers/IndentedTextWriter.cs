// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace ComputeSharp.SourceGeneration.Helpers;

/// <summary>
/// A helper type to build sequences of values with pooled buffers.
/// </summary>
internal sealed class IndentedTextWriter : IDisposable
{
    /// <summary>
    /// The default indentation (4 spaces).
    /// </summary>
    private const string DefaultIndentation = "    ";

    /// <summary>
    /// The default new line (<c>'\n'</c>).
    /// </summary>
    private const char DefaultNewLine = '\n';

    /// <summary>
    /// The <see cref="ImmutableArrayBuilder{T}"/> instance that text will be written to.
    /// </summary>
    private ImmutableArrayBuilder<char> builder;

    /// <summary>
    /// The current indentation level.
    /// </summary>
    private int currentIndentationLevel;

    /// <summary>
    /// The current indentation, as text.
    /// </summary>
    private string currentIndentation = "";

    /// <summary>
    /// The cached array of available indentations, as text.
    /// </summary>
    private string[] availableIndentations;

    /// <summary>
    /// Creates an <see cref="IndentedTextWriter"/> instance with a pooled underlying data writer.
    /// </summary>
    /// <returns>An <see cref="IndentedTextWriter"/> instance to write data to.</returns>
    public static IndentedTextWriter Rent()
    {
        return new(ImmutableArrayBuilder<char>.Rent());
    }

    /// <summary>
    /// Creates a new <see cref="IndentedTextWriter"/> object with the specified parameters.
    /// </summary>
    /// <param name="builder">The target <see cref="ImmutableArrayBuilder{T}"/> instance to use.</param>
    private IndentedTextWriter(ImmutableArrayBuilder<char> builder)
    {
        this.builder = builder;
        this.currentIndentationLevel = 0;
        this.currentIndentation = DefaultIndentation;
        this.availableIndentations = new string[4];

        this.availableIndentations[0] = "";

        for (int i = 1, n = this.availableIndentations.Length; i < n; i++)
        {
            this.availableIndentations[i] = this.availableIndentations[i - 1] + DefaultIndentation;
        }
    }

    /// <summary>
    /// Increases the current indentation level.
    /// </summary>
    public void IncreaseIndent()
    {
        this.currentIndentationLevel++;

        if (this.currentIndentationLevel == this.availableIndentations.Length)
        {
            Array.Resize(ref this.availableIndentations, this.availableIndentations.Length * 2);
        }

        // Set both the current indentation and the current position in the indentations
        // array to the expected indentation for the incremented level (ie. one level more).
        this.currentIndentation = this.availableIndentations[this.currentIndentationLevel]
            ??= this.availableIndentations[this.currentIndentationLevel - 1] + DefaultIndentation;
    }

    /// <summary>
    /// Decreases the current indentation level.
    /// </summary>
    public void DecreaseIndent()
    {
        this.currentIndentationLevel--;
        this.currentIndentation = this.availableIndentations[this.currentIndentationLevel];
    }

    /// <summary>
    /// Writes a block to the underlying buffer.
    /// </summary>
    /// <returns>A <see cref="Block"/> value to close the open block with.</returns>
    public Block WriteBlock()
    {
        WriteLine("{");
        IncreaseIndent();

        return new(this);
    }

    /// <summary>
    /// Writes a line to the underlying buffer.
    /// </summary>
    public void WriteLine()
    {
        this.builder.Add(DefaultNewLine);
    }

    /// <summary>
    /// Writes content to the underlying buffer.
    /// </summary>
    /// <param name="content">The content to write.</param>
    /// <param name="isMultiline">Whether the input content is multiline.</param>
    public void WriteLine(string content, bool isMultiline = false)
    {
        WriteLine(content.AsSpan(), isMultiline);
    }

    /// <summary>
    /// Writes content to the underlying buffer.
    /// </summary>
    /// <param name="content">The content to write.</param>
    /// <param name="isMultiline">Whether the input content is multiline.</param>
    public void WriteLine(ReadOnlySpan<char> content, bool isMultiline = false)
    {
        if (isMultiline)
        {
            while (content.Length > 0)
            {
                int newLineIndex = content.IndexOf(DefaultNewLine);

                if (newLineIndex < 0)
                {
                    // There are no new lines left, so the content can be written as a single line
                    WriteSingleLine(content);
                }
                else
                {
                    // Write the current line
                    WriteSingleLine(content[..newLineIndex]);

                    // Move past the new line character (the result could be an empty span)
                    content = content[(newLineIndex + 1)..];
                }
            }
        }
        else
        {
            WriteSingleLine(content);
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.builder.ToString();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.builder.Dispose();
    }

    /// <summary>
    /// Writes a single line to the underlying buffer.
    /// </summary>
    /// <param name="content">The content to write.</param>
    private void WriteSingleLine(ReadOnlySpan<char> content)
    {
        if (this.builder.Count == 0 || this.builder.WrittenSpan[^1] == DefaultNewLine)
        {
            this.builder.AddRange(this.currentIndentation.AsSpan());
        }

        this.builder.AddRange(content);
    }

    /// <summary>
    /// Represents an indented block that needs to be closed.
    /// </summary>
    public struct Block : IDisposable
    {
        /// <summary>
        /// The <see cref="IndentedTextWriter"/> instance to write to.
        /// </summary>
        private IndentedTextWriter? writer;

        /// <summary>
        /// Creates a new <see cref="Block"/> instance with the specified parameters.
        /// </summary>
        /// <param name="writer">The input <see cref="IndentedTextWriter"/> instance to wrap.</param>
        public Block(IndentedTextWriter writer)
        {
            this.writer = writer;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            IndentedTextWriter? writer = this.writer;

            this.writer = null;

            if (writer is not null)
            {
                writer.DecreaseIndent();
                writer.WriteLine("}");
            }
        }
    }
}