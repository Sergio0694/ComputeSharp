using System;
using System.Runtime.CompilerServices;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Shaders.Interop.Extensions;

/// <summary>
/// Extensions for the <see cref="ID2D1Multithread"/> type.
/// </summary>
internal static class ID2D1EffectContextExtensions
{
    /// <summary>
    /// Gets an <see cref="ID2D1Multithread"/> from an input <see cref="ID2D1EffectContext"/> object.
    /// </summary>
    /// <param name="effectContext">The input <see cref="ID2D1EffectContext"/> instance.</param>
    /// <param name="multithread">The resulting <see cref="ID2D1Multithread"/> object.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation</returns>
    /// <remarks>
    /// <para>
    /// The <see cref="ID2D1EffectContext"/> object is not thread-safe, so it cannot be used concurrently
    /// when outside of a scenario where D2D has explicitly called into user code (eg. in the public APIs
    /// from <see cref="ID2D1EffectImpl"/>. To avoid issues, it is necessary to lock with the same lock
    /// used by D2D when using any public API on <see cref="ID2D1EffectContext"/> from other callsites.
    /// The same goes for any type out of <c>d2d1effectauthor.h</c>, such as <see cref="ID2D1ResourceTexture"/>.
    /// </para>
    /// <para>
    /// To lock, the <see cref="ID2D1Multithread"/> object can be used. To retrieve this, a <see cref="ID2D1Factory"/>
    /// instance is needed. This cannot be retrieved directly from an <see cref="ID2D1EffectContext"/>, but this
    /// can be used to create any <see cref="ID2D1Effect"/> instance (such as a flood effect, which is fairly cheap).
    /// From that, <see cref="IUnknown.QueryInterface"/> can be used to get an <see cref="ID2D1Image"/> object.
    /// The fact <see cref="ID2D1Effect"/> implements this interface is mentioned in the <see cref="ID2D1Image"/> docs,
    /// see <see href="https://docs.microsoft.com/en-us/windows/desktop/api/d2d1_1/nf-d2d1_1-id2d1effect-getoutput"/>.
    /// </para>
    /// <para>
    /// From the <see cref="ID2D1Image"/>, an <see cref="ID2D1Factory"/> can be retrieved. Finally, this can
    /// be cast to <see cref="ID2D1Multithread"/>, which can then be kept and used to synchronize later calls.
    /// </para>
    /// </remarks>
    public static unsafe int GetD2D1Multithread(this ref ID2D1EffectContext effectContext, ID2D1Multithread** multithread)
    {
        using ComPtr<ID2D1Effect> floodEffect = default;

        int hresult = effectContext.CreateEffect(
            effectId: (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_D2D1Flood)),
            effect: (void**)floodEffect.GetAddressOf());

        if (Windows.SUCCEEDED(hresult))
        {
            using ComPtr<ID2D1Image> d2D1Image = default;

            hresult = floodEffect.CopyTo(d2D1Image.GetAddressOf());

            if (Windows.SUCCEEDED(hresult))
            {
                using ComPtr<ID2D1Factory> d2D1Factory = default;

                d2D1Image.Get()->GetFactory(d2D1Factory.GetAddressOf());

                hresult = d2D1Factory.CopyTo(multithread);
            }
        }

        return hresult;
    }
}