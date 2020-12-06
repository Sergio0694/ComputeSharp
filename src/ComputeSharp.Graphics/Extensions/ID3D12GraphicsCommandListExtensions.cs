using TerraFX.Interop;

namespace ComputeSharp.Graphics.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extensions for the <see cref="ID3D12GraphicsCommandList"/> type.
    /// </summary>
    internal static unsafe class ID3D12GraphicsCommandListExtensions
    {
        /// <summary>
        /// Copies a texture memory region from one resource (a buffer) to another (a texture).
        /// </summary>
        /// <param name="d3D12GraphicsCommandList">The <see cref="ID3D12GraphicsCommandList"/> instance in use.</param>
        /// <param name="d3D12ResourceDestination">The destination <see cref="ID3D12Resource"/> (a texture) to write to.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="z">The depthwise offset in the destination texture.</param>
        /// <param name="d3D12ResourceSource">The source <see cref="ID3D12Resource"/> (a buffer) to read from.</param>
        /// <param name="d3D12PlacedSubresourceFootprintSource">The <see cref="D3D12_PLACED_SUBRESOURCE_FOOTPRINT"/> value describing <paramref name="d3D12ResourceSource"/>.</param>

        public static void CopyTextureRegion(
            this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList,
            ID3D12Resource* d3D12ResourceDestination,
            uint x,
            uint y,
            ushort z,
            ID3D12Resource* d3D12ResourceSource,
            D3D12_PLACED_SUBRESOURCE_FOOTPRINT* d3D12PlacedSubresourceFootprintSource)
        {
            D3D12_TEXTURE_COPY_LOCATION
                d3D12TextureCopyLocationDestination = new(d3D12ResourceDestination, 0),
                d3D12TextureCopyLocationSource = new(d3D12ResourceSource, in *d3D12PlacedSubresourceFootprintSource);

            d3D12GraphicsCommandList.CopyTextureRegion(&d3D12TextureCopyLocationDestination, x, y, z, &d3D12TextureCopyLocationSource, null);
        }

        /// <summary>
        /// Copies a texture memory region from one resource (a texture) to another (a buffer).
        /// </summary>
        /// <param name="d3D12GraphicsCommandList">The <see cref="ID3D12GraphicsCommandList"/> instance in use.</param>
        /// <param name="d3D12ResourceDestination">The destination <see cref="ID3D12Resource"/> (a buffer) to write to.</param>
        /// <param name="d3D12PlacedSubresourceFootprintDestination">The <see cref="D3D12_PLACED_SUBRESOURCE_FOOTPRINT"/> value describing <paramref name="d3D12ResourceDestination"/>.</param>
        /// <param name="d3D12ResourceSource">The source <see cref="ID3D12Resource"/> (a texture) to read from.</param>
        /// <param name="x">The horizontal offset in the source texture.</param>
        /// <param name="y">The vertical offset in the source texture.</param>
        /// <param name="z">The depthwise offset in the source texture.</param>
        /// <param name="width">The width of the memory area to read from.</param>
        /// <param name="height">The height of the memory area to read from.</param>
        /// <param name="depth">The depth of the memory area to read from.</param>
        public static void CopyTextureRegion(
            this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList,
            ID3D12Resource* d3D12ResourceDestination,
            D3D12_PLACED_SUBRESOURCE_FOOTPRINT* d3D12PlacedSubresourceFootprintDestination,
            ID3D12Resource* d3D12ResourceSource,
            uint x,
            uint y,
            ushort z,
            uint width,
            uint height,
            ushort depth)
        {
            D3D12_TEXTURE_COPY_LOCATION
                d3D12TextureCopyLocationDestination = new(d3D12ResourceDestination, in *d3D12PlacedSubresourceFootprintDestination),
                d3D12TextureCopyLocationSource = new(d3D12ResourceSource, 0);
            D3D12_BOX d3D12Box = new((int)x, (int)y, z, (int)(x + width), (int)(y + height), z + depth);

            d3D12GraphicsCommandList.CopyTextureRegion(&d3D12TextureCopyLocationDestination, 0, 0, 0, &d3D12TextureCopyLocationSource, &d3D12Box);
        }
    }
}
