using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Resources.Interop;
using ComputeSharp.Descriptors;
using ComputeSharp.Win32;

namespace ComputeSharp.Shaders.Dispatching;

/// <summary>
/// An <see cref="IGraphicsResource"/> loader for compute shaders dispatched via <see cref="ID3D12GraphicsCommandList"/>.
/// </summary>
internal readonly unsafe struct D3D12GraphicsCommandListGraphicsResourceLoader : IGraphicsResourceLoader
{
    /// <summary>
    /// The <see cref="ID3D12GraphicsCommandList"/> object in use.
    /// </summary>
    private readonly ID3D12GraphicsCommandList* d3D12GraphicsCommandList;

    /// <summary>
    /// The <see cref="GraphicsDevice"/> object in use.
    /// </summary>
    private readonly GraphicsDevice graphicsDevice;

    /// <summary>
    /// The offset into the compute root descriptor table for loaded resources.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is used to correctly align the explicit resources captured by shaders, with the implicit ones
    /// that are injected by ComputeSharp. For instance, the constant buffer is always added automatically.
    /// </para>
    /// <para>
    /// For compute shaders capturing an implicit texture, this is used to skip the first target resource,
    /// as that is loaded explicitly by the shader runner using the target texture passed in by the user,
    /// which is not actually captured by the shader itself. The target root descriptor table index is therefore
    /// 2 in this case, as it needs to skip not  only the this implicit texture to draw on, but also the constant
    /// buffer with capture values.
    /// </para>
    /// </remarks>
    private readonly uint rootParameterOffset;

    /// <summary>
    /// Creates a new <see cref="D3D12GraphicsCommandListGraphicsResourceLoader"/> instance.
    /// </summary>
    /// <param name="d3D12GraphicsCommandList">The <see cref="ID3D12GraphicsCommandList"/> object to use.</param>
    /// <param name="graphicsDevice">The <see cref="GraphicsDevice"/> instance that <paramref name="d3D12GraphicsCommandList"/> is tied to.</param>
    /// <param name="rootParameterOffset">The offset into the compute root descriptor table for loaded resources.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal D3D12GraphicsCommandListGraphicsResourceLoader(
        ID3D12GraphicsCommandList* d3D12GraphicsCommandList,
        GraphicsDevice graphicsDevice,
        uint rootParameterOffset)
    {
        this.d3D12GraphicsCommandList = d3D12GraphicsCommandList;
        this.graphicsDevice = graphicsDevice;
        this.rootParameterOffset = rootParameterOffset;
    }

    /// <inheritdoc/>
    void IGraphicsResourceLoader.LoadGraphicsResource(IGraphicsResource resource, uint index)
    {
        default(ArgumentNullException).ThrowIfNull(resource);

        if (resource is not ID3D12ReadOnlyResource readOnlyResource)
        {
            default(ArgumentException).Throw(nameof(resource));

            return;
        }

        this.d3D12GraphicsCommandList->SetComputeRootDescriptorTable(
            RootParameterIndex: this.rootParameterOffset + index,
            BaseDescriptor: readOnlyResource.ValidateAndGetGpuDescriptorHandle(this.graphicsDevice));
    }
}