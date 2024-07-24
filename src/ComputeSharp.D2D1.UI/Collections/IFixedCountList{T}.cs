using System.Collections.Generic;
using System.Collections.Immutable;

namespace ComputeSharp.D2D1.WinUI.Collections;

/// <summary>
/// An interface for a list with a fixed collection.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
internal interface IFixedCountList<T>
{
    /// <summary>
    /// Gets the collection of valid indices for the current effect.
    /// </summary>
    ImmutableArray<int> Indices { get; }

    /// <inheritdoc cref="IList{T}.this"/>
    T this[int index] { get; set; }
}