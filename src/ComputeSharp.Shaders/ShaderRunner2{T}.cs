using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Shaders.Extensions;
using ComputeSharp.Shaders.Renderer;
using ComputeSharp.Shaders.Renderer.Models;
using ComputeSharp.Shaders.Translation;
using ComputeSharp.Shaders.Translation.Interop;
using ComputeSharp.Shaders.Translation.Models;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_COMMAND_LIST_TYPE;

namespace ComputeSharp.Shaders
{
    /// <summary>
    /// A <see langword="class"/> with helper methods to support <see cref="ShaderRunner2{T}"/>.
    /// This type is primarily needed to avoid having fields being repeated per generic instantiation.
    /// </summary>
    internal static class ShaderRunner2
    {
        /// <summary>
        /// A thread-safe map of reusable GPU buffers for captured locals.
        /// </summary>
        [ThreadStatic]
        private static ConditionalWeakTable<GraphicsDevice2, ConstantBuffer2<Int4>>? variablesBuffers;

        /// <summary>
        /// Gets the reusable <see cref="ConstantBuffer2{T}"/> instance for a shader invocation.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice2"/> to use to run the shader.</param>
        /// <returns>The reusable <see cref="ConstantBuffer2{T}"/> instance to populate.</returns>
        [Pure]
        public static ConstantBuffer2<Int4> GetVariablesBuffer(GraphicsDevice2 device)
        {
            ConditionalWeakTable<GraphicsDevice2, ConstantBuffer2<Int4>> map = variablesBuffers ??= new();

            return map.GetValue(device, gpu => new ConstantBuffer2<Int4>(gpu, 4096));
        }
    }

    /// <summary>
    /// A <see langword="class"/> responsible for performing all the necessary operations to compile and run a compute shader.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to run.</typeparam>
    internal static class ShaderRunner2<T>
        where T : struct, IComputeShader
    {
        /// <summary>
        /// The mapping used to cache and reuse compiled shaders.
        /// </summary>
        private static readonly Dictionary<ShaderKey, CachedShader2<T>> ShadersCache = new();

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice2"/> instance, with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice2"/> to use to run the shader.</param>
        /// <param name="x">The number of iterations to run on the X axis.</param>
        /// <param name="y">The number of iterations to run on the Y axis.</param>
        /// <param name="z">The number of iterations to run on the Z axis.</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
        public static void Run(GraphicsDevice2 device, int x, int y, int z, in T shader)
        {
            // Calculate the optimized thread num values
            int threadsX, threadsY = 1, threadsZ = 1;
            if (y == 1 && z == 1) threadsX = (int)device.WavefrontSize;
            else if (z == 1) threadsX = threadsY = 8;
            else threadsX = threadsY = threadsZ = 4;

            Run(device, x, y, z, threadsX, threadsY, threadsZ, shader);
        }

        /// <summary>
        /// Compiles and runs the input shader on a target <see cref="GraphicsDevice2"/> instance, with the specified parameters.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice2"/> to use to run the shader.</param>
        /// <param name="x">The number of iterations to run on the X axis.</param>
        /// <param name="y">The number of iterations to run on the Y axis.</param>
        /// <param name="z">The number of iterations to run on the Z axis.</param>
        /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
        /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
        /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
        public static void Run(
            GraphicsDevice2 device,
            int x, int y, int z,
            int threadsX, int threadsY, int threadsZ,
            in T shader)
        {
            // Create the shader key
            ShaderKey key = new(ShaderHashCodeProvider.GetHashCode(shader), threadsX, threadsY, threadsZ);
            CachedShader2<T> shaderData;
            PipelineData? pipelineData;

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
                if (!shaderData.CachedPipelines.TryGetValue(device, out pipelineData))
                {
                    CreatePipelineData(device, shaderData, out pipelineData);
                }
            }

            // Create the commands list and set the pipeline state
            using CommandList2 commandList = new(device, D3D12_COMMAND_LIST_TYPE_COMPUTE);

            commandList.SetPipelineData(pipelineData);

            // Extract the dispatch data for the shader invocation
            using DispatchData2 dispatchData = shaderData.Loader.GetDispatchData(shader, x, y, z);

            // Load the captured buffers
            ReadOnlySpan<D3D12_GPU_DESCRIPTOR_HANDLE> resources = dispatchData.Resources;

            for (int i = 0; i < resources.Length; i++)
            {
                commandList.SetComputeRootDescriptorTable(i + 1, resources[i]);
            }

            // Initialize the loop targets and the captured values
            ConstantBuffer2<Int4> variablesBuffer = ShaderRunner2.GetVariablesBuffer(device);

            variablesBuffer.SetData(dispatchData.Variables);

            commandList.SetComputeRootDescriptorTable(0, variablesBuffer.D3D12GpuDescriptorHandle);

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
        /// Loads a shader with the specified parameters.
        /// </summary>
        /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
        /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
        /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
        /// <param name="shaderData">The <see cref="CachedShader2{T}"/> instance to return with the cached shader data.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadShader(int threadsX, int threadsY, int threadsZ, in T shader, out CachedShader2<T> shaderData)
        {
            // Load the input shader
            ShaderLoader2<T> shaderLoader = ShaderLoader2<T>.Load(shader);

            // Render the loaded shader
            string shaderSource = ShaderRenderer.Instance.Render(new ShaderInfo
            {
                BuffersList = shaderLoader.HslsBuffersInfo,
                FieldsList = shaderLoader.FieldsInfo,
                NumThreadsX = threadsX,
                NumThreadsY = threadsY,
                NumThreadsZ = threadsZ,
                ThreadsIdsVariableName = shaderLoader.ThreadsIdsVariableName,
                ShaderBody = shaderLoader.MethodBody,
                FunctionsList = shaderLoader.FunctionsInfo,
                LocalFunctionsList = shaderLoader.LocalFunctionsInfo
            });

            // Compile the loaded shader to HLSL bytecode
            IDxcBlobObject shaderBytecode = new(ShaderCompiler2.CompileShader(shaderSource));

            // Get the cached shader data
            shaderData = new CachedShader2<T>(shaderLoader, shaderBytecode);
        }

        /// <summary>
        /// Creates and caches a <see cref="PipelineData"/> instance for a given shader.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice2"/> to use to run the shader.</param>
        /// <param name="shaderData">The <see cref="CachedShader2{T}"/> instance with the data on the loaded shader.</param>
        /// <param name="pipelineData">The resulting <see cref="PipelineData"/> instance to use to run the shader.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static unsafe void CreatePipelineData(GraphicsDevice2 device, in CachedShader2<T> shaderData, out PipelineData pipelineData)
        {
            using ComPtr<ID3D12RootSignature> d3D12RootSignature = device.D3D12Device->CreateRootSignature(shaderData.Loader.D3D12DescriptorRanges1);

            D3D12_SHADER_BYTECODE d3D12ShaderBytecode = new((ID3DBlob*)shaderData.Bytecode.DxcBlob);

            using ComPtr<ID3D12PipelineState> d3D12PipelineState = device.D3D12Device->CreateComputePipelineState(d3D12RootSignature.Get(), d3D12ShaderBytecode);

            pipelineData = new PipelineData(d3D12RootSignature.Move(), d3D12PipelineState.Move());

            shaderData.CachedPipelines.Add(device, pipelineData);
        }
    }
}
