using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Shaders.Dispatching;
using ComputeSharp.Shaders.Extensions;
using ComputeSharp.Shaders.Translation;
using ComputeSharp.Shaders.Translation.Models;
using ComputeSharp.__Internals;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.Shaders;

/// <summary>
/// A <see langword="class"/> responsible for performing all the necessary operations to compile and run a compute shader.
/// </summary>
/// <typeparam name="T">The type of compute shader to run.</typeparam>
internal static class ShaderRunner<T>
    where T : struct, IShader
{
    /// <summary>
    /// The mapping used to cache and reuse compiled shaders.
    /// </summary>
    private static readonly Dictionary<ShaderKey, ICachedShader> ShadersCache = new();

    /// <summary>
    /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="z">The number of iterations to run on the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    public static unsafe void Run(GraphicsDevice device, int x, int y, int z, ref T shader)
    {
        // Here we calculate the optimized [numthreads] values. Using small thread group sizes leads to the
        // best average performance due to better occupancy of the GPU with different shaders, using any
        // number of registers and any amount of thread local storage. We use the wave size of the device in
        // use for 1D dispatches, otherwise a multiple of 32 that is greater than or equal to 64, which still
        // results in an evenly divisible number of waves per thread group on all existing GPU devices.
        bool xIs1 = x == 1;
        bool yIs1 = y == 1;
        bool zIs1 = z == 1;
        int mask = *(byte*)&xIs1 << 2 | *(byte*)&yIs1 << 1 | *(byte*)&zIs1;
        int threadsX;
        int threadsY;
        int threadsZ;

        switch (mask - 1)
        {
            case 0: // (_, _, 1)
                threadsX = threadsY = 8;
                threadsZ = 1;
                break;
            case 1: // (_, 1, _)
                threadsX = threadsZ = 8;
                threadsY = 1;
                break;
            case 2: // (_, 1, 1)
                threadsX = (int)device.WavefrontSize;
                threadsY = threadsZ = 1;
                break;
            case 3: // (1, _, _)
                threadsX = 1;
                threadsY = threadsZ = 8;
                break;
            case 4: // (1, _, 1)
                threadsX = threadsZ = 1;
                threadsY = (int)device.WavefrontSize;
                break;
            case 5: // (1, 1, _)
                threadsX = threadsY = 1;
                threadsZ = (int)device.WavefrontSize;
                break;
            default: // (_, _, _)
                threadsX = threadsY = threadsZ = 4;
                break;
        }

        Run(device, x, y, z, threadsX, threadsY, threadsZ, ref shader);
    }

    /// <summary>
    /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="z">The number of iterations to run on the Z axis.</param>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    public static unsafe void Run(
        GraphicsDevice device,
        int x,
        int y,
        int z,
        int threadsX,
        int threadsY,
        int threadsZ,
        ref T shader)
    {
        device.ThrowIfDisposed();

        Guard.IsGreaterThan(x, 0, nameof(x));
        Guard.IsGreaterThan(y, 0, nameof(y));
        Guard.IsGreaterThan(z, 0, nameof(z));
        Guard.IsBetweenOrEqualTo(threadsX, 1, 1024, nameof(threadsX));
        Guard.IsBetweenOrEqualTo(threadsY, 1, 1024, nameof(threadsY));
        Guard.IsBetweenOrEqualTo(threadsZ, 1, 64, nameof(threadsZ));
        Guard.IsLessThanOrEqualTo(threadsX * threadsY * threadsZ, 1024, "threadsXYZ");

        int groupsX = Math.DivRem(x, threadsX, out int modX) + (modX == 0 ? 0 : 1);
        int groupsY = Math.DivRem(y, threadsY, out int modY) + (modY == 0 ? 0 : 1);
        int groupsZ = Math.DivRem(z, threadsZ, out int modZ) + (modZ == 0 ? 0 : 1);

        Guard.IsBetweenOrEqualTo(groupsX, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION, nameof(groupsX));
        Guard.IsBetweenOrEqualTo(groupsY, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION, nameof(groupsX));
        Guard.IsBetweenOrEqualTo(groupsZ, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION, nameof(groupsX));

        ShaderKey key = new(shader.GetDispatchId(), threadsX, threadsY, threadsZ);
        PipelineData? pipelineData;

        lock (ShadersCache)
        {
            // Get or preload the shader
            if (!ShadersCache.TryGetValue(key, out ICachedShader? shaderData))
            {
                LoadShader(threadsX, threadsY, threadsZ, ref shader, out shaderData);

                // Cache for later use
                ShadersCache.Add(key, shaderData);
            }

            // Create the reusable pipeline state
            if (!shaderData.CachedPipelines.TryGetValue(device, out pipelineData))
            {
                CreatePipelineData(device, shaderData, out pipelineData);
            }
        }

        using CommandList commandList = new(device, pipelineData.D3D12PipelineState);

        commandList.D3D12GraphicsCommandList->SetComputeRootSignature(pipelineData.D3D12RootSignature);

        ComputeShaderDispatchDataLoader dataLoader = new(commandList.D3D12GraphicsCommandList);

        shader.LoadDispatchData(ref dataLoader, device, x, y, z);

        commandList.D3D12GraphicsCommandList->Dispatch((uint)groupsX, (uint)groupsY, (uint)groupsZ);
        commandList.ExecuteAndWaitForCompletion();
    }

    /// <summary>
    /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="texture">The target texture to invoke the pixel shader upon.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the pixel shader to run.</param>
    public static unsafe void Run<TPixel>(GraphicsDevice device, IReadWriteTexture2D<TPixel> texture, ref T shader)
        where TPixel : unmanaged
    {
        device.ThrowIfDisposed();

        int x = texture.Width;
        int y = texture.Height;
        int threadsX = 8;
        int threadsY = 8;
        int groupsX = Math.DivRem(x, threadsX, out int modX) + (modX == 0 ? 0 : 1);
        int groupsY = Math.DivRem(y, threadsY, out int modY) + (modY == 0 ? 0 : 1);

        Guard.IsBetweenOrEqualTo(groupsX, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION, nameof(groupsX));
        Guard.IsBetweenOrEqualTo(groupsY, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION, nameof(groupsX));

        ShaderKey key = new(shader.GetDispatchId(), threadsX, threadsY, 1);
        PipelineData? pipelineData;

        lock (ShadersCache)
        {
            // Get or preload the shader
            if (!ShadersCache.TryGetValue(key, out ICachedShader? shaderData))
            {
                LoadShader(threadsX, threadsY, 1, ref shader, out shaderData);

                // Cache for later use
                ShadersCache.Add(key, shaderData);
            }

            // Create the reusable pipeline state
            if (!shaderData.CachedPipelines.TryGetValue(device, out pipelineData))
            {
                CreatePipelineData(device, shaderData, out pipelineData);
            }
        }

        using CommandList commandList = new(device, pipelineData.D3D12PipelineState);

        commandList.D3D12GraphicsCommandList->SetComputeRootSignature(pipelineData.D3D12RootSignature);

        PixelShaderDispatchDataLoader dataLoader = new(commandList.D3D12GraphicsCommandList);

        shader.LoadDispatchData(ref dataLoader, device, x, y, 1);

        // Load the implicit output texture
        commandList.D3D12GraphicsCommandList->SetComputeRootDescriptorTable(
            1,
            ((GraphicsResourceHelper.IGraphicsResource)texture).ValidateAndGetGpuDescriptorHandle(device));

        commandList.D3D12GraphicsCommandList->Dispatch((uint)groupsX, (uint)groupsY, 1);
        commandList.ExecuteAndWaitForCompletion();
    }

    /// <summary>
    /// Loads a shader with the specified parameters.
    /// </summary>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    /// <param name="shaderData">The <see cref="ICachedShader"/> instance to return with the cached shader data.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static unsafe void LoadShader(int threadsX, int threadsY, int threadsZ, ref T shader, out ICachedShader shaderData)
    {
        if (shader.TryGetBytecode(threadsX, threadsY, threadsZ, out ReadOnlySpan<byte> bytecode))
        {
            shaderData = new ICachedShader.Embedded(bytecode);
        }
        else
        {
            shader.BuildHlslString(out ArrayPoolStringBuilder builder, threadsX, threadsY, threadsZ);

            using ComPtr<IDxcBlob> dxcBlobBytecode = ShaderCompiler.Instance.CompileShader(builder.WrittenSpan);

            builder.Dispose();

            shaderData = new ICachedShader.Dynamic(dxcBlobBytecode.Get());
        }
    }

    /// <summary>
    /// Creates and caches a <see cref="PipelineData"/> instance for a given shader.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="shaderData">The <see cref="ICachedShader"/> instance with the data on the loaded shader.</param>
    /// <param name="pipelineData">The resulting <see cref="PipelineData"/> instance to use to run the shader.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static unsafe void CreatePipelineData(GraphicsDevice device, ICachedShader shaderData, out PipelineData pipelineData)
    {
        using ComPtr<ID3D12RootSignature> d3D12RootSignature = default;

        ShaderDispatchMetadataLoader metadataLoader = new(device.D3D12Device);

        Unsafe.NullRef<T>().LoadDispatchMetadata(ref metadataLoader, out *(IntPtr*)&d3D12RootSignature);
        
        using ComPtr<ID3D12PipelineState> d3D12PipelineState = device.D3D12Device->CreateComputePipelineState(d3D12RootSignature.Get(), shaderData.D3D12ShaderBytecode);

        pipelineData = new PipelineData(d3D12RootSignature.Get(), d3D12PipelineState.Get());

        shaderData.CachedPipelines.Add(device, pipelineData);
    }
}
