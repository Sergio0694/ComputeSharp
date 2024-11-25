using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace ComputeSharp;

/// <summary>
/// Packed pixel type containing four 8-bit unsigned normalized values ranging from 0 to 255.
/// The color components are stored in blue, green, red, and alpha order (least significant to most significant byte).
/// The format is binary compatible with System.Drawing.Imaging.PixelFormat.Format32bppArgb.
/// <para>
/// Ranges from [0, 0, 0, 0] to [1, 1, 1, 1] in vector form.
/// </para>
/// </summary>
/// <remarks>This struct is fully mutable.</remarks>
[StructLayout(LayoutKind.Sequential)]
public struct Bgra32 : IEquatable<Bgra32>, IPixel<Bgra32, Float4>, ISpanFormattable
{
    /// <summary>
    /// The blue component.
    /// </summary>
    public byte B;

    /// <summary>
    /// The green component.
    /// </summary>
    public byte G;

    /// <summary>
    /// The red component.
    /// </summary>
    public byte R;

    /// <summary>
    /// The alpha component.
    /// </summary>
    public byte A;

    /// <summary>
    /// Initializes a new instance of the <see cref="Bgra32"/> struct.
    /// </summary>
    /// <param name="r">The red component.</param>
    /// <param name="g">The green component.</param>
    /// <param name="b">The blue component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bgra32(byte r, byte g, byte b)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = byte.MaxValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Bgra32"/> struct.
    /// </summary>
    /// <param name="r">The red component.</param>
    /// <param name="g">The green component.</param>
    /// <param name="b">The blue component.</param>
    /// <param name="a">The alpha component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bgra32(byte r, byte g, byte b, byte a)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }

    /// <summary>
    /// Gets or sets the packed representation of the <see cref="Bgra32"/> struct.
    /// </summary>
    public uint PackedValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => Unsafe.As<Bgra32, uint>(ref Unsafe.AsRef(in this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Unsafe.As<Bgra32, uint>(ref this) = value;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Float4 ToPixel()
    {
        int pack = Unsafe.As<Bgra32, int>(ref Unsafe.AsRef(in this));
        Vector128<byte> vByte = Vector128.CreateScalarUnsafe(pack).AsByte();
        Vector128<ushort> vUShort = Vector128.WidenLower(vByte);
        Vector128<int> vInt = Vector128.WidenLower(vUShort).AsInt32();
        Vector128<int> vShuffle = Vector128.Shuffle(vInt, Vector128.Create(2, 1, 0, 3));
        Vector128<float> vFloat = Vector128.ConvertToSingle(vShuffle);
        Vector128<float> vMax = Vector128.Create((float)byte.MaxValue);
        Vector128<float> vNorm = Vector128.Divide(vFloat, vMax);

        return vNorm.AsVector4();
    }

    /// <summary>
    /// Compares two <see cref="Bgra32"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Bgra32"/> on the left side of the operand.</param>
    /// <param name="right">The <see cref="Bgra32"/> on the right side of the operand.</param>
    /// <returns>
    /// True if the <paramref name="left"/> parameter is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Bgra32 left, Bgra32 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Bgra32"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Bgra32"/> on the left side of the operand.</param>
    /// <param name="right">The <see cref="Bgra32"/> on the right side of the operand.</param>
    /// <returns>
    /// True if the <paramref name="left"/> parameter is not equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Bgra32 left, Bgra32 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj)
    {
        return obj is Bgra32 other && Equals(other);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(Bgra32 other)
    {
        return PackedValue.Equals(other.PackedValue);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly int GetHashCode()
    {
        return PackedValue.GetHashCode();
    }

    /// <inheritdoc />
    public override readonly string ToString()
    {
        return $"{nameof(Bgra32)}({this.B}, {this.G}, {this.R}, {this.A})";
    }

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return string.Create(formatProvider, $"{nameof(Bgra32)}({this.B}, {this.G}, {this.R}, {this.A})");
    }

    /// <inheritdoc />
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return destination.TryWrite(provider, $"{nameof(Bgra32)}({this.B}, {this.G}, {this.R}, {this.A})", out charsWritten);
    }
}