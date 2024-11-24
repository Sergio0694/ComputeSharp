using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Intrinsics;

#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <inheritdoc cref="Int1x1"/>
[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
public unsafe partial struct Int1x1
{
    [FieldOffset(0)]
    private int m11;

    /// <summary>
    /// Creates a new <see cref="Int1x1"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    public Int1x1(int m11)
    {
        this.m11 = m11;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int1x1"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref int this[int row] => ref *(int*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x1"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Creates a new <see cref="Int1x1"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int1x1"/> instance.</param>
    public static implicit operator Int1x1(int x)
    {
        Int1x1 matrix;

        matrix.m11 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int1x1"/> value to a <see cref="Float1x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float1x1(Int1x1 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int1x1"/> value to a <see cref="Double1x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double1x1(Int1x1 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int1x1"/> value to a <see cref="UInt1x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt1x1(Int1x1 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator -(Int1x1 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int1x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator +(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Divides two <see cref="Int1x1"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator /(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int1x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator %(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int1x1"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator *(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int1x1"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator *(Int1x1 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int1x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int1x1"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator *(int left, Int1x1 right) => default;

    /// <summary>
    /// Subtracts two <see cref="Int1x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator -(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x1"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator >(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x1"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator >=(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x1"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator <(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x1"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator <=(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator ~(Int1x1 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator >>(Int1x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator >>(Int1x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator >>(Int1x1 matrix, Int1x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator >>(Int1x1 matrix, UInt1x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator >>>(Int1x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator >>>(Int1x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator >>>(Int1x1 matrix, Int1x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator >>>(Int1x1 matrix, UInt1x1 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator <<(Int1x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator <<(Int1x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator <<(Int1x1 matrix, Int1x1 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator <<(Int1x1 matrix, UInt1x1 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator &(Int1x1 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator &(Int1x1 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int1x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator &(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt1x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator &(Int1x1 left, UInt1x1 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator |(Int1x1 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator |(Int1x1 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int1x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator |(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt1x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator |(Int1x1 left, UInt1x1 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator ^(Int1x1 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator ^(Int1x1 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int1x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator ^(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt1x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x1 operator ^(Int1x1 left, UInt1x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x1"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator ==(Int1x1 left, Int1x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x1"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x1 operator !=(Int1x1 left, Int1x1 right) => default;
}

/// <inheritdoc cref="Int1x2"/>
[StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
public unsafe partial struct Int1x2
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    /// <summary>
    /// Creates a new <see cref="Int1x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    public Int1x2(int m11, int m12)
    {
        this.m11 = m11;
        this.m12 = m12;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int1x2"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[int row] => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x2"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Creates a new <see cref="Int1x2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int1x2"/> instance.</param>
    public static implicit operator Int1x2(int x)
    {
        Int1x2 matrix;

        matrix.m11 = x;
        matrix.m12 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int1x2"/> value to a <see cref="Float1x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float1x2(Int1x2 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int1x2"/> value to a <see cref="Double1x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double1x2(Int1x2 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int1x2"/> value to a <see cref="UInt1x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt1x2(Int1x2 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator -(Int1x2 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int1x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator +(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Divides two <see cref="Int1x2"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator /(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int1x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator %(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int1x2"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator *(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int1x2"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator *(Int1x2 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int1x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int1x2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator *(int left, Int1x2 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x2 input value.</param>
    /// <param name="y">The second Int2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x2, Int2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static int operator *(Int1x2 x, Int2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x1 input value.</param>
    /// <param name="y">The second Int1x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x1, Int1x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x2 operator *(Int1x1 x, Int1x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x2 input value.</param>
    /// <param name="y">The second Int2x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x2, Int2x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x1 operator *(Int1x2 x, Int2x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x2 input value.</param>
    /// <param name="y">The second Int2x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x2, Int2x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x2 operator *(Int1x2 x, Int2x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x2 input value.</param>
    /// <param name="y">The second Int2x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x2, Int2x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x3 operator *(Int1x2 x, Int2x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x2 input value.</param>
    /// <param name="y">The second Int2x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x2, Int2x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x4 operator *(Int1x2 x, Int2x4 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int1x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator -(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x2"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator >(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x2"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator >=(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x2"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator <(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x2"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator <=(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator ~(Int1x2 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator >>(Int1x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator >>(Int1x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator >>(Int1x2 matrix, Int1x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator >>(Int1x2 matrix, UInt1x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator >>>(Int1x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator >>>(Int1x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator >>>(Int1x2 matrix, Int1x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator >>>(Int1x2 matrix, UInt1x2 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator <<(Int1x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator <<(Int1x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator <<(Int1x2 matrix, Int1x2 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator <<(Int1x2 matrix, UInt1x2 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator &(Int1x2 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator &(Int1x2 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int1x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator &(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt1x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator &(Int1x2 left, UInt1x2 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator |(Int1x2 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator |(Int1x2 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int1x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator |(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt1x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator |(Int1x2 left, UInt1x2 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator ^(Int1x2 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator ^(Int1x2 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int1x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator ^(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt1x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x2 operator ^(Int1x2 left, UInt1x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator ==(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x2 operator !=(Int1x2 left, Int1x2 right) => default;

    /// <summary>
    /// Casts a <see cref="Int2"/> value to a <see cref="Int1x2"/> one.
    /// </summary>
    /// <param name="vector">The input <see cref="Int2"/> value to cast.</param>
    public static implicit operator Int1x2(Int2 vector) => *(Int1x2*)&vector;
}

/// <inheritdoc cref="Int1x3"/>
[StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
public unsafe partial struct Int1x3
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m12;

    [FieldOffset(8)]
    private int m13;

    /// <summary>
    /// Creates a new <see cref="Int1x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    public Int1x3(int m11, int m12, int m13)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int1x3"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[int row] => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x3"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M13 => ref this.m13;

    /// <summary>
    /// Creates a new <see cref="Int1x3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int1x3"/> instance.</param>
    public static implicit operator Int1x3(int x)
    {
        Int1x3 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m13 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int1x3"/> value to a <see cref="Float1x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float1x3(Int1x3 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int1x3"/> value to a <see cref="Double1x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double1x3(Int1x3 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int1x3"/> value to a <see cref="UInt1x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt1x3(Int1x3 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator -(Int1x3 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int1x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator +(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Divides two <see cref="Int1x3"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator /(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int1x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator %(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int1x3"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator *(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int1x3"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator *(Int1x3 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int1x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int1x3"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator *(int left, Int1x3 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x3 input value.</param>
    /// <param name="y">The second Int3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x3, Int3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static int operator *(Int1x3 x, Int3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x1 input value.</param>
    /// <param name="y">The second Int1x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x1, Int1x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x3 operator *(Int1x1 x, Int1x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x3 input value.</param>
    /// <param name="y">The second Int3x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x3, Int3x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x1 operator *(Int1x3 x, Int3x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x3 input value.</param>
    /// <param name="y">The second Int3x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x3, Int3x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x2 operator *(Int1x3 x, Int3x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x3 input value.</param>
    /// <param name="y">The second Int3x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x3, Int3x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x3 operator *(Int1x3 x, Int3x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x3 input value.</param>
    /// <param name="y">The second Int3x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x3, Int3x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x4 operator *(Int1x3 x, Int3x4 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int1x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator -(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x3"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator >(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x3"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator >=(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x3"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator <(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x3"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator <=(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator ~(Int1x3 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator >>(Int1x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator >>(Int1x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator >>(Int1x3 matrix, Int1x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator >>(Int1x3 matrix, UInt1x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator >>>(Int1x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator >>>(Int1x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator >>>(Int1x3 matrix, Int1x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator >>>(Int1x3 matrix, UInt1x3 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator <<(Int1x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator <<(Int1x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator <<(Int1x3 matrix, Int1x3 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator <<(Int1x3 matrix, UInt1x3 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator &(Int1x3 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator &(Int1x3 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int1x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator &(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt1x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator &(Int1x3 left, UInt1x3 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator |(Int1x3 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator |(Int1x3 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int1x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator |(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt1x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator |(Int1x3 left, UInt1x3 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator ^(Int1x3 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator ^(Int1x3 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int1x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator ^(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt1x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x3 operator ^(Int1x3 left, UInt1x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x3"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator ==(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x3"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x3 operator !=(Int1x3 left, Int1x3 right) => default;

    /// <summary>
    /// Casts a <see cref="Int3"/> value to a <see cref="Int1x3"/> one.
    /// </summary>
    /// <param name="vector">The input <see cref="Int3"/> value to cast.</param>
    public static implicit operator Int1x3(Int3 vector) => *(Int1x3*)&vector;
}

/// <inheritdoc cref="Int1x4"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
public unsafe partial struct Int1x4
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
    /// Creates a new <see cref="Int1x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m14">The value to assign to the component at position [1, 4].</param>
    public Int1x4(int m11, int m12, int m13, int m14)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
        this.m14 = m14;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int1x4"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int4 this[int row] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x4"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M13 => ref this.m13;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M14 => ref this.m14;

    /// <summary>
    /// Creates a new <see cref="Int1x4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int1x4"/> instance.</param>
    public static implicit operator Int1x4(int x)
    {
        Int1x4 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m13 = x;
        matrix.m14 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int1x4"/> value to a <see cref="Float1x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float1x4(Int1x4 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int1x4"/> value to a <see cref="Double1x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double1x4(Int1x4 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int1x4"/> value to a <see cref="UInt1x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int1x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt1x4(Int1x4 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator -(Int1x4 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int1x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator +(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Divides two <see cref="Int1x4"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator /(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int1x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator %(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int1x4"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator *(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int1x4"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator *(Int1x4 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int1x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int1x4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator *(int left, Int1x4 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x4 input value.</param>
    /// <param name="y">The second Int4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x4, Int4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static int operator *(Int1x4 x, Int4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x1 input value.</param>
    /// <param name="y">The second Int1x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x1, Int1x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x4 operator *(Int1x1 x, Int1x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x4 input value.</param>
    /// <param name="y">The second Int4x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x4, Int4x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x1 operator *(Int1x4 x, Int4x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x4 input value.</param>
    /// <param name="y">The second Int4x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x4, Int4x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x2 operator *(Int1x4 x, Int4x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x4 input value.</param>
    /// <param name="y">The second Int4x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x4, Int4x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x3 operator *(Int1x4 x, Int4x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int1x4 input value.</param>
    /// <param name="y">The second Int4x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int1x4, Int4x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int1x4 operator *(Int1x4 x, Int4x4 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int1x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator -(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x4"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator >(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x4"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator >=(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x4"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator <(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x4"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator <=(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator ~(Int1x4 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator >>(Int1x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator >>(Int1x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator >>(Int1x4 matrix, Int1x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator >>(Int1x4 matrix, UInt1x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator >>>(Int1x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator >>>(Int1x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator >>>(Int1x4 matrix, Int1x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int1x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator >>>(Int1x4 matrix, UInt1x4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator <<(Int1x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator <<(Int1x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator <<(Int1x4 matrix, Int1x4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int1x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator <<(Int1x4 matrix, UInt1x4 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator &(Int1x4 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator &(Int1x4 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int1x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator &(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt1x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator &(Int1x4 left, UInt1x4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator |(Int1x4 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator |(Int1x4 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int1x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator |(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt1x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator |(Int1x4 left, UInt1x4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator ^(Int1x4 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator ^(Int1x4 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int1x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator ^(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int1x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int1x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt1x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int1x4 operator ^(Int1x4 left, UInt1x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator ==(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int1x4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int1x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int1x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool1x4 operator !=(Int1x4 left, Int1x4 right) => default;

    /// <summary>
    /// Casts a <see cref="Int4"/> value to a <see cref="Int1x4"/> one.
    /// </summary>
    /// <param name="vector">The input <see cref="Int4"/> value to cast.</param>
    public static implicit operator Int1x4(Int4 vector) => *(Int1x4*)&vector;
}

/// <inheritdoc cref="Int2x1"/>
[StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
public unsafe partial struct Int2x1
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m21;

    /// <summary>
    /// Creates a new <see cref="Int2x1"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    public Int2x1(int m11, int m21)
    {
        this.m11 = m11;
        this.m21 = m21;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int2x1"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref int this[int row] => ref *(int*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x1"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Creates a new <see cref="Int2x1"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int2x1"/> instance.</param>
    public static implicit operator Int2x1(int x)
    {
        Int2x1 matrix;

        matrix.m11 = x;
        matrix.m21 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int2x1"/> value to a <see cref="Float2x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float2x1(Int2x1 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int2x1"/> value to a <see cref="Double2x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double2x1(Int2x1 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int2x1"/> value to a <see cref="UInt2x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt2x1(Int2x1 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator -(Int2x1 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int2x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator +(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Divides two <see cref="Int2x1"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator /(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int2x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator %(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int2x1"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator *(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int2x1"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator *(Int2x1 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int2x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int2x1"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator *(int left, Int2x1 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x1 input value.</param>
    /// <param name="y">The second Int1x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x1, Int1x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x1 operator *(Int2x1 x, Int1x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x1 input value.</param>
    /// <param name="y">The second Int1x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x1, Int1x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x2 operator *(Int2x1 x, Int1x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x1 input value.</param>
    /// <param name="y">The second Int1x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x1, Int1x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x3 operator *(Int2x1 x, Int1x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x1 input value.</param>
    /// <param name="y">The second Int1x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x1, Int1x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x4 operator *(Int2x1 x, Int1x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x2 input value.</param>
    /// <param name="y">The second Int2x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x2, Int2x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x1 operator *(Int2x2 x, Int2x1 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int2x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator -(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x1"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator >(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x1"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator >=(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x1"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator <(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x1"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator <=(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator ~(Int2x1 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator >>(Int2x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator >>(Int2x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator >>(Int2x1 matrix, Int2x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator >>(Int2x1 matrix, UInt2x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator >>>(Int2x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator >>>(Int2x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator >>>(Int2x1 matrix, Int2x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator >>>(Int2x1 matrix, UInt2x1 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator <<(Int2x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator <<(Int2x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator <<(Int2x1 matrix, Int2x1 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator <<(Int2x1 matrix, UInt2x1 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator &(Int2x1 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator &(Int2x1 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int2x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator &(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt2x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator &(Int2x1 left, UInt2x1 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator |(Int2x1 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator |(Int2x1 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int2x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator |(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt2x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator |(Int2x1 left, UInt2x1 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator ^(Int2x1 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator ^(Int2x1 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int2x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator ^(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt2x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x1 operator ^(Int2x1 left, UInt2x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x1"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator ==(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x1"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x1 operator !=(Int2x1 left, Int2x1 right) => default;

    /// <summary>
    /// Casts a <see cref="Int2x1"/> value to a <see cref="Int2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x1"/> value to cast.</param>
    public static implicit operator Int2(Int2x1 matrix) => *(Int2*)&matrix;
}

/// <inheritdoc cref="Int2x2"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
public unsafe partial struct Int2x2
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
    /// Creates a new <see cref="Int2x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    public Int2x2(int m11, int m12, int m21, int m22)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m21 = m21;
        this.m22 = m22;
    }

    /// <summary>
    /// Creates a new <see cref="Int2x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    public Int2x2(Int2 row1, Int2 row2)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m21 = row2.X;
        this.m22 = row2.Y;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int2x2"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[int row] => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x2"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Creates a new <see cref="Int2x2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int2x2"/> instance.</param>
    public static implicit operator Int2x2(int x)
    {
        Int2x2 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m21 = x;
        matrix.m22 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int2x2"/> value to a <see cref="Float2x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float2x2(Int2x2 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int2x2"/> value to a <see cref="Double2x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double2x2(Int2x2 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int2x2"/> value to a <see cref="UInt2x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt2x2(Int2x2 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator -(Int2x2 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int2x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator +(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Divides two <see cref="Int2x2"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator /(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int2x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator %(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int2x2"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator *(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int2x2"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator *(Int2x2 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int2x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int2x2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator *(int left, Int2x2 right) => default;

    /// <summary>
    /// Subtracts two <see cref="Int2x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator -(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x2"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator >(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x2"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator >=(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x2"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator <(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x2"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator <=(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator ~(Int2x2 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator >>(Int2x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator >>(Int2x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator >>(Int2x2 matrix, Int2x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator >>(Int2x2 matrix, UInt2x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator >>>(Int2x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator >>>(Int2x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator >>>(Int2x2 matrix, Int2x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator >>>(Int2x2 matrix, UInt2x2 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator <<(Int2x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator <<(Int2x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator <<(Int2x2 matrix, Int2x2 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator <<(Int2x2 matrix, UInt2x2 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator &(Int2x2 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator &(Int2x2 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int2x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator &(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt2x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator &(Int2x2 left, UInt2x2 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator |(Int2x2 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator |(Int2x2 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int2x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator |(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt2x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator |(Int2x2 left, UInt2x2 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator ^(Int2x2 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator ^(Int2x2 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int2x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator ^(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt2x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x2 operator ^(Int2x2 left, UInt2x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator ==(Int2x2 left, Int2x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x2 operator !=(Int2x2 left, Int2x2 right) => default;
}

/// <inheritdoc cref="Int2x3"/>
[StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
public unsafe partial struct Int2x3
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
    /// Creates a new <see cref="Int2x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m23">The value to assign to the component at position [2, 3].</param>
    public Int2x3(int m11, int m12, int m13, int m21, int m22, int m23)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
        this.m21 = m21;
        this.m22 = m22;
        this.m23 = m23;
    }

    /// <summary>
    /// Creates a new <see cref="Int2x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    public Int2x3(Int3 row1, Int3 row2)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m13 = row1.Z;
        this.m21 = row2.X;
        this.m22 = row2.Y;
        this.m23 = row2.Z;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int2x3"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[int row] => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x3"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M13 => ref this.m13;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M23 => ref this.m23;

    /// <summary>
    /// Creates a new <see cref="Int2x3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int2x3"/> instance.</param>
    public static implicit operator Int2x3(int x)
    {
        Int2x3 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m13 = x;
        matrix.m21 = x;
        matrix.m22 = x;
        matrix.m23 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int2x3"/> value to a <see cref="Float2x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float2x3(Int2x3 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int2x3"/> value to a <see cref="Double2x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double2x3(Int2x3 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int2x3"/> value to a <see cref="UInt2x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt2x3(Int2x3 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator -(Int2x3 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int2x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator +(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Divides two <see cref="Int2x3"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator /(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int2x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator %(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int2x3"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator *(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int2x3"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator *(Int2x3 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int2x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int2x3"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator *(int left, Int2x3 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x3 input value.</param>
    /// <param name="y">The second Int3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x3, Int3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2 operator *(Int2x3 x, Int3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x2 input value.</param>
    /// <param name="y">The second Int2x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x2, Int2x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x3 operator *(Int2x2 x, Int2x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x3 input value.</param>
    /// <param name="y">The second Int3x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x3, Int3x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x1 operator *(Int2x3 x, Int3x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x3 input value.</param>
    /// <param name="y">The second Int3x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x3, Int3x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x2 operator *(Int2x3 x, Int3x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x3 input value.</param>
    /// <param name="y">The second Int3x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x3, Int3x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x3 operator *(Int2x3 x, Int3x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x3 input value.</param>
    /// <param name="y">The second Int3x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x3, Int3x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x4 operator *(Int2x3 x, Int3x4 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int2x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator -(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x3"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator >(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x3"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator >=(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x3"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator <(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x3"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator <=(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator ~(Int2x3 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator >>(Int2x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator >>(Int2x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator >>(Int2x3 matrix, Int2x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator >>(Int2x3 matrix, UInt2x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator >>>(Int2x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator >>>(Int2x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator >>>(Int2x3 matrix, Int2x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator >>>(Int2x3 matrix, UInt2x3 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator <<(Int2x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator <<(Int2x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator <<(Int2x3 matrix, Int2x3 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator <<(Int2x3 matrix, UInt2x3 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator &(Int2x3 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator &(Int2x3 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int2x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator &(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt2x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator &(Int2x3 left, UInt2x3 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator |(Int2x3 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator |(Int2x3 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int2x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator |(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt2x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator |(Int2x3 left, UInt2x3 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator ^(Int2x3 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator ^(Int2x3 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int2x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator ^(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt2x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x3 operator ^(Int2x3 left, UInt2x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x3"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator ==(Int2x3 left, Int2x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x3"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x3 operator !=(Int2x3 left, Int2x3 right) => default;
}

/// <inheritdoc cref="Int2x4"/>
[StructLayout(LayoutKind.Explicit, Size = 32, Pack = 4)]
public unsafe partial struct Int2x4
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
    /// Creates a new <see cref="Int2x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m13">The value to assign to the component at position [1, 3].</param>
    /// <param name="m14">The value to assign to the component at position [1, 4].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m23">The value to assign to the component at position [2, 3].</param>
    /// <param name="m24">The value to assign to the component at position [2, 4].</param>
    public Int2x4(int m11, int m12, int m13, int m14, int m21, int m22, int m23, int m24)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
        this.m14 = m14;
        this.m21 = m21;
        this.m22 = m22;
        this.m23 = m23;
        this.m24 = m24;
    }

    /// <summary>
    /// Creates a new <see cref="Int2x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    public Int2x4(Int4 row1, Int4 row2)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m13 = row1.Z;
        this.m14 = row1.W;
        this.m21 = row2.X;
        this.m22 = row2.Y;
        this.m23 = row2.Z;
        this.m24 = row2.W;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int2x4"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int4 this[int row] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x4"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M13 => ref this.m13;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M14 => ref this.m14;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M23 => ref this.m23;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M24 => ref this.m24;

    /// <summary>
    /// Creates a new <see cref="Int2x4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int2x4"/> instance.</param>
    public static implicit operator Int2x4(int x)
    {
        Int2x4 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m13 = x;
        matrix.m14 = x;
        matrix.m21 = x;
        matrix.m22 = x;
        matrix.m23 = x;
        matrix.m24 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int2x4"/> value to a <see cref="Float2x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float2x4(Int2x4 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int2x4"/> value to a <see cref="Double2x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double2x4(Int2x4 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int2x4"/> value to a <see cref="UInt2x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int2x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt2x4(Int2x4 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator -(Int2x4 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int2x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator +(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Divides two <see cref="Int2x4"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator /(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int2x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator %(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int2x4"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator *(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int2x4"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator *(Int2x4 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int2x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int2x4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator *(int left, Int2x4 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x4 input value.</param>
    /// <param name="y">The second Int4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x4, Int4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2 operator *(Int2x4 x, Int4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x2 input value.</param>
    /// <param name="y">The second Int2x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x2, Int2x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x4 operator *(Int2x2 x, Int2x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x4 input value.</param>
    /// <param name="y">The second Int4x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x4, Int4x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x1 operator *(Int2x4 x, Int4x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x4 input value.</param>
    /// <param name="y">The second Int4x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x4, Int4x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x2 operator *(Int2x4 x, Int4x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x4 input value.</param>
    /// <param name="y">The second Int4x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x4, Int4x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x3 operator *(Int2x4 x, Int4x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int2x4 input value.</param>
    /// <param name="y">The second Int4x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int2x4, Int4x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2x4 operator *(Int2x4 x, Int4x4 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int2x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator -(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x4"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator >(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x4"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator >=(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x4"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator <(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x4"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator <=(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator ~(Int2x4 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator >>(Int2x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator >>(Int2x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator >>(Int2x4 matrix, Int2x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator >>(Int2x4 matrix, UInt2x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator >>>(Int2x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator >>>(Int2x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator >>>(Int2x4 matrix, Int2x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int2x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator >>>(Int2x4 matrix, UInt2x4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator <<(Int2x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator <<(Int2x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator <<(Int2x4 matrix, Int2x4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int2x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator <<(Int2x4 matrix, UInt2x4 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator &(Int2x4 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator &(Int2x4 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int2x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator &(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt2x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator &(Int2x4 left, UInt2x4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator |(Int2x4 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator |(Int2x4 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int2x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator |(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt2x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator |(Int2x4 left, UInt2x4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator ^(Int2x4 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator ^(Int2x4 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int2x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator ^(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int2x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int2x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt2x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int2x4 operator ^(Int2x4 left, UInt2x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator ==(Int2x4 left, Int2x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int2x4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int2x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int2x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2x4 operator !=(Int2x4 left, Int2x4 right) => default;
}

/// <inheritdoc cref="Int3x1"/>
[StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
public unsafe partial struct Int3x1
{
    [FieldOffset(0)]
    private int m11;

    [FieldOffset(4)]
    private int m21;

    [FieldOffset(8)]
    private int m31;

    /// <summary>
    /// Creates a new <see cref="Int3x1"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    public Int3x1(int m11, int m21, int m31)
    {
        this.m11 = m11;
        this.m21 = m21;
        this.m31 = m31;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int3x1"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref int this[int row] => ref *(int*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x1"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M31 => ref this.m31;

    /// <summary>
    /// Creates a new <see cref="Int3x1"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int3x1"/> instance.</param>
    public static implicit operator Int3x1(int x)
    {
        Int3x1 matrix;

        matrix.m11 = x;
        matrix.m21 = x;
        matrix.m31 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int3x1"/> value to a <see cref="Float3x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float3x1(Int3x1 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int3x1"/> value to a <see cref="Double3x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double3x1(Int3x1 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int3x1"/> value to a <see cref="UInt3x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt3x1(Int3x1 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator -(Int3x1 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int3x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator +(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Divides two <see cref="Int3x1"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator /(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int3x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator %(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int3x1"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator *(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int3x1"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator *(Int3x1 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int3x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int3x1"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator *(int left, Int3x1 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x1 input value.</param>
    /// <param name="y">The second Int1x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x1, Int1x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x1 operator *(Int3x1 x, Int1x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x1 input value.</param>
    /// <param name="y">The second Int1x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x1, Int1x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x2 operator *(Int3x1 x, Int1x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x1 input value.</param>
    /// <param name="y">The second Int1x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x1, Int1x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x3 operator *(Int3x1 x, Int1x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x1 input value.</param>
    /// <param name="y">The second Int1x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x1, Int1x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x4 operator *(Int3x1 x, Int1x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x3 input value.</param>
    /// <param name="y">The second Int3x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x3, Int3x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x1 operator *(Int3x3 x, Int3x1 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int3x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator -(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x1"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator >(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x1"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator >=(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x1"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator <(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x1"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator <=(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator ~(Int3x1 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator >>(Int3x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator >>(Int3x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator >>(Int3x1 matrix, Int3x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator >>(Int3x1 matrix, UInt3x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator >>>(Int3x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator >>>(Int3x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator >>>(Int3x1 matrix, Int3x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator >>>(Int3x1 matrix, UInt3x1 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator <<(Int3x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator <<(Int3x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator <<(Int3x1 matrix, Int3x1 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator <<(Int3x1 matrix, UInt3x1 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator &(Int3x1 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator &(Int3x1 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int3x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator &(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt3x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator &(Int3x1 left, UInt3x1 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator |(Int3x1 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator |(Int3x1 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int3x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator |(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt3x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator |(Int3x1 left, UInt3x1 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator ^(Int3x1 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator ^(Int3x1 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int3x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator ^(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt3x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x1 operator ^(Int3x1 left, UInt3x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x1"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator ==(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x1"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x1 operator !=(Int3x1 left, Int3x1 right) => default;

    /// <summary>
    /// Casts a <see cref="Int3x1"/> value to a <see cref="Int3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x1"/> value to cast.</param>
    public static implicit operator Int3(Int3x1 matrix) => *(Int3*)&matrix;
}

/// <inheritdoc cref="Int3x2"/>
[StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
public unsafe partial struct Int3x2
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
    /// Creates a new <see cref="Int3x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m32">The value to assign to the component at position [3, 2].</param>
    public Int3x2(int m11, int m12, int m21, int m22, int m31, int m32)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m21 = m21;
        this.m22 = m22;
        this.m31 = m31;
        this.m32 = m32;
    }

    /// <summary>
    /// Creates a new <see cref="Int3x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    public Int3x2(Int2 row1, Int2 row2, Int2 row3)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m21 = row2.X;
        this.m22 = row2.Y;
        this.m31 = row3.X;
        this.m32 = row3.Y;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int3x2"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[int row] => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x2"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M31 => ref this.m31;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M32 => ref this.m32;

    /// <summary>
    /// Creates a new <see cref="Int3x2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int3x2"/> instance.</param>
    public static implicit operator Int3x2(int x)
    {
        Int3x2 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m21 = x;
        matrix.m22 = x;
        matrix.m31 = x;
        matrix.m32 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int3x2"/> value to a <see cref="Float3x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float3x2(Int3x2 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int3x2"/> value to a <see cref="Double3x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double3x2(Int3x2 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int3x2"/> value to a <see cref="UInt3x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt3x2(Int3x2 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator -(Int3x2 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int3x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator +(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Divides two <see cref="Int3x2"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator /(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int3x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator %(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int3x2"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator *(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int3x2"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator *(Int3x2 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int3x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int3x2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator *(int left, Int3x2 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x2 input value.</param>
    /// <param name="y">The second Int2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x2, Int2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3 operator *(Int3x2 x, Int2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x2 input value.</param>
    /// <param name="y">The second Int2x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x2, Int2x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x1 operator *(Int3x2 x, Int2x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x2 input value.</param>
    /// <param name="y">The second Int2x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x2, Int2x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x2 operator *(Int3x2 x, Int2x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x2 input value.</param>
    /// <param name="y">The second Int2x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x2, Int2x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x3 operator *(Int3x2 x, Int2x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x2 input value.</param>
    /// <param name="y">The second Int2x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x2, Int2x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x4 operator *(Int3x2 x, Int2x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x3 input value.</param>
    /// <param name="y">The second Int3x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x3, Int3x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x2 operator *(Int3x3 x, Int3x2 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int3x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator -(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x2"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator >(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x2"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator >=(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x2"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator <(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x2"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator <=(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator ~(Int3x2 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator >>(Int3x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator >>(Int3x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator >>(Int3x2 matrix, Int3x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator >>(Int3x2 matrix, UInt3x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator >>>(Int3x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator >>>(Int3x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator >>>(Int3x2 matrix, Int3x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator >>>(Int3x2 matrix, UInt3x2 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator <<(Int3x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator <<(Int3x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator <<(Int3x2 matrix, Int3x2 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator <<(Int3x2 matrix, UInt3x2 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator &(Int3x2 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator &(Int3x2 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int3x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator &(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt3x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator &(Int3x2 left, UInt3x2 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator |(Int3x2 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator |(Int3x2 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int3x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator |(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt3x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator |(Int3x2 left, UInt3x2 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator ^(Int3x2 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator ^(Int3x2 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int3x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator ^(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt3x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x2 operator ^(Int3x2 left, UInt3x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator ==(Int3x2 left, Int3x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x2 operator !=(Int3x2 left, Int3x2 right) => default;
}

/// <inheritdoc cref="Int3x3"/>
[StructLayout(LayoutKind.Explicit, Size = 36, Pack = 4)]
public unsafe partial struct Int3x3
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
    /// Creates a new <see cref="Int3x3"/> instance with the specified parameters.
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
    public Int3x3(int m11, int m12, int m13, int m21, int m22, int m23, int m31, int m32, int m33)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
        this.m21 = m21;
        this.m22 = m22;
        this.m23 = m23;
        this.m31 = m31;
        this.m32 = m32;
        this.m33 = m33;
    }

    /// <summary>
    /// Creates a new <see cref="Int3x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    public Int3x3(Int3 row1, Int3 row2, Int3 row3)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m13 = row1.Z;
        this.m21 = row2.X;
        this.m22 = row2.Y;
        this.m23 = row2.Z;
        this.m31 = row3.X;
        this.m32 = row3.Y;
        this.m33 = row3.Z;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int3x3"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[int row] => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x3"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M13 => ref this.m13;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M23 => ref this.m23;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M31 => ref this.m31;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M32 => ref this.m32;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M33 => ref this.m33;

    /// <summary>
    /// Creates a new <see cref="Int3x3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int3x3"/> instance.</param>
    public static implicit operator Int3x3(int x)
    {
        Int3x3 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m13 = x;
        matrix.m21 = x;
        matrix.m22 = x;
        matrix.m23 = x;
        matrix.m31 = x;
        matrix.m32 = x;
        matrix.m33 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int3x3"/> value to a <see cref="Float3x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float3x3(Int3x3 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int3x3"/> value to a <see cref="Double3x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double3x3(Int3x3 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int3x3"/> value to a <see cref="UInt3x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt3x3(Int3x3 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator -(Int3x3 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int3x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator +(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Divides two <see cref="Int3x3"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator /(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int3x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator %(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int3x3"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator *(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int3x3"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator *(Int3x3 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int3x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int3x3"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator *(int left, Int3x3 right) => default;

    /// <summary>
    /// Subtracts two <see cref="Int3x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator -(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x3"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator >(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x3"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator >=(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x3"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator <(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x3"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator <=(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator ~(Int3x3 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator >>(Int3x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator >>(Int3x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator >>(Int3x3 matrix, Int3x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator >>(Int3x3 matrix, UInt3x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator >>>(Int3x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator >>>(Int3x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator >>>(Int3x3 matrix, Int3x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator >>>(Int3x3 matrix, UInt3x3 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator <<(Int3x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator <<(Int3x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator <<(Int3x3 matrix, Int3x3 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator <<(Int3x3 matrix, UInt3x3 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator &(Int3x3 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator &(Int3x3 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int3x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator &(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt3x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator &(Int3x3 left, UInt3x3 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator |(Int3x3 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator |(Int3x3 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int3x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator |(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt3x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator |(Int3x3 left, UInt3x3 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator ^(Int3x3 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator ^(Int3x3 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int3x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator ^(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt3x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x3 operator ^(Int3x3 left, UInt3x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x3"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator ==(Int3x3 left, Int3x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x3"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x3 operator !=(Int3x3 left, Int3x3 right) => default;
}

/// <inheritdoc cref="Int3x4"/>
[StructLayout(LayoutKind.Explicit, Size = 48, Pack = 4)]
public unsafe partial struct Int3x4
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
    /// Creates a new <see cref="Int3x4"/> instance with the specified parameters.
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
    public Int3x4(int m11, int m12, int m13, int m14, int m21, int m22, int m23, int m24, int m31, int m32, int m33, int m34)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
        this.m14 = m14;
        this.m21 = m21;
        this.m22 = m22;
        this.m23 = m23;
        this.m24 = m24;
        this.m31 = m31;
        this.m32 = m32;
        this.m33 = m33;
        this.m34 = m34;
    }

    /// <summary>
    /// Creates a new <see cref="Int3x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    public Int3x4(Int4 row1, Int4 row2, Int4 row3)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m13 = row1.Z;
        this.m14 = row1.W;
        this.m21 = row2.X;
        this.m22 = row2.Y;
        this.m23 = row2.Z;
        this.m24 = row2.W;
        this.m31 = row3.X;
        this.m32 = row3.Y;
        this.m33 = row3.Z;
        this.m34 = row3.W;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int3x4"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int4 this[int row] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x4"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M13 => ref this.m13;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M14 => ref this.m14;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M23 => ref this.m23;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M24 => ref this.m24;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M31 => ref this.m31;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M32 => ref this.m32;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M33 => ref this.m33;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M34 => ref this.m34;

    /// <summary>
    /// Creates a new <see cref="Int3x4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int3x4"/> instance.</param>
    public static implicit operator Int3x4(int x)
    {
        Int3x4 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m13 = x;
        matrix.m14 = x;
        matrix.m21 = x;
        matrix.m22 = x;
        matrix.m23 = x;
        matrix.m24 = x;
        matrix.m31 = x;
        matrix.m32 = x;
        matrix.m33 = x;
        matrix.m34 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int3x4"/> value to a <see cref="Float3x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float3x4(Int3x4 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int3x4"/> value to a <see cref="Double3x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double3x4(Int3x4 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int3x4"/> value to a <see cref="UInt3x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int3x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt3x4(Int3x4 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator -(Int3x4 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int3x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator +(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Divides two <see cref="Int3x4"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator /(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int3x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator %(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int3x4"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator *(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int3x4"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator *(Int3x4 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int3x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int3x4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator *(int left, Int3x4 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x4 input value.</param>
    /// <param name="y">The second Int4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x4, Int4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3 operator *(Int3x4 x, Int4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x3 input value.</param>
    /// <param name="y">The second Int3x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x3, Int3x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x4 operator *(Int3x3 x, Int3x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x4 input value.</param>
    /// <param name="y">The second Int4x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x4, Int4x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x1 operator *(Int3x4 x, Int4x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x4 input value.</param>
    /// <param name="y">The second Int4x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x4, Int4x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x2 operator *(Int3x4 x, Int4x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x4 input value.</param>
    /// <param name="y">The second Int4x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x4, Int4x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x3 operator *(Int3x4 x, Int4x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int3x4 input value.</param>
    /// <param name="y">The second Int4x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int3x4, Int4x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3x4 operator *(Int3x4 x, Int4x4 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int3x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator -(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x4"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator >(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x4"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator >=(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x4"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator <(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x4"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator <=(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator ~(Int3x4 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator >>(Int3x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator >>(Int3x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator >>(Int3x4 matrix, Int3x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator >>(Int3x4 matrix, UInt3x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator >>>(Int3x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator >>>(Int3x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator >>>(Int3x4 matrix, Int3x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int3x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator >>>(Int3x4 matrix, UInt3x4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator <<(Int3x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator <<(Int3x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator <<(Int3x4 matrix, Int3x4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int3x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator <<(Int3x4 matrix, UInt3x4 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator &(Int3x4 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator &(Int3x4 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int3x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator &(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt3x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator &(Int3x4 left, UInt3x4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator |(Int3x4 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator |(Int3x4 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int3x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator |(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt3x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator |(Int3x4 left, UInt3x4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator ^(Int3x4 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator ^(Int3x4 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int3x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator ^(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int3x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int3x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt3x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int3x4 operator ^(Int3x4 left, UInt3x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator ==(Int3x4 left, Int3x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int3x4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int3x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int3x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool3x4 operator !=(Int3x4 left, Int3x4 right) => default;
}

/// <inheritdoc cref="Int4x1"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
public unsafe partial struct Int4x1
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
    /// Creates a new <see cref="Int4x1"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m41">The value to assign to the component at position [4, 1].</param>
    public Int4x1(int m11, int m21, int m31, int m41)
    {
        this.m11 = m11;
        this.m21 = m21;
        this.m31 = m31;
        this.m41 = m41;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int4x1"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref int this[int row] => ref *(int*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x1"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x1"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M31 => ref this.m31;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M41 => ref this.m41;

    /// <summary>
    /// Creates a new <see cref="Int4x1"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int4x1"/> instance.</param>
    public static implicit operator Int4x1(int x)
    {
        Int4x1 matrix;

        matrix.m11 = x;
        matrix.m21 = x;
        matrix.m31 = x;
        matrix.m41 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int4x1"/> value to a <see cref="Float4x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float4x1(Int4x1 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int4x1"/> value to a <see cref="Double4x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double4x1(Int4x1 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int4x1"/> value to a <see cref="UInt4x1"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x1"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt4x1(Int4x1 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator -(Int4x1 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int4x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator +(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Divides two <see cref="Int4x1"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator /(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int4x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator %(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int4x1"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator *(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int4x1"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator *(Int4x1 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int4x1"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int4x1"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator *(int left, Int4x1 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x1 input value.</param>
    /// <param name="y">The second Int1x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x1, Int1x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x1 operator *(Int4x1 x, Int1x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x1 input value.</param>
    /// <param name="y">The second Int1x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x1, Int1x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x2 operator *(Int4x1 x, Int1x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x1 input value.</param>
    /// <param name="y">The second Int1x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x1, Int1x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x3 operator *(Int4x1 x, Int1x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x1 input value.</param>
    /// <param name="y">The second Int1x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x1, Int1x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x4 operator *(Int4x1 x, Int1x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x4 input value.</param>
    /// <param name="y">The second Int4x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x4, Int4x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x1 operator *(Int4x4 x, Int4x1 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int4x1"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator -(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x1"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator >(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x1"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator >=(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x1"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator <(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x1"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator <=(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator ~(Int4x1 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator >>(Int4x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator >>(Int4x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator >>(Int4x1 matrix, Int4x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator >>(Int4x1 matrix, UInt4x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator >>>(Int4x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator >>>(Int4x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator >>>(Int4x1 matrix, Int4x1 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x1"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator >>>(Int4x1 matrix, UInt4x1 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator <<(Int4x1 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator <<(Int4x1 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator <<(Int4x1 matrix, Int4x1 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x1"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator <<(Int4x1 matrix, UInt4x1 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator &(Int4x1 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator &(Int4x1 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int4x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator &(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt4x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator &(Int4x1 left, UInt4x1 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator |(Int4x1 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator |(Int4x1 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int4x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator |(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt4x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator |(Int4x1 left, UInt4x1 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator ^(Int4x1 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator ^(Int4x1 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int4x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator ^(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x1"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x1"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt4x1"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x1 operator ^(Int4x1 left, UInt4x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x1"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator ==(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x1"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x1"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x1"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x1 operator !=(Int4x1 left, Int4x1 right) => default;

    /// <summary>
    /// Casts a <see cref="Int4x1"/> value to a <see cref="Int4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x1"/> value to cast.</param>
    public static implicit operator Int4(Int4x1 matrix) => *(Int4*)&matrix;
}

/// <inheritdoc cref="Int4x2"/>
[StructLayout(LayoutKind.Explicit, Size = 32, Pack = 4)]
public unsafe partial struct Int4x2
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
    /// Creates a new <see cref="Int4x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="m11">The value to assign to the component at position [1, 1].</param>
    /// <param name="m12">The value to assign to the component at position [1, 2].</param>
    /// <param name="m21">The value to assign to the component at position [2, 1].</param>
    /// <param name="m22">The value to assign to the component at position [2, 2].</param>
    /// <param name="m31">The value to assign to the component at position [3, 1].</param>
    /// <param name="m32">The value to assign to the component at position [3, 2].</param>
    /// <param name="m41">The value to assign to the component at position [4, 1].</param>
    /// <param name="m42">The value to assign to the component at position [4, 2].</param>
    public Int4x2(int m11, int m12, int m21, int m22, int m31, int m32, int m41, int m42)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m21 = m21;
        this.m22 = m22;
        this.m31 = m31;
        this.m32 = m32;
        this.m41 = m41;
        this.m42 = m42;
    }

    /// <summary>
    /// Creates a new <see cref="Int4x2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    /// <param name="row4">The value to assign to the row at position [4].</param>
    public Int4x2(Int2 row1, Int2 row2, Int2 row3, Int2 row4)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m21 = row2.X;
        this.m22 = row2.Y;
        this.m31 = row3.X;
        this.m32 = row3.Y;
        this.m41 = row4.X;
        this.m42 = row4.Y;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int4x2"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[int row] => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x2"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x2"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M31 => ref this.m31;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M32 => ref this.m32;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M41 => ref this.m41;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M42 => ref this.m42;

    /// <summary>
    /// Creates a new <see cref="Int4x2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int4x2"/> instance.</param>
    public static implicit operator Int4x2(int x)
    {
        Int4x2 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m21 = x;
        matrix.m22 = x;
        matrix.m31 = x;
        matrix.m32 = x;
        matrix.m41 = x;
        matrix.m42 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int4x2"/> value to a <see cref="Float4x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float4x2(Int4x2 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int4x2"/> value to a <see cref="Double4x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double4x2(Int4x2 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int4x2"/> value to a <see cref="UInt4x2"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt4x2(Int4x2 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator -(Int4x2 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int4x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator +(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Divides two <see cref="Int4x2"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator /(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int4x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator %(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int4x2"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator *(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int4x2"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator *(Int4x2 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int4x2"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int4x2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator *(int left, Int4x2 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x2 input value.</param>
    /// <param name="y">The second Int2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x2, Int2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4 operator *(Int4x2 x, Int2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x2 input value.</param>
    /// <param name="y">The second Int2x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x2, Int2x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x1 operator *(Int4x2 x, Int2x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x2 input value.</param>
    /// <param name="y">The second Int2x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x2, Int2x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x2 operator *(Int4x2 x, Int2x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x2 input value.</param>
    /// <param name="y">The second Int2x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x2, Int2x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x3 operator *(Int4x2 x, Int2x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x2 input value.</param>
    /// <param name="y">The second Int2x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x2, Int2x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x4 operator *(Int4x2 x, Int2x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x4 input value.</param>
    /// <param name="y">The second Int4x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x4, Int4x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x2 operator *(Int4x4 x, Int4x2 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int4x2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator -(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x2"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator >(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x2"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator >=(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x2"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator <(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x2"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator <=(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator ~(Int4x2 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator >>(Int4x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator >>(Int4x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator >>(Int4x2 matrix, Int4x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator >>(Int4x2 matrix, UInt4x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator >>>(Int4x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator >>>(Int4x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator >>>(Int4x2 matrix, Int4x2 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x2"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator >>>(Int4x2 matrix, UInt4x2 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator <<(Int4x2 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator <<(Int4x2 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator <<(Int4x2 matrix, Int4x2 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x2"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator <<(Int4x2 matrix, UInt4x2 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator &(Int4x2 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator &(Int4x2 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int4x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator &(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt4x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator &(Int4x2 left, UInt4x2 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator |(Int4x2 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator |(Int4x2 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int4x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator |(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt4x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator |(Int4x2 left, UInt4x2 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator ^(Int4x2 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator ^(Int4x2 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int4x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator ^(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x2"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x2"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt4x2"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x2 operator ^(Int4x2 left, UInt4x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator ==(Int4x2 left, Int4x2 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x2 operator !=(Int4x2 left, Int4x2 right) => default;
}

/// <inheritdoc cref="Int4x3"/>
[StructLayout(LayoutKind.Explicit, Size = 48, Pack = 4)]
public unsafe partial struct Int4x3
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
    /// Creates a new <see cref="Int4x3"/> instance with the specified parameters.
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
    public Int4x3(int m11, int m12, int m13, int m21, int m22, int m23, int m31, int m32, int m33, int m41, int m42, int m43)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
        this.m21 = m21;
        this.m22 = m22;
        this.m23 = m23;
        this.m31 = m31;
        this.m32 = m32;
        this.m33 = m33;
        this.m41 = m41;
        this.m42 = m42;
        this.m43 = m43;
    }

    /// <summary>
    /// Creates a new <see cref="Int4x3"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    /// <param name="row4">The value to assign to the row at position [4].</param>
    public Int4x3(Int3 row1, Int3 row2, Int3 row3, Int3 row4)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m13 = row1.Z;
        this.m21 = row2.X;
        this.m22 = row2.Y;
        this.m23 = row2.Z;
        this.m31 = row3.X;
        this.m32 = row3.Y;
        this.m33 = row3.Z;
        this.m41 = row4.X;
        this.m42 = row4.Y;
        this.m43 = row4.Z;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int4x3"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[int row] => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x3"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x3"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M13 => ref this.m13;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M23 => ref this.m23;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M31 => ref this.m31;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M32 => ref this.m32;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M33 => ref this.m33;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M41 => ref this.m41;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M42 => ref this.m42;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M43 => ref this.m43;

    /// <summary>
    /// Creates a new <see cref="Int4x3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int4x3"/> instance.</param>
    public static implicit operator Int4x3(int x)
    {
        Int4x3 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m13 = x;
        matrix.m21 = x;
        matrix.m22 = x;
        matrix.m23 = x;
        matrix.m31 = x;
        matrix.m32 = x;
        matrix.m33 = x;
        matrix.m41 = x;
        matrix.m42 = x;
        matrix.m43 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int4x3"/> value to a <see cref="Float4x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float4x3(Int4x3 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int4x3"/> value to a <see cref="Double4x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double4x3(Int4x3 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int4x3"/> value to a <see cref="UInt4x3"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x3"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt4x3(Int4x3 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator -(Int4x3 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int4x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator +(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Divides two <see cref="Int4x3"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator /(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int4x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator %(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int4x3"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator *(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int4x3"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator *(Int4x3 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int4x3"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int4x3"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator *(int left, Int4x3 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x3 input value.</param>
    /// <param name="y">The second Int3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x3, Int3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4 operator *(Int4x3 x, Int3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x3 input value.</param>
    /// <param name="y">The second Int3x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x3, Int3x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x1 operator *(Int4x3 x, Int3x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x3 input value.</param>
    /// <param name="y">The second Int3x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x3, Int3x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x2 operator *(Int4x3 x, Int3x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x3 input value.</param>
    /// <param name="y">The second Int3x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x3, Int3x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x3 operator *(Int4x3 x, Int3x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x3 input value.</param>
    /// <param name="y">The second Int3x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x3, Int3x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x4 operator *(Int4x3 x, Int3x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x4 input value.</param>
    /// <param name="y">The second Int4x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x4, Int4x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4x3 operator *(Int4x4 x, Int4x3 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int4x3"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator -(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x3"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator >(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x3"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator >=(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x3"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator <(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x3"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator <=(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator ~(Int4x3 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator >>(Int4x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator >>(Int4x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator >>(Int4x3 matrix, Int4x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator >>(Int4x3 matrix, UInt4x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator >>>(Int4x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator >>>(Int4x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator >>>(Int4x3 matrix, Int4x3 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x3"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator >>>(Int4x3 matrix, UInt4x3 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator <<(Int4x3 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator <<(Int4x3 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator <<(Int4x3 matrix, Int4x3 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x3"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator <<(Int4x3 matrix, UInt4x3 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator &(Int4x3 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator &(Int4x3 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int4x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator &(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt4x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator &(Int4x3 left, UInt4x3 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator |(Int4x3 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator |(Int4x3 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int4x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator |(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt4x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator |(Int4x3 left, UInt4x3 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator ^(Int4x3 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator ^(Int4x3 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int4x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator ^(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x3"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x3"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt4x3"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x3 operator ^(Int4x3 left, UInt4x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x3"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator ==(Int4x3 left, Int4x3 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x3"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x3"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x3"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x3 operator !=(Int4x3 left, Int4x3 right) => default;
}

/// <inheritdoc cref="Int4x4"/>
[StructLayout(LayoutKind.Explicit, Size = 64, Pack = 4)]
public unsafe partial struct Int4x4
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
    /// Creates a new <see cref="Int4x4"/> instance with the specified parameters.
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
    public Int4x4(int m11, int m12, int m13, int m14, int m21, int m22, int m23, int m24, int m31, int m32, int m33, int m34, int m41, int m42, int m43, int m44)
    {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
        this.m14 = m14;
        this.m21 = m21;
        this.m22 = m22;
        this.m23 = m23;
        this.m24 = m24;
        this.m31 = m31;
        this.m32 = m32;
        this.m33 = m33;
        this.m34 = m34;
        this.m41 = m41;
        this.m42 = m42;
        this.m43 = m43;
        this.m44 = m44;
    }

    /// <summary>
    /// Creates a new <see cref="Int4x4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="row1">The value to assign to the row at position [1].</param>
    /// <param name="row2">The value to assign to the row at position [2].</param>
    /// <param name="row3">The value to assign to the row at position [3].</param>
    /// <param name="row4">The value to assign to the row at position [4].</param>
    public Int4x4(Int4 row1, Int4 row2, Int4 row3, Int4 row4)
    {
        this.m11 = row1.X;
        this.m12 = row1.Y;
        this.m13 = row1.Z;
        this.m14 = row1.W;
        this.m21 = row2.X;
        this.m22 = row2.Y;
        this.m23 = row2.Z;
        this.m24 = row2.W;
        this.m31 = row3.X;
        this.m32 = row3.Y;
        this.m33 = row3.Z;
        this.m34 = row3.W;
        this.m41 = row4.X;
        this.m42 = row4.Y;
        this.m43 = row4.Z;
        this.m44 = row4.W;
    }

    /// <summary>
    /// Gets a reference to a specific row in the current <see cref="Int4x4"/> instance.
    /// </summary>
    /// <param name="row">The row to access.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int4 this[int row] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Int2*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x4"/> instance.
    /// </summary>
    /// <param name="xy0">The identifier of the first item to index.</param>
    /// <param name="xy1">The identifier of the second item to index.</param>
    /// <param name="xy2">The identifier of the third item to index.</param>
    /// <remarks>
    /// <para>Unlike with vector types, these properties cannot validate in advance which combinations are writeable, so callers should ensure proper use.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [UnscopedRef]
    public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Int3*)UndefinedData.Memory;
        
    /// <summary>
    /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x4"/> instance.
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
    public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M11 => ref this.m11;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M12 => ref this.m12;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M13 => ref this.m13;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [1, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M14 => ref this.m14;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M21 => ref this.m21;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M22 => ref this.m22;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M23 => ref this.m23;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [2, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M24 => ref this.m24;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M31 => ref this.m31;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M32 => ref this.m32;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M33 => ref this.m33;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [3, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M34 => ref this.m34;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 1].
    /// </summary>
    [UnscopedRef]
    public ref int M41 => ref this.m41;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 2].
    /// </summary>
    [UnscopedRef]
    public ref int M42 => ref this.m42;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 3].
    /// </summary>
    [UnscopedRef]
    public ref int M43 => ref this.m43;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the component at position [4, 4].
    /// </summary>
    [UnscopedRef]
    public ref int M44 => ref this.m44;

    /// <summary>
    /// Creates a new <see cref="Int4x4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int4x4"/> instance.</param>
    public static implicit operator Int4x4(int x)
    {
        Int4x4 matrix;

        matrix.m11 = x;
        matrix.m12 = x;
        matrix.m13 = x;
        matrix.m14 = x;
        matrix.m21 = x;
        matrix.m22 = x;
        matrix.m23 = x;
        matrix.m24 = x;
        matrix.m31 = x;
        matrix.m32 = x;
        matrix.m33 = x;
        matrix.m34 = x;
        matrix.m41 = x;
        matrix.m42 = x;
        matrix.m43 = x;
        matrix.m44 = x;

        return matrix;
    }

    /// <summary>
    /// Casts a <see cref="Int4x4"/> value to a <see cref="Float4x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float4x4(Int4x4 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int4x4"/> value to a <see cref="Double4x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double4x4(Int4x4 matrix) => default;

    /// <summary>
    /// Casts a <see cref="Int4x4"/> value to a <see cref="UInt4x4"/> one.
    /// </summary>
    /// <param name="matrix">The input <see cref="Int4x4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt4x4(Int4x4 matrix) => default;

    /// <summary>
    /// Negates a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to negate.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator -(Int4x4 matrix) => default;

    /// <summary>
    /// Sums two <see cref="Int4x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator +(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Divides two <see cref="Int4x4"/> values (elementwise division).
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator /(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int4x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator %(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int4x4"/> values (elementwise product).
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator *(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int4x4"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator *(Int4x4 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int4x4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int4x4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator *(int left, Int4x4 right) => default;

    /// <summary>
    /// Subtracts two <see cref="Int4x4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator -(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x4"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator >(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x4"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator >=(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x4"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator <(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x4"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator <=(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="matrix"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator ~(Int4x4 matrix) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator >>(Int4x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator >>(Int4x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator >>(Int4x4 matrix, Int4x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator >>(Int4x4 matrix, UInt4x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator >>>(Int4x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator >>>(Int4x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator >>>(Int4x4 matrix, Int4x4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4x4"/> value without considering sign.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator >>>(Int4x4 matrix, UInt4x4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator <<(Int4x4 matrix, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator <<(Int4x4 matrix, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator <<(Int4x4 matrix, Int4x4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="matrix">The <see cref="Int4x4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="matrix"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator <<(Int4x4 matrix, UInt4x4 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator &(Int4x4 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator &(Int4x4 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int4x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator &(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt4x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator &(Int4x4 left, UInt4x4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator |(Int4x4 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator |(Int4x4 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int4x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator |(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt4x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator |(Int4x4 left, UInt4x4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator ^(Int4x4 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator ^(Int4x4 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int4x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator ^(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4x4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4x4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt4x4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4x4 operator ^(Int4x4 left, UInt4x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator ==(Int4x4 left, Int4x4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4x4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4x4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4x4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4x4 operator !=(Int4x4 left, Int4x4 right) => default;
}
