using System;
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
    /// Gets or creates a managed object of a specified type for an input native object.
    /// </summary>
    /// <typeparam name="T">The interface type to retrieve an instance of.</typeparam>
    /// <param name="nativeObject">A pointer to the native object to get a managed wrapper for.</param>
    /// <returns>The resulting managed object wrapping <paramref name="nativeObject"/>.</returns>
    public static unsafe T GetOrCreateManagedObject<T>(IUnknown* nativeObject)
        where T : class
    {
#if WINDOWS_UWP
        // On UWP, Marshal.GetObjectForIUnknown handles all the marshalling/wrapping logic
        return (T)Marshal.GetObjectForIUnknown((IntPtr)nativeObject);
#else
        return MarshalInspectable<T>.FromAbi((IntPtr)nativeObject);
#endif
    }

    /// <summary>
    /// Retrieves the underlying native object for an input RCW and casts it to the specified type.
    /// </summary>
    /// <typeparam name="TFrom">The type of managed object to unwrap.</typeparam>
    /// <typeparam name="TTo">The interface type to retrieve an instance of.</typeparam>
    /// <param name="managedObject">The input RCW instance to unwrap.</param>
    /// <param name="nativeObject">A pointer to the resulting native object to retrieve.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    public static unsafe HRESULT GetNativeObject<TFrom, TTo>(TFrom managedObject, TTo** nativeObject)
        where TFrom : class
        where TTo : unmanaged // IUnknown
    {
        using ComPtr<IUnknown> unknownObject = default;

#if WINDOWS_UWP
        // On UWP, due to built-in COM/WinRT support, Marshal.GetIUnknownForObject can handle all the logic
        unknownObject.Attach((IUnknown*)Marshal.GetIUnknownForObject(managedObject));
#else
        // On WinUI 3, delegate the RCW unwrapping or CCW creation logic to CsWinRT's APIs
        unknownObject.Attach((IUnknown*)MarshalInspectable<TFrom>.FromManaged(managedObject));
#endif

        // QueryInterface to the specific interface we need
        return unknownObject.CopyTo(nativeObject);
    }
}
