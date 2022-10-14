namespace System.Collections.Generic;

/// <summary>
/// A helper type with extensions for the <see cref="Dictionary{TKey, TValue}"/> type.
/// </summary>
internal static class DictionaryExtensions
{
    /// <summary>
    /// Tries to remove an item from a <see cref="Dictionary{TKey, TValue}"/> instance and returns it if present.
    /// </summary>
    /// <typeparam name="TKey">The type of dictionary keys.</typeparam>
    /// <typeparam name="TValue">The type of dictionary values.</typeparam>
    /// <param name="dictionary">The input <see cref="Dictionary{TKey, TValue}"/> instance.</param>
    /// <param name="key">The lookup key to use.</param>
    /// <param name="value">The resulting item, if present.</param>
    /// <returns>Whether or not an item was found with the given key.</returns>
    public static bool Remove<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, out TValue? value)
        where TKey : notnull
    {
        if (dictionary.TryGetValue(key, out value))
        {
            _ = dictionary.Remove(key);

            return true;
        }

        return false;
    }
}