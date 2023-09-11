using System;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.__Internals;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Loaders;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Interop.Helpers;

/// <summary>
/// Provides shared methods to interop with D2D1 APIs and manage shader effects of all kinds.
/// </summary>
internal static unsafe class D2D1ShaderEffectMarshaller
{
    /// <summary>
    /// Creates an effect with a specified id.
    /// </summary>
    /// <param name="d2D1DeviceContext">A pointer to the <c>ID2D1DeviceContext</c> instance to use.</param>
    /// <param name="d2D1Effect">A pointer to the resulting <c>ID2D1Effect*</c> pointer to produce.</param>
    /// <param name="effectId">The id of the effect to create.</param>
    public static void CreateFromD2D1DeviceContext(void* d2D1DeviceContext, void** d2D1Effect, in Guid effectId)
    {
        fixed (Guid* pGuid = &effectId)
        {
            ((ID2D1DeviceContext*)d2D1DeviceContext)->CreateEffect(
                effectId: pGuid,
                effect: (ID2D1Effect**)d2D1Effect).Assert();
        }
    }

    /// <summary>
    /// Sets the constant buffer from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 shader to set the constant buffer for.</typeparam>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="shader">The input D2D1 shader to set the contant buffer for.</param>
    public static void SetConstantBufferForD2D1Effect<T>(void* d2D1Effect, in T shader)
        where T : unmanaged, ID2D1Shader
    {
        D2D1EffectDispatchDataLoader dataLoader = new((ID2D1Effect*)d2D1Effect);

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);
    }

    /// <summary>
    /// Sets an <see cref="IUnknown"/> object from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="propertyIndex">The index of the property to set.</param>
    /// <param name="propertyValueUnknown">The <see cref="IUnknown"/> object to set as value.</param>
    public static void SetUnknownForD2D1Effect(void* d2D1Effect, uint propertyIndex, void* propertyValueUnknown)
    {
        ((ID2D1Effect*)d2D1Effect)->SetValue(
            index: propertyIndex,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)&propertyValueUnknown,
            dataSize: (uint)sizeof(void*)).Assert();
    }

    /// <summary>
    /// Sets the resource texture manager from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="propertyIndex">The index of the property to set.</param>
    /// <param name="resourceTextureManager">The input <see cref="D2D1ResourceTextureManager"/> instance.</param>
    public static void SetResourceTextureManagerForD2D1Effect(void* d2D1Effect, uint propertyIndex, D2D1ResourceTextureManager resourceTextureManager)
    {
        using ComPtr<ID2D1ResourceTextureManager> resourceTextureManager2 = default;

        resourceTextureManager.GetD2D1ResourceTextureManager(resourceTextureManager2.GetAddressOf());

        ((ID2D1Effect*)d2D1Effect)->SetValue(
            index: propertyIndex,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)resourceTextureManager2.GetAddressOf(),
            dataSize: (uint)sizeof(void*)).Assert();
    }
}