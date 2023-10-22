using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Win32;

namespace ComputeSharp.Graphics.Resources.Interop;

/// <summary>
/// A type representing a mapped memory resource.
/// </summary>
/// <param name="d3D12Resource">The input <see cref="ID3D12Resource"/> instance to map.</param>
/// <param name="pointer">The pointer to the mapped resource.</param>
[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
internal readonly unsafe ref struct ID3D12ResourceMap(ID3D12Resource* d3D12Resource, void* pointer)
{
    /// <summary>
    /// The pointer to the mapped resource.
    /// </summary>
    public readonly void* Pointer = pointer;

    /// <inheritdoc cref="IDisposable.Dispose"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        d3D12Resource->Unmap();
    }
}