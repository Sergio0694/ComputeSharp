using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ComputeSharp.Shaders.Dispatching;
using ComputeSharp.__Internals;
using ComputeSharp.Shaders.Models;

#pragma warning disable CS0618

namespace ComputeSharp.Shaders.Loading;

/// <summary>
/// A <see langword="class"/> responsible for performing all the necessary operations to compile and load shader data.
/// </summary>
/// <typeparam name="T">The type of compute shader to load.</typeparam>
internal static class PipelineDataLoader<T>
    where T : struct, IShader
{
    /// <summary>
    /// The mapping used to cache and reuse compiled shaders.
    /// </summary>
    private static readonly Dictionary<ShaderKey, ICachedShader> ShadersCache = new();

    /// <summary>
    /// Gets the <see cref="PipelineData"/> instance for a given shader.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    /// <returns>The <see cref="PipelineData"/> instance for a given shader.</returns>
    public static unsafe PipelineData GetPipelineData(GraphicsDevice device, int threadsX, int threadsY, int threadsZ, ref T shader)
    {
        ShaderKey key = new(shader.GetDispatchId(), threadsX, threadsY, threadsZ);

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
            if (!shaderData.CachedPipelines.TryGetValue(device, out PipelineData? pipelineData))
            {
                CreatePipelineData(device, shaderData, out pipelineData);
            }

            return pipelineData;
        }
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
        ShaderBytecodeLoader bytecodeLoader = default;

        shader.LoadBytecode(ref bytecodeLoader, threadsX, threadsY, threadsZ);

        shaderData = bytecodeLoader.GetCachedShader();
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
        device.CreatePipelineData(out pipelineData);
        shaderData.CachedPipelines.Add(device, pipelineData);
    }
}