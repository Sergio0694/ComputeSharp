using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS0618

namespace ComputeSharp.WinUI.Extensions;

/// <summary>
/// A helper type for <see cref="SemaphoreSlim"/>.
/// </summary>
internal static class SemaphoreSlimExtensions
{
    /// <summary>
    /// Acquires a lock for the current instance, that is automatically released outside the <see langword="using"/> block.
    /// </summary>
    public static async ValueTask<Lock> LockAsync(this SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync().ConfigureAwait(false);

        return new(semaphore);
    }

    /// <summary>
    /// Private class that implements the automatic release of the semaphore
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct Lock : IDisposable
    {
        /// <summary>
        /// The underlying semaphore used by this instance.
        /// </summary>
        private readonly SemaphoreSlim semaphore;

        /// <summary>
        /// Creates a new <see cref="Lock"/> instance.
        /// </summary>
        /// <param name="semaphore">The <see cref="SemaphoreSlim"/> instance to wrap.</param>
        [Obsolete("Use AsyncMutex.LockAsync instead.")]
        public Lock(SemaphoreSlim semaphore)
        {
            this.semaphore = semaphore;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _ = this.semaphore.Release();
        }
    }
}