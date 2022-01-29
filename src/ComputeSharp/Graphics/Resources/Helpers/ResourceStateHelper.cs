using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;
using TerraFX.Interop.DirectX;
using static TerraFX.Interop.DirectX.D3D12_RESOURCE_STATES;

namespace ComputeSharp.Graphics.Resources.Helpers;

/// <summary>
/// A helper type with utility methods for <see cref="ResourceState"/>.
/// </summary>
internal static class ResourceStateHelper
{
    /// <summary>
    /// Gets the corresponding <see cref="D3D12_RESOURCE_STATES"/> value for the input resource state.
    /// </summary>
    /// <returns>The <see cref="D3D12_RESOURCE_STATES"/> value corresponding to the input resource state.</returns>
    /// <exception cref="System.ArgumentException">Thrown when the input value is not valid.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static D3D12_RESOURCE_STATES GetD3D12ResourceStates(ResourceState resourceState)
    {
        if (resourceState == ResourceState.ReadOnly)
        {
            return D3D12_RESOURCE_STATE_NON_PIXEL_SHADER_RESOURCE;
        }        
        
        if (resourceState == ResourceState.ReadWrite)
        {
            return D3D12_RESOURCE_STATE_UNORDERED_ACCESS;
        }

        return ThrowHelper.ThrowArgumentException<D3D12_RESOURCE_STATES>("The target resource state is not valid.");
    }
}
