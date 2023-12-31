using System;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="ReadWriteTexture2D{T}"/> and <see cref="ReadWriteTexture2D{T, TPixel}"/> types.
/// </summary>
public static class ReadWriteTexture2DExtensions
{
    /// <summary>
    /// Retrieves a wrapping <see cref="IReadOnlyTexture2D{T}"/> instance for the input resource.
    /// </summary>
    /// <param name="texture">The input <see cref="ReadWriteTexture2D{T}"/> instance to create a wrapper for.</param>
    /// <returns>An <see cref="IReadOnlyTexture2D{T}"/> instance wrapping the current resource.</returns>
    /// <remarks>
    /// <para>The returned instance can be used in a shader to enable texture sampling.</para>
    /// <para>
    /// This is an advanced API that can only be used after the current instance has been transitioned to be in a readonly state. To do so,
    /// use <see cref="ComputeContextExtensions.Transition(ref readonly ComputeContext, ReadWriteTexture2D{float}, ResourceState)"/>,
    /// and specify <see cref="ResourceState.ReadOnly"/>. After that, this method can be used to get a readonly wrapper for
    /// the current texture to use in a shader. This instance should not be cached or reused, but just passed directly to a shader
    /// being dispatched through that same <see cref="ComputeContext"/>, as it will not work if the texture changes state later on.
    /// Before the end of that list of operations, the texture also needs to be transitioned back to writeable state, using the same
    /// API as before but specifying <see cref="ResourceState.ReadWrite"/>. Failing to do so results in undefined behavior.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown if the current instance or its associated device are disposed.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current instance is not in a readonly state.</exception>
    public static IReadOnlyTexture2D<float> AsReadOnly(this ReadWriteTexture2D<float> texture)
    {
        default(ArgumentNullException).ThrowIfNull(texture);

        return texture.AsReadOnly();
    }

    /// <summary>
    /// Retrieves a wrapping <see cref="IReadOnlyTexture2D{T}"/> instance for the input resource.
    /// </summary>
    /// <param name="texture">The input <see cref="ReadWriteTexture2D{T}"/> instance to create a wrapper for.</param>
    /// <returns>An <see cref="IReadOnlyTexture2D{T}"/> instance wrapping the current resource.</returns>
    /// <remarks>
    /// <para>The returned instance can be used in a shader to enable texture sampling.</para>
    /// <para>
    /// This is an advanced API that can only be used after the current instance has been transitioned to be in a readonly state. To do so,
    /// use <see cref="ComputeContextExtensions.Transition(ref readonly ComputeContext, ReadWriteTexture2D{Float2}, ResourceState)"/>,
    /// and specify <see cref="ResourceState.ReadOnly"/>. After that, this method can be used to get a readonly wrapper for
    /// the current texture to use in a shader. This instance should not be cached or reused, but just passed directly to a shader
    /// being dispatched through that same <see cref="ComputeContext"/>, as it will not work if the texture changes state later on.
    /// Before the end of that list of operations, the texture also needs to be transitioned back to writeable state, using the same
    /// API as before but specifying <see cref="ResourceState.ReadWrite"/>. Failing to do so results in undefined behavior.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown if the current instance or its associated device are disposed.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current instance is not in a readonly state.</exception>
    public static IReadOnlyTexture2D<Float2> AsReadOnly(this ReadWriteTexture2D<Float2> texture)
    {
        default(ArgumentNullException).ThrowIfNull(texture);

        return texture.AsReadOnly();
    }

    /// <summary>
    /// Retrieves a wrapping <see cref="IReadOnlyTexture2D{T}"/> instance for the input resource.
    /// </summary>
    /// <param name="texture">The input <see cref="ReadWriteTexture2D{T}"/> instance to create a wrapper for.</param>
    /// <returns>An <see cref="IReadOnlyTexture2D{T}"/> instance wrapping the current resource.</returns>
    /// <remarks>
    /// <para>The returned instance can be used in a shader to enable texture sampling.</para>
    /// <para>
    /// This is an advanced API that can only be used after the current instance has been transitioned to be in a readonly state. To do so,
    /// use <see cref="ComputeContextExtensions.Transition(ref readonly ComputeContext, ReadWriteTexture2D{Float3}, ResourceState)"/>,
    /// and specify <see cref="ResourceState.ReadOnly"/>. After that, this method can be used to get a readonly wrapper for
    /// the current texture to use in a shader. This instance should not be cached or reused, but just passed directly to a shader
    /// being dispatched through that same <see cref="ComputeContext"/>, as it will not work if the texture changes state later on.
    /// Before the end of that list of operations, the texture also needs to be transitioned back to writeable state, using the same
    /// API as before but specifying <see cref="ResourceState.ReadWrite"/>. Failing to do so results in undefined behavior.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown if the current instance or its associated device are disposed.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current instance is not in a readonly state.</exception>
    public static IReadOnlyTexture2D<Float3> AsReadOnly(this ReadWriteTexture2D<Float3> texture)
    {
        default(ArgumentNullException).ThrowIfNull(texture);

        return texture.AsReadOnly();
    }

    /// <summary>
    /// Retrieves a wrapping <see cref="IReadOnlyTexture2D{T}"/> instance for the input resource.
    /// </summary>
    /// <param name="texture">The input <see cref="ReadWriteTexture2D{T}"/> instance to create a wrapper for.</param>
    /// <returns>An <see cref="IReadOnlyTexture2D{T}"/> instance wrapping the current resource.</returns>
    /// <remarks>
    /// <para>The returned instance can be used in a shader to enable texture sampling.</para>
    /// <para>
    /// This is an advanced API that can only be used after the current instance has been transitioned to be in a readonly state. To do so,
    /// use <see cref="ComputeContextExtensions.Transition(ref readonly ComputeContext, ReadWriteTexture2D{Float4}, ResourceState)"/>,
    /// and specify <see cref="ResourceState.ReadOnly"/>. After that, this method can be used to get a readonly wrapper for
    /// the current texture to use in a shader. This instance should not be cached or reused, but just passed directly to a shader
    /// being dispatched through that same <see cref="ComputeContext"/>, as it will not work if the texture changes state later on.
    /// Before the end of that list of operations, the texture also needs to be transitioned back to writeable state, using the same
    /// API as before but specifying <see cref="ResourceState.ReadWrite"/>. Failing to do so results in undefined behavior.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown if the current instance or its associated device are disposed.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current instance is not in a readonly state.</exception>
    public static IReadOnlyTexture2D<Float4> AsReadOnly(this ReadWriteTexture2D<Float4> texture)
    {
        default(ArgumentNullException).ThrowIfNull(texture);

        return texture.AsReadOnly();
    }

    /// <summary>
    /// Retrieves a wrapping <see cref="IReadOnlyNormalizedTexture2D{TPixel}"/> instance for the input resource.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="texture">The input <see cref="ReadWriteTexture2D{T, TPixel}"/> instance to create a wrapper for.</param>
    /// <returns>An <see cref="IReadOnlyNormalizedTexture2D{TPixel}"/> instance wrapping the current resource.</returns>
    /// <remarks>
    /// <para>The returned instance can be used in a shader to enable texture sampling.</para>
    /// <para>
    /// This is an advanced API that can only be used after the current instance has been transitioned to be in a readonly state. To do so,
    /// use <see cref="ComputeContextExtensions.Transition{T, TPixel}(ref readonly ComputeContext, ReadWriteTexture2D{T, TPixel}, ResourceState)"/>,
    /// and specify <see cref="ResourceState.ReadOnly"/>. After that, this method can be used to get a readonly wrapper for
    /// the current texture to use in a shader. This instance should not be cached or reused, but just passed directly to a shader
    /// being dispatched through that same <see cref="ComputeContext"/>, as it will not work if the texture changes state later on.
    /// Before the end of that list of operations, the texture also needs to be transitioned back to writeable state, using the same
    /// API as before but specifying <see cref="ResourceState.ReadWrite"/>. Failing to do so results in undefined behavior.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown if the current instance or its associated device are disposed.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the current instance is not in a readonly state.</exception>
    public static IReadOnlyNormalizedTexture2D<TPixel> AsReadOnly<T, TPixel>(this ReadWriteTexture2D<T, TPixel> texture)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        default(ArgumentNullException).ThrowIfNull(texture);

        return texture.AsReadOnly();
    }
}