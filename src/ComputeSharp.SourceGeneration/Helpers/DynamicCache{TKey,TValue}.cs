using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ComputeSharp.SourceGeneration.Helpers;

/// <summary>
/// A dynamic cache that can be used to cache computed values within incremental models.
/// This automatically trims excess items, and relies on the incremental state tables
/// keeping values alive for at least one incremental step, in order to work correctly.
/// </summary>
/// <typeparam name="TKey">The type of keys to use for the cache.</typeparam>
/// <typeparam name="TValue">The type of values to store in the cache.</typeparam>
internal sealed class DynamicCache<TKey, TValue>
    where TKey : class
{
    /// <summary>
    /// The backing <see cref="ConcurrentDictionary{TKey, TValue}"/> instance for the cache.
    /// </summary>
    private readonly ConcurrentDictionary<Entry, TValue> map = [];

    /// <summary>
    /// The <see cref="ConditionalWeakTable{TKey, TValue}"/> tracking dead entries to remove.
    /// </summary>
    private readonly ConditionalWeakTable<TKey, EntryRemover> table = new();

    /// <summary>
    /// Gets or creates a new value for a given key, using a supplied callback if needed.
    /// </summary>
    /// <param name="key">The key to use as lookup.</param>
    /// <param name="callback">The callback to use to create new values, if needed.</param>
    /// <param name="cancellationToken">A cancellation token for the operation of creating a new value.</param>
    /// <returns>The resulting value.</returns>
    /// <exception cref="OperationCanceledException">Thrown if <paramref name="cancellationToken"/> is canceled.</exception>
    /// <remarks>
    /// This method might replace <typeparamref name="TKey"/> with a new instance that has the same
    /// value according to its equality comparison logic. Callers should always use the last value of
    /// <typeparamref name="TKey"/> after this method returns and discard the previous one, if different.
    /// </remarks>
    public TValue GetOrCreate(ref TKey key, GetOrCreateCallback callback, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Create a new entry that we will use to perform the lookup.
        // Each entry simply forwards equality logic to the wrapped object.
        Entry entry = new(key);

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // We're performing a lookup on this temporary entry. We need it to
            // track the value it will potentially match against, so we can
            // return it to the caller. This ensures the same object is used.
            entry.SetIsPerformingLookup(true);

            // Try to check whether we already have a value in the cache for
            // this object. That is, we want to check whether there is an entry
            // with a value equal to the one we have now (not necessarily the same
            // object). If we find it, we return it and throw away the new entry.
            if (this.map.TryGetValue(entry, out TValue? value))
            {
                cancellationToken.ThrowIfCancellationRequested();

                // We have a match, so replace the object with the one that actually matched.
                // This guarantees that it will remain alive, so the entry will not die.
                key = entry.GetLastMatchedValue();

                return value;
            }

            cancellationToken.ThrowIfCancellationRequested();

            // Execute the slow fallback path, invoking the callback and trying to add a new
            // value to the cache. If this succeeds, it means that the current key object has
            // been added to the cache, so there is nothing left to do. If this fails, it means
            // another thread has beat us to adding the key, so we should perform the initial
            // lookup again to make sure we can find the exact instance that is in the cache.
            // This is needed to ensure valid cache entries remain alive over time.
            if (TryGetOrCreate(entry, key, callback, cancellationToken, out value))
            {
                // If the operation has been canceled after inserting a new key and value,
                // the value will just be wasted, but there isn't really anything we can do
                // about it at this point. We also ignore the unnecessary remover finalizer.
                cancellationToken.ThrowIfCancellationRequested();

                return value;
            }
        }
    }

    /// <summary>
    /// Tries to get or create a new value for a given key, using a supplied callback.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/> instance to try to insert into the cache.</param>
    /// <param name="key">The key to use as lookup.</param>
    /// <param name="callback">The callback to use to create new values, if needed.</param>
    /// <param name="cancellationToken">A cancellation token for the operation of creating a new value.</param>
    /// <param name="value">The resulting value (should be ignored if the method fails).</param>
    /// <returns>Whether <paramref name="entry"/> was successfully inserted into the cache.</returns>
    /// <exception cref="OperationCanceledException">Thrown if <paramref name="cancellationToken"/> is canceled.</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private bool TryGetOrCreate(
        Entry entry,
        TKey key,
        GetOrCreateCallback callback,
        CancellationToken cancellationToken,
        out TValue value)
    {
        // No value is present, so we can create it now
        value = callback(key, cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        // We're about to try to add the item to the cache, so we no longer need to
        // track the last matched value. We already have a reference to the object.
        entry.SetIsPerformingLookup(false);

        // Try to add the value (this only fails if someone raced with this thread).
        // In this case, our key will also be added to the cache. Callers will have
        // a strong reference to it, which ensures the weak reference remains alive.
        if (!this.map.TryAdd(entry, value))
        {
            return false;
        }

        // We need to setup the removal of this key-value pair when the key is no longer
        // referenced. To do this, we add a new EntryRemover instance to the table. When
        // the key has no active references and is collected (which means the entry will
        // also become invalid), the finalizer of the EntryRemover instance will run on
        // a following GC, and remove that dead Entry instance from the map automatically.
        this.table.Add(key, new EntryRemover(this.map, entry));

        return true;
    }

    /// <summary>
    /// A callback to create a new value from a given key.
    /// </summary>
    /// <param name="key">The resulting <typeparamref name="TValue"/> instance.</param>
    /// <param name="cancellationToken">A cancellation token for the operation of creating a new value.</param>
    /// <returns></returns>
    public delegate TValue GetOrCreateCallback(TKey key, CancellationToken cancellationToken);

    /// <summary>
    /// An entry to use in <see cref="DynamicCache{TKey, TValue}"/>.
    /// </summary>
    /// <param name="key">The key to use for the entry.</param>
    private sealed class Entry(TKey key)
    {
        /// <summary>
        /// A weak reference to the actual entry instance.
        /// </summary>
        private readonly WeakReference<TKey> reference = new(key);

        /// <summary>
        /// The hashcode of the target key (so it's available even after the key is gone).
        /// </summary>
        private readonly int hashCode = EqualityComparer<TKey>.Default.GetHashCode(key);

        /// <summary>
        /// The last key matched from <see cref="Equals(object?)"/>, if available.
        /// </summary>
        private TKey? lastMatchedKey;

        /// <summary>
        /// Indicates whether the entry is currently in lookup mode.
        /// </summary>
        private bool isPerformingLookup;

        /// <summary>
        /// Gets whether or not the current entry is alive.
        /// </summary>
        public bool IsAlive => this.reference.TryGetTarget(out _);

        /// <summary>
        /// Gets the last matched key retrieved during a lookup operation.
        /// </summary>
        /// <returns>The last matched key retrieved during a lookup operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no key is available.</exception>
        public TKey GetLastMatchedValue()
        {
            TKey? lastMatchedKey = this.lastMatchedKey;

            if (lastMatchedKey is null)
            {
                EntryHelper.ThrowInvalidOperationExceptionForLastMatchedKey();
            }

            return lastMatchedKey;
        }

        /// <summary>
        /// Sets whether the current instance is performing a lookup.
        /// </summary>
        /// <param name="value">The new value for the configuration.</param>
        public void SetIsPerformingLookup(bool value)
        {
            this.lastMatchedKey = null;
            this.isPerformingLookup = value;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is not Entry entry)
            {
                return false;
            }

            // Special case matching on the entry identity directly. This is used
            // by the finalizer of EntryRemover to find the entry to remove.
            if (this == entry)
            {
                return true;
            }

            _ = this.reference.TryGetTarget(out TKey? left);
            _ = entry.reference.TryGetTarget(out TKey? right);

            bool isMatch = EqualityComparer<TKey>.Default.Equals(left, right);

            // If we have a match and we're in lookup mode, store the last item. Note that the dictionary
            // does not guarantee the order of arguments to Equals calls, so we cannot rely on the Entry
            // object being used for lookup the one whose Equals method is being called. So if there is
            // a match, we check which of the two input Entry instances is the one being used for lookups,
            // and set the last match key object on that one. If there is no match, there is no need to
            // clear the last key, as even in case the entry ends up being inserted into the map in the
            // falback path, before doing so SetIsPerformingLookup(false) will be called, which will clear
            // any previous matches. So there is already no way for the key to be accidentally rooted here.
            if (isMatch)
            {
                if (this.isPerformingLookup)
                {
                    this.lastMatchedKey = right;
                }
                else if (entry.isPerformingLookup)
                {
                    entry.lastMatchedKey = left;
                }
            }

            return isMatch;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.hashCode;
        }
    }

    /// <summary>
    /// An object reponsible for removing dead <see cref="Entry"/> instance from the table.
    /// </summary>
    /// <remarks>
    /// Creates a new <see cref="EntryRemover"/> instance with the specified parameters.
    /// </remarks>
    /// <param name="map"><inheritdoc cref="DynamicCache{TKey, TValue}.map" path="/node()"/></param>
    /// <param name="entry">The target <see cref="Entry"/> instance to remove from <see cref="map"/>.</param>
    private sealed class EntryRemover(ConcurrentDictionary<Entry, TValue> map, Entry entry)
    {
        /// <summary>
        /// Removes the target entry from the map when the current instance is finalized.
        /// </summary>
        ~EntryRemover()
        {
            _ = map.TryRemove(entry, out _);
        }
    }
}

/// <summary>
/// Private helpers for the <see cref="DynamicCache{TKey, TValue}.Entry"/> type.
/// </summary>
file static class EntryHelper
{
    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> when there is no last matching key.
    /// </summary>
    [DoesNotReturn]
    public static void ThrowInvalidOperationExceptionForLastMatchedKey()
    {
        throw new InvalidOperationException("No last matching key has been found.");
    }
}