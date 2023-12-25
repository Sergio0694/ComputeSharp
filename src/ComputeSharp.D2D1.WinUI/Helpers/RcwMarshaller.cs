using System;
using System.Diagnostics.CodeAnalysis;
using ComputeSharp.Win32;
using WinRT;
using IInspectable = ComputeSharp.Win32.IInspectable;

namespace ComputeSharp.D2D1.WinUI.Helpers;

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
        return MarshalInterface<T>.FromAbi((IntPtr)nativeObject);
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
        *nativeObject = (IInspectable*)MarshalInspectable<T>.FromManaged(managedObject);
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
        where TTo : unmanaged, IComObject
    {
        using ComPtr<IUnknown> unknownObject = default;

        // Here we only want to get an IUnknown* pointer for a given interface, and then we'll do
        // QueryInterface ourselves to get the target COM type. So MarshalInterface<TFrom> is fine.
        unknownObject.Attach((IUnknown*)MarshalInterface<TFrom>.FromManaged(managedObject));

        return unknownObject.CopyTo(nativeObject);
    }
}