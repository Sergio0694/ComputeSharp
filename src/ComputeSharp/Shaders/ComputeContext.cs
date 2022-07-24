﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using ComputeSharp.__Internals;
using ComputeSharp.Graphics.Commands;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Interop;
using ComputeSharp.Shaders.Dispatching;
using ComputeSharp.Shaders.Loading;
using ComputeSharp.Shaders.Models;
using TerraFX.Interop.DirectX;

#pragma warning disable CS0618

namespace ComputeSharp;

/// <summary>
/// A context to batch compute operations in a single invocation, minimizing GPU overhead.
/// </summary>
/// <remarks>
/// <para>
/// This type must always be used in a <see langword="using"/> statement and disposed properly.
/// Not doing so is undefined behavior and may result in the target device not being disposed correctly.
/// </para>
/// <para>
/// For more documentation on this, see the remarks in <see cref="GraphicsDeviceExtensions.CreateComputeContext(ComputeSharp.GraphicsDevice)"/>.
/// </para>
/// </remarks>
public struct ComputeContext : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// The <see cref="GraphicsDevice"/> instance owning the current context.
    /// </summary>
    private GraphicsDevice? device;

    /// <summary>
    /// The current <see cref="CommandList"/> instance used to dispatch shaders.
    /// </summary>
    private CommandList commandList;

    /// <summary>
    /// The lease to ensure <see cref="device"/> is not disposed until the context is no longer in use.
    /// </summary>
    private ReferenceTracker.Lease lease;

    /// <summary>
    /// Creates a new <see cref="ComputeContext"/> instance with the specified parameters.
    /// </summary>
    /// <param name="device">The <see cref="GraphicsDevice"/> instance owning the current context.</param>
    internal ComputeContext(GraphicsDevice device)
    {
        this.device = device;
        this.commandList = default;
        this.lease = device.GetReferenceTracker().GetLease();
    }

    /// <summary>
    /// Gets the <see cref="ComputeSharp.GraphicsDevice"/> associated with the current instance.
    /// </summary>
    public GraphicsDevice GraphicsDevice
    {
        get
        {
            ThrowInvalidOperationExceptionIfDeviceIsNull();

            return this.device;
        }
    }

    /// <summary>
    /// Inserts a resource barrier for a specific resource.
    /// </summary>
    /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to insert the barrier for.</param>
    internal readonly unsafe void Barrier(ID3D12Resource* d3D12Resource)
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        ref CommandList commandList = ref GetCommandList(in this);

        commandList.D3D12GraphicsCommandList->UnorderedAccessViewBarrier(d3D12Resource);
    }

    /// <summary>
    /// Clears a specific resource.
    /// </summary>
    /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to clear.</param>
    /// <param name="d3D12GpuDescriptorHandle">The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value for the target resource.</param>
    /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> value for the target resource.</param>
    /// <param name="isNormalized">Indicates whether the target resource uses a normalized format.</param>
    internal readonly unsafe void Clear(
        ID3D12Resource* d3D12Resource,
        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle,
        D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle,
        bool isNormalized)
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        ref CommandList commandList = ref GetCommandList(in this, pipelineState: null);

        commandList.D3D12GraphicsCommandList->ClearUnorderedAccessView(d3D12Resource, d3D12GpuDescriptorHandle, d3D12CpuDescriptorHandle, isNormalized);
    }

    /// <summary>
    /// Fills a specific resource.
    /// </summary>
    /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to fill.</param>
    /// <param name="d3D12GpuDescriptorHandle">The <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> value for the target resource.</param>
    /// <param name="d3D12CpuDescriptorHandle">The <see cref="D3D12_CPU_DESCRIPTOR_HANDLE"/> value for the target resource.</param>
    /// <param name="value">The value to use to fill the resource.</param>
    internal readonly unsafe void Fill(
        ID3D12Resource* d3D12Resource,
        D3D12_GPU_DESCRIPTOR_HANDLE d3D12GpuDescriptorHandle,
        D3D12_CPU_DESCRIPTOR_HANDLE d3D12CpuDescriptorHandle,
        Float4 value)
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        ref CommandList commandList = ref GetCommandList(in this, pipelineState: null);

        commandList.D3D12GraphicsCommandList->FillUnorderedAccessView(d3D12Resource, d3D12GpuDescriptorHandle, d3D12CpuDescriptorHandle, value);
    }

    /// <summary>
    /// Runs the input shader with the specified parameters.
    /// </summary>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="z">The number of iterations to run on the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    internal readonly unsafe void Run<T>(int x, int y, int z, ref T shader)
        where T : struct, IComputeShader
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        // Here we calculate the optimized [numthreads] values. Using small thread group sizes leads to the
        // best average performance due to better occupancy of the GPU with different shaders, using any
        // number of registers and any amount of thread local storage. We use 64 for 1D dispatches, otherwise
        // a multiple of 32 that is greater than or equal to 64, which still results in an evenly divisible
        // number of waves per thread group on all existing GPU devices. All GPUs will generally have a wavefront
        // size of either 16 (eg. Intel mobile GPUs), 32 (nvidia GPUs) or 64 (AMD GPUs), so in all cases 64 will
        // be a multiple of that, guaranteeing that all thread waves will be saturated when dispatching shaders.

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
                threadsX = 64;
                threadsY = threadsZ = 1;
                break;
            case 3: // (1, _, _)
                threadsX = 1;
                threadsY = threadsZ = 8;
                break;
            case 4: // (1, _, 1)
                threadsX = threadsZ = 1;
                threadsY = 64;
                break;
            case 5: // (1, 1, _)
                threadsX = threadsY = 1;
                threadsZ = 64;
                break;
            default: // (_, _, _)
                threadsX = threadsY = threadsZ = 4;
                break;
        }

        Run(x, y, z, threadsX, threadsY, threadsZ, ref shader);
    }

    /// <summary>
    /// Runs the input shader with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of compute shader to run.</typeparam>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="z">The number of iterations to run on the Z axis.</param>
    /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
    /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
    /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
    internal readonly unsafe void Run<T>(
        int x,
        int y,
        int z,
        int threadsX,
        int threadsY,
        int threadsZ,
        ref T shader)
        where T : struct, IComputeShader
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        Guard.IsGreaterThan(x, 0);
        Guard.IsGreaterThan(y, 0);
        Guard.IsGreaterThan(z, 0);
        Guard.IsBetweenOrEqualTo(threadsX, 1, 1024);
        Guard.IsBetweenOrEqualTo(threadsY, 1, 1024);
        Guard.IsBetweenOrEqualTo(threadsZ, 1, 64);
        Guard.IsLessThanOrEqualTo(threadsX * threadsY * threadsZ, 1024, "threadsXYZ");

        int groupsX = Math.DivRem(x, threadsX, out int modX) + (modX == 0 ? 0 : 1);
        int groupsY = Math.DivRem(y, threadsY, out int modY) + (modY == 0 ? 0 : 1);
        int groupsZ = Math.DivRem(z, threadsZ, out int modZ) + (modZ == 0 ? 0 : 1);

        Guard.IsBetweenOrEqualTo(groupsX, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION);
        Guard.IsBetweenOrEqualTo(groupsY, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION, nameof(groupsX));
        Guard.IsBetweenOrEqualTo(groupsZ, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION, nameof(groupsX));

        PipelineData pipelineData = PipelineDataLoader<T>.GetPipelineData(this.device, threadsX, threadsY, threadsZ, ref shader);

        ref CommandList commandList = ref GetCommandList(in this, pipelineData.D3D12PipelineState);

        commandList.D3D12GraphicsCommandList->SetComputeRootSignature(pipelineData.D3D12RootSignature);

        ComputeShaderDispatchDataLoader dataLoader = new(commandList.D3D12GraphicsCommandList);

        shader.LoadDispatchData(ref dataLoader, this.device, x, y, z);

        commandList.D3D12GraphicsCommandList->Dispatch((uint)groupsX, (uint)groupsY, (uint)groupsZ);
    }

    /// <summary>
    /// Runs the input shader with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to run.</typeparam>
    /// <typeparam name="TPixel">The type of pixel to work on.</typeparam>
    /// <param name="texture">The target texture to invoke the pixel shader upon.</param>
    /// <param name="shader">The input <typeparamref name="T"/> instance representing the pixel shader to run.</param>
    internal readonly unsafe void Run<T, TPixel>(IReadWriteNormalizedTexture2D<TPixel> texture, ref T shader)
        where T : struct, IPixelShader<TPixel>
        where TPixel : unmanaged
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        int x = texture.Width;
        int y = texture.Height;
        const int threadsX = 8;
        const int threadsY = 8;
        const int threadsZ = 1;
        int groupsX = Math.DivRem(x, threadsX, out int modX) + (modX == 0 ? 0 : 1);
        int groupsY = Math.DivRem(y, threadsY, out int modY) + (modY == 0 ? 0 : 1);

        Guard.IsBetweenOrEqualTo(groupsX, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION);
        Guard.IsBetweenOrEqualTo(groupsY, 1, D3D11.D3D11_CS_DISPATCH_MAX_THREAD_GROUPS_PER_DIMENSION, nameof(groupsX));

        PipelineData pipelineData = PipelineDataLoader<T>.GetPipelineData(this.device, threadsX, threadsY, threadsZ, ref shader);

        ref CommandList commandList = ref GetCommandList(in this, pipelineData.D3D12PipelineState);

        commandList.D3D12GraphicsCommandList->SetComputeRootSignature(pipelineData.D3D12RootSignature);

        PixelShaderDispatchDataLoader dataLoader = new(commandList.D3D12GraphicsCommandList);

        shader.LoadDispatchData(ref dataLoader, this.device, x, y, 1);

        // Load the implicit output texture
        commandList.D3D12GraphicsCommandList->SetComputeRootDescriptorTable(
            1,
            ((GraphicsResourceHelper.IGraphicsResource)texture).ValidateAndGetGpuDescriptorHandle(this.device));

        commandList.D3D12GraphicsCommandList->Dispatch((uint)groupsX, (uint)groupsY, 1);
    }

    /// <summary>
    /// Inserts a transition for a specific resource.
    /// </summary>
    /// <param name="d3D12Resource">The <see cref="ID3D12Resource"/> to change state for.</param>
    /// <param name="d3D12ResourceStatesBefore">The starting <see cref="D3D12_RESOURCE_STATES"/> value for the transition.</param>
    /// <param name="d3D12ResourceStatesAfter">The destnation <see cref="D3D12_RESOURCE_STATES"/> value for the transition.</param>
    internal readonly unsafe void Transition(
        ID3D12Resource* d3D12Resource,
        D3D12_RESOURCE_STATES d3D12ResourceStatesBefore,
        D3D12_RESOURCE_STATES d3D12ResourceStatesAfter)
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        ref CommandList commandList = ref GetCommandList(in this, pipelineState: null);

        commandList.D3D12GraphicsCommandList->TransitionBarrier(d3D12Resource, d3D12ResourceStatesBefore, d3D12ResourceStatesAfter);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        this.device = null;

        if (!this.commandList.IsAllocated)
        {
            this.lease.Dispose();

            return;
        }

        try
        {
            this.commandList.ExecuteAndWaitForCompletion();
        }
        finally
        {
            this.lease.Dispose();
        }
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask DisposeAsync()
    {
        ThrowInvalidOperationExceptionIfDeviceIsNull();

        this.device = null;

        if (!this.commandList.IsAllocated)
        {
            this.lease.Dispose();

#if NET6_0_OR_GREATER
            return ValueTask.CompletedTask;
#else
            return new(Task.CompletedTask);
#endif
        }

        try
        {
            return this.commandList.ExecuteAndWaitForCompletionAsync();
        }
        finally
        {
            this.lease.Dispose();
        }
    }

    /// <summary>
    /// Gets the current <see cref="CommandList"/> instance.
    /// </summary>
    /// <param name="this">The current <see cref="ComputeContext"/> instance.</param>
    /// <returns>A reference to the <see cref="CommandList"/> instance to use.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the <see cref="CommandList"/> has not been initialized yet.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe ref CommandList GetCommandList(in ComputeContext @this)
    {
        // This method has to take the context by readonly reference to allow callers to be marked as readonly.
        // This is needed to skip the hidden copies done by Roslyn, which would break the dispatching, as the
        // original context would not see the changes done by the following queued dispatches.
        ref CommandList commandList = ref Unsafe.AsRef(in @this).commandList;

        if (!commandList.IsAllocated)
        {
            ThrowHelper.ThrowInvalidOperationException("The current compute context has not yet been initialized.");
        }

        return ref commandList;
    }

    /// <summary>
    /// Gets the current <see cref="CommandList"/> instance, and initializes it as needed.
    /// </summary>
    /// <param name="this">The current <see cref="ComputeContext"/> instance.</param>
    /// <param name="pipelineState">The input <see cref="ID3D12PipelineState"/> to load.</param>
    /// <returns>A reference to the <see cref="CommandList"/> instance to use.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static unsafe ref CommandList GetCommandList(in ComputeContext @this, ID3D12PipelineState* pipelineState)
    {
        ref ComputeContext context = ref Unsafe.AsRef(in @this);

        if (context.commandList.IsAllocated)
        {
            // Skip setting the pipeline state if the new state is null. This is the case when the upcoming
            // operation is not a shader dispatch, but just a resource clear. In this case there is no state.
            if (pipelineState is not null)
            {
                context.commandList.D3D12GraphicsCommandList->SetPipelineState(pipelineState);
            }
        }
        else
        {
            context.commandList = new CommandList(context.device!, pipelineState);
        }

        return ref context.commandList;
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if <see cref="device"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if <see cref="device"/> is <see langword="null"/>.</exception>
    [MemberNotNull(nameof(device))]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly void ThrowInvalidOperationExceptionIfDeviceIsNull()
    {
        if (this.device is null)
        {
            ThrowHelper.ThrowInvalidOperationException("The current compute context is not in a valid state.");
        }
    }
}
