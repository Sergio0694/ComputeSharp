using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.D2D1.Shaders.Loaders;
using System.Buffers;
using System.Runtime.CompilerServices;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Shaders.Interop.Helpers;

/// <summary>
/// Shared helpers for render info update context objects.
/// </summary>
internal static unsafe class D2D1RenderInfoUpdateContext
{
    /// <summary>
    /// Retrieves the <typeparamref name="T"/> instance currently being used in the associated effect.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to process.</typeparam>
    /// <param name="d2D1RenderInfoUpdateContext">The <see cref="ID2D1RenderInfoUpdateContext"/> instance to use.</param>
    /// <returns>The <typeparamref name="T"/> instance currently being used in the associated effect.</returns>
    public static T GetConstantBuffer<T>(ID2D1RenderInfoUpdateContext* d2D1RenderInfoUpdateContext)
        where T : unmanaged, ID2D1Shader
    {
        uint constantBufferSize;

        d2D1RenderInfoUpdateContext->GetConstantBufferSize(&constantBufferSize).Assert();

        byte[] buffer = ArrayPool<byte>.Shared.Rent((int)constantBufferSize);

        fixed (byte* pBuffer = buffer)
        {
            d2D1RenderInfoUpdateContext->GetConstantBuffer(pBuffer, constantBufferSize).Assert();
        }

        Unsafe.SkipInit(out T shader);

        shader.InitializeFromDispatchData(buffer);

        ArrayPool<byte>.Shared.Return(buffer);

        return shader;
    }

    /// <summary>
    /// Updates the <typeparamref name="T"/> instance to be used in the associated effect.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to process.</typeparam>
    /// <param name="d2D1RenderInfoUpdateContext">The <see cref="ID2D1RenderInfoUpdateContext"/> instance to use.</param>
    /// <param name="shader">The <typeparamref name="T"/> instance to be used in the associated effect.</param>
    public static void SetConstantBuffer<T>(ID2D1RenderInfoUpdateContext* d2D1RenderInfoUpdateContext, in T shader)
        where T : unmanaged, ID2D1Shader
    {
        D2D1RenderInfoUpdateContextDispatchDataLoader dataLoader = new(d2D1RenderInfoUpdateContext);

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);
    }
}
