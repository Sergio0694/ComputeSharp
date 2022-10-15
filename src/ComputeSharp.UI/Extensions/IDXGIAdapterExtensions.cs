using System;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using Win32 = TerraFX.Interop.Windows.Windows;

#if WINDOWS_UWP
namespace ComputeSharp.Uwp.Extensions;
#else
namespace ComputeSharp.WinUI.Extensions;
#endif

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="IDXGIAdapter"/> type.
/// </summary>
internal static unsafe class IDXGIAdapterExtensions
{
    /// <summary>
    /// Calculates the refresh rate for a given adapter.
    /// </summary>
    /// <param name="dxgiAdapter">The input adapter to query.</param>
    /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> in use to render content.</param>
    /// <returns>The current composition refresh rate.</returns>
    public static int GetCompositionRefreshRate(this ref IDXGIAdapter dxgiAdapter, DXGI_FORMAT dxgiFormat)
    {
        using ComPtr<IDXGIAdapter1> dxgiAdapter1 = default;

        dxgiAdapter.QueryInterface(Win32.__uuidof<IDXGIAdapter1>(), dxgiAdapter1.GetVoidAddressOf()).Assert();

        using ComPtr<IDXGIOutput> dxgiOutput = default;

        dxgiAdapter1.Get()->EnumOutputs(0, dxgiOutput.GetAddressOf()).Assert();

        using ComPtr<IDXGIOutput1> dxgiOutput1 = default;

        dxgiOutput.CopyTo(dxgiOutput1.GetAddressOf()).Assert();

        DXGI_MODE_DESC1 dxgiModeDescIn = default;

        dxgiModeDescIn.Format = dxgiFormat;

        DXGI_MODE_DESC1 dxgiModeDescOut = default;

        dxgiOutput1.Get()->FindClosestMatchingMode1(&dxgiModeDescIn, &dxgiModeDescOut, null).Assert();

        // Calculates the best target framerate based on monitor refresh rate. Even with v-sync disabled, the target framerate
        // should always match the refresh rate in use, instead of being capped to 60fps (as users could have a 120Hz screen
        // and disabled v-sync). Here the refresh rate is retrieved, then the target framerate is calculated as being the
        // rounding up of this value to the next integer, and then clamped between 30 and 360fps.
        double refreshRateInHz = dxgiModeDescOut.RefreshRate.Numerator / (double)dxgiModeDescOut.RefreshRate.Denominator;
        int refreshRate = Math.Clamp((int)Math.Ceiling(refreshRateInHz), 30, 360);

        return refreshRate;
    }
}