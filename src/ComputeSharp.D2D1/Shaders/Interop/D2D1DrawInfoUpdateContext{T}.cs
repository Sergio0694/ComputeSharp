using System;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// An object allowing <see cref="D2D1TransformMapper{T}.MapInputsToOutput"/> to interact with the shader data from within a transform.
/// </summary>
/// <typeparam name="T">The type of shader the transform will interact with.</typeparam>
public readonly ref struct D2D1DrawInfoUpdateContext<T>
    where T : unmanaged, ID2D1PixelShader
{
    /// <summary>
    /// Retrieves the <typeparamref name="T"/> instance currently being used in the associated effect.
    /// </summary>
    /// <returns>The <typeparamref name="T"/> instance currently being used in the associated effect.</returns>
    public T GetConstantBuffer()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates the <typeparamref name="T"/> instance to be used in the associated effect.
    /// </summary>
    /// <param name="shader">The <typeparamref name="T"/> instance to be used in the associated effect.</param>
    public void SetConstantBuffer(in T shader)
    {
        throw new NotImplementedException();
    }
}