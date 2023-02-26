#if WINDOWS_UWP
using System.Runtime.InteropServices;
#endif
using TerraFX.Interop.Windows;
#if !WINDOWS_UWP
using WinRT;
#endif

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Helpers;
#else
namespace ComputeSharp.D2D1.WinUI.Helpers;
#endif

/// <summary>
/// A helper type to handle marshalling of RCW instances.
/// </summary>
internal static class RcwMarshaller
{
    /// <summary>
    /// Retrieves the underlying native object for an input RCW and casts it to the specified type.
    /// </summary>
    /// <typeparam name="T">The interface type to retrieve an instance of.</typeparam>
    /// <param name="managedObject">The input RCW instance to unwrap.</param>
    /// <param name="nativeObject">A pointer to the resulting native object to retrieve.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static unsafe HRESULT QueryInterface<T>(object managedObject, T** nativeObject)
        where T : unmanaged // IUnknown
    {
        using ComPtr<IUnknown> unknownObject = default;

#if WINDOWS_UWP
        // On UWP, due to built-in COM/WinRT support, Marshal.GetIUnknownForObject can handle all the logic
        unknownObject.Attach((IUnknown*)Marshal.GetIUnknownForObject(managedObject));
#else
        // On WinUI 3, CsWinRT owns the RCWs, which can be unwrapped via IWinRTObject.NativeObject
        unknownObject.Attach((IUnknown*)((IWinRTObject)managedObject).NativeObject.GetRef());
#endif

        // QueryInterface to the specific interface we need
        return unknownObject.CopyTo(nativeObject);
    }
}
