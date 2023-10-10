using System;
using System.Diagnostics.CodeAnalysis;
#if WINDOWS_UWP
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Extensions;
#endif
using TerraFX.Interop.Windows;
#if !WINDOWS_UWP
using WinRT;
#endif
using IInspectable = TerraFX.Interop.WinRT.IInspectable;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Helpers;
#else
namespace ComputeSharp.D2D1.WinUI.Helpers;
#endif

/// <summary>
/// A helper type to handle marshalling of RCW instances.
/// </summary>
internal static unsafe class RcwMarshaller
{
    /// <summary>
    /// Gets or creates a managed object of a specified type for an input native object.
    /// </summary>
    /// <typeparam name="T">The interface type to retrieve an instance of.</typeparam>
    /// <param name="nativeObject">A pointer to the native object to get a managed wrapper for.</param>
    /// <returns>The resulting managed object wrapping <paramref name="nativeObject"/>.</returns>
    /// <remarks>This method should only be called with <typeparamref name="T"/> being a projected interface type.</remarks>
    public static T GetOrCreateManagedInterface<T>(IUnknown* nativeObject)
        where T : class
    {
#if WINDOWS_UWP
        // On UWP, Marshal.GetObjectForIUnknown handles all the marshalling/wrapping logic
        return (T)Marshal.GetObjectForIUnknown((IntPtr)nativeObject);
#else
        return MarshalInterface<T>.FromAbi((IntPtr)nativeObject);
#endif
    }

    /// <summary>
    /// Retrieves the underlying native object for an input RCW.
    /// </summary>
    /// <typeparam name="T">The type of managed object to unwrap.</typeparam>
    /// <param name="managedObject">The input RCW instance to unwrap.</param>
    /// <param name="nativeObject">A pointer to the resulting native object to retrieve.</param>
    /// <remarks>This method should only be called with <typeparamref name="T"/> being a concrete projected type.</remarks>
    public static void GetNativeObject<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.Interfaces)] T>(T managedObject, IInspectable** nativeObject)
        where T : class
    {
#if WINDOWS_UWP
        using ComPtr<IUnknown> unknownObject = default;

        // On UWP, due to built-in COM/WinRT support, Marshal.GetIUnknownForObject can handle all the logic.
        // We get back an IUnknown* pointer, so we need to QueryInterface for IInspectable* ourselves.
        unknownObject.Attach((IUnknown*)Marshal.GetIUnknownForObject(managedObject));

        unknownObject.CopyTo(nativeObject).Assert();
#else
        // On WinUI 3, delegate the RCW unwrapping or CCW creation logic to CsWinRT's APIs
        *nativeObject = (IInspectable*)MarshalInspectable<T>.FromManaged(managedObject);
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
    /// <remarks>This method should only be called with <typeparamref name="TFrom"/> being a projected interface type.</remarks>
    public static HRESULT GetNativeInterface<TFrom, TTo>(TFrom managedObject, TTo** nativeObject)
        where TFrom : class
        where TTo : unmanaged // IUnknown
    {
        using ComPtr<IUnknown> unknownObject = default;

#if WINDOWS_UWP
        unknownObject.Attach((IUnknown*)Marshal.GetIUnknownForObject(managedObject));

        unknownObject.CopyTo(nativeObject).Assert();
#else
        // Here we only want to get an IUnknown* pointer for a given interface, and then we'll do
        // QueryInterface ourselves to get the target COM type. So MarshalInterface<TFrom> is fine.
        unknownObject.Attach((IUnknown*)MarshalInterface<TFrom>.FromManaged(managedObject));
#endif

        return unknownObject.CopyTo(nativeObject);
    }
}
