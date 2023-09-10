using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;

#pragma warning disable CS0649

namespace ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;

/// <summary>
/// The updater for <see cref="TerraFX.Interop.DirectX.ID2D1RenderInfo"/> types to use with built-in effects.
/// </summary>
[Guid("430C5B40-AE16-485F-90E6-4FA4915144B6")]
internal unsafe struct ID2D1RenderInfoUpdateContext
{
    /// <summary>
    /// Gets the <see cref="System.Guid"/> for <see cref="ID2D1RenderInfoUpdateContext"/> (<c>430C5B40-AE16-485F-90E6-4FA4915144B6</c>).
    /// </summary>
    public static ref readonly Guid Guid
    {
        get
        {
            ReadOnlySpan<byte> data = new byte[] {
                0x40, 0x5B, 0x0C, 0x43,
                0x16, 0xAE,
                0x5F, 0x48,
                0x90, 0xE6,
                0x4F,
                0xA4,
                0x91,
                0x51,
                0x44,
                0xB6
            };

            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    /// <summary>
    /// The vtable for the current instance.
    /// </summary>
    private readonly void** lpVtbl;

    /// <summary>
    /// Retrieves the size of the constant buffer for the current effect.
    /// </summary>
    /// <param name="size">The resulting size of the constant buffer.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT GetConstantBufferSize(uint* size)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1RenderInfoUpdateContext*, uint*, int>)this.lpVtbl[3])(
            (ID2D1RenderInfoUpdateContext*)Unsafe.AsPointer(ref this),
            size);
    }

    /// <summary>
    /// Retrieves the constant buffer data for the current effect.
    /// </summary>
    /// <param name="buffer">A pointer to the buffer to fill with the constant buffer data.</param>
    /// <param name="bufferCount">The size of the buffer provided through <paramref name="buffer"/>.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT GetConstantBuffer(byte* buffer, uint bufferCount)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1RenderInfoUpdateContext*, byte*, uint, int>)this.lpVtbl[4])(
            (ID2D1RenderInfoUpdateContext*)Unsafe.AsPointer(ref this),
            buffer,
            bufferCount);
    }

    /// <summary>
    /// Sets the constant buffer data for the current effect.
    /// </summary>
    /// <param name="buffer">A pointer to the constant buffer data to use to update the effect.</param>
    /// <param name="bufferCount">The size of the buffer provided through <paramref name="buffer"/>.</param>
    /// <returns>An <see cref="HRESULT"/> for the operation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HRESULT SetConstantBuffer(byte* buffer, uint bufferCount)
    {
        return ((delegate* unmanaged[Stdcall]<ID2D1RenderInfoUpdateContext*, byte*, uint, int>)this.lpVtbl[5])(
            (ID2D1RenderInfoUpdateContext*)Unsafe.AsPointer(ref this),
            buffer,
            bufferCount);
    }
}