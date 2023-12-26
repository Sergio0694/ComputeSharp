using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable enable
#pragma warning disable CS0660, CS0661

namespace ComputeSharp;

/// <inheritdoc cref="Double2"/>
[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
public unsafe partial struct Double2
{
    [FieldOffset(0)]
    private double x;

    [FieldOffset(8)]
    private double y;

    /// <summary>
    /// Creates a new <see cref="Double2"/> instance with the specified parameters.
    /// </summary>
    /// <param name="x">The value to assign to the first vector component.</param>
    /// <param name="y">The value to assign to the second vector component.</param>
    public Double2(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Gets a reference to a specific component in the current <see cref="Double2"/> instance.
    /// </summary>
    /// <param name="i">The index of the component to access.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref double this[int i] => ref *(double*)UndefinedData.Memory;

    /// <summary>
    /// Gets a <see cref="Double2"/> value with all components set to 0.
    /// </summary>
    public static Double2 Zero => 0;

    /// <summary>
    /// Gets a <see cref="Double2"/> value with all components set to 1.
    /// </summary>
    public static Double2 One => 1;

    /// <summary>
    /// Gets a <see cref="Double2"/> value with the <see cref="X"/> component set to 1, and the others to 0.
    /// </summary>
    public static Double2 UnitX => new(1, 0);

    /// <summary>
    /// Gets a <see cref="Double2"/> value with the <see cref="Y"/> component set to 1, and the others to 0.
    /// </summary>
    public static Double2 UnitY => new(0, 1);

    /// <summary>
    /// Gets a reference to the <see cref="double"/> value representing the <c>X</c> component.
    /// </summary>
    [UnscopedRef]
    public ref double X => ref this.x;

    /// <summary>
    /// Gets a reference to the <see cref="double"/> value representing the <c>Y</c> component.
    /// </summary>
    [UnscopedRef]
    public ref double Y => ref this.y;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double2 XX => ref *(Double2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Double2 XY => ref *(Double2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Double2 YX => ref *(Double2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double2 YY => ref *(Double2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 XXX => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 XXY => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 XYX => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 XYY => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 YXX => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 YXY => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 YYX => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 YYY => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 XXXX => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 XXXY => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 XXYX => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 XXYY => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 XYXX => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 XYXY => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 XYYX => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 XYYY => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 YXXX => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 YXXY => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 YXYX => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 YXYY => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 YYXX => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 YYXY => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 YYYX => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 YYYY => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="double"/> value representing the <c>R</c> component.
    /// </summary>
    [UnscopedRef]
    public ref double R => ref this.x;

    /// <summary>
    /// Gets a reference to the <see cref="double"/> value representing the <c>G</c> component.
    /// </summary>
    [UnscopedRef]
    public ref double G => ref this.y;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double2 RR => ref *(Double2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Double2 RG => ref *(Double2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public ref Double2 GR => ref *(Double2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double2"/> value with the components <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double2 GG => ref *(Double2*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 RRR => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 RRG => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 RGR => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 RGG => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 GRR => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 GRG => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 GGR => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double3"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double3 GGG => ref *(Double3*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 RRRR => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 RRRG => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 RRGR => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 RRGG => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 RGRR => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 RGRG => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 RGGR => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 RGGG => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 GRRR => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 GRRG => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 GRGR => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 GRGG => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 GGRR => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 GGRG => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 GGGR => ref *(Double4*)UndefinedData.Memory;

    /// <summary>
    /// Gets a readonly reference to the <see cref="Double4"/> value with the components <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
    /// </summary>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    [UnscopedRef]
    public readonly ref readonly Double4 GGGG => ref *(Double4*)UndefinedData.Memory;

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        string separator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator;

        return $"<{this.x}{separator} {this.y}>";
    }

    /// <summary>
    /// Creates a new <see cref="Double2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Double2"/> instance.</param>
    public static implicit operator Double2(double x) => new(x, x);

    /// <summary>
    /// Casts a <see cref="Double2"/> value to a <see cref="Float2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Double2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator Float2(Double2 xy) => default;

    /// <summary>
    /// Casts a <see cref="Double2"/> value to a <see cref="Int2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Double2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator Int2(Double2 xy) => default;

    /// <summary>
    /// Casts a <see cref="Double2"/> value to a <see cref="UInt2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Double2"/> value to cast.</param>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static explicit operator UInt2(Double2 xy) => default;

    /// <summary>
    /// Negates a <see cref="Double2"/> value.
    /// </summary>
    /// <param name="xy">The <see cref="Double2"/> value to negate.</param>
    /// <returns>The negated value of <paramref name="xy"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Double2 operator -(Double2 xy) => default;

    /// <summary>
    /// Sums two <see cref="Double2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to sum.</param>
    /// <param name="right">The second <see cref="Double2"/> value to sum.</param>
    /// <returns>The result of adding <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Double2 operator +(Double2 left, Double2 right) => default;

    /// <summary>
    /// Divides two <see cref="Double2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Double2"/> value to divide.</param>
    /// <returns>The result of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Double2 operator /(Double2 left, Double2 right) => default;

    /// <summary>
    /// Calculates the remainder of the division between two <see cref="Double2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to divide.</param>
    /// <param name="right">The second <see cref="Double2"/> value to divide.</param>
    /// <returns>The remainder of dividing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Double2 operator %(Double2 left, Double2 right) => default;

    /// <summary>
    /// Multiplies two <see cref="Double2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to multiply.</param>
    /// <param name="right">The second <see cref="Double2"/> value to multiply.</param>
    /// <returns>The result of multiplying <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Double2 operator *(Double2 left, Double2 right) => default;

    /// <summary>
    /// Subtracts two <see cref="Double2"/> values.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to subtract.</param>
    /// <param name="right">The second <see cref="Double2"/> value to subtract.</param>
    /// <returns>The result of subtracting <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Double2 operator -(Double2 left, Double2 right) => default;

    /// <summary>
    /// Compares two <see cref="Double2"/> values to see if the first is greater than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Double2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2 operator >(Double2 left, Double2 right) => default;

    /// <summary>
    /// Compares two <see cref="Double2"/> values to see if the first is greater than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Double2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2 operator >=(Double2 left, Double2 right) => default;

    /// <summary>
    /// Compares two <see cref="Double2"/> values to see if the first is lower than the second.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Double2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2 operator <(Double2 left, Double2 right) => default;

    /// <summary>
    /// Compares two <see cref="Double2"/> values to see if the first is lower than or equal to the second.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Double2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2 operator <=(Double2 left, Double2 right) => default;

    /// <summary>
    /// Compares two <see cref="Double2"/> values to see if they are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Double2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2 operator ==(Double2 left, Double2 right) => default;

    /// <summary>
    /// Compares two <see cref="Double2"/> values to see if they are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Double2"/> value to compare.</param>
    /// <param name="right">The second <see cref="Double2"/> value to compare.</param>
    /// <returns>The result of comparing <paramref name="left"/> and <paramref name="right"/>.</returns>
    /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
    public static Bool2 operator !=(Double2 left, Double2 right) => default;
}
