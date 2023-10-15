using System;
using System.ComponentModel;
using ComputeSharp.Interop;

namespace ComputeSharp.__Internals;

/// <summary>
/// A base <see langword="interface"/> representing a given shader that can be dispatched.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("This interface is not intended to be used directly by user code")]
public interface IShader
{
    /// <summary>
    /// The number of threads in each thread group for the X axis.
    /// </summary>
    int ThreadsX { get; }

    /// <summary>
    /// The number of threads in each thread group for the Y axis.
    /// </summary>
    int ThreadsY { get; }

    /// <summary>
    /// The number of threads in each thread group for the Z axis.
    /// </summary>
    int ThreadsZ { get; }

    /// <summary>
    /// Gets the size in bytes of the constant buffer for the current shader.
    /// </summary>
    /// <remarks>
    /// Constant buffer data is bound to shaders via 32 bit root constants, and loaded before dispatching via
    /// <see href="https://learn.microsoft.com/windows/win32/api/d3d12/nf-d3d12-id3d12graphicscommandlist-setcomputeroot32bitconstants"><c>ID3D12GraphicsCommandList::SetComputeRoot32BitConstants</c></see>,
    /// so the size must be a multiple of the size of a DWORD value (ie. 4 bytes).
    /// </remarks>
    int ConstantBufferSize { get; }

    /// <summary>
    /// Gets whether the shader requires a static sampler being declared and initialized.
    /// </summary>
    /// <remarks>
    /// When requested, a static sampler with linear sampling and mirror addressing will be bound to <c>register(s)</c>.
    /// </remarks>
    bool IsStaticSamplerRequired { get; }

    /// <summary>
    /// Gets the resource descriptor ranges for all resources that should be bound to this shader.
    /// </summary>
    ReadOnlyMemory<ResourceDescriptorRange> ResourceDescriptorRanges { get; }

    /// <summary>
    /// Gets the HLSL source code for the current shader instance.
    /// </summary>
    string HlslSource { get; }

    /// <summary>
    /// Gets the HLSL bytecode for the current shader.
    /// </summary>
    ReadOnlyMemory<byte> HlslBytecode { get; }

    /// <summary>
    /// Loads the constant buffer of a given input shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of data loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the data.</param>
    /// <param name="x">The number of iterations to run on the X axis.</param>
    /// <param name="y">The number of iterations to run on the Y axis.</param>
    /// <param name="z">The number of iterations to run on the Z axis.</param>
    void LoadConstantBuffer<TLoader>(ref TLoader loader, int x, int y, int z)
        where TLoader : struct, IConstantBufferLoader;

    /// <summary>
    /// Loads the graphics resources for the shader.
    /// </summary>
    /// <typeparam name="TLoader">The type of graphics resource loader being used.</typeparam>
    /// <param name="loader">The <typeparamref name="TLoader"/> instance to use to load the data.</param>
    void LoadGraphicsResources<TLoader>(ref TLoader loader)
        where TLoader : struct, IGraphicsResourceLoader;
}