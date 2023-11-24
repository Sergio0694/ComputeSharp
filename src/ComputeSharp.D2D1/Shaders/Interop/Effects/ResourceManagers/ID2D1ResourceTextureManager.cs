using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <summary>
/// The resource texture manager type to use with built-in effects.
/// </summary>
internal unsafe struct ID2D1ResourceTextureManager : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0xE4, 0xC7, 0x4F, 0x3C,
                0x19, 0xA4,
                0xCA, 0x46,
                0xB5, 0xF6,
                0x66,
                0xEB,
                0x4F,
                0xF1,
                0x8D,
                0x64
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <summary>
    /// Creates or finds the given resource texture, depending on whether a resource id is specified.
    /// It also optionally initializes the texture with the specified data.
    /// </summary>
    /// <param name="resourceId">An optional pointer to the unique id that identifies the resource texture.</param>
    /// <param name="resourceTextureProperties">The properties used to create the resource texture.</param>
    /// <param name="data">The optional data to be loaded into the resource texture.</param>
    /// <param name="strides">An optional pointer to the stride to advance through the resource texture, according to dimension.</param>
    /// <param name="dataSize">The size, in bytes, of the data.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT Initialize(
        Guid* resourceId,
        D2D1_RESOURCE_TEXTURE_PROPERTIES* resourceTextureProperties,
        byte* data,
        uint* strides,
        uint dataSize)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1ResourceTextureManager*, Guid*, D2D1_RESOURCE_TEXTURE_PROPERTIES*, byte*, uint*, uint, int>)this.lpVtbl[3])(
            (ID2D1ResourceTextureManager*)Unsafe.AsPointer(ref this),
            resourceId,
            resourceTextureProperties,
            data,
            strides,
            dataSize);
    }

    /// <summary>
    /// Updates the specific resource texture inside the specific range or box using the supplied data.
    /// </summary>
    /// <param name="minimumExtents">The "left" extent of the updates if specified. If <see langword="null"/>, the entire texture is updated.</param>
    /// <param name="maximimumExtents">The "right" extent of the updates if specified. If <see langword="null"/>, the entire texture is updated.</param>
    /// <param name="strides">The stride to advance through the input data, according to dimension.</param>
    /// <param name="dimensions">The number of dimensions in the resource texture. This must match the number used to load the texture.</param>
    /// <param name="data">The data to be placed into the resource texture.</param>
    /// <param name="dataCount">The size of the data buffer to be used to update the resource texture.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT Update(
        uint* minimumExtents,
        uint* maximimumExtents,
        uint* strides,
        uint dimensions,
        byte* data,
        uint dataCount)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1ResourceTextureManager*, uint*, uint*, uint*, uint, byte*, uint, int>)this.lpVtbl[4])(
            (ID2D1ResourceTextureManager*)Unsafe.AsPointer(ref this),
            minimumExtents,
            maximimumExtents,
            strides,
            dimensions,
            data,
            dataCount);
    }
}