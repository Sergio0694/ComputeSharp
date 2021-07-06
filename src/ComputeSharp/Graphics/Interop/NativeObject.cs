using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ComputeSharp.Interop
{
    /// <summary>
    /// Base class for a <see cref="IDisposable"/> class.
    /// </summary>
    public abstract class NativeObject : IDisposable
    {
        /// <summary>
        /// Indicates whether or not the current instance has been disposed.
        /// </summary>
        private volatile int isDisposed;

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~NativeObject()
        {
            CheckAndDispose();
        }

        /// <summary>
        /// Gets whether or not the current instance has already been disposed.
        /// </summary>
        internal bool IsDisposed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.isDisposed != 0;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            CheckAndDispose();

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckAndDispose()
        {
            if (Interlocked.CompareExchange(ref this.isDisposed, 1, 0) == 0)
            {
                OnDispose();
            }
        }

        /// <summary>
        /// Releases unmanaged and (optionally) managed resources.
        /// </summary>
        protected abstract void OnDispose();

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if the current instance has been disposed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                // We can't use ThrowHelper here as we only want to invoke ToString if we
                // are about to throw an exception. The JIT will recognize this pattern
                // as this method has a single basic block that always throws an exception.
                void Throw() => throw new ObjectDisposedException(ToString());

                Throw();
            }
        }
    }
}
