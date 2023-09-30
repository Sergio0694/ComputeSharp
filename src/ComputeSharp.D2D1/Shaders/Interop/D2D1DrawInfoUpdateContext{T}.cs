using System.Buffers;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.D2D1.Shaders.Loaders;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// An object allowing <see cref="D2D1TransformMapper{T}.MapInputsToOutput"/> to interact with the shader data from within a transform.
/// </summary>
/// <typeparam name="T">The type of shader the transform will interact with.</typeparam>
public readonly unsafe ref struct D2D1DrawInfoUpdateContext<T>
    where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
{
    /// <summary>
    /// The <see cref="ID2D1DrawInfoUpdateContext"/> instance associated with the current object.
    /// </summary>
    private readonly ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext;

    /// <summary>
    /// Creates a new <see cref="D2D1DrawInfoUpdateContext{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="d2D1DrawInfoUpdateContext">The <see cref="ID2D1DrawInfoUpdateContext"/> instance associated with the current object.</param>
    internal D2D1DrawInfoUpdateContext(ID2D1DrawInfoUpdateContext* d2D1DrawInfoUpdateContext)
    {
        this.d2D1DrawInfoUpdateContext = d2D1DrawInfoUpdateContext;
    }

    /// <summary>
    /// Retrieves the <typeparamref name="T"/> instance currently being used in the associated effect.
    /// </summary>
    /// <returns>The <typeparamref name="T"/> instance currently being used in the associated effect.</returns>
    public T GetConstantBuffer()
    {
        uint constantBufferSize;

        this.d2D1DrawInfoUpdateContext->GetConstantBufferSize(&constantBufferSize).Assert();

        byte[] buffer = ArrayPool<byte>.Shared.Rent((int)constantBufferSize);

        fixed (byte* pBuffer = buffer)
        {
            this.d2D1DrawInfoUpdateContext->GetConstantBuffer(pBuffer, constantBufferSize).Assert();
        }

        Unsafe.SkipInit(out T shader);

        shader = shader.CreateFromConstantBuffer(buffer);

        ArrayPool<byte>.Shared.Return(buffer);

        return shader;
    }

    /// <summary>
    /// Updates the <typeparamref name="T"/> instance to be used in the associated effect.
    /// </summary>
    /// <param name="shader">The <typeparamref name="T"/> instance to be used in the associated effect.</param>
    public void SetConstantBuffer(in T shader)
    {
        D2D1DrawInfoUpdateContextConstantBufferLoader dataLoader = new(this.d2D1DrawInfoUpdateContext);

        Unsafe.AsRef(in shader).LoadConstantBuffer(in shader, ref dataLoader);
    }
}