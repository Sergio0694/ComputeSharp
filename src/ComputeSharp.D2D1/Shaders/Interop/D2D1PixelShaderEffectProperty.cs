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
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 0.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager0 = 1;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 1.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager1 = 2;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 2.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager2 = 3;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 3.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager3 = 4;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 4.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager4 = 5;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 5.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager5 = 6;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 6.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager6 = 7;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 7.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager7 = 8;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 8.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager8 = 9;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 9.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager9 = 10;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 10.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager10 = 11;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 11.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager11 = 12;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 12.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager12 = 13;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 13.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager13 = 14;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 14.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager14 = 15;

    /// <summary>
    /// The index for the <c>ID2D1ResourceTextureManager</c> instance for the resource texture at index 15.
    /// </summary>
    /// <remarks>For more info, see <see cref="D2D1ResourceTextureManager"/>.</remarks>
    public const uint ResourceTextureManager15 = 16;
}
