using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Text.RegularExpressions;
using DirectX12GameEngine.Shaders.Primitives;

namespace DirectX12GameEngine.Shaders.Mappings
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
            [typeof(uint).FullName] = "uint",
            [typeof(int).FullName] = "int",
            [typeof(double).FullName] = "double",
            [typeof(float).FullName] = "float",
            [typeof(Vector2).FullName] = "float2",
            [typeof(Vector3).FullName] = "float3",
            [typeof(Vector4).FullName] = "float4",
            [typeof(Matrix4x4).FullName] = "float4x4",
            [typeof(ThreadIds).FullName] = "uint3",
            [typeof(RWBufferResource<>).FullName] = "RWBuffer"
        };

        /// <summary>
        /// Checks whether or not a given <see cref="Type"/> is an HLSL known type
        /// </summary>
        /// <param name="type">The input <see cref="Type"/> instance to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input <see cref="Type"/> is in fact an HLSL known type</returns>
        [Pure]
        public static bool IsKnownType(Type type)
        {
            Type declaredType = type.GetElementOrDeclaredType();
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
            Type declaredType = type.GetElementOrDeclaredType();
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
