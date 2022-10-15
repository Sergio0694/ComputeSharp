using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGeneration.Helpers;

/// <summary>
/// Helpers to create and work with <see cref="SyntaxToken"/>-s.
/// </summary>
internal static class SyntaxTokenHelper
{
    /// <summary>
    /// Creates a <see cref="SyntaxToken"/> representing a raw multiline string literal with the given text.
    /// </summary>
    /// <param name="text">The text to use for the created raw multiline string literal token.</param>
    /// <param name="indentationLevel">The current indentation level to use.</param>
    /// <returns>A <see cref="SyntaxToken"/> representing a raw multiline string literal with the value of <paramref name="text"/>.</returns>
    public static SyntaxToken CreateRawMultilineStringLiteral(string text, int indentationLevel)
    {
        // Create a token to represent the raw multiline string literal expression. Here some spaces are
        // also added to properly align the resulting text with one indentation below the declaring string constant.
        // The spaces are: 4 for each containing type, 4 for the containing method, and 4 for the one additional indentation.
        // An extra newline and indentation has to be added to the raw text when there is no trailing newline, as not doing
        // so would otherwise case the terminating """ token to fall on the same line, which is invalid syntax. This is only
        // needed to make the code valid, as the actual literal context of the string is not affected and remains the same.
        string indentation = new(' ', (4 * indentationLevel) + 4 + 4);

        return Token(
            TriviaList(),
            SyntaxKind.MultiLineRawStringLiteralToken,
            $"\"\"\"\n{indentation}{text.Replace("\n", $"\n{indentation}")}{(text.EndsWith("\n") ? "" : $"\n{indentation}")}\"\"\"",
            text,
            TriviaList());
    }
}