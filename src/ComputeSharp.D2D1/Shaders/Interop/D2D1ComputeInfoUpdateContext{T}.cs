using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// An object allowing <see cref="D2D1DrawTransformMapper{T}.MapInputsToOutput"/> to interact with the shader data from within a transform.
/// </summary>
/// <typeparam name="T">The type of shader the transform will interact with.</typeparam>
public readonly unsafe ref struct D2D1ComputeInfoUpdateContext<T>
    where T : unmanaged, ID2D1ComputeShader
{
    /// <summary>
    /// The <see cref="ID2D1RenderInfoUpdateContext"/> instance associated with the current object.
    /// </summary>
    private readonly ID2D1RenderInfoUpdateContext* d2D1RenderInfoUpdateContext;

    /// <summary>
    /// Creates a new <see cref="D2D1ComputeInfoUpdateContext{T}"/> instance with the specified parameters.
    /// </summary>
    /// <param name="d2D1RenderInfoUpdateContext">The <see cref="ID2D1RenderInfoUpdateContext"/> instance associated with the current object.</param>
    internal D2D1ComputeInfoUpdateContext(ID2D1RenderInfoUpdateContext* d2D1RenderInfoUpdateContext)
    {
        this.d2D1RenderInfoUpdateContext = d2D1RenderInfoUpdateContext;
    }

    /// <summary>
    /// Retrieves the <typeparamref name="T"/> instance currently being used in the associated effect.
    /// </summary>
    /// <returns>The <typeparamref name="T"/> instance currently being used in the associated effect.</returns>
    public T GetConstantBuffer()
    {
        return D2D1RenderInfoUpdateContext.GetConstantBuffer<T>(this.d2D1RenderInfoUpdateContext);
    }

    /// <summary>
    /// Updates the <typeparamref name="T"/> instance to be used in the associated effect.
    /// </summary>
    /// <param name="shader">The <typeparamref name="T"/> instance to be used in the associated effect.</param>
    public void SetConstantBuffer(in T shader)
    {
        D2D1RenderInfoUpdateContext.SetConstantBuffer(this.d2D1RenderInfoUpdateContext, in shader);
    }
}