using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ComputeSharp.SourceGeneration.Models;

/// <summary>
/// A model for a captured source location, to be used within equatable incremental models.
/// The location is captured by value (ie. with no <see cref="SyntaxTree"/> references), so
/// that models with a captured location will correctly compare as equal across unrelated
/// edits (and so that they will never keep alive (or leak) any stale compilation objects).
/// </summary>
/// <param name="FilePath">The path of the source file for the referenced location.</param>
/// <param name="TextSpan">The span for the referenced location.</param>
/// <param name="LineSpan">The line span for the referenced location.</param>
internal sealed record LocationInfo(string FilePath, TextSpan TextSpan, LinePositionSpan LineSpan)
{
    /// <summary>
    /// Creates a new <see cref="LocationInfo"/> instance from an input <see cref="Location"/> value.
    /// </summary>
    /// <param name="location">The <see cref="Location"/> value to capture, if available.</param>
    /// <returns>A <see cref="LocationInfo"/> instance for <paramref name="location"/>, if a source location was available.</returns>
    public static LocationInfo? From(Location? location)
    {
        if (location is not { SourceTree: not null })
        {
            return null;
        }

        FileLinePositionSpan lineSpan = location.GetLineSpan();

        return new LocationInfo(lineSpan.Path, location.SourceSpan, lineSpan.Span);
    }

    /// <summary>
    /// Creates a new <see cref="LocationInfo"/> instance from an input <see cref="ISymbol"/> value.
    /// </summary>
    /// <param name="symbol">The <see cref="ISymbol"/> instance to capture the location for.</param>
    /// <returns>A <see cref="LocationInfo"/> instance for <paramref name="symbol"/>, if a source location was available.</returns>
    public static LocationInfo? From(ISymbol symbol)
    {
        return From(symbol.Locations.FirstOrDefault());
    }

    /// <summary>
    /// Creates a new <see cref="Location"/> instance with the state from this model.
    /// </summary>
    /// <returns>A new <see cref="Location"/> instance with the state from this model.</returns>
    public Location ToLocation()
    {
        return Location.Create(FilePath, TextSpan, LineSpan);
    }
}
