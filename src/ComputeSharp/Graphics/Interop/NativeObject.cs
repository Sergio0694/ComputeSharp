using System;

#pragma warning disable CA1063

namespace ComputeSharp.Interop;

/// <summary>
/// Base class for a <see cref="IDisposable"/> class.
/// </summary>
public abstract partial class NativeObject : IDisposable
{
    /// <summary>
    /// A mask that indicates the state of the current object.
    /// The mask uses the following schema:
    /// <list type="bullet">
    ///     <item>[0, 30]: the number of existing reference tracking leases.</item>
    ///     <item>[31]: whether or not <see cref="Dispose"/> has been called.</item>
    /// </list>
    /// </summary>
    private volatile int referenceTrackingMask;

    /// <summary>
    /// Creates a new <see cref="NativeObject"/> instance.
    /// </summary>
    private protected NativeObject()
    {
        this.referenceTrackingMask = 0;
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