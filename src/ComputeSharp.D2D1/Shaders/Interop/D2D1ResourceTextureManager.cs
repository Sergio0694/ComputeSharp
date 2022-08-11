using System;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using TerraFX.Interop.DirectX;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides an <c>ID2D1ResourceTextureManager</c> implementation, which can be used to manage resources in
/// an effect created with <see cref="D2D1PixelShaderEffect"/> and share them across multiple effect instances.
/// </summary>
/// <remarks>
/// <para>
/// The built-in <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nn-d2d1_1-id2d1effect"><c>ID2D1Effect</c></see>
/// implementation provided by <see cref="D2D1PixelShaderEffect"/> uses resource texture manager objects to handle resource textures.
/// </para>
/// <para>
/// These managers are COM objects implementing the following interface:
/// <code>
/// [uuid(3C4FC7E4-A419-46CA-B5F6-66EB4FF18D64)]
/// interface ID2D1ResourceTextureManager : IUnknown
/// {
///     HRESULT CreateResourceTexture(
///         [in, optional] const GUID                             *resourceId,
///         [in]           const D2D1_RESOURCE_TEXTURE_PROPERTIES *resourceTextureProperties,
///         [in, optional] const BYTE                             *data,
///         [in, optional] const UINT32                           *strides,
///                        UINT32                                 dataSize);
/// 
///     HRESULT Update(
///         [in, optional] const UINT32 *minimumExtents,
///         [in, optional] const UINT32 *maximimumExtents,
///         [in]           const UINT32 *strides,
///                        UINT32       dimensions,
///         [in]           const BYTE   *data,
///                        UINT32       dataCount);
/// };
/// </code>
/// </para>
/// <para>
/// These can be used to create resource textures, pass them to effects, and also share resources among different effects.
/// </para>
/// <para>
/// This interface is implemented by ComputeSharp.D2D1, and it can be used through the APIs in <see cref="D2D1ResourceTextureManager"/>.
/// That is, <see cref="CreateResourceTextureManager"/> is first use to create an <c>ID2D1ResourceTextureManager</c> instance. Then,
/// <see cref="CreateResourceTexture"/> can be used to initialize the resource texture held by the manager. The manager can also be
/// assigned to an effect at any time, using the available property indices from <see cref="D2D1PixelShaderEffectProperty"/>. The
/// <see cref="UpdateResourceTexture"/> API can also be used to update texture data after the initial creation.
/// </para>
/// <para>
/// The <c>ID2D1ResourceTextureManager</c> contract allows callers to create a resource texture at any time even before the effect
/// has been initialized and the manager assigned to it. In that case, the manager will buffer the data internally, and will defer
/// the creation of the actual resource texture until when it's possible for it to do so, at which point it will also free up the buffer.
/// </para>
/// <para>
/// If there was a need to have for a custom resource manager, in order for external implementations to be accepted by the effect objects
/// created by <see cref="D2D1PixelShaderEffect"/>, the <c>ID2D1ResourceTextureManagerInternal</c> interface also needs to be implemented:
/// <code>
/// [uuid(5CBB1024-8EA1-4689-81BF-8AD190B5EF5D)]
/// interface ID2D1ResourceTextureManager : IUnknown
/// {
///     HRESULT Initialize([in] ID2D1EffectContext *effectContext);
/// 
///     HRESULT GetResourceTexture([out] ID2D1ResourceTexture **resourceTexture);
/// };
/// </code>
/// </para>
/// </remarks>
[Guid("3C4FC7E4-A419-46CA-B5F6-66EB4FF18D64")]
public static unsafe class D2D1ResourceTextureManager
{
    /// <summary>
    /// Creates a new <c>ID2D1ResourceTextureManager</c> instance.
    /// </summary>
    /// <param name="resourceTextureManager">A pointer to the resulting <c>ID2D1ResourceTextureManager</c> instance.</param>
    public static void CreateResourceTextureManager(void** resourceTextureManager)
    {
        // TODO: validate argument and return

        _ = ResourceTextureManager.Factory((ResourceTextureManager**)resourceTextureManager);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resourceTextureManager"></param>
    /// <param name="resourceId"></param>
    /// <param name="extents"></param>
    /// <param name="bufferPrecision"></param>
    /// <param name="channelDepth"></param>
    /// <param name="filter"></param>
    /// <param name="extendModes"></param>
    /// <param name="data"></param>
    /// <param name="strides"></param>
    public static void CreateResourceTexture(
        void* resourceTextureManager,
        Guid resourceId,
        ReadOnlySpan<uint> extents,
        D2D1BufferPrecision bufferPrecision,
        D2D1ChannelDepth channelDepth,
        D2D1Filter filter,
        ReadOnlySpan<D2D1ExtendMode> extendModes,
        ReadOnlySpan<byte> data,
        ReadOnlySpan<uint> strides)
    {
        // TODO: validate argument and return

        fixed (uint* pExtents = extents)
        fixed (D2D1ExtendMode* pExtendModes = extendModes)
        fixed (byte* pData = data)
        fixed (uint* pStrides = strides)
        {
            D2D1_RESOURCE_TEXTURE_PROPERTIES d2D1ResourceTextureProperties;
            d2D1ResourceTextureProperties.extents = pExtents;
            d2D1ResourceTextureProperties.dimensions = (uint)extents.Length;
            d2D1ResourceTextureProperties.bufferPrecision = (D2D1_BUFFER_PRECISION)bufferPrecision;
            d2D1ResourceTextureProperties.channelDepth = (D2D1_CHANNEL_DEPTH)channelDepth;
            d2D1ResourceTextureProperties.filter = (D2D1_FILTER)filter;
            d2D1ResourceTextureProperties.extendModes = (D2D1_EXTEND_MODE*)pExtendModes;

            _ = ((delegate* unmanaged[Stdcall]<void*, Guid*, D2D1_RESOURCE_TEXTURE_PROPERTIES*, byte*, uint*, uint, int>)(*(void***)resourceTextureManager)[3])(
                resourceTextureManager,
                &resourceId,
                &d2D1ResourceTextureProperties,
                pData,
                pStrides,
                (uint)data.Length);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resourceTextureManager"></param>
    /// <param name="minimumExtents"></param>
    /// <param name="maximumExtents"></param>
    /// <param name="strides"></param>
    /// <param name="dimensions"></param>
    /// <param name="data"></param>
    public static void UpdateResourceTexture(
        void* resourceTextureManager,
        ReadOnlySpan<uint> minimumExtents,
        ReadOnlySpan<uint> maximumExtents,
        ReadOnlySpan<uint> strides,
        uint dimensions,
        ReadOnlySpan<byte> data)
    {
        // TODO: validate argument and return

        fixed (uint* pMinimumExtents = minimumExtents)
        fixed (uint* pMaximumExtents = maximumExtents)
        fixed (uint* pStrides = strides)
        fixed (byte* pData = data)
        {
            _ = ((delegate* unmanaged[Stdcall]<void*, uint*, uint*, uint*, uint, byte*, uint, int>)(*(void***)resourceTextureManager)[4])(
                resourceTextureManager,
                pMinimumExtents,
                pMaximumExtents,
                pStrides,
                dimensions,
                pData,
                (uint)data.Length);
        }
    }
}
