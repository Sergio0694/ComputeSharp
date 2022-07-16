using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ComputeSharp.Interop;

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
    /// <exception cref="ObjectDisposedException">Thrown if the current instance is disposed.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void ThrowIfDisposed()
    {
        if (IsDisposed)
        {
            // We can't use ThrowHelper here as we only want to invoke ToString if we
            // are about to throw an exception. The JIT will recognize this pattern
            // as this method has a single basic block that always throws an exception.
            static void Throw(object self) => throw new ObjectDisposedException(self.ToString());

            Throw(this);
        }
    }
}
