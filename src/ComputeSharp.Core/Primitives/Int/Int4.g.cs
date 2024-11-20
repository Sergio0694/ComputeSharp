using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Intrinsics;

#nullable enable
#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <inheritdoc cref="Int4"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
public unsafe partial struct Int4
{
    [FieldOffset(0)]
    private int x;

    [FieldOffset(4)]
    private int y;

    [FieldOffset(8)]
    private int z;

    [FieldOffset(12)]
    private int w;

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(int x, int y, int z, int w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(Int2 xy, int z, int w)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(Int1x2 xy, int z, int w)
    {
        this.x = xy.M11;
        this.y = xy.M12;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="z">The value to assign to the third vector component.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(Int2x1 xy, int z, int w)
    {
        this.x = xy.M11;
        this.y = xy.M21;
        this.z = z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(int x, Int2 yz, int w)
    {
        this.x = x;
        this.y = yz.X;
        this.z = yz.Y;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(int x, Int1x2 yz, int w)
    {
        this.x = x;
        this.y = yz.M11;
        this.z = yz.M12;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(int x, Int2x1 yz, int w)
    {
        this.x = x;
        this.y = yz.M11;
        this.z = yz.M21;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(int x, int y, Int2 zw)
    {
        this.x = x;
        this.y = y;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(int x, int y, Int1x2 zw)
    {
        this.x = x;
        this.y = y;
        this.z = zw.M11;
        this.w = zw.M12;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(int x, int y, Int2x1 zw)
    {
        this.x = x;
        this.y = y;
        this.z = zw.M11;
        this.w = zw.M21;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int2 xy, Int2 zw)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int2 xy, Int1x2 zw)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = zw.M11;
        this.w = zw.M12;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int2 xy, Int2x1 zw)
    {
        this.x = xy.X;
        this.y = xy.Y;
        this.z = zw.M11;
        this.w = zw.M21;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int1x2 xy, Int2 zw)
    {
        this.x = xy.M11;
        this.y = xy.M12;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int1x2 xy, Int1x2 zw)
    {
        this.x = xy.M11;
        this.y = xy.M12;
        this.z = zw.M11;
        this.w = zw.M12;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int1x2 xy, Int2x1 zw)
    {
        this.x = xy.M11;
        this.y = xy.M12;
        this.z = zw.M11;
        this.w = zw.M21;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int2x1 xy, Int2 zw)
    {
        this.x = xy.M11;
        this.y = xy.M21;
        this.z = zw.X;
        this.w = zw.Y;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int2x1 xy, Int1x2 zw)
    {
        this.x = xy.M11;
        this.y = xy.M21;
        this.z = zw.M11;
        this.w = zw.M12;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xy">The value to assign to the first and second vector components.</param>
    /// <param name="zw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(Int2x1 xy, Int2x1 zw)
    {
        this.x = xy.M11;
        this.y = xy.M21;
        this.z = zw.M11;
        this.w = zw.M21;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(Int3 xyz, int w)
    {
        this.x = xyz.X;
        this.y = xyz.Y;
        this.z = xyz.Z;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(Int1x3 xyz, int w)
    {
        this.x = xyz.M11;
        this.y = xyz.M12;
        this.z = xyz.M13;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="xyz">The value to assign to the first, second and third vector components.</param>
    /// <param name="w">The value to assign to the fourth vector component.</param>
    public Int4(Int3x1 xyz, int w)
    {
        this.x = xyz.M11;
        this.y = xyz.M21;
        this.z = xyz.M31;
        this.w = w;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yzw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(int x, Int3 yzw)
    {
        this.x = x;
        this.y = yzw.X;
        this.z = yzw.Y;
        this.w = yzw.Z;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yzw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(int x, Int1x3 yzw)
    {
        this.x = x;
        this.y = yzw.M11;
        this.z = yzw.M12;
        this.w = yzw.M13;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="yzw">The value to assign to the first, second, third and fourth vector components.</param>
    public Int4(int x, Int3x1 yzw)
    {
        this.x = x;
        this.y = yzw.M11;
        this.z = yzw.M21;
        this.w = yzw.M31;
    }

    /// <summary>
    /// Gets a reference to a specific component in the current <see cref="Int4"/> instance.
    /// </summary>
    /// <param name="i">The index of the component to access.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref int this[int i] => ref *(int*)UndefinedData.Memory;

    /// <summary>
    /// Gets a <see cref="Int4"/> value with all components set to 0.
    /// </summary>
    public static Int4 Zero => 0;

    /// <summary>
    /// Gets a <see cref="Int4"/> value with all components set to 1.
    /// </summary>
    public static Int4 One => 1;

    /// <summary>
    /// Gets a <see cref="Int4"/> value with the <see cref="X"/> component set to 1, and the others to 0.
    /// </summary>
    public static Int4 UnitX => new(1, 0, 0, 0);

    /// <summary>
    /// Gets a <see cref="Int4"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
    /// </summary>
    public static Int4 UnitY => new(0, 1, 0, 0);

    /// <summary>
    /// Gets a <see cref="Int4"/> value with the <see cref="Z"/> component set to 1, and the others to 0.
    /// </summary>
    public static Int4 UnitZ => new(0, 0, 1, 0);

    /// <summary>
    /// Gets a <see cref="Int4"/> value with the <see cref="W"/> component set to 1, and the others to 0.
    /// </summary>
    public static Int4 UnitW => new(0, 0, 0, 1);

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the <c>X</c> component.
    /// </summary>
    [UnscopedRef]
    public ref int X => ref this.x;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the <c>Y</c> component.
    /// </summary>
    [UnscopedRef]
    public ref int Y => ref this.y;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the <c>Z</c> component.
    /// </summary>
    [UnscopedRef]
    public ref int Z => ref this.z;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the <c>W</c> component.
    /// </summary>
    [UnscopedRef]
    public ref int W => ref this.w;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int2 XX => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 XY => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 XZ => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 XW => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 YX => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int2 YY => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 YZ => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 YW => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 ZX => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 ZY => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int2 ZZ => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 ZW => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 WX => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 WY => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 WZ => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int2 WW => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XXX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XXY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XXZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XXW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XYX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XYY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 XYZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 XYW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XZX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 XZY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XZZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 XZW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XWX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 XWY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 XWZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 XWW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YXX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YXY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 YXZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 YXW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YYX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YYY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YYZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YYW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 YZX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YZY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YZZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 YZW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 YWX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YWY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 YWZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 YWW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZXX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ZXY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZXZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ZXW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ZYX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZYY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZYZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ZYW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZZX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZZY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZZZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZZW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ZWX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ZWY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZWZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ZWW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WXX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 WXY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 WXZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WXW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 WYX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WYY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 WYZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WYW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 WZX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 WZY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WZZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WZW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WWX => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WWY => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WWZ => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 WWW => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XXWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 XYZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 XYWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XYWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 XZYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 XZWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XZWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 XWYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 XWZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 XWWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 YXZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 YXWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YXWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YYWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 YZXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 YZWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YZWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 YWXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 YWZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 YWWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ZXYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ZXWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZXWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ZYXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ZYWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZYWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZZWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ZWXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ZWYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ZWWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 WXYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 WXZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WXWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 WYXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 WYZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WYWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 WZXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 WZYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WZWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWXX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWXY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWXZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWXW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWYX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWYY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWYZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWYW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWZX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWZY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWZZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWZW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWWX => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWWY => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWWZ => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 WWWW => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the <c>R</c> component.
    /// </summary>
    [UnscopedRef]
    public ref int R => ref this.x;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the <c>G</c> component.
    /// </summary>
    [UnscopedRef]
    public ref int G => ref this.y;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the <c>B</c> component.
    /// </summary>
    [UnscopedRef]
    public ref int B => ref this.z;

    /// <summary>
    /// Gets a reference to the <see cref="int"/> value representing the <c>A</c> component.
    /// </summary>
    [UnscopedRef]
    public ref int A => ref this.w;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int2 RR => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 RG => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 RB => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 RA => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 GR => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int2 GG => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 GB => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 GA => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 BR => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 BG => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int2 BB => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 BA => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 AR => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 AG => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int2"/> value with the components <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int2 AB => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int2"/> value with the components <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int2 AA => ref *(Int2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RRR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RRG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RRB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RRA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RGR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RGG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 RGB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 RGA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RBR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 RBG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RBB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 RBA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RAR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 RAG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 RAB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 RAA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GRR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GRG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 GRB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 GRA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GGR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GGG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GGB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GGA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 GBR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GBG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GBB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 GBA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 GAR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GAG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 GAB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 GAA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BRR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 BRG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BRB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 BRA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 BGR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BGG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BGB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 BGA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BBR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BBG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BBB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BBA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 BAR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 BAG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BAB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 BAA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ARR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ARG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ARB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ARA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 AGR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 AGG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 AGB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 AGA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ABR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int3 ABG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ABB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 ABA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 AAR => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 AAG => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 AAB => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int3"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int3 AAA => ref *(Int3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RRAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 RGBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 RGAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RGAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 RBGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 RBAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RBAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RARR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RARG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RARB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RARA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RAGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RAGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 RAGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RAGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RABR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 RABG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RABB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RABA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RAAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RAAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RAAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 RAAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 GRBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 GRAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GRAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GGAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 GBRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 GBAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GBAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GARR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GARG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 GARB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GARA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GAGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GAGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GAGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GAGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 GABR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GABG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GABB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GABA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GAAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GAAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GAAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 GAAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 BRGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 BRAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BRAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 BGRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 BGAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BGAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BBAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BARR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 BARG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BARB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BARA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 BAGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BAGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BAGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BAGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BABR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BABG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BABB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BABA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BAAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BAAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BAAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 BAAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ARGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ARBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ARAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 AGRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 AGBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AGAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABRR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ABRG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABRB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABRA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Int4 ABGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABBR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABBG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABBB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABBA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 ABAA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AARR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AARG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AARB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AARA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AAGR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AAGG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AAGB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AAGA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AABR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AABG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AABB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AABA => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AAAR => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AAAG => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AAAB => ref *(Int4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Int4"/> value with the components <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Int4 AAAA => ref *(Int4*)UndefinedData.Memory;

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        string separator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator;

        return $"<{this.x}{separator} {this.y}{separator} {this.z}{separator} {this.w}>";
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int4"/> instance.</param>
    public static implicit operator Int4(int x) => new(x, x, x, x);

    /// <summary>
    /// Casts a <see cref="Int4"/> value to a <see cref="Float4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="Int4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Float4(Int4 xyzw) => default;

    /// <summary>
    /// Casts a <see cref="Int4"/> value to a <see cref="Double4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="Int4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static implicit operator Double4(Int4 xyzw) => default;

    /// <summary>
    /// Casts a <see cref="Int4"/> value to a <see cref="UInt4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="Int4"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt4(Int4 xyzw) => default;

    /// <summary>
    /// Negates a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to negate.</param>
    /// <returns>The negated value of <paramref name="xyzw"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator -(Int4 xyzw) => default;

    /// <summary>
    /// Sums two <see cref="Int4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to sum.</param>
    /// <param name="right">The second <see cref="Int4"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator +(Int4 left, Int4 right) => default;

    /// <summary>
    /// Divides two <see cref="Int4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator /(Int4 left, Int4 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Int4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to divide.</param>
    /// <param name="right">The second <see cref="Int4"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator %(Int4 left, Int4 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Int4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Int4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator *(Int4 left, Int4 right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="Int4"/> and <see cref="int"/> values.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to multiply.</param>
    /// <param name="right">The <see cref="int"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator *(Int4 left, int right) => default;

    /// <summary>
    /// Multiplies a pair of <see cref="int"/> and <see cref="Int4"/> values.
    /// </summary>
    /// <param name="left">The <see cref="int"/> value to multiply.</param>
    /// <param name="right">The <see cref="Int4"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator *(int left, Int4 right) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4 input value.</param>
    /// <param name="y">The second Int4x1 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4, Int4x1)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static int operator *(Int4 x, Int4x1 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4 input value.</param>
    /// <param name="y">The second Int4x2 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4, Int4x2)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int2 operator *(Int4 x, Int4x2 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4 input value.</param>
    /// <param name="y">The second Int4x3 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4, Int4x3)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int3 operator *(Int4 x, Int4x3 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4 input value.</param>
    /// <param name="y">The second Int4x4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4, Int4x4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4 operator *(Int4 x, Int4x4 y) => default;

    /// <summary>
    /// Multiplies two values using matrix math.
    /// </summary>
    /// <param name="x">The first Int4x4 input value.</param>
    /// <param name="y">The second Int4 input value.</param>
    /// <returns>The result of <paramref name="x"/> times <paramref name="y"/>. The result has the dimension <paramref name="x"/>-rows by <paramref name="y"/>-columns.</returns>
    /// <remarks>
    /// <para>This operator is equivalent to using <see cref="Hlsl.Mul(Int4x4, Int4)"/> with the same input arguments.</para>
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-mul"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("mul", RequiresParametersMatching = true)]
    public static Int4 operator *(Int4x4 x, Int4 y) => default;

    /// <summary>
    /// Subtracts two <see cref="Int4"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Int4"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator -(Int4 left, Int4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator >(Int4 left, Int4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator >=(Int4 left, Int4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator <(Int4 left, Int4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator <=(Int4 left, Int4 right) => default;

    /// <summary>
    /// Bitwise negates a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to bitwise negate.</param>
    /// <returns>The bitwise negated value of <paramref name="xyzw"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator ~(Int4 xyzw) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator >>(Int4 xyzw, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator >>(Int4 xyzw, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator >>(Int4 xyzw, Int4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator >>(Int4 xyzw, UInt4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4"/> value without considering sign.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator >>>(Int4 xyzw, int amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4"/> value without considering sign.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator >>>(Int4 xyzw, uint amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4"/> value without considering sign.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator >>>(Int4 xyzw, Int4 amount) => default;

    /// <summary>
    /// Shifts right a <see cref="Int4"/> value without considering sign.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift right.</param>
    /// <param name="amount">The amount to shift each element right by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> right by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator >>>(Int4 xyzw, UInt4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator <<(Int4 xyzw, int amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator <<(Int4 xyzw, uint amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator <<(Int4 xyzw, Int4 amount) => default;

    /// <summary>
    /// Shifts left a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="xyzw">The <see cref="Int4"/> value to shift left.</param>
    /// <param name="amount">The amount to shift each element left by.</param>
    /// <returns>The result of shifting <paramref name="xyzw"/> left by <paramref name="amount"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator <<(Int4 xyzw, UInt4 amount) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator &(Int4 left, int right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator &(Int4 left, uint right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="Int4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator &(Int4 left, Int4 right) => default;

    /// <summary>
    /// Bitwise ands a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise and.</param>
    /// <param name="right">The <see cref="UInt4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise and between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator &(Int4 left, UInt4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator |(Int4 left, int right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator |(Int4 left, uint right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="Int4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator |(Int4 left, Int4 right) => default;

    /// <summary>
    /// Bitwise ors a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise or.</param>
    /// <param name="right">The <see cref="UInt4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise or between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator |(Int4 left, UInt4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="int"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator ^(Int4 left, int right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="uint"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator ^(Int4 left, uint right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="Int4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator ^(Int4 left, Int4 right) => default;

    /// <summary>
    /// Bitwise xors a <see cref="Int4"/> value.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> value to bitwise xor.</param>
    /// <param name="right">The <see cref="UInt4"/> value to combine.</param>
    /// <returns>The result of performing the bitwise xor between <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Int4 operator ^(Int4 left, UInt4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator ==(Int4 left, Int4 right) => default;

    /// <summary>
    /// Compares two <see cref="Int4"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Int4"/> value to compare.</param>
    /// <param name="right">The second <see cref="Int4"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool4 operator !=(Int4 left, Int4 right) => default;
}
