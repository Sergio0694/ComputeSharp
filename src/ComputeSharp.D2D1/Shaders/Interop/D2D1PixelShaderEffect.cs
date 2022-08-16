using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Helpers;
using ComputeSharp.D2D1.Interop.Effects;
using ComputeSharp.D2D1.Shaders.Interop.Buffers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Loaders;
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
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Factory1"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static void RegisterForD2D1Factory1<T>(void* d2D1Factory1, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
    {
        if (d2D1Factory1 is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(d2D1Factory1), "The input ID2D1Factory1 object cannot be null.");
        }

        RegisterForD2D1Factory1<T>(d2D1Factory1, null, out effectId);
    }

    /// <summary>
    /// Registers an effect from an input D2D1 pixel shader, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <typeparam name="TMapper">The type of <see cref="ID2D1TransformMapper{T}"/> implementation to register.</typeparam>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Factory1"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static void RegisterForD2D1Factory1<T, TMapper>(void* d2D1Factory1, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
        where TMapper : class, ID2D1TransformMapper<T>, new()
    {
        if (d2D1Factory1 is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(d2D1Factory1), "The input ID2D1Factory1 object cannot be null.");
        }

        RegisterForD2D1Factory1(d2D1Factory1, static () => new TMapper(), out effectId);
    }

    /// <summary>
    /// Registers an effect from an input D2D1 pixel shader, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <param name="mapperFactory">An optional factory of <see cref="ID2D1TransformMapper{T}"/> instances to use for the transform.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Factory1"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static void RegisterForD2D1Factory1<T>(void* d2D1Factory1, Func<ID2D1TransformMapper<T>>? mapperFactory, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
    {
        if (d2D1Factory1 is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(d2D1Factory1), "The input ID2D1Factory1 object cannot be null.");
        }

        effectId = default;

        PixelShaderEffect.For<T>.Initialize(mapperFactory);

        // Setup the input string
        StringBuilder effectInputsBuilder = new();

        for (int i = 0; i < PixelShaderEffect.For<T>.InputCount; i++)
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
    <Property name='ConstantBuffer' type='blob'>
        <Property name='DisplayName' type='string' value='ConstantBuffer'/>
    </Property>
    <Property name='ResourceTextureManager0' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager0'/>
    </Property>
    <Property name='ResourceTextureManager1' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager1'/>
    </Property>
    <Property name='ResourceTextureManager2' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager2'/>
    </Property>
    <Property name='ResourceTextureManager3' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager3'/>
    </Property>
    <Property name='ResourceTextureManager4' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager4'/>
    </Property>
    <Property name='ResourceTextureManager5' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager5'/>
    </Property>
    <Property name='ResourceTextureManager6' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager6'/>
    </Property>
    <Property name='ResourceTextureManager7' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager7'/>
    </Property>
    <Property name='ResourceTextureManager8' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager8'/>
    </Property>
    <Property name='ResourceTextureManager9' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager9'/>
    </Property>
    <Property name='ResourceTextureManager10' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager10'/>
    </Property>
    <Property name='ResourceTextureManager11' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager11'/>
    </Property>
    <Property name='ResourceTextureManager12' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager12'/>
    </Property>
    <Property name='ResourceTextureManager13' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager13'/>
    </Property>
    <Property name='ResourceTextureManager14' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager14'/>
    </Property>
    <Property name='ResourceTextureManager15' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager15'/>
    </Property>
</Effect>";

        fixed (char* pXml = xml)
        fixed (char* pBufferPropertyName = "ConstantBuffer")
        fixed (char* pResourceTextureManager0PropertyName = "ResourceTextureManager0")
        fixed (char* pResourceTextureManager1PropertyName = "ResourceTextureManager1")
        fixed (char* pResourceTextureManager2PropertyName = "ResourceTextureManager2")
        fixed (char* pResourceTextureManager3PropertyName = "ResourceTextureManager3")
        fixed (char* pResourceTextureManager4PropertyName = "ResourceTextureManager4")
        fixed (char* pResourceTextureManager5PropertyName = "ResourceTextureManager5")
        fixed (char* pResourceTextureManager6PropertyName = "ResourceTextureManager6")
        fixed (char* pResourceTextureManager7PropertyName = "ResourceTextureManager7")
        fixed (char* pResourceTextureManager8PropertyName = "ResourceTextureManager8")
        fixed (char* pResourceTextureManager9PropertyName = "ResourceTextureManager9")
        fixed (char* pResourceTextureManager10PropertyName = "ResourceTextureManager10")
        fixed (char* pResourceTextureManager11PropertyName = "ResourceTextureManager11")
        fixed (char* pResourceTextureManager12PropertyName = "ResourceTextureManager12")
        fixed (char* pResourceTextureManager13PropertyName = "ResourceTextureManager13")
        fixed (char* pResourceTextureManager14PropertyName = "ResourceTextureManager14")
        fixed (char* pResourceTextureManager15PropertyName = "ResourceTextureManager15")
        {
            // Prepare the effect binding functions
            D2D1_PROPERTY_BINDING* d2D1PropertyBinding = stackalloc D2D1_PROPERTY_BINDING[17];

            // Property names
            d2D1PropertyBinding[0].propertyName = (ushort*)pBufferPropertyName;
            d2D1PropertyBinding[1].propertyName = (ushort*)pResourceTextureManager0PropertyName;
            d2D1PropertyBinding[2].propertyName = (ushort*)pResourceTextureManager1PropertyName;
            d2D1PropertyBinding[3].propertyName = (ushort*)pResourceTextureManager2PropertyName;
            d2D1PropertyBinding[4].propertyName = (ushort*)pResourceTextureManager3PropertyName;
            d2D1PropertyBinding[5].propertyName = (ushort*)pResourceTextureManager4PropertyName;
            d2D1PropertyBinding[6].propertyName = (ushort*)pResourceTextureManager5PropertyName;
            d2D1PropertyBinding[7].propertyName = (ushort*)pResourceTextureManager6PropertyName;
            d2D1PropertyBinding[8].propertyName = (ushort*)pResourceTextureManager7PropertyName;
            d2D1PropertyBinding[9].propertyName = (ushort*)pResourceTextureManager8PropertyName;
            d2D1PropertyBinding[10].propertyName = (ushort*)pResourceTextureManager9PropertyName;
            d2D1PropertyBinding[11].propertyName = (ushort*)pResourceTextureManager10PropertyName;
            d2D1PropertyBinding[12].propertyName = (ushort*)pResourceTextureManager11PropertyName;
            d2D1PropertyBinding[13].propertyName = (ushort*)pResourceTextureManager12PropertyName;
            d2D1PropertyBinding[14].propertyName = (ushort*)pResourceTextureManager13PropertyName;
            d2D1PropertyBinding[15].propertyName = (ushort*)pResourceTextureManager14PropertyName;
            d2D1PropertyBinding[16].propertyName = (ushort*)pResourceTextureManager15PropertyName;

            // Property getters
            d2D1PropertyBinding[0].getFunction = PixelShaderEffect.GetConstantBuffer;
            d2D1PropertyBinding[1].getFunction = PixelShaderEffect.GetResourceTextureManager0;
            d2D1PropertyBinding[2].getFunction = PixelShaderEffect.GetResourceTextureManager1;
            d2D1PropertyBinding[3].getFunction = PixelShaderEffect.GetResourceTextureManager2;
            d2D1PropertyBinding[4].getFunction = PixelShaderEffect.GetResourceTextureManager3;
            d2D1PropertyBinding[5].getFunction = PixelShaderEffect.GetResourceTextureManager4;
            d2D1PropertyBinding[6].getFunction = PixelShaderEffect.GetResourceTextureManager5;
            d2D1PropertyBinding[7].getFunction = PixelShaderEffect.GetResourceTextureManager6;
            d2D1PropertyBinding[8].getFunction = PixelShaderEffect.GetResourceTextureManager7;
            d2D1PropertyBinding[9].getFunction = PixelShaderEffect.GetResourceTextureManager8;
            d2D1PropertyBinding[10].getFunction = PixelShaderEffect.GetResourceTextureManager9;
            d2D1PropertyBinding[11].getFunction = PixelShaderEffect.GetResourceTextureManager10;
            d2D1PropertyBinding[12].getFunction = PixelShaderEffect.GetResourceTextureManager11;
            d2D1PropertyBinding[13].getFunction = PixelShaderEffect.GetResourceTextureManager12;
            d2D1PropertyBinding[14].getFunction = PixelShaderEffect.GetResourceTextureManager13;
            d2D1PropertyBinding[15].getFunction = PixelShaderEffect.GetResourceTextureManager14;
            d2D1PropertyBinding[16].getFunction = PixelShaderEffect.GetResourceTextureManager15;

            // Property setters
            d2D1PropertyBinding[0].setFunction = PixelShaderEffect.SetConstantBuffer;
            d2D1PropertyBinding[1].setFunction = PixelShaderEffect.SetResourceTextureManager0;
            d2D1PropertyBinding[2].setFunction = PixelShaderEffect.SetResourceTextureManager1;
            d2D1PropertyBinding[3].setFunction = PixelShaderEffect.SetResourceTextureManager2;
            d2D1PropertyBinding[4].setFunction = PixelShaderEffect.SetResourceTextureManager3;
            d2D1PropertyBinding[5].setFunction = PixelShaderEffect.SetResourceTextureManager4;
            d2D1PropertyBinding[6].setFunction = PixelShaderEffect.SetResourceTextureManager5;
            d2D1PropertyBinding[7].setFunction = PixelShaderEffect.SetResourceTextureManager6;
            d2D1PropertyBinding[8].setFunction = PixelShaderEffect.SetResourceTextureManager7;
            d2D1PropertyBinding[9].setFunction = PixelShaderEffect.SetResourceTextureManager8;
            d2D1PropertyBinding[10].setFunction = PixelShaderEffect.SetResourceTextureManager9;
            d2D1PropertyBinding[11].setFunction = PixelShaderEffect.SetResourceTextureManager10;
            d2D1PropertyBinding[12].setFunction = PixelShaderEffect.SetResourceTextureManager11;
            d2D1PropertyBinding[13].setFunction = PixelShaderEffect.SetResourceTextureManager12;
            d2D1PropertyBinding[14].setFunction = PixelShaderEffect.SetResourceTextureManager13;
            d2D1PropertyBinding[15].setFunction = PixelShaderEffect.SetResourceTextureManager14;
            d2D1PropertyBinding[16].setFunction = PixelShaderEffect.SetResourceTextureManager15;

            fixed (Guid* pGuid = &PixelShaderEffect.For<T>.Id)
            {
                // Register the effect
                ((ID2D1Factory1*)d2D1Factory1)->RegisterEffectFromString(
                    classId: pGuid,
                    propertyXml: (ushort*)pXml,
                    bindings: d2D1PropertyBinding,
                    bindingsCount: 17,
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
    public static ReadOnlyMemory<byte> GetRegistrationBlob<T>(out Guid effectId)
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
    public static ReadOnlyMemory<byte> GetRegistrationBlob<T, TMapper>(out Guid effectId)
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
    public static ReadOnlyMemory<byte> GetRegistrationBlob<T>(Func<ID2D1TransformMapper<T>>? mapperFactory, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader
    {
        effectId = default;

        PixelShaderEffect.For<T>.Initialize(mapperFactory);

        using ArrayPoolBinaryWriter writer = new(ArrayPoolBinaryWriter.DefaultInitialBufferSize);

        // Blob id
        writer.Write(D2D1EffectRegistrationData.V1.BlobId);

        // Effect id and number of inputs
        writer.Write(PixelShaderEffect.For<T>.Id);
        writer.Write(PixelShaderEffect.For<T>.InputCount);
        
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
        for (int i = 0; i < PixelShaderEffect.For<T>.InputCount; i++)
        {
            writer.WriteAsUtf8("<Input name='Source");
            writer.WriteAsUtf8(i.ToString(CultureInfo.InvariantCulture));
            writer.WriteAsUtf8($"'/>");
        }

        // Write the last part of the XML (including the buffer property)
        writer.WriteAsUtf8(@"
    </Inputs>
    <Property name='ConstantBuffer' type='blob'>
        <Property name='DisplayName' type='string' value='ConstantBuffer'/>
    </Property>
    <Property name='ResourceTextureManager0' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager0'/>
    </Property>
    <Property name='ResourceTextureManager1' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager1'/>
    </Property>
    <Property name='ResourceTextureManager2' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager2'/>
    </Property>
    <Property name='ResourceTextureManager3' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager3'/>
    </Property>
    <Property name='ResourceTextureManager4' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager4'/>
    </Property>
    <Property name='ResourceTextureManager5' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager5'/>
    </Property>
    <Property name='ResourceTextureManager6' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager6'/>
    </Property>
    <Property name='ResourceTextureManager7' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager7'/>
    </Property>
    <Property name='ResourceTextureManager8' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager8'/>
    </Property>
    <Property name='ResourceTextureManager9' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager9'/>
    </Property>
    <Property name='ResourceTextureManager10' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager10'/>
    </Property>
    <Property name='ResourceTextureManager11' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager11'/>
    </Property>
    <Property name='ResourceTextureManager12' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager12'/>
    </Property>
    <Property name='ResourceTextureManager13' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager13'/>
    </Property>
    <Property name='ResourceTextureManager14' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager14'/>
    </Property>
    <Property name='ResourceTextureManager15' type='iunknown'>
        <Property name='DisplayName' type='string' value='ResourceTextureManager15'/>
    </Property>
</Effect>");

        // Null terminator for the text
        writer.Write((byte)'\0');

        // Bindings
        writer.Write(17);
        writer.WriteAsUtf8("ConstantBuffer");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetConstantBuffer);
        writer.Write((nint)PixelShaderEffect.SetConstantBuffer);
        writer.WriteAsUtf8("ResourceTextureManager0");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager0);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager0);
        writer.WriteAsUtf8("ResourceTextureManager1");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager1);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager1);
        writer.WriteAsUtf8("ResourceTextureManager2");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager2);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager2);
        writer.WriteAsUtf8("ResourceTextureManager3");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager3);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager3);
        writer.WriteAsUtf8("ResourceTextureManager4");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager4);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager4);
        writer.WriteAsUtf8("ResourceTextureManager5");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager5);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager5);
        writer.WriteAsUtf8("ResourceTextureManager6");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager6);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager6);
        writer.WriteAsUtf8("ResourceTextureManager7");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager7);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager7);
        writer.WriteAsUtf8("ResourceTextureManager8");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager8);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager8);
        writer.WriteAsUtf8("ResourceTextureManager9");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager9);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager9);
        writer.WriteAsUtf8("ResourceTextureManager10");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager10);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager10);
        writer.WriteAsUtf8("ResourceTextureManager11");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager11);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager11);
        writer.WriteAsUtf8("ResourceTextureManager12");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager12);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager12);
        writer.WriteAsUtf8("ResourceTextureManager13");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager13);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager13);
        writer.WriteAsUtf8("ResourceTextureManager14");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager14);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager14);
        writer.WriteAsUtf8("ResourceTextureManager15");
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager15);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager15);

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
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1DeviceContext"/> or <paramref name="d2D1Effect"/> are <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if this method is called before initializing a shader effect for the shader type <typeparamref name="T"/>.
    /// To do so, make sure to call either <see cref="RegisterForD2D1Factory1{T}(void*, out Guid)"/> (or an overload), or
    /// to use <see cref="GetRegistrationBlob{T}(out Guid)"/> (or an overload), and use that blob to register an effect.
    /// </exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1devicecontext-createeffect"/>.</remarks>
    public static void CreateFromD2D1DeviceContext<T>(void* d2D1DeviceContext, void** d2D1Effect)
        where T : unmanaged, ID2D1PixelShader
    {
        if (d2D1DeviceContext is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(d2D1DeviceContext), "The input ID2D1DeviceContext object cannot be null.");
        }

        if (d2D1Effect is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(d2D1Effect), "The pointer to the target ID2D1Effect result cannot be null.");
        }

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
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> is <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetConstantBufferForD2D1Effect<T>(in T shader, void* d2D1Effect)
        where T : unmanaged, ID2D1PixelShader
    {
        if (d2D1Effect is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(d2D1Effect), "The input ID2D1DeviceContext object cannot be null.");
        }

        D2D1EffectDispatchDataLoader dataLoader = new((ID2D1Effect*)d2D1Effect);

        Unsafe.AsRef(in shader).LoadDispatchData(ref dataLoader);
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
        if (d2D1Effect is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(d2D1Effect), "The input ID2D1Effect object cannot be null.");
        }

        if (resourceTextureManager is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(resourceTextureManager), "The input ID2D1ResourceTextureManager object cannot be null.");
        }

        ((ID2D1Effect*)d2D1Effect)->SetValue(
            index: D2D1PixelShaderEffectProperty.ResourceTextureManager0 + (uint)resourceTextureIndex,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)&resourceTextureManager,
            dataSize: (uint)sizeof(void*)).Assert();
    }

    /// <summary>
    /// Sets the resource texture manager from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="resourceTextureManager">The input <see cref="D2D1ResourceTextureManager"/> instance..</param>
    /// <param name="resourceTextureIndex">The index of the resource texture to assign the resource texture manager to.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> or <paramref name="resourceTextureManager"/> are <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetResourceTextureManagerForD2D1Effect(void* d2D1Effect, D2D1ResourceTextureManager resourceTextureManager, int resourceTextureIndex)
    {
        if (d2D1Effect is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(d2D1Effect), "The input ID2D1Effect object cannot be null.");
        }

        if (resourceTextureManager is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(resourceTextureManager), "The input D2D1ResourceTextureManager object cannot be null.");
        }

        using ComPtr<ID2D1ResourceTextureManager> resourceTextureManager2 = default;

        resourceTextureManager.GetD2D1ResourceTextureManager(resourceTextureManager2.GetAddressOf());

        ((ID2D1Effect*)d2D1Effect)->SetValue(
            index: D2D1PixelShaderEffectProperty.ResourceTextureManager0 + (uint)resourceTextureIndex,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)resourceTextureManager2.GetAddressOf(),
            dataSize: (uint)sizeof(void*)).Assert();
    }
}
