using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Shaders.Interop.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
unsafe partial struct ComputeShaderEffect
{
#if !NET6_0_OR_GREATER
    /// <summary>
    /// A cached <see cref="PixelShaderEffect.PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetConstantBufferImpl"/>.
    /// </summary>
    private static readonly PixelShaderEffect.PropertyGetFunctionDelegate GetConstantBufferWrapper = GetConstantBufferImpl;

    /// <summary>
    /// A cached <see cref="PixelShaderEffect.PropertySetFunctionDelegate"/> instance wrapping <see cref="SetConstantBufferImpl"/>.
    /// </summary>
    private static readonly PixelShaderEffect.PropertySetFunctionDelegate SetConstantBufferWrapper = SetConstantBufferImpl;

    /// <summary>
    /// A cached <see cref="PixelShaderEffect.PropertyGetFunctionDelegate"/> instance wrapping <see cref="GetTransformMapperImpl"/>.
    /// </summary>
    private static readonly PixelShaderEffect.PropertyGetFunctionDelegate GetTransformMapperWrapper = GetTransformMapperImpl;

    /// <summary>
    /// A cached <see cref="PixelShaderEffect.PropertySetFunctionDelegate"/> instance wrapping <see cref="SetTransformMapperImpl"/>.
    /// </summary>
    private static readonly PixelShaderEffect.PropertySetFunctionDelegate SetTransformMapperWrapper = SetTransformMapperImpl;
#endif

    /// <summary>
    /// Gets the get accessor for the constant buffer.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetConstantBuffer
#else
    public static void* GetConstantBuffer
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => &GetConstantBufferImpl;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(GetConstantBufferWrapper);
#endif
    }

    /// <summary>
    /// Gets the set accessor for the constant buffer.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetConstantBuffer
#else
    public static void* SetConstantBuffer
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => &SetConstantBufferImpl;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(SetConstantBufferWrapper);
#endif
    }

    /// <summary>
    /// Gets the get accessor for the transform mapper.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int> GetTransformMapper
#else
    public static void* GetTransformMapper
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => &GetTransformMapperImpl;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(GetTransformMapperWrapper);
#endif
    }

    /// <summary>
    /// Gets the set accessor for the transform mapper.
    /// </summary>
#if NET6_0_OR_GREATER
    public static delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int> SetTransformMapper
#else
    public static void* SetTransformMapper
#endif
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
        get => &SetTransformMapperImpl;
#else
        get => (void*)Marshal.GetFunctionPointerForDelegate(SetTransformMapperWrapper);
#endif
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        ComputeShaderEffect* @this = (ComputeShaderEffect*)effect;

        return D2D1ShaderEffect.GetConstantBuffer(
            data: data,
            dataSize: dataSize,
            actualSize: actualSize,
            constantBufferSize: (uint)@this->constantBufferSize,
            constantBuffer: @this->constantBuffer);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetConstantBufferImpl(IUnknown* effect, byte* data, uint dataSize)
    {
        ComputeShaderEffect* @this = (ComputeShaderEffect*)effect;

        return D2D1ShaderEffect.SetConstantBuffer(
            data: data,
            dataSize: dataSize,
            constantBufferSize: (uint)@this->constantBufferSize,
            constantBuffer: ref @this->constantBuffer);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int GetTransformMapperImpl(IUnknown* effect, byte* data, uint dataSize, uint* actualSize)
    {
        ComputeShaderEffect* @this = (ComputeShaderEffect*)effect;

        return D2D1ShaderEffect.GetTransformMapper(
            data: data,
            dataSize: dataSize,
            actualSize: actualSize,
            d2D1TransformMapper: @this->d2D1TransformMapper);
    }

    /// <inheritdoc cref="D2D1_PROPERTY_BINDING.getFunction"/>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static int SetTransformMapperImpl(IUnknown* effect, byte* data, uint dataSize)
    {
        ComputeShaderEffect* @this = (ComputeShaderEffect*)effect;

        return D2D1ShaderEffect.SetTransformMapper(
            data: data,
            dataSize: dataSize,
            d2D1TransformMapper: ref @this->d2D1TransformMapper);
    }
}