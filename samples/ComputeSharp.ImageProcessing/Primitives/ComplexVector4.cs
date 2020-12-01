// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System.Numerics;

namespace SixLabors.ImageSharp
{
    /// <summary>
    /// A vector with a pair of <see cref="Vector4"/> representing a complex color.
    /// </summary>
    internal struct ComplexVector4
    {
        /// <summary>
        /// The real part of the complex vector.
        /// </summary>
        public Vector4 Real;

        /// <summary>
        /// The imaginary part of the complex number.
        /// </summary>
        public Vector4 Imaginary;
    }
}