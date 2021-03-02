using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Float1x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public partial struct Float1x1
    {
        [FieldOffset(0)]
        private float m00;

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
        private float m00;

        [FieldOffset(4)]
        private float m01;

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
    }

    /// <inheritdoc cref="Float1x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public partial struct Float1x3
    {
        [FieldOffset(0)]
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m02;

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
    }

    /// <inheritdoc cref="Float1x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Float1x4
    {
        [FieldOffset(0)]
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m02;

        [FieldOffset(12)]
        private float m03;

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
    }

    /// <inheritdoc cref="Float2x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Float2x1
    {
        [FieldOffset(0)]
        private float m00;

        [FieldOffset(4)]
        private float m10;

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
    }

    /// <inheritdoc cref="Float2x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Float2x2
    {
        [FieldOffset(0)]
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m10;

        [FieldOffset(12)]
        private float m11;

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
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m02;

        [FieldOffset(12)]
        private float m10;

        [FieldOffset(16)]
        private float m11;

        [FieldOffset(20)]
        private float m12;

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
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m02;

        [FieldOffset(12)]
        private float m03;

        [FieldOffset(16)]
        private float m10;

        [FieldOffset(20)]
        private float m11;

        [FieldOffset(24)]
        private float m12;

        [FieldOffset(28)]
        private float m13;

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
        private float m00;

        [FieldOffset(4)]
        private float m10;

        [FieldOffset(8)]
        private float m20;

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
    }

    /// <inheritdoc cref="Float3x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct Float3x2
    {
        [FieldOffset(0)]
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m10;

        [FieldOffset(12)]
        private float m11;

        [FieldOffset(16)]
        private float m20;

        [FieldOffset(20)]
        private float m21;

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
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m02;

        [FieldOffset(12)]
        private float m10;

        [FieldOffset(16)]
        private float m11;

        [FieldOffset(20)]
        private float m12;

        [FieldOffset(24)]
        private float m20;

        [FieldOffset(28)]
        private float m21;

        [FieldOffset(32)]
        private float m22;

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
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m02;

        [FieldOffset(12)]
        private float m03;

        [FieldOffset(16)]
        private float m10;

        [FieldOffset(20)]
        private float m11;

        [FieldOffset(24)]
        private float m12;

        [FieldOffset(28)]
        private float m13;

        [FieldOffset(32)]
        private float m20;

        [FieldOffset(36)]
        private float m21;

        [FieldOffset(40)]
        private float m22;

        [FieldOffset(44)]
        private float m23;

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
        private float m00;

        [FieldOffset(4)]
        private float m10;

        [FieldOffset(8)]
        private float m20;

        [FieldOffset(12)]
        private float m30;

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
    }

    /// <inheritdoc cref="Float4x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct Float4x2
    {
        [FieldOffset(0)]
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m10;

        [FieldOffset(12)]
        private float m11;

        [FieldOffset(16)]
        private float m20;

        [FieldOffset(20)]
        private float m21;

        [FieldOffset(24)]
        private float m30;

        [FieldOffset(28)]
        private float m31;

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
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m02;

        [FieldOffset(12)]
        private float m10;

        [FieldOffset(16)]
        private float m11;

        [FieldOffset(20)]
        private float m12;

        [FieldOffset(24)]
        private float m20;

        [FieldOffset(28)]
        private float m21;

        [FieldOffset(32)]
        private float m22;

        [FieldOffset(36)]
        private float m30;

        [FieldOffset(40)]
        private float m31;

        [FieldOffset(44)]
        private float m32;

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
        private float m00;

        [FieldOffset(4)]
        private float m01;

        [FieldOffset(8)]
        private float m02;

        [FieldOffset(12)]
        private float m03;

        [FieldOffset(16)]
        private float m10;

        [FieldOffset(20)]
        private float m11;

        [FieldOffset(24)]
        private float m12;

        [FieldOffset(28)]
        private float m13;

        [FieldOffset(32)]
        private float m20;

        [FieldOffset(36)]
        private float m21;

        [FieldOffset(40)]
        private float m22;

        [FieldOffset(44)]
        private float m23;

        [FieldOffset(48)]
        private float m30;

        [FieldOffset(52)]
        private float m31;

        [FieldOffset(56)]
        private float m32;

        [FieldOffset(60)]
        private float m33;

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
