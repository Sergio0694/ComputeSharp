using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if !NET5_0
using RuntimeHelpers = ComputeSharp.SourceGenerators.Helpers.RuntimeHelpers;
using MemoryMarshal = ComputeSharp.SourceGenerators.Helpers.MemoryMarshal;
#endif

namespace ComputeSharp
{
    /// <inheritdoc cref="Double1x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public unsafe partial struct Double1x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double1x1), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        /// <summary>
        /// Creates a new <see cref="Double1x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        public Double1x1(double m11)
        {
            this.m11 = m11;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double1x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref double this[int row] => ref *(double*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Creates a new <see cref="Double1x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double1x1"/> instance.</param>
        public static implicit operator Double1x1(double x)
        {
            Double1x1 matrix;

            matrix.m11 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double1x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double1x1"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x1 operator -(Double1x1 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double1x1"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x1 operator +(Double1x1 left, Double1x1 right) => default;

        /// <summary>
        /// Divides two <see cref="Double1x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double1x1"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x1 operator /(Double1x1 left, Double1x1 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double1x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double1x1"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x1 operator *(Double1x1 left, Double1x1 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double1x1"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x1 operator -(Double1x1 left, Double1x1 right) => default;
    }

    /// <inheritdoc cref="Double1x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    public unsafe partial struct Double1x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double1x2), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        /// <summary>
        /// Creates a new <see cref="Double1x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        public Double1x2(double m11, double m12)
        {
            this.m11 = m11;
            this.m12 = m12;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double1x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[int row] => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Creates a new <see cref="Double1x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double1x2"/> instance.</param>
        public static implicit operator Double1x2(double x)
        {
            Double1x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double1x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double1x2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x2 operator -(Double1x2 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double1x2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x2 operator +(Double1x2 left, Double1x2 right) => default;

        /// <summary>
        /// Divides two <see cref="Double1x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double1x2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x2 operator /(Double1x2 left, Double1x2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double1x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double1x2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x2 operator *(Double1x2 left, Double1x2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double1x2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x2 operator -(Double1x2 left, Double1x2 right) => default;

        /// <summary>
        /// Casts a <see cref="Double2"/> value to a <see cref="Double1x2"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Double2"/> value to cast.</param>
        public static implicit operator Double1x2(Double2 vector) => *(Double1x2*)&vector;
    }

    /// <inheritdoc cref="Double1x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    public unsafe partial struct Double1x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double1x3), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m13;

        /// <summary>
        /// Creates a new <see cref="Double1x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        public Double1x3(double m11, double m12, double m13)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double1x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[int row] => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref double M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Creates a new <see cref="Double1x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double1x3"/> instance.</param>
        public static implicit operator Double1x3(double x)
        {
            Double1x3 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double1x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double1x3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x3 operator -(Double1x3 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double1x3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x3 operator +(Double1x3 left, Double1x3 right) => default;

        /// <summary>
        /// Divides two <see cref="Double1x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double1x3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x3 operator /(Double1x3 left, Double1x3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double1x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double1x3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x3 operator *(Double1x3 left, Double1x3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double1x3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x3 operator -(Double1x3 left, Double1x3 right) => default;

        /// <summary>
        /// Casts a <see cref="Double3"/> value to a <see cref="Double1x3"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Double3"/> value to cast.</param>
        public static implicit operator Double1x3(Double3 vector) => *(Double1x3*)&vector;
    }

    /// <inheritdoc cref="Double1x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 8)]
    public unsafe partial struct Double1x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double1x4), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m13;

        [FieldOffset(24)]
        private double m14;

        /// <summary>
        /// Creates a new <see cref="Double1x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        public Double1x4(double m11, double m12, double m13, double m14)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double1x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[int row] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref double M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref double M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Creates a new <see cref="Double1x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double1x4"/> instance.</param>
        public static implicit operator Double1x4(double x)
        {
            Double1x4 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;
            matrix.m14 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double1x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double1x4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x4 operator -(Double1x4 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double1x4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x4 operator +(Double1x4 left, Double1x4 right) => default;

        /// <summary>
        /// Divides two <see cref="Double1x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double1x4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x4 operator /(Double1x4 left, Double1x4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double1x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double1x4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x4 operator *(Double1x4 left, Double1x4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double1x4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double1x4 operator -(Double1x4 left, Double1x4 right) => default;

        /// <summary>
        /// Casts a <see cref="Double4"/> value to a <see cref="Double1x4"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Double4"/> value to cast.</param>
        public static implicit operator Double1x4(Double4 vector) => *(Double1x4*)&vector;
    }

    /// <inheritdoc cref="Double2x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
    public unsafe partial struct Double2x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double2x1), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m21;

        /// <summary>
        /// Creates a new <see cref="Double2x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        public Double2x1(double m11, double m21)
        {
            this.m11 = m11;
            this.m21 = m21;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double2x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref double this[int row] => ref *(double*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Creates a new <see cref="Double2x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double2x1"/> instance.</param>
        public static implicit operator Double2x1(double x)
        {
            Double2x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double2x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double2x1"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x1 operator -(Double2x1 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2x1"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x1 operator +(Double2x1 left, Double2x1 right) => default;

        /// <summary>
        /// Divides two <see cref="Double2x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2x1"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x1 operator /(Double2x1 left, Double2x1 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double2x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2x1"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x1 operator *(Double2x1 left, Double2x1 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2x1"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x1 operator -(Double2x1 left, Double2x1 right) => default;

        /// <summary>
        /// Casts a <see cref="Double2x1"/> value to a <see cref="Double2"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Double2x1"/> value to cast.</param>
        public static implicit operator Double2(Double2x1 matrix) => *(Double2*)&matrix;
    }

    /// <inheritdoc cref="Double2x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 8)]
    public unsafe partial struct Double2x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double2x2), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m21;

        [FieldOffset(24)]
        private double m22;

        /// <summary>
        /// Creates a new <see cref="Double2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        public Double2x2(double m11, double m12, double m21, double m22)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
        }

        /// <summary>
        /// Creates a new <see cref="Double2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Double2x2(Double2 row1, Double2 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double2x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[int row] => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Creates a new <see cref="Double2x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double2x2"/> instance.</param>
        public static implicit operator Double2x2(double x)
        {
            Double2x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m21 = x;
            matrix.m22 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double2x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double2x2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x2 operator -(Double2x2 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2x2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x2 operator +(Double2x2 left, Double2x2 right) => default;

        /// <summary>
        /// Divides two <see cref="Double2x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2x2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x2 operator /(Double2x2 left, Double2x2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double2x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2x2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x2 operator *(Double2x2 left, Double2x2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2x2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x2 operator -(Double2x2 left, Double2x2 right) => default;
    }

    /// <inheritdoc cref="Double2x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 48, Pack = 8)]
    public unsafe partial struct Double2x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double2x3), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m13;

        [FieldOffset(24)]
        private double m21;

        [FieldOffset(32)]
        private double m22;

        [FieldOffset(40)]
        private double m23;

        /// <summary>
        /// Creates a new <see cref="Double2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        public Double2x3(double m11, double m12, double m13, double m21, double m22, double m23)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
        }

        /// <summary>
        /// Creates a new <see cref="Double2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Double2x3(Double3 row1, Double3 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m13 = row1.Z;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m23 = row2.Z;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double2x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[int row] => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref double M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref double M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Creates a new <see cref="Double2x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double2x3"/> instance.</param>
        public static implicit operator Double2x3(double x)
        {
            Double2x3 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;
            matrix.m21 = x;
            matrix.m22 = x;
            matrix.m23 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double2x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double2x3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x3 operator -(Double2x3 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2x3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x3 operator +(Double2x3 left, Double2x3 right) => default;

        /// <summary>
        /// Divides two <see cref="Double2x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2x3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x3 operator /(Double2x3 left, Double2x3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double2x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2x3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x3 operator *(Double2x3 left, Double2x3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2x3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x3 operator -(Double2x3 left, Double2x3 right) => default;
    }

    /// <inheritdoc cref="Double2x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 64, Pack = 8)]
    public unsafe partial struct Double2x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double2x4), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m13;

        [FieldOffset(24)]
        private double m14;

        [FieldOffset(32)]
        private double m21;

        [FieldOffset(40)]
        private double m22;

        [FieldOffset(48)]
        private double m23;

        [FieldOffset(56)]
        private double m24;

        /// <summary>
        /// Creates a new <see cref="Double2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        /// <param name="m24">The value to assign to the component at position [2, 4].</param>
        public Double2x4(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24)
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
        /// Creates a new <see cref="Double2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Double2x4(Double4 row1, Double4 row2)
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
        /// Gets a reference to a specific row in the current <see cref="Double2x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[int row] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref double M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref double M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref double M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref double M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Creates a new <see cref="Double2x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double2x4"/> instance.</param>
        public static implicit operator Double2x4(double x)
        {
            Double2x4 matrix;

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
        /// Negates a <see cref="Double2x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double2x4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x4 operator -(Double2x4 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2x4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x4 operator +(Double2x4 left, Double2x4 right) => default;

        /// <summary>
        /// Divides two <see cref="Double2x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2x4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x4 operator /(Double2x4 left, Double2x4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double2x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2x4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x4 operator *(Double2x4 left, Double2x4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2x4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double2x4 operator -(Double2x4 left, Double2x4 right) => default;
    }

    /// <inheritdoc cref="Double3x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
    public unsafe partial struct Double3x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double3x1), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m21;

        [FieldOffset(16)]
        private double m31;

        /// <summary>
        /// Creates a new <see cref="Double3x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        public Double3x1(double m11, double m21, double m31)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double3x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref double this[int row] => ref *(double*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref double M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Creates a new <see cref="Double3x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double3x1"/> instance.</param>
        public static implicit operator Double3x1(double x)
        {
            Double3x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;
            matrix.m31 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double3x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double3x1"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x1 operator -(Double3x1 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3x1"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x1 operator +(Double3x1 left, Double3x1 right) => default;

        /// <summary>
        /// Divides two <see cref="Double3x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3x1"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x1 operator /(Double3x1 left, Double3x1 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double3x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3x1"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x1 operator *(Double3x1 left, Double3x1 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3x1"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x1 operator -(Double3x1 left, Double3x1 right) => default;

        /// <summary>
        /// Casts a <see cref="Double3x1"/> value to a <see cref="Double3"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Double3x1"/> value to cast.</param>
        public static implicit operator Double3(Double3x1 matrix) => *(Double3*)&matrix;
    }

    /// <inheritdoc cref="Double3x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 48, Pack = 8)]
    public unsafe partial struct Double3x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double3x2), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m21;

        [FieldOffset(24)]
        private double m22;

        [FieldOffset(32)]
        private double m31;

        [FieldOffset(40)]
        private double m32;

        /// <summary>
        /// Creates a new <see cref="Double3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        public Double3x2(double m11, double m12, double m21, double m22, double m31, double m32)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.m31 = m31;
            this.m32 = m32;
        }

        /// <summary>
        /// Creates a new <see cref="Double3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Double3x2(Double2 row1, Double2 row2, Double2 row3)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m31 = row3.X;
            this.m32 = row3.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double3x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[int row] => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref double M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref double M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Creates a new <see cref="Double3x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double3x2"/> instance.</param>
        public static implicit operator Double3x2(double x)
        {
            Double3x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m21 = x;
            matrix.m22 = x;
            matrix.m31 = x;
            matrix.m32 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double3x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double3x2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x2 operator -(Double3x2 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3x2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x2 operator +(Double3x2 left, Double3x2 right) => default;

        /// <summary>
        /// Divides two <see cref="Double3x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3x2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x2 operator /(Double3x2 left, Double3x2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double3x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3x2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x2 operator *(Double3x2 left, Double3x2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3x2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x2 operator -(Double3x2 left, Double3x2 right) => default;
    }

    /// <inheritdoc cref="Double3x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 72, Pack = 8)]
    public unsafe partial struct Double3x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double3x3), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m13;

        [FieldOffset(24)]
        private double m21;

        [FieldOffset(32)]
        private double m22;

        [FieldOffset(40)]
        private double m23;

        [FieldOffset(48)]
        private double m31;

        [FieldOffset(56)]
        private double m32;

        [FieldOffset(64)]
        private double m33;

        /// <summary>
        /// Creates a new <see cref="Double3x3"/> instance with the specified parameters.
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
        public Double3x3(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33)
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
        /// Creates a new <see cref="Double3x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Double3x3(Double3 row1, Double3 row2, Double3 row3)
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
        /// Gets a reference to a specific row in the current <see cref="Double3x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[int row] => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref double M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref double M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref double M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref double M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref double M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Creates a new <see cref="Double3x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double3x3"/> instance.</param>
        public static implicit operator Double3x3(double x)
        {
            Double3x3 matrix;

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
        /// Negates a <see cref="Double3x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double3x3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x3 operator -(Double3x3 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3x3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x3 operator +(Double3x3 left, Double3x3 right) => default;

        /// <summary>
        /// Divides two <see cref="Double3x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3x3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x3 operator /(Double3x3 left, Double3x3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double3x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3x3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x3 operator *(Double3x3 left, Double3x3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3x3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x3 operator -(Double3x3 left, Double3x3 right) => default;
    }

    /// <inheritdoc cref="Double3x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 96, Pack = 8)]
    public unsafe partial struct Double3x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double3x4), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m13;

        [FieldOffset(24)]
        private double m14;

        [FieldOffset(32)]
        private double m21;

        [FieldOffset(40)]
        private double m22;

        [FieldOffset(48)]
        private double m23;

        [FieldOffset(56)]
        private double m24;

        [FieldOffset(64)]
        private double m31;

        [FieldOffset(72)]
        private double m32;

        [FieldOffset(80)]
        private double m33;

        [FieldOffset(88)]
        private double m34;

        /// <summary>
        /// Creates a new <see cref="Double3x4"/> instance with the specified parameters.
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
        public Double3x4(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34)
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
        /// Creates a new <see cref="Double3x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Double3x4(Double4 row1, Double4 row2, Double4 row3)
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
        /// Gets a reference to a specific row in the current <see cref="Double3x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[int row] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref double M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref double M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref double M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref double M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref double M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref double M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref double M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 4].
        /// </summary>
        public readonly ref double M34 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m34), 1));

        /// <summary>
        /// Creates a new <see cref="Double3x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double3x4"/> instance.</param>
        public static implicit operator Double3x4(double x)
        {
            Double3x4 matrix;

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
        /// Negates a <see cref="Double3x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double3x4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x4 operator -(Double3x4 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3x4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x4 operator +(Double3x4 left, Double3x4 right) => default;

        /// <summary>
        /// Divides two <see cref="Double3x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3x4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x4 operator /(Double3x4 left, Double3x4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double3x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3x4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x4 operator *(Double3x4 left, Double3x4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3x4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double3x4 operator -(Double3x4 left, Double3x4 right) => default;
    }

    /// <inheritdoc cref="Double4x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 8)]
    public unsafe partial struct Double4x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double4x1), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m21;

        [FieldOffset(16)]
        private double m31;

        [FieldOffset(24)]
        private double m41;

        /// <summary>
        /// Creates a new <see cref="Double4x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        public Double4x1(double m11, double m21, double m31, double m41)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
            this.m41 = m41;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Double4x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref double this[int row] => ref *(double*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref double M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref double M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Creates a new <see cref="Double4x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double4x1"/> instance.</param>
        public static implicit operator Double4x1(double x)
        {
            Double4x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;
            matrix.m31 = x;
            matrix.m41 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Double4x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double4x1"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x1 operator -(Double4x1 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4x1"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x1 operator +(Double4x1 left, Double4x1 right) => default;

        /// <summary>
        /// Divides two <see cref="Double4x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4x1"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x1 operator /(Double4x1 left, Double4x1 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double4x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4x1"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x1 operator *(Double4x1 left, Double4x1 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4x1"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x1 operator -(Double4x1 left, Double4x1 right) => default;

        /// <summary>
        /// Casts a <see cref="Double4x1"/> value to a <see cref="Double4"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Double4x1"/> value to cast.</param>
        public static implicit operator Double4(Double4x1 matrix) => *(Double4*)&matrix;
    }

    /// <inheritdoc cref="Double4x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 64, Pack = 8)]
    public unsafe partial struct Double4x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double4x2), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m21;

        [FieldOffset(24)]
        private double m22;

        [FieldOffset(32)]
        private double m31;

        [FieldOffset(40)]
        private double m32;

        [FieldOffset(48)]
        private double m41;

        [FieldOffset(56)]
        private double m42;

        /// <summary>
        /// Creates a new <see cref="Double4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        /// <param name="m42">The value to assign to the component at position [4, 2].</param>
        public Double4x2(double m11, double m12, double m21, double m22, double m31, double m32, double m41, double m42)
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
        /// Creates a new <see cref="Double4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Double4x2(Double2 row1, Double2 row2, Double2 row3, Double2 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Double4x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[int row] => ref *(Double2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref double M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref double M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref double M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref double M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Creates a new <see cref="Double4x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double4x2"/> instance.</param>
        public static implicit operator Double4x2(double x)
        {
            Double4x2 matrix;

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
        /// Negates a <see cref="Double4x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double4x2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x2 operator -(Double4x2 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4x2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x2 operator +(Double4x2 left, Double4x2 right) => default;

        /// <summary>
        /// Divides two <see cref="Double4x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4x2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x2 operator /(Double4x2 left, Double4x2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double4x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4x2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x2 operator *(Double4x2 left, Double4x2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4x2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x2 operator -(Double4x2 left, Double4x2 right) => default;
    }

    /// <inheritdoc cref="Double4x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 96, Pack = 8)]
    public unsafe partial struct Double4x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double4x3), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m13;

        [FieldOffset(24)]
        private double m21;

        [FieldOffset(32)]
        private double m22;

        [FieldOffset(40)]
        private double m23;

        [FieldOffset(48)]
        private double m31;

        [FieldOffset(56)]
        private double m32;

        [FieldOffset(64)]
        private double m33;

        [FieldOffset(72)]
        private double m41;

        [FieldOffset(80)]
        private double m42;

        [FieldOffset(88)]
        private double m43;

        /// <summary>
        /// Creates a new <see cref="Double4x3"/> instance with the specified parameters.
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
        public Double4x3(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33, double m41, double m42, double m43)
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
        /// Creates a new <see cref="Double4x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Double4x3(Double3 row1, Double3 row2, Double3 row3, Double3 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Double4x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[int row] => ref *(Double3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref double M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref double M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref double M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref double M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref double M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref double M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref double M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 3].
        /// </summary>
        public readonly ref double M43 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m43), 1));

        /// <summary>
        /// Creates a new <see cref="Double4x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double4x3"/> instance.</param>
        public static implicit operator Double4x3(double x)
        {
            Double4x3 matrix;

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
        /// Negates a <see cref="Double4x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double4x3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x3 operator -(Double4x3 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4x3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x3 operator +(Double4x3 left, Double4x3 right) => default;

        /// <summary>
        /// Divides two <see cref="Double4x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4x3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x3 operator /(Double4x3 left, Double4x3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double4x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4x3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x3 operator *(Double4x3 left, Double4x3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4x3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x3 operator -(Double4x3 left, Double4x3 right) => default;
    }

    /// <inheritdoc cref="Double4x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 128, Pack = 8)]
    public unsafe partial struct Double4x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Double4x4), sizeof(Double4));

        [FieldOffset(0)]
        private double m11;

        [FieldOffset(8)]
        private double m12;

        [FieldOffset(16)]
        private double m13;

        [FieldOffset(24)]
        private double m14;

        [FieldOffset(32)]
        private double m21;

        [FieldOffset(40)]
        private double m22;

        [FieldOffset(48)]
        private double m23;

        [FieldOffset(56)]
        private double m24;

        [FieldOffset(64)]
        private double m31;

        [FieldOffset(72)]
        private double m32;

        [FieldOffset(80)]
        private double m33;

        [FieldOffset(88)]
        private double m34;

        [FieldOffset(96)]
        private double m41;

        [FieldOffset(104)]
        private double m42;

        [FieldOffset(112)]
        private double m43;

        [FieldOffset(120)]
        private double m44;

        /// <summary>
        /// Creates a new <see cref="Double4x4"/> instance with the specified parameters.
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
        public Double4x4(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
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
        /// Creates a new <see cref="Double4x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Double4x4(Double4 row1, Double4 row2, Double4 row3, Double4 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Double4x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[int row] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Double2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Double3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Double4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref double M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref double M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref double M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref double M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref double M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref double M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref double M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref double M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref double M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref double M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref double M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [3, 4].
        /// </summary>
        public readonly ref double M34 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m34), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref double M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref double M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 3].
        /// </summary>
        public readonly ref double M43 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m43), 1));

        /// <summary>
        /// Gets a reference to the <see cref="double"/> value representing the component at position [4, 4].
        /// </summary>
        public readonly ref double M44 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m44), 1));

        /// <summary>
        /// Creates a new <see cref="Double4x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double4x4"/> instance.</param>
        public static implicit operator Double4x4(double x)
        {
            Double4x4 matrix;

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
        /// Negates a <see cref="Double4x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double4x4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x4 operator -(Double4x4 matrix) => default;

        /// <summary>
        /// Sums two <see cref="Double4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4x4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x4 operator +(Double4x4 left, Double4x4 right) => default;

        /// <summary>
        /// Divides two <see cref="Double4x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4x4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x4 operator /(Double4x4 left, Double4x4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="Double4x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4x4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x4 operator *(Double4x4 left, Double4x4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="Double4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4x4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Double4x4 operator -(Double4x4 left, Double4x4 right) => default;
    }
}
