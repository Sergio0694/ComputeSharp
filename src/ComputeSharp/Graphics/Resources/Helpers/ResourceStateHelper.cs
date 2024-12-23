using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Win32;
using static ComputeSharp.Win32.D3D12_RESOURCE_STATES;

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
    /// <exception cref="ArgumentException">Thrown when the input value is not valid.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static D3D12_RESOURCE_STATES GetD3D12ResourceStates(ResourceState resourceState)
    {
        if (resourceState == ResourceState.ReadOnly)
        {
            // Transitioning to 'NON_PIXEL_SHADER_RESOURCE' here is intentional, for writeable textures. Non writeable textures
            // are created as 'D3D12_RESOURCE_STATE_COMMON', which should not be used in this scenario. Writable textures need
            // a state transition to 'NON_PIXEL_SHADER_RESOURCE' after being used as 'UNORDERED_ACCESS'. Readonly textures can
            // be created as 'COMMON', which is a shorthand for read-compatible states. The difference is intentional, and it
            // ensures correct usage and synchronization based on how resources are accessed.
            //
            // Transitioning to 'COMMON' instead of 'NON_PIXEL_SHADER_RESOURCE' might "work" due to leniency in the hardware or
            // driver implementation. However, it's not guaranteed behavior and could lead to undefined results in other
            // environments or under different conditions. To ensure portability and correctness, explicit and precise resource
            // states like 'NON_PIXEL_SHADER_RESOURCE' should be preferred, which is why the latter is being used here.
            return D3D12_RESOURCE_STATE_NON_PIXEL_SHADER_RESOURCE;
        }

        if (resourceState == ResourceState.ReadWrite)
        {
            return D3D12_RESOURCE_STATE_UNORDERED_ACCESS;
        }

        return default(ArgumentException).Throw<D3D12_RESOURCE_STATES>(nameof(resourceState));
    }
}