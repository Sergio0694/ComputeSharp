using System;
using System.Diagnostics;
#if NET6_0_OR_GREATER
using ComputeSharp.Core.Extensions;
#endif
using ComputeSharp.__Internals;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using ComputeSharp.Shaders.Models;

#pragma warning disable CS0618

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that represents an <see cref="ID3D12Device"/> instance that can be used to run compute shaders.
/// </summary>
[DebuggerDisplay("{ToString(),raw}")]
public abstract unsafe partial class GraphicsDevice : IReferenceTrackedObject
{
    internal abstract unsafe void CreatePipelineData<T>(ICachedShader shaderData, out PipelineData pipelineData)
        where T : struct, IShader;

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if the current device has been lost.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the current device has been lost.</exception>
    internal abstract void ThrowIfDeviceLost();

    /// <summary>
    /// The <see cref="ReferenceTracker"/> value for the current instance.
    /// </summary>
    private protected ReferenceTracker referenceTracker;

    /// <summary>
    /// Gets the locally unique identifier for the current device.
    /// </summary>
    public Luid Luid { get; protected set; }

    /// <summary>
    /// Gets the name of the current <see cref="GraphicsDevice"/> instance.
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Gets the size of the dedicated memory for the current device.
    /// </summary>
    public nuint DedicatedMemorySize { get; protected set; }

    /// <summary>
    /// Gets the size of the shared system memory for the current device.
    /// </summary>
    public nuint SharedMemorySize { get; protected set; }

    /// <summary>
    /// Gets whether or not the current device is hardware accelerated.
    /// This value is <see langword="false"/> for software fallback devices.
    /// </summary>
    public bool IsHardwareAccelerated { get; protected set; }

    /// <summary>
    /// Gets the number of total lanes on the current device (eg. CUDA cores on an nVidia GPU).
    /// </summary>
    public uint ComputeUnits { get; protected set; }

    /// <summary>
    /// Gets the number of lanes in a SIMD wave on the current device (also known as "wavefront size" or "warp width").
    /// </summary>
    public uint WavefrontSize { get; protected set; }

    /// <summary>
    /// Gets whether or not the current device has a cache coherent UMA architecture.
    /// </summary>
    internal bool IsCacheCoherentUMA { get; }


    /// <summary>
    /// Checks whether the current device supports double precision floating point operations in shaders.
    /// </summary>
    /// <returns>Whether the current device supports double precision floating point operations in shaders.</returns>
    public abstract bool IsDoublePrecisionSupportAvailable();

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadOnlyTexture1D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadOnlyTexture1D{T}"/> instances can be created by the current device.</returns>
    public abstract bool IsReadOnlyTexture1DSupportedForType<T>()
        where T : unmanaged;

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadWriteTexture1D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadWriteTexture1D{T}"/> instances can be created by the current device.</returns>
    public abstract bool IsReadWriteTexture1DSupportedForType<T>()
        where T : unmanaged;

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadOnlyTexture2D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadOnlyTexture2D{T}"/> instances can be created by the current device.</returns>
    public abstract bool IsReadOnlyTexture2DSupportedForType<T>()
        where T : unmanaged;

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadWriteTexture2D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadWriteTexture2D{T}"/> instances can be created by the current device.</returns>
    public abstract bool IsReadWriteTexture2DSupportedForType<T>()
        where T : unmanaged;

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadOnlyTexture3D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadOnlyTexture3D{T}"/> instances can be created by the current device.</returns>
    public abstract bool IsReadOnlyTexture3DSupportedForType<T>()
        where T : unmanaged;

    /// <summary>
    /// Checks whether the current device supports the creation of
    /// <see cref="ReadWriteTexture3D{T}"/> resources for a specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values to check support for.</typeparam>
    /// <returns>Whether <see cref="ReadWriteTexture3D{T}"/> instances can be created by the current device.</returns>
    public abstract bool IsReadWriteTexture3DSupportedForType<T>()
        where T : unmanaged;

    void IReferenceTrackedObject.DangerousOnDispose()
    {

    }

    /// <summary>
    /// Registers that a new resource has been allocated on the current device.
    /// </summary>
    internal void RegisterAllocatedResource()
    {
#if NET6_0_OR_GREATER
        this.pool.AddRef();
        this.allocator.AddRef();
#endif
    }

    /// <summary>
    /// Unregisters a generic resource that was allocated on the current device.
    /// </summary>
    internal void UnregisterAllocatedResource()
    {
#if NET6_0_OR_GREATER
        this.pool.Release();
        this.allocator.Release();
#endif
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[{Luid}] {Name}";
    }
}