using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using ComputeSharp.D2D1.Descriptors;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop.Effects;
using ComputeSharp.D2D1.Shaders.Interop.Buffers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;
using ComputeSharp.D2D1.Shaders.Interop.Effects.TransformMappers;
using ComputeSharp.D2D1.Shaders.Loaders;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// An implementation of a D2D1 pixel shader effect that can be used to instantiate <c>ID2D1Effect</c> objects.
/// </summary>
public static unsafe class D2D1PixelShaderEffect
{
    /// <summary>
    /// Gets the effect id of the D2D effect using this shader.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the effect id for.</typeparam>
    /// <returns>The effect id of the D2D effect using <typeparamref name="T"/> shaders.</returns>
    public static ref readonly Guid GetEffectId<T>()
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        Unsafe.SkipInit(out T shader);

        return ref shader.EffectId;
    }

    /// <summary>
    /// Gets the effect display name of the D2D effect using this shader, if available.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the effect display name for.</typeparam>
    /// <returns>The effect display name of the D2D effect using <typeparamref name="T"/> shaders, if available.</returns>
    public static string? GetEffectDisplayName<T>()
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        Unsafe.SkipInit(out T shader);

        return shader.EffectDisplayName;
    }

    /// <summary>
    /// Gets the effect description of the D2D effect using this shader, if available.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the effect description for.</typeparam>
    /// <returns>The effect description of the D2D effect using <typeparamref name="T"/> shaders, if available.</returns>
    public static string? GetEffectDescription<T>()
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        Unsafe.SkipInit(out T shader);

        return shader.EffectDescription;
    }

    /// <summary>
    /// Gets the effect category of the D2D effect using this shader, if available.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the effect category for.</typeparam>
    /// <returns>The effect category of the D2D effect using <typeparamref name="T"/> shaders, if available.</returns>
    public static string? GetEffectCategory<T>()
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        Unsafe.SkipInit(out T shader);

        return shader.EffectCategory;
    }

    /// <summary>
    /// Gets the effect author of the D2D effect using this shader, if available.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the effect author for.</typeparam>
    /// <returns>The effect author of the D2D effect using <typeparamref name="T"/> shaders, if available.</returns>
    public static string? GetEffectAuthor<T>()
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        Unsafe.SkipInit(out T shader);

        return shader.EffectAuthor;
    }

    /// <summary>
    /// Registers an effect from an input D2D1 pixel shader, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to register.</typeparam>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <param name="effectId">The <see cref="Guid"/> of the registered effect, which can be used to call <c>ID2D1DeviceContext::CreateEffect</c>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Factory1"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown if an effect is registered multiple times with different properties.</exception>
    /// <remarks>
    /// <para>The returned <paramref name="effectId"/> value is the same as that returned by <see cref="GetEffectId{T}"/>.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</para>
    /// </remarks>
    public static void RegisterForD2D1Factory1<T>(void* d2D1Factory1, out Guid effectId)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Factory1);

        effectId = default;

        using D2D1EffectXmlFactory.EffectXml effectXml = D2D1EffectXmlFactory.GetXmlBuffer<T>();

        fixed (char* pXml = effectXml.GetOrAllocateBuffer())
        fixed (char* pBufferPropertyName = nameof(D2D1PixelShaderEffectProperty.ConstantBuffer))
        fixed (char* pResourceTextureManager0PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager0))
        fixed (char* pResourceTextureManager1PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager1))
        fixed (char* pResourceTextureManager2PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager2))
        fixed (char* pResourceTextureManager3PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager3))
        fixed (char* pResourceTextureManager4PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager4))
        fixed (char* pResourceTextureManager5PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager5))
        fixed (char* pResourceTextureManager6PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager6))
        fixed (char* pResourceTextureManager7PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager7))
        fixed (char* pResourceTextureManager8PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager8))
        fixed (char* pResourceTextureManager9PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager9))
        fixed (char* pResourceTextureManager10PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager10))
        fixed (char* pResourceTextureManager11PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager11))
        fixed (char* pResourceTextureManager12PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager12))
        fixed (char* pResourceTextureManager13PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager13))
        fixed (char* pResourceTextureManager14PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager14))
        fixed (char* pResourceTextureManager15PropertyName = nameof(D2D1PixelShaderEffectProperty.ResourceTextureManager15))
        fixed (char* pTransformMapperPropertyName = nameof(D2D1PixelShaderEffectProperty.TransformMapper))
        {
            // Prepare the effect binding functions
            D2D1_PROPERTY_BINDING* d2D1PropertyBinding = stackalloc D2D1_PROPERTY_BINDING[(int)D2D1PixelShaderEffectProperty.NumberOfProperties];

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
            d2D1PropertyBinding[17].propertyName = (ushort*)pTransformMapperPropertyName;

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
            d2D1PropertyBinding[17].getFunction = PixelShaderEffect.GetTransformMapper;

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
            d2D1PropertyBinding[17].setFunction = PixelShaderEffect.SetTransformMapper;

            fixed (Guid* pGuid = &PixelShaderEffect.For<T>.Instance.Id)
            {
                // Register the effect
                ((ID2D1Factory1*)d2D1Factory1)->RegisterEffectFromString(
                    classId: pGuid,
                    propertyXml: (ushort*)pXml,
                    bindings: d2D1PropertyBinding,
                    bindingsCount: D2D1PixelShaderEffectProperty.NumberOfProperties,
                    effectFactory: PixelShaderEffect.For<T>.Instance.Factory).Assert();
            }

            effectId = PixelShaderEffect.For<T>.Instance.Id;
        }
    }

    /// <summary>
    /// Gets a binary blob containing serialized information that can be used to register an effect, by using <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to get the registration blob for.</typeparam>
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
    /// <para>The returned <paramref name="effectId"/> value is the same as that returned by <see cref="GetEffectId{T}"/>.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</para>
    /// </remarks>
    public static ReadOnlyMemory<byte> GetRegistrationBlob<T>(out Guid effectId)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        ArrayPoolBufferWriter writer = new(ArrayPoolBufferWriter.DefaultInitialBufferSize);

        // Blob id
        writer.Write(D2D1EffectRegistrationData.V1.BlobId);

        // Effect id and number of inputs
        writer.Write(PixelShaderEffect.For<T>.Instance.Id);
        writer.Write(PixelShaderEffect.For<T>.Instance.InputCount);

        // Retrieve the effect XML and include it into the registration blob
        using (D2D1EffectXmlFactory.EffectXml effectXml = D2D1EffectXmlFactory.GetXmlBuffer<T>())
        {
            ReadOnlySpan<char> registrationText = effectXml.GetOrAllocateBuffer();
            int maxRegistrationTextByteCount = Encoding.UTF8.GetMaxByteCount(registrationText.Length);
            byte[] registrationTextUtf8 = ArrayPool<byte>.Shared.Rent(maxRegistrationTextByteCount);

            // Encode the effect XML into UTF8 to write into the registration blob.
            // This is marginally slower than building it inline here directly in
            // UTF8 format, but the registration blob is not the primary scenario.
            // Besides, the overhead is just a matter of a few more microseconds.
            int registrationTextByteCount = Encoding.UTF8.GetBytes(registrationText, registrationTextUtf8);

            // Build the XML text
            writer.Write(registrationTextUtf8.AsSpan(0, registrationTextByteCount));

            ArrayPool<byte>.Shared.Return(registrationTextUtf8);
        }

        // Bindings
        writer.Write(D2D1PixelShaderEffectProperty.NumberOfProperties);
        writer.Write("ConstantBuffer"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetConstantBuffer);
        writer.Write((nint)PixelShaderEffect.SetConstantBuffer);
        writer.Write("ResourceTextureManager0"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager0);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager0);
        writer.Write("ResourceTextureManager1"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager1);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager1);
        writer.Write("ResourceTextureManager2"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager2);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager2);
        writer.Write("ResourceTextureManager3"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager3);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager3);
        writer.Write("ResourceTextureManager4"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager4);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager4);
        writer.Write("ResourceTextureManager5"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager5);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager5);
        writer.Write("ResourceTextureManager6"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager6);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager6);
        writer.Write("ResourceTextureManager7"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager7);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager7);
        writer.Write("ResourceTextureManager8"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager8);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager8);
        writer.Write("ResourceTextureManager9"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager9);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager9);
        writer.Write("ResourceTextureManager10"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager10);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager10);
        writer.Write("ResourceTextureManager11"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager11);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager11);
        writer.Write("ResourceTextureManager12"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager12);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager12);
        writer.Write("ResourceTextureManager13"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager13);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager13);
        writer.Write("ResourceTextureManager14"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager14);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager14);
        writer.Write("ResourceTextureManager15"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetResourceTextureManager15);
        writer.Write((nint)PixelShaderEffect.SetResourceTextureManager15);
        writer.Write("TransformMapper"u8);
        writer.Write((byte)'\0');
        writer.Write((nint)PixelShaderEffect.GetTransformMapper);
        writer.Write((nint)PixelShaderEffect.SetTransformMapper);

        // Effect factory
        writer.Write((nint)PixelShaderEffect.For<T>.Instance.Factory);

        byte[] registrationBlob = writer.WrittenSpan.ToArray();

        writer.Dispose();

        // Extract the effect id (the same that was encoded in the registration blob)
        effectId = PixelShaderEffect.For<T>.Instance.Id;

        return registrationBlob;
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
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        default(ArgumentNullException).ThrowIfNull(d2D1DeviceContext);
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);

        fixed (Guid* pGuid = &PixelShaderEffect.For<T>.Instance.Id)
        {
            ((ID2D1DeviceContext*)d2D1DeviceContext)->CreateEffect(
                effectId: pGuid,
                effect: (ID2D1Effect**)d2D1Effect).Assert();
        }
    }

    /// <summary>
    /// Sets the constant buffer from an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <typeparam name="T">The type of D2D1 pixel shader to set the constant buffer for.</typeparam>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="shader">The input D2D1 pixel shader to set the contant buffer for.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> is <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetConstantBufferForD2D1Effect<T>(void* d2D1Effect, in T shader)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);

        D2D1EffectConstantBufferLoader dataLoader = new((ID2D1Effect*)d2D1Effect);

        Unsafe.AsRef(in shader).LoadConstantBuffer(in shader, ref dataLoader);
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
    /// <param name="resourceTextureManager">The input <see cref="D2D1ResourceTextureManager"/> instance.</param>
    /// <param name="resourceTextureIndex">The index of the resource texture to assign the resource texture manager to.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> or <paramref name="resourceTextureManager"/> are <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetResourceTextureManagerForD2D1Effect(void* d2D1Effect, D2D1ResourceTextureManager resourceTextureManager, int resourceTextureIndex)
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);
        default(ArgumentNullException).ThrowIfNull(resourceTextureManager);

        using ComPtr<ID2D1ResourceTextureManager> resourceTextureManager2 = default;

        resourceTextureManager.GetD2D1ResourceTextureManager(resourceTextureManager2.GetAddressOf());

        ((ID2D1Effect*)d2D1Effect)->SetValue(
            index: D2D1PixelShaderEffectProperty.ResourceTextureManager0 + (uint)resourceTextureIndex,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)resourceTextureManager2.GetAddressOf(),
            dataSize: (uint)sizeof(void*)).Assert();
    }

    /// <summary>
    /// Sets the transform mapper for an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="transformMapper">The input <c>ID2D1TransformMapper</c> object (see <see cref="D2D1TransformMapper{T}"/>).</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> or <paramref name="transformMapper"/> are <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetTransformMapperForD2D1Effect(void* d2D1Effect, void* transformMapper)
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);
        default(ArgumentNullException).ThrowIfNull(transformMapper);

        ((ID2D1Effect*)d2D1Effect)->SetValue(
            index: D2D1PixelShaderEffectProperty.TransformMapper,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)&transformMapper,
            dataSize: (uint)sizeof(void*)).Assert();
    }

    /// <summary>
    /// Sets the transform mapper for an input D2D1 effect, by calling <c>ID2D1Effect::SetValue</c>.
    /// </summary>
    /// <param name="d2D1Effect">A pointer to the <c>ID2D1Effect</c> instance to use.</param>
    /// <param name="transformMapper">The input <c>ID2D1TransformMapper</c> object.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Effect"/> or <paramref name="transformMapper"/> are <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</remarks>
    public static void SetTransformMapperForD2D1Effect<T>(void* d2D1Effect, D2D1TransformMapper<T> transformMapper)
        where T : unmanaged, ID2D1PixelShader, ID2D1PixelShaderDescriptor<T>
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Effect);
        default(ArgumentNullException).ThrowIfNull(transformMapper);

        using ComPtr<ID2D1TransformMapper> transformMapper2 = default;

        transformMapper.GetD2D1TransformMapper(transformMapper2.GetAddressOf());

        ((ID2D1Effect*)d2D1Effect)->SetValue(
            index: D2D1PixelShaderEffectProperty.TransformMapper,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_IUNKNOWN,
            data: (byte*)transformMapper2.GetAddressOf(),
            dataSize: (uint)sizeof(void*)).Assert();
    }
}