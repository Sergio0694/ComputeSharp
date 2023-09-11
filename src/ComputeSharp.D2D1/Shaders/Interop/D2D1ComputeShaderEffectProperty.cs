namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// <para>A container for indices of properties from the <see cref="D2D1ComputeShaderEffect"/> type.</para>
/// <para>The values in this type can be used to easily set and retrieve properties from effect instances.</para>
/// </summary>
public static class D2D1ComputeShaderEffectProperty
{
    /// <summary>
    /// The index for the constant buffer property of a <see cref="D2D1ComputeShaderEffect"/> object.
    /// </summary>
    /// <remarks>
    /// <para>The value to set for this property can be retrieved with <see cref="D2D1ComputeShader.GetConstantBuffer{T}(in T)"/>.</para>
    /// <para>The value of this property is of type <c>D2D1_PROPERTY_TYPE_BLOB</c>.</para>
    /// <para>This value can be passed when calling <c>ID2D1Effect::GetValue</c>.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-getvalue(u)"/>.</para>
    /// </remarks>
    public const uint ConstantBuffer = 0;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager0"/>
    public const uint ResourceTextureManager0 = 1;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager1"/>
    public const uint ResourceTextureManager1 = 2;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager2"/>
    public const uint ResourceTextureManager2 = 3;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager3"/>
    public const uint ResourceTextureManager3 = 4;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager4"/>
    public const uint ResourceTextureManager4 = 5;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager5"/>
    public const uint ResourceTextureManager5 = 6;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager6"/>
    public const uint ResourceTextureManager6 = 7;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager7"/>
    public const uint ResourceTextureManager7 = 8;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager8"/>
    public const uint ResourceTextureManager8 = 9;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager9"/>
    public const uint ResourceTextureManager9 = 10;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager10"/>
    public const uint ResourceTextureManager10 = 11;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager11"/>
    public const uint ResourceTextureManager11 = 12;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager12"/>
    public const uint ResourceTextureManager12 = 13;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager13"/>
    public const uint ResourceTextureManager13 = 14;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager14"/>
    public const uint ResourceTextureManager14 = 15;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.ResourceTextureManager15"/>
    public const uint ResourceTextureManager15 = 16;

    /// <summary>
    /// The index for the <c>ID2D1TransformMapper</c> property of a <see cref="D2D1ComputeShaderEffect"/> object.
    /// </summary>
    /// <remarks>
    /// <para>The value of this property is of type <c>D2D1_PROPERTY_TYPE_IUNKNOWN</c>.</para>
    /// <para>This value can be passed when calling <c>ID2D1Effect::SetValue</c>.</para>
    /// <para>For more info, see <see href="https://learn.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-setvalue(uint32_d2d1_property_type_constbyte_uint32)"/>.</para>
    /// </remarks>
    public const uint TransformMapper = 17;

    /// <inheritdoc cref="D2D1PixelShaderEffectProperty.NumberOfProperties"/>
    internal const uint NumberOfProperties = 18;
}