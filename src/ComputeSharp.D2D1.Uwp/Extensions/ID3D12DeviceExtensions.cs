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
}