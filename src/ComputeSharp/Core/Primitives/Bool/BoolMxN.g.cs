using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if !NET5_0
using RuntimeHelpers = ComputeSharp.SourceGenerators.Helpers.RuntimeHelpers;
using MemoryMarshal = ComputeSharp.SourceGenerators.Helpers.MemoryMarshal;
#endif

namespace ComputeSharp
{
    /// <inheritdoc cref="Bool1x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public unsafe partial struct Bool1x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool1x1), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        /// <summary>
        /// Creates a new <see cref="Bool1x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        public Bool1x1(bool m11)
        {
            this.m11 = m11;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool1x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref bool this[int row] => ref *(bool*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Creates a new <see cref="Bool1x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool1x1"/> instance.</param>
        public static implicit operator Bool1x1(bool x)
        {
            Bool1x1 matrix;

            matrix.m11 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool1x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool1x1"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool1x1 operator !(Bool1x1 matrix) => default;
    }

    /// <inheritdoc cref="Bool1x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public unsafe partial struct Bool1x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool1x2), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        /// <summary>
        /// Creates a new <see cref="Bool1x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        public Bool1x2(bool m11, bool m12)
        {
            this.m11 = m11;
            this.m12 = m12;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool1x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[int row] => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Creates a new <see cref="Bool1x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool1x2"/> instance.</param>
        public static implicit operator Bool1x2(bool x)
        {
            Bool1x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool1x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool1x2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool1x2 operator !(Bool1x2 matrix) => default;

        /// <summary>
        /// Casts a <see cref="Bool2"/> value to a <see cref="Bool1x2"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Bool2"/> value to cast.</param>
        public static implicit operator Bool1x2(Bool2 vector) => *(Bool1x2*)&vector;
    }

    /// <inheritdoc cref="Bool1x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct Bool1x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool1x3), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m13;

        /// <summary>
        /// Creates a new <see cref="Bool1x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        public Bool1x3(bool m11, bool m12, bool m13)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool1x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[int row] => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref bool M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Creates a new <see cref="Bool1x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool1x3"/> instance.</param>
        public static implicit operator Bool1x3(bool x)
        {
            Bool1x3 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool1x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool1x3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool1x3 operator !(Bool1x3 matrix) => default;

        /// <summary>
        /// Casts a <see cref="Bool3"/> value to a <see cref="Bool1x3"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Bool3"/> value to cast.</param>
        public static implicit operator Bool1x3(Bool3 vector) => *(Bool1x3*)&vector;
    }

    /// <inheritdoc cref="Bool1x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public unsafe partial struct Bool1x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool1x4), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m13;

        [FieldOffset(12)]
        private bool m14;

        /// <summary>
        /// Creates a new <see cref="Bool1x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        public Bool1x4(bool m11, bool m12, bool m13, bool m14)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool1x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[int row] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool1x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref bool M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref bool M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Creates a new <see cref="Bool1x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool1x4"/> instance.</param>
        public static implicit operator Bool1x4(bool x)
        {
            Bool1x4 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;
            matrix.m14 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool1x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool1x4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool1x4 operator !(Bool1x4 matrix) => default;

        /// <summary>
        /// Casts a <see cref="Bool4"/> value to a <see cref="Bool1x4"/> one.
        /// </summary>
        /// <param name="vector">The input <see cref="Bool4"/> value to cast.</param>
        public static implicit operator Bool1x4(Bool4 vector) => *(Bool1x4*)&vector;
    }

    /// <inheritdoc cref="Bool2x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
    public unsafe partial struct Bool2x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool2x1), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m21;

        /// <summary>
        /// Creates a new <see cref="Bool2x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        public Bool2x1(bool m11, bool m21)
        {
            this.m11 = m11;
            this.m21 = m21;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool2x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref bool this[int row] => ref *(bool*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Creates a new <see cref="Bool2x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool2x1"/> instance.</param>
        public static implicit operator Bool2x1(bool x)
        {
            Bool2x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool2x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool2x1"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool2x1 operator !(Bool2x1 matrix) => default;

        /// <summary>
        /// Casts a <see cref="Bool2x1"/> value to a <see cref="Bool2"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Bool2x1"/> value to cast.</param>
        public static implicit operator Bool2(Bool2x1 matrix) => *(Bool2*)&matrix;
    }

    /// <inheritdoc cref="Bool2x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public unsafe partial struct Bool2x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool2x2), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m21;

        [FieldOffset(12)]
        private bool m22;

        /// <summary>
        /// Creates a new <see cref="Bool2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        public Bool2x2(bool m11, bool m12, bool m21, bool m22)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
        }

        /// <summary>
        /// Creates a new <see cref="Bool2x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Bool2x2(Bool2 row1, Bool2 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool2x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[int row] => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Creates a new <see cref="Bool2x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool2x2"/> instance.</param>
        public static implicit operator Bool2x2(bool x)
        {
            Bool2x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m21 = x;
            matrix.m22 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool2x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool2x2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool2x2 operator !(Bool2x2 matrix) => default;
    }

    /// <inheritdoc cref="Bool2x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    public unsafe partial struct Bool2x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool2x3), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m13;

        [FieldOffset(12)]
        private bool m21;

        [FieldOffset(16)]
        private bool m22;

        [FieldOffset(20)]
        private bool m23;

        /// <summary>
        /// Creates a new <see cref="Bool2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        public Bool2x3(bool m11, bool m12, bool m13, bool m21, bool m22, bool m23)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
        }

        /// <summary>
        /// Creates a new <see cref="Bool2x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Bool2x3(Bool3 row1, Bool3 row2)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m13 = row1.Z;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m23 = row2.Z;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool2x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[int row] => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref bool M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref bool M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Creates a new <see cref="Bool2x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool2x3"/> instance.</param>
        public static implicit operator Bool2x3(bool x)
        {
            Bool2x3 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m13 = x;
            matrix.m21 = x;
            matrix.m22 = x;
            matrix.m23 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool2x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool2x3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool2x3 operator !(Bool2x3 matrix) => default;
    }

    /// <inheritdoc cref="Bool2x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 4)]
    public unsafe partial struct Bool2x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool2x4), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m13;

        [FieldOffset(12)]
        private bool m14;

        [FieldOffset(16)]
        private bool m21;

        [FieldOffset(20)]
        private bool m22;

        [FieldOffset(24)]
        private bool m23;

        [FieldOffset(28)]
        private bool m24;

        /// <summary>
        /// Creates a new <see cref="Bool2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m13">The value to assign to the component at position [1, 3].</param>
        /// <param name="m14">The value to assign to the component at position [1, 4].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m23">The value to assign to the component at position [2, 3].</param>
        /// <param name="m24">The value to assign to the component at position [2, 4].</param>
        public Bool2x4(bool m11, bool m12, bool m13, bool m14, bool m21, bool m22, bool m23, bool m24)
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
        /// Creates a new <see cref="Bool2x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        public Bool2x4(Bool4 row1, Bool4 row2)
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
        /// Gets a reference to a specific row in the current <see cref="Bool2x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[int row] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool2x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref bool M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref bool M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref bool M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref bool M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Creates a new <see cref="Bool2x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool2x4"/> instance.</param>
        public static implicit operator Bool2x4(bool x)
        {
            Bool2x4 matrix;

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
        /// Negates a <see cref="Bool2x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool2x4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool2x4 operator !(Bool2x4 matrix) => default;
    }

    /// <inheritdoc cref="Bool3x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 12, Pack = 4)]
    public unsafe partial struct Bool3x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool3x1), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m21;

        [FieldOffset(8)]
        private bool m31;

        /// <summary>
        /// Creates a new <see cref="Bool3x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        public Bool3x1(bool m11, bool m21, bool m31)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool3x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref bool this[int row] => ref *(bool*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref bool M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Creates a new <see cref="Bool3x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool3x1"/> instance.</param>
        public static implicit operator Bool3x1(bool x)
        {
            Bool3x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;
            matrix.m31 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool3x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool3x1"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool3x1 operator !(Bool3x1 matrix) => default;

        /// <summary>
        /// Casts a <see cref="Bool3x1"/> value to a <see cref="Bool3"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Bool3x1"/> value to cast.</param>
        public static implicit operator Bool3(Bool3x1 matrix) => *(Bool3*)&matrix;
    }

    /// <inheritdoc cref="Bool3x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 24, Pack = 4)]
    public unsafe partial struct Bool3x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool3x2), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m21;

        [FieldOffset(12)]
        private bool m22;

        [FieldOffset(16)]
        private bool m31;

        [FieldOffset(20)]
        private bool m32;

        /// <summary>
        /// Creates a new <see cref="Bool3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        public Bool3x2(bool m11, bool m12, bool m21, bool m22, bool m31, bool m32)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.m31 = m31;
            this.m32 = m32;
        }

        /// <summary>
        /// Creates a new <see cref="Bool3x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Bool3x2(Bool2 row1, Bool2 row2, Bool2 row3)
        {
            this.m11 = row1.X;
            this.m12 = row1.Y;
            this.m21 = row2.X;
            this.m22 = row2.Y;
            this.m31 = row3.X;
            this.m32 = row3.Y;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool3x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[int row] => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref bool M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref bool M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Creates a new <see cref="Bool3x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool3x2"/> instance.</param>
        public static implicit operator Bool3x2(bool x)
        {
            Bool3x2 matrix;

            matrix.m11 = x;
            matrix.m12 = x;
            matrix.m21 = x;
            matrix.m22 = x;
            matrix.m31 = x;
            matrix.m32 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool3x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool3x2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool3x2 operator !(Bool3x2 matrix) => default;
    }

    /// <inheritdoc cref="Bool3x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 36, Pack = 4)]
    public unsafe partial struct Bool3x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool3x3), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m13;

        [FieldOffset(12)]
        private bool m21;

        [FieldOffset(16)]
        private bool m22;

        [FieldOffset(20)]
        private bool m23;

        [FieldOffset(24)]
        private bool m31;

        [FieldOffset(28)]
        private bool m32;

        [FieldOffset(32)]
        private bool m33;

        /// <summary>
        /// Creates a new <see cref="Bool3x3"/> instance with the specified parameters.
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
        public Bool3x3(bool m11, bool m12, bool m13, bool m21, bool m22, bool m23, bool m31, bool m32, bool m33)
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
        /// Creates a new <see cref="Bool3x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Bool3x3(Bool3 row1, Bool3 row2, Bool3 row3)
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
        /// Gets a reference to a specific row in the current <see cref="Bool3x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[int row] => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref bool M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref bool M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref bool M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref bool M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref bool M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Creates a new <see cref="Bool3x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool3x3"/> instance.</param>
        public static implicit operator Bool3x3(bool x)
        {
            Bool3x3 matrix;

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
        /// Negates a <see cref="Bool3x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool3x3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool3x3 operator !(Bool3x3 matrix) => default;
    }

    /// <inheritdoc cref="Bool3x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 48, Pack = 4)]
    public unsafe partial struct Bool3x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool3x4), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m13;

        [FieldOffset(12)]
        private bool m14;

        [FieldOffset(16)]
        private bool m21;

        [FieldOffset(20)]
        private bool m22;

        [FieldOffset(24)]
        private bool m23;

        [FieldOffset(28)]
        private bool m24;

        [FieldOffset(32)]
        private bool m31;

        [FieldOffset(36)]
        private bool m32;

        [FieldOffset(40)]
        private bool m33;

        [FieldOffset(44)]
        private bool m34;

        /// <summary>
        /// Creates a new <see cref="Bool3x4"/> instance with the specified parameters.
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
        public Bool3x4(bool m11, bool m12, bool m13, bool m14, bool m21, bool m22, bool m23, bool m24, bool m31, bool m32, bool m33, bool m34)
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
        /// Creates a new <see cref="Bool3x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        public Bool3x4(Bool4 row1, Bool4 row2, Bool4 row3)
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
        /// Gets a reference to a specific row in the current <see cref="Bool3x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[int row] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool3x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref bool M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref bool M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref bool M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref bool M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref bool M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref bool M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref bool M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 4].
        /// </summary>
        public readonly ref bool M34 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m34), 1));

        /// <summary>
        /// Creates a new <see cref="Bool3x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool3x4"/> instance.</param>
        public static implicit operator Bool3x4(bool x)
        {
            Bool3x4 matrix;

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
        /// Negates a <see cref="Bool3x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool3x4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool3x4 operator !(Bool3x4 matrix) => default;
    }

    /// <inheritdoc cref="Bool4x1"/>
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public unsafe partial struct Bool4x1
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool4x1), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m21;

        [FieldOffset(8)]
        private bool m31;

        [FieldOffset(12)]
        private bool m41;

        /// <summary>
        /// Creates a new <see cref="Bool4x1"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        public Bool4x1(bool m11, bool m21, bool m31, bool m41)
        {
            this.m11 = m11;
            this.m21 = m21;
            this.m31 = m31;
            this.m41 = m41;
        }

        /// <summary>
        /// Gets a reference to a specific row in the current <see cref="Bool4x1"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref bool this[int row] => ref *(bool*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x1"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref bool M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref bool M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Creates a new <see cref="Bool4x1"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool4x1"/> instance.</param>
        public static implicit operator Bool4x1(bool x)
        {
            Bool4x1 matrix;

            matrix.m11 = x;
            matrix.m21 = x;
            matrix.m31 = x;
            matrix.m41 = x;

            return matrix;
        }

        /// <summary>
        /// Negates a <see cref="Bool4x1"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool4x1"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool4x1 operator !(Bool4x1 matrix) => default;

        /// <summary>
        /// Casts a <see cref="Bool4x1"/> value to a <see cref="Bool4"/> one.
        /// </summary>
        /// <param name="matrix">The input <see cref="Bool4x1"/> value to cast.</param>
        public static implicit operator Bool4(Bool4x1 matrix) => *(Bool4*)&matrix;
    }

    /// <inheritdoc cref="Bool4x2"/>
    [StructLayout(LayoutKind.Explicit, Size = 32, Pack = 4)]
    public unsafe partial struct Bool4x2
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool4x2), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m21;

        [FieldOffset(12)]
        private bool m22;

        [FieldOffset(16)]
        private bool m31;

        [FieldOffset(20)]
        private bool m32;

        [FieldOffset(24)]
        private bool m41;

        [FieldOffset(28)]
        private bool m42;

        /// <summary>
        /// Creates a new <see cref="Bool4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="m11">The value to assign to the component at position [1, 1].</param>
        /// <param name="m12">The value to assign to the component at position [1, 2].</param>
        /// <param name="m21">The value to assign to the component at position [2, 1].</param>
        /// <param name="m22">The value to assign to the component at position [2, 2].</param>
        /// <param name="m31">The value to assign to the component at position [3, 1].</param>
        /// <param name="m32">The value to assign to the component at position [3, 2].</param>
        /// <param name="m41">The value to assign to the component at position [4, 1].</param>
        /// <param name="m42">The value to assign to the component at position [4, 2].</param>
        public Bool4x2(bool m11, bool m12, bool m21, bool m22, bool m31, bool m32, bool m41, bool m42)
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
        /// Creates a new <see cref="Bool4x2"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Bool4x2(Bool2 row1, Bool2 row2, Bool2 row3, Bool2 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Bool4x2"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[int row] => ref *(Bool2*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x2"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref bool M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref bool M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref bool M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref bool M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Creates a new <see cref="Bool4x2"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool4x2"/> instance.</param>
        public static implicit operator Bool4x2(bool x)
        {
            Bool4x2 matrix;

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
        /// Negates a <see cref="Bool4x2"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool4x2"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool4x2 operator !(Bool4x2 matrix) => default;
    }

    /// <inheritdoc cref="Bool4x3"/>
    [StructLayout(LayoutKind.Explicit, Size = 48, Pack = 4)]
    public unsafe partial struct Bool4x3
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool4x3), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m13;

        [FieldOffset(12)]
        private bool m21;

        [FieldOffset(16)]
        private bool m22;

        [FieldOffset(20)]
        private bool m23;

        [FieldOffset(24)]
        private bool m31;

        [FieldOffset(28)]
        private bool m32;

        [FieldOffset(32)]
        private bool m33;

        [FieldOffset(36)]
        private bool m41;

        [FieldOffset(40)]
        private bool m42;

        [FieldOffset(44)]
        private bool m43;

        /// <summary>
        /// Creates a new <see cref="Bool4x3"/> instance with the specified parameters.
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
        public Bool4x3(bool m11, bool m12, bool m13, bool m21, bool m22, bool m23, bool m31, bool m32, bool m33, bool m41, bool m42, bool m43)
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
        /// Creates a new <see cref="Bool4x3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Bool4x3(Bool3 row1, Bool3 row2, Bool3 row3, Bool3 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Bool4x3"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[int row] => ref *(Bool3*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x3"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref bool M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref bool M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref bool M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref bool M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref bool M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref bool M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref bool M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 3].
        /// </summary>
        public readonly ref bool M43 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m43), 1));

        /// <summary>
        /// Creates a new <see cref="Bool4x3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool4x3"/> instance.</param>
        public static implicit operator Bool4x3(bool x)
        {
            Bool4x3 matrix;

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
        /// Negates a <see cref="Bool4x3"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool4x3"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool4x3 operator !(Bool4x3 matrix) => default;
    }

    /// <inheritdoc cref="Bool4x4"/>
    [StructLayout(LayoutKind.Explicit, Size = 64, Pack = 4)]
    public unsafe partial struct Bool4x4
    {
        /// <summary>
        /// A private buffer to which the undefined properties will point to.
        /// </summary>
        private static readonly void* UndefinedData = (void*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(Bool4x4), sizeof(Bool4));

        [FieldOffset(0)]
        private bool m11;

        [FieldOffset(4)]
        private bool m12;

        [FieldOffset(8)]
        private bool m13;

        [FieldOffset(12)]
        private bool m14;

        [FieldOffset(16)]
        private bool m21;

        [FieldOffset(20)]
        private bool m22;

        [FieldOffset(24)]
        private bool m23;

        [FieldOffset(28)]
        private bool m24;

        [FieldOffset(32)]
        private bool m31;

        [FieldOffset(36)]
        private bool m32;

        [FieldOffset(40)]
        private bool m33;

        [FieldOffset(44)]
        private bool m34;

        [FieldOffset(48)]
        private bool m41;

        [FieldOffset(52)]
        private bool m42;

        [FieldOffset(56)]
        private bool m43;

        [FieldOffset(60)]
        private bool m44;

        /// <summary>
        /// Creates a new <see cref="Bool4x4"/> instance with the specified parameters.
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
        public Bool4x4(bool m11, bool m12, bool m13, bool m14, bool m21, bool m22, bool m23, bool m24, bool m31, bool m32, bool m33, bool m34, bool m41, bool m42, bool m43, bool m44)
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
        /// Creates a new <see cref="Bool4x4"/> instance with the specified parameters.
        /// </summary>
        /// <param name="row1">The value to assign to the row at position [1].</param>
        /// <param name="row2">The value to assign to the row at position [2].</param>
        /// <param name="row3">The value to assign to the row at position [3].</param>
        /// <param name="row4">The value to assign to the row at position [4].</param>
        public Bool4x4(Bool4 row1, Bool4 row2, Bool4 row3, Bool4 row4)
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
        /// Gets a reference to a specific row in the current <see cref="Bool4x4"/> instance.
        /// </summary>
        /// <param name="row">The row to access.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[int row] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool2 this[MatrixIndex xy0, MatrixIndex xy1] => ref *(Bool2*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool3 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2] => ref *(Bool3*)UndefinedData;
        
        /// <summary>
        /// Gets a swizzled reference to a specific sequence of items in the current <see cref="Bool4x4"/> instance.
        /// </summary>
        /// <param name="xy0">The identifier of the first item to index.</param>
        /// <param name="xy1">The identifier of the second item to index.</param>
        /// <param name="xy2">The identifier of the third item to index.</param>
        /// <param name="xy3">The identifier of the fourth item to index.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public readonly ref Bool4 this[MatrixIndex xy0, MatrixIndex xy1, MatrixIndex xy2, MatrixIndex xy3] => ref *(Bool4*)UndefinedData;

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 1].
        /// </summary>
        public readonly ref bool M11 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m11), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 2].
        /// </summary>
        public readonly ref bool M12 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m12), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 3].
        /// </summary>
        public readonly ref bool M13 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m13), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [1, 4].
        /// </summary>
        public readonly ref bool M14 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m14), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 1].
        /// </summary>
        public readonly ref bool M21 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m21), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 2].
        /// </summary>
        public readonly ref bool M22 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m22), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 3].
        /// </summary>
        public readonly ref bool M23 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m23), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [2, 4].
        /// </summary>
        public readonly ref bool M24 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m24), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 1].
        /// </summary>
        public readonly ref bool M31 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m31), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 2].
        /// </summary>
        public readonly ref bool M32 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m32), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 3].
        /// </summary>
        public readonly ref bool M33 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m33), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [3, 4].
        /// </summary>
        public readonly ref bool M34 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m34), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 1].
        /// </summary>
        public readonly ref bool M41 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m41), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 2].
        /// </summary>
        public readonly ref bool M42 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m42), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 3].
        /// </summary>
        public readonly ref bool M43 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m43), 1));

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the component at position [4, 4].
        /// </summary>
        public readonly ref bool M44 => ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in this.m44), 1));

        /// <summary>
        /// Creates a new <see cref="Bool4x4"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool4x4"/> instance.</param>
        public static implicit operator Bool4x4(bool x)
        {
            Bool4x4 matrix;

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
        /// Negates a <see cref="Bool4x4"/> value.
        /// </summary>
        /// <param name="matrix">The <see cref="Bool4x4"/> value to negate.</param>
        /// <remarks>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</remarks>
        public static Bool4x4 operator !(Bool4x4 matrix) => default;
    }
}
