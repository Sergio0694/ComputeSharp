using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Graphics.Helpers;
using ComputeSharp.Resources;

namespace ComputeSharp;

/// <inheritdoc cref="GraphicsDeviceExtensions"/>
partial class GraphicsDeviceExtensions
{
    /// <summary>
    /// Allocates a new constant buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="length">The length of the buffer to allocate.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ConstantBuffer{T}"/> instance of size <paramref name="length"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, int length, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, length, allocationMode);
    }

    /// <summary>
    /// Allocates a new constant buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
    /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input array.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, T[] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateConstantBuffer<T>(source.AsSpan());
    }

    /// <summary>
    /// Allocates a new constant buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated buffer.</param>
    /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, ReadOnlySpan<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ConstantBuffer<T> buffer = new(device, source.Length, AllocationMode.Default);

        buffer.CopyFrom(source);

        return buffer;
    }

    /// <summary>
    /// Allocates a new constant buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <see cref="Buffer{T}"/> with the data to copy on the allocated buffer.</param>
    /// <returns>A constant <see cref="ConstantBuffer{T}"/> instance with the contents of the input <see cref="Buffer{T}"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ConstantBuffer<T> AllocateConstantBuffer<T>(this GraphicsDevice device, Buffer<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ConstantBuffer<T> constantBuffer = new(device, source.Length, AllocationMode.Default);

        constantBuffer.CopyFrom(source);

        return constantBuffer;
    }

    /// <summary>
    /// Allocates a new readonly buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="length">The length of the buffer to allocate.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadOnlyBuffer{T}"/> instance of size <paramref name="length"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, int length, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, length, allocationMode);
    }

    /// <summary>
    /// Allocates a new readonly buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
    /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input array.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, T[] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyBuffer<T>(source.AsSpan());
    }

    /// <summary>
    /// Allocates a new readonly buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated buffer.</param>
    /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, ReadOnlySpan<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ReadOnlyBuffer<T> buffer = new(device, source.Length, AllocationMode.Default);

        buffer.CopyFrom(source);

        return buffer;
    }

    /// <summary>
    /// Allocates a new readonly buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <see cref="Buffer{T}"/> with the data to copy on the allocated buffer.</param>
    /// <returns>A read write <see cref="ReadOnlyBuffer{T}"/> instance with the contents of the input <see cref="Buffer{T}"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, Buffer<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadOnlyBuffer<T> readWriteBuffer = new(device, source.Length, AllocationMode.Default);

        readWriteBuffer.CopyFrom(source);

        return readWriteBuffer;
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadOnlyTexture1D{T}"/> instance of size [<paramref name="width"/>].</returns>
    public static ReadOnlyTexture1D<T> AllocateReadOnlyTexture1D<T>(this GraphicsDevice device, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, allocationMode);
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture1D{T}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture1D<T> AllocateReadOnlyTexture1D<T>(this GraphicsDevice device, T[] source, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture1D<T>(source.AsSpan(0, width));
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture1D{T}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture1D<T> AllocateReadOnlyTexture1D<T>(this GraphicsDevice device, T[] source, int offset, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture1D<T>(source.AsSpan(offset, width));
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadOnlyTexture1D{T}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture1D<T> AllocateReadOnlyTexture1D<T>(this GraphicsDevice device, T[] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadOnlyTexture1D<T> texture = new(device, source.Length, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture1D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlyTexture1D<T> AllocateReadOnlyTexture1D<T>(this GraphicsDevice device, ReadOnlySpan<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ReadOnlyTexture1D<T> texture = new(device, source.Length, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadOnlyTexture1D{T, TPixel}"/> instance of size [<paramref name="width"/>].</returns>
    public static ReadOnlyTexture1D<T, TPixel> AllocateReadOnlyTexture1D<T, TPixel>(this GraphicsDevice device, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, allocationMode);
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture1D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture1D<T, TPixel> AllocateReadOnlyTexture1D<T, TPixel>(this GraphicsDevice device, T[] source, int width)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture1D<T, TPixel>(source.AsSpan(0, width));
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture1D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture1D<T, TPixel> AllocateReadOnlyTexture1D<T, TPixel>(this GraphicsDevice device, T[] source, int offset, int width)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture1D<T, TPixel>(source.AsSpan(offset, width));
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadOnlyTexture1D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture1D<T, TPixel> AllocateReadOnlyTexture1D<T, TPixel>(this GraphicsDevice device, T[] source)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadOnlyTexture1D<T, TPixel> texture = new(device, source.Length, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture1D{T, TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlyTexture1D<T, TPixel> AllocateReadOnlyTexture1D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> source)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        ReadOnlyTexture1D<T, TPixel> texture = new(device, source.Length, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadOnlyTexture2D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
    public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, allocationMode);
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, T[] source, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture2D<T>(source.AsSpan(), width, height);
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, T[] source, int offset, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture2D<T>(source.AsSpan(offset), width, height);
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadOnlyTexture2D{T}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, T[,] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadOnlyTexture2D<T> texture = new(device, source.GetLength(1), source.GetLength(0), AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, ReadOnlySpan<T> source, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ReadOnlyTexture2D<T> texture = new(device, width, height, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
    public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, allocationMode);
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, T[] source, int width, int height)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture2D<T, TPixel>(source.AsSpan(), width, height);
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, T[] source, int offset, int width, int height)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture2D<T, TPixel>(source.AsSpan(offset), width, height);
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, T[,] source)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadOnlyTexture2D<T, TPixel> texture = new(device, source.GetLength(1), source.GetLength(0), AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlyTexture2D<T, TPixel> AllocateReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> source, int width, int height)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        ReadOnlyTexture2D<T, TPixel> texture = new(device, width, height, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadOnlyTexture3D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>, <paramref name="depth"/>].</returns>
    public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, depth, allocationMode);
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture3D{T}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, T[] source, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture3D<T>(source.AsSpan(), width, height, depth);
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture3D{T}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, T[] source, int offset, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture3D<T>(source.AsSpan(offset), width, height, depth);
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadOnlyTexture3D{T}"/> instance with the contents of the input array.</returns>
    /// <remarks>
    /// The source 3D array needs to have each 2D plane stacked on the depth axis.
    /// That is, the expected layout of the input array has to be [depth, height, width].
    /// </remarks>
    public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, T[,,] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadOnlyTexture3D<T> texture = new(device, source.GetLength(2), source.GetLength(1), source.GetLength(0), AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture3D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlyTexture3D<T> AllocateReadOnlyTexture3D<T>(this GraphicsDevice device, ReadOnlySpan<T> source, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ReadOnlyTexture3D<T> texture = new(device, width, height, depth, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadOnlyTexture3D{T, TPixel}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>, <paramref name="depth"/>].</returns>
    public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, depth, allocationMode);
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture3D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, T[] source, int width, int height, int depth)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture3D<T, TPixel>(source.AsSpan(), width, height, depth);
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture3D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, T[] source, int offset, int width, int height, int depth)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadOnlyTexture3D<T, TPixel>(source.AsSpan(offset), width, height, depth);
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadOnlyTexture3D{T, TPixel}"/> instance with the contents of the input array.</returns>
    /// <remarks>
    /// The source 3D array needs to have each 2D plane stacked on the depth axis.
    /// That is, the expected layout of the input array has to be [depth, height, width].
    /// </remarks>
    public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, T[,,] source)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadOnlyTexture3D<T, TPixel> texture = new(device, source.GetLength(2), source.GetLength(1), source.GetLength(0), AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new readonly 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture3D{T, TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadOnlyTexture3D<T, TPixel> AllocateReadOnlyTexture3D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> source, int width, int height, int depth)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        ReadOnlyTexture3D<T, TPixel> texture = new(device, width, height, depth, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new read write buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="length">The length of the buffer to allocate.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadWriteBuffer{T}"/> instance of size <paramref name="length"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, int length, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, length, allocationMode);
    }

    /// <summary>
    /// Allocates a new read write buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated buffer.</param>
    /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input array.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, T[] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteBuffer<T>(source.AsSpan());
    }

    /// <summary>
    /// Allocates a new read write buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated buffer.</param>
    /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, ReadOnlySpan<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ReadWriteBuffer<T> buffer = new(device, source.Length, AllocationMode.Default);

        buffer.CopyFrom(source);

        return buffer;
    }

    /// <summary>
    /// Allocates a new read write buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="source">The input <see cref="Buffer{T}"/> with the data to copy on the allocated buffer.</param>
    /// <returns>A read write <see cref="ReadWriteBuffer{T}"/> instance with the contents of the input <see cref="Buffer{T}"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, Buffer<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadWriteBuffer<T> readWriteBuffer = new(device, source.Length, AllocationMode.Default);

        readWriteBuffer.CopyFrom(source);

        return readWriteBuffer;
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadWriteTexture1D{T}"/> instance of size [<paramref name="width"/>].</returns>
    public static ReadWriteTexture1D<T> AllocateReadWriteTexture1D<T>(this GraphicsDevice device, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, allocationMode);
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture1D{T}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture1D<T> AllocateReadWriteTexture1D<T>(this GraphicsDevice device, T[] source, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture1D<T>(source.AsSpan(0, width));
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture1D{T}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture1D<T> AllocateReadWriteTexture1D<T>(this GraphicsDevice device, T[] source, int offset, int width)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture1D<T>(source.AsSpan(offset, width));
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadWriteTexture1D{T}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture1D<T> AllocateReadWriteTexture1D<T>(this GraphicsDevice device, T[] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadWriteTexture1D<T> texture = new(device, source.Length, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <returns>A <see cref="ReadWriteTexture1D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadWriteTexture1D<T> AllocateReadWriteTexture1D<T>(this GraphicsDevice device, ReadOnlySpan<T> source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ReadWriteTexture1D<T> texture = new(device, source.Length, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadWriteTexture1D{T, TPixel}"/> instance of size [<paramref name="width"/>].</returns>
    public static ReadWriteTexture1D<T, TPixel> AllocateReadWriteTexture1D<T, TPixel>(this GraphicsDevice device, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, allocationMode);
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture1D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture1D<T, TPixel> AllocateReadWriteTexture1D<T, TPixel>(this GraphicsDevice device, T[] source, int width)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture1D<T, TPixel>(source.AsSpan(0, width));
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture1D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture1D<T, TPixel> AllocateReadWriteTexture1D<T, TPixel>(this GraphicsDevice device, T[] source, int offset, int width)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture1D<T, TPixel>(source.AsSpan(offset, width));
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadWriteTexture1D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture1D<T, TPixel> AllocateReadWriteTexture1D<T, TPixel>(this GraphicsDevice device, T[] source)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadWriteTexture1D<T, TPixel> texture = new(device, source.Length, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <returns>A <see cref="ReadWriteTexture1D{T, TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadWriteTexture1D<T, TPixel> AllocateReadWriteTexture1D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> source)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        ReadWriteTexture1D<T, TPixel> texture = new(device, source.Length, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadWriteTexture2D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
    public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, allocationMode);
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, T[] source, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture2D<T>(source.AsSpan(), width, height);
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, T[] source, int offset, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture2D<T>(source.AsSpan(offset), width, height);
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadWriteTexture2D{T}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, T[,] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadWriteTexture2D<T> texture = new(device, source.GetLength(1), source.GetLength(0), AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, ReadOnlySpan<T> source, int width, int height)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ReadWriteTexture2D<T> texture = new(device, width, height, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadWriteTexture2D{T, TPixel}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
    public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, allocationMode);
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, T[] source, int width, int height)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture2D<T, TPixel>(source.AsSpan(), width, height);
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, T[] source, int offset, int width, int height)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture2D<T, TPixel>(source.AsSpan(offset), width, height);
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadWriteTexture2D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, T[,] source)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadWriteTexture2D<T, TPixel> texture = new(device, source.GetLength(1), source.GetLength(0), AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T, TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadWriteTexture2D<T, TPixel> AllocateReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> source, int width, int height)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        ReadWriteTexture2D<T, TPixel> texture = new(device, width, height, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A zeroed <see cref="ReadWriteTexture3D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
    public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, depth, allocationMode);
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture3D{T}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, T[] source, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture3D<T>(source.AsSpan(), width, height, depth);
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture3D{T}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, T[] source, int offset, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture3D<T>(source.AsSpan(offset), width, height, depth);
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadWriteTexture3D{T}"/> instance with the contents of the input array.</returns>
    /// <remarks>
    /// The source 3D array needs to have each 2D plane stacked on the depth axis.
    /// That is, the expected layout of the input array has to be [depth, height, width].
    /// </remarks>
    public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, T[,,] source)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadWriteTexture3D<T> texture = new(device, source.GetLength(2), source.GetLength(1), source.GetLength(0), AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture3D{T}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadWriteTexture3D<T> AllocateReadWriteTexture3D<T>(this GraphicsDevice device, ReadOnlySpan<T> source, int width, int height, int depth)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        ReadWriteTexture3D<T> texture = new(device, width, height, depth, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="ReadWriteTexture3D{T, TPixel}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>, <paramref name="depth"/>].</returns>
    public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, depth, allocationMode);
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture3D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, T[] source, int width, int height, int depth)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture3D<T, TPixel>(source.AsSpan(), width, height, depth);
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <param name="offset">The starting offset within <paramref name="source"/> to read data from.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture3D{T, TPixel}"/> instance with the contents of the input array.</returns>
    public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, T[] source, int offset, int width, int height, int depth)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        return device.AllocateReadWriteTexture3D<T, TPixel>(source.AsSpan(offset), width, height, depth);
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <typeparamref name="T"/> array with the data to copy on the allocated texture.</param>
    /// <returns>A read write <see cref="ReadWriteTexture3D{T, TPixel}"/> instance with the contents of the input array.</returns>
    /// <remarks>
    /// The source 3D array needs to have each 2D plane stacked on the depth axis.
    /// That is, the expected layout of the input array has to be [depth, height, width].
    /// </remarks>
    public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, T[,,] source)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(source);

        ReadWriteTexture3D<T, TPixel> texture = new(device, source.GetLength(2), source.GetLength(1), source.GetLength(0), AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new writeable 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="source">The input <see cref="ReadOnlySpan{T}"/> with the data to copy on the allocated texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture3D{T, TPixel}"/> instance with the contents of the input <see cref="ReadOnlySpan{T}"/>.</returns>
    public static ReadWriteTexture3D<T, TPixel> AllocateReadWriteTexture3D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<T> source, int width, int height, int depth)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        ReadWriteTexture3D<T, TPixel> texture = new(device, width, height, depth, AllocationMode.Default);

        texture.CopyFrom(source);

        return texture;
    }

    /// <summary>
    /// Allocates a new upload buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="length">The length of the buffer to allocate.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="ReadOnlyBuffer{T}"/> instance of size <paramref name="length"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static UploadBuffer<T> AllocateUploadBuffer<T>(this GraphicsDevice device, int length, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, length, allocationMode);
    }

    /// <summary>
    /// Allocates a new upload 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="UploadTexture1D{T}"/> instance of size [<paramref name="width"/>].</returns>
    public static UploadTexture1D<T> AllocateUploadTexture1D<T>(this GraphicsDevice device, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, allocationMode);
    }

    /// <summary>
    /// Allocates a new upload 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="UploadTexture2D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
    public static UploadTexture2D<T> AllocateUploadTexture2D<T>(this GraphicsDevice device, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, allocationMode);
    }

    /// <summary>
    /// Allocates a new upload 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="UploadTexture3D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>, <paramref name="depth"/>].</returns>
    public static UploadTexture3D<T> AllocateUploadTexture3D<T>(this GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, depth, allocationMode);
    }

    /// <summary>
    /// Allocates a new readback buffer with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="length">The length of the buffer to allocate.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="ReadBackBuffer{T}"/> instance of size <paramref name="length"/>.</returns>
    [RequiresUnreferencedCode("This method reads type info of all fields of the resource element type (recursively).")]
    public static ReadBackBuffer<T> AllocateReadBackBuffer<T>(this GraphicsDevice device, int length, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, length, allocationMode);
    }

    /// <summary>
    /// Allocates a new readback 1D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="ReadBackTexture1D{T}"/> instance of size [<paramref name="width"/>].</returns>
    public static ReadBackTexture1D<T> AllocateReadBackTexture1D<T>(this GraphicsDevice device, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, allocationMode);
    }

    /// <summary>
    /// Allocates a new readback 2D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="ReadBackTexture2D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>].</returns>
    public static ReadBackTexture2D<T> AllocateReadBackTexture2D<T>(this GraphicsDevice device, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, allocationMode);
    }

    /// <summary>
    /// Allocates a new readback 3D texture with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the buffer.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <param name="depth">The depth of the texture.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="ReadBackTexture3D{T}"/> instance of size [<paramref name="width"/>, <paramref name="height"/>, <paramref name="depth"/>].</returns>
    public static ReadBackTexture3D<T> AllocateReadBackTexture3D<T>(this GraphicsDevice device, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return new(device, width, height, depth, allocationMode);
    }

    /// <summary>
    /// Loads a new readonly 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static ReadOnlyTexture2D<T, TPixel> LoadReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, string filename)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(filename);

        return device.LoadReadOnlyTexture2D<T, TPixel>(filename.AsSpan());
    }

    /// <summary>
    /// Loads a new readonly 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static ReadOnlyTexture2D<T, TPixel> LoadReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<char> filename)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        using UploadTexture2D<T> upload = WICHelper.Instance.LoadTexture<T>(device, filename);

        ReadOnlyTexture2D<T, TPixel> texture = device.AllocateReadOnlyTexture2D<T, TPixel>(upload.Width, upload.Height);

        upload.CopyTo(texture);

        return texture;
    }

    /// <summary>
    /// Loads a new readonly 2D texture with the contents of the specified buffer.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="span">The buffer with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static ReadOnlyTexture2D<T, TPixel> LoadReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<byte> span)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        using UploadTexture2D<T> upload = WICHelper.Instance.LoadTexture<T>(device, span);

        ReadOnlyTexture2D<T, TPixel> texture = device.AllocateReadOnlyTexture2D<T, TPixel>(upload.Width, upload.Height);

        upload.CopyTo(texture);

        return texture;
    }

    /// <summary>
    /// Loads a new readonly 2D texture with the contents of the specified stream.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="stream">The stream with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadOnlyTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static ReadOnlyTexture2D<T, TPixel> LoadReadOnlyTexture2D<T, TPixel>(this GraphicsDevice device, Stream stream)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(stream);

        using UploadTexture2D<T> upload = WICHelper.Instance.LoadTexture<T>(device, stream);

        ReadOnlyTexture2D<T, TPixel> texture = device.AllocateReadOnlyTexture2D<T, TPixel>(upload.Width, upload.Height);

        upload.CopyTo(texture);

        return texture;
    }

    /// <summary>
    /// Loads a new writeable 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static ReadWriteTexture2D<T, TPixel> LoadReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, string filename)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(filename);

        return device.LoadReadWriteTexture2D<T, TPixel>(filename.AsSpan());
    }

    /// <summary>
    /// Loads a new writeable 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static ReadWriteTexture2D<T, TPixel> LoadReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<char> filename)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        using UploadTexture2D<T> upload = WICHelper.Instance.LoadTexture<T>(device, filename);

        ReadWriteTexture2D<T, TPixel> texture = device.AllocateReadWriteTexture2D<T, TPixel>(upload.Width, upload.Height);

        upload.CopyTo(texture);

        return texture;
    }

    /// <summary>
    /// Loads a new writeable 2D texture with the contents of the specified buffer.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="span">The buffer with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static ReadWriteTexture2D<T, TPixel> LoadReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, ReadOnlySpan<byte> span)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);

        using UploadTexture2D<T> upload = WICHelper.Instance.LoadTexture<T>(device, span);

        ReadWriteTexture2D<T, TPixel> texture = device.AllocateReadWriteTexture2D<T, TPixel>(upload.Width, upload.Height);

        upload.CopyTo(texture);

        return texture;
    }

    /// <summary>
    /// Loads a new writeable 2D texture with the contents of the specified stream.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="stream">The stream with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="ReadWriteTexture2D{T, TPixel}"/> instance with the contents of the specified file.</returns>
    public static ReadWriteTexture2D<T, TPixel> LoadReadWriteTexture2D<T, TPixel>(this GraphicsDevice device, Stream stream)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(stream);

        using UploadTexture2D<T> upload = WICHelper.Instance.LoadTexture<T>(device, stream);

        ReadWriteTexture2D<T, TPixel> texture = device.AllocateReadWriteTexture2D<T, TPixel>(upload.Width, upload.Height);

        upload.CopyTo(texture);

        return texture;
    }

    /// <summary>
    /// Loads a new upload 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    /// <returns>A <see cref="UploadTexture2D{T}"/> instance with the contents of the specified file.</returns>
    public static UploadTexture2D<T> LoadUploadTexture2D<T>(this GraphicsDevice device, string filename)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(filename);

        return device.LoadUploadTexture2D<T>(filename.AsSpan());
    }

    /// <summary>
    /// Loads a new upload 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    /// <returns>A <see cref="UploadTexture2D{T}"/> instance with the contents of the specified file.</returns>
    public static UploadTexture2D<T> LoadUploadTexture2D<T>(this GraphicsDevice device, ReadOnlySpan<char> filename)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return WICHelper.Instance.LoadTexture<T>(device, filename);
    }

    /// <summary>
    /// Loads a new upload 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="span">The buffer with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="UploadTexture2D{T}"/> instance with the contents of the specified file.</returns>
    public static UploadTexture2D<T> LoadUploadTexture2D<T>(this GraphicsDevice device, ReadOnlySpan<byte> span)
        where T : unmanaged
    {
        Guard.IsNotNull(device);

        return WICHelper.Instance.LoadTexture<T>(device, span);
    }

    /// <summary>
    /// Loads a new upload 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="stream">The stream with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="UploadTexture2D{T}"/> instance with the contents of the specified file.</returns>
    public static UploadTexture2D<T> LoadUploadTexture2D<T>(this GraphicsDevice device, Stream stream)
        where T : unmanaged
    {
        Guard.IsNotNull(device);
        Guard.IsNotNull(stream);

        return WICHelper.Instance.LoadTexture<T>(device, stream);
    }
}
