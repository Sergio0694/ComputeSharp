using System;
using ComputeSharp.D2D1.Interop.Effects;
using ComputeSharp.D2D1.Interop.Helpers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// An implementation of a D2D1 compute shader effect that can be used to instantiate <c>ID2D1Effect</c> objects.
/// </summary>
public static unsafe class D2D1ComputeShaderEffect
{
    /// <summary>
    /// Creates an effect wrapping an input D2D1 compute shader, by calling <c>ID2D1DeviceContext::CreateEffect</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 compute shader to create an effect for.</typeparam>
    /// <param name="d2D1DeviceContext">A pointer to the <c>ID2D1DeviceContext</c> instance to use.</param>
    /// <param name="d2D1Effect">A pointer to the resulting <c>ID2D1Effect*</c> pointer to produce.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1DeviceContext"/> or <paramref name="d2D1Effect"/> are <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if this method is called before initializing a shader effect for the shader type <typeparamref name="T"/>.
    /// </exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1devicecontext-createeffect"/>.</remarks>
    public static void CreateFromD2D1DeviceContext<T>(void* d2D1DeviceContext, void** d2D1Effect)
        where T : unmanaged, ID2D1ComputeShader
    {
        default(ArgumentNullException).ThrowIfNull(d2D1DeviceContext);
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);

        D2D1ShaderEffectMarshaller.CreateFromD2D1DeviceContext(d2D1DeviceContext, d2D1Effect, in ComputeShaderEffect.For<T>.Instance.Id);
    }

    /// <summary>
    /// Sets the constant buffer from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 compute shader to set the constant buffer for.</typeparam>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="shader">The input D2D1 compute shader to set the contant buffer for.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> is <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetConstantBufferForD2D1Effect<T>(void* d2D1Effect, in T shader)
        where T : unmanaged, ID2D1ComputeShader
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);

        D2D1ShaderEffectMarshaller.SetConstantBufferForD2D1Effect(d2D1Effect, in shader);
    }

    /// <summary>
    /// Sets the resource texture manager from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="resourceTextureManager">The input <c>ID2D1ResourceTextureManager</c> object (see <see cref="D2D1ResourceTextureManager"/>).</param>
    /// <param name="resourceTextureIndex">The index of the resource texture to assign the resource texture manager to.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> or <paramref name="resourceTextureManager"/> are <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetResourceTextureManagerForD2D1Effect(void* d2D1Effect, void* resourceTextureManager, int resourceTextureIndex)
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);
        default(ArgumentNullException).ThrowIfNull(resourceTextureManager);

        D2D1ShaderEffectMarshaller.SetUnknownForD2D1Effect(
            d2D1Effect: d2D1Effect,
            propertyIndex: D2D1ComputeShaderEffectProperty.ResourceTextureManager0 + (uint)resourceTextureIndex,
            propertyValueUnknown: resourceTextureManager);
    }

    /// <summary>
    /// Sets the resource texture manager from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="resourceTextureManager">The input <see cref="D2D1ResourceTextureManager"/> instance.</param>
    /// <param name="resourceTextureIndex">The index of the resource texture to assign the resource texture manager to.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> or <paramref name="resourceTextureManager"/> are <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetResourceTextureManagerForD2D1Effect(void* d2D1Effect, D2D1ResourceTextureManager resourceTextureManager, int resourceTextureIndex)
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);
        default(ArgumentNullException).ThrowIfNull(resourceTextureManager);

        D2D1ShaderEffectMarshaller.SetResourceTextureManagerForD2D1Effect(
            d2D1Effect: d2D1Effect,
            propertyIndex: D2D1ComputeShaderEffectProperty.ResourceTextureManager0 + (uint)resourceTextureIndex,
            resourceTextureManager: resourceTextureManager);
    }

    /// <summary>
    /// Sets the transform mapper for an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="transformMapper">The input <c>ID2D1TransformMapper</c> object (see <see cref="D2D1ComputeTransformMapper{T}"/>).</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> or <paramref name="transformMapper"/> are <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetTransformMapperForD2D1Effect(void* d2D1Effect, void* transformMapper)
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);
        default(ArgumentNullException).ThrowIfNull(transformMapper);

        D2D1ShaderEffectMarshaller.SetUnknownForD2D1Effect(
            d2D1Effect: d2D1Effect,
            propertyIndex: D2D1ComputeShaderEffectProperty.TransformMapper,
            propertyValueUnknown: transformMapper);
    }

    /// <summary>
    /// Sets the transform mapper for an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="transformMapper">The input <c>ID2D1TransformMapper</c> object.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> or <paramref name="transformMapper"/> are <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetTransformMapperForD2D1Effect<T>(void* d2D1Effect, D2D1ComputeTransformMapper<T> transformMapper)
        where T : unmanaged, ID2D1ComputeShader
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);
        default(ArgumentNullException).ThrowIfNull(transformMapper);

        using ComPtr<ID2D1TransformMapper> transformMapper2 = default;

        transformMapper.GetD2D1TransformMapper(transformMapper2.GetAddressOf());

        D2D1ShaderEffectMarshaller.SetUnknownForD2D1Effect(
            d2D1Effect: d2D1Effect,
            propertyIndex: D2D1ComputeShaderEffectProperty.TransformMapper,
            propertyValueUnknown: transformMapper2.Get());
    }
}