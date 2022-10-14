using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// Provides an <c>ID2D1ResourceTextureManager</c> implementation, which can be used to manage resources in
/// an effect created with <see cref="D2D1PixelShaderEffect"/> and share them across multiple effect instances.
/// </summary>
/// <remarks>
/// <para>
/// The built-in <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nn-d2d1effectauthor-id2d1effectimpl"><c>ID2D1EffectImpl</c></see>
/// implementation provided by <see cref="D2D1PixelShaderEffect"/> (which makes it possible to register and create
/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nn-d2d1_1-id2d1effect"><c>ID2D1Effect</c></see>
/// instances that can be used to run D2D1 pixel shaders) uses resource texture manager objects to handle resource textures.
/// </para>
/// <para>
/// These managers are COM objects implementing the following interface:
/// <code>
/// [uuid(3C4FC7E4-A419-46CA-B5F6-66EB4FF18D64)]
/// interface ID2D1ResourceTextureManager : IUnknown
/// {
///     HRESULT Initialize(
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
/// For details of how these work, <c>Initialize</c> maps to
/// <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1effectcontext-createresourcetexture"><c>ID2D1EffectContext::CreateResourceTexture</c></see>,
/// whereas <c>Update</c> maps to <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nf-d2d1effectauthor-id2d1resourcetexture-update"><c>ID2D1ResourceTexture::Update</c></see>.
/// The same behavior and parameters as those methods is used.
/// </para>
/// <para>
/// This interface is implemented by ComputeSharp.D2D1, and it can be used through the APIs in <see cref="D2D1ResourceTextureManager"/>.
/// That is, <see cref="Create"/> is first used to create an <c>ID2D1ResourceTextureManager</c> instance. Then, <see cref="Initialize"/>
/// can be used to initialize the resource texture held by the manager. The manager can also be assigned to an effect at any time,
/// using the available property indices from <see cref="D2D1PixelShaderEffectProperty"/>. To update texture data after its initial
/// creation, <see cref="Update(void*, ReadOnlySpan{uint}, ReadOnlySpan{uint}, ReadOnlySpan{uint}, ReadOnlySpan{byte})"/> can be used.
/// </para>
/// <para>
/// An RCW (runtime callable wrapper, see <see href="https://docs.microsoft.com/dotnet/standard/native-interop/runtime-callable-wrapper"/>),
/// is also available for all of these APIs, implemented by the same <see cref="D2D1PixelShaderEffect"/>. In this case, the constructor can
/// be used to create and initialize an instance (equivalent to calling <see cref="Create"/> and <see cref="Initialize"/>), and then
/// <see cref="Update(ReadOnlySpan{uint}, ReadOnlySpan{uint}, ReadOnlySpan{uint}, ReadOnlySpan{byte})"/> is available for texture data updates.
/// The instance implements <see cref="ICustomQueryInterface"/>, which can be used in advanced scenarios to retrieve the underlying COM object.
/// </para>
/// <para>
/// The <c>ID2D1ResourceTextureManager</c> contract allows callers to create a resource texture at any time even before the effect
/// has been initialized and the manager assigned to it. In that case, the manager will buffer the data internally, and will defer
/// the creation of the actual resource texture until it's possible for it to do so, at which point it will also free the buffer.
/// </para>
/// <para>
/// If implementing a custom resource manager is needed, in order for external implementations to be accepted by the effect objects
/// created by <see cref="D2D1PixelShaderEffect"/>, the <c>ID2D1ResourceTextureManagerInternal</c> interface also needs to be implemented:
/// <code>
/// [uuid(5CBB1024-8EA1-4689-81BF-8AD190B5EF5D)]
/// interface ID2D1ResourceTextureManagerInternal : IUnknown
/// {
///     HRESULT Initialize(
///         [in]           ID2D1EffectContext *effectContext,
///         [in, optional] const UINT32       *dimensions);
/// 
///     HRESULT GetResourceTexture([out] ID2D1ResourceTexture **resourceTexture);
/// };
/// </code>
/// </para>
/// <para>
/// Note that due to the fact <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nn-d2d1effectauthor-id2d1effectcontext"><c>ID2D1EffectContext</c></see>
/// and <see href="https://docs.microsoft.com/windows/win32/api/d2d1effectauthor/nn-d2d1effectauthor-id2d1resourcetexture"><c>ID2D1ResourceTexture</c></see> are not thread safe,
/// they are only safe to use without additional synchronization when <c>Initialize</c> and <c>GetResourceTexture</c> are called. This happens from the internal
/// <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nn-d2d1_1-id2d1multithread"><c>ID2D1EffectImpl</c></see> object, which is invoked by D2D, which will handle
/// taking the necessary lock before invoking these APIs. Due to the fact the context has to be stored internally, custom implementations have to make sure to retrieve the
/// right <c>ID2D1Multithread</c> instance from the effect context and use that to synchronize all accesses to those two objects from all APIs (including those exposed by
/// <c>ID2D1ResourceTextureManager</c>). The built-in implementation of these two interfaces provided by <see cref="D2D1ResourceTextureManager"/> takes care of all this.
/// </para>
/// <para>
/// The <c>dimensions</c> parameter in <c>ID2D1ResourceTextureManagerInternal::Initialize</c> is optional, and can be passed by an <c>ID2D1Effect</c>
/// when initializing an input resurce texture manager, if the dimensions of the resource texture at the target index are known in advance.
/// This can allow the manager to perform additional validation when the resource texture is initialized. Not providing a value is not an
/// error, but if a resource texture with invalid size is used the effect might fail to render later on and be more difficult to troubleshoot.
/// </para>
/// </remarks>
public sealed unsafe class D2D1ResourceTextureManager : ICustomQueryInterface
{
    /// <summary>
    /// The <see cref="D2D1ResourceTextureManagerImpl"/> object wrapped by the current instance.
    /// </summary>
    private readonly D2D1ResourceTextureManagerImpl* d2D1ResourceManagerImpl;

    /// <summary>
    /// Creates a new <see cref="D2D1ResourceTextureManager"/> instance with the specified parameters.
    /// </summary>
    /// <param name="extents">The extents of the resource to create.</param>
    /// <param name="bufferPrecision">The buffer precision for the resource to create.</param>
    /// <param name="channelDepth">The channel depth for the resource to create.</param>
    /// <param name="filter">The filter for the resource to create.</param>
    /// <param name="extendModes">The extend modes for the resource to create.</param>
    /// <exception cref="Exception">Thrown if creating or initializing the <see cref="D2D1ResourceTextureManager"/> instance failed.</exception>
    /// <remarks>
    /// <para>
    /// Since <see cref="D2D1ResourceTextureManager"/> has a finalizer, which can be invoked at any point on the finalizer
    /// thread, it can only be used in effects associated with a device context using a factory that supports multithreaded
    /// use (ie. the underlying <c>ID2D1Factory</c> has to be created using <c>D2D1_FACTORY_TYPE_MULTI_THREADED </c>).
    /// Not doing so will cause the resource texture manager to fail to be initialized when used in an effect.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1/ne-d2d1-d2d1_factory_type"/>.
    /// </para>
    /// </remarks>
    public D2D1ResourceTextureManager(
        ReadOnlySpan<uint> extents,
        D2D1BufferPrecision bufferPrecision,
        D2D1ChannelDepth channelDepth,
        D2D1Filter filter,
        ReadOnlySpan<D2D1ExtendMode> extendModes)
        : this(
              null,
              extents,
              bufferPrecision,
              channelDepth,
              filter,
              extendModes,
              ReadOnlySpan<byte>.Empty,
              ReadOnlySpan<uint>.Empty)
    {
    }

    /// <summary>
    /// Creates a new <see cref="D2D1ResourceTextureManager"/> instance with the specified parameters.
    /// </summary>
    /// <param name="resourceId">The resource id for the resource to create.</param>
    /// <param name="extents">The extents of the resource to create.</param>
    /// <param name="bufferPrecision">The buffer precision for the resource to create.</param>
    /// <param name="channelDepth">The channel depth for the resource to create.</param>
    /// <param name="filter">The filter for the resource to create.</param>
    /// <param name="extendModes">The extend modes for the resource to create.</param>
    /// <exception cref="Exception">Thrown if creating or initializing the <see cref="D2D1ResourceTextureManager"/> instance failed.</exception>
    /// <remarks>
    /// <para>
    /// Since <see cref="D2D1ResourceTextureManager"/> has a finalizer, which can be invoked at any point on the finalizer
    /// thread, it can only be used in effects associated with a device context using a factory that supports multithreaded
    /// use (ie. the underlying <c>ID2D1Factory</c> has to be created using <c>D2D1_FACTORY_TYPE_MULTI_THREADED </c>).
    /// Not doing so will cause the resource texture manager to fail to be initialized when used in an effect.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1/ne-d2d1-d2d1_factory_type"/>.
    /// </para>
    /// </remarks>
    public D2D1ResourceTextureManager(
        Guid resourceId,
        ReadOnlySpan<uint> extents,
        D2D1BufferPrecision bufferPrecision,
        D2D1ChannelDepth channelDepth,
        D2D1Filter filter,
        ReadOnlySpan<D2D1ExtendMode> extendModes)
        : this(
              &resourceId,
              extents,
              bufferPrecision,
              channelDepth,
              filter,
              extendModes,
              ReadOnlySpan<byte>.Empty,
              ReadOnlySpan<uint>.Empty)
    {
    }

    /// <summary>
    /// Creates a new <see cref="D2D1ResourceTextureManager"/> instance with the specified parameters.
    /// </summary>
    /// <param name="extents">The extents of the resource to create.</param>
    /// <param name="bufferPrecision">The buffer precision for the resource to create.</param>
    /// <param name="channelDepth">The channel depth for the resource to create.</param>
    /// <param name="filter">The filter for the resource to create.</param>
    /// <param name="extendModes">The extend modes for the resource to create.</param>
    /// <param name="data">The data to load in the resource to create.</param>
    /// <param name="strides">The strides for the data supplied for the resource to create.</param>
    /// <exception cref="Exception">Thrown if creating or initializing the <see cref="D2D1ResourceTextureManager"/> instance failed.</exception>
    /// <remarks>
    /// <para>
    /// Since <see cref="D2D1ResourceTextureManager"/> has a finalizer, which can be invoked at any point on the finalizer
    /// thread, it can only be used in effects associated with a device context using a factory that supports multithreaded
    /// use (ie. the underlying <c>ID2D1Factory</c> has to be created using <c>D2D1_FACTORY_TYPE_MULTI_THREADED </c>).
    /// Not doing so will cause the resource texture manager to fail to be initialized when used in an effect.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1/ne-d2d1-d2d1_factory_type"/>.
    /// </para>
    /// </remarks>
    public D2D1ResourceTextureManager(
        ReadOnlySpan<uint> extents,
        D2D1BufferPrecision bufferPrecision,
        D2D1ChannelDepth channelDepth,
        D2D1Filter filter,
        ReadOnlySpan<D2D1ExtendMode> extendModes,
        ReadOnlySpan<byte> data,
        ReadOnlySpan<uint> strides)
        : this(
              null,
              extents,
              bufferPrecision,
              channelDepth,
              filter,
              extendModes,
              data,
              strides)
    {
    }

    /// <summary>
    /// Creates a new <see cref="D2D1ResourceTextureManager"/> instance with the specified parameters.
    /// </summary>
    /// <param name="resourceId">The resource id for the resource to create.</param>
    /// <param name="extents">The extents of the resource to create.</param>
    /// <param name="bufferPrecision">The buffer precision for the resource to create.</param>
    /// <param name="channelDepth">The channel depth for the resource to create.</param>
    /// <param name="filter">The filter for the resource to create.</param>
    /// <param name="extendModes">The extend modes for the resource to create.</param>
    /// <param name="data">The data to load in the resource to create.</param>
    /// <param name="strides">The strides for the data supplied for the resource to create.</param>
    /// <exception cref="Exception">Thrown if creating or initializing the <see cref="D2D1ResourceTextureManager"/> instance failed.</exception>
    /// <remarks>
    /// <para>
    /// Since <see cref="D2D1ResourceTextureManager"/> has a finalizer, which can be invoked at any point on the finalizer
    /// thread, it can only be used in effects associated with a device context using a factory that supports multithreaded
    /// use (ie. the underlying <c>ID2D1Factory</c> has to be created using <c>D2D1_FACTORY_TYPE_MULTI_THREADED </c>).
    /// Not doing so will cause the resource texture manager to fail to be initialized when used in an effect.
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1/ne-d2d1-d2d1_factory_type"/>.
    /// </para>
    /// </remarks>
    public D2D1ResourceTextureManager(
        Guid resourceId,
        ReadOnlySpan<uint> extents,
        D2D1BufferPrecision bufferPrecision,
        D2D1ChannelDepth channelDepth,
        D2D1Filter filter,
        ReadOnlySpan<D2D1ExtendMode> extendModes,
        ReadOnlySpan<byte> data,
        ReadOnlySpan<uint> strides)
        : this(
              &resourceId,
              extents,
              bufferPrecision,
              channelDepth,
              filter,
              extendModes,
              data,
              strides)
    {
    }

    /// <summary>
    /// Creates a new <see cref="D2D1ResourceTextureManager"/> instance with the specified parameters.
    /// </summary>
    /// <param name="resourceId">The resource id for the resource to create.</param>
    /// <param name="extents">The extents of the resource to create.</param>
    /// <param name="bufferPrecision">The buffer precision for the resource to create.</param>
    /// <param name="channelDepth">The channel depth for the resource to create.</param>
    /// <param name="filter">The filter for the resource to create.</param>
    /// <param name="extendModes">The extend modes for the resource to create.</param>
    /// <param name="data">The data to load in the resource to create.</param>
    /// <param name="strides">The strides for the data supplied for the resource to create.</param>
    /// <exception cref="Exception">Thrown if creating or initializing the <see cref="D2D1ResourceTextureManager"/> instance failed.</exception>
    private D2D1ResourceTextureManager(
        Guid* resourceId,
        ReadOnlySpan<uint> extents,
        D2D1BufferPrecision bufferPrecision,
        D2D1ChannelDepth channelDepth,
        D2D1Filter filter,
        ReadOnlySpan<D2D1ExtendMode> extendModes,
        ReadOnlySpan<byte> data,
        ReadOnlySpan<uint> strides)
    {
        // Manually validate the parameters here to ensure the native objects wouldn't try
        // to read out of bounds and potentially AV. For those cases, we just throw here.
        if (extents.Length == 0 ||
            extendModes.Length == 0 ||
            extents.Length != extendModes.Length ||
            (data.Length > 0 && strides.Length != extents.Length - 1))
        {
            GC.SuppressFinalize(this);

            Marshal.ThrowExceptionForHR(E.E_INVALIDARG);
        }

        fixed (D2D1ResourceTextureManagerImpl** d2D1ResourceManagerImpl = &this.d2D1ResourceManagerImpl)
        {
            D2D1ResourceTextureManagerImpl.Factory(d2D1ResourceManagerImpl, requiresMultithread: true);
        }

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

            int hresult = D2D1ResourceTextureManagerImpl.Initialize(
                @this: this.d2D1ResourceManagerImpl,
                resourceId: resourceId,
                resourceTextureProperties: &d2D1ResourceTextureProperties,
                data: pData,
                strides: pStrides,
                dataSize: (uint)data.Length);

            Marshal.ThrowExceptionForHR(hresult);
        }
    }

    /// <summary>
    /// Releases the underlying <c>ID2D1ResourceTextureManager</c> object.
    /// </summary>
    ~D2D1ResourceTextureManager()
    {
        if (this.d2D1ResourceManagerImpl is not null)
        {
            this.d2D1ResourceManagerImpl->Release();
        }
    }

    /// <summary>
    /// Updates the specific resource texture inside the specific range or box using the supplied data.
    /// </summary>
    /// <param name="minimumExtents">The "left" extent of the updates if specified. If <see langword="null"/>, the entire texture is updated.</param>
    /// <param name="maximimumExtents">The "right" extent of the updates if specified. If <see langword="null"/>, the entire texture is updated.</param>
    /// <param name="strides">The stride to advance through the input data, according to dimension.</param>
    /// <param name="data">The data to be placed into the resource texture.</param>
    public void Update(
        ReadOnlySpan<uint> minimumExtents,
        ReadOnlySpan<uint> maximimumExtents,
        ReadOnlySpan<uint> strides,
        ReadOnlySpan<byte> data)
    {
        fixed (uint* pMinimumExtents = minimumExtents)
        fixed (uint* pMaximumExtents = maximimumExtents)
        fixed (uint* pStrides = strides)
        fixed (byte* pData = data)
        {
            int dimensions = strides.Length + 1;

            // Ensure that if the extents are not null, their length is correct. This is again to
            // avoid the native method assuming they'd be longer than they are and causing an AV.
            if (pMinimumExtents is not null &&
                pMaximumExtents is not null &&
                (minimumExtents.Length != dimensions || maximimumExtents.Length != dimensions))
            {
                Marshal.ThrowExceptionForHR(E.E_INVALIDARG);
            }

            int hresult = D2D1ResourceTextureManagerImpl.Update(
                @this: this.d2D1ResourceManagerImpl,
                minimumExtents: pMinimumExtents,
                maximimumExtents: pMaximumExtents,
                strides: pStrides,
                dimensions: (uint)dimensions,
                data: pData,
                dataCount: (uint)data.Length);

            GC.KeepAlive(this);

            Marshal.ThrowExceptionForHR(hresult);
        }
    }

    /// <summary>
    /// Gets the underlying <see cref="ID2D1ResourceTextureManager"/> object.
    /// </summary>
    /// <param name="resourceTextureManager">The underlying <see cref="ID2D1ResourceTextureManager"/> object.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void GetD2D1ResourceTextureManager(ID2D1ResourceTextureManager** resourceTextureManager)
    {
        _ = this.d2D1ResourceManagerImpl->AddRef();

        *resourceTextureManager = (ID2D1ResourceTextureManager*)this.d2D1ResourceManagerImpl;

        GC.KeepAlive(this);
    }

    /// <inheritdoc/>
    CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
    {
        fixed (Guid* pIid = &iid)
        fixed (IntPtr* pPpv = &ppv)
        {
            int hresult = this.d2D1ResourceManagerImpl->QueryInterface(pIid, (void**)pPpv);

            GC.KeepAlive(this);

            return hresult switch
            {
                S.S_OK => CustomQueryInterfaceResult.Handled,
                _ => CustomQueryInterfaceResult.Failed
            };
        }
    }

    /// <summary>
    /// Creates a new <c>ID2D1ResourceTextureManager</c> instance.
    /// </summary>
    /// <param name="resourceTextureManager">A pointer to the resulting <c>ID2D1ResourceTextureManager</c> instance.</param>
    /// <remarks>
    /// The resulting <c>ID2D1ResourceTextureManager</c> instance does not require multithread support for the factory that is
    /// assigned to it when initialized from an effect. Callers have the responsibility of ensuring thread safety when using it.
    /// </remarks>
    public static void Create(void** resourceTextureManager)
    {
        D2D1ResourceTextureManagerImpl.Factory((D2D1ResourceTextureManagerImpl**)resourceTextureManager, requiresMultithread: false);
    }

    /// <summary>
    /// Initializes a given <c>ID2D1ResourceTextureManager</c> instance.
    /// </summary>
    /// <param name="resourceTextureManager">A pointer to the <c>ID2D1ResourceTextureManager</c> instance to use.</param>
    /// <param name="resourceId">The resource id for the resource to create.</param>
    /// <param name="extents">The extents of the resource to create.</param>
    /// <param name="bufferPrecision">The buffer precision for the resource to create.</param>
    /// <param name="channelDepth">The channel depth for the resource to create.</param>
    /// <param name="filter">The filter for the resource to create.</param>
    /// <param name="extendModes">The extend modes for the resource to create.</param>
    /// <param name="data">The data to load in the resource to create.</param>
    /// <param name="strides">The strides for the data supplied for the resource to create.</param>
    /// <remarks>
    /// Depending on internal state, <paramref name="resourceTextureManager"/> might immediately create the resource
    /// texture, or it might buffer the data internally and then create the resource texture at a later time.
    /// </remarks>
    public static void Initialize(
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
        // Validate parameters early from here too to avoid AVs in the native method
        if (extents.Length == 0 ||
            extendModes.Length == 0 ||
            extents.Length != extendModes.Length ||
            (data.Length > 0 && strides.Length != extents.Length - 1))
        {
            Marshal.ThrowExceptionForHR(E.E_INVALIDARG);
        }

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

            int hresult = ((ID2D1ResourceTextureManager*)resourceTextureManager)->Initialize(
                resourceId: &resourceId,
                resourceTextureProperties: &d2D1ResourceTextureProperties,
                data: pData,
                strides: pStrides,
                dataSize: (uint)data.Length);

            Marshal.ThrowExceptionForHR(hresult);
        }
    }

    /// <summary>
    /// Updates the data in a given <c>ID2D1ResourceTextureManager</c> instance.
    /// </summary>
    /// <param name="resourceTextureManager">A pointer to the <c>ID2D1ResourceTextureManager</c> instance to use.</param>
    /// <param name="minimumExtents">The "left" extent of the updates if specified (if empty, the entire texture is updated).</param>
    /// <param name="maximimumExtents">The "right" extent of the updates if specified (if empty, the entire texture is updated).</param>
    /// <param name="strides">The stride to advance through the input data.</param>
    /// <param name="data">The data to be placed into the resource texture.</param>
    public static void Update(
        void* resourceTextureManager,
        ReadOnlySpan<uint> minimumExtents,
        ReadOnlySpan<uint> maximimumExtents,
        ReadOnlySpan<uint> strides,
        ReadOnlySpan<byte> data)
    {
        fixed (uint* pMinimumExtents = minimumExtents)
        fixed (uint* pMaximumExtents = maximimumExtents)
        fixed (uint* pStrides = strides)
        fixed (byte* pData = data)
        {
            int dimensions = strides.Length + 1;

            // Same validation as in the Update instance method above
            if (pMinimumExtents is not null &&
                pMaximumExtents is not null &&
                (minimumExtents.Length != dimensions || maximimumExtents.Length != dimensions))
            {
                Marshal.ThrowExceptionForHR(E.E_INVALIDARG);
            }

            int hresult = ((ID2D1ResourceTextureManager*)resourceTextureManager)->Update(
                minimumExtents: pMinimumExtents,
                maximimumExtents: pMaximumExtents,
                strides: pStrides,
                dimensions: (uint)dimensions,
                data: pData,
                dataCount: (uint)data.Length);

            Marshal.ThrowExceptionForHR(hresult);
        }
    }
}