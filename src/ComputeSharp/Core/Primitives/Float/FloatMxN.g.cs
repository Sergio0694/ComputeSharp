using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Float1x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public partial struct Float1x1
    {
        [FieldOffset(0)]
        private float m11;

        /// <summary>
        /// Creates a new <see cref="Float1x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        public Float1x1(float m11)
        {
            this.m11 = m11;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float1x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref float this[int row] => throw new InvalidExecutionContextException($"{nameof(Float1x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float1x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float1x1"/> instance.</param>
        public static implicit operator Float1x1(float x)
        {
            Float1x1 matrix;

            matrix.m11 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float1x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float1x1"/> value to negate.</param>
        public static Float1x1 operator -(Float1x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Float1x1)}.-");

        /// <summary>
        /// Sums two <see cref="Float1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float1x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float1x1"/> value to sum.</param>
        public static Float1x1 operator +(Float1x1 left, Float1x1 right) => throw new InvalidExecutionContextException($"{nameof(Float1x1)}.+");

        /// <summary>
        /// Divides two <see cref="Float1x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float1x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float1x1"/> value to divide.</param>
        public static Float1x1 operator /(Float1x1 left, Float1x1 right) => throw new InvalidExecutionContextException($"{nameof(Float1x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Float1x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float1x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float1x1"/> value to multiply.</param>
        public static Float1x1 operator *(Float1x1 left, Float1x1 right) => throw new InvalidExecutionContextException($"{nameof(Float1x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float1x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float1x1"/> value to subtract.</param>
        public static Float1x1 operator -(Float1x1 left, Float1x1 right) => throw new InvalidExecutionContextException($"{nameof(Float1x1)}.-");
    }

    /// <inheritdoc cref="Float1x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Float1x2
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        /// <summary>
        /// Creates a new <see cref="Float1x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        public Float1x2(float m11, float m12)
        {
            this.m11 = m11;
            this.m12 = m12;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float1x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float1x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float1x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float1x2"/> instance.</param>
        public static implicit operator Float1x2(float x)
        {
            Float1x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float1x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float1x2"/> value to negate.</param>
        public static Float1x2 operator -(Float1x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Float1x2)}.-");

        /// <summary>
        /// Sums two <see cref="Float1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float1x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float1x2"/> value to sum.</param>
        public static Float1x2 operator +(Float1x2 left, Float1x2 right) => throw new InvalidExecutionContextException($"{nameof(Float1x2)}.+");

        /// <summary>
        /// Divides two <see cref="Float1x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float1x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float1x2"/> value to divide.</param>
        public static Float1x2 operator /(Float1x2 left, Float1x2 right) => throw new InvalidExecutionContextException($"{nameof(Float1x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Float1x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float1x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float1x2"/> value to multiply.</param>
        public static Float1x2 operator *(Float1x2 left, Float1x2 right) => throw new InvalidExecutionContextException($"{nameof(Float1x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float1x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float1x2"/> value to subtract.</param>
        public static Float1x2 operator -(Float1x2 left, Float1x2 right) => throw new InvalidExecutionContextException($"{nameof(Float1x2)}.-");

        /// <summary>
        /// Casts a <see cref="Float2"/> value to a <see cref="Float1x2"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Float2"/> value to cast.</param>
        public static implicit operator Float1x2(Float2 vector) => throw new InvalidExecutionContextException($"{typeof(Float1x2)}.{nameof(Float1x2)}({nameof(Float2)})");
    }

    /// <inheritdoc cref="Float1x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public partial struct Float1x3
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m13;

        /// <summary>
        /// Creates a new <see cref="Float1x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        public Float1x3(float m11, float m12, float m13)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float1x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float1x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float1x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float1x3"/> instance.</param>
        public static implicit operator Float1x3(float x)
        {
            Float1x3 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float1x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float1x3"/> value to negate.</param>
        public static Float1x3 operator -(Float1x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Float1x3)}.-");

        /// <summary>
        /// Sums two <see cref="Float1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float1x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float1x3"/> value to sum.</param>
        public static Float1x3 operator +(Float1x3 left, Float1x3 right) => throw new InvalidExecutionContextException($"{nameof(Float1x3)}.+");

        /// <summary>
        /// Divides two <see cref="Float1x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float1x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float1x3"/> value to divide.</param>
        public static Float1x3 operator /(Float1x3 left, Float1x3 right) => throw new InvalidExecutionContextException($"{nameof(Float1x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Float1x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float1x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float1x3"/> value to multiply.</param>
        public static Float1x3 operator *(Float1x3 left, Float1x3 right) => throw new InvalidExecutionContextException($"{nameof(Float1x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float1x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float1x3"/> value to subtract.</param>
        public static Float1x3 operator -(Float1x3 left, Float1x3 right) => throw new InvalidExecutionContextException($"{nameof(Float1x3)}.-");

        /// <summary>
        /// Casts a <see cref="Float3"/> value to a <see cref="Float1x3"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Float3"/> value to cast.</param>
        public static implicit operator Float1x3(Float3 vector) => throw new InvalidExecutionContextException($"{typeof(Float1x3)}.{nameof(Float1x3)}({nameof(Float3)})");
    }

    /// <inheritdoc cref="Float1x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Float1x4
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m13;

        [FieldOffset(12)]
        private float m14;

        /// <summary>
        /// Creates a new <see cref="Float1x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        public Float1x4(float m11, float m12, float m13, float m14)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float1x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float1x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float1x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float1x4"/> instance.</param>
        public static implicit operator Float1x4(float x)
        {
            Float1x4 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;
            matrix.m14 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float1x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float1x4"/> value to negate.</param>
        public static Float1x4 operator -(Float1x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Float1x4)}.-");

        /// <summary>
        /// Sums two <see cref="Float1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float1x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float1x4"/> value to sum.</param>
        public static Float1x4 operator +(Float1x4 left, Float1x4 right) => throw new InvalidExecutionContextException($"{nameof(Float1x4)}.+");

        /// <summary>
        /// Divides two <see cref="Float1x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float1x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float1x4"/> value to divide.</param>
        public static Float1x4 operator /(Float1x4 left, Float1x4 right) => throw new InvalidExecutionContextException($"{nameof(Float1x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Float1x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float1x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float1x4"/> value to multiply.</param>
        public static Float1x4 operator *(Float1x4 left, Float1x4 right) => throw new InvalidExecutionContextException($"{nameof(Float1x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float1x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float1x4"/> value to subtract.</param>
        public static Float1x4 operator -(Float1x4 left, Float1x4 right) => throw new InvalidExecutionContextException($"{nameof(Float1x4)}.-");

        /// <summary>
        /// Casts a <see cref="Float4"/> value to a <see cref="Float1x4"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Float4"/> value to cast.</param>
        public static implicit operator Float1x4(Float4 vector) => throw new InvalidExecutionContextException($"{typeof(Float1x4)}.{nameof(Float1x4)}({nameof(Float4)})");
    }

    /// <inheritdoc cref="Float2x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Float2x1
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m21;

        /// <summary>
        /// Creates a new <see cref="Float2x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        public Float2x1(float m11, float m21)
        {
            this.m11 = m11;
            this.m21 = m21;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float2x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref float this[int row] => throw new InvalidExecutionContextException($"{nameof(Float2x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float2x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float2x1"/> instance.</param>
        public static implicit operator Float2x1(float x)
        {
            Float2x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float2x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float2x1"/> value to negate.</param>
        public static Float2x1 operator -(Float2x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Float2x1)}.-");

        /// <summary>
        /// Sums two <see cref="Float2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float2x1"/> value to sum.</param>
        public static Float2x1 operator +(Float2x1 left, Float2x1 right) => throw new InvalidExecutionContextException($"{nameof(Float2x1)}.+");

        /// <summary>
        /// Divides two <see cref="Float2x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float2x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float2x1"/> value to divide.</param>
        public static Float2x1 operator /(Float2x1 left, Float2x1 right) => throw new InvalidExecutionContextException($"{nameof(Float2x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Float2x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float2x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float2x1"/> value to multiply.</param>
        public static Float2x1 operator *(Float2x1 left, Float2x1 right) => throw new InvalidExecutionContextException($"{nameof(Float2x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float2x1"/> value to subtract.</param>
        public static Float2x1 operator -(Float2x1 left, Float2x1 right) => throw new InvalidExecutionContextException($"{nameof(Float2x1)}.-");

        /// <summary>
        /// Casts a <see cref="Float2x1"/> value to a <see cref="Float2"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Float2x1"/> value to cast.</param>
        public static implicit operator Float2(Float2x1 matrix) => throw new InvalidExecutionContextException($"{typeof(Float2x1)}.{nameof(Float2)}({nameof(Float2x1)})");
    }

    /// <inheritdoc cref="Float2x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Float2x2
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m21;

        [FieldOffset(12)]
        private float m22;

        /// <summary>
        /// Creates a new <see cref="Float2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        public Float2x2(float m11, float m12, float m21, float m22)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
        }

        /// <summary>
        /// Creates a new <see cref="Float2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Float2x2(Float2 row1, Float2 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float2x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float2x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float2x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float2x2"/> instance.</param>
        public static implicit operator Float2x2(float x)
        {
            Float2x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m21 = x;
            matrix.m22 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float2x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float2x2"/> value to negate.</param>
        public static Float2x2 operator -(Float2x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Float2x2)}.-");

        /// <summary>
        /// Sums two <see cref="Float2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float2x2"/> value to sum.</param>
        public static Float2x2 operator +(Float2x2 left, Float2x2 right) => throw new InvalidExecutionContextException($"{nameof(Float2x2)}.+");

        /// <summary>
        /// Divides two <see cref="Float2x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float2x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float2x2"/> value to divide.</param>
        public static Float2x2 operator /(Float2x2 left, Float2x2 right) => throw new InvalidExecutionContextException($"{nameof(Float2x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Float2x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float2x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float2x2"/> value to multiply.</param>
        public static Float2x2 operator *(Float2x2 left, Float2x2 right) => throw new InvalidExecutionContextException($"{nameof(Float2x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float2x2"/> value to subtract.</param>
        public static Float2x2 operator -(Float2x2 left, Float2x2 right) => throw new InvalidExecutionContextException($"{nameof(Float2x2)}.-");
    }

    /// <inheritdoc cref="Float2x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct Float2x3
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m13;

        [FieldOffset(12)]
        private float m21;

        [FieldOffset(16)]
        private float m22;

        [FieldOffset(20)]
        private float m23;

        /// <summary>
        /// Creates a new <see cref="Float2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        public Float2x3(float m11, float m12, float m13, float m21, float m22, float m23)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
        }

        /// <summary>
        /// Creates a new <see cref="Float2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Float2x3(Float3 row1, Float3 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m13 = row1.Z;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m23 = row2.Z;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float2x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float2x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float2x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float2x3"/> instance.</param>
        public static implicit operator Float2x3(float x)
        {
            Float2x3 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;
            matrix.m21 = x;
            matrix.m22 = x;
            matrix.m23 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float2x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float2x3"/> value to negate.</param>
        public static Float2x3 operator -(Float2x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Float2x3)}.-");

        /// <summary>
        /// Sums two <see cref="Float2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float2x3"/> value to sum.</param>
        public static Float2x3 operator +(Float2x3 left, Float2x3 right) => throw new InvalidExecutionContextException($"{nameof(Float2x3)}.+");

        /// <summary>
        /// Divides two <see cref="Float2x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float2x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float2x3"/> value to divide.</param>
        public static Float2x3 operator /(Float2x3 left, Float2x3 right) => throw new InvalidExecutionContextException($"{nameof(Float2x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Float2x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float2x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float2x3"/> value to multiply.</param>
        public static Float2x3 operator *(Float2x3 left, Float2x3 right) => throw new InvalidExecutionContextException($"{nameof(Float2x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float2x3"/> value to subtract.</param>
        public static Float2x3 operator -(Float2x3 left, Float2x3 right) => throw new InvalidExecutionContextException($"{nameof(Float2x3)}.-");
    }

    /// <inheritdoc cref="Float2x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct Float2x4
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m13;

        [FieldOffset(12)]
        private float m14;

        [FieldOffset(16)]
        private float m21;

        [FieldOffset(20)]
        private float m22;

        [FieldOffset(24)]
        private float m23;

        [FieldOffset(28)]
        private float m24;

        /// <summary>
        /// Creates a new <see cref="Float2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        /// <param name="m24">The value to assign to the component at position [2, 4].</param>
        public Float2x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24)
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
        /// Creates a new <see cref="Float2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Float2x4(Float4 row1, Float4 row2)
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
        /// Gets a reference to a specific row in the current <see cref="Float2x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float2x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float2x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float2x4"/> instance.</param>
        public static implicit operator Float2x4(float x)
        {
            Float2x4 matrix;

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
        /// Negates a <see cref="Float2x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float2x4"/> value to negate.</param>
        public static Float2x4 operator -(Float2x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Float2x4)}.-");

        /// <summary>
        /// Sums two <see cref="Float2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float2x4"/> value to sum.</param>
        public static Float2x4 operator +(Float2x4 left, Float2x4 right) => throw new InvalidExecutionContextException($"{nameof(Float2x4)}.+");

        /// <summary>
        /// Divides two <see cref="Float2x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float2x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float2x4"/> value to divide.</param>
        public static Float2x4 operator /(Float2x4 left, Float2x4 right) => throw new InvalidExecutionContextException($"{nameof(Float2x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Float2x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float2x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float2x4"/> value to multiply.</param>
        public static Float2x4 operator *(Float2x4 left, Float2x4 right) => throw new InvalidExecutionContextException($"{nameof(Float2x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float2x4"/> value to subtract.</param>
        public static Float2x4 operator -(Float2x4 left, Float2x4 right) => throw new InvalidExecutionContextException($"{nameof(Float2x4)}.-");
    }

    /// <inheritdoc cref="Float3x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public partial struct Float3x1
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m21;

        [FieldOffset(8)]
        private float m31;

        /// <summary>
        /// Creates a new <see cref="Float3x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        public Float3x1(float m11, float m21, float m31)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float3x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref float this[int row] => throw new InvalidExecutionContextException($"{nameof(Float3x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float3x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float3x1"/> instance.</param>
        public static implicit operator Float3x1(float x)
        {
            Float3x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;
            matrix.m31 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float3x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float3x1"/> value to negate.</param>
        public static Float3x1 operator -(Float3x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Float3x1)}.-");

        /// <summary>
        /// Sums two <see cref="Float3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float3x1"/> value to sum.</param>
        public static Float3x1 operator +(Float3x1 left, Float3x1 right) => throw new InvalidExecutionContextException($"{nameof(Float3x1)}.+");

        /// <summary>
        /// Divides two <see cref="Float3x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float3x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float3x1"/> value to divide.</param>
        public static Float3x1 operator /(Float3x1 left, Float3x1 right) => throw new InvalidExecutionContextException($"{nameof(Float3x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Float3x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float3x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float3x1"/> value to multiply.</param>
        public static Float3x1 operator *(Float3x1 left, Float3x1 right) => throw new InvalidExecutionContextException($"{nameof(Float3x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float3x1"/> value to subtract.</param>
        public static Float3x1 operator -(Float3x1 left, Float3x1 right) => throw new InvalidExecutionContextException($"{nameof(Float3x1)}.-");

        /// <summary>
        /// Casts a <see cref="Float3x1"/> value to a <see cref="Float3"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Float3x1"/> value to cast.</param>
        public static implicit operator Float3(Float3x1 matrix) => throw new InvalidExecutionContextException($"{typeof(Float3x1)}.{nameof(Float3)}({nameof(Float3x1)})");
    }

    /// <inheritdoc cref="Float3x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct Float3x2
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m21;

        [FieldOffset(12)]
        private float m22;

        [FieldOffset(16)]
        private float m31;

        [FieldOffset(20)]
        private float m32;

        /// <summary>
        /// Creates a new <see cref="Float3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        public Float3x2(float m11, float m12, float m21, float m22, float m31, float m32)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.m31 = m31;
            this.m32 = m32;
        }

        /// <summary>
        /// Creates a new <see cref="Float3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Float3x2(Float2 row1, Float2 row2, Float2 row3)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m31 = row3.X;
            this.m32 = row3.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float3x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float3x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float3x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float3x2"/> instance.</param>
        public static implicit operator Float3x2(float x)
        {
            Float3x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m21 = x;
            matrix.m22 = x;
            matrix.m31 = x;
            matrix.m32 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float3x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float3x2"/> value to negate.</param>
        public static Float3x2 operator -(Float3x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Float3x2)}.-");

        /// <summary>
        /// Sums two <see cref="Float3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float3x2"/> value to sum.</param>
        public static Float3x2 operator +(Float3x2 left, Float3x2 right) => throw new InvalidExecutionContextException($"{nameof(Float3x2)}.+");

        /// <summary>
        /// Divides two <see cref="Float3x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float3x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float3x2"/> value to divide.</param>
        public static Float3x2 operator /(Float3x2 left, Float3x2 right) => throw new InvalidExecutionContextException($"{nameof(Float3x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Float3x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float3x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float3x2"/> value to multiply.</param>
        public static Float3x2 operator *(Float3x2 left, Float3x2 right) => throw new InvalidExecutionContextException($"{nameof(Float3x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float3x2"/> value to subtract.</param>
        public static Float3x2 operator -(Float3x2 left, Float3x2 right) => throw new InvalidExecutionContextException($"{nameof(Float3x2)}.-");
    }

    /// <inheritdoc cref="Float3x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 36)]
    public partial struct Float3x3
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m13;

        [FieldOffset(12)]
        private float m21;

        [FieldOffset(16)]
        private float m22;

        [FieldOffset(20)]
        private float m23;

        [FieldOffset(24)]
        private float m31;

        [FieldOffset(28)]
        private float m32;

        [FieldOffset(32)]
        private float m33;

        /// <summary>
        /// Creates a new <see cref="Float3x3"/> instance with the specified parameters.
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
        public Float3x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
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
        /// Creates a new <see cref="Float3x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Float3x3(Float3 row1, Float3 row2, Float3 row3)
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
        /// Gets a reference to a specific row in the current <see cref="Float3x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float3x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float3x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float3x3"/> instance.</param>
        public static implicit operator Float3x3(float x)
        {
            Float3x3 matrix;

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
        /// Negates a <see cref="Float3x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float3x3"/> value to negate.</param>
        public static Float3x3 operator -(Float3x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Float3x3)}.-");

        /// <summary>
        /// Sums two <see cref="Float3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float3x3"/> value to sum.</param>
        public static Float3x3 operator +(Float3x3 left, Float3x3 right) => throw new InvalidExecutionContextException($"{nameof(Float3x3)}.+");

        /// <summary>
        /// Divides two <see cref="Float3x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float3x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float3x3"/> value to divide.</param>
        public static Float3x3 operator /(Float3x3 left, Float3x3 right) => throw new InvalidExecutionContextException($"{nameof(Float3x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Float3x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float3x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float3x3"/> value to multiply.</param>
        public static Float3x3 operator *(Float3x3 left, Float3x3 right) => throw new InvalidExecutionContextException($"{nameof(Float3x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float3x3"/> value to subtract.</param>
        public static Float3x3 operator -(Float3x3 left, Float3x3 right) => throw new InvalidExecutionContextException($"{nameof(Float3x3)}.-");
    }

    /// <inheritdoc cref="Float3x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public partial struct Float3x4
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m13;

        [FieldOffset(12)]
        private float m14;

        [FieldOffset(16)]
        private float m21;

        [FieldOffset(20)]
        private float m22;

        [FieldOffset(24)]
        private float m23;

        [FieldOffset(28)]
        private float m24;

        [FieldOffset(32)]
        private float m31;

        [FieldOffset(36)]
        private float m32;

        [FieldOffset(40)]
        private float m33;

        [FieldOffset(44)]
        private float m34;

        /// <summary>
        /// Creates a new <see cref="Float3x4"/> instance with the specified parameters.
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
        public Float3x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34)
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
        /// Creates a new <see cref="Float3x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Float3x4(Float4 row1, Float4 row2, Float4 row3)
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
        /// Gets a reference to a specific row in the current <see cref="Float3x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float3x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float3x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float3x4"/> instance.</param>
        public static implicit operator Float3x4(float x)
        {
            Float3x4 matrix;

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
        /// Negates a <see cref="Float3x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float3x4"/> value to negate.</param>
        public static Float3x4 operator -(Float3x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Float3x4)}.-");

        /// <summary>
        /// Sums two <see cref="Float3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float3x4"/> value to sum.</param>
        public static Float3x4 operator +(Float3x4 left, Float3x4 right) => throw new InvalidExecutionContextException($"{nameof(Float3x4)}.+");

        /// <summary>
        /// Divides two <see cref="Float3x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float3x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float3x4"/> value to divide.</param>
        public static Float3x4 operator /(Float3x4 left, Float3x4 right) => throw new InvalidExecutionContextException($"{nameof(Float3x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Float3x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float3x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float3x4"/> value to multiply.</param>
        public static Float3x4 operator *(Float3x4 left, Float3x4 right) => throw new InvalidExecutionContextException($"{nameof(Float3x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float3x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float3x4"/> value to subtract.</param>
        public static Float3x4 operator -(Float3x4 left, Float3x4 right) => throw new InvalidExecutionContextException($"{nameof(Float3x4)}.-");
    }

    /// <inheritdoc cref="Float4x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Float4x1
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m21;

        [FieldOffset(8)]
        private float m31;

        [FieldOffset(12)]
        private float m41;

        /// <summary>
        /// Creates a new <see cref="Float4x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        public Float4x1(float m11, float m21, float m31, float m41)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
            this.m41 = m41;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Float4x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref float this[int row] => throw new InvalidExecutionContextException($"{nameof(Float4x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float4x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float4x1"/> instance.</param>
        public static implicit operator Float4x1(float x)
        {
            Float4x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;
            matrix.m31 = x;
            matrix.m41 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Float4x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float4x1"/> value to negate.</param>
        public static Float4x1 operator -(Float4x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Float4x1)}.-");

        /// <summary>
        /// Sums two <see cref="Float4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float4x1"/> value to sum.</param>
        public static Float4x1 operator +(Float4x1 left, Float4x1 right) => throw new InvalidExecutionContextException($"{nameof(Float4x1)}.+");

        /// <summary>
        /// Divides two <see cref="Float4x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float4x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float4x1"/> value to divide.</param>
        public static Float4x1 operator /(Float4x1 left, Float4x1 right) => throw new InvalidExecutionContextException($"{nameof(Float4x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Float4x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float4x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float4x1"/> value to multiply.</param>
        public static Float4x1 operator *(Float4x1 left, Float4x1 right) => throw new InvalidExecutionContextException($"{nameof(Float4x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float4x1"/> value to subtract.</param>
        public static Float4x1 operator -(Float4x1 left, Float4x1 right) => throw new InvalidExecutionContextException($"{nameof(Float4x1)}.-");

        /// <summary>
        /// Casts a <see cref="Float4x1"/> value to a <see cref="Float4"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Float4x1"/> value to cast.</param>
        public static implicit operator Float4(Float4x1 matrix) => throw new InvalidExecutionContextException($"{typeof(Float4x1)}.{nameof(Float4)}({nameof(Float4x1)})");
    }

    /// <inheritdoc cref="Float4x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct Float4x2
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m21;

        [FieldOffset(12)]
        private float m22;

        [FieldOffset(16)]
        private float m31;

        [FieldOffset(20)]
        private float m32;

        [FieldOffset(24)]
        private float m41;

        [FieldOffset(28)]
        private float m42;

        /// <summary>
        /// Creates a new <see cref="Float4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        /// <param name="m42">The value to assign to the component at position [4, 2].</param>
        public Float4x2(float m11, float m12, float m21, float m22, float m31, float m32, float m41, float m42)
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
        /// Creates a new <see cref="Float4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Float4x2(Float2 row1, Float2 row2, Float2 row3, Float2 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Float4x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float4x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float4x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float4x2"/> instance.</param>
        public static implicit operator Float4x2(float x)
        {
            Float4x2 matrix;

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
        /// Negates a <see cref="Float4x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float4x2"/> value to negate.</param>
        public static Float4x2 operator -(Float4x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Float4x2)}.-");

        /// <summary>
        /// Sums two <see cref="Float4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float4x2"/> value to sum.</param>
        public static Float4x2 operator +(Float4x2 left, Float4x2 right) => throw new InvalidExecutionContextException($"{nameof(Float4x2)}.+");

        /// <summary>
        /// Divides two <see cref="Float4x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float4x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float4x2"/> value to divide.</param>
        public static Float4x2 operator /(Float4x2 left, Float4x2 right) => throw new InvalidExecutionContextException($"{nameof(Float4x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Float4x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float4x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float4x2"/> value to multiply.</param>
        public static Float4x2 operator *(Float4x2 left, Float4x2 right) => throw new InvalidExecutionContextException($"{nameof(Float4x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float4x2"/> value to subtract.</param>
        public static Float4x2 operator -(Float4x2 left, Float4x2 right) => throw new InvalidExecutionContextException($"{nameof(Float4x2)}.-");
    }

    /// <inheritdoc cref="Float4x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public partial struct Float4x3
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m13;

        [FieldOffset(12)]
        private float m21;

        [FieldOffset(16)]
        private float m22;

        [FieldOffset(20)]
        private float m23;

        [FieldOffset(24)]
        private float m31;

        [FieldOffset(28)]
        private float m32;

        [FieldOffset(32)]
        private float m33;

        [FieldOffset(36)]
        private float m41;

        [FieldOffset(40)]
        private float m42;

        [FieldOffset(44)]
        private float m43;

        /// <summary>
        /// Creates a new <see cref="Float4x3"/> instance with the specified parameters.
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
        public Float4x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33, float m41, float m42, float m43)
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
        /// Creates a new <see cref="Float4x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Float4x3(Float3 row1, Float3 row2, Float3 row3, Float3 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Float4x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float4x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float4x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float4x3"/> instance.</param>
        public static implicit operator Float4x3(float x)
        {
            Float4x3 matrix;

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
        /// Negates a <see cref="Float4x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float4x3"/> value to negate.</param>
        public static Float4x3 operator -(Float4x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Float4x3)}.-");

        /// <summary>
        /// Sums two <see cref="Float4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float4x3"/> value to sum.</param>
        public static Float4x3 operator +(Float4x3 left, Float4x3 right) => throw new InvalidExecutionContextException($"{nameof(Float4x3)}.+");

        /// <summary>
        /// Divides two <see cref="Float4x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float4x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float4x3"/> value to divide.</param>
        public static Float4x3 operator /(Float4x3 left, Float4x3 right) => throw new InvalidExecutionContextException($"{nameof(Float4x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Float4x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float4x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float4x3"/> value to multiply.</param>
        public static Float4x3 operator *(Float4x3 left, Float4x3 right) => throw new InvalidExecutionContextException($"{nameof(Float4x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float4x3"/> value to subtract.</param>
        public static Float4x3 operator -(Float4x3 left, Float4x3 right) => throw new InvalidExecutionContextException($"{nameof(Float4x3)}.-");
    }

    /// <inheritdoc cref="Float4x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public partial struct Float4x4
    {
        [FieldOffset(0)]
        private float m11;

        [FieldOffset(4)]
        private float m12;

        [FieldOffset(8)]
        private float m13;

        [FieldOffset(12)]
        private float m14;

        [FieldOffset(16)]
        private float m21;

        [FieldOffset(20)]
        private float m22;

        [FieldOffset(24)]
        private float m23;

        [FieldOffset(28)]
        private float m24;

        [FieldOffset(32)]
        private float m31;

        [FieldOffset(36)]
        private float m32;

        [FieldOffset(40)]
        private float m33;

        [FieldOffset(44)]
        private float m34;

        [FieldOffset(48)]
        private float m41;

        [FieldOffset(52)]
        private float m42;

        [FieldOffset(56)]
        private float m43;

        [FieldOffset(60)]
        private float m44;

        /// <summary>
        /// Creates a new <see cref="Float4x4"/> instance with the specified parameters.
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
        public Float4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
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
        /// Creates a new <see cref="Float4x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Float4x4(Float4 row1, Float4 row2, Float4 row3, Float4 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Float4x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Float4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Float4x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Float2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Float4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Float3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Float4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Float4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Float4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Float4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Creates a new <see cref="Float4x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float4x4"/> instance.</param>
        public static implicit operator Float4x4(float x)
        {
            Float4x4 matrix;

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
        /// Negates a <see cref="Float4x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Float4x4"/> value to negate.</param>
        public static Float4x4 operator -(Float4x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Float4x4)}.-");

        /// <summary>
        /// Sums two <see cref="Float4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float4x4"/> value to sum.</param>
        public static Float4x4 operator +(Float4x4 left, Float4x4 right) => throw new InvalidExecutionContextException($"{nameof(Float4x4)}.+");

        /// <summary>
        /// Divides two <see cref="Float4x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Float4x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float4x4"/> value to divide.</param>
        public static Float4x4 operator /(Float4x4 left, Float4x4 right) => throw new InvalidExecutionContextException($"{nameof(Float4x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Float4x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Float4x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float4x4"/> value to multiply.</param>
        public static Float4x4 operator *(Float4x4 left, Float4x4 right) => throw new InvalidExecutionContextException($"{nameof(Float4x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float4x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float4x4"/> value to subtract.</param>
        public static Float4x4 operator -(Float4x4 left, Float4x4 right) => throw new InvalidExecutionContextException($"{nameof(Float4x4)}.-");
    }
}
