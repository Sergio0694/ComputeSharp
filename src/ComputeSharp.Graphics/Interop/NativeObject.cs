using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Core.Interop
{
    /// <summary>
    /// Base class for a <see cref="IDisposable"/> class.
    /// </summary>
    public abstract class NativeObject : IDisposable
    {
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
        internal bool IsDisposed { get; private set; }

        /// <summary>
        /// Releases all the native resources for the current instance.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            CheckAndDispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        private void CheckAndDispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

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
