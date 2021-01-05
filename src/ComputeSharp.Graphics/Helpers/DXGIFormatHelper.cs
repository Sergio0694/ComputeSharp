using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.DXGI_FORMAT;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A helper type with utility methods for <see cref="DXGI_FORMAT"/>.
    /// </summary>
    internal static class DXGIFormatHelper
    {
        /// <summary>
        /// Gets the appropriate <see cref="DXGI_FORMAT"/> value for the input type argument.
        /// </summary>
        /// <typeparam name="T">The input type argument to get the corresponding <see cref="DXGI_FORMAT"/> for.</typeparam>
        /// <returns>The <see cref="DXGI_FORMAT"/> value corresponding to <typeparamref name="T"/>.</returns>
        /// <exception cref="System.ArgumentException">Thrown when the input type <typeparamref name="T"/> is not supported.</exception>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DXGI_FORMAT GetForType<T>()
            where T : unmanaged
        {
            if (typeof(T) == typeof(int)) return DXGI_FORMAT_R32_SINT;
            else if (typeof(T) == typeof(Int2)) return DXGI_FORMAT_R32G32_SINT;
            else if (typeof(T) == typeof(Int3)) return DXGI_FORMAT_R32G32B32_SINT;
            else if (typeof(T) == typeof(Int4)) return DXGI_FORMAT_R32G32B32A32_SINT;
            else if (typeof(T) == typeof(uint)) return DXGI_FORMAT_R32_UINT;
            else if (typeof(T) == typeof(UInt2)) return DXGI_FORMAT_R32G32_UINT;
            else if (typeof(T) == typeof(UInt3)) return DXGI_FORMAT_R32G32B32_UINT;
            else if (typeof(T) == typeof(UInt4)) return DXGI_FORMAT_R32G32B32A32_UINT;
            else if (typeof(T) == typeof(float)) return DXGI_FORMAT_R32_FLOAT;
            else if (typeof(T) == typeof(Float2)) return DXGI_FORMAT_R32G32_FLOAT;
            else if (typeof(T) == typeof(Float3)) return DXGI_FORMAT_R32G32B32_FLOAT;
            else if (typeof(T) == typeof(Float4)) return DXGI_FORMAT_R32G32B32A32_FLOAT;
            else if (typeof(T) == typeof(Vector2)) return DXGI_FORMAT_R32G32_FLOAT;
            else if (typeof(T) == typeof(Vector3)) return DXGI_FORMAT_R32G32B32_FLOAT;
            else if (typeof(T) == typeof(Vector4)) return DXGI_FORMAT_R32G32B32A32_FLOAT;
            else if (typeof(T) == typeof(Bgra32)) return DXGI_FORMAT_B8G8R8A8_UNORM;
            else if (typeof(T) == typeof(Rgba32)) return DXGI_FORMAT_R8G8B8A8_UNORM;
            else if (typeof(T) == typeof(Rgba64)) return DXGI_FORMAT_R16G16B16A16_UNORM;
            else if (typeof(T) == typeof(R8)) return DXGI_FORMAT_R8_UNORM;
            else if (typeof(T) == typeof(R16)) return DXGI_FORMAT_R16_UNORM;
            else if (typeof(T) == typeof(Rg16)) return DXGI_FORMAT_R8G8_UNORM;
            else if (typeof(T) == typeof(Rg32)) return DXGI_FORMAT_R16G16_UNORM;
            else return ThrowHelper.ThrowArgumentException<DXGI_FORMAT>("Invalid texture type");
        }
    }
}
