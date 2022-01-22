namespace ComputeSharp;

/// <summary>
/// A <see langword="enum"/> to be used with <see cref="EmbeddedBytecodeAttribute"/> to indicate the dispatch 
/// </summary>
public enum DispatchAxis
{
    /// <summary>
    /// Indicates a shader dispatch only along the X axis.
    /// </summary>
    /// <remarks>
    /// This applies to using <see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, in T)"/> or <see cref="ComputeContextExtensions.For{T}(in ComputeContext, int, in T)"/>.
    /// </remarks>
    X,

    /// <summary>
    /// Indicates a shader dispatch only along the Y axis.
    /// </summary>
    /// <remarks>
    /// This applies to using <see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/> or <see cref="ComputeContextExtensions.For{T}(in ComputeContext, int, int, int, in T)"/>,
    /// but only as long as the input dispatch size on both the X and Z axes is <c>1</c>. Using any other combination will not be able to leverage the precompiled shader bytecode.
    /// </remarks>
    Y,

    /// <summary>
    /// Indicates a shader dispatch only along the Z axis.
    /// </summary>
    /// <remarks>
    /// This applies to using <see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/> or <see cref="ComputeContextExtensions.For{T}(in ComputeContext, int, int, int, in T)"/>,
    /// but only as long as the input dispatch size on both the X and Y axes is <c>1</c>. Using any other combination will not be able to leverage the precompiled shader bytecode.
    /// </remarks>
    Z,

    /// <summary>
    /// Indicates a shader dispatch along the X and Y axes.
    /// </summary>
    /// <remarks>
    /// This applies to using <see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, in T)"/>, <see cref="GraphicsDeviceExtensions.ForEach{T, TPixel}(GraphicsDevice, IReadWriteNormalizedTexture2D{TPixel})"/>,
    /// <see cref="GraphicsDeviceExtensions.ForEach{T, TPixel}(GraphicsDevice, IReadWriteNormalizedTexture2D{TPixel}, in T)"/>, <see cref="ComputeContextExtensions.For{T}(in ComputeContext, int, int, in T)"/>,
    /// <see cref="ComputeContextExtensions.ForEach{T, TPixel}(in ComputeContext, IReadWriteNormalizedTexture2D{TPixel})"/> or <see cref="ComputeContextExtensions.ForEach{T, TPixel}(in ComputeContext, IReadWriteNormalizedTexture2D{TPixel}, in T)"/>.
    /// </remarks>
    XY,

    /// <summary>
    /// Indicates a shader dispatch along the X and Z axes.
    /// </summary>
    /// <remarks>
    /// This applies to using <see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/> or <see cref="ComputeContextExtensions.For{T}(in ComputeContext, int, int, int, in T)"/>,
    /// but only as long as the input dispatch size on the Y axis <c>1</c>. Using any other combination will not be able to leverage the precompiled shader bytecode.
    /// </remarks>
    XZ,

    /// <summary>
    /// Indicates a shader dispatch along the Y and Z axes.
    /// </summary>
    /// <remarks>
    /// This applies to using <see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/> or <see cref="ComputeContextExtensions.For{T}(in ComputeContext, int, int, int, in T)"/>,
    /// but only as long as the input dispatch size on the X axis <c>1</c>. Using any other combination will not be able to leverage the precompiled shader bytecode.
    /// </remarks>
    YZ,

    /// <summary>
    /// Indicates a shader dispatch along the X, Y and Z axes.
    /// </summary>
    /// <remarks>
    /// This applies to using <see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/> or <see cref="ComputeContextExtensions.For{T}(in ComputeContext, int, int, int, in T)"/>.
    /// </remarks>
    XYZ
}
