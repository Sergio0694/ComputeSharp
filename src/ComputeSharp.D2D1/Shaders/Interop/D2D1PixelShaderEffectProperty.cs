namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// <para>A container for indices of properties from the <see cref="D2D1PixelShaderEffect"/> type.</para>
/// <para>The values in this type can be used to easily set and retrieve properties from effect instances.</para>
/// </summary>
public static class D2D1PixelShaderEffectProperty
{
    /// <summary>
    /// <para>The index for the constant buffer property of a <see cref="D2D1PixelShaderEffect"/> object.</para>
    /// </summary>
    /// <remarks>
    /// <para>The value to set for this property can be retrieved with <see cref="D2D1PixelShader.GetConstantBuffer{T}(in T)"/>.</para>
    /// <para>The value of this property is of type <c>D2D1_PROPERTY_TYPE_BLOB</c>.</para>
    /// <para>This value can be passed when calling <c>ID2D1Effect::GetValue</c>.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1properties-getvalue(u)"/>.</para>
    /// </remarks>
    public const uint ConstantBuffer = 0;
}
