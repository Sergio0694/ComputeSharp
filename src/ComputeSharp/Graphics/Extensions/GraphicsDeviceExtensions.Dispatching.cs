using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;

namespace ComputeSharp;

/// <summary>
/// A <see langword="class"/> that contains extension methods for the <see cref="GraphicsDevice"/> type, used to run compute shaders.
/// </summary>
public static partial class GraphicsDeviceExtensions
{
    /// <summary>
    /// Runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to run.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    public static void For<T>(this GraphicsDevice device, int x, in T shader)
        where T : struct, IComputeShader
    {
        Guard.IsNotNull(device);

        using ComputeContext context = device.CreateComputeContext();

        context.Run(x, 1, 1, ref Unsafe.AsRef(in shader));
    }

    /// <summary>
    /// Runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to run.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    public static void For<T>(this GraphicsDevice device, int x, int y, in T shader)
        where T : struct, IComputeShader
    {
        Guard.IsNotNull(device);

        using ComputeContext context = device.CreateComputeContext();

        context.Run(x, y, 1, ref Unsafe.AsRef(in shader));
    }

    /// <summary>
    /// Runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to run.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="z">The number of iterations to run on the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    public static void For<T>(this GraphicsDevice device, int x, int y, int z, in T shader)
        where T : struct, IComputeShader
    {
        Guard.IsNotNull(device);

        using ComputeContext context = device.CreateComputeContext();

        context.Run(x, y, z, ref Unsafe.AsRef(in shader));
    }

    /// <summary>
    /// Runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to run.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="z">The number of iterations to run on the Z axis.</param>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    public static void For<T>(this GraphicsDevice device, int x, int y, int z, int threadsX, int threadsY, int threadsZ, in T shader)
        where T : struct, IComputeShader
    {
        Guard.IsNotNull(device);

        using ComputeContext context = device.CreateComputeContext();

        context.Run(x, y, z, threadsX, threadsY, threadsZ, ref Unsafe.AsRef(in shader));
    }

    /// <summary>
    /// Runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to run.</typeparam>
    /// <typeparam name="TPixel">The type of pixels being processed by the shader.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="texture">The target texture to apply the pixel shader to.</param>
    public static void ForEach<T, TPixel>(this GraphicsDevice device, IReadWriteNormalizedTexture2D<TPixel> texture)
        where T : struct, IPixelShader<TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(texture);

        using ComputeContext context = device.CreateComputeContext();

        context.Run(texture, ref Unsafe.AsRef(default(T)));
    }

    /// <summary>
    /// Runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to run.</typeparam>
    /// <typeparam name="TPixel">The type of pixels being processed by the shader.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="texture">The target texture to apply the pixel shader to.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the pixel shader to run.</param>
    public static void ForEach<T, TPixel>(this GraphicsDevice device, IReadWriteNormalizedTexture2D<TPixel> texture, in T shader)
        where T : struct, IPixelShader<TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(texture);

        using ComputeContext context = device.CreateComputeContext();

        context.Run(texture, ref Unsafe.AsRef(in shader));
    }

    /// <summary>
    /// Creates a new <see cref="ComputeContext"/> instance to batch compute operations together.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run shaders.</param>
    /// <returns>A new <see cref="ComputeContext"/> instance to batch compute operations together.</returns>
    /// <remarks>
    /// The returned <see cref="ComputeContext"/> instance should be used in a <see langword="using"/> block or declaration:
    /// <code>
    /// using (var context = device.CreateComputeContext())
    /// {
    ///     // Dispatch shaders here...
    /// }
    /// </code>
    /// <para>All dispatched shaders will be executed as soon as the context goes out of scope.</para>
    /// <para>Asynchronous execution is also supported, through the <see cref="System.IAsyncDisposable"/> interface:</para>
    /// <code>
    /// await using (var context = device.CreateComputeContext())
    /// {
    ///     // Dispatch shaders here, they will be executed asynchronously
    /// }
    /// </code>
    /// Copying or not disposing the <see cref="ComputeContext"/> instance results in undefined behavior.
    /// </remarks>
    public static ComputeContext CreateComputeContext(this GraphicsDevice device)
    {
        Guard.IsNotNull(device);

        using var _0 = device.GetReferenceTracker().GetLease();

        device.ThrowIfDeviceLost();

        return new(device);
    }
}
