namespace System.Collections.Generic;

/// <summary>
/// A helper type with extensions for the <see cref="Queue{T}"/> type.
/// </summary>
internal static class QueueExtensions
{
    /// <summary>
    /// Tries to dequeue an item from the input <see cref="Queue{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items in the queue.</typeparam>
    /// <param name="queue">The input queue.</param>
    /// <param name="result">The resulting item, if any.</param>
    /// <returns>Whether or not an item was found.</returns>
    public static bool TryDequeue<T>(this Queue<T> queue, out T? result)
    {
        if (queue.Count > 0)
        {
            result = queue.Dequeue();

            return true;
        }

        result = default!;

        return false;
    }
}