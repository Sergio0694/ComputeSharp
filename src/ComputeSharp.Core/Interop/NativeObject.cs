using System;

namespace ComputeSharp.Core.Interop
{
    /// <summary>
    /// Base class for a <see cref="IDisposable"/> class.
    /// </summary>
    public abstract class NativeObject : IDisposable
    {
        /// <summary>
        /// Indicates whether or not the current instance has already been disposed.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~NativeObject()
        {
            CheckAndDispose(false);
        }

        /// <summary>
        /// Releases all the native resources for the current instance.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            CheckAndDispose(true);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        private void CheckAndDispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                this.isDisposed = true;

                Dispose(disposing);
            }
        }

        /// <summary>
        /// Releases unmanaged and (optionally) managed resources.
        /// </summary>
        /// <param name="disposing">When set to <see langword="true"/>, indicates to dispose managed resources as well.</param>
        protected abstract void Dispose(bool disposing);
    }
}
