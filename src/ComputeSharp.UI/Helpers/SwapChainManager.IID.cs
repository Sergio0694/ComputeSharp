using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if WINDOWS_UWP
namespace ComputeSharp.Uwp.Helpers;
#else
namespace ComputeSharp.WinUI.Helpers;
#endif

/// <inheritdoc/>
partial class SwapChainManager<TOwner>
{
    /// <summary>
    /// Gets the IID of <see cref="Win32.ISwapChainPanelNative"/>.
    /// </summary>
    private static unsafe Guid* IID_ISwapChainPanelNative
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
#if WINDOWS_UWP
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
#else
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
#endif
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }
}