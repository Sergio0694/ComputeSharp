using System;
using System.Text;
using ComputeSharp.D2D1Interop.Extensions;
using ComputeSharp.D2D1Interop.Tests.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1Interop;

/// <inheritdoc/>
unsafe partial class D2D1InteropServices
{
    /// <summary>
    /// Registers an effect from an input D2D1 pixel shader, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static unsafe void RegisterPixelShaderEffectForD2D1Factory1<T>(void* d2D1Factory1, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
    {
        effectId = default;

        // Setup the input string
        StringBuilder effectInputsBuilder = new();

        for (int i = 0; i < PixelShaderEffect.For<T>.NumberOfInputs; i++)
        {
            effectInputsBuilder.Append($"<Input name='Source");
            effectInputsBuilder.Append(i);
            effectInputsBuilder.Append($"'/>");
        }

        // Prepare the XML with a variable number of inputs
        string xml = @$"<?xml version='1.0'?>
<Effect>
    <!-- System Properties -->
    <Property name='DisplayName' type='string' value='{typeof(T).FullName}'/>
    <Property name='Author' type='string' value='ComputeSharp.D2D1Interop'/>
    <Property name='Category' type='string' value='Stylize'/>
    <Property name='Description' type='string' value='A custom D2D1 effect using a pixel shader'/>
    <Inputs>
        {effectInputsBuilder}
    </Inputs>
    <Property name='Buffer' type='blob'>
        <Property name='DisplayName' type='string' value='Buffer'/>
    </Property>
</Effect>";

        fixed (char* pXml = xml)
        fixed (char* pPropertyName = "Buffer")
        {
            // Prepare the effect binding functions
            D2D1_PROPERTY_BINDING d2D1PropertyBinding;
            d2D1PropertyBinding.propertyName = (ushort*)pPropertyName;
            d2D1PropertyBinding.getFunction =
                (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, HRESULT>)
                (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, uint*, int>)
                &PixelShaderEffect.GetConstantBuffer;
            d2D1PropertyBinding.setFunction =
                (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, HRESULT>)
                (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, int>)
                &PixelShaderEffect.SetConstantBuffer;

            Guid shaderId = typeof(T).GUID;

            // Register the effect
            ((ID2D1Factory1*)d2D1Factory1)->RegisterEffectFromString(
                classId: &shaderId,
                propertyXml: (ushort*)pXml,
                bindings: &d2D1PropertyBinding,
                bindingsCount: 1,
                effectFactory: PixelShaderEffect.For<T>.Factory).Assert();

            effectId = shaderId;
        }
    }

    /// <summary>
    /// Creates an effect wrapping an input D2D1 pixel shader, by calling <c>ID2D1DeviceContext::CreateEffect</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to create an effect for.</typeparam>
    /// <param name="d2D1DeviceContext">A pointer to the <c>ID2D1DeviceContext</c> instance to use.</param>
    /// <param name="d2D1Effect">A pointer to the resulting <c>ID2D1Effect*</c> pointer to produce.</param>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1devicecontext-createeffect"/>.</remarks>
    public static unsafe void CreatePixelShaderEffectFromD2D1DeviceContext<T>(void* d2D1DeviceContext, void** d2D1Effect)
        where T : unmanaged, ID2D1PixelShader
    {
        Guid shaderId = typeof(T).GUID;

        ((ID2D1DeviceContext*)d2D1DeviceContext)->CreateEffect(
            effectId: &shaderId,
            effect: (ID2D1Effect**)d2D1Effect).Assert();
    }

    /// <summary>
    /// Sets the constant buffer from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to set the constant buffer for.</typeparam>
    /// <param name="shader">The input D2D1 pixel shader to set the contant buffer for.</param>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static unsafe void SetConstantBufferForD2D1Effect<T>(in T shader, void* d2D1Effect)
        where T : unmanaged, ID2D1PixelShader
    {
        // Get the shader state
        ReadOnlyMemory<byte> buffer = D2D1InteropServices.GetPixelShaderConstantBufferForD2D1DrawInfo(in shader);

        if (buffer.Length > 0)
        {
            fixed (byte* p = buffer.Span)
            {
                // Load the effect buffer
                ((ID2D1Effect*)d2D1Effect)->SetValue(
                    index: 0,
                    type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BLOB,
                    data: p,
                    dataSize: (uint)buffer.Length).Assert();
            }
        }

        // TODO: optimize with a custom dispatch data loader
    }
}
