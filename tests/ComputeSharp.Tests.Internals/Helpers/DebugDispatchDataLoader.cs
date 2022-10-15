using System;
using System.Runtime.CompilerServices;
using ComputeSharp.__Internals;

#pragma warning disable CS0618

namespace ComputeSharp.Tests.Internals.Helpers;

/// <summary>
/// A debug data loader for generic shaders.
/// </summary>
internal readonly unsafe struct DebugDispatchDataLoader : IDispatchDataLoader
{
    /// <summary>
    /// The backing, mutable data for <see cref="Values"/> and <see cref="Resources"/>.
    /// </summary>
    private readonly StrongBox<(uint[] Values, ulong[] Resources)> data;

    /// <summary>
    /// Gets the captured values.
    /// </summary>
    public uint[] Values => this.data.Value.Values;

    /// <summary>
    /// Gets the captured resources.
    /// </summary>
    public ulong[] Resources => this.data.Value.Resources;

    /// <summary>
    /// Creates a new <see cref="DebugDispatchDataLoader"/> instance.
    /// </summary>
    /// <returns>A new <see cref="DebugDispatchDataLoader"/> instance to use.</returns>
    public static DebugDispatchDataLoader Create()
    {
        DebugDispatchDataLoader @this = default;

        Unsafe.AsRef(in @this.data) = new StrongBox<(uint[], ulong[])>((Array.Empty<uint>(), Array.Empty<ulong>()));

        return @this;
    }

    /// <inheritdoc/>
    public void LoadCapturedValues(ReadOnlySpan<uint> data)
    {
        this.data.Value.Values = data.ToArray();
    }

    /// <inheritdoc/>
    public void LoadCapturedResources(ReadOnlySpan<ulong> data)
    {
        this.data.Value.Resources = data.ToArray();
    }
}