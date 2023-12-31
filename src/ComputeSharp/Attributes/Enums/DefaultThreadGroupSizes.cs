using System;

namespace ComputeSharp;

/// <summary>
/// A <see langword="enum"/> to be used with <see cref="ThreadGroupSizeAttribute"/> to more easily set default values for the thread group size.
/// </summary>
[Flags]
public enum DefaultThreadGroupSizes
{
    /// <summary>
    /// Indicates a shader dispatch only along the X axis.
    /// </summary>
    /// <remarks>
    /// This applies to using:
    /// <list type="bullet">
    ///   <item><see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, in T)"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.For{T}(ref readonly ComputeContext, int, in T)"/>.</item>
    /// </list>
    /// </remarks>
    X = 1 << 0,

    /// <summary>
    /// Indicates a shader dispatch only along the Y axis.
    /// </summary>
    /// <remarks>
    /// This applies to using:
    /// <list type="bullet">
    ///   <item><see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.For{T}(ref readonly ComputeContext, int, int, int, in T)"/>.</item>
    /// </list>
    /// But only as long as the input dispatch size on both the X and Z axes is <c>1</c>.
    /// Using any other combination will not be able to leverage the precompiled shader bytecode.
    /// </remarks>
    Y = 1 << 1,

    /// <summary>
    /// Indicates a shader dispatch only along the Z axis.
    /// </summary>
    /// <remarks>
    /// This applies to using:
    /// <list type="bullet">
    ///   <item><see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.For{T}(ref readonly ComputeContext, int, int, int, in T)"/>.</item>
    /// </list>
    /// But only as long as the input dispatch size on both the X and Y axes is <c>1</c>.
    /// Using any other combination will not be able to leverage the precompiled shader bytecode.
    /// </remarks>
    Z = 1 << 2,

    /// <summary>
    /// Indicates a shader dispatch along the X and Y axes.
    /// </summary>
    /// <remarks>
    /// This applies to using:
    /// <list type="bullet">
    ///   <item><see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, in T)"/>.</item>
    ///   <item><see cref="GraphicsDeviceExtensions.ForEach{T, TPixel}(GraphicsDevice, IReadWriteNormalizedTexture2D{TPixel})"/>.</item>
    ///   <item><see cref="GraphicsDeviceExtensions.ForEach{T, TPixel}(GraphicsDevice, IReadWriteNormalizedTexture2D{TPixel}, in T)"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.For{T}(ref readonly ComputeContext, int, int, in T)"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.ForEach{T, TPixel}(ref readonly ComputeContext, IReadWriteNormalizedTexture2D{TPixel})"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.ForEach{T, TPixel}(ref readonly ComputeContext, IReadWriteNormalizedTexture2D{TPixel}, in T)"/>..</item>
    /// </list>
    /// </remarks>
    XY = X | Y,

    /// <summary>
    /// Indicates a shader dispatch along the X and Z axes.
    /// </summary>
    /// <remarks>
    /// This applies to using:
    /// <list type="bullet">
    ///   <item><see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.For{T}(ref readonly ComputeContext, int, int, int, in T)"/>.</item>
    /// </list>
    /// But only as long as the input dispatch size on the Y axis <c>1</c>.
    /// Using any other combination will not be able to leverage the precompiled shader bytecode.
    /// </remarks>
    XZ = X | Z,

    /// <summary>
    /// Indicates a shader dispatch along the Y and Z axes.
    /// </summary>
    /// <remarks>
    /// This applies to using:
    /// <list type="bullet">
    ///   <item><see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.For{T}(ref readonly ComputeContext, int, int, int, in T)"/>.</item>
    /// </list>
    /// But only as long as the input dispatch size on the X axis <c>1</c>.
    /// Using any other combination will not be able to leverage the precompiled shader bytecode.
    /// </remarks>
    YZ = Y | Z,

    /// <summary>
    /// Indicates a shader dispatch along the X, Y and Z axes.
    /// </summary>
    /// <remarks>
    /// This applies to using:
    /// <list type="bullet">
    ///   <item><see cref="GraphicsDeviceExtensions.For{T}(GraphicsDevice, int, int, int, in T)"/>.</item>
    ///   <item><see cref="ComputeContextExtensions.For{T}(ref readonly ComputeContext, int, int, int, in T)"/>.</item>
    /// </list>
    /// </remarks>
    XYZ = X | Y | Z
}