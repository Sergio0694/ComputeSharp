using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Double1x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Double1x1
    {
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
        public ref double this[int row] => throw new InvalidExecutionContextException($"{nameof(Double1x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double1x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double1x1"/> value to negate.</param>
        public static Double1x1 operator -(Double1x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Double1x1)}.-");

        /// <summary>
        /// Sums two <see cref="Double1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double1x1"/> value to sum.</param>
        public static Double1x1 operator +(Double1x1 left, Double1x1 right) => throw new InvalidExecutionContextException($"{nameof(Double1x1)}.+");

        /// <summary>
        /// Divides two <see cref="Double1x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double1x1"/> value to divide.</param>
        public static Double1x1 operator /(Double1x1 left, Double1x1 right) => throw new InvalidExecutionContextException($"{nameof(Double1x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Double1x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double1x1"/> value to multiply.</param>
        public static Double1x1 operator *(Double1x1 left, Double1x1 right) => throw new InvalidExecutionContextException($"{nameof(Double1x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double1x1"/> value to subtract.</param>
        public static Double1x1 operator -(Double1x1 left, Double1x1 right) => throw new InvalidExecutionContextException($"{nameof(Double1x1)}.-");
    }

    /// <inheritdoc cref="Double1x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Double1x2
    {
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
        public ref Double2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double1x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double1x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double1x2"/> value to negate.</param>
        public static Double1x2 operator -(Double1x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Double1x2)}.-");

        /// <summary>
        /// Sums two <see cref="Double1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double1x2"/> value to sum.</param>
        public static Double1x2 operator +(Double1x2 left, Double1x2 right) => throw new InvalidExecutionContextException($"{nameof(Double1x2)}.+");

        /// <summary>
        /// Divides two <see cref="Double1x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double1x2"/> value to divide.</param>
        public static Double1x2 operator /(Double1x2 left, Double1x2 right) => throw new InvalidExecutionContextException($"{nameof(Double1x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Double1x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double1x2"/> value to multiply.</param>
        public static Double1x2 operator *(Double1x2 left, Double1x2 right) => throw new InvalidExecutionContextException($"{nameof(Double1x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double1x2"/> value to subtract.</param>
        public static Double1x2 operator -(Double1x2 left, Double1x2 right) => throw new InvalidExecutionContextException($"{nameof(Double1x2)}.-");

        /// <summary>
        /// Casts a <see cref="Double2"/> value to a <see cref="Double1x2"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Double2"/> value to cast.</param>
        public static implicit operator Double1x2(Double2 vector) => throw new InvalidExecutionContextException($"{typeof(Double1x2)}.{nameof(Double1x2)}({nameof(Double2)})");
    }

    /// <inheritdoc cref="Double1x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct Double1x3
    {
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
        public ref Double3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double1x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double1x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double1x3"/> value to negate.</param>
        public static Double1x3 operator -(Double1x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Double1x3)}.-");

        /// <summary>
        /// Sums two <see cref="Double1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double1x3"/> value to sum.</param>
        public static Double1x3 operator +(Double1x3 left, Double1x3 right) => throw new InvalidExecutionContextException($"{nameof(Double1x3)}.+");

        /// <summary>
        /// Divides two <see cref="Double1x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double1x3"/> value to divide.</param>
        public static Double1x3 operator /(Double1x3 left, Double1x3 right) => throw new InvalidExecutionContextException($"{nameof(Double1x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Double1x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double1x3"/> value to multiply.</param>
        public static Double1x3 operator *(Double1x3 left, Double1x3 right) => throw new InvalidExecutionContextException($"{nameof(Double1x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double1x3"/> value to subtract.</param>
        public static Double1x3 operator -(Double1x3 left, Double1x3 right) => throw new InvalidExecutionContextException($"{nameof(Double1x3)}.-");

        /// <summary>
        /// Casts a <see cref="Double3"/> value to a <see cref="Double1x3"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Double3"/> value to cast.</param>
        public static implicit operator Double1x3(Double3 vector) => throw new InvalidExecutionContextException($"{typeof(Double1x3)}.{nameof(Double1x3)}({nameof(Double3)})");
    }

    /// <inheritdoc cref="Double1x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct Double1x4
    {
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
        public ref Double4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double1x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double1x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double1x4"/> value to negate.</param>
        public static Double1x4 operator -(Double1x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Double1x4)}.-");

        /// <summary>
        /// Sums two <see cref="Double1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double1x4"/> value to sum.</param>
        public static Double1x4 operator +(Double1x4 left, Double1x4 right) => throw new InvalidExecutionContextException($"{nameof(Double1x4)}.+");

        /// <summary>
        /// Divides two <see cref="Double1x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double1x4"/> value to divide.</param>
        public static Double1x4 operator /(Double1x4 left, Double1x4 right) => throw new InvalidExecutionContextException($"{nameof(Double1x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Double1x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double1x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double1x4"/> value to multiply.</param>
        public static Double1x4 operator *(Double1x4 left, Double1x4 right) => throw new InvalidExecutionContextException($"{nameof(Double1x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double1x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double1x4"/> value to subtract.</param>
        public static Double1x4 operator -(Double1x4 left, Double1x4 right) => throw new InvalidExecutionContextException($"{nameof(Double1x4)}.-");

        /// <summary>
        /// Casts a <see cref="Double4"/> value to a <see cref="Double1x4"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Double4"/> value to cast.</param>
        public static implicit operator Double1x4(Double4 vector) => throw new InvalidExecutionContextException($"{typeof(Double1x4)}.{nameof(Double1x4)}({nameof(Double4)})");
    }

    /// <inheritdoc cref="Double2x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Double2x1
    {
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
        public ref double this[int row] => throw new InvalidExecutionContextException($"{nameof(Double2x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double2x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double2x1"/> value to negate.</param>
        public static Double2x1 operator -(Double2x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Double2x1)}.-");

        /// <summary>
        /// Sums two <see cref="Double2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2x1"/> value to sum.</param>
        public static Double2x1 operator +(Double2x1 left, Double2x1 right) => throw new InvalidExecutionContextException($"{nameof(Double2x1)}.+");

        /// <summary>
        /// Divides two <see cref="Double2x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2x1"/> value to divide.</param>
        public static Double2x1 operator /(Double2x1 left, Double2x1 right) => throw new InvalidExecutionContextException($"{nameof(Double2x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Double2x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2x1"/> value to multiply.</param>
        public static Double2x1 operator *(Double2x1 left, Double2x1 right) => throw new InvalidExecutionContextException($"{nameof(Double2x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2x1"/> value to subtract.</param>
        public static Double2x1 operator -(Double2x1 left, Double2x1 right) => throw new InvalidExecutionContextException($"{nameof(Double2x1)}.-");

        /// <summary>
        /// Casts a <see cref="Double2x1"/> value to a <see cref="Double2"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Double2x1"/> value to cast.</param>
        public static implicit operator Double2(Double2x1 matrix) => throw new InvalidExecutionContextException($"{typeof(Double2x1)}.{nameof(Double2)}({nameof(Double2x1)})");
    }

    /// <inheritdoc cref="Double2x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct Double2x2
    {
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
        public ref Double2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double2x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double2x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double2x2"/> value to negate.</param>
        public static Double2x2 operator -(Double2x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Double2x2)}.-");

        /// <summary>
        /// Sums two <see cref="Double2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2x2"/> value to sum.</param>
        public static Double2x2 operator +(Double2x2 left, Double2x2 right) => throw new InvalidExecutionContextException($"{nameof(Double2x2)}.+");

        /// <summary>
        /// Divides two <see cref="Double2x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2x2"/> value to divide.</param>
        public static Double2x2 operator /(Double2x2 left, Double2x2 right) => throw new InvalidExecutionContextException($"{nameof(Double2x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Double2x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2x2"/> value to multiply.</param>
        public static Double2x2 operator *(Double2x2 left, Double2x2 right) => throw new InvalidExecutionContextException($"{nameof(Double2x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2x2"/> value to subtract.</param>
        public static Double2x2 operator -(Double2x2 left, Double2x2 right) => throw new InvalidExecutionContextException($"{nameof(Double2x2)}.-");
    }

    /// <inheritdoc cref="Double2x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public partial struct Double2x3
    {
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
        public ref Double3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double2x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double2x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double2x3"/> value to negate.</param>
        public static Double2x3 operator -(Double2x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Double2x3)}.-");

        /// <summary>
        /// Sums two <see cref="Double2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2x3"/> value to sum.</param>
        public static Double2x3 operator +(Double2x3 left, Double2x3 right) => throw new InvalidExecutionContextException($"{nameof(Double2x3)}.+");

        /// <summary>
        /// Divides two <see cref="Double2x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2x3"/> value to divide.</param>
        public static Double2x3 operator /(Double2x3 left, Double2x3 right) => throw new InvalidExecutionContextException($"{nameof(Double2x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Double2x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2x3"/> value to multiply.</param>
        public static Double2x3 operator *(Double2x3 left, Double2x3 right) => throw new InvalidExecutionContextException($"{nameof(Double2x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2x3"/> value to subtract.</param>
        public static Double2x3 operator -(Double2x3 left, Double2x3 right) => throw new InvalidExecutionContextException($"{nameof(Double2x3)}.-");
    }

    /// <inheritdoc cref="Double2x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public partial struct Double2x4
    {
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
        public ref Double4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double2x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double2x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double2x4"/> value to negate.</param>
        public static Double2x4 operator -(Double2x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Double2x4)}.-");

        /// <summary>
        /// Sums two <see cref="Double2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2x4"/> value to sum.</param>
        public static Double2x4 operator +(Double2x4 left, Double2x4 right) => throw new InvalidExecutionContextException($"{nameof(Double2x4)}.+");

        /// <summary>
        /// Divides two <see cref="Double2x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2x4"/> value to divide.</param>
        public static Double2x4 operator /(Double2x4 left, Double2x4 right) => throw new InvalidExecutionContextException($"{nameof(Double2x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Double2x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double2x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2x4"/> value to multiply.</param>
        public static Double2x4 operator *(Double2x4 left, Double2x4 right) => throw new InvalidExecutionContextException($"{nameof(Double2x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2x4"/> value to subtract.</param>
        public static Double2x4 operator -(Double2x4 left, Double2x4 right) => throw new InvalidExecutionContextException($"{nameof(Double2x4)}.-");
    }

    /// <inheritdoc cref="Double3x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct Double3x1
    {
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
        public ref double this[int row] => throw new InvalidExecutionContextException($"{nameof(Double3x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double3x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double3x1"/> value to negate.</param>
        public static Double3x1 operator -(Double3x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Double3x1)}.-");

        /// <summary>
        /// Sums two <see cref="Double3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3x1"/> value to sum.</param>
        public static Double3x1 operator +(Double3x1 left, Double3x1 right) => throw new InvalidExecutionContextException($"{nameof(Double3x1)}.+");

        /// <summary>
        /// Divides two <see cref="Double3x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3x1"/> value to divide.</param>
        public static Double3x1 operator /(Double3x1 left, Double3x1 right) => throw new InvalidExecutionContextException($"{nameof(Double3x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Double3x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3x1"/> value to multiply.</param>
        public static Double3x1 operator *(Double3x1 left, Double3x1 right) => throw new InvalidExecutionContextException($"{nameof(Double3x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3x1"/> value to subtract.</param>
        public static Double3x1 operator -(Double3x1 left, Double3x1 right) => throw new InvalidExecutionContextException($"{nameof(Double3x1)}.-");

        /// <summary>
        /// Casts a <see cref="Double3x1"/> value to a <see cref="Double3"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Double3x1"/> value to cast.</param>
        public static implicit operator Double3(Double3x1 matrix) => throw new InvalidExecutionContextException($"{typeof(Double3x1)}.{nameof(Double3)}({nameof(Double3x1)})");
    }

    /// <inheritdoc cref="Double3x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public partial struct Double3x2
    {
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
        public ref Double2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double3x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double3x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double3x2"/> value to negate.</param>
        public static Double3x2 operator -(Double3x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Double3x2)}.-");

        /// <summary>
        /// Sums two <see cref="Double3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3x2"/> value to sum.</param>
        public static Double3x2 operator +(Double3x2 left, Double3x2 right) => throw new InvalidExecutionContextException($"{nameof(Double3x2)}.+");

        /// <summary>
        /// Divides two <see cref="Double3x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3x2"/> value to divide.</param>
        public static Double3x2 operator /(Double3x2 left, Double3x2 right) => throw new InvalidExecutionContextException($"{nameof(Double3x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Double3x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3x2"/> value to multiply.</param>
        public static Double3x2 operator *(Double3x2 left, Double3x2 right) => throw new InvalidExecutionContextException($"{nameof(Double3x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3x2"/> value to subtract.</param>
        public static Double3x2 operator -(Double3x2 left, Double3x2 right) => throw new InvalidExecutionContextException($"{nameof(Double3x2)}.-");
    }

    /// <inheritdoc cref="Double3x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 72)]
    public partial struct Double3x3
    {
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
        public ref Double3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double3x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double3x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double3x3"/> value to negate.</param>
        public static Double3x3 operator -(Double3x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Double3x3)}.-");

        /// <summary>
        /// Sums two <see cref="Double3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3x3"/> value to sum.</param>
        public static Double3x3 operator +(Double3x3 left, Double3x3 right) => throw new InvalidExecutionContextException($"{nameof(Double3x3)}.+");

        /// <summary>
        /// Divides two <see cref="Double3x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3x3"/> value to divide.</param>
        public static Double3x3 operator /(Double3x3 left, Double3x3 right) => throw new InvalidExecutionContextException($"{nameof(Double3x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Double3x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3x3"/> value to multiply.</param>
        public static Double3x3 operator *(Double3x3 left, Double3x3 right) => throw new InvalidExecutionContextException($"{nameof(Double3x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3x3"/> value to subtract.</param>
        public static Double3x3 operator -(Double3x3 left, Double3x3 right) => throw new InvalidExecutionContextException($"{nameof(Double3x3)}.-");
    }

    /// <inheritdoc cref="Double3x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 96)]
    public partial struct Double3x4
    {
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
        public ref Double4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double3x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double3x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double3x4"/> value to negate.</param>
        public static Double3x4 operator -(Double3x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Double3x4)}.-");

        /// <summary>
        /// Sums two <see cref="Double3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double3x4"/> value to sum.</param>
        public static Double3x4 operator +(Double3x4 left, Double3x4 right) => throw new InvalidExecutionContextException($"{nameof(Double3x4)}.+");

        /// <summary>
        /// Divides two <see cref="Double3x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double3x4"/> value to divide.</param>
        public static Double3x4 operator /(Double3x4 left, Double3x4 right) => throw new InvalidExecutionContextException($"{nameof(Double3x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Double3x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double3x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double3x4"/> value to multiply.</param>
        public static Double3x4 operator *(Double3x4 left, Double3x4 right) => throw new InvalidExecutionContextException($"{nameof(Double3x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double3x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double3x4"/> value to subtract.</param>
        public static Double3x4 operator -(Double3x4 left, Double3x4 right) => throw new InvalidExecutionContextException($"{nameof(Double3x4)}.-");
    }

    /// <inheritdoc cref="Double4x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct Double4x1
    {
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
        public ref double this[int row] => throw new InvalidExecutionContextException($"{nameof(Double4x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double4x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double4x1"/> value to negate.</param>
        public static Double4x1 operator -(Double4x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Double4x1)}.-");

        /// <summary>
        /// Sums two <see cref="Double4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4x1"/> value to sum.</param>
        public static Double4x1 operator +(Double4x1 left, Double4x1 right) => throw new InvalidExecutionContextException($"{nameof(Double4x1)}.+");

        /// <summary>
        /// Divides two <see cref="Double4x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4x1"/> value to divide.</param>
        public static Double4x1 operator /(Double4x1 left, Double4x1 right) => throw new InvalidExecutionContextException($"{nameof(Double4x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Double4x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4x1"/> value to multiply.</param>
        public static Double4x1 operator *(Double4x1 left, Double4x1 right) => throw new InvalidExecutionContextException($"{nameof(Double4x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4x1"/> value to subtract.</param>
        public static Double4x1 operator -(Double4x1 left, Double4x1 right) => throw new InvalidExecutionContextException($"{nameof(Double4x1)}.-");

        /// <summary>
        /// Casts a <see cref="Double4x1"/> value to a <see cref="Double4"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Double4x1"/> value to cast.</param>
        public static implicit operator Double4(Double4x1 matrix) => throw new InvalidExecutionContextException($"{typeof(Double4x1)}.{nameof(Double4)}({nameof(Double4x1)})");
    }

    /// <inheritdoc cref="Double4x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public partial struct Double4x2
    {
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
        public ref Double2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double4x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double4x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double4x2"/> value to negate.</param>
        public static Double4x2 operator -(Double4x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Double4x2)}.-");

        /// <summary>
        /// Sums two <see cref="Double4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4x2"/> value to sum.</param>
        public static Double4x2 operator +(Double4x2 left, Double4x2 right) => throw new InvalidExecutionContextException($"{nameof(Double4x2)}.+");

        /// <summary>
        /// Divides two <see cref="Double4x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4x2"/> value to divide.</param>
        public static Double4x2 operator /(Double4x2 left, Double4x2 right) => throw new InvalidExecutionContextException($"{nameof(Double4x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Double4x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4x2"/> value to multiply.</param>
        public static Double4x2 operator *(Double4x2 left, Double4x2 right) => throw new InvalidExecutionContextException($"{nameof(Double4x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4x2"/> value to subtract.</param>
        public static Double4x2 operator -(Double4x2 left, Double4x2 right) => throw new InvalidExecutionContextException($"{nameof(Double4x2)}.-");
    }

    /// <inheritdoc cref="Double4x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 96)]
    public partial struct Double4x3
    {
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
        public ref Double3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double4x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double4x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double4x3"/> value to negate.</param>
        public static Double4x3 operator -(Double4x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Double4x3)}.-");

        /// <summary>
        /// Sums two <see cref="Double4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4x3"/> value to sum.</param>
        public static Double4x3 operator +(Double4x3 left, Double4x3 right) => throw new InvalidExecutionContextException($"{nameof(Double4x3)}.+");

        /// <summary>
        /// Divides two <see cref="Double4x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4x3"/> value to divide.</param>
        public static Double4x3 operator /(Double4x3 left, Double4x3 right) => throw new InvalidExecutionContextException($"{nameof(Double4x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Double4x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4x3"/> value to multiply.</param>
        public static Double4x3 operator *(Double4x3 left, Double4x3 right) => throw new InvalidExecutionContextException($"{nameof(Double4x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4x3"/> value to subtract.</param>
        public static Double4x3 operator -(Double4x3 left, Double4x3 right) => throw new InvalidExecutionContextException($"{nameof(Double4x3)}.-");
    }

    /// <inheritdoc cref="Double4x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public partial struct Double4x4
    {
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
        public ref Double4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Double4x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Double2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Double4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Double3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Double4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Double4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Double4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Double4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Double4x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Double4x4"/> value to negate.</param>
        public static Double4x4 operator -(Double4x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Double4x4)}.-");

        /// <summary>
        /// Sums two <see cref="Double4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double4x4"/> value to sum.</param>
        public static Double4x4 operator +(Double4x4 left, Double4x4 right) => throw new InvalidExecutionContextException($"{nameof(Double4x4)}.+");

        /// <summary>
        /// Divides two <see cref="Double4x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double4x4"/> value to divide.</param>
        public static Double4x4 operator /(Double4x4 left, Double4x4 right) => throw new InvalidExecutionContextException($"{nameof(Double4x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Double4x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Double4x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double4x4"/> value to multiply.</param>
        public static Double4x4 operator *(Double4x4 left, Double4x4 right) => throw new InvalidExecutionContextException($"{nameof(Double4x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double4x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double4x4"/> value to subtract.</param>
        public static Double4x4 operator -(Double4x4 left, Double4x4 right) => throw new InvalidExecutionContextException($"{nameof(Double4x4)}.-");
    }
}
