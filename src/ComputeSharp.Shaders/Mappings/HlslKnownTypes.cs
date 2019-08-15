using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Text.RegularExpressions;
using ComputeSharp.Graphics.Buffers;

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
            [typeof(int).FullName] = "int",
            [typeof(uint).FullName] = "uint",
            [typeof(float).FullName] = "float",
            [typeof(double).FullName] = "double",
            [typeof(Vector2).FullName] = "float2",
            [typeof(Vector3).FullName] = "float3",
            [typeof(Vector4).FullName] = "float4",
            [typeof(Matrix4x4).FullName] = "float4x4",
            [typeof(ThreadIds).FullName] = "uint3",
            [typeof(ReadWriteBuffer<>).FullName] = "RWStructuredBuffer"
        };

        /// <summary>
        /// Checks whether or not the input type is a known scalar type
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known HLSL scalar type</returns>
        [Pure]
        public static bool IsKnownScalarType(Type type) => type == typeof(bool) ||
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
        public static bool IsKnownVectorType(Type type) => type == typeof(Vector2) ||
                                                           type == typeof(Vector3) ||
                                                           type == typeof(Vector4) ||
                                                           type == typeof(Matrix4x4);

        /// <summary>
        /// Checks whether or not the input type is a <see cref="ReadWriteBuffer{T}"/> value
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a <see cref="ReadWriteBuffer{T}"/> instance</returns>
        [Pure]
        public static bool IsReadWriteBufferType(Type type) => type.IsGenericType &&
                                                               type.GetGenericTypeDefinition() == typeof(ReadWriteBuffer<>);

        /// <summary>
        /// Checks whether or not the input type is a <see cref="ReadOnlyBuffer{T}"/> value
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a <see cref="ReadOnlyBuffer{T}"/> instance</returns>
        [Pure]
        public static bool IsReadOnlyBufferType(Type type) => type.IsGenericType &&
                                                               type.GetGenericTypeDefinition() == typeof(ReadOnlyBuffer<>);

        /// <summary>
        /// Checks whether or not a given <see cref="Type"/> is an HLSL known type
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact a known HLSL type</returns>
        [Pure]
        public static bool IsKnownType(Type type)
        {
            Type declaredType = type.IsArray ? type.GetElementType() : type;
            string fullname = $"{declaredType.Namespace}{Type.Delimiter}{declaredType.Name}";

            return KnownTypes.ContainsKey(fullname);
        }

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
