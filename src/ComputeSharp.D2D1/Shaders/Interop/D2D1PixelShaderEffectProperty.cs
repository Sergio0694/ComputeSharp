namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// <para>A container for indices of properties from the <see cref="D2D1PixelShaderEffect"/> type.</para>
/// <para>The values in this type can be used to easily set and retrieve properties from effect instances.</para>
/// </summary>
public static class D2D1PixelShaderEffectProperty
{
    /// <summary>
    /// The index for the constant buffer property of a <see cref="D2D1PixelShaderEffect"/> object.
    /// </summary>
    /// <remarks>
    /// <para>The value to set for this property can be retrieved with <see cref="D2D1PixelShader.GetConstantBuffer{T}(in T)"/>.</para>
    /// <para>The value of this property is of type <c>D2D1_PROPERTY_TYPE_BLOB</c>.</para>
    /// <para>This value can be passed when calling <c>ID2D1Effect::GetValue</c>.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-getvalue(u)"/>.</para>
    /// </remarks>
    public const uint ConstantBuffer = 0;

    /// <summary>
    /// The index for the <c>ID2D1DrawTransformMapper</c> property of a <see cref="D2D1PixelShaderEffect"/> object.
    /// </summary>
    /// <remarks>
    /// <para>The value of this property is of type <c>D2D1_PROPERTY_TYPE_IUNKNOWN</c>.</para>
    /// <para>This value can be passed when calling <c>ID2D1Effect::SetValue</c>.</para>
    /// <para>For more info, see <see href="https://learn.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</para>
    /// </remarks>
    public const uint TransformMapper = 1;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 0.
    /// </summary>
    /// <remarks>
    /// <para>For more info, see <see cref="D2D1ResourceTextureManager"/>.</para>
    /// <para>This property is only available if a shader declares at least N + 1 resource textures.</para>
    /// </remarks>
    public const uint ResourceTextureManager0 = 2;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 1.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager1 = 3;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 2.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager2 = 4;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 3.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager3 = 5;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 4.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager4 = 6;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 5.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager5 = 7;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 6.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager6 = 8;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 7.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager7 = 9;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 8.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager8 = 10;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 9.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager9 = 11;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 10.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager10 = 12;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 11.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager11 = 13;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 12.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager12 = 14;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 13.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager13 = 15;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 14.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager14 = 16;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 15.
    /// </summary>
    /// <remarks><inheritdoc cref="ResourceTextureManager0" path="/remarks/node()"/></remarks>
    public const uint ResourceTextureManager15 = 17;

    /// <summary>
    /// The maximum total number of properties.
    /// </summary>
    /// <remarks>This should always be the value of the last property above, plus 1.</remarks>
    internal const int MaximumNumberOfAvailableProperties = 18;

    /// <summary>
    /// The number of always available properties.
    /// </summary>
    /// <remarks>These are <see cref="ConstantBuffer"/> and <see cref="TransformMapper"/>.</remarks>
    internal const int NumberOfAlwaysAvailableProperties = 2;
}