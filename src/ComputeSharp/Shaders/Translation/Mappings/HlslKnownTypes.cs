using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace ComputeSharp.Shaders.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL type names to common .NET types.
    /// </summary>
    internal static class HlslKnownTypes
    {
        /// <summary>
        /// The set of HLSL vector types.
        /// </summary>
        private static HashSet<Type> HlslMappedVectorTypes { get; } = new(new[]
        {
            typeof(Bool2), typeof(Bool3), typeof(Bool4),
            typeof(Int2), typeof(Int3), typeof(Int4),
            typeof(UInt2), typeof(UInt3), typeof(UInt4),
            typeof(Float2), typeof(Float3), typeof(Float4),
            typeof(Double2), typeof(Double3), typeof(Double4)
        });

        /// <summary>
        /// The set of HLSL matrix types.
        /// </summary>
        private static HashSet<Type> HlslMappedMatrixTypes { get; } = new(new[]
        {
            typeof(Bool1x1), typeof(Bool1x2), typeof(Bool1x3), typeof(Bool1x4),
            typeof(Bool2x1), typeof(Bool2x2), typeof(Bool2x3), typeof(Bool2x4),
            typeof(Bool3x1), typeof(Bool3x2), typeof(Bool3x3), typeof(Bool3x4),
            typeof(Bool4x1), typeof(Bool4x2), typeof(Bool4x3), typeof(Bool4x4),
            typeof(Int1x1), typeof(Int1x2), typeof(Int1x3), typeof(Int1x4),
            typeof(Int2x1), typeof(Int2x2), typeof(Int2x3), typeof(Int2x4),
            typeof(Int3x1), typeof(Int3x2), typeof(Int3x3), typeof(Int3x4),
            typeof(Int4x1), typeof(Int4x2), typeof(Int4x3), typeof(Int4x4),
            typeof(UInt1x1), typeof(UInt1x2), typeof(UInt1x3), typeof(UInt1x4),
            typeof(UInt2x1), typeof(UInt2x2), typeof(UInt2x3), typeof(UInt2x4),
            typeof(UInt3x1), typeof(UInt3x2), typeof(UInt3x3), typeof(UInt3x4),
            typeof(UInt4x1), typeof(UInt4x2), typeof(UInt4x3), typeof(UInt4x4),
            typeof(Float1x1), typeof(Float1x2), typeof(Float1x3), typeof(Float1x4),
            typeof(Float2x1), typeof(Float2x2), typeof(Float2x3), typeof(Float2x4),
            typeof(Float3x1), typeof(Float3x2), typeof(Float3x3), typeof(Float3x4),
            typeof(Float4x1), typeof(Float4x2), typeof(Float4x3), typeof(Float4x4),
            typeof(Double1x1), typeof(Double1x2), typeof(Double1x3), typeof(Double1x4),
            typeof(Double2x1), typeof(Double2x2), typeof(Double2x3), typeof(Double2x4),
            typeof(Double3x1), typeof(Double3x2), typeof(Double3x3), typeof(Double3x4),
            typeof(Double4x1), typeof(Double4x2), typeof(Double4x3), typeof(Double4x4)
        });

        /// <summary>
        /// Checks whether or not the input type is a known scalar type.
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check.</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known HLSL scalar type.</returns>
        [Pure]
        public static bool IsKnownScalarType(Type type) => type == typeof(bool) ||
                                                           type == typeof(Bool) ||
                                                           type == typeof(int) ||
                                                           type == typeof(uint) ||
                                                           type == typeof(float) ||
                                                           type == typeof(double);

        /// <summary>
        /// Checks whether or not the input type is a known vector type.
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check.</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known HLSL vector type.</returns>
        [Pure]
        public static bool IsKnownVectorType(Type type) => HlslMappedVectorTypes.Contains(type) ||
                                                           type == typeof(Vector2) ||
                                                           type == typeof(Vector3) ||
                                                           type == typeof(Vector4);

        /// <summary>
        /// Checks whether or not the input type is a known matrix type.
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check.</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known HLSL matrix type.</returns>
        [Pure]
        public static bool IsKnownMatrixType(Type type) => HlslMappedMatrixTypes.Contains(type);

        /// <summary>
        /// Checks whether or not the input type is a known buffer type.
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check.</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known buffer type.</returns>
        [Pure]
        public static bool IsKnownBufferType(Type type) => IsConstantBufferType(type) ||
                                                           IsReadOnlyResourceType(type) ||
                                                           IsReadWriteResourceType(type);

        /// <summary>
        /// Checks whether or not the input type is a <see cref="ConstantBuffer{T}"/> value.
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check.</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a <see cref="ConstantBuffer{T}"/> instance.</returns>
        [Pure]
        public static bool IsConstantBufferType(Type type) => type.IsGenericType &&
                                                              type.GetGenericTypeDefinition() == typeof(ConstantBuffer<>);

        /// <summary>
        /// Checks whether or not the input type is a known readonly resource type (buffer or texture).
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check.</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is a readonly resource type.</returns>
        [Pure]
        public static bool IsReadOnlyResourceType(Type type)
        {
            if (!type.IsGenericType) return false;

            Type genericType = type.GetGenericTypeDefinition();

            return
                genericType == typeof(ReadOnlyBuffer<>) ||
                genericType == typeof(ReadOnlyTexture2D<>) ||
                genericType == typeof(ReadOnlyTexture2D<,>) ||
                genericType == typeof(ReadOnlyTexture3D<>) ||
                genericType == typeof(ReadOnlyTexture3D<,>) ||
                genericType == typeof(IReadOnlyTexture2D<>) ||
                genericType == typeof(IReadOnlyTexture3D<>);
        }

        /// <summary>
        /// Checks whether or not the input type is a known writeable resource type (buffer or texture).
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check.</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is a writeable resource type.</returns>
        [Pure]
        public static bool IsReadWriteResourceType(Type type)
        {
            if (!type.IsGenericType) return false;

            Type genericType = type.GetGenericTypeDefinition();

            return
                genericType == typeof(ReadWriteBuffer<>) ||
                genericType == typeof(ReadWriteTexture2D<>) ||
                genericType == typeof(ReadWriteTexture2D<,>) ||
                genericType == typeof(ReadWriteTexture3D<>) ||
                genericType == typeof(ReadWriteTexture3D<,>) ||
                genericType == typeof(IReadWriteTexture2D<>) ||
                genericType == typeof(IReadWriteTexture3D<>); ;
        }
    }
}
