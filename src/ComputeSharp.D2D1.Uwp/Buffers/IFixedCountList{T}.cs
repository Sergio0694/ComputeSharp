using System.Collections;
using System.Collections.Generic;

namespace ComputeSharp.D2D1.Uwp.Buffers;

/// <summary>
/// An interface for a list with a fixed collection.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
interface IFixedCountList<T>
{
    /// <inheritdoc cref="ICollection.Count"/>
    int Count { get; }

    /// <inheritdoc cref="IList{T}.this"/>
    T this[int index] { get; set; }
}