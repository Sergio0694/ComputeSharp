using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Int1x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public partial struct Int1x1
    {
        [FieldOffset(0)]
        private int m11;

        /// <summary>
        /// Creates a new <see cref="Int1x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        public Int1x1(int m11)
        {
            this.m11 = m11;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int1x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref int this[int row] => throw new InvalidExecutionContextException($"{nameof(Int1x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int1x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int1x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int1x1"/> value to negate.</param>
        public static Int1x1 operator -(Int1x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Int1x1)}.-");

        /// <summary>
        /// Sums two <see cref="Int1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int1x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int1x1"/> value to sum.</param>
        public static Int1x1 operator +(Int1x1 left, Int1x1 right) => throw new InvalidExecutionContextException($"{nameof(Int1x1)}.+");

        /// <summary>
        /// Divides two <see cref="Int1x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int1x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int1x1"/> value to divide.</param>
        public static Int1x1 operator /(Int1x1 left, Int1x1 right) => throw new InvalidExecutionContextException($"{nameof(Int1x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Int1x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int1x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int1x1"/> value to multiply.</param>
        public static Int1x1 operator *(Int1x1 left, Int1x1 right) => throw new InvalidExecutionContextException($"{nameof(Int1x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int1x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int1x1"/> value to subtract.</param>
        public static Int1x1 operator -(Int1x1 left, Int1x1 right) => throw new InvalidExecutionContextException($"{nameof(Int1x1)}.-");
    }

    /// <inheritdoc cref="Int1x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Int1x2
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        /// <summary>
        /// Creates a new <see cref="Int1x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        public Int1x2(int m11, int m12)
        {
            this.m11 = m11;
            this.m12 = m12;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int1x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int1x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int1x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int1x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int1x2"/> value to negate.</param>
        public static Int1x2 operator -(Int1x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Int1x2)}.-");

        /// <summary>
        /// Sums two <see cref="Int1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int1x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int1x2"/> value to sum.</param>
        public static Int1x2 operator +(Int1x2 left, Int1x2 right) => throw new InvalidExecutionContextException($"{nameof(Int1x2)}.+");

        /// <summary>
        /// Divides two <see cref="Int1x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int1x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int1x2"/> value to divide.</param>
        public static Int1x2 operator /(Int1x2 left, Int1x2 right) => throw new InvalidExecutionContextException($"{nameof(Int1x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Int1x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int1x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int1x2"/> value to multiply.</param>
        public static Int1x2 operator *(Int1x2 left, Int1x2 right) => throw new InvalidExecutionContextException($"{nameof(Int1x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int1x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int1x2"/> value to subtract.</param>
        public static Int1x2 operator -(Int1x2 left, Int1x2 right) => throw new InvalidExecutionContextException($"{nameof(Int1x2)}.-");
    }

    /// <inheritdoc cref="Int1x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public partial struct Int1x3
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m13;

        /// <summary>
        /// Creates a new <see cref="Int1x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        public Int1x3(int m11, int m12, int m13)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int1x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int1x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int1x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int1x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int1x3"/> value to negate.</param>
        public static Int1x3 operator -(Int1x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Int1x3)}.-");

        /// <summary>
        /// Sums two <see cref="Int1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int1x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int1x3"/> value to sum.</param>
        public static Int1x3 operator +(Int1x3 left, Int1x3 right) => throw new InvalidExecutionContextException($"{nameof(Int1x3)}.+");

        /// <summary>
        /// Divides two <see cref="Int1x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int1x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int1x3"/> value to divide.</param>
        public static Int1x3 operator /(Int1x3 left, Int1x3 right) => throw new InvalidExecutionContextException($"{nameof(Int1x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Int1x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int1x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int1x3"/> value to multiply.</param>
        public static Int1x3 operator *(Int1x3 left, Int1x3 right) => throw new InvalidExecutionContextException($"{nameof(Int1x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int1x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int1x3"/> value to subtract.</param>
        public static Int1x3 operator -(Int1x3 left, Int1x3 right) => throw new InvalidExecutionContextException($"{nameof(Int1x3)}.-");
    }

    /// <inheritdoc cref="Int1x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Int1x4
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m13;

        [FieldOffset(12)]
        private int m14;

        /// <summary>
        /// Creates a new <see cref="Int1x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        public Int1x4(int m11, int m12, int m13, int m14)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int1x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int1x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int1x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int1x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int1x4"/> value to negate.</param>
        public static Int1x4 operator -(Int1x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Int1x4)}.-");

        /// <summary>
        /// Sums two <see cref="Int1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int1x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int1x4"/> value to sum.</param>
        public static Int1x4 operator +(Int1x4 left, Int1x4 right) => throw new InvalidExecutionContextException($"{nameof(Int1x4)}.+");

        /// <summary>
        /// Divides two <see cref="Int1x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int1x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int1x4"/> value to divide.</param>
        public static Int1x4 operator /(Int1x4 left, Int1x4 right) => throw new InvalidExecutionContextException($"{nameof(Int1x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Int1x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int1x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int1x4"/> value to multiply.</param>
        public static Int1x4 operator *(Int1x4 left, Int1x4 right) => throw new InvalidExecutionContextException($"{nameof(Int1x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int1x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int1x4"/> value to subtract.</param>
        public static Int1x4 operator -(Int1x4 left, Int1x4 right) => throw new InvalidExecutionContextException($"{nameof(Int1x4)}.-");
    }

    /// <inheritdoc cref="Int2x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Int2x1
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m21;

        /// <summary>
        /// Creates a new <see cref="Int2x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        public Int2x1(int m11, int m21)
        {
            this.m11 = m11;
            this.m21 = m21;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int2x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref int this[int row] => throw new InvalidExecutionContextException($"{nameof(Int2x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int2x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int2x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int2x1"/> value to negate.</param>
        public static Int2x1 operator -(Int2x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Int2x1)}.-");

        /// <summary>
        /// Sums two <see cref="Int2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int2x1"/> value to sum.</param>
        public static Int2x1 operator +(Int2x1 left, Int2x1 right) => throw new InvalidExecutionContextException($"{nameof(Int2x1)}.+");

        /// <summary>
        /// Divides two <see cref="Int2x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int2x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int2x1"/> value to divide.</param>
        public static Int2x1 operator /(Int2x1 left, Int2x1 right) => throw new InvalidExecutionContextException($"{nameof(Int2x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Int2x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int2x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int2x1"/> value to multiply.</param>
        public static Int2x1 operator *(Int2x1 left, Int2x1 right) => throw new InvalidExecutionContextException($"{nameof(Int2x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int2x1"/> value to subtract.</param>
        public static Int2x1 operator -(Int2x1 left, Int2x1 right) => throw new InvalidExecutionContextException($"{nameof(Int2x1)}.-");
    }

    /// <inheritdoc cref="Int2x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Int2x2
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m21;

        [FieldOffset(12)]
        private int m22;

        /// <summary>
        /// Creates a new <see cref="Int2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        public Int2x2(int m11, int m12, int m21, int m22)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
        }

        /// <summary>
        /// Creates a new <see cref="Int2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Int2x2(Int2 row1, Int2 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int2x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int2x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int2x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int2x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int2x2"/> value to negate.</param>
        public static Int2x2 operator -(Int2x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Int2x2)}.-");

        /// <summary>
        /// Sums two <see cref="Int2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int2x2"/> value to sum.</param>
        public static Int2x2 operator +(Int2x2 left, Int2x2 right) => throw new InvalidExecutionContextException($"{nameof(Int2x2)}.+");

        /// <summary>
        /// Divides two <see cref="Int2x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int2x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int2x2"/> value to divide.</param>
        public static Int2x2 operator /(Int2x2 left, Int2x2 right) => throw new InvalidExecutionContextException($"{nameof(Int2x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Int2x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int2x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int2x2"/> value to multiply.</param>
        public static Int2x2 operator *(Int2x2 left, Int2x2 right) => throw new InvalidExecutionContextException($"{nameof(Int2x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int2x2"/> value to subtract.</param>
        public static Int2x2 operator -(Int2x2 left, Int2x2 right) => throw new InvalidExecutionContextException($"{nameof(Int2x2)}.-");
    }

    /// <inheritdoc cref="Int2x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct Int2x3
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m13;

        [FieldOffset(12)]
        private int m21;

        [FieldOffset(16)]
        private int m22;

        [FieldOffset(20)]
        private int m23;

        /// <summary>
        /// Creates a new <see cref="Int2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        public Int2x3(int m11, int m12, int m13, int m21, int m22, int m23)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
        }

        /// <summary>
        /// Creates a new <see cref="Int2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Int2x3(Int3 row1, Int3 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m13 = row1.Z;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m23 = row2.Z;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int2x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int2x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int2x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int2x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int2x3"/> value to negate.</param>
        public static Int2x3 operator -(Int2x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Int2x3)}.-");

        /// <summary>
        /// Sums two <see cref="Int2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int2x3"/> value to sum.</param>
        public static Int2x3 operator +(Int2x3 left, Int2x3 right) => throw new InvalidExecutionContextException($"{nameof(Int2x3)}.+");

        /// <summary>
        /// Divides two <see cref="Int2x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int2x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int2x3"/> value to divide.</param>
        public static Int2x3 operator /(Int2x3 left, Int2x3 right) => throw new InvalidExecutionContextException($"{nameof(Int2x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Int2x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int2x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int2x3"/> value to multiply.</param>
        public static Int2x3 operator *(Int2x3 left, Int2x3 right) => throw new InvalidExecutionContextException($"{nameof(Int2x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int2x3"/> value to subtract.</param>
        public static Int2x3 operator -(Int2x3 left, Int2x3 right) => throw new InvalidExecutionContextException($"{nameof(Int2x3)}.-");
    }

    /// <inheritdoc cref="Int2x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct Int2x4
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m13;

        [FieldOffset(12)]
        private int m14;

        [FieldOffset(16)]
        private int m21;

        [FieldOffset(20)]
        private int m22;

        [FieldOffset(24)]
        private int m23;

        [FieldOffset(28)]
        private int m24;

        /// <summary>
        /// Creates a new <see cref="Int2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        /// <param name="m24">The value to assign to the component at position [2, 4].</param>
        public Int2x4(int m11, int m12, int m13, int m14, int m21, int m22, int m23, int m24)
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
        /// Creates a new <see cref="Int2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Int2x4(Int4 row1, Int4 row2)
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
        /// Gets a reference to a specific row in the current <see cref="Int2x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int2x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int2x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int2x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int2x4"/> value to negate.</param>
        public static Int2x4 operator -(Int2x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Int2x4)}.-");

        /// <summary>
        /// Sums two <see cref="Int2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int2x4"/> value to sum.</param>
        public static Int2x4 operator +(Int2x4 left, Int2x4 right) => throw new InvalidExecutionContextException($"{nameof(Int2x4)}.+");

        /// <summary>
        /// Divides two <see cref="Int2x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int2x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int2x4"/> value to divide.</param>
        public static Int2x4 operator /(Int2x4 left, Int2x4 right) => throw new InvalidExecutionContextException($"{nameof(Int2x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Int2x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int2x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int2x4"/> value to multiply.</param>
        public static Int2x4 operator *(Int2x4 left, Int2x4 right) => throw new InvalidExecutionContextException($"{nameof(Int2x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int2x4"/> value to subtract.</param>
        public static Int2x4 operator -(Int2x4 left, Int2x4 right) => throw new InvalidExecutionContextException($"{nameof(Int2x4)}.-");
    }

    /// <inheritdoc cref="Int3x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public partial struct Int3x1
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m21;

        [FieldOffset(8)]
        private int m31;

        /// <summary>
        /// Creates a new <see cref="Int3x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        public Int3x1(int m11, int m21, int m31)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int3x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref int this[int row] => throw new InvalidExecutionContextException($"{nameof(Int3x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int3x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int3x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int3x1"/> value to negate.</param>
        public static Int3x1 operator -(Int3x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Int3x1)}.-");

        /// <summary>
        /// Sums two <see cref="Int3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int3x1"/> value to sum.</param>
        public static Int3x1 operator +(Int3x1 left, Int3x1 right) => throw new InvalidExecutionContextException($"{nameof(Int3x1)}.+");

        /// <summary>
        /// Divides two <see cref="Int3x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int3x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int3x1"/> value to divide.</param>
        public static Int3x1 operator /(Int3x1 left, Int3x1 right) => throw new InvalidExecutionContextException($"{nameof(Int3x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Int3x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int3x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int3x1"/> value to multiply.</param>
        public static Int3x1 operator *(Int3x1 left, Int3x1 right) => throw new InvalidExecutionContextException($"{nameof(Int3x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int3x1"/> value to subtract.</param>
        public static Int3x1 operator -(Int3x1 left, Int3x1 right) => throw new InvalidExecutionContextException($"{nameof(Int3x1)}.-");
    }

    /// <inheritdoc cref="Int3x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct Int3x2
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m21;

        [FieldOffset(12)]
        private int m22;

        [FieldOffset(16)]
        private int m31;

        [FieldOffset(20)]
        private int m32;

        /// <summary>
        /// Creates a new <see cref="Int3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        public Int3x2(int m11, int m12, int m21, int m22, int m31, int m32)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.m31 = m31;
            this.m32 = m32;
        }

        /// <summary>
        /// Creates a new <see cref="Int3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Int3x2(Int2 row1, Int2 row2, Int2 row3)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m31 = row3.X;
            this.m32 = row3.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int3x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int3x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int3x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int3x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int3x2"/> value to negate.</param>
        public static Int3x2 operator -(Int3x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Int3x2)}.-");

        /// <summary>
        /// Sums two <see cref="Int3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int3x2"/> value to sum.</param>
        public static Int3x2 operator +(Int3x2 left, Int3x2 right) => throw new InvalidExecutionContextException($"{nameof(Int3x2)}.+");

        /// <summary>
        /// Divides two <see cref="Int3x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int3x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int3x2"/> value to divide.</param>
        public static Int3x2 operator /(Int3x2 left, Int3x2 right) => throw new InvalidExecutionContextException($"{nameof(Int3x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Int3x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int3x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int3x2"/> value to multiply.</param>
        public static Int3x2 operator *(Int3x2 left, Int3x2 right) => throw new InvalidExecutionContextException($"{nameof(Int3x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int3x2"/> value to subtract.</param>
        public static Int3x2 operator -(Int3x2 left, Int3x2 right) => throw new InvalidExecutionContextException($"{nameof(Int3x2)}.-");
    }

    /// <inheritdoc cref="Int3x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 36)]
    public partial struct Int3x3
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m13;

        [FieldOffset(12)]
        private int m21;

        [FieldOffset(16)]
        private int m22;

        [FieldOffset(20)]
        private int m23;

        [FieldOffset(24)]
        private int m31;

        [FieldOffset(28)]
        private int m32;

        [FieldOffset(32)]
        private int m33;

        /// <summary>
        /// Creates a new <see cref="Int3x3"/> instance with the specified parameters.
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
        public Int3x3(int m11, int m12, int m13, int m21, int m22, int m23, int m31, int m32, int m33)
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
        /// Creates a new <see cref="Int3x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Int3x3(Int3 row1, Int3 row2, Int3 row3)
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
        /// Gets a reference to a specific row in the current <see cref="Int3x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int3x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int3x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int3x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int3x3"/> value to negate.</param>
        public static Int3x3 operator -(Int3x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Int3x3)}.-");

        /// <summary>
        /// Sums two <see cref="Int3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int3x3"/> value to sum.</param>
        public static Int3x3 operator +(Int3x3 left, Int3x3 right) => throw new InvalidExecutionContextException($"{nameof(Int3x3)}.+");

        /// <summary>
        /// Divides two <see cref="Int3x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int3x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int3x3"/> value to divide.</param>
        public static Int3x3 operator /(Int3x3 left, Int3x3 right) => throw new InvalidExecutionContextException($"{nameof(Int3x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Int3x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int3x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int3x3"/> value to multiply.</param>
        public static Int3x3 operator *(Int3x3 left, Int3x3 right) => throw new InvalidExecutionContextException($"{nameof(Int3x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int3x3"/> value to subtract.</param>
        public static Int3x3 operator -(Int3x3 left, Int3x3 right) => throw new InvalidExecutionContextException($"{nameof(Int3x3)}.-");
    }

    /// <inheritdoc cref="Int3x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public partial struct Int3x4
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m13;

        [FieldOffset(12)]
        private int m14;

        [FieldOffset(16)]
        private int m21;

        [FieldOffset(20)]
        private int m22;

        [FieldOffset(24)]
        private int m23;

        [FieldOffset(28)]
        private int m24;

        [FieldOffset(32)]
        private int m31;

        [FieldOffset(36)]
        private int m32;

        [FieldOffset(40)]
        private int m33;

        [FieldOffset(44)]
        private int m34;

        /// <summary>
        /// Creates a new <see cref="Int3x4"/> instance with the specified parameters.
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
        public Int3x4(int m11, int m12, int m13, int m14, int m21, int m22, int m23, int m24, int m31, int m32, int m33, int m34)
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
        /// Creates a new <see cref="Int3x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Int3x4(Int4 row1, Int4 row2, Int4 row3)
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
        /// Gets a reference to a specific row in the current <see cref="Int3x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int3x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int3x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int3x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int3x4"/> value to negate.</param>
        public static Int3x4 operator -(Int3x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Int3x4)}.-");

        /// <summary>
        /// Sums two <see cref="Int3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int3x4"/> value to sum.</param>
        public static Int3x4 operator +(Int3x4 left, Int3x4 right) => throw new InvalidExecutionContextException($"{nameof(Int3x4)}.+");

        /// <summary>
        /// Divides two <see cref="Int3x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int3x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int3x4"/> value to divide.</param>
        public static Int3x4 operator /(Int3x4 left, Int3x4 right) => throw new InvalidExecutionContextException($"{nameof(Int3x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Int3x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int3x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int3x4"/> value to multiply.</param>
        public static Int3x4 operator *(Int3x4 left, Int3x4 right) => throw new InvalidExecutionContextException($"{nameof(Int3x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int3x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int3x4"/> value to subtract.</param>
        public static Int3x4 operator -(Int3x4 left, Int3x4 right) => throw new InvalidExecutionContextException($"{nameof(Int3x4)}.-");
    }

    /// <inheritdoc cref="Int4x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public partial struct Int4x1
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m21;

        [FieldOffset(8)]
        private int m31;

        [FieldOffset(12)]
        private int m41;

        /// <summary>
        /// Creates a new <see cref="Int4x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        public Int4x1(int m11, int m21, int m31, int m41)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
            this.m41 = m41;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Int4x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref int this[int row] => throw new InvalidExecutionContextException($"{nameof(Int4x1)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int4x1)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int4x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int4x1"/> value to negate.</param>
        public static Int4x1 operator -(Int4x1 matrix) => throw new InvalidExecutionContextException($"{nameof(Int4x1)}.-");

        /// <summary>
        /// Sums two <see cref="Int4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int4x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int4x1"/> value to sum.</param>
        public static Int4x1 operator +(Int4x1 left, Int4x1 right) => throw new InvalidExecutionContextException($"{nameof(Int4x1)}.+");

        /// <summary>
        /// Divides two <see cref="Int4x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int4x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int4x1"/> value to divide.</param>
        public static Int4x1 operator /(Int4x1 left, Int4x1 right) => throw new InvalidExecutionContextException($"{nameof(Int4x1)}./");

        /// <summary>
        /// Multiplies two <see cref="Int4x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int4x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int4x1"/> value to multiply.</param>
        public static Int4x1 operator *(Int4x1 left, Int4x1 right) => throw new InvalidExecutionContextException($"{nameof(Int4x1)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int4x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int4x1"/> value to subtract.</param>
        public static Int4x1 operator -(Int4x1 left, Int4x1 right) => throw new InvalidExecutionContextException($"{nameof(Int4x1)}.-");
    }

    /// <inheritdoc cref="Int4x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct Int4x2
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m21;

        [FieldOffset(12)]
        private int m22;

        [FieldOffset(16)]
        private int m31;

        [FieldOffset(20)]
        private int m32;

        [FieldOffset(24)]
        private int m41;

        [FieldOffset(28)]
        private int m42;

        /// <summary>
        /// Creates a new <see cref="Int4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        /// <param name="m42">The value to assign to the component at position [4, 2].</param>
        public Int4x2(int m11, int m12, int m21, int m22, int m31, int m32, int m41, int m42)
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
        /// Creates a new <see cref="Int4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Int4x2(Int2 row1, Int2 row2, Int2 row3, Int2 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Int4x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int2 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int4x2)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int4x2)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int4x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int4x2"/> value to negate.</param>
        public static Int4x2 operator -(Int4x2 matrix) => throw new InvalidExecutionContextException($"{nameof(Int4x2)}.-");

        /// <summary>
        /// Sums two <see cref="Int4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int4x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int4x2"/> value to sum.</param>
        public static Int4x2 operator +(Int4x2 left, Int4x2 right) => throw new InvalidExecutionContextException($"{nameof(Int4x2)}.+");

        /// <summary>
        /// Divides two <see cref="Int4x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int4x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int4x2"/> value to divide.</param>
        public static Int4x2 operator /(Int4x2 left, Int4x2 right) => throw new InvalidExecutionContextException($"{nameof(Int4x2)}./");

        /// <summary>
        /// Multiplies two <see cref="Int4x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int4x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int4x2"/> value to multiply.</param>
        public static Int4x2 operator *(Int4x2 left, Int4x2 right) => throw new InvalidExecutionContextException($"{nameof(Int4x2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int4x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int4x2"/> value to subtract.</param>
        public static Int4x2 operator -(Int4x2 left, Int4x2 right) => throw new InvalidExecutionContextException($"{nameof(Int4x2)}.-");
    }

    /// <inheritdoc cref="Int4x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public partial struct Int4x3
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m13;

        [FieldOffset(12)]
        private int m21;

        [FieldOffset(16)]
        private int m22;

        [FieldOffset(20)]
        private int m23;

        [FieldOffset(24)]
        private int m31;

        [FieldOffset(28)]
        private int m32;

        [FieldOffset(32)]
        private int m33;

        [FieldOffset(36)]
        private int m41;

        [FieldOffset(40)]
        private int m42;

        [FieldOffset(44)]
        private int m43;

        /// <summary>
        /// Creates a new <see cref="Int4x3"/> instance with the specified parameters.
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
        public Int4x3(int m11, int m12, int m13, int m21, int m22, int m23, int m31, int m32, int m33, int m41, int m42, int m43)
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
        /// Creates a new <see cref="Int4x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Int4x3(Int3 row1, Int3 row2, Int3 row3, Int3 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Int4x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int3 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int4x3)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int4x3)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int4x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int4x3"/> value to negate.</param>
        public static Int4x3 operator -(Int4x3 matrix) => throw new InvalidExecutionContextException($"{nameof(Int4x3)}.-");

        /// <summary>
        /// Sums two <see cref="Int4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int4x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int4x3"/> value to sum.</param>
        public static Int4x3 operator +(Int4x3 left, Int4x3 right) => throw new InvalidExecutionContextException($"{nameof(Int4x3)}.+");

        /// <summary>
        /// Divides two <see cref="Int4x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int4x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int4x3"/> value to divide.</param>
        public static Int4x3 operator /(Int4x3 left, Int4x3 right) => throw new InvalidExecutionContextException($"{nameof(Int4x3)}./");

        /// <summary>
        /// Multiplies two <see cref="Int4x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int4x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int4x3"/> value to multiply.</param>
        public static Int4x3 operator *(Int4x3 left, Int4x3 right) => throw new InvalidExecutionContextException($"{nameof(Int4x3)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int4x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int4x3"/> value to subtract.</param>
        public static Int4x3 operator -(Int4x3 left, Int4x3 right) => throw new InvalidExecutionContextException($"{nameof(Int4x3)}.-");
    }

    /// <inheritdoc cref="Int4x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public partial struct Int4x4
    {
        [FieldOffset(0)]
        private int m11;

        [FieldOffset(4)]
        private int m12;

        [FieldOffset(8)]
        private int m13;

        [FieldOffset(12)]
        private int m14;

        [FieldOffset(16)]
        private int m21;

        [FieldOffset(20)]
        private int m22;

        [FieldOffset(24)]
        private int m23;

        [FieldOffset(28)]
        private int m24;

        [FieldOffset(32)]
        private int m31;

        [FieldOffset(36)]
        private int m32;

        [FieldOffset(40)]
        private int m33;

        [FieldOffset(44)]
        private int m34;

        [FieldOffset(48)]
        private int m41;

        [FieldOffset(52)]
        private int m42;

        [FieldOffset(56)]
        private int m43;

        [FieldOffset(60)]
        private int m44;

        /// <summary>
        /// Creates a new <see cref="Int4x4"/> instance with the specified parameters.
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
        public Int4x4(int m11, int m12, int m13, int m14, int m21, int m22, int m23, int m24, int m31, int m32, int m33, int m34, int m41, int m42, int m43, int m44)
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
        /// Creates a new <see cref="Int4x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Int4x4(Int4 row1, Int4 row2, Int4 row3, Int4 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Int4x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        public ref Int4 this[int row] => throw new InvalidExecutionContextException($"{nameof(Int4x4)}[int]");

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        public ref Int2 this[MatrixIndex xy0, MatrixIndex xy1] => throw new InvalidExecutionContextException($"{nameof(Int4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        public ref Int3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => throw new InvalidExecutionContextException($"{nameof(Int4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Int4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        public ref Int4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => throw new InvalidExecutionContextException($"{nameof(Int4x4)}[{nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}, {nameof(MatrixIndex)}]");

        /// <summary>
        /// Negates a <see cref="Int4x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Int4x4"/> value to negate.</param>
        public static Int4x4 operator -(Int4x4 matrix) => throw new InvalidExecutionContextException($"{nameof(Int4x4)}.-");

        /// <summary>
        /// Sums two <see cref="Int4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int4x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int4x4"/> value to sum.</param>
        public static Int4x4 operator +(Int4x4 left, Int4x4 right) => throw new InvalidExecutionContextException($"{nameof(Int4x4)}.+");

        /// <summary>
        /// Divides two <see cref="Int4x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="Int4x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int4x4"/> value to divide.</param>
        public static Int4x4 operator /(Int4x4 left, Int4x4 right) => throw new InvalidExecutionContextException($"{nameof(Int4x4)}./");

        /// <summary>
        /// Multiplies two <see cref="Int4x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="Int4x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int4x4"/> value to multiply.</param>
        public static Int4x4 operator *(Int4x4 left, Int4x4 right) => throw new InvalidExecutionContextException($"{nameof(Int4x4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int4x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int4x4"/> value to subtract.</param>
        public static Int4x4 operator -(Int4x4 left, Int4x4 right) => throw new InvalidExecutionContextException($"{nameof(Int4x4)}.-");
    }
}
