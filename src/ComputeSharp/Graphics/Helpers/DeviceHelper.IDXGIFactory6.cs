using System;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
#if NET6_0_OR_GREATER
using RuntimeHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RuntimeHelpers = ComputeSharp.NetStandard.System.Runtime.CompilerServices.RuntimeHelpers;
#endif

#pragma warning disable CA1416

namespace ComputeSharp.Graphics.Helpers;

/// <inheritdoc cref="DeviceHelper"/>
partial class DeviceHelper
{
    /// <summary>
    /// The creation flags for <see cref="IDXGIFactory"/> instances.
    /// </summary>
    private static readonly uint IDXGIFactoryCreationFlags =
#if DEBUG
        DXGI.DXGI_CREATE_FACTORY_DEBUG;
#else
        Configuration.IsDebugOutputEnabled ? (uint)DXGI.DXGI_CREATE_FACTORY_DEBUG : 0;
#endif

    /// <summary>
    /// Creates a new <see cref="IDXGIFactory6"/> instance to be used to enumerate devices.
    /// </summary>
    /// <param name="dxgiFactory6">The resulting <see cref="IDXGIFactory6"/> instance.</param>
    internal static unsafe void CreateDXGIFactory6(IDXGIFactory6** dxgiFactory6)
    {
        if (Configuration.IsDebugOutputEnabled)
        {
            EnableDebugMode();
        }

        if (Configuration.IsDeviceRemovedExtendedDataEnabled)
        {
            EnableDeviceRemovedExtendedDataConfiguration();
        }

        using ComPtr<IDXGIFactory4> dxgiFactory4 = default;

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
    /// Enables the DRED settings are enabled (see <see href="https://devblogs.microsoft.com/directx/dred/"/>).
    /// </summary>
    private static unsafe void EnableDeviceRemovedExtendedDataConfiguration()
    {
        using ComPtr<ID3D12DeviceRemovedExtendedDataSettings1> d3D12DeviceRemovedExtendedDataSettings = default;

        DirectX.D3D12GetDebugInterface(
            riid: Windows.__uuidof<ID3D12DeviceRemovedExtendedDataSettings1>(),
            ppvDebug: d3D12DeviceRemovedExtendedDataSettings.GetVoidAddressOf()).Assert();

        // Enable the auto-breadcrumbs and page faults reporting
        d3D12DeviceRemovedExtendedDataSettings.Get()->SetAutoBreadcrumbsEnablement(D3D12_DRED_ENABLEMENT.D3D12_DRED_ENABLEMENT_FORCED_ON);
        d3D12DeviceRemovedExtendedDataSettings.Get()->SetBreadcrumbContextEnablement(D3D12_DRED_ENABLEMENT.D3D12_DRED_ENABLEMENT_FORCED_ON);
        d3D12DeviceRemovedExtendedDataSettings.Get()->SetPageFaultEnablement(D3D12_DRED_ENABLEMENT.D3D12_DRED_ENABLEMENT_FORCED_ON);
    }

    /// <summary>
    /// A custom <see cref="IDXGIFactory6"/> fallback implementation to use on systems with no support for it.
    /// </summary>
    private unsafe struct IDXGIFactory4As6Backcompat
    {
#if !NET6_0_OR_GREATER
        /// <inheritdoc cref="Release"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate uint ReleaseDelegate(IDXGIFactory4As6Backcompat* @this);

        /// <inheritdoc cref="EnumAdapters"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int EnumAdaptersDelegate(IDXGIFactory4As6Backcompat* @this, uint Adapter, IDXGIAdapter** ppAdapter);

        /// <inheritdoc cref="EnumWarpAdapter"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int EnumWarpAdapterDelegate(IDXGIFactory4As6Backcompat* @this, Guid* riid, void** ppvAdapter);

        /// <inheritdoc cref="EnumAdapterByGpuPreference"/>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int EnumAdapterByGpuPreferenceDelegate(IDXGIFactory4As6Backcompat* @this, uint Adapter, DXGI_GPU_PREFERENCE GpuPreference, Guid* riid, void** ppvAdapter);

        /// <summary>
        /// A cached <see cref="ReleaseDelegate"/> instance wrapping <see cref="Release"/>.
        /// </summary>
        private static readonly ReleaseDelegate ReleaseWrapper = Release;

        /// <summary>
        /// A cached <see cref="EnumAdaptersDelegate"/> instance wrapping <see cref="EnumAdapters"/>.
        /// </summary>
        private static readonly EnumAdaptersDelegate EnumAdaptersWrapper = EnumAdapters;

        /// <summary>
        /// A cached <see cref="EnumWarpAdapterDelegate"/> instance wrapping <see cref="EnumWarpAdapter"/>.
        /// </summary>
        private static readonly EnumWarpAdapterDelegate EnumWarpAdapterWrapper = EnumWarpAdapter;

        /// <summary>
        /// A cached <see cref="EnumAdapterByGpuPreferenceDelegate"/> instance wrapping <see cref="EnumAdapterByGpuPreference"/>.
        /// </summary>
        private static readonly EnumAdapterByGpuPreferenceDelegate EnumAdapterByGpuPreferenceWrapper = EnumAdapterByGpuPreference;
#endif

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

#if NET6_0_OR_GREATER
            lpVtbl[2] = (delegate* unmanaged<IDXGIFactory4As6Backcompat*, uint>)&Release;
            lpVtbl[7] = (delegate* unmanaged<IDXGIFactory4As6Backcompat*, uint, IDXGIAdapter**, int>)&EnumAdapters;
            lpVtbl[27] = (delegate* unmanaged<IDXGIFactory4As6Backcompat*, Guid*, void**, int>)&EnumWarpAdapter;
            lpVtbl[29] = (delegate* unmanaged<IDXGIFactory4As6Backcompat*, uint, DXGI_GPU_PREFERENCE, Guid*, void**, int>)&EnumAdapterByGpuPreference;
#else
            lpVtbl[2] = (void*)Marshal.GetFunctionPointerForDelegate(ReleaseWrapper);
            lpVtbl[7] = (void*)Marshal.GetFunctionPointerForDelegate(EnumAdaptersWrapper);
            lpVtbl[27] = (void*)Marshal.GetFunctionPointerForDelegate(EnumWarpAdapterWrapper);
            lpVtbl[29] = (void*)Marshal.GetFunctionPointerForDelegate(EnumAdapterByGpuPreferenceWrapper);
#endif

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
            IDXGIFactory4As6Backcompat* @this = (IDXGIFactory4As6Backcompat*)NativeMemory.Alloc((nuint)sizeof(IDXGIFactory4As6Backcompat));

            @this->lpVtbl = Vtbl;
            @this->dxgiFactory4 = dxgiFactory4;

            _ = dxgiFactory4->AddRef();

            *dxgiFactory6 = (IDXGIFactory6*)@this;
        }

        /// <inheritdoc cref="IUnknown.Release"/>
        [UnmanagedCallersOnly]
        private static uint Release(IDXGIFactory4As6Backcompat* @this)
        {
            _ = @this->dxgiFactory4->Release();

            NativeMemory.Free(@this);

            return 0;
        }

        /// <inheritdoc cref="IDXGIFactory6.EnumAdapters(uint, IDXGIAdapter**)"/>
        [UnmanagedCallersOnly]
        private static int EnumAdapters(IDXGIFactory4As6Backcompat* @this, uint Adapter, IDXGIAdapter** ppAdapter)
        {
            return @this->dxgiFactory4->EnumAdapters(Adapter, ppAdapter);
        }

        /// <inheritdoc cref="IDXGIFactory6.EnumWarpAdapter(Guid*, void**)"/>
        [UnmanagedCallersOnly]
        private static int EnumWarpAdapter(IDXGIFactory4As6Backcompat* @this, Guid* riid, void** ppvAdapter)
        {
            return @this->dxgiFactory4->EnumWarpAdapter(riid, ppvAdapter);
        }

        /// <inheritdoc cref="IDXGIFactory6.EnumAdapterByGpuPreference(uint, DXGI_GPU_PREFERENCE, Guid*, void**)"/>
        [UnmanagedCallersOnly]
        private static int EnumAdapterByGpuPreference(IDXGIFactory4As6Backcompat* @this, uint Adapter, DXGI_GPU_PREFERENCE GpuPreference, Guid* riid, void** ppvAdapter)
        {
            return @this->dxgiFactory4->EnumAdapters1(Adapter, (IDXGIAdapter1**)ppvAdapter);
        }
    }
}