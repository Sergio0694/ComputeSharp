using System;
using System.Runtime.CompilerServices;
using System.Threading;
using ComputeSharp.Graphics.Helpers;

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
            if (Interlocked.CompareExchange(ref this.isDisposed, 1, 0) == 0)
            {
                OnDispose();
            }
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            // If the current instance is a graphics device, check that the LUID doesn't
            // match the one for the default device, which is deliberately never disposed.
            // This type check is inlined and then resolved at JIT time if the target instance
            // is visible to the compiler. That is, if Dispose() is explicitly called on a
            // sealed type that is not GraphicsDevice, this entire path will just be removed.
            if (GetType() == typeof(GraphicsDevice) &&
                DeviceHelper.GetDefaultDeviceLuid() == Unsafe.As<GraphicsDevice>(this).Luid)
            {
                return;
            }

            if (Interlocked.CompareExchange(ref this.isDisposed, 1, 0) == 0)
            {
                [MethodImpl(MethodImplOptions.NoInlining)]
                void DisposeAndSuppressFinalize()
                {
                    OnDispose();

                    GC.SuppressFinalize(this);
                }

                DisposeAndSuppressFinalize();
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
