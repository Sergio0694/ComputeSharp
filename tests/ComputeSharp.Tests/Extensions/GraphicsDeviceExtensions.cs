using System;
using System.IO;
using ComputeSharp.Resources;

namespace ComputeSharp.Tests.Extensions;

/// <summary>
/// A helper class for testing resource types.
/// </summary>
public static class GraphicsDeviceExtensions
{
    /// <summary>
    /// Allocates a new <see cref="Buffer{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the buffer.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
    /// <param name="type">The type of buffer to allocate.</param>
    /// <param name="length">The length of the buffer to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="Buffer{T}"/> instance of the requested size.</returns>
    public static Buffer<T> AllocateBuffer<T>(this GraphicsDevice device, Type type, int length, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ConstantBuffer<>) => device.AllocateConstantBuffer<T>(length, allocationMode),
            _ when type == typeof(ReadOnlyBuffer<>) => device.AllocateReadOnlyBuffer<T>(length, allocationMode),
            _ when type == typeof(ReadWriteBuffer<>) => device.AllocateReadWriteBuffer<T>(length, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture1D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="Texture1D{T}"/> instance of the requested size.</returns>
    public static Texture1D<T> AllocateTexture1D<T>(this GraphicsDevice device, Type type, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture1D<>) => device.AllocateReadOnlyTexture1D<T>(width, allocationMode),
            _ when type == typeof(ReadWriteTexture1D<>) => device.AllocateReadWriteTexture1D<T>(width, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture1D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="Texture1D{T}"/> instance of the requested size.</returns>
    public static Texture1D<T> AllocateTexture1D<T, TPixel>(this GraphicsDevice device, Type type, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture1D<,>) => device.AllocateReadOnlyTexture1D<T, TPixel>(width, allocationMode),
            _ when type == typeof(ReadWriteTexture1D<,>) => device.AllocateReadWriteTexture1D<T, TPixel>(width, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture2D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="height">The height of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance of the requested size.</returns>
    public static Texture2D<T> AllocateTexture2D<T>(this GraphicsDevice device, Type type, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture2D<>) => device.AllocateReadOnlyTexture2D<T>(width, height, allocationMode),
            _ when type == typeof(ReadWriteTexture2D<>) => device.AllocateReadWriteTexture2D<T>(width, height, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture2D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="height">The height of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance of the requested size.</returns>
    public static Texture2D<T> AllocateTexture2D<T, TPixel>(this GraphicsDevice device, Type type, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture2D<,>) => device.AllocateReadOnlyTexture2D<T, TPixel>(width, height, allocationMode),
            _ when type == typeof(ReadWriteTexture2D<,>) => device.AllocateReadWriteTexture2D<T, TPixel>(width, height, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture3D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="height">The height of the texture to create.</param>
    /// <param name="depth">The depth of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance of the requested size.</returns>
    public static Texture3D<T> AllocateTexture3D<T>(this GraphicsDevice device, Type type, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture3D<>) => device.AllocateReadOnlyTexture3D<T>(width, height, depth, allocationMode),
            _ when type == typeof(ReadWriteTexture3D<>) => device.AllocateReadWriteTexture3D<T>(width, height, depth, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture3D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="height">The height of the texture to create.</param>
    /// <param name="depth">The depth of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="Texture3D{T}"/> instance of the requested size.</returns>
    public static Texture3D<T> AllocateTexture3D<T, TPixel>(this GraphicsDevice device, Type type, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture3D<,>) => device.AllocateReadOnlyTexture3D<T, TPixel>(width, height, depth, allocationMode),
            _ when type == typeof(ReadWriteTexture3D<,>) => device.AllocateReadWriteTexture3D<T, TPixel>(width, height, depth, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="TransferBuffer{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the buffer.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
    /// <param name="type">The type of buffer to allocate.</param>
    /// <param name="length">The length of the buffer to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="TransferBuffer{T}"/> instance of the requested size.</returns>
    public static TransferBuffer<T> AllocateTransferBuffer<T>(this GraphicsDevice device, Type type, int length, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(UploadBuffer<>) => device.AllocateUploadBuffer<T>(length, allocationMode),
            _ when type == typeof(ReadBackBuffer<>) => device.AllocateReadBackBuffer<T>(length, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="TransferTexture1D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the buffer.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
    /// <param name="type">The type of buffer to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="TransferTexture1D{T}"/> instance of the requested size.</returns>
    public static TransferTexture1D<T> AllocateTransferTexture1D<T>(this GraphicsDevice device, Type type, int width, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(UploadTexture1D<>) => device.AllocateUploadTexture1D<T>(width, allocationMode),
            _ when type == typeof(ReadBackTexture1D<>) => device.AllocateReadBackTexture1D<T>(width, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="TransferTexture2D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the buffer.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
    /// <param name="type">The type of buffer to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="height">The height of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="TransferTexture2D{T}"/> instance of the requested size.</returns>
    public static TransferTexture2D<T> AllocateTransferTexture2D<T>(this GraphicsDevice device, Type type, int width, int height, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(UploadTexture2D<>) => device.AllocateUploadTexture2D<T>(width, height, allocationMode),
            _ when type == typeof(ReadBackTexture2D<>) => device.AllocateReadBackTexture2D<T>(width, height, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="TransferTexture3D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="height">The height of the texture to create.</param>
    /// <param name="depth">The depth of the texture to create.</param>
    /// <param name="allocationMode">The allocation mode to use for the new resource.</param>
    /// <returns>A <see cref="TransferTexture3D{T}"/> instance of the requested size.</returns>
    public static TransferTexture3D<T> AllocateTransferTexture3D<T>(this GraphicsDevice device, Type type, int width, int height, int depth, AllocationMode allocationMode = AllocationMode.Default)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(UploadTexture3D<>) => device.AllocateUploadTexture3D<T>(width, height, depth, allocationMode),
            _ when type == typeof(ReadBackTexture3D<>) => device.AllocateReadBackTexture3D<T>(width, height, depth, allocationMode),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Buffer{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the buffer.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
    /// <param name="type">The type of buffer to allocate.</param>
    /// <param name="data">The data to load on the buffer.</param>
    /// <returns>A <see cref="Buffer{T}"/> instance of the requested type.</returns>
    public static Buffer<T> AllocateBuffer<T>(this GraphicsDevice device, Type type, T[] data)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ConstantBuffer<>) => device.AllocateConstantBuffer(data),
            _ when type == typeof(ReadOnlyBuffer<>) => device.AllocateReadOnlyBuffer(data),
            _ when type == typeof(ReadWriteBuffer<>) => device.AllocateReadWriteBuffer(data),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture1D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="data">The data to load on the texture.</param>
    /// <returns>A <see cref="Texture1D{T}"/> instance of the requested type.</returns>
    public static Texture1D<T> AllocateTexture1D<T>(this GraphicsDevice device, Type type, T[] data)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture1D<>) => device.AllocateReadOnlyTexture1D(data),
            _ when type == typeof(ReadWriteTexture1D<>) => device.AllocateReadWriteTexture1D(data),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture2D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="data">The data to load on the texture.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="height">The height of the texture to create.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance of the requested type.</returns>
    public static Texture2D<T> AllocateTexture2D<T>(this GraphicsDevice device, Type type, T[] data, int width, int height)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture2D<>) => device.AllocateReadOnlyTexture2D(data, width, height),
            _ when type == typeof(ReadWriteTexture2D<>) => device.AllocateReadWriteTexture2D(data, width, height),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture3D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="data">The data to load on the texture.</param>
    /// <param name="width">The width of the texture to create.</param>
    /// <param name="height">The height of the texture to create.</param>
    /// <param name="depth">The depth of the texture to create.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance of the requested type.</returns>
    public static Texture3D<T> AllocateTexture3D<T>(this GraphicsDevice device, Type type, T[] data, int width, int height, int depth)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture3D<>) => device.AllocateReadOnlyTexture3D(data, width, height, depth),
            _ when type == typeof(ReadWriteTexture3D<>) => device.AllocateReadWriteTexture3D(data, width, height, depth),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Texture3D{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the texture.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the texture for.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="data">The input 3D array to use as source data for the texture.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance of the requested size.</returns>
    public static Texture3D<T> AllocateTexture3D<T>(this GraphicsDevice device, Type type, T[,,] data)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture3D<>) => device.AllocateReadOnlyTexture3D(data),
            _ when type == typeof(ReadWriteTexture3D<>) => device.AllocateReadWriteTexture3D(data),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Allocates a new <see cref="Buffer{T}"/> instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of items in the buffer.</typeparam>
    /// <param name="device">The target <see cref="GraphicsDevice"/> instance to allocate the buffer for.</param>
    /// <param name="type">The type of buffer to allocate.</param>
    /// <param name="data">The data to load on the buffer.</param>
    /// <returns>A <see cref="Buffer{T}"/> instance of the requested type.</returns>
    public static Buffer<T> AllocateBuffer<T>(this GraphicsDevice device, Type type, Buffer<T> data)
        where T : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ConstantBuffer<>) => device.AllocateConstantBuffer(data),
            _ when type == typeof(ReadOnlyBuffer<>) => device.AllocateReadOnlyBuffer(data),
            _ when type == typeof(ReadWriteBuffer<>) => device.AllocateReadWriteBuffer(data),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Loads an image from a specific file into a target <see cref="UploadTexture2D{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The target <see cref="UploadTexture2D{T}"/> instance.</param>
    /// <param name="inputType">The type of filepath to use.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    public static void Load<T>(this UploadTexture2D<T> texture, Type inputType, string filename)
        where T : unmanaged
    {
        switch (inputType)
        {
            case var _ when inputType == typeof(string):
                texture.Load(filename);
                break;
            case var _ when inputType == typeof(ReadOnlySpan<char>):
                texture.Load(filename);
                break;
            default:
                throw new ArgumentException($"Invalid input type: {inputType}", nameof(inputType));
        }
    }

    /// <summary>
    /// Loads a new readonly 2D texture with the contents of the specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="textureType">The type of texture to allocate.</param>
    /// <param name="inputType">The type of filepath to use.</param>
    /// <param name="filename">The filename of the image file to load and decode into the texture.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance with the contents of the specified file.</returns>
    public static Texture2D<T> LoadTexture2D<T, TPixel>(this GraphicsDevice device, Type textureType, Type inputType, string filename)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        return textureType switch
        {
            _ when textureType == typeof(ReadOnlyTexture2D<,>) => inputType switch
            {
                _ when inputType == typeof(string) => device.LoadReadOnlyTexture2D<T, TPixel>(filename),
                _ when inputType == typeof(ReadOnlySpan<char>) => device.LoadReadOnlyTexture2D<T, TPixel>(filename.AsSpan()),
                _ => throw new ArgumentException($"Invalid input type: {inputType}", nameof(inputType))
            },
            _ when textureType == typeof(ReadWriteTexture2D<,>) => inputType switch
            {
                _ when inputType == typeof(string) => device.LoadReadWriteTexture2D<T, TPixel>(filename),
                _ when inputType == typeof(ReadOnlySpan<char>) => device.LoadReadWriteTexture2D<T, TPixel>(filename.AsSpan()),
                _ => throw new ArgumentException($"Invalid input type: {inputType}", nameof(inputType))
            },
            _ => throw new ArgumentException($"Invalid texture type: {textureType}", nameof(textureType))
        };
    }

    /// <summary>
    /// Loads a new readonly 2D texture with the contents of the specified buffer.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="buffer">The buffer with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance with the contents of the specified buffer.</returns>
    public static Texture2D<T> LoadTexture2D<T, TPixel>(this GraphicsDevice device, Type type, byte[] buffer)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture2D<,>) => device.LoadReadOnlyTexture2D<T, TPixel>(buffer),
            _ when type == typeof(ReadWriteTexture2D<,>) => device.LoadReadWriteTexture2D<T, TPixel>(buffer),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Loads a new readonly 2D texture with the contents of the specified stream.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <typeparam name="TPixel">The type of pixels used on the GPU side.</typeparam>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the texture.</param>
    /// <param name="type">The type of texture to allocate.</param>
    /// <param name="stream">The stream with the image data to load and decode into the texture.</param>
    /// <returns>A <see cref="Texture2D{T}"/> instance with the contents of the specified stream.</returns>
    public static Texture2D<T> LoadTexture2D<T, TPixel>(this GraphicsDevice device, Type type, Stream stream)
        where T : unmanaged, IPixel<T, TPixel>
        where TPixel : unmanaged
    {
        return type switch
        {
            _ when type == typeof(ReadOnlyTexture2D<,>) => device.LoadReadOnlyTexture2D<T, TPixel>(stream),
            _ when type == typeof(ReadWriteTexture2D<,>) => device.LoadReadWriteTexture2D<T, TPixel>(stream),
            _ => throw new ArgumentException($"Invalid type: {type}", nameof(type))
        };
    }

    /// <summary>
    /// Saves a texture to a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture to save to an image.</param>
    /// <param name="inputType">The type of filepath to use.</param>
    /// <param name="filename">The filename of the image file to save.</param>
    public static void Save<T>(this Texture2D<T> texture, Type inputType, string filename)
        where T : unmanaged
    {
        switch (inputType)
        {
            case var _ when inputType == typeof(string):
                texture.Save(filename);
                break;
            case var _ when inputType == typeof(ReadOnlySpan<char>):
                texture.Save(filename.AsSpan());
                break;
            default:
                throw new ArgumentException($"Invalid input type: {inputType}", nameof(inputType));
        }
    }

    /// <summary>
    /// Saves a texture to a specified file.
    /// </summary>
    /// <typeparam name="T">The type of items to store in the texture.</typeparam>
    /// <param name="texture">The texture to save to an image.</param>
    /// <param name="inputType">The type of filepath to use.</param>
    /// <param name="filename">The filename of the image file to save.</param>
    public static void Save<T>(this ReadBackTexture2D<T> texture, Type inputType, string filename)
        where T : unmanaged
    {
        switch (inputType)
        {
            case var _ when inputType == typeof(string):
                texture.Save(filename);
                break;
            case var _ when inputType == typeof(ReadOnlySpan<char>):
                texture.Save(filename.AsSpan());
                break;
            default:
                throw new ArgumentException($"Invalid input type: {inputType}", nameof(inputType));
        }
    }
}