using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Shaders.Dispatching;
using ComputeSharp.Shaders.Extensions;
using ComputeSharp.__Internals;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

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
    /// The map of cached <see cref="PipelineData"/> instances for each GPU in use.
    /// </summary>
    private static readonly ConditionalWeakTable<GraphicsDevice, PipelineData> CachedPipelines = new();

    /// <summary>
    /// Gets the <see cref="PipelineData"/> instance for a given shader.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <returns>The <see cref="PipelineData"/> instance for a given shader.</returns>
    public static PipelineData GetPipelineData(GraphicsDevice device)
    {
        return CachedPipelines.GetValue(device, CreatePipelineData);
    }

    /// <summary>
    /// Creates and caches a <see cref="PipelineData"/> instance for a given shader.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
    /// <returns>The resulting <see cref="PipelineData"/> instance to use to run the shader.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static unsafe PipelineData CreatePipelineData(GraphicsDevice device)
    {
        PipelineData pipelineData;

        using (ComPtr<ID3D12RootSignature> d3D12RootSignature = default)
        {
            ShaderDispatchMetadataLoader metadataLoader = new(device.D3D12Device);

            Unsafe.SkipInit(out T shader);

            shader.LoadDispatchMetadata(ref metadataLoader, out *(IntPtr*)&d3D12RootSignature);

            ReadOnlyMemory<byte> hlslBytecode = shader.HlslBytecode;

            fixed (byte* hlslBytecodePtr = hlslBytecode.Span)
            {
                D3D12_SHADER_BYTECODE d3D12ShaderBytecode = new(hlslBytecodePtr, (nuint)hlslBytecode.Length);

                using ComPtr<ID3D12PipelineState> d3D12PipelineState = device.D3D12Device->CreateComputePipelineState(d3D12RootSignature.Get(), d3D12ShaderBytecode);

                pipelineData = new PipelineData(d3D12RootSignature.Get(), d3D12PipelineState.Get());
            }
        }

        // Add the pipeline to the cache for this type
        CachedPipelines.Add(device, pipelineData);

        // Register the newly created pipeline data to enable early disposal
        device.RegisterPipelineData(pipelineData);

        return pipelineData;
    }
}