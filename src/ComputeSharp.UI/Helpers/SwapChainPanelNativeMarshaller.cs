using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Win32;
#if !WINDOWS_UWP
using Microsoft.UI.Xaml.Controls;
#endif
using System.Runtime.InteropServices;
#if WINDOWS_UWP
using Windows.UI.Xaml.Controls;
#endif
using WinRT;

#if WINDOWS_UWP
namespace ComputeSharp.Uwp.Helpers;
#else
namespace ComputeSharp.WinUI.Helpers;
#endif

/// <summary>
/// A helper type to handle marshalling <see cref="SwapChainPanel"/> objects for native interop.
/// </summary>
internal static unsafe class SwapChainPanelNativeMarshaller
{
    /// <summary>
    /// Retrieves the underlying <see cref="ISwapChainPanelNative"/> object for an input <see cref="SwapChainPanel"/> instance.
    /// </summary>
    /// <param name="managedObject">The input <see cref="SwapChainPanel"/> instance to unwrap.</param>
    /// <param name="nativeObject">A pointer to the resulting <see cref="ISwapChainPanelNative"/> object to retrieve.</param>
    public static void GetNativeObject(SwapChainPanel managedObject, ISwapChainPanelNative** nativeObject)
    {
#if WINDOWS_UWP
        ReadOnlySpan<byte> data =
        [
            0xD2, 0x19, 0x2F, 0xF9,
            0xDE, 0x3A,
            0xA6, 0x45,
            0xA2,
            0x0C,
            0xF6,
            0xF1,
            0xEA,
            0x90,
            0x55,
            0x4B
        ];
#else
        ReadOnlySpan<byte> data =
        [
            0xB8, 0xD0, 0xAA, 0x63,
            0x24, 0x7C,
            0xFF, 0x40,
            0x85,
            0xA8,
            0x64,
            0x0D,
            0x94,
            0x4C,
            0xC3,
            0x25
        ];
#endif
        // Unwrap and get the 'ISwapChainPanelNative' interface with the right IID depending on the UI framework
        ((IWinRTObject)managedObject).NativeObject.TryAs(
            iid: Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data)),
            ppv: out *(nint*)nativeObject).Assert();
    }
}