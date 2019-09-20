using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ComputeSharp.Shaders.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL type names to common .NET types
    /// </summary>
    internal static class HlslKnownTypes
    {
        /// <summary>
        /// The mapping of supported known types to HLSL types
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> KnownTypes = new Dictionary<string, string>
        {
            [typeof(bool).FullName] = "bool",
            [typeof(Bool).FullName] = "bool",
            [typeof(Bool2).FullName] = "bool2",
            [typeof(Bool3).FullName] = "bool3",
            [typeof(Bool4).FullName] = "bool4",
            [typeof(int).FullName] = "int",
            [typeof(Int2).FullName] = "int2",
            [typeof(Int3).FullName] = "int3",
            [typeof(Int4).FullName] = "int4",
            [typeof(uint).FullName] = "uint",
            [typeof(UInt2).FullName] = "uint2",
            [typeof(UInt3).FullName] = "uint3",
            [typeof(UInt4).FullName] = "uint4",
            [typeof(float).FullName] = "float",
            [typeof(Float2).FullName] = "float2",
            [typeof(Float3).FullName] = "float3",
            [typeof(Float4).FullName] = "float4",
            [typeof(Vector2).FullName] = "float2",
            [typeof(Vector3).FullName] = "float3",
            [typeof(Vector4).FullName] = "float4",
            [typeof(double).FullName] = "double",
            [typeof(Double2).FullName] = "double2",
            [typeof(Double3).FullName] = "double3",
            [typeof(Double4).FullName] = "double4",
            [typeof(ThreadIds).FullName] = "uint3",
            [typeof(ReadOnlyBuffer<>).FullName] = "StructuredBuffer",
            [typeof(ReadWriteBuffer<>).FullName] = "RWStructuredBuffer"
        };

        private static HashSet<Type> _HlslMappedVectorTypes { get; } = new HashSet<Type>(new[]
        {
            typeof(Bool2), typeof(Bool3), typeof(Bool4),
            typeof(Int2), typeof(Int3), typeof(Int4),
            typeof(UInt2), typeof(UInt3), typeof(UInt4),
            typeof(Float2), typeof(Float3), typeof(Float4),
            typeof(Double2), typeof(Double3), typeof(Double4)
        });

        /// <summary>
        /// Gets the known HLSL vector types available as mapped types
        /// </summary>
        public static IReadOnlyCollection<Type> HlslMappedVectorTypes => _HlslMappedVectorTypes;

        /// <summary>
        /// Checks whether or not the input type is a known HLSL-compatible type
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known HLSL type</returns>
        [Pure]
        public static bool IsKnownType(Type type) => IsKnownScalarType(type) ||
                                                     IsKnownVectorType(type) ||
                                                     IsConstantBufferType(type) ||
                                                     IsReadOnlyBufferType(type) ||
                                                     IsReadWriteBufferType(type);

        /// <summary>
        /// Checks whether or not the input type is a known scalar type
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known HLSL scalar type</returns>
        [Pure]
        public static bool IsKnownScalarType(Type type) => type == typeof(bool) ||
                                                           type == typeof(Bool) ||
                                                           type == typeof(int) ||
                                                           type == typeof(uint) ||
                                                           type == typeof(float) ||
                                                           type == typeof(double);

        /// <summary>
        /// Checks whether or not the input type is a known vector type
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known HLSL vector type</returns>
        [Pure]
        public static bool IsKnownVectorType(Type type) => _HlslMappedVectorTypes.Contains(type) ||
                                                           type == typeof(Vector2) ||
                                                           type == typeof(Vector3) ||
                                                           type == typeof(Vector4);

        /// <summary>
        /// Checks whether or not the input type is a known buffer type
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known buffer type</returns>
        [Pure]
        public static bool IsKnownBufferType(Type type) => IsConstantBufferType(type) ||
                                                           IsReadOnlyBufferType(type) ||
                                                           IsReadWriteBufferType(type);

        /// <summary>
        /// Checks whether or not the input type is a <see cref="ConstantBuffer{T}"/> value
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a <see cref="ConstantBuffer{T}"/> instance</returns>
        [Pure]
        public static bool IsConstantBufferType(Type type) => type.IsGenericType &&
                                                              type.GetGenericTypeDefinition() == typeof(ConstantBuffer<>);

        /// <summary>
        /// Checks whether or not the input type is a <see cref="ReadOnlyBuffer{T}"/> value
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a <see cref="ReadOnlyBuffer{T}"/> instance</returns>
        [Pure]
        public static bool IsReadOnlyBufferType(Type type) => type.IsGenericType &&
                                                              type.GetGenericTypeDefinition() == typeof(ReadOnlyBuffer<>);

        /// <summary>
        /// Checks whether or not the input type is a <see cref="ReadWriteBuffer{T}"/> value
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a <see cref="ReadWriteBuffer{T}"/> instance</returns>
        [Pure]
        public static bool IsReadWriteBufferType(Type type) => type.IsGenericType &&
                                                               type.GetGenericTypeDefinition() == typeof(ReadWriteBuffer<>);

        /// <summary>
        /// Gets the mapped HLSL-compatible type name for the input <see cref="Type"/> instance
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to map</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader</returns>
        [Pure]
        public static string GetMappedName(Type type)
        {
            Type declaredType = type.IsArray ? type.GetElementType() : type;
            string
                fullname = $"{declaredType.Namespace}{Type.Delimiter}{declaredType.Name}",
                mappedName = KnownTypes.TryGetValue(fullname, out string mapped) ? mapped : declaredType.Name,
                hlslName = declaredType.IsGenericType ? mappedName + $"<{GetMappedName(declaredType.GetGenericArguments()[0])}>" : mappedName;

            return hlslName;
        }

        /// <summary>
        /// Gets the mapped HLSL-compatible type name for the input type name
        /// </summary>
        /// <param name="name">The input type name to map</param>
        /// <returns>The HLSL-compatible type name that can be used in an HLSL shader</returns>
        [Pure]
        public static string GetMappedName(string name)
        {
            int indexOfOpenBracket = name.IndexOf('<');
            string
                genericArguments = indexOfOpenBracket >= 0 ? name.Substring(indexOfOpenBracket) : string.Empty,
                polishedName = indexOfOpenBracket >= 0 ? name.Remove(indexOfOpenBracket) + "`1" : name,
                mappedName = KnownTypes.TryGetValue(polishedName, out string mapped) ? mapped : Regex.Match(polishedName, @"[^\.]+$").Value;

            return $"{mappedName}{genericArguments}";
        }
    }
}
