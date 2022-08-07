using System;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.__Internals;
using TerraFX.Interop.DirectX;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Loaders;

/// <summary>
/// A data loader for D2D1 pixel shaders dispatched via <see cref="ID2D1DrawInfo"/>, to just retrieve the constant buffer size.
/// </summary>
internal unsafe struct D2D1BufferSizeDispatchDataLoader : ID2D1DispatchDataLoader
{
    /// <summary>
    /// The size of the constant buffer.
    /// </summary>
    private int size;

    /// <summary>
    /// Gets the size of the constant buffer.
    /// </summary>
    /// <returns>The size of the constant buffer.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetConstantBufferSize()
    {
        return this.size;
    }

    /// <inheritdoc/>
    void ID2D1DispatchDataLoader.LoadConstantBuffer(ReadOnlySpan<byte> data)
    {
        this.size = data.Length;
    }

    /// <summary>
    /// A helper class caching computed constant buffer sizes for generic shaders.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to retrieve info for.</typeparam>
    public static class For<T>
        where T : unmanaged, ID2D1PixelShader
    {
        /// <summary>
        /// Gets the constant buffer size for a shader of type <typeparamref name="T"/>.
        /// </summary>
        public static int ConstantBufferSize { get; } = GetConstantBufferSize();

        /// <summary>
        /// Calculates the constant buffer size for a shader of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The constant buffer size for a shader of type <typeparamref name="T"/>.</returns>
        private static int GetConstantBufferSize()
        {
            D2D1BufferSizeDispatchDataLoader dataLoader = default;

            Unsafe.SkipInit(out T shader);

            shader.LoadDispatchData(ref dataLoader);

            return dataLoader.GetConstantBufferSize();
        }
    }
}
