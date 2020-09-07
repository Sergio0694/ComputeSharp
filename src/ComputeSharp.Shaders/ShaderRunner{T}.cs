using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Shaders.Renderer;
using ComputeSharp.Shaders.Renderer.Models;
using ComputeSharp.Shaders.Translation;
using ComputeSharp.Shaders.Translation.Models;
using Vortice.Direct3D12;
using CommandList = ComputeSharp.Graphics.Commands.CommandList;
using PipelineState = ComputeSharp.Graphics.Commands.PipelineState;

namespace ComputeSharp.Shaders
{
    /// <summary>
    /// A <see langword="class"/> with helper methods to support <see cref="ShaderRunner{T}"/>.
    /// This type is primarily needed to avoid having fields being repeated per generic instantiation.
    /// </summary>
    internal static class ShaderRunner
    {
        /// <summary>
        /// A thread-safe map of reusable GPU buffers for captured locals
        /// </summary>
        [ThreadStatic]
        private static ConditionalWeakTable<GraphicsDevice, ConstantBuffer<Int4>>? variablesBuffers;

        /// <summary>
        /// Gets the reusable <see cref="ConstantBuffer{T}"/> instance for a shader invocation
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <returns>The reusable <see cref="ConstantBuffer{T}"/> instance to populate</returns>
        [Pure]
        public static ConstantBuffer<Int4> GetVariablesBuffer(GraphicsDevice device)
        {
            var map = variablesBuffers ??= new ConditionalWeakTable<GraphicsDevice, ConstantBuffer<Int4>>();

            return map.GetValue(device, gpu => new ConstantBuffer<Int4>(gpu, 4096));
        }
    }

    /// <summary>
    /// A <see langword="class"/> responsible for performing all the necessary operations to compile and run a compute shader
    /// </summary>
    /// <typeparam name="T">The type of compute shader to run</typeparam>
    internal static class ShaderRunner<T>
        where T : struct, IComputeShader
    {
        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of iterations to run on the X axis</param>
        /// <param name="y">The number of iterations to run on the Y axis</param>
        /// <param name="z">The number of iterations to run on the Z axis</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run</param>
        public static void Run(GraphicsDevice device, int x, int y, int z, in T shader)
        {
            // Calculate the optimized thread num values
            int threadsX, threadsY = 1, threadsZ = 1;
            if (y == 1 && z == 1) threadsX = device.WavefrontSize;
            else if (z == 1) threadsX = threadsY = 8;
            else threadsX = threadsY = threadsZ = 4;

            Run(device, x, y, z, threadsX, threadsY, threadsZ, shader);
        }

        /// <summary>
        /// The mapping used to cache and reuse compiled shaders
        /// </summary>
        private static readonly Dictionary<ShaderKey, CachedShader<T>> ShadersCache = new Dictionary<ShaderKey, CachedShader<T>>();

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice"/> instance, with the specified parameters
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="x">The number of iterations to run on the X axis</param>
        /// <param name="y">The number of iterations to run on the Y axis</param>
        /// <param name="z">The number of iterations to run on the Z axis</param>
        /// <param name="threadsX">The number of threads in each thread group for the X axis</param>
        /// <param name="threadsY">The number of threads in each thread group for the Y axis</param>
        /// <param name="threadsZ">The number of threads in each thread group for the Z axis</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run</param>
        public static void Run(
            GraphicsDevice device,
            int x, int y, int z,
            int threadsX, int threadsY, int threadsZ,
            in T shader)
        {
            // Create the shader key
            ShaderKey key = new ShaderKey(ShaderHashCodeProvider.GetHashCode(shader), threadsX, threadsY, threadsZ);
            CachedShader<T> shaderData;
            PipelineState pipelineState;

            lock (ShadersCache)
            {
                // Get or preload the shader
                if (!ShadersCache.TryGetValue(key, out shaderData))
                {
                    LoadShader(threadsX, threadsY, threadsZ, shader, out shaderData);

                    // Cache for later use
                    ShadersCache.Add(key, shaderData);
                }

                // Create the reusable pipeline state
                if (!shaderData.CachedPipelines.TryGetValue(device, out pipelineState))
                {
                    CreatePipelineState(device, shaderData, out pipelineState);
                }
            }

            // Create the commands list and set the pipeline state
            using CommandList commandList = new CommandList(device, CommandListType.Compute);
            commandList.SetPipelineState(pipelineState);

            // Extract the dispatch data for the shader invocation
            using DispatchData dispatchData = shaderData.Loader.GetDispatchData(shader, x, y, z);

            // Load the captured buffers
            Span<GraphicsResource> resources = dispatchData.Resources;
            for (int i = 0; i < resources.Length; i++)
            {
                commandList.SetComputeRootDescriptorTable(i + 1, resources[i]);
            }

            // Initialize the loop targets and the captured values
            ConstantBuffer<Int4> variablesBuffer = ShaderRunner.GetVariablesBuffer(device);
            variablesBuffer.SetData(dispatchData.Variables);
            commandList.SetComputeRootDescriptorTable(0, variablesBuffer);

            // Calculate the dispatch values
            int
                groupsX = Math.DivRem(x, threadsX, out int modX) + (modX == 0 ? 0 : 1),
                groupsY = Math.DivRem(y, threadsY, out int modY) + (modY == 0 ? 0 : 1),
                groupsZ = Math.DivRem(z, threadsZ, out int modZ) + (modZ == 0 ? 0 : 1);

            // Dispatch and wait for completion
            commandList.Dispatch(groupsX, groupsY, groupsZ);
            commandList.ExecuteAndWaitForCompletion();
        }

        /// <summary>
        /// Loads a shader with the specified parameters
        /// </summary>
        /// <param name="threadsX">The number of threads in each thread group for the X axis</param>
        /// <param name="threadsY">The number of threads in each thread group for the Y axis</param>
        /// <param name="threadsZ">The number of threads in each thread group for the Z axis</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run</param>
        /// <param name="shaderData">The <see cref="CachedShader{T}"/> instance to return with the cached shader data</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadShader(int threadsX, int threadsY, int threadsZ, in T shader, out CachedShader<T> shaderData)
        {
            // Load the input shader
            ShaderLoader<T> shaderLoader = ShaderLoader<T>.Load(shader);

            // Render the loaded shader
            string shaderSource = ShaderRenderer.Instance.Render(new ShaderInfo
            {
                BuffersList = shaderLoader.BuffersList,
                FieldsList = shaderLoader.FieldsList,
                NumThreadsX = threadsX,
                NumThreadsY = threadsY,
                NumThreadsZ = threadsZ,
                ThreadsIdsVariableName = shaderLoader.ThreadsIdsVariableName,
                ShaderBody = shaderLoader.MethodBody,
                FunctionsList = shaderLoader.FunctionsList,
                LocalFunctionsList = shaderLoader.LocalFunctionsList
            });

            // Compile the loaded shader to HLSL bytecode
            ShaderBytecode shaderBytecode = ShaderCompiler.CompileShader(shaderSource);

            // Get the cached shader data
            shaderData = new CachedShader<T>(shaderLoader, shaderBytecode);
        }

        /// <summary>
        /// Creates and caches a <see cref="PipelineState"/> instance for a given shader
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader</param>
        /// <param name="shaderData">The <see cref="CachedShader{T}"/> instance with the data on the loaded shader</param>
        /// <param name="pipelineState">The resulting <see cref="PipelineState"/> instance to use to run the shader</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void CreatePipelineState(GraphicsDevice device, in CachedShader<T> shaderData, out PipelineState pipelineState)
        {
            // Create the root signature for the pipeline and get the pipeline state
            RootSignatureDescription1 rootSignatureDescription = new RootSignatureDescription1(RootSignatureFlags.None, shaderData.Loader.RootParameters);
            VersionedRootSignatureDescription versionedRootSignatureDescription = new VersionedRootSignatureDescription(rootSignatureDescription);
            ID3D12RootSignature rootSignature = device.CreateRootSignature(versionedRootSignatureDescription);
            pipelineState = new PipelineState(device, rootSignature, shaderData.Bytecode);

            // Cache the pipeline state for the target device
            shaderData.CachedPipelines.Add(device, pipelineState);
        }
    }
}
