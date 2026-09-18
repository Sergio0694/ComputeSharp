using System;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

namespace ComputeSharp.Graphics.Helpers;

/// <summary>
/// The vtable for <see cref="DeviceHelper.IDXGIFactory4As6Backcompat"/>, matching the layout of <see cref="IDXGIFactory6"/>.
/// </summary>
/// <remarks>
/// All slots that are not implemented are intentionally left as <see langword="null"/>, so that calling them will fail fast.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct IDXGIFactory4As6BackcompatVftbl
{
    public void* QueryInterface;
    public void* AddRef;
    public delegate* unmanaged[MemberFunction]<DeviceHelper.IDXGIFactory4As6Backcompat*, uint> Release;
    public void* SetPrivateData;
    public void* SetPrivateDataInterface;
    public void* GetPrivateData;
    public void* GetParent;
    public delegate* unmanaged[MemberFunction]<DeviceHelper.IDXGIFactory4As6Backcompat*, uint, IDXGIAdapter**, int> EnumAdapters;
    public void* MakeWindowAssociation;
    public void* GetWindowAssociation;
    public void* CreateSwapChain;
    public void* CreateSoftwareAdapter;
    public void* EnumAdapters1;
    public void* IsCurrent;
    public void* IsWindowedStereoEnabled;
    public void* CreateSwapChainForHwnd;
    public void* CreateSwapChainForCoreWindow;
    public void* GetSharedResourceAdapterLuid;
    public void* RegisterStereoStatusWindow;
    public void* RegisterStereoStatusEvent;
    public void* UnregisterStereoStatus;
    public void* RegisterOcclusionStatusWindow;
    public void* RegisterOcclusionStatusEvent;
    public void* UnregisterOcclusionStatus;
    public void* CreateSwapChainForComposition;
    public void* GetCreationFlags;
    public void* EnumAdapterByLuid;
    public delegate* unmanaged[MemberFunction]<DeviceHelper.IDXGIFactory4As6Backcompat*, Guid*, void**, int> EnumWarpAdapter;
    public void* CheckFeatureSupport;
    public delegate* unmanaged[MemberFunction]<DeviceHelper.IDXGIFactory4As6Backcompat*, uint, DXGI_GPU_PREFERENCE, Guid*, void**, int> EnumAdapterByGpuPreference;
}