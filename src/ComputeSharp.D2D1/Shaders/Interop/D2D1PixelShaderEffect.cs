using System;
using System.Globalization;
using System.Runtime.CompilerServices;
#if !NET6_0_OR_GREATER
using System.Runtime.InteropServices;
#endif
using System.Text;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Interop.Effects;
using ComputeSharp.D2D1.Shaders.Dispatching;
using ComputeSharp.D2D1.Shaders.Interop.Buffers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

#pragma warning disable CS0618

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// An implementation of a D2D1 pixel shader effect that can be used to instantiate <c>ID2D1Effect</c> objects.
/// </summary>
public static unsafe class D2D1PixelShaderEffect
{
    /// <summary>
    /// Registers an effect from an input D2D1 pixel shader, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static unsafe void RegisterForD2D1Factory1<T>(void* d2D1Factory1, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
    {
        RegisterForD2D1Factory1<T>(d2D1Factory1, null, out effectId);
    }

    /// <summary>
    /// Registers an effect from an input D2D1 pixel shader, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <typeparam name="TMapper">The type of <see cref="ID2D1TransformMapper{T}"/> implementation to register.</typeparam>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static unsafe void RegisterForD2D1Factory1<T, TMapper>(void* d2D1Factory1, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
        where TMapper : class, ID2D1TransformMapper<T>, new()
    {
        RegisterForD2D1Factory1(d2D1Factory1, static () => new TMapper(), out effectId);
    }

    /// <summary>
    /// Registers an effect from an input D2D1 pixel shader, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <param name="mapperFactory">An optional factory of <see cref="ID2D1TransformMapper{T}"/> instances to use for the transform.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static unsafe void RegisterForD2D1Factory1<T>(void* d2D1Factory1, Func<ID2D1TransformMapper<T>>? mapperFactory, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
    {
        effectId = default;

        PixelShaderEffect.For<T>.Initialize(mapperFactory);

        // Setup the input string
        StringBuilder effectInputsBuilder = new();

        for (int i = 0; i < PixelShaderEffect.For<T>.NumberOfInputs; i++)
        {
            effectInputsBuilder.Append("<Input name='Source");
            effectInputsBuilder.Append(i);
            effectInputsBuilder.Append("'/>");
        }

        // Prepare the XML with a variable number of inputs
        string xml = @$"<?xml version='1.0'?>
<Effect>
    <Property name='DisplayName' type='string' value='{typeof(T).FullName}'/>
    <Property name='Author' type='string' value='ComputeSharp.D2D1'/>
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
#if NET6_0_OR_GREATER
                (delegate* unmanaged<IUnknown*, byte*, uint, uint*, int>)
                &PixelShaderEffect.GetConstantBuffer;
#else
                (void*)Marshal.GetFunctionPointerForDelegate(PixelShaderEffect.GetConstantBufferWrapper);
#endif
            d2D1PropertyBinding.setFunction =
                (delegate* unmanaged[Stdcall]<IUnknown*, byte*, uint, HRESULT>)
#if NET6_0_OR_GREATER
                (delegate* unmanaged<IUnknown*, byte*, uint, int>)
                &PixelShaderEffect.SetConstantBuffer;
#else
                (void*)Marshal.GetFunctionPointerForDelegate(PixelShaderEffect.SetConstantBufferWrapper);
#endif

            fixed (Guid* pGuid = &PixelShaderEffect.For<T>.Id)
            {
                // Register the effect
                ((ID2D1Factory1*)d2D1Factory1)->RegisterEffectFromString(
                    classId: pGuid,
                    propertyXml: (ushort*)pXml,
                    bindings: &d2D1PropertyBinding,
                    bindingsCount: 1,
                    effectFactory: PixelShaderEffect.For<T>.Factory).Assert();
            }

            effectId = PixelShaderEffect.For<T>.Id;
        }
    }

    /// <summary>
    /// Gets a binary blob containing serialized information that can be used to register an effect, by using <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <returns>A blob containing serialized information that can be used to register an effect.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info and for details on the binary format, see <see cref="GetRegistrationBlob{T}(Func{ID2D1TransformMapper{T}}?, out Guid)"/>.</remarks>
    public static unsafe ReadOnlyMemory<byte> GetRegistrationBlob<T>(out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
    {
        return GetRegistrationBlob<T>(null, out effectId);
    }

    /// <summary>
    /// Gets a binary blob containing serialized information that can be used to register an effect, by using <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <typeparam name="TMapper">The type of <see cref="ID2D1TransformMapper{T}"/> implementation to register.</typeparam>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <returns>A blob containing serialized information that can be used to register an effect.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info and for details on the binary format, see <see cref="GetRegistrationBlob{T}(Func{ID2D1TransformMapper{T}}?, out Guid)"/>.</remarks>
    public static unsafe ReadOnlyMemory<byte> GetRegistrationBlob<T, TMapper>(out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
        where TMapper : class, ID2D1TransformMapper<T>, new()
    {
        return GetRegistrationBlob(static () => new TMapper(), out effectId);
    }

    /// <summary>
    /// Gets a binary blob containing serialized information that can be used to register an effect, by using <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the registration blob for.</typeparam>
    /// <param name="mapperFactory">An optional factory of <see cref="ID2D1TransformMapper{T}"/> instances to use for the transform.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <returns>A blob containing serialized information that can be used to register an effect.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>
    /// <para>
    /// This blob can be useful to marshal registration data across an app domain boundary without having assembly references cross this boundary as well.
    /// </para>
    /// <para>
    /// For instance, an application could allow plugins to create effects through the <c>ComputeSharp.D2D1</c> APIs, and to then create an effect registration
    /// blob to pass to the main application, which could then use it to register the effect. This way, even if a plugin was loaded with a separate assembly load
    /// context and using a different (potentially incompatible) version of <c>ComputeSharp.D2D1</c>, the two could still work side by side with no issues.
    /// </para>
    /// <para>
    /// The binary blob contains information with the following format:
    /// <list type="bullet">
    ///   <item>The blob version id (a <see cref="Guid"/>).</item>
    ///   <item>The effect id (a <see cref="Guid"/>).</item>
    ///   <item>The number of inputs for the effect (an <see cref="int"/>).</item>
    ///   <item>The effect XML description (as null-terminated UTF8 text).</item>
    ///   <item>The number of property bindings (an <see cref="int"/>).</item>
    ///     <item>The property name (as null-terminated UTF8 text).</item>
    ///     <item>The property getter (a <see langword="delegate* unmanaged[Stdcall]&lt;IUnknown*, byte*, uint, uint*, HRESULT&gt;"/>).</item>
    ///     <item>The property setter (a <see langword="delegate* unmanaged[Stdcall]&lt;IUnknown*, byte*, uint, HRESULT&gt;"/>).</item>
    ///   <item>The effect factory (a <see langword="delegate* unmanaged[Stdcall]&lt;IUnknown**, HRESULT&gt;"/>).</item>
    /// </list>
    /// The property name, getter and setter are grouped together after the number of bindings.
    /// </para>
    /// <para>
    /// To make the deserialization easier, the <see cref="D2D1EffectRegistrationData"/> type can be used to read and validate the returned blob.
    /// The leading blob id will determine what subtype should be used to deserialize the blob (eg. <see cref="D2D1EffectRegistrationData.V1"/>).
    /// </para>
    /// <para>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.
    /// </para>
    /// </remarks>
    public static unsafe ReadOnlyMemory<byte> GetRegistrationBlob<T>(Func<ID2D1TransformMapper<T>>? mapperFactory, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
    {
        effectId = default;

        PixelShaderEffect.For<T>.Initialize(mapperFactory);

        using ArrayPoolBinaryWriter writer = new(ArrayPoolBinaryWriter.DefaultInitialBufferSize);

        // Blob id
        writer.Write(D2D1EffectRegistrationData.V1.BlobId);

        // Effect id and number of inputs
        writer.Write(PixelShaderEffect.For<T>.Id);
        writer.Write(PixelShaderEffect.For<T>.NumberOfInputs);
        
        // Build the XML text
        writer.WriteAsUtf8(@"<?xml version='1.0'?>
<Effect>
    <Property name='DisplayName' type='string' value='");
        writer.WriteAsUtf8(typeof(T).FullName!);
        writer.WriteAsUtf8(@"'/>
    <Property name='Author' type='string' value='ComputeSharp.D2D1'/>
    <Property name='Category' type='string' value='Stylize'/>
    <Property name='Description' type='string' value='A custom D2D1 effect using a pixel shader'/>
    <Inputs>
        ");

        // Add the input nodes
        for (int i = 0; i < PixelShaderEffect.For<T>.NumberOfInputs; i++)
        {
            writer.WriteAsUtf8("<Input name='Source");
            writer.WriteAsUtf8(i.ToString(CultureInfo.InvariantCulture));
            writer.WriteAsUtf8($"'/>");
        }

        // Write the last part of the XML (including the buffer property)
        writer.WriteAsUtf8(@"
    </Inputs>
    <Property name='Buffer' type='blob'>
        <Property name='DisplayName' type='string' value='Buffer'/>
    </Property>
</Effect>");

        // Null terminator for the text
        writer.Write((byte)'\0');

        // Property accessors
#if NET6_0_OR_GREATER
        nint getBufferPointer = (nint)(delegate* unmanaged<IUnknown*, byte*, uint, uint*, int>)&PixelShaderEffect.GetConstantBuffer;
        nint setBufferPointer = (nint)(delegate* unmanaged<IUnknown*, byte*, uint, int>)&PixelShaderEffect.SetConstantBuffer;
#else
        nint getBufferPointer = Marshal.GetFunctionPointerForDelegate(PixelShaderEffect.GetConstantBufferWrapper);
        nint setBufferPointer = Marshal.GetFunctionPointerForDelegate(PixelShaderEffect.SetConstantBufferWrapper);
#endif

        // Bindings
        writer.Write(1);
        writer.WriteAsUtf8("Buffer");
        writer.Write((byte)'\0');
        writer.Write(getBufferPointer);
        writer.Write(setBufferPointer);

        // Effect factory
        writer.Write((nint)PixelShaderEffect.For<T>.Factory);

        effectId = PixelShaderEffect.For<T>.Id;

        return writer.WrittenSpan.ToArray();
    }

    /// <summary>
    /// Creates an effect wrapping an input D2D1 pixel shader, by calling <c>ID2D1DeviceContext::CreateEffect</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to create an effect for.</typeparam>
    /// <param name="d2D1DeviceContext">A pointer to the <c>ID2D1DeviceContext</c> instance to use.</param>
    /// <param name="d2D1Effect">A pointer to the resulting <c>ID2D1Effect*</c> pointer to produce.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown if this method is called before initializing a shader effect for the shader type <typeparamref name="T"/>.
    /// To do so, make sure to call either <see cref="RegisterForD2D1Factory1{T}(void*, out Guid)"/> (or an overload), or
    /// to use <see cref="GetRegistrationBlob{T}(out Guid)"/> (or an overload), and use that blob to register an effect.
    /// </exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1devicecontext-createeffect"/>.</remarks>
    public static unsafe void CreateFromD2D1DeviceContext<T>(void* d2D1DeviceContext, void** d2D1Effect)
        where T : unmanaged, ID2D1PixelShader
    {
        if (!PixelShaderEffect.For<T>.TryGetId(out Guid id))
        {
            ThrowHelper.ThrowInvalidOperationException("The effect for the input shader type has not been initialized yet.");
        }

        ((ID2D1DeviceContext*)d2D1DeviceContext)->CreateEffect(
            effectId: &id,
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
        D2D1EffectDispatchDataLoader dataLoader = new((ID2D1Effect*)d2D1Effect);

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);
    }
}
