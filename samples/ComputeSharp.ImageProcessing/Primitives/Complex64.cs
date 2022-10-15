// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.CompilerServices;

namespace SixLabors.ImageSharp;

/// <summary>
/// Represents a complex number, where the real and imaginary parts are stored as <see cref="float"/> values.
/// </summary>
/// <remarks>
/// This is a more efficient version of the <see cref="Complex64"/> type.
/// </remarks>
internal readonly struct Complex64
{
    /// <summary>
    /// The real part of the complex number.
    /// </summary>
    public readonly float Real;

    /// <summary>
    /// The imaginary part of the complex number.
    /// </summary>
    public readonly float Imaginary;

    /// <summary>
    /// Initializes a new instance of the <see cref="Complex64"/> struct.
    /// </summary>
    /// <param name="real">The real part in the complex number.</param>
    /// <param name="imaginary">The imaginary part in the complex number.</param>
    public Complex64(float real, float imaginary)
    {
        this.Real = real;
        this.Imaginary = imaginary;
    }

    /// <summary>
    /// Performs the multiplication operation between a <see cref="Complex64"/> instance and a <see cref="float"/> scalar.
    /// </summary>
    /// <param name="value">The <see cref="Complex64"/> value to multiply.</param>
    /// <param name="scalar">The <see cref="float"/> scalar to use to multiply the <see cref="Complex64"/> value.</param>
    /// <returns>The <see cref="Complex64"/> result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex64 operator *(Complex64 value, float scalar) => new(value.Real * scalar, value.Imaginary * scalar);
}