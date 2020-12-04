using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;

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
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> parameter curreently in use by the texture.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="d3D12ResourceSource">The source <see cref="ID3D12Resource"/> (a buffer) to read from.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        /// <param name="elementSizeInBytes">The size of each element to copy.</param>

        public static void CopyTextureRegion(
            this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList,
            ID3D12Resource* d3D12ResourceDestination,
            DXGI_FORMAT dxgiFormat,
            uint x,
            uint y,
            ID3D12Resource* d3D12ResourceSource,
            uint width,
            uint height,
            uint elementSizeInBytes)
        {
            D3D12_SUBRESOURCE_FOOTPRINT d3D12SubresourceFootprint = new(
                dxgiFormat,
                width,
                height,
                1,
                (elementSizeInBytes * width + FX.D3D12_TEXTURE_DATA_PITCH_ALIGNMENT - 1) & ~((uint)FX.D3D12_TEXTURE_DATA_PITCH_ALIGNMENT - 1));
            D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint;
            d3D12PlacedSubresourceFootprint.Offset = 0;
            d3D12PlacedSubresourceFootprint.Footprint = d3D12SubresourceFootprint;
            D3D12_TEXTURE_COPY_LOCATION
                d3D12TextureCopyLocationDestination = new(d3D12ResourceDestination, 0),
                d3D12TextureCopyLocationSource = new(d3D12ResourceSource, d3D12PlacedSubresourceFootprint);

            d3D12GraphicsCommandList.CopyTextureRegion(&d3D12TextureCopyLocationDestination, x, y, 0, &d3D12TextureCopyLocationSource, null);
        }

        /// <summary>
        /// Copies a texture memory region from one resource (a texture) to another (a buffer).
        /// </summary>
        /// <param name="d3D12GraphicsCommandList">The <see cref="ID3D12GraphicsCommandList"/> instance in use.</param>
        /// <param name="d3D12ResourceDestination">The destination <see cref="ID3D12Resource"/> (a buffer) to write to.</param>
        /// <param name="elementSizeInBytes">The size of each element to copy.</param>
        /// <param name="d3D12ResourceSource">The source <see cref="ID3D12Resource"/> (a texture) to read from.</param>
        /// <param name="dxgiFormat">The <see cref="DXGI_FORMAT"/> parameter curreently in use by the texture.</param>
        /// <param name="x">The horizontal offset in the destination texture.</param>
        /// <param name="y">The vertical offset in the destination texture.</param>
        /// <param name="width">The width of the memory area to write to.</param>
        /// <param name="height">The height of the memory area to write to.</param>
        public static void CopyTextureRegion(
            this ref ID3D12GraphicsCommandList d3D12GraphicsCommandList,
            ID3D12Resource* d3D12ResourceDestination,
            uint elementSizeInBytes,
            ID3D12Resource* d3D12ResourceSource,
            DXGI_FORMAT dxgiFormat,
            uint x,
            uint y,
            uint width,
            uint height)
        {
            D3D12_SUBRESOURCE_FOOTPRINT d3D12SubresourceFootprint = new(
                dxgiFormat,
                width,
                height,
                1,
                (elementSizeInBytes * width + FX.D3D12_TEXTURE_DATA_PITCH_ALIGNMENT - 1) & ~((uint)FX.D3D12_TEXTURE_DATA_PITCH_ALIGNMENT - 1));
            D3D12_PLACED_SUBRESOURCE_FOOTPRINT d3D12PlacedSubresourceFootprint;
            d3D12PlacedSubresourceFootprint.Offset = 0;
            d3D12PlacedSubresourceFootprint.Footprint = d3D12SubresourceFootprint;
            D3D12_TEXTURE_COPY_LOCATION
                d3D12TextureCopyLocationDestination = new(d3D12ResourceDestination, d3D12PlacedSubresourceFootprint),
                d3D12TextureCopyLocationSource = new(d3D12ResourceSource, 0);
            D3D12_BOX d3D12Box = new((int)x, (int)y, (int)(x + width), (int)(y + height));

            d3D12GraphicsCommandList.CopyTextureRegion(&d3D12TextureCopyLocationDestination, 0, 0, 0, &d3D12TextureCopyLocationSource, &d3D12Box);
        }
    }
}
