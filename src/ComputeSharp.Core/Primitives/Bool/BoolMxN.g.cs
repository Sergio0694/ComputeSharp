using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <inheritdoc cref="Bool1x1"/>
[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
public unsafe partial struct Bool1x1
{
    [FieldOffset(0)]
    private int m11;

    /// <summary>
    /// Creates a new <see cref="Bool1x1"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    public Bool1x1(bool m11)
    {
        this.m11 = m11 ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool1x1"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref bool this[int row] => ref *(bool*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Creates a new <see cref="Bool1x1"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool1x1"/> instance.</param>
    public static implicit operator Bool1x1(bool x)
    {
        Bool1x1 matrix;

        matrix.m11 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool1x1"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator !(Bool1x1 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool1x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x1"/> value to and.</param>
    /// <param name="right">The <see cref="Bool1x1"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator &(Bool1x1 left, Bool1x1 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool1x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x1"/> value to or.</param>
    /// <param name="right">The <see cref="Bool1x1"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator |(Bool1x1 left, Bool1x1 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool1x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x1"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool1x1"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator ^(Bool1x1 left, Bool1x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool1x1"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool1x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool1x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator ==(Bool1x1 left, Bool1x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool1x1"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool1x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool1x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator !=(Bool1x1 left, Bool1x1 right) => default;
}

/// <inheritdoc cref="Bool1x2"/>
[StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
public unsafe partial struct Bool1x2
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    /// <summary>
    /// Creates a new <see cref="Bool1x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    public Bool1x2(bool m11, bool m12)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool1x2"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[int row] => ref *(Bool2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Creates a new <see cref="Bool1x2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool1x2"/> instance.</param>
    public static implicit operator Bool1x2(bool x)
    {
        Bool1x2 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool1x2"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator !(Bool1x2 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool1x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x2"/> value to and.</param>
    /// <param name="right">The <see cref="Bool1x2"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator &(Bool1x2 left, Bool1x2 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool1x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x2"/> value to or.</param>
    /// <param name="right">The <see cref="Bool1x2"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator |(Bool1x2 left, Bool1x2 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool1x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x2"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool1x2"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator ^(Bool1x2 left, Bool1x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool1x2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool1x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool1x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator ==(Bool1x2 left, Bool1x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool1x2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool1x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool1x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator !=(Bool1x2 left, Bool1x2 right) => default;

    /// <summary>
    /// Casts a <see cref="Bool2"/> value to a <see cref="Bool1x2"/> one.
    /// </summary>
    /// <param name="vector">The input <see cref="Bool2"/> value to cast.</param>
    public static implicit operator Bool1x2(Bool2 vector) => *(Bool1x2*)&vector;
}

/// <inheritdoc cref="Bool1x3"/>
[StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
public unsafe partial struct Bool1x3
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    /// <summary>
    /// Creates a new <see cref="Bool1x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    public Bool1x3(bool m11, bool m12, bool m13)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m13 = m13 ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool1x3"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[int row] => ref *(Bool3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M13 => ref Unsafe.As<int, bool>(ref this.m13);

    /// <summary>
    /// Creates a new <see cref="Bool1x3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool1x3"/> instance.</param>
    public static implicit operator Bool1x3(bool x)
    {
        Bool1x3 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m13 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool1x3"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator !(Bool1x3 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool1x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x3"/> value to and.</param>
    /// <param name="right">The <see cref="Bool1x3"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator &(Bool1x3 left, Bool1x3 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool1x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x3"/> value to or.</param>
    /// <param name="right">The <see cref="Bool1x3"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator |(Bool1x3 left, Bool1x3 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool1x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x3"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool1x3"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator ^(Bool1x3 left, Bool1x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool1x3"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool1x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool1x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator ==(Bool1x3 left, Bool1x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool1x3"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool1x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool1x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator !=(Bool1x3 left, Bool1x3 right) => default;

    /// <summary>
    /// Casts a <see cref="Bool3"/> value to a <see cref="Bool1x3"/> one.
    /// </summary>
    /// <param name="vector">The input <see cref="Bool3"/> value to cast.</param>
    public static implicit operator Bool1x3(Bool3 vector) => *(Bool1x3*)&vector;
}

/// <inheritdoc cref="Bool1x4"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
public unsafe partial struct Bool1x4
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    [FieldOffset(12)]
    private int m14;

    /// <summary>
    /// Creates a new <see cref="Bool1x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m14">The value to assign to the component at position [1, 4].</param>
    public Bool1x4(bool m11, bool m12, bool m13, bool m14)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m13 = m13 ? 1 : 0;
        this.m14 = m14 ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool1x4"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool4 this[int row] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M13 => ref Unsafe.As<int, bool>(ref this.m13);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M14 => ref Unsafe.As<int, bool>(ref this.m14);

    /// <summary>
    /// Creates a new <see cref="Bool1x4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool1x4"/> instance.</param>
    public static implicit operator Bool1x4(bool x)
    {
        Bool1x4 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m13 = x ? 1 : 0;
        matrix.m14 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool1x4"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator !(Bool1x4 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool1x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x4"/> value to and.</param>
    /// <param name="right">The <see cref="Bool1x4"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator &(Bool1x4 left, Bool1x4 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool1x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x4"/> value to or.</param>
    /// <param name="right">The <see cref="Bool1x4"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator |(Bool1x4 left, Bool1x4 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool1x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool1x4"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool1x4"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator ^(Bool1x4 left, Bool1x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool1x4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool1x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool1x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator ==(Bool1x4 left, Bool1x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool1x4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool1x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool1x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator !=(Bool1x4 left, Bool1x4 right) => default;

    /// <summary>
    /// Casts a <see cref="Bool4"/> value to a <see cref="Bool1x4"/> one.
    /// </summary>
    /// <param name="vector">The input <see cref="Bool4"/> value to cast.</param>
    public static implicit operator Bool1x4(Bool4 vector) => *(Bool1x4*)&vector;
}

/// <inheritdoc cref="Bool2x1"/>
[StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
public unsafe partial struct Bool2x1
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m21;

    /// <summary>
    /// Creates a new <see cref="Bool2x1"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    public Bool2x1(bool m11, bool m21)
    {
        this.m11 = m11 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool2x1"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref bool this[int row] => ref *(bool*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Creates a new <see cref="Bool2x1"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool2x1"/> instance.</param>
    public static implicit operator Bool2x1(bool x)
    {
        Bool2x1 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool2x1"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator !(Bool2x1 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool2x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x1"/> value to and.</param>
    /// <param name="right">The <see cref="Bool2x1"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator &(Bool2x1 left, Bool2x1 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool2x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x1"/> value to or.</param>
    /// <param name="right">The <see cref="Bool2x1"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator |(Bool2x1 left, Bool2x1 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool2x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x1"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool2x1"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator ^(Bool2x1 left, Bool2x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool2x1"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool2x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool2x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator ==(Bool2x1 left, Bool2x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool2x1"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool2x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool2x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator !=(Bool2x1 left, Bool2x1 right) => default;

    /// <summary>
    /// Casts a <see cref="Bool2x1"/> value to a <see cref="Bool2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Bool2x1"/> value to cast.</param>
    public static implicit operator Bool2(Bool2x1 matrix) => *(Bool2*)&matrix;
}

/// <inheritdoc cref="Bool2x2"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
public unsafe partial struct Bool2x2
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m21;

    [FieldOffset(12)]
    private int m22;

    /// <summary>
    /// Creates a new <see cref="Bool2x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    public Bool2x2(bool m11, bool m12, bool m21, bool m22)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool2x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    public Bool2x2(Bool2 row1, Bool2 row2)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool2x2"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[int row] => ref *(Bool2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Creates a new <see cref="Bool2x2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool2x2"/> instance.</param>
    public static implicit operator Bool2x2(bool x)
    {
        Bool2x2 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool2x2"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator !(Bool2x2 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool2x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x2"/> value to and.</param>
    /// <param name="right">The <see cref="Bool2x2"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator &(Bool2x2 left, Bool2x2 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool2x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x2"/> value to or.</param>
    /// <param name="right">The <see cref="Bool2x2"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator |(Bool2x2 left, Bool2x2 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool2x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x2"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool2x2"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator ^(Bool2x2 left, Bool2x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool2x2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool2x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool2x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator ==(Bool2x2 left, Bool2x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool2x2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool2x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool2x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator !=(Bool2x2 left, Bool2x2 right) => default;
}

/// <inheritdoc cref="Bool2x3"/>
[StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
public unsafe partial struct Bool2x3
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    [FieldOffset(12)]
    private int m21;

    [FieldOffset(16)]
    private int m22;

    [FieldOffset(20)]
    private int m23;

    /// <summary>
    /// Creates a new <see cref="Bool2x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m23">The value to assign to the component at position [2, 3].</param>
    public Bool2x3(bool m11, bool m12, bool m13, bool m21, bool m22, bool m23)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m13 = m13 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
        this.m23 = m23 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool2x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    public Bool2x3(Bool3 row1, Bool3 row2)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m13 = row1.Z ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
        this.m23 = row2.Z ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool2x3"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[int row] => ref *(Bool3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M13 => ref Unsafe.As<int, bool>(ref this.m13);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M23 => ref Unsafe.As<int, bool>(ref this.m23);

    /// <summary>
    /// Creates a new <see cref="Bool2x3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool2x3"/> instance.</param>
    public static implicit operator Bool2x3(bool x)
    {
        Bool2x3 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m13 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;
        matrix.m23 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool2x3"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator !(Bool2x3 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool2x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x3"/> value to and.</param>
    /// <param name="right">The <see cref="Bool2x3"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator &(Bool2x3 left, Bool2x3 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool2x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x3"/> value to or.</param>
    /// <param name="right">The <see cref="Bool2x3"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator |(Bool2x3 left, Bool2x3 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool2x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x3"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool2x3"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator ^(Bool2x3 left, Bool2x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool2x3"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool2x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool2x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator ==(Bool2x3 left, Bool2x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool2x3"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool2x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool2x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator !=(Bool2x3 left, Bool2x3 right) => default;
}

/// <inheritdoc cref="Bool2x4"/>
[StructLayout(LayoutKind.Explicit, Size = 32, Pack = 4)]
public unsafe partial struct Bool2x4
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    [FieldOffset(12)]
    private int m14;

    [FieldOffset(16)]
    private int m21;

    [FieldOffset(20)]
    private int m22;

    [FieldOffset(24)]
    private int m23;

    [FieldOffset(28)]
    private int m24;

    /// <summary>
    /// Creates a new <see cref="Bool2x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m14">The value to assign to the component at position [1, 4].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m23">The value to assign to the component at position [2, 3].</param>
    /// <param name="m24">The value to assign to the component at position [2, 4].</param>
    public Bool2x4(bool m11, bool m12, bool m13, bool m14, bool m21, bool m22, bool m23, bool m24)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m13 = m13 ? 1 : 0;
        this.m14 = m14 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
        this.m23 = m23 ? 1 : 0;
        this.m24 = m24 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool2x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    public Bool2x4(Bool4 row1, Bool4 row2)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m13 = row1.Z ? 1 : 0;
        this.m14 = row1.W ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
        this.m23 = row2.Z ? 1 : 0;
        this.m24 = row2.W ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool2x4"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool4 this[int row] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M13 => ref Unsafe.As<int, bool>(ref this.m13);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M14 => ref Unsafe.As<int, bool>(ref this.m14);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M23 => ref Unsafe.As<int, bool>(ref this.m23);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M24 => ref Unsafe.As<int, bool>(ref this.m24);

    /// <summary>
    /// Creates a new <see cref="Bool2x4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool2x4"/> instance.</param>
    public static implicit operator Bool2x4(bool x)
    {
        Bool2x4 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m13 = x ? 1 : 0;
        matrix.m14 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;
        matrix.m23 = x ? 1 : 0;
        matrix.m24 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool2x4"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator !(Bool2x4 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool2x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x4"/> value to and.</param>
    /// <param name="right">The <see cref="Bool2x4"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator &(Bool2x4 left, Bool2x4 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool2x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x4"/> value to or.</param>
    /// <param name="right">The <see cref="Bool2x4"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator |(Bool2x4 left, Bool2x4 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool2x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool2x4"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool2x4"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator ^(Bool2x4 left, Bool2x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool2x4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool2x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool2x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator ==(Bool2x4 left, Bool2x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool2x4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool2x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool2x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator !=(Bool2x4 left, Bool2x4 right) => default;
}

/// <inheritdoc cref="Bool3x1"/>
[StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
public unsafe partial struct Bool3x1
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m21;

    [FieldOffset(8)]
    private int m31;

    /// <summary>
    /// Creates a new <see cref="Bool3x1"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    public Bool3x1(bool m11, bool m21, bool m31)
    {
        this.m11 = m11 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m31 = m31 ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool3x1"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref bool this[int row] => ref *(bool*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M31 => ref Unsafe.As<int, bool>(ref this.m31);

    /// <summary>
    /// Creates a new <see cref="Bool3x1"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool3x1"/> instance.</param>
    public static implicit operator Bool3x1(bool x)
    {
        Bool3x1 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m31 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool3x1"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator !(Bool3x1 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool3x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x1"/> value to and.</param>
    /// <param name="right">The <see cref="Bool3x1"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator &(Bool3x1 left, Bool3x1 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool3x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x1"/> value to or.</param>
    /// <param name="right">The <see cref="Bool3x1"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator |(Bool3x1 left, Bool3x1 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool3x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x1"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool3x1"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator ^(Bool3x1 left, Bool3x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool3x1"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool3x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool3x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator ==(Bool3x1 left, Bool3x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool3x1"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool3x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool3x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator !=(Bool3x1 left, Bool3x1 right) => default;

    /// <summary>
    /// Casts a <see cref="Bool3x1"/> value to a <see cref="Bool3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Bool3x1"/> value to cast.</param>
    public static implicit operator Bool3(Bool3x1 matrix) => *(Bool3*)&matrix;
}

/// <inheritdoc cref="Bool3x2"/>
[StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
public unsafe partial struct Bool3x2
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m21;

    [FieldOffset(12)]
    private int m22;

    [FieldOffset(16)]
    private int m31;

    [FieldOffset(20)]
    private int m32;

    /// <summary>
    /// Creates a new <see cref="Bool3x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m32">The value to assign to the component at position [3, 2].</param>
    public Bool3x2(bool m11, bool m12, bool m21, bool m22, bool m31, bool m32)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
        this.m31 = m31 ? 1 : 0;
        this.m32 = m32 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool3x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    public Bool3x2(Bool2 row1, Bool2 row2, Bool2 row3)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
        this.m31 = row3.X ? 1 : 0;
        this.m32 = row3.Y ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool3x2"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[int row] => ref *(Bool2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M31 => ref Unsafe.As<int, bool>(ref this.m31);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M32 => ref Unsafe.As<int, bool>(ref this.m32);

    /// <summary>
    /// Creates a new <see cref="Bool3x2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool3x2"/> instance.</param>
    public static implicit operator Bool3x2(bool x)
    {
        Bool3x2 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;
        matrix.m31 = x ? 1 : 0;
        matrix.m32 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool3x2"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator !(Bool3x2 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool3x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x2"/> value to and.</param>
    /// <param name="right">The <see cref="Bool3x2"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator &(Bool3x2 left, Bool3x2 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool3x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x2"/> value to or.</param>
    /// <param name="right">The <see cref="Bool3x2"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator |(Bool3x2 left, Bool3x2 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool3x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x2"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool3x2"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator ^(Bool3x2 left, Bool3x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool3x2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool3x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool3x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator ==(Bool3x2 left, Bool3x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool3x2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool3x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool3x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator !=(Bool3x2 left, Bool3x2 right) => default;
}

/// <inheritdoc cref="Bool3x3"/>
[StructLayout(LayoutKind.Explicit, Size = 36, Pack = 4)]
public unsafe partial struct Bool3x3
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    [FieldOffset(12)]
    private int m21;

    [FieldOffset(16)]
    private int m22;

    [FieldOffset(20)]
    private int m23;

    [FieldOffset(24)]
    private int m31;

    [FieldOffset(28)]
    private int m32;

    [FieldOffset(32)]
    private int m33;

    /// <summary>
    /// Creates a new <see cref="Bool3x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m23">The value to assign to the component at position [2, 3].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m32">The value to assign to the component at position [3, 2].</param>
    /// <param name="m33">The value to assign to the component at position [3, 3].</param>
    public Bool3x3(bool m11, bool m12, bool m13, bool m21, bool m22, bool m23, bool m31, bool m32, bool m33)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m13 = m13 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
        this.m23 = m23 ? 1 : 0;
        this.m31 = m31 ? 1 : 0;
        this.m32 = m32 ? 1 : 0;
        this.m33 = m33 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool3x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    public Bool3x3(Bool3 row1, Bool3 row2, Bool3 row3)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m13 = row1.Z ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
        this.m23 = row2.Z ? 1 : 0;
        this.m31 = row3.X ? 1 : 0;
        this.m32 = row3.Y ? 1 : 0;
        this.m33 = row3.Z ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool3x3"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[int row] => ref *(Bool3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M13 => ref Unsafe.As<int, bool>(ref this.m13);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M23 => ref Unsafe.As<int, bool>(ref this.m23);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M31 => ref Unsafe.As<int, bool>(ref this.m31);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M32 => ref Unsafe.As<int, bool>(ref this.m32);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M33 => ref Unsafe.As<int, bool>(ref this.m33);

    /// <summary>
    /// Creates a new <see cref="Bool3x3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool3x3"/> instance.</param>
    public static implicit operator Bool3x3(bool x)
    {
        Bool3x3 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m13 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;
        matrix.m23 = x ? 1 : 0;
        matrix.m31 = x ? 1 : 0;
        matrix.m32 = x ? 1 : 0;
        matrix.m33 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool3x3"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator !(Bool3x3 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool3x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x3"/> value to and.</param>
    /// <param name="right">The <see cref="Bool3x3"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator &(Bool3x3 left, Bool3x3 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool3x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x3"/> value to or.</param>
    /// <param name="right">The <see cref="Bool3x3"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator |(Bool3x3 left, Bool3x3 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool3x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x3"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool3x3"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator ^(Bool3x3 left, Bool3x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool3x3"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool3x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool3x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator ==(Bool3x3 left, Bool3x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool3x3"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool3x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool3x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator !=(Bool3x3 left, Bool3x3 right) => default;
}

/// <inheritdoc cref="Bool3x4"/>
[StructLayout(LayoutKind.Explicit, Size = 48, Pack = 4)]
public unsafe partial struct Bool3x4
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    [FieldOffset(12)]
    private int m14;

    [FieldOffset(16)]
    private int m21;

    [FieldOffset(20)]
    private int m22;

    [FieldOffset(24)]
    private int m23;

    [FieldOffset(28)]
    private int m24;

    [FieldOffset(32)]
    private int m31;

    [FieldOffset(36)]
    private int m32;

    [FieldOffset(40)]
    private int m33;

    [FieldOffset(44)]
    private int m34;

    /// <summary>
    /// Creates a new <see cref="Bool3x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m14">The value to assign to the component at position [1, 4].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m23">The value to assign to the component at position [2, 3].</param>
    /// <param name="m24">The value to assign to the component at position [2, 4].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m32">The value to assign to the component at position [3, 2].</param>
    /// <param name="m33">The value to assign to the component at position [3, 3].</param>
    /// <param name="m34">The value to assign to the component at position [3, 4].</param>
    public Bool3x4(bool m11, bool m12, bool m13, bool m14, bool m21, bool m22, bool m23, bool m24, bool m31, bool m32, bool m33, bool m34)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m13 = m13 ? 1 : 0;
        this.m14 = m14 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
        this.m23 = m23 ? 1 : 0;
        this.m24 = m24 ? 1 : 0;
        this.m31 = m31 ? 1 : 0;
        this.m32 = m32 ? 1 : 0;
        this.m33 = m33 ? 1 : 0;
        this.m34 = m34 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool3x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    public Bool3x4(Bool4 row1, Bool4 row2, Bool4 row3)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m13 = row1.Z ? 1 : 0;
        this.m14 = row1.W ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
        this.m23 = row2.Z ? 1 : 0;
        this.m24 = row2.W ? 1 : 0;
        this.m31 = row3.X ? 1 : 0;
        this.m32 = row3.Y ? 1 : 0;
        this.m33 = row3.Z ? 1 : 0;
        this.m34 = row3.W ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool3x4"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool4 this[int row] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M13 => ref Unsafe.As<int, bool>(ref this.m13);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M14 => ref Unsafe.As<int, bool>(ref this.m14);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M23 => ref Unsafe.As<int, bool>(ref this.m23);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M24 => ref Unsafe.As<int, bool>(ref this.m24);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M31 => ref Unsafe.As<int, bool>(ref this.m31);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M32 => ref Unsafe.As<int, bool>(ref this.m32);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M33 => ref Unsafe.As<int, bool>(ref this.m33);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M34 => ref Unsafe.As<int, bool>(ref this.m34);

    /// <summary>
    /// Creates a new <see cref="Bool3x4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool3x4"/> instance.</param>
    public static implicit operator Bool3x4(bool x)
    {
        Bool3x4 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m13 = x ? 1 : 0;
        matrix.m14 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;
        matrix.m23 = x ? 1 : 0;
        matrix.m24 = x ? 1 : 0;
        matrix.m31 = x ? 1 : 0;
        matrix.m32 = x ? 1 : 0;
        matrix.m33 = x ? 1 : 0;
        matrix.m34 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool3x4"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator !(Bool3x4 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool3x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x4"/> value to and.</param>
    /// <param name="right">The <see cref="Bool3x4"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator &(Bool3x4 left, Bool3x4 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool3x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x4"/> value to or.</param>
    /// <param name="right">The <see cref="Bool3x4"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator |(Bool3x4 left, Bool3x4 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool3x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool3x4"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool3x4"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator ^(Bool3x4 left, Bool3x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool3x4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool3x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool3x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator ==(Bool3x4 left, Bool3x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool3x4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool3x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool3x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator !=(Bool3x4 left, Bool3x4 right) => default;
}

/// <inheritdoc cref="Bool4x1"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
public unsafe partial struct Bool4x1
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m21;

    [FieldOffset(8)]
    private int m31;

    [FieldOffset(12)]
    private int m41;

    /// <summary>
    /// Creates a new <see cref="Bool4x1"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m41">The value to assign to the component at position [4, 1].</param>
    public Bool4x1(bool m11, bool m21, bool m31, bool m41)
    {
        this.m11 = m11 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m31 = m31 ? 1 : 0;
        this.m41 = m41 ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool4x1"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref bool this[int row] => ref *(bool*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M31 => ref Unsafe.As<int, bool>(ref this.m31);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M41 => ref Unsafe.As<int, bool>(ref this.m41);

    /// <summary>
    /// Creates a new <see cref="Bool4x1"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool4x1"/> instance.</param>
    public static implicit operator Bool4x1(bool x)
    {
        Bool4x1 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m31 = x ? 1 : 0;
        matrix.m41 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool4x1"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator !(Bool4x1 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool4x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x1"/> value to and.</param>
    /// <param name="right">The <see cref="Bool4x1"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator &(Bool4x1 left, Bool4x1 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool4x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x1"/> value to or.</param>
    /// <param name="right">The <see cref="Bool4x1"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator |(Bool4x1 left, Bool4x1 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool4x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x1"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool4x1"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator ^(Bool4x1 left, Bool4x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool4x1"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool4x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool4x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator ==(Bool4x1 left, Bool4x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool4x1"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool4x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool4x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator !=(Bool4x1 left, Bool4x1 right) => default;

    /// <summary>
    /// Casts a <see cref="Bool4x1"/> value to a <see cref="Bool4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Bool4x1"/> value to cast.</param>
    public static implicit operator Bool4(Bool4x1 matrix) => *(Bool4*)&matrix;
}

/// <inheritdoc cref="Bool4x2"/>
[StructLayout(LayoutKind.Explicit, Size = 32, Pack = 4)]
public unsafe partial struct Bool4x2
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m21;

    [FieldOffset(12)]
    private int m22;

    [FieldOffset(16)]
    private int m31;

    [FieldOffset(20)]
    private int m32;

    [FieldOffset(24)]
    private int m41;

    [FieldOffset(28)]
    private int m42;

    /// <summary>
    /// Creates a new <see cref="Bool4x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m32">The value to assign to the component at position [3, 2].</param>
    /// <param name="m41">The value to assign to the component at position [4, 1].</param>
    /// <param name="m42">The value to assign to the component at position [4, 2].</param>
    public Bool4x2(bool m11, bool m12, bool m21, bool m22, bool m31, bool m32, bool m41, bool m42)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
        this.m31 = m31 ? 1 : 0;
        this.m32 = m32 ? 1 : 0;
        this.m41 = m41 ? 1 : 0;
        this.m42 = m42 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    /// <param name="row4">The value to assign to the row at position [4].</param>
    public Bool4x2(Bool2 row1, Bool2 row2, Bool2 row3, Bool2 row4)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
        this.m31 = row3.X ? 1 : 0;
        this.m32 = row3.Y ? 1 : 0;
        this.m41 = row4.X ? 1 : 0;
        this.m42 = row4.Y ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool4x2"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[int row] => ref *(Bool2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M31 => ref Unsafe.As<int, bool>(ref this.m31);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M32 => ref Unsafe.As<int, bool>(ref this.m32);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M41 => ref Unsafe.As<int, bool>(ref this.m41);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M42 => ref Unsafe.As<int, bool>(ref this.m42);

    /// <summary>
    /// Creates a new <see cref="Bool4x2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool4x2"/> instance.</param>
    public static implicit operator Bool4x2(bool x)
    {
        Bool4x2 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;
        matrix.m31 = x ? 1 : 0;
        matrix.m32 = x ? 1 : 0;
        matrix.m41 = x ? 1 : 0;
        matrix.m42 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool4x2"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator !(Bool4x2 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool4x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x2"/> value to and.</param>
    /// <param name="right">The <see cref="Bool4x2"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator &(Bool4x2 left, Bool4x2 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool4x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x2"/> value to or.</param>
    /// <param name="right">The <see cref="Bool4x2"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator |(Bool4x2 left, Bool4x2 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool4x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x2"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool4x2"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator ^(Bool4x2 left, Bool4x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool4x2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool4x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool4x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator ==(Bool4x2 left, Bool4x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool4x2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool4x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool4x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator !=(Bool4x2 left, Bool4x2 right) => default;
}

/// <inheritdoc cref="Bool4x3"/>
[StructLayout(LayoutKind.Explicit, Size = 48, Pack = 4)]
public unsafe partial struct Bool4x3
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    [FieldOffset(12)]
    private int m21;

    [FieldOffset(16)]
    private int m22;

    [FieldOffset(20)]
    private int m23;

    [FieldOffset(24)]
    private int m31;

    [FieldOffset(28)]
    private int m32;

    [FieldOffset(32)]
    private int m33;

    [FieldOffset(36)]
    private int m41;

    [FieldOffset(40)]
    private int m42;

    [FieldOffset(44)]
    private int m43;

    /// <summary>
    /// Creates a new <see cref="Bool4x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m23">The value to assign to the component at position [2, 3].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m32">The value to assign to the component at position [3, 2].</param>
    /// <param name="m33">The value to assign to the component at position [3, 3].</param>
    /// <param name="m41">The value to assign to the component at position [4, 1].</param>
    /// <param name="m42">The value to assign to the component at position [4, 2].</param>
    /// <param name="m43">The value to assign to the component at position [4, 3].</param>
    public Bool4x3(bool m11, bool m12, bool m13, bool m21, bool m22, bool m23, bool m31, bool m32, bool m33, bool m41, bool m42, bool m43)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m13 = m13 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
        this.m23 = m23 ? 1 : 0;
        this.m31 = m31 ? 1 : 0;
        this.m32 = m32 ? 1 : 0;
        this.m33 = m33 ? 1 : 0;
        this.m41 = m41 ? 1 : 0;
        this.m42 = m42 ? 1 : 0;
        this.m43 = m43 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    /// <param name="row4">The value to assign to the row at position [4].</param>
    public Bool4x3(Bool3 row1, Bool3 row2, Bool3 row3, Bool3 row4)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m13 = row1.Z ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
        this.m23 = row2.Z ? 1 : 0;
        this.m31 = row3.X ? 1 : 0;
        this.m32 = row3.Y ? 1 : 0;
        this.m33 = row3.Z ? 1 : 0;
        this.m41 = row4.X ? 1 : 0;
        this.m42 = row4.Y ? 1 : 0;
        this.m43 = row4.Z ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool4x3"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[int row] => ref *(Bool3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M13 => ref Unsafe.As<int, bool>(ref this.m13);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M23 => ref Unsafe.As<int, bool>(ref this.m23);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M31 => ref Unsafe.As<int, bool>(ref this.m31);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M32 => ref Unsafe.As<int, bool>(ref this.m32);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M33 => ref Unsafe.As<int, bool>(ref this.m33);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M41 => ref Unsafe.As<int, bool>(ref this.m41);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M42 => ref Unsafe.As<int, bool>(ref this.m42);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M43 => ref Unsafe.As<int, bool>(ref this.m43);

    /// <summary>
    /// Creates a new <see cref="Bool4x3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool4x3"/> instance.</param>
    public static implicit operator Bool4x3(bool x)
    {
        Bool4x3 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m13 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;
        matrix.m23 = x ? 1 : 0;
        matrix.m31 = x ? 1 : 0;
        matrix.m32 = x ? 1 : 0;
        matrix.m33 = x ? 1 : 0;
        matrix.m41 = x ? 1 : 0;
        matrix.m42 = x ? 1 : 0;
        matrix.m43 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool4x3"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator !(Bool4x3 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool4x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x3"/> value to and.</param>
    /// <param name="right">The <see cref="Bool4x3"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator &(Bool4x3 left, Bool4x3 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool4x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x3"/> value to or.</param>
    /// <param name="right">The <see cref="Bool4x3"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator |(Bool4x3 left, Bool4x3 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool4x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x3"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool4x3"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator ^(Bool4x3 left, Bool4x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool4x3"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool4x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool4x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator ==(Bool4x3 left, Bool4x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool4x3"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool4x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool4x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator !=(Bool4x3 left, Bool4x3 right) => default;
}

/// <inheritdoc cref="Bool4x4"/>
[StructLayout(LayoutKind.Explicit, Size = 64, Pack = 4)]
public unsafe partial struct Bool4x4
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    [FieldOffset(12)]
    private int m14;

    [FieldOffset(16)]
    private int m21;

    [FieldOffset(20)]
    private int m22;

    [FieldOffset(24)]
    private int m23;

    [FieldOffset(28)]
    private int m24;

    [FieldOffset(32)]
    private int m31;

    [FieldOffset(36)]
    private int m32;

    [FieldOffset(40)]
    private int m33;

    [FieldOffset(44)]
    private int m34;

    [FieldOffset(48)]
    private int m41;

    [FieldOffset(52)]
    private int m42;

    [FieldOffset(56)]
    private int m43;

    [FieldOffset(60)]
    private int m44;

    /// <summary>
    /// Creates a new <see cref="Bool4x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m14">The value to assign to the component at position [1, 4].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m23">The value to assign to the component at position [2, 3].</param>
    /// <param name="m24">The value to assign to the component at position [2, 4].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m32">The value to assign to the component at position [3, 2].</param>
    /// <param name="m33">The value to assign to the component at position [3, 3].</param>
    /// <param name="m34">The value to assign to the component at position [3, 4].</param>
    /// <param name="m41">The value to assign to the component at position [4, 1].</param>
    /// <param name="m42">The value to assign to the component at position [4, 2].</param>
    /// <param name="m43">The value to assign to the component at position [4, 3].</param>
    /// <param name="m44">The value to assign to the component at position [4, 4].</param>
    public Bool4x4(bool m11, bool m12, bool m13, bool m14, bool m21, bool m22, bool m23, bool m24, bool m31, bool m32, bool m33, bool m34, bool m41, bool m42, bool m43, bool m44)
    {
        this.m11 = m11 ? 1 : 0;
        this.m12 = m12 ? 1 : 0;
        this.m13 = m13 ? 1 : 0;
        this.m14 = m14 ? 1 : 0;
        this.m21 = m21 ? 1 : 0;
        this.m22 = m22 ? 1 : 0;
        this.m23 = m23 ? 1 : 0;
        this.m24 = m24 ? 1 : 0;
        this.m31 = m31 ? 1 : 0;
        this.m32 = m32 ? 1 : 0;
        this.m33 = m33 ? 1 : 0;
        this.m34 = m34 ? 1 : 0;
        this.m41 = m41 ? 1 : 0;
        this.m42 = m42 ? 1 : 0;
        this.m43 = m43 ? 1 : 0;
        this.m44 = m44 ? 1 : 0;
    }

    /// <summary>
    /// Creates a new <see cref="Bool4x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    /// <param name="row4">The value to assign to the row at position [4].</param>
    public Bool4x4(Bool4 row1, Bool4 row2, Bool4 row3, Bool4 row4)
    {
        this.m11 = row1.X ? 1 : 0;
        this.m12 = row1.Y ? 1 : 0;
        this.m13 = row1.Z ? 1 : 0;
        this.m14 = row1.W ? 1 : 0;
        this.m21 = row2.X ? 1 : 0;
        this.m22 = row2.Y ? 1 : 0;
        this.m23 = row2.Z ? 1 : 0;
        this.m24 = row2.W ? 1 : 0;
        this.m31 = row3.X ? 1 : 0;
        this.m32 = row3.Y ? 1 : 0;
        this.m33 = row3.Z ? 1 : 0;
        this.m34 = row3.W ? 1 : 0;
        this.m41 = row4.X ? 1 : 0;
        this.m42 = row4.Y ? 1 : 0;
        this.m43 = row4.Z ? 1 : 0;
        this.m44 = row4.W ? 1 : 0;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Bool4x4"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool4 this[int row] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <param name="xy3">The identifier of the fourth item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>s
    [UnscopedRef]
    public ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M11 => ref Unsafe.As<int, bool>(ref this.m11);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M12 => ref Unsafe.As<int, bool>(ref this.m12);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M13 => ref Unsafe.As<int, bool>(ref this.m13);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M14 => ref Unsafe.As<int, bool>(ref this.m14);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M21 => ref Unsafe.As<int, bool>(ref this.m21);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M22 => ref Unsafe.As<int, bool>(ref this.m22);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M23 => ref Unsafe.As<int, bool>(ref this.m23);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M24 => ref Unsafe.As<int, bool>(ref this.m24);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M31 => ref Unsafe.As<int, bool>(ref this.m31);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M32 => ref Unsafe.As<int, bool>(ref this.m32);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M33 => ref Unsafe.As<int, bool>(ref this.m33);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M34 => ref Unsafe.As<int, bool>(ref this.m34);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 1].
    /// </summary>
    [UnscopedRef]
    public ref bool M41 => ref Unsafe.As<int, bool>(ref this.m41);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 2].
    /// </summary>
    [UnscopedRef]
    public ref bool M42 => ref Unsafe.As<int, bool>(ref this.m42);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 3].
    /// </summary>
    [UnscopedRef]
    public ref bool M43 => ref Unsafe.As<int, bool>(ref this.m43);

    /// <summary>
    /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 4].
    /// </summary>
    [UnscopedRef]
    public ref bool M44 => ref Unsafe.As<int, bool>(ref this.m44);

    /// <summary>
    /// Creates a new <see cref="Bool4x4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Bool4x4"/> instance.</param>
    public static implicit operator Bool4x4(bool x)
    {
        Bool4x4 matrix;

        matrix.m11 = x ? 1 : 0;
        matrix.m12 = x ? 1 : 0;
        matrix.m13 = x ? 1 : 0;
        matrix.m14 = x ? 1 : 0;
        matrix.m21 = x ? 1 : 0;
        matrix.m22 = x ? 1 : 0;
        matrix.m23 = x ? 1 : 0;
        matrix.m24 = x ? 1 : 0;
        matrix.m31 = x ? 1 : 0;
        matrix.m32 = x ? 1 : 0;
        matrix.m33 = x ? 1 : 0;
        matrix.m34 = x ? 1 : 0;
        matrix.m41 = x ? 1 : 0;
        matrix.m42 = x ? 1 : 0;
        matrix.m43 = x ? 1 : 0;
        matrix.m44 = x ? 1 : 0;

        return matrix;
    }

    /// <summary>
    /// Negates a <see cref="Bool4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Bool4x4"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator !(Bool4x4 matrix) => default;

    /// <summary>
    /// Ands two <see cref="Bool4x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x4"/> value to and.</param>
    /// <param name="right">The <see cref="Bool4x4"/> value to combine.</param>
    /// <returns>The result of performing the and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator &(Bool4x4 left, Bool4x4 right) => default;

    /// <summary>
    /// Ors two <see cref="Bool4x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x4"/> value to or.</param>
    /// <param name="right">The <see cref="Bool4x4"/> value to combine.</param>
    /// <returns>The result of performing the or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator |(Bool4x4 left, Bool4x4 right) => default;

    /// <summary>
    /// Xors two <see cref="Bool4x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Bool4x4"/> value to xor.</param>
    /// <param name="right">The <see cref="Bool4x4"/> value to combine.</param>
    /// <returns>The result of performing the xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator ^(Bool4x4 left, Bool4x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool4x4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool4x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool4x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator ==(Bool4x4 left, Bool4x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Bool4x4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Bool4x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Bool4x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator !=(Bool4x4 left, Bool4x4 right) => default;
}
