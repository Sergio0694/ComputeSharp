using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CA1416

namespace ComputeSharp.Graphics.Helpers;

/// <inheritdoc cref="DeviceHelper"/>
internal static partial class DeviceHelper
{
    /// <summary>
    /// The creation flags for <see cref="IDXGIFactory"/> instances.
    /// </summary>
    private const uint IDXGIFactoryCreationFlags =
#if DEBUG
        DXGI.DXGI_CREATE_FACTORY_DEBUG;
#else
        0;
#endif

    /// <summary>
    /// Creates a new <see cref="IDXGIFactory6"/> instance to be used to enumerate devices.
    /// </summary>
    /// <param name="dxgiFactory6">The resulting <see cref="IDXGIFactory6"/> instance.</param>
    private static unsafe void CreateDXGIFactory6(IDXGIFactory6** dxgiFactory6)
    {
        using ComPtr<IDXGIFactory4> dxgiFactory4 = default;

        EnableDebugMode();

        DirectX.CreateDXGIFactory2(IDXGIFactoryCreationFlags, Windows.__uuidof<IDXGIFactory4>(), dxgiFactory4.GetVoidAddressOf()).Assert();

        HRESULT result = dxgiFactory4.CopyTo(dxgiFactory6);

        if (result == S.S_OK)
        {
            return;
        }

        if (result == E.E_NOINTERFACE)
        {
            IDXGIFactory4As6Backcompat.Create(dxgiFactory4.Get(), dxgiFactory6);

            return;
        }

        result.Assert();

        return;
    }

    /// <summary>
    /// Enables the debug layer for DirectX APIs.
    /// </summary>
    [Conditional("DEBUG")]
    private static unsafe void EnableDebugMode()
    {
        using ComPtr<ID3D12Debug> d3D12Debug = default;
        using ComPtr<ID3D12Debug1> d3D12Debug1 = default;

        DirectX.D3D12GetDebugInterface(Windows.__uuidof<ID3D12Debug>(), d3D12Debug.GetVoidAddressOf()).Assert();

        d3D12Debug.Get()->EnableDebugLayer();

        if (Windows.SUCCEEDED(d3D12Debug.CopyTo(d3D12Debug1.GetAddressOf())))
        {
            d3D12Debug1.Get()->SetEnableGPUBasedValidation(Windows.TRUE);
            d3D12Debug1.Get()->SetEnableSynchronizedCommandQueueValidation(Windows.TRUE);
        }
    }

    /// <summary>
    /// A custom <see cref="IDXGIFactory6"/> fallback implementation to use on systems with no support for it.
    /// </summary>
    private unsafe struct IDXGIFactory4As6Backcompat
    {
        /// <summary>
        /// The shared method table pointer for all <see cref="IDXGIFactory4As6Backcompat"/> instances.
        /// </summary>
        private static readonly void** Vtbl = InitVtbl();

        /// <summary>
        /// Builds the custom method table pointer for <see cref="IDXGIFactory4As6Backcompat"/>.
        /// </summary>
        /// <returns>The method table pointer for <see cref="IDXGIFactory4As6Backcompat"/>.</returns>
        private static void** InitVtbl()
        {
            void** lpVtbl = (void**)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(IDXGIFactory4As6Backcompat), sizeof(void*) * 30);

            new Span<IntPtr>(lpVtbl, 30).Clear();

            lpVtbl[2] = (delegate* unmanaged<IDXGIFactory4As6Backcompat*, uint>)&Release;
            lpVtbl[27] = (delegate* unmanaged<IDXGIFactory4As6Backcompat*, Guid*, void**, int>)&EnumWarpAdapter;
            lpVtbl[29] = (delegate* unmanaged<IDXGIFactory4As6Backcompat*, uint, DXGI_GPU_PREFERENCE, Guid*, void**, int>)&EnumAdapterByGpuPreference;

            return lpVtbl;
        }

        /// <summary>
        /// The method table pointer for the current instance.
        /// </summary>
        private void** lpVtbl;

        /// <summary>
        /// The wrapped <see cref="IDXGIFactory4"/> instance.
        /// </summary>
        private IDXGIFactory4* dxgiFactory4;

        /// <summary>
        /// Creates and initializes a new <see cref="IDXGIFactory4As6Backcompat"/> instance.
        /// </summary>
        /// <param name="dxgiFactory4">The <see cref="IDXGIFactory4"/> instance to wrap.</param>
        /// <param name="dxgiFactory6">The resulting <see cref="IDXGIFactory6"/> instance.</param>
        public static void Create(IDXGIFactory4* dxgiFactory4, IDXGIFactory6** dxgiFactory6)
        {
            IDXGIFactory4As6Backcompat* @this = (IDXGIFactory4As6Backcompat*)Marshal.AllocHGlobal(sizeof(IDXGIFactory4As6Backcompat));

            @this->lpVtbl = Vtbl;
            @this->dxgiFactory4 = dxgiFactory4;

            *dxgiFactory6 = (IDXGIFactory6*)@this;
        }

        /// <inheritdoc cref="IUnknown.Release"/>
        [UnmanagedCallersOnly]
        public static uint Release(IDXGIFactory4As6Backcompat* @this)
        {
            @this->dxgiFactory4->Release();

            Marshal.FreeHGlobal((IntPtr)@this);

            return 0;
        }

        /// <inheritdoc cref="IDXGIFactory6.EnumWarpAdapter(Guid*, void**)"/>
        [UnmanagedCallersOnly]
        public static int EnumWarpAdapter(IDXGIFactory4As6Backcompat* @this, Guid* riid, void** ppvAdapter)
        {
            return @this->dxgiFactory4->EnumWarpAdapter(riid, ppvAdapter);
        }

        /// <inheritdoc cref="IDXGIFactory6.EnumAdapterByGpuPreference(uint, DXGI_GPU_PREFERENCE, Guid*, void**)"/>
        [UnmanagedCallersOnly]
        public static int EnumAdapterByGpuPreference(IDXGIFactory4As6Backcompat* @this, uint Adapter, DXGI_GPU_PREFERENCE GpuPreference, Guid* riid, void** ppvAdapter)
        {
            return @this->dxgiFactory4->EnumAdapters1(Adapter, (IDXGIAdapter1**)ppvAdapter);
        }
    }
}
