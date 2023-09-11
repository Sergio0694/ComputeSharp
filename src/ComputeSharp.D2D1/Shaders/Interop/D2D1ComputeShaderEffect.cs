using System;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop.Effects;
using ComputeSharp.D2D1.Interop.Helpers;
using ComputeSharp.D2D1.Shaders.Interop.Buffers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// An implementation of a D2D1 compute shader effect that can be used to instantiate <c>ID2D1Effect</c> objects.
/// </summary>
public static unsafe class D2D1ComputeShaderEffect
{
    /// <summary>
    /// Registers an effect from an input D2D1 compute shader, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 compute shader to register.</typeparam>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Factory1"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static void RegisterForD2D1Factory1<T>(void* d2D1Factory1, out Guid effectId)
        where T : unmanaged, ID2D1ComputeShader
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Factory1);

        effectId = default;

        ArrayPoolBufferWriter<char> writer = new(ArrayPoolBinaryWriter.DefaultInitialBufferSize);

        // Build the XML text
        writer.WriteRaw("""
            <?xml version='1.0'?>
            <Effect>
                <Property name='DisplayName' type='string' value='
            """);
        writer.WriteRaw(typeof(T).FullName!);
        writer.WriteRaw("""
            '/>
                <Property name='Author' type='string' value='ComputeSharp.D2D1'/>
                <Property name='Category' type='string' value='Stylize'/>
                <Property name='Description' type='string' value='A custom D2D1 effect using a compute shader'/>
                <Inputs>

            """);

        // Add the input nodes
        for (int i = 0; i < ComputeShaderEffect.For<T>.Instance.InputCount; i++)
        {
            writer.WriteRaw("        <Input name='Source");
            writer.WriteAsUnicode(i);
            writer.WriteRaw("""
                '/>

                """);
        }

        // Write the last part of the XML (including the buffer property)
        writer.WriteRaw("""
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
                <Property name='TransformMapper' type='iunknown'>
                    <Property name='DisplayName' type='string' value='TransformMapper'/>
                </Property>
            </Effect>
            """);

        // Null terminator for the text
        writer.WriteRaw('\0');

        fixed (char* pXml = writer.WrittenSpan)
        fixed (char* pBufferPropertyName = nameof(D2D1ComputeShaderEffectProperty.ConstantBuffer))
        fixed (char* pResourceTextureManager0PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager0))
        fixed (char* pResourceTextureManager1PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager1))
        fixed (char* pResourceTextureManager2PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager2))
        fixed (char* pResourceTextureManager3PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager3))
        fixed (char* pResourceTextureManager4PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager4))
        fixed (char* pResourceTextureManager5PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager5))
        fixed (char* pResourceTextureManager6PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager6))
        fixed (char* pResourceTextureManager7PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager7))
        fixed (char* pResourceTextureManager8PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager8))
        fixed (char* pResourceTextureManager9PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager9))
        fixed (char* pResourceTextureManager10PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager10))
        fixed (char* pResourceTextureManager11PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager11))
        fixed (char* pResourceTextureManager12PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager12))
        fixed (char* pResourceTextureManager13PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager13))
        fixed (char* pResourceTextureManager14PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager14))
        fixed (char* pResourceTextureManager15PropertyName = nameof(D2D1ComputeShaderEffectProperty.ResourceTextureManager15))
        fixed (char* pTransformMapperPropertyName = nameof(D2D1ComputeShaderEffectProperty.TransformMapper))
        {
            // Prepare the effect binding functions
            D2D1_PROPERTY_BINDING* d2D1PropertyBinding = stackalloc D2D1_PROPERTY_BINDING[(int)D2D1ComputeShaderEffectProperty.NumberOfProperties];

            // Property names
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ConstantBuffer].propertyName = (ushort*)pBufferPropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager0].propertyName = (ushort*)pResourceTextureManager0PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager1].propertyName = (ushort*)pResourceTextureManager1PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager2].propertyName = (ushort*)pResourceTextureManager2PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager3].propertyName = (ushort*)pResourceTextureManager3PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager4].propertyName = (ushort*)pResourceTextureManager4PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager5].propertyName = (ushort*)pResourceTextureManager5PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager6].propertyName = (ushort*)pResourceTextureManager6PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager7].propertyName = (ushort*)pResourceTextureManager7PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager8].propertyName = (ushort*)pResourceTextureManager8PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager9].propertyName = (ushort*)pResourceTextureManager9PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager10].propertyName = (ushort*)pResourceTextureManager10PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager11].propertyName = (ushort*)pResourceTextureManager11PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager12].propertyName = (ushort*)pResourceTextureManager12PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager13].propertyName = (ushort*)pResourceTextureManager13PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager14].propertyName = (ushort*)pResourceTextureManager14PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager15].propertyName = (ushort*)pResourceTextureManager15PropertyName;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.TransformMapper].propertyName = (ushort*)pTransformMapperPropertyName;

            // Property getters
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ConstantBuffer].getFunction = ComputeShaderEffect.GetConstantBuffer;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager0].getFunction = ComputeShaderEffect.GetResourceTextureManager0;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager1].getFunction = ComputeShaderEffect.GetResourceTextureManager1;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager2].getFunction = ComputeShaderEffect.GetResourceTextureManager2;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager3].getFunction = ComputeShaderEffect.GetResourceTextureManager3;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager4].getFunction = ComputeShaderEffect.GetResourceTextureManager4;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager5].getFunction = ComputeShaderEffect.GetResourceTextureManager5;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager6].getFunction = ComputeShaderEffect.GetResourceTextureManager6;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager7].getFunction = ComputeShaderEffect.GetResourceTextureManager7;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager8].getFunction = ComputeShaderEffect.GetResourceTextureManager8;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager9].getFunction = ComputeShaderEffect.GetResourceTextureManager9;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager10].getFunction = ComputeShaderEffect.GetResourceTextureManager10;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager11].getFunction = ComputeShaderEffect.GetResourceTextureManager11;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager12].getFunction = ComputeShaderEffect.GetResourceTextureManager12;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager13].getFunction = ComputeShaderEffect.GetResourceTextureManager13;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager14].getFunction = ComputeShaderEffect.GetResourceTextureManager14;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager15].getFunction = ComputeShaderEffect.GetResourceTextureManager15;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.TransformMapper].getFunction = ComputeShaderEffect.GetTransformMapper;

            // Property setters
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ConstantBuffer].setFunction = ComputeShaderEffect.SetConstantBuffer;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager0].setFunction = ComputeShaderEffect.SetResourceTextureManager0;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager1].setFunction = ComputeShaderEffect.SetResourceTextureManager1;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager2].setFunction = ComputeShaderEffect.SetResourceTextureManager2;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager3].setFunction = ComputeShaderEffect.SetResourceTextureManager3;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager4].setFunction = ComputeShaderEffect.SetResourceTextureManager4;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager5].setFunction = ComputeShaderEffect.SetResourceTextureManager5;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager6].setFunction = ComputeShaderEffect.SetResourceTextureManager6;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager7].setFunction = ComputeShaderEffect.SetResourceTextureManager7;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager8].setFunction = ComputeShaderEffect.SetResourceTextureManager8;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager9].setFunction = ComputeShaderEffect.SetResourceTextureManager9;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager10].setFunction = ComputeShaderEffect.SetResourceTextureManager10;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager11].setFunction = ComputeShaderEffect.SetResourceTextureManager11;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager12].setFunction = ComputeShaderEffect.SetResourceTextureManager12;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager13].setFunction = ComputeShaderEffect.SetResourceTextureManager13;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager14].setFunction = ComputeShaderEffect.SetResourceTextureManager14;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.ResourceTextureManager15].setFunction = ComputeShaderEffect.SetResourceTextureManager15;
            d2D1PropertyBinding[D2D1ComputeShaderEffectProperty.TransformMapper].setFunction = ComputeShaderEffect.SetTransformMapper;

            fixed (Guid* pGuid = &ComputeShaderEffect.For<T>.Instance.Id)
            {
                // Register the effect
                ((ID2D1Factory1*)d2D1Factory1)->RegisterEffectFromString(
                    classId: pGuid,
                    propertyXml: (ushort*)pXml,
                    bindings: d2D1PropertyBinding,
                    bindingsCount: D2D1ComputeShaderEffectProperty.NumberOfProperties,
                    effectFactory: ComputeShaderEffect.For<T>.Instance.Factory).Assert();
            }

            effectId = ComputeShaderEffect.For<T>.Instance.Id;
        }

        writer.Dispose();
    }

    /// <summary>
    /// Gets a binary blob containing serialized information that can be used to register an effect, by using <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 compute shader to get the registration blob for.</typeparam>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <returns>A blob containing serialized information that can be used to register an effect.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>
    /// The format for the returned blob is the same as that returned by <see cref="D2D1PixelShaderEffect.GetRegistrationBlob"/>, see remarks there.
    /// </remarks>
    public static ReadOnlyMemory<byte> GetRegistrationBlob<T>(out Guid effectId)
        where T : unmanaged, ID2D1ComputeShader
    {
        ArrayPoolBufferWriter<byte> writer = new(ArrayPoolBinaryWriter.DefaultInitialBufferSize);

        // Blob id
        writer.WriteRaw(D2D1EffectRegistrationData.V1.BlobId);

        // Effect id and number of inputs
        writer.WriteRaw(ComputeShaderEffect.For<T>.Instance.Id);
        writer.WriteRaw(ComputeShaderEffect.For<T>.Instance.InputCount);

        // Build the XML text
        writer.WriteRaw("""
            <?xml version='1.0'?>
            <Effect>
                <Property name='DisplayName' type='string' value='
            """u8);
        writer.WriteAsUtf8(typeof(T).FullName!);
        writer.WriteRaw("""
            '/>
                <Property name='Author' type='string' value='ComputeSharp.D2D1'/>
                <Property name='Category' type='string' value='Stylize'/>
                <Property name='Description' type='string' value='A custom D2D1 effect using a compute shader'/>
                <Inputs>

            """u8);

        // Add the input nodes
        for (int i = 0; i < ComputeShaderEffect.For<T>.Instance.InputCount; i++)
        {
            writer.WriteRaw("        <Input name='Source"u8);
            writer.WriteAsUtf8(i);
            writer.WriteRaw("""
                '/>

                """u8);
        }

        // Write the last part of the XML (including the buffer property)
        writer.WriteRaw("""
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
                <Property name='TransformMapper' type='iunknown'>
                    <Property name='DisplayName' type='string' value='TransformMapper'/>
                </Property>
            </Effect>
            """u8);

        // Null terminator for the text
        writer.WriteRaw((byte)'\0');

        // Bindings
        writer.WriteRaw(D2D1ComputeShaderEffectProperty.NumberOfProperties);
        writer.WriteRaw("ConstantBuffer"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetConstantBuffer);
        writer.WriteRaw((nint)ComputeShaderEffect.SetConstantBuffer);
        writer.WriteRaw("ResourceTextureManager0"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager0);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager0);
        writer.WriteRaw("ResourceTextureManager1"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager1);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager1);
        writer.WriteRaw("ResourceTextureManager2"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager2);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager2);
        writer.WriteRaw("ResourceTextureManager3"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager3);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager3);
        writer.WriteRaw("ResourceTextureManager4"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager4);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager4);
        writer.WriteRaw("ResourceTextureManager5"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager5);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager5);
        writer.WriteRaw("ResourceTextureManager6"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager6);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager6);
        writer.WriteRaw("ResourceTextureManager7"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager7);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager7);
        writer.WriteRaw("ResourceTextureManager8"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager8);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager8);
        writer.WriteRaw("ResourceTextureManager9"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager9);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager9);
        writer.WriteRaw("ResourceTextureManager10"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager10);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager10);
        writer.WriteRaw("ResourceTextureManager11"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager11);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager11);
        writer.WriteRaw("ResourceTextureManager12"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager12);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager12);
        writer.WriteRaw("ResourceTextureManager13"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager13);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager13);
        writer.WriteRaw("ResourceTextureManager14"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager14);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager14);
        writer.WriteRaw("ResourceTextureManager15"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetResourceTextureManager15);
        writer.WriteRaw((nint)ComputeShaderEffect.SetResourceTextureManager15);
        writer.WriteRaw("TransformMapper"u8);
        writer.WriteRaw((byte)'\0');
        writer.WriteRaw((nint)ComputeShaderEffect.GetTransformMapper);
        writer.WriteRaw((nint)ComputeShaderEffect.SetTransformMapper);

        // Effect factory
        writer.WriteRaw((nint)ComputeShaderEffect.For<T>.Instance.Factory);

        byte[] registrationBlob = writer.WrittenSpan.ToArray();

        writer.Dispose();

        // Extract the effect id (the same that was encoded in the registration blob)
        effectId = ComputeShaderEffect.For<T>.Instance.Id;

        return registrationBlob;
    }

    /// <summary>
    /// Creates an effect wrapping an input D2D1 compute shader, by calling <c>ID2D1DeviceContext::CreateEffect</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 compute shader to create an effect for.</typeparam>
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