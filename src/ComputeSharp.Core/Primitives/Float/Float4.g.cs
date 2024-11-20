using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Intrinsics;

#nullable enable
#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <inheritdoc cref="Float4"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
public unsafe partial struct Float4
{
    [FieldOffset(0)]
    private float x;

    [FieldOffset(4)]
    private float y;

    [FieldOffset(8)]
    private float z;

    [FieldOffset(12)]
    private float w;

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(Float2 xy, float z, float w)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(Float1x2 xy, float z, float w)
    {
        this.x = xy.M11;
        this.y = xy.M12;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(Float2x1 xy, float z, float w)
    {
        this.x = xy.M11;
        this.y = xy.M21;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(float x, Float2 yz, float w)
    {
        this.x = x;
        this.y = yz.X;
        this.z = yz.Y;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(float x, Float1x2 yz, float w)
    {
        this.x = x;
        this.y = yz.M11;
        this.z = yz.M12;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(float x, Float2x1 yz, float w)
    {
        this.x = x;
        this.y = yz.M11;
        this.z = yz.M21;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(float x, float y, Float2 zw)
    {
        this.x = x;
        this.y = y;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(float x, float y, Float1x2 zw)
    {
        this.x = x;
        this.y = y;
        this.z = zw.M11;
        this.w = zw.M12;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(float x, float y, Float2x1 zw)
    {
        this.x = x;
        this.y = y;
        this.z = zw.M11;
        this.w = zw.M21;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float2 xy, Float2 zw)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float2 xy, Float1x2 zw)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = zw.M11;
        this.w = zw.M12;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float2 xy, Float2x1 zw)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = zw.M11;
        this.w = zw.M21;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float1x2 xy, Float2 zw)
    {
        this.x = xy.M11;
        this.y = xy.M12;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float1x2 xy, Float1x2 zw)
    {
        this.x = xy.M11;
        this.y = xy.M12;
        this.z = zw.M11;
        this.w = zw.M12;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float1x2 xy, Float2x1 zw)
    {
        this.x = xy.M11;
        this.y = xy.M12;
        this.z = zw.M11;
        this.w = zw.M21;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float2x1 xy, Float2 zw)
    {
        this.x = xy.M11;
        this.y = xy.M21;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float2x1 xy, Float1x2 zw)
    {
        this.x = xy.M11;
        this.y = xy.M21;
        this.z = zw.M11;
        this.w = zw.M12;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(Float2x1 xy, Float2x1 zw)
    {
        this.x = xy.M11;
        this.y = xy.M21;
        this.z = zw.M11;
        this.w = zw.M21;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(Float3 xyz, float w)
    {
        this.x = xyz.X;
        this.y = xyz.Y;
        this.z = xyz.Z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(Float1x3 xyz, float w)
    {
        this.x = xyz.M11;
        this.y = xyz.M12;
        this.z = xyz.M13;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Float4(Float3x1 xyz, float w)
    {
        this.x = xyz.M11;
        this.y = xyz.M21;
        this.z = xyz.M31;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yzw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(float x, Float3 yzw)
    {
        this.x = x;
        this.y = yzw.X;
        this.z = yzw.Y;
        this.w = yzw.Z;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yzw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(float x, Float1x3 yzw)
    {
        this.x = x;
        this.y = yzw.M11;
        this.z = yzw.M12;
        this.w = yzw.M13;
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yzw">The value to assign to the first, second, third and fourth vector components.</param>
    public Float4(float x, Float3x1 yzw)
    {
        this.x = x;
        this.y = yzw.M11;
        this.z = yzw.M21;
        this.w = yzw.M31;
    }

    /// <summary>
    /// Gets a reference to a specific component in the current <see cref="Float4"/> instance.
    /// </summary>
    /// <param name="i">The index of the component to access.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref float this[int i] => ref *(float*)UndefinedData.Memory;

    /// <summary>
    /// Gets a <see cref="Float4"/> value with all components set to 0.
    /// </summary>
    public static Float4 Zero => 0;

    /// <summary>
    /// Gets a <see cref="Float4"/> value with all components set to 1.
    /// </summary>
    public static Float4 One => 1;

    /// <summary>
    /// Gets a <see cref="Float4"/> value with the <see cref="X"/> component set to 1, and the others to 0.
    /// </summary>
    public static Float4 UnitX => new(1, 0, 0, 0);

    /// <summary>
    /// Gets a <see cref="Float4"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
    /// </summary>
    public static Float4 UnitY => new(0, 1, 0, 0);

    /// <summary>
    /// Gets a <see cref="Float4"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
    /// </summary>
    public static Float4 UnitZ => new(0, 0, 1, 0);

    /// <summary>
    /// Gets a <see cref="Float4"/> value with the <see cref="W"/> component set to 1, and the others to 0.
    /// </summary>
    public static Float4 UnitW => new(0, 0, 0, 1);

    /// <summary>
    /// Gets a reference to the <see cref="float"/> value representing the <c>X</c> component.
    /// </summary>
    [UnscopedRef]
    public ref float X => ref this.x;

    /// <summary>
    /// Gets a reference to the <see cref="float"/> value representing the <c>Y</c> component.
    /// </summary>
    [UnscopedRef]
    public ref float Y => ref this.y;

    /// <summary>
    /// Gets a reference to the <see cref="float"/> value representing the <c>Z</c> component.
    /// </summary>
    [UnscopedRef]
    public ref float Z => ref this.z;

    /// <summary>
    /// Gets a reference to the <see cref="float"/> value representing the <c>W</c> component.
    /// </summary>
    [UnscopedRef]
    public ref float W => ref this.w;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float2 XX => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 XY => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 XZ => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 XW => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 YX => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float2 YY => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 YZ => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 YW => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 ZX => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 ZY => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float2 ZZ => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 ZW => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 WX => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 WY => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 WZ => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float2 WW => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XXX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XXY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XXZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XXW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XYX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XYY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 XYZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 XYW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XZX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 XZY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XZZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 XZW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XWX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 XWY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 XWZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 XWW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YXX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YXY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 YXZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 YXW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YYX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YYY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YYZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YYW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 YZX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YZY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YZZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 YZW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 YWX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YWY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 YWZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 YWW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZXX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ZXY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZXZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ZXW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ZYX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZYY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZYZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ZYW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZZX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZZY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZZZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZZW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ZWX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ZWY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZWZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ZWW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WXX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 WXY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 WXZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WXW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 WYX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WYY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 WYZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WYW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 WZX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 WZY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WZZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WZW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WWX => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WWY => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WWZ => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 WWW => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XXWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 XYZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 XYWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XYWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 XZYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 XZWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XZWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 XWYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 XWZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 XWWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 YXZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 YXWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YXWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YYWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 YZXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 YZWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YZWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 YWXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 YWZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 YWWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ZXYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ZXWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZXWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ZYXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ZYWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZYWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZZWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ZWXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ZWYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ZWWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 WXYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 WXZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WXWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 WYXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 WYZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WYWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 WZXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 WZYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WZWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWXX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWXY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWXZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWXW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWYX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWYY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWYZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWYW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWZX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWZY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWZZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWZW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWWX => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWWY => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWWZ => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 WWWW => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="float"/> value representing the <c>R</c> component.
    /// </summary>
    [UnscopedRef]
    public ref float R => ref this.x;

    /// <summary>
    /// Gets a reference to the <see cref="float"/> value representing the <c>G</c> component.
    /// </summary>
    [UnscopedRef]
    public ref float G => ref this.y;

    /// <summary>
    /// Gets a reference to the <see cref="float"/> value representing the <c>B</c> component.
    /// </summary>
    [UnscopedRef]
    public ref float B => ref this.z;

    /// <summary>
    /// Gets a reference to the <see cref="float"/> value representing the <c>A</c> component.
    /// </summary>
    [UnscopedRef]
    public ref float A => ref this.w;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float2 RR => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 RG => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 RB => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 RA => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 GR => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float2 GG => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 GB => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 GA => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 BR => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 BG => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float2 BB => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 BA => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 AR => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 AG => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float2 AB => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float2"/> value with the components <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float2 AA => ref *(Float2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RRR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RRG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RRB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RRA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RGR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RGG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 RGB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 RGA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RBR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 RBG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RBB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 RBA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RAR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 RAG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 RAB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 RAA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GRR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GRG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 GRB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 GRA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GGR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GGG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GGB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GGA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 GBR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GBG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GBB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 GBA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 GAR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GAG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 GAB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 GAA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BRR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 BRG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BRB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 BRA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 BGR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BGG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BGB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 BGA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BBR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BBG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BBB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BBA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 BAR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 BAG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BAB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 BAA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ARR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ARG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ARB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ARA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 AGR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 AGG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 AGB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 AGA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ABR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float3 ABG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ABB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 ABA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 AAR => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 AAG => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 AAB => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float3 AAA => ref *(Float3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RRAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 RGBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 RGAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RGAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 RBGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 RBAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RBAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RARR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RARG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RARB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RARA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RAGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RAGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 RAGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RAGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RABR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 RABG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RABB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RABA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RAAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RAAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RAAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 RAAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 GRBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 GRAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GRAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GGAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 GBRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 GBAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GBAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GARR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GARG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 GARB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GARA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GAGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GAGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GAGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GAGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 GABR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GABG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GABB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GABA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GAAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GAAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GAAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 GAAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 BRGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 BRAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BRAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 BGRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 BGAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BGAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BBAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BARR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 BARG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BARB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BARA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 BAGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BAGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BAGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BAGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BABR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BABG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BABB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BABA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BAAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BAAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BAAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 BAAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ARGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ARBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ARAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 AGRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 AGBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AGAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABRR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ABRG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABRB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABRA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Float4 ABGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABBR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABBG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABBB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABBA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 ABAA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AARR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AARG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AARB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AARA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AAGR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AAGG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AAGB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AAGA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AABR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AABG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AABB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AABA => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AAAR => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AAAG => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AAAB => ref *(Float4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Float4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Float4 AAAA => ref *(Float4*)UndefinedData.Memory;

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        string separator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator;

        return $"<{this.x}{separator} {this.y}{separator} {this.z}{separator} {this.w}>";
    }

    /// <summary>
    /// Creates a new <see cref="Float4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Float4"/> instance.</param>
    public static implicit operator Float4(float x) => new(x, x, x, x);

    /// <summary>
    /// Casts a <see cref="Float4"/> value to a <see cref="Double4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="Float4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double4(Float4 xyzw) => default;

    /// <summary>
    /// Casts a <see cref="Float4"/> value to a <see cref="Int4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="Float4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator Int4(Float4 xyzw) => default;

    /// <summary>
    /// Casts a <see cref="Float4"/> value to a <see cref="UInt4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="Float4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt4(Float4 xyzw) => default;

    /// <summary>
    /// Negates a <see cref="Float4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Float4"/> value to negate.</param>
    /// <returns>The negated value of <paramref name="xyzw"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Float4 operator -(Float4 xyzw) => default;

    /// <summary>
    /// Sums two <see cref="Float4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to sum.</param>
    /// <param name="right">The second <see cref="Float4"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Float4 operator +(Float4 left, Float4 right) => default;

    /// <summary>
    /// Divides two <see cref="Float4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Float4"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Float4 operator /(Float4 left, Float4 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Float4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Float4"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Float4 operator %(Float4 left, Float4 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Float4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Float4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Float4 operator *(Float4 left, Float4 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Float4"/> and <see cref="float"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Float4"/> value to multiply.</param>
    /// <param name="right">The <see cref="float"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Float4 operator *(Float4 left, float right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="float"/> and <see cref="Float4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="float"/> value to multiply.</param>
    /// <param name="right">The <see cref="Float4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Float4 operator *(float left, Float4 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Float4 input value.</param>
    /// <param name="y">The second Float4x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Float4, Float4x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static float operator *(Float4 x, Float4x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Float4 input value.</param>
    /// <param name="y">The second Float4x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Float4, Float4x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Float2 operator *(Float4 x, Float4x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Float4 input value.</param>
    /// <param name="y">The second Float4x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Float4, Float4x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Float3 operator *(Float4 x, Float4x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Float4 input value.</param>
    /// <param name="y">The second Float4x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Float4, Float4x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Float4 operator *(Float4 x, Float4x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Float4x4 input value.</param>
    /// <param name="y">The second Float4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Float4x4, Float4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Float4 operator *(Float4x4 x, Float4 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Float4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Float4"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Float4 operator -(Float4 left, Float4 right) => default;

    /// <summary>
    /// Compares two <see cref="Float4"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Float4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator >(Float4 left, Float4 right) => default;

    /// <summary>
    /// Compares two <see cref="Float4"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Float4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator >=(Float4 left, Float4 right) => default;

    /// <summary>
    /// Compares two <see cref="Float4"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Float4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator <(Float4 left, Float4 right) => default;

    /// <summary>
    /// Compares two <see cref="Float4"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Float4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator <=(Float4 left, Float4 right) => default;

    /// <summary>
    /// Compares two <see cref="Float4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Float4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator ==(Float4 left, Float4 right) => default;

    /// <summary>
    /// Compares two <see cref="Float4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Float4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Float4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator !=(Float4 left, Float4 right) => default;
}
