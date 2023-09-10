using System;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Shaders.Interop.Helpers;

/// <summary>
/// Shared helpers for render transform mappers.
/// </summary>
internal static unsafe class D2D1TransformMapper
{
    /// <summary>
    /// Gets the underlying <see cref="ID2D1TransformMapper"/> object.
    /// </summary>
    /// <param name="d2D1TransformMapperImpl">The wrapped <see cref="D2D1TransformMapperImpl"/> instance.</param>
    /// <param name="owningInstance">The owning <see cref="ID2D1TransformMapperInterop"/> instance.</param>
    /// <param name="transformMapper">The resulting <see cref="ID2D1TransformMapper"/> instance.</param>
    public static void GetD2D1TransformMapper(
        D2D1TransformMapperImpl* d2D1TransformMapperImpl,
        ID2D1TransformMapperInterop owningInstance,
        ID2D1TransformMapper** transformMapper)
    {
        bool lockTaken = false;

        d2D1TransformMapperImpl->SpinLock.Enter(ref lockTaken);

        // Whenever the CCW is requested from this object, we also make sure that the current instance is tracked
        // in the GC handle stored in the CCW. This could've been reset in case there were no other references to
        // the CCW (see comments in D2D1TransformMapperImpl about this), so here we're assigning a reference to the
        // current object again to ensure the returned CCW object will keep the instance alive if needed.
        try
        {
            d2D1TransformMapperImpl->EnsureTargetIsTracked(owningInstance);
            d2D1TransformMapperImpl->CopyToWithNoLock(transformMapper);
        }
        finally
        {
            d2D1TransformMapperImpl->SpinLock.Exit();

            GC.KeepAlive(owningInstance);
        }
    }

    /// <summary>
    /// Returns an interface according to a specified interface ID.
    /// </summary>
    /// <param name="d2D1TransformMapperImpl">The wrapped <see cref="D2D1TransformMapperImpl"/> instance.</param>
    /// <param name="owningInstance">The owning <see cref="ID2D1TransformMapperInterop"/> instance.</param>
    /// <param name="iid">The GUID of the requested interface.</param>
    /// <param name="ppv">A reference to the requested interface, when this method returns.</param>
    /// <returns>The resulting <see cref="CustomQueryInterfaceResult"/> value.</returns>
    public static CustomQueryInterfaceResult GetInterface(
        D2D1TransformMapperImpl* d2D1TransformMapperImpl,
        ID2D1TransformMapperInterop owningInstance,
        ref Guid iid,
        out IntPtr ppv)
    {
        fixed (Guid* pIid = &iid)
        fixed (IntPtr* pPpv = &ppv)
        {
            int hresult;
            bool lockTaken = false;

            d2D1TransformMapperImpl->SpinLock.Enter(ref lockTaken);

            try
            {
                d2D1TransformMapperImpl->EnsureTargetIsTracked(owningInstance);

                hresult = d2D1TransformMapperImpl->QueryInterfaceWithNoLock(pIid, (void**)pPpv);
            }
            finally
            {
                d2D1TransformMapperImpl->SpinLock.Exit();

                GC.KeepAlive(owningInstance);
            }

            return hresult switch
            {
                S.S_OK => CustomQueryInterfaceResult.Handled,
                _ => CustomQueryInterfaceResult.Failed
            };
        }
    }
}
