using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using ComputeSharp.Shaders.Extensions;
using ComputeSharp.Descriptors;
using ComputeSharp.Interop;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_DESCRIPTOR_RANGE_FLAGS;

namespace ComputeSharp.Shaders.Loading;

/// <summary>
/// A <see langword="class"/> responsible for performing all the necessary operations to compile and load shader data.
/// </summary>
/// <typeparam name="T">The type of compute shader to load.</typeparam>
internal static unsafe class PipelineDataLoader<T>
    where T : struct, IComputeShaderDescriptor<T>
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
    private static PipelineData CreatePipelineData(GraphicsDevice device)
    {
        PipelineData pipelineData;

        using (ComPtr<ID3D12RootSignature> d3D12RootSignature = default)
        using (ComPtr<ID3D12PipelineState> d3D12PipelineState = default)
        {
            CreateD3D12RootSignature(device.D3D12Device, d3D12RootSignature.GetAddressOf());
            CreateD3D12PipelineState(device.D3D12Device, d3D12RootSignature.Get(), d3D12PipelineState.GetAddressOf());

            pipelineData = new PipelineData(d3D12RootSignature.Get(), d3D12PipelineState.Get());
        }

        // Add the pipeline to the cache for this type
        CachedPipelines.Add(device, pipelineData);

        // Register the newly created pipeline data to enable early disposal
        device.RegisterPipelineData(pipelineData);

        return pipelineData;
    }

    /// <summary>
    /// Creates a D3D12 root signature for the current shader type.
    /// </summary>
    /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the root signature.</param>
    /// <param name="d3D12RootSignature">The allocated <see cref="ID3D12RootSignature"/> instance.</param>
    private static void CreateD3D12RootSignature(ID3D12Device* d3D12Device, ID3D12RootSignature** d3D12RootSignature)
    {
        Unsafe.SkipInit(out T shader);

        ReadOnlySpan<ResourceDescriptorRange> resourceDescriptorRanges = shader.ResourceDescriptorRanges.Span;

        D3D12_DESCRIPTOR_RANGE1[]? d3D12DescriptorRangesArray = null;

        // Create the span and optional array of D3D12_DESCRIPTOR_RANGE1 objects
        Span<D3D12_DESCRIPTOR_RANGE1> d3D12DescriptorRanges = resourceDescriptorRanges.Length <= 8
            ? stackalloc D3D12_DESCRIPTOR_RANGE1[8]
            : d3D12DescriptorRangesArray = ArrayPool<D3D12_DESCRIPTOR_RANGE1>.Shared.Rent(resourceDescriptorRanges.Length);

        // Ensure the size has the right length to match the available descriptor ranges
        d3D12DescriptorRanges = d3D12DescriptorRanges.Slice(0, resourceDescriptorRanges.Length);

        // Initialize the D3D12_DESCRIPTOR_RANGE1 values from the shader descriptor ranges
        for (int i = 0; i < resourceDescriptorRanges.Length; i++)
        {
            ref readonly ResourceDescriptorRange resourceDescriptorRange = ref resourceDescriptorRanges[i];
            ref D3D12_DESCRIPTOR_RANGE1 d3D12DescriptorRange = ref d3D12DescriptorRanges[i];

            d3D12DescriptorRange.RangeType = (D3D12_DESCRIPTOR_RANGE_TYPE)resourceDescriptorRange.Type;
            d3D12DescriptorRange.NumDescriptors = 1;
            d3D12DescriptorRange.BaseShaderRegister = resourceDescriptorRange.Register;
            d3D12DescriptorRange.RegisterSpace = 0;
            d3D12DescriptorRange.Flags = D3D12_DESCRIPTOR_RANGE_FLAG_NONE;
            d3D12DescriptorRange.OffsetInDescriptorsFromTableStart = D3D12.D3D12_DESCRIPTOR_RANGE_OFFSET_APPEND;
        }

        // Calculate the actual size of the constant buffer in DWORD values
        int root32BitConstantCount = shader.ConstantBufferSize / sizeof(int);

        // Create the D3D12 root signature
        d3D12Device->CreateRootSignature(
            root32BitConstantCount,
            d3D12DescriptorRanges,
            shader.IsStaticSamplerRequired,
            d3D12RootSignature);

        // Return the array to the pool if no exceptions have been thrown
        if (d3D12DescriptorRangesArray is not null)
        {
            ArrayPool<D3D12_DESCRIPTOR_RANGE1>.Shared.Return(d3D12DescriptorRangesArray);
        }
    }

    /// <summary>
    /// Creates a D3D12 pipeline state for the current shader type.
    /// </summary>
    /// <param name="d3D12Device">The target <see cref="ID3D12Device"/> to use to create the root signature.</param>
    /// <param name="d3D12RootSignature">The input <see cref="ID3D12RootSignature"/> value to use.</param>
    /// <param name="d3D12PipelineState">The resulting <see cref="ID3D12PipelineState"/> instance.</param>
    private static void CreateD3D12PipelineState(ID3D12Device* d3D12Device, ID3D12RootSignature* d3D12RootSignature, ID3D12PipelineState** d3D12PipelineState)
    {
        Unsafe.SkipInit(out T shader);

        fixed (byte* hlslBytecodePtr = shader.HlslBytecode.Span)
        {
            D3D12_SHADER_BYTECODE d3D12ShaderBytecode = new(hlslBytecodePtr, (nuint)shader.HlslBytecode.Length);

            d3D12Device->CreateComputePipelineState(d3D12RootSignature, d3D12ShaderBytecode, d3D12PipelineState);
        }
    }
}