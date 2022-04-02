using System.Runtime.CompilerServices;
using ComputeSharp.D2D1Interop.__Internals;
using ComputeSharp.D2D1Interop.Shaders.Dispatching;
using TerraFX.Interop.DirectX;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1Interop.Interop;

/// <summary>
/// Provides methods to extract reflection info on compute shaders generated using this library.
/// </summary>
public static class InteropServices
{
    /// <summary>
    /// Sets the constant buffer from an input D2D1 pixel shader, by calling <c>ID2D1DrawInfo::SetPixelShaderConstantBuffer</c>.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to retrieve info for.</typeparam>
    /// <param name="shader">The input compute shader to retrieve info for.</param>
    /// <param name="d2D1DrawInfo">A pointer to the <c>ID2D1DrawInfo</c> instance to use.</param>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1drawinfo-setpixelshaderconstantbuffer"/>.</remarks>
    public static unsafe void SetPixelShaderConstantBufferForD2D1DrawInfo<T>(in T shader, void* d2D1DrawInfo)
        where T : struct, ID2D1Shader
    {
        D2D1DrawInfoDispatchDataLoader dataLoader = new((ID2D1DrawInfo*)d2D1DrawInfo);

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);
    }
}
