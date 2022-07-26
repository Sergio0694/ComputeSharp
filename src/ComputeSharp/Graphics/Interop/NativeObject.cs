using System;

namespace ComputeSharp.Interop;

/// <summary>
/// Base class for a <see cref="IDisposable"/> class.
/// </summary>
public abstract partial class NativeObject : IDisposable
{
    /// <summary>
    /// An object to use to synchronize the reference tracking operations.
    /// </summary>
    private readonly object lockObject;

    /// <summary>
    /// The number of existing reference tracking leases for the current instance.
    /// </summary>
    private int leasesCount;

    /// <summary>
    /// Whether or not <see cref="Dispose"/> has been called on the current instance.
    /// </summary>
    private bool isDisposeRequested;

    /// <summary>
    /// Creates a new <see cref="NativeObject"/> instance.
    /// </summary>
    private protected NativeObject()
    {
        this.lockObject = new object();
        this.leasesCount = 0;
        this.isDisposeRequested = false;
    }

    /// <summary>
    /// Releases unmanaged resources and performs other cleanup operations.
    /// </summary>
    ~NativeObject()
    {
        RequestDispose();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        RequestDispose();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases all resources (including unmanaged ones) for the tracked object.
    /// </summary>
    /// <remarks>This method is guaranteed to only ever be called once for a given tracked object.</remarks>
    private protected abstract void OnDispose();
}
