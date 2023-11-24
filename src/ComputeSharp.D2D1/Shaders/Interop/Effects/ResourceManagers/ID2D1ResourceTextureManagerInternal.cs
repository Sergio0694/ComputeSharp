using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Win32;

#pragma warning disable CS0649, IDE0055

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

/// <summary>
/// The internal resource texture manager type to use with built-in effects.
/// </summary>
internal unsafe struct ID2D1ResourceTextureManagerInternal : IComObject
{
    /// <inheritdoc/>
    static Guid* IComObject.IID
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ReadOnlySpan<byte> data =
            [
                0x24, 0x10, 0xBB, 0x5C,
                0xA1, 0x8E,
                0x89, 0x46,
                0x81, 0xBF,
                0x8A,
                0xD1,
                0x90,
                0xB5,
                0xEF,
                0x5D
            ];

            return (Guid*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <summary>
    /// Sets the <see cref="ID2D1EffectContext"/> for the current <see cref="ID2D1ResourceTextureManagerInternal"/> instance.
    /// </summary>
    /// <param name="effectContext">The input <see cref="ID2D1EffectContext"/> for the manager.</param>
    /// <param name="dimensions">A pointer to an optional amount of dimensions for the target resource texture, for validation.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT Initialize(ID2D1EffectContext* effectContext, uint* dimensions)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1ResourceTextureManagerInternal*, ID2D1EffectContext*, uint*, int>)this.lpVtbl[3])(
            (ID2D1ResourceTextureManagerInternal*)Unsafe.AsPointer(ref this),
            effectContext,
            dimensions);
    }

    /// <summary>
    /// Gets the <see cref="ID2D1ResourceTexture"/> instance held by the manager.
    /// </summary>
    /// <param name="resourceTexture">The resulting <see cref="ID2D1ResourceTexture"/> instance.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT GetResourceTexture(ID2D1ResourceTexture** resourceTexture)
    {
        return ((delegate* unmanaged[MemberFunction]<ID2D1ResourceTextureManagerInternal*, ID2D1ResourceTexture**, int>)this.lpVtbl[4])(
            (ID2D1ResourceTextureManagerInternal*)Unsafe.AsPointer(ref this),
            resourceTexture);
    }
}