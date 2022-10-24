using System.Numerics;
using System.Runtime.InteropServices;
using TerraFX.Interop;
using Windows.Foundation;

namespace ABI.Microsoft.Graphics.Canvas;

/// <summary>
/// Native APIs from <c>Microsoft.Graphics.Canvas</c> (ie. Win2D).
/// </summary>
internal static unsafe class Win2D
{
    /// <summary>
    /// Calculates the bounds for a given <see cref="ICanvasImageInterop"/> instance.
    /// </summary>
    /// <param name="resourceCreator">The <see cref="ICanvasResourceCreator"/> object in use.</param>
    /// <param name="image">The input <see cref="ICanvasImageInterop"/> instance to compute the bounds for.</param>
    /// <param name="transform">The transform to use (optional).</param>
    /// <param name="rect">The resulting bounds.</param>
    /// <returns>The <see cref="TerraFX.Interop.Windows.HRESULT"/> for the operation.</returns>
    [DllImport("Microsoft.Graphics.Canvas.dll", PreserveSig = true, ExactSpelling = true)]
    public static extern int GetBoundsForICanvasImageInterop(
        ICanvasResourceCreator* resourceCreator,
        ICanvasImageInterop* image,
        [NativeTypeName("Windows::Foundation::Numerics::Matrix3x2 const*")] Matrix3x2* transform,
        Rect* rect);
}
