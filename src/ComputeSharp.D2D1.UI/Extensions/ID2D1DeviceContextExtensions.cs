using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Uwp.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID2D1DeviceContext"/> type.
/// </summary>
internal static unsafe class ID2D1DeviceContextExtensions
{
    /// <summary>
    /// Checks whether a given <see cref="ID2D1DeviceContext"/> has an <see cref="ID2D1CommandList"/> as render target.
    /// </summary>
    /// <param name="deviceContext">The input <see cref="ID2D1DeviceContext"/> instance to check.</param>
    /// <returns>Whether the target of <paramref name="deviceContext"/> is an <see cref="ID2D1CommandList"/>.</returns>
    public static bool HasCommandListTarget(this ref ID2D1DeviceContext deviceContext)
    {
        using ComPtr<ID2D1Image> d2D1Image = default;

        // First get the target and check that it is not null
        deviceContext.GetTarget(d2D1Image.GetAddressOf());

        if (d2D1Image.Get() is null)
        {
            return false;
        }

        using ComPtr<ID2D1CommandList> d2D1CommandList = default;

        // If there is a target, check if it is an ID2D1CommandList
        HRESULT hresult = d2D1Image.CopyTo(d2D1CommandList.GetAddressOf());

        // Special case E_NOINTERFACE: this should return false and it's not an error
        if (hresult == E.E_NOINTERFACE)
        {
            return false;
        }

        // Throw for any other failure
        hresult.Assert();

        return true;
    }

    /// <summary>
    /// Reads the DPI value from a target <see cref="ID2D1DeviceContext"/> object.
    /// </summary>
    /// <param name="deviceContext">The input <see cref="ID2D1DeviceContext"/> instance.</param>
    /// <returns>The DPIs from <paramref name="deviceContext"/>.</returns>
    public static float GetDpi(this ref ID2D1DeviceContext deviceContext)
    {
        float dpiX;
        float dpiY;

        deviceContext.GetDpi(&dpiX, &dpiY);

        return dpiX;
    }

    /// <summary>
    /// Gets an <see cref="ID2D1Factory1"/> object from a given <see cref="ID2D1DeviceContext"/>.
    /// </summary>
    /// <param name="deviceContext">The input <see cref="ID2D1DeviceContext"/> instance.</param>
    /// <param name="d2D1Factory1">The resulting <see cref="ID2D1Factory1"/> object.</param>
    /// <returns>The <see cref="HRESULT"/> for the operation.</returns>
    /// <remarks>
    /// The <see cref="Interop.D2D1PixelShaderEffect"/> APIs specifically need an <see cref="ID2D1Factory1"/> (as <see cref="ID2D1Factory1.RegisterEffectFromString"/> is used).
    /// </remarks>
    public static HRESULT GetFactory1(this ref ID2D1DeviceContext deviceContext, ID2D1Factory1** d2D1Factory1)
    {
        using ComPtr<ID2D1Factory> d2D1Factory = default;

        // Get the underlying ID2D1Factory from the input context, which can be used to register effects
        deviceContext.GetFactory(d2D1Factory.GetAddressOf());

        // QueryInterface for the ID2D1Factory1 object (which should always be present)
        return d2D1Factory.CopyTo(d2D1Factory1);
    }
}