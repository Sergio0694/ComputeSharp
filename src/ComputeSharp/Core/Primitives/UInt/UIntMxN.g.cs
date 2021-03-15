using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if !NET5_0
using RuntimeHelpers = ComputeSharp.SourceGenerators.Helpers.RuntimeHelpers;
using MemoryMarshal = ComputeSharp.SourceGenerators.Helpers.MemoryMarshal;
#endif

namespace ComputeSharp
{
    /// <inheritdoc cref="UInt1x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public unsafe partial struct UInt1x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt1x1), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        /// <summary>
        /// Creates a new <see cref="UInt1x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        public UInt1x1(uint m11)
        {
            this.m11 = m11;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt1x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref uint this[int row] => ref *(uint*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Creates a new <see cref="UInt1x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt1x1"/> instance.</param>
        public static implicit operator UInt1x1(uint x)
        {
            UInt1x1 matrix;

            matrix.m11 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt1x1"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x1 operator +(UInt1x1 left, UInt1x1 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt1x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt1x1"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x1 operator /(UInt1x1 left, UInt1x1 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt1x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt1x1"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x1 operator *(UInt1x1 left, UInt1x1 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt1x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt1x1"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x1 operator -(UInt1x1 left, UInt1x1 right) => default;
    }

    /// <inheritdoc cref="UInt1x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public unsafe partial struct UInt1x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt1x2), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        /// <summary>
        /// Creates a new <see cref="UInt1x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        public UInt1x2(uint m11, uint m12)
        {
            this.m11 = m11;
            this.m12 = m12;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt1x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[int row] => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Creates a new <see cref="UInt1x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt1x2"/> instance.</param>
        public static implicit operator UInt1x2(uint x)
        {
            UInt1x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt1x2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x2 operator +(UInt1x2 left, UInt1x2 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt1x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt1x2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x2 operator /(UInt1x2 left, UInt1x2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt1x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt1x2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x2 operator *(UInt1x2 left, UInt1x2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt1x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt1x2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x2 operator -(UInt1x2 left, UInt1x2 right) => default;

        /// <summary>
        /// Casts a <see cref="UInt2"/> value to a <see cref="UInt1x2"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="UInt2"/> value to cast.</param>
        public static implicit operator UInt1x2(UInt2 vector) => *(UInt1x2*)&vector;
    }

    /// <inheritdoc cref="UInt1x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct UInt1x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt1x3), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m13;

        /// <summary>
        /// Creates a new <see cref="UInt1x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        public UInt1x3(uint m11, uint m12, uint m13)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt1x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[int row] => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref uint M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Creates a new <see cref="UInt1x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt1x3"/> instance.</param>
        public static implicit operator UInt1x3(uint x)
        {
            UInt1x3 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt1x3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x3 operator +(UInt1x3 left, UInt1x3 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt1x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt1x3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x3 operator /(UInt1x3 left, UInt1x3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt1x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt1x3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x3 operator *(UInt1x3 left, UInt1x3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt1x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt1x3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x3 operator -(UInt1x3 left, UInt1x3 right) => default;

        /// <summary>
        /// Casts a <see cref="UInt3"/> value to a <see cref="UInt1x3"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="UInt3"/> value to cast.</param>
        public static implicit operator UInt1x3(UInt3 vector) => *(UInt1x3*)&vector;
    }

    /// <inheritdoc cref="UInt1x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public unsafe partial struct UInt1x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt1x4), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m13;

        [FieldOffset(12)]
        private uint m14;

        /// <summary>
        /// Creates a new <see cref="UInt1x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        public UInt1x4(uint m11, uint m12, uint m13, uint m14)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt1x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[int row] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref uint M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref uint M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Creates a new <see cref="UInt1x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt1x4"/> instance.</param>
        public static implicit operator UInt1x4(uint x)
        {
            UInt1x4 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;
            matrix.m14 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt1x4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x4 operator +(UInt1x4 left, UInt1x4 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt1x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt1x4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x4 operator /(UInt1x4 left, UInt1x4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt1x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt1x4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x4 operator *(UInt1x4 left, UInt1x4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt1x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt1x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt1x4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt1x4 operator -(UInt1x4 left, UInt1x4 right) => default;

        /// <summary>
        /// Casts a <see cref="UInt4"/> value to a <see cref="UInt1x4"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="UInt4"/> value to cast.</param>
        public static implicit operator UInt1x4(UInt4 vector) => *(UInt1x4*)&vector;
    }

    /// <inheritdoc cref="UInt2x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public unsafe partial struct UInt2x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt2x1), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m21;

        /// <summary>
        /// Creates a new <see cref="UInt2x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        public UInt2x1(uint m11, uint m21)
        {
            this.m11 = m11;
            this.m21 = m21;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt2x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref uint this[int row] => ref *(uint*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Creates a new <see cref="UInt2x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt2x1"/> instance.</param>
        public static implicit operator UInt2x1(uint x)
        {
            UInt2x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt2x1"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x1 operator +(UInt2x1 left, UInt2x1 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt2x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt2x1"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x1 operator /(UInt2x1 left, UInt2x1 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt2x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt2x1"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x1 operator *(UInt2x1 left, UInt2x1 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt2x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt2x1"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x1 operator -(UInt2x1 left, UInt2x1 right) => default;

        /// <summary>
        /// Casts a <see cref="UInt2x1"/> value to a <see cref="UInt2"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="UInt2x1"/> value to cast.</param>
        public static implicit operator UInt2(UInt2x1 matrix) => *(UInt2*)&matrix;
    }

    /// <inheritdoc cref="UInt2x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public unsafe partial struct UInt2x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt2x2), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m21;

        [FieldOffset(12)]
        private uint m22;

        /// <summary>
        /// Creates a new <see cref="UInt2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        public UInt2x2(uint m11, uint m12, uint m21, uint m22)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
        }

        /// <summary>
        /// Creates a new <see cref="UInt2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public UInt2x2(UInt2 row1, UInt2 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt2x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[int row] => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Creates a new <see cref="UInt2x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt2x2"/> instance.</param>
        public static implicit operator UInt2x2(uint x)
        {
            UInt2x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m21 = x;
            matrix.m22 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt2x2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x2 operator +(UInt2x2 left, UInt2x2 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt2x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt2x2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x2 operator /(UInt2x2 left, UInt2x2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt2x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt2x2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x2 operator *(UInt2x2 left, UInt2x2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt2x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt2x2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x2 operator -(UInt2x2 left, UInt2x2 right) => default;
    }

    /// <inheritdoc cref="UInt2x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    public unsafe partial struct UInt2x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt2x3), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m13;

        [FieldOffset(12)]
        private uint m21;

        [FieldOffset(16)]
        private uint m22;

        [FieldOffset(20)]
        private uint m23;

        /// <summary>
        /// Creates a new <see cref="UInt2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        public UInt2x3(uint m11, uint m12, uint m13, uint m21, uint m22, uint m23)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
        }

        /// <summary>
        /// Creates a new <see cref="UInt2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public UInt2x3(UInt3 row1, UInt3 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m13 = row1.Z;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m23 = row2.Z;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt2x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[int row] => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref uint M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref uint M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Creates a new <see cref="UInt2x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt2x3"/> instance.</param>
        public static implicit operator UInt2x3(uint x)
        {
            UInt2x3 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;
            matrix.m21 = x;
            matrix.m22 = x;
            matrix.m23 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt2x3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x3 operator +(UInt2x3 left, UInt2x3 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt2x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt2x3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x3 operator /(UInt2x3 left, UInt2x3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt2x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt2x3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x3 operator *(UInt2x3 left, UInt2x3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt2x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt2x3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x3 operator -(UInt2x3 left, UInt2x3 right) => default;
    }

    /// <inheritdoc cref="UInt2x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 4)]
    public unsafe partial struct UInt2x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt2x4), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m13;

        [FieldOffset(12)]
        private uint m14;

        [FieldOffset(16)]
        private uint m21;

        [FieldOffset(20)]
        private uint m22;

        [FieldOffset(24)]
        private uint m23;

        [FieldOffset(28)]
        private uint m24;

        /// <summary>
        /// Creates a new <see cref="UInt2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        /// <param name="m24">The value to assign to the component at position [2, 4].</param>
        public UInt2x4(uint m11, uint m12, uint m13, uint m14, uint m21, uint m22, uint m23, uint m24)
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
        /// Creates a new <see cref="UInt2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public UInt2x4(UInt4 row1, UInt4 row2)
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
        /// Gets a reference to a specific row in the current <see cref="UInt2x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[int row] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref uint M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref uint M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref uint M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref uint M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Creates a new <see cref="UInt2x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt2x4"/> instance.</param>
        public static implicit operator UInt2x4(uint x)
        {
            UInt2x4 matrix;

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
        /// Sums two <see cref="UInt2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt2x4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x4 operator +(UInt2x4 left, UInt2x4 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt2x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt2x4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x4 operator /(UInt2x4 left, UInt2x4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt2x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt2x4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x4 operator *(UInt2x4 left, UInt2x4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt2x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt2x4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt2x4 operator -(UInt2x4 left, UInt2x4 right) => default;
    }

    /// <inheritdoc cref="UInt3x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct UInt3x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt3x1), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m21;

        [FieldOffset(8)]
        private uint m31;

        /// <summary>
        /// Creates a new <see cref="UInt3x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        public UInt3x1(uint m11, uint m21, uint m31)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt3x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref uint this[int row] => ref *(uint*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref uint M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Creates a new <see cref="UInt3x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt3x1"/> instance.</param>
        public static implicit operator UInt3x1(uint x)
        {
            UInt3x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;
            matrix.m31 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt3x1"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x1 operator +(UInt3x1 left, UInt3x1 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt3x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt3x1"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x1 operator /(UInt3x1 left, UInt3x1 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt3x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt3x1"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x1 operator *(UInt3x1 left, UInt3x1 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt3x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt3x1"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x1 operator -(UInt3x1 left, UInt3x1 right) => default;

        /// <summary>
        /// Casts a <see cref="UInt3x1"/> value to a <see cref="UInt3"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="UInt3x1"/> value to cast.</param>
        public static implicit operator UInt3(UInt3x1 matrix) => *(UInt3*)&matrix;
    }

    /// <inheritdoc cref="UInt3x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    public unsafe partial struct UInt3x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt3x2), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m21;

        [FieldOffset(12)]
        private uint m22;

        [FieldOffset(16)]
        private uint m31;

        [FieldOffset(20)]
        private uint m32;

        /// <summary>
        /// Creates a new <see cref="UInt3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        public UInt3x2(uint m11, uint m12, uint m21, uint m22, uint m31, uint m32)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.m31 = m31;
            this.m32 = m32;
        }

        /// <summary>
        /// Creates a new <see cref="UInt3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public UInt3x2(UInt2 row1, UInt2 row2, UInt2 row3)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m31 = row3.X;
            this.m32 = row3.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt3x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[int row] => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref uint M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref uint M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Creates a new <see cref="UInt3x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt3x2"/> instance.</param>
        public static implicit operator UInt3x2(uint x)
        {
            UInt3x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m21 = x;
            matrix.m22 = x;
            matrix.m31 = x;
            matrix.m32 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt3x2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x2 operator +(UInt3x2 left, UInt3x2 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt3x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt3x2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x2 operator /(UInt3x2 left, UInt3x2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt3x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt3x2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x2 operator *(UInt3x2 left, UInt3x2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt3x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt3x2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x2 operator -(UInt3x2 left, UInt3x2 right) => default;
    }

    /// <inheritdoc cref="UInt3x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 36, Pack = 4)]
    public unsafe partial struct UInt3x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt3x3), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m13;

        [FieldOffset(12)]
        private uint m21;

        [FieldOffset(16)]
        private uint m22;

        [FieldOffset(20)]
        private uint m23;

        [FieldOffset(24)]
        private uint m31;

        [FieldOffset(28)]
        private uint m32;

        [FieldOffset(32)]
        private uint m33;

        /// <summary>
        /// Creates a new <see cref="UInt3x3"/> instance with the specified parameters.
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
        public UInt3x3(uint m11, uint m12, uint m13, uint m21, uint m22, uint m23, uint m31, uint m32, uint m33)
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
        /// Creates a new <see cref="UInt3x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public UInt3x3(UInt3 row1, UInt3 row2, UInt3 row3)
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
        /// Gets a reference to a specific row in the current <see cref="UInt3x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[int row] => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref uint M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref uint M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref uint M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref uint M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref uint M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Creates a new <see cref="UInt3x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt3x3"/> instance.</param>
        public static implicit operator UInt3x3(uint x)
        {
            UInt3x3 matrix;

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
        /// Sums two <see cref="UInt3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt3x3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x3 operator +(UInt3x3 left, UInt3x3 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt3x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt3x3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x3 operator /(UInt3x3 left, UInt3x3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt3x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt3x3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x3 operator *(UInt3x3 left, UInt3x3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt3x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt3x3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x3 operator -(UInt3x3 left, UInt3x3 right) => default;
    }

    /// <inheritdoc cref="UInt3x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 48, Pack = 4)]
    public unsafe partial struct UInt3x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt3x4), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m13;

        [FieldOffset(12)]
        private uint m14;

        [FieldOffset(16)]
        private uint m21;

        [FieldOffset(20)]
        private uint m22;

        [FieldOffset(24)]
        private uint m23;

        [FieldOffset(28)]
        private uint m24;

        [FieldOffset(32)]
        private uint m31;

        [FieldOffset(36)]
        private uint m32;

        [FieldOffset(40)]
        private uint m33;

        [FieldOffset(44)]
        private uint m34;

        /// <summary>
        /// Creates a new <see cref="UInt3x4"/> instance with the specified parameters.
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
        public UInt3x4(uint m11, uint m12, uint m13, uint m14, uint m21, uint m22, uint m23, uint m24, uint m31, uint m32, uint m33, uint m34)
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
        /// Creates a new <see cref="UInt3x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public UInt3x4(UInt4 row1, UInt4 row2, UInt4 row3)
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
        /// Gets a reference to a specific row in the current <see cref="UInt3x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[int row] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref uint M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref uint M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref uint M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref uint M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref uint M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref uint M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref uint M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 4].
        /// </summary>
        public readonly ref uint M34 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m34), 1));

        /// <summary>
        /// Creates a new <see cref="UInt3x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt3x4"/> instance.</param>
        public static implicit operator UInt3x4(uint x)
        {
            UInt3x4 matrix;

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
        /// Sums two <see cref="UInt3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt3x4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x4 operator +(UInt3x4 left, UInt3x4 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt3x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt3x4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x4 operator /(UInt3x4 left, UInt3x4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt3x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt3x4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x4 operator *(UInt3x4 left, UInt3x4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt3x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt3x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt3x4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt3x4 operator -(UInt3x4 left, UInt3x4 right) => default;
    }

    /// <inheritdoc cref="UInt4x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public unsafe partial struct UInt4x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt4x1), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m21;

        [FieldOffset(8)]
        private uint m31;

        [FieldOffset(12)]
        private uint m41;

        /// <summary>
        /// Creates a new <see cref="UInt4x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        public UInt4x1(uint m11, uint m21, uint m31, uint m41)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
            this.m41 = m41;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="UInt4x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref uint this[int row] => ref *(uint*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref uint M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref uint M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Creates a new <see cref="UInt4x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt4x1"/> instance.</param>
        public static implicit operator UInt4x1(uint x)
        {
            UInt4x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;
            matrix.m31 = x;
            matrix.m41 = x;

            return matrix;
        }

        /// <summary>
        /// Sums two <see cref="UInt4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x1"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt4x1"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x1 operator +(UInt4x1 left, UInt4x1 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt4x1"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x1"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt4x1"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x1 operator /(UInt4x1 left, UInt4x1 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt4x1"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x1"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt4x1"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x1 operator *(UInt4x1 left, UInt4x1 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt4x1"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x1"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt4x1"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x1 operator -(UInt4x1 left, UInt4x1 right) => default;

        /// <summary>
        /// Casts a <see cref="UInt4x1"/> value to a <see cref="UInt4"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="UInt4x1"/> value to cast.</param>
        public static implicit operator UInt4(UInt4x1 matrix) => *(UInt4*)&matrix;
    }

    /// <inheritdoc cref="UInt4x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 4)]
    public unsafe partial struct UInt4x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt4x2), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m21;

        [FieldOffset(12)]
        private uint m22;

        [FieldOffset(16)]
        private uint m31;

        [FieldOffset(20)]
        private uint m32;

        [FieldOffset(24)]
        private uint m41;

        [FieldOffset(28)]
        private uint m42;

        /// <summary>
        /// Creates a new <see cref="UInt4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        /// <param name="m42">The value to assign to the component at position [4, 2].</param>
        public UInt4x2(uint m11, uint m12, uint m21, uint m22, uint m31, uint m32, uint m41, uint m42)
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
        /// Creates a new <see cref="UInt4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public UInt4x2(UInt2 row1, UInt2 row2, UInt2 row3, UInt2 row4)
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
        /// Gets a reference to a specific row in the current <see cref="UInt4x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[int row] => ref *(UInt2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref uint M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref uint M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref uint M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref uint M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Creates a new <see cref="UInt4x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt4x2"/> instance.</param>
        public static implicit operator UInt4x2(uint x)
        {
            UInt4x2 matrix;

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
        /// Sums two <see cref="UInt4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x2"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt4x2"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x2 operator +(UInt4x2 left, UInt4x2 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt4x2"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x2"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt4x2"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x2 operator /(UInt4x2 left, UInt4x2 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt4x2"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt4x2"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x2 operator *(UInt4x2 left, UInt4x2 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt4x2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt4x2"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x2 operator -(UInt4x2 left, UInt4x2 right) => default;
    }

    /// <inheritdoc cref="UInt4x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 48, Pack = 4)]
    public unsafe partial struct UInt4x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt4x3), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m13;

        [FieldOffset(12)]
        private uint m21;

        [FieldOffset(16)]
        private uint m22;

        [FieldOffset(20)]
        private uint m23;

        [FieldOffset(24)]
        private uint m31;

        [FieldOffset(28)]
        private uint m32;

        [FieldOffset(32)]
        private uint m33;

        [FieldOffset(36)]
        private uint m41;

        [FieldOffset(40)]
        private uint m42;

        [FieldOffset(44)]
        private uint m43;

        /// <summary>
        /// Creates a new <see cref="UInt4x3"/> instance with the specified parameters.
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
        public UInt4x3(uint m11, uint m12, uint m13, uint m21, uint m22, uint m23, uint m31, uint m32, uint m33, uint m41, uint m42, uint m43)
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
        /// Creates a new <see cref="UInt4x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public UInt4x3(UInt3 row1, UInt3 row2, UInt3 row3, UInt3 row4)
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
        /// Gets a reference to a specific row in the current <see cref="UInt4x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[int row] => ref *(UInt3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref uint M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref uint M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref uint M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref uint M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref uint M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref uint M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref uint M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 3].
        /// </summary>
        public readonly ref uint M43 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m43), 1));

        /// <summary>
        /// Creates a new <see cref="UInt4x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt4x3"/> instance.</param>
        public static implicit operator UInt4x3(uint x)
        {
            UInt4x3 matrix;

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
        /// Sums two <see cref="UInt4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x3"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt4x3"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x3 operator +(UInt4x3 left, UInt4x3 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt4x3"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x3"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt4x3"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x3 operator /(UInt4x3 left, UInt4x3 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt4x3"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x3"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt4x3"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x3 operator *(UInt4x3 left, UInt4x3 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt4x3"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x3"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt4x3"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x3 operator -(UInt4x3 left, UInt4x3 right) => default;
    }

    /// <inheritdoc cref="UInt4x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 64, Pack = 4)]
    public unsafe partial struct UInt4x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(UInt4x4), sizeof(UInt4));

        [FieldOffset(0)]
        private uint m11;

        [FieldOffset(4)]
        private uint m12;

        [FieldOffset(8)]
        private uint m13;

        [FieldOffset(12)]
        private uint m14;

        [FieldOffset(16)]
        private uint m21;

        [FieldOffset(20)]
        private uint m22;

        [FieldOffset(24)]
        private uint m23;

        [FieldOffset(28)]
        private uint m24;

        [FieldOffset(32)]
        private uint m31;

        [FieldOffset(36)]
        private uint m32;

        [FieldOffset(40)]
        private uint m33;

        [FieldOffset(44)]
        private uint m34;

        [FieldOffset(48)]
        private uint m41;

        [FieldOffset(52)]
        private uint m42;

        [FieldOffset(56)]
        private uint m43;

        [FieldOffset(60)]
        private uint m44;

        /// <summary>
        /// Creates a new <see cref="UInt4x4"/> instance with the specified parameters.
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
        public UInt4x4(uint m11, uint m12, uint m13, uint m14, uint m21, uint m22, uint m23, uint m24, uint m31, uint m32, uint m33, uint m34, uint m41, uint m42, uint m43, uint m44)
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
        /// Creates a new <see cref="UInt4x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public UInt4x4(UInt4 row1, UInt4 row2, UInt4 row3, UInt4 row4)
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
        /// Gets a reference to a specific row in the current <see cref="UInt4x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[int row] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(UInt2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(UInt3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="UInt4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref UInt4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(UInt4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref uint M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref uint M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref uint M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref uint M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref uint M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref uint M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref uint M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref uint M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref uint M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref uint M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref uint M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [3, 4].
        /// </summary>
        public readonly ref uint M34 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m34), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref uint M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref uint M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 3].
        /// </summary>
        public readonly ref uint M43 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m43), 1));

        /// <summary>
        /// Gets a reference to the <see cref="uint"/> value representing the component at position [4, 4].
        /// </summary>
        public readonly ref uint M44 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m44), 1));

        /// <summary>
        /// Creates a new <see cref="UInt4x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt4x4"/> instance.</param>
        public static implicit operator UInt4x4(uint x)
        {
            UInt4x4 matrix;

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
        /// Sums two <see cref="UInt4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x4"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt4x4"/> value to sum.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x4 operator +(UInt4x4 left, UInt4x4 right) => default;

        /// <summary>
        /// Divides two <see cref="UInt4x4"/> values (elementwise division).
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x4"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt4x4"/> value to divide.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x4 operator /(UInt4x4 left, UInt4x4 right) => default;

        /// <summary>
        /// Multiplies two <see cref="UInt4x4"/> values (elementwise product).
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x4"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt4x4"/> value to multiply.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x4 operator *(UInt4x4 left, UInt4x4 right) => default;

        /// <summary>
        /// Subtracts two <see cref="UInt4x4"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt4x4"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt4x4"/> value to subtract.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static UInt4x4 operator -(UInt4x4 left, UInt4x4 right) => default;
    }
}
