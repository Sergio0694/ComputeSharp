using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Attributes;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.Shaders.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL method names to common .NET method
    /// </summary>
    internal static class HlslKnownMethods
    {
        private static IReadOnlyDictionary<string, string>? _KnownMethods;

        /// <summary>
        /// The mapping of supported known methods to HLSL methods
        /// </summary>
        private static IReadOnlyDictionary<string, string> KnownMethods
        {
            get
            {
                if (_KnownMethods != null) return _KnownMethods;

                // Initialize the initial mappings
                Dictionary<string, string> knownMethods = new Dictionary<string, string>
                {
                    // Math
                    ["System.Math.Max"] = "max",
                    ["System.Math.Min"] = "min",
                    ["System.Math.Pow"] = "pow",
                    ["System.Math.Sin"] = "sin",
                    ["System.Math.Sinh"] = "sinh",
                    ["System.Math.Asin"] = "asin",
                    ["System.Math.Cos"] = "cos",
                    ["System.Math.Cosh"] = "cosh",
                    ["System.Math.Acos"] = "acos",
                    ["System.Math.Tan"] = "tan",
                    ["System.Math.Tanh"] = "tanh",
                    ["System.Math.Atan"] = "atan",
                    ["System.Math.Atan2"] = "atan2",
                    ["System.Math.Ceiling"] = "ceil",
                    ["System.Math.Floor"] = "floor",
                    ["System.Math.Clamp"] = "clamp",
                    ["System.Math.Exp"] = "exp",
                    ["System.Math.Log"] = "log",
                    ["System.Math.Log10"] = "log10",
                    ["System.Math.Round"] = "round",
                    ["System.Math.Sqrt"] = "sqrt",
                    ["System.Math.Sign"] = "sign",
                    ["System.Math.Truncate"] = "trunc",
                    ["System.Math.PI"] = "3.1415926535897931",

                    // MathF
                    ["System.MathF.Max"] = "max",
                    ["System.MathF.Min"] = "min",
                    ["System.MathF.Pow"] = "pow",
                    ["System.MathF.Sin"] = "sin",
                    ["System.MathF.Sinh"] = "sinh",
                    ["System.MathF.Asin"] = "asin",
                    ["System.MathF.Cos"] = "cos",
                    ["System.MathF.Cosh"] = "cosh",
                    ["System.MathF.Acos"] = "acos",
                    ["System.MathF.Tan"] = "tan",
                    ["System.MathF.Tanh"] = "tanh",
                    ["System.MathF.Atan"] = "atan",
                    ["System.MathF.Atan2"] = "atan2",
                    ["System.MathF.Ceiling"] = "ceil",
                    ["System.MathF.Floor"] = "floor",
                    ["System.MathF.Clamp"] = "clamp",
                    ["System.MathF.Exp"] = "exp",
                    ["System.MathF.Log"] = "log",
                    ["System.MathF.Log10"] = "log10",
                    ["System.MathF.Round"] = "round",
                    ["System.MathF.Sqrt"] = "sqrt",
                    ["System.MathF.Sign"] = "sign",
                    ["System.MathF.Truncate"] = "trunc",
                    ["System.MathF.PI"] = "3.14159274f",

                    // Float
                    ["float.IsFinite"] = "isfinite",
                    ["float.IsInfinity"] = "isinf",
                    ["float.IsNaN"] = "isnan",

                    // ThreadIds
                    ["ComputeSharp.ThreadIds.X"] = ".x",
                    ["ComputeSharp.ThreadIds.Y"] = ".y",
                    ["ComputeSharp.ThreadIds.Z"] = ".z",

                    // Vector2
                    ["System.Numerics.Vector2.X"] = ".x",
                    ["System.Numerics.Vector2.Y"] = ".y",
                    ["System.Numerics.Vector2.Dot"] = "dot",
                    ["System.Numerics.Vector2.Lerp"] = "lerp",
                    ["System.Numerics.Vector2.Transform"] = "mul",
                    ["System.Numerics.Vector2.TransformNormal"] = "mul",
                    ["System.Numerics.Vector2.Normalize"] = "normalize",
                    ["System.Numerics.Vector2.Zero"] = "(float2)0",
                    ["System.Numerics.Vector2.One"] = "float2(1.0f, 1.0f)",
                    ["System.Numerics.Vector2.UnitX"] = "float2(1.0f, 0.0f)",
                    ["System.Numerics.Vector2.UnitY"] = "float2(0.0f, 1.0f)",

                    // Vector3
                    ["System.Numerics.Vector3.X"] = ".x",
                    ["System.Numerics.Vector3.Y"] = ".y",
                    ["System.Numerics.Vector3.Z"] = ".z",
                    ["System.Numerics.Vector3.Cross"] = "cross",
                    ["System.Numerics.Vector3.Dot"] = "dot",
                    ["System.Numerics.Vector3.Lerp"] = "lerp",
                    ["System.Numerics.Vector3.Transform"] = "mul",
                    ["System.Numerics.Vector3.TransformNormal"] = "mul",
                    ["System.Numerics.Vector3.Normalize"] = "normalize",
                    ["System.Numerics.Vector3.Zero"] = "(float3)0",
                    ["System.Numerics.Vector3.One"] = "float3(1.0f, 1.0f, 1.0f)",
                    ["System.Numerics.Vector3.UnitX"] = "float3(1.0f, 0.0f, 0.0f)",
                    ["System.Numerics.Vector3.UnitY"] = "float3(0.0f, 1.0f, 0.0f)",
                    ["System.Numerics.Vector3.UnitZ"] = "float3(0.0f, 0.0f, 1.0f)",

                    // Vector4
                    ["System.Numerics.Vector4.X"] = ".x",
                    ["System.Numerics.Vector4.Y"] = ".y",
                    ["System.Numerics.Vector4.Z"] = ".z",
                    ["System.Numerics.Vector4.W"] = ".w",
                    ["System.Numerics.Vector4.Lerp"] = "lerp",
                    ["System.Numerics.Vector4.Transform"] = "mul",
                    ["System.Numerics.Vector4.Normalize"] = "normalize",
                    ["System.Numerics.Vector4.Zero"] = "(float4)0",
                    ["System.Numerics.Vector4.One"] = "float4(1.0f, 1.0f, 1.0f, 1.0f)",

                    // Bool2
                    ["ComputeSharp.Bool2.False"] = "(bool2)0",
                    ["ComputeSharp.Bool2.True"] = "bool2(true, true)",
                    ["ComputeSharp.Bool2.TrueX"] = "bool2(true, false)",
                    ["ComputeSharp.Bool2.TrueY"] = "bool2(false, true)",

                    // Bool3
                    ["ComputeSharp.Bool3.False"] = "(bool3)0",
                    ["ComputeSharp.Bool3.True"] = "bool3(true, true, true)",
                    ["ComputeSharp.Bool3.TrueX"] = "bool3(true, false, false)",
                    ["ComputeSharp.Bool3.TrueY"] = "bool3(false, true, false)",
                    ["ComputeSharp.Bool3.TrueZ"] = "bool3(false, false, true)",

                    // Bool4
                    ["ComputeSharp.Bool4.False"] = "(bool4)0",
                    ["ComputeSharp.Bool4.True"] = "bool4(true, true, true, true)",
                    ["ComputeSharp.Bool4.TrueX"] = "bool4(true, false, false, false)",
                    ["ComputeSharp.Bool4.TrueY"] = "bool4(false, true, false, false)",
                    ["ComputeSharp.Bool4.TrueZ"] = "bool4(false, false, true, false)",
                    ["ComputeSharp.Bool4.TrueW"] = "bool4(false, false, false, true)",

                    // Int2
                    ["ComputeSharp.Int2.Zero"] = "(int2)0",
                    ["ComputeSharp.Int2.One"] = "int2(1, 1)",
                    ["ComputeSharp.Int2.UnitX"] = "int2(1, 0)",
                    ["ComputeSharp.Int2.UnitY"] = "int2(0, 1)",

                    // Int3
                    ["ComputeSharp.Int3.Zero"] = "(int3)0",
                    ["ComputeSharp.Int3.One"] = "int3(1, 1, 1)",
                    ["ComputeSharp.Int3.UnitX"] = "int3(1, 0, 0)",
                    ["ComputeSharp.Int3.UnitY"] = "int3(0, 1, 0)",
                    ["ComputeSharp.Int3.UnitZ"] = "int3(0, 0, 1)",

                    // Int4
                    ["ComputeSharp.Int4.Zero"] = "(int4)0",
                    ["ComputeSharp.Int4.One"] = "int4(1, 1, 1, 1)",
                    ["ComputeSharp.Int4.UnitX"] = "int4(1, 0, 0, 0)",
                    ["ComputeSharp.Int4.UnitY"] = "int4(0, 1, 0, 0)",
                    ["ComputeSharp.Int4.UnitZ"] = "int4(0, 0, 1, 0)",
                    ["ComputeSharp.Int4.UnitW"] = "int4(0, 0, 0, 1)",

                    // UInt2
                    ["ComputeSharp.UInt2.Zero"] = "(uint2)0",
                    ["ComputeSharp.UInt2.One"] = "uint2(1u, 1u)",
                    ["ComputeSharp.UInt2.UnitX"] = "uint2(1u, 0u)",
                    ["ComputeSharp.UInt2.UnitY"] = "uint2(0u, 1u)",

                    // UInt3
                    ["ComputeSharp.UInt3.Zero"] = "(uint3)0",
                    ["ComputeSharp.UInt3.One"] = "uint3(1u, 1u, 1u)",
                    ["ComputeSharp.UInt3.UnitX"] = "uint3(1u, 0u, 0u)",
                    ["ComputeSharp.UInt3.UnitY"] = "uint3(0u, 1u, 0u)",
                    ["ComputeSharp.UInt3.UnitZ"] = "uint3(0u, 0u, 1u)",

                    // UInt4
                    ["ComputeSharp.UInt4.Zero"] = "(uint4)0",
                    ["ComputeSharp.UInt4.One"] = "uint4(1u, 1u, 1u, 1u)",
                    ["ComputeSharp.UInt4.UnitX"] = "uint4(1u, 0u, 0u, 0u)",
                    ["ComputeSharp.UInt4.UnitY"] = "uint4(0u, 1u, 0u, 0u)",
                    ["ComputeSharp.UInt4.UnitZ"] = "uint4(0u, 0u, 1u, 0u)",
                    ["ComputeSharp.UInt4.UnitW"] = "uint4(0u, 0u, 0u, 1u)",

                    // Float2
                    ["ComputeSharp.Float2.Zero"] = "(float2)0",
                    ["ComputeSharp.Float2.One"] = "float2(1.0f, 1.0f)",
                    ["ComputeSharp.Float2.UnitX"] = "float2(1.0f, 0.0f)",
                    ["ComputeSharp.Float2.UnitY"] = "float2(0.0f, 1.0f)",

                    // Float3
                    ["ComputeSharp.Float3.Zero"] = "(float3)0",
                    ["ComputeSharp.Float3.One"] = "float3(1.0f, 1.0f, 1.0f)",
                    ["ComputeSharp.Float3.UnitX"] = "float3(1.0f, 0.0f, 0.0f)",
                    ["ComputeSharp.Float3.UnitY"] = "float3(0.0f, 1.0f, 0.0f)",
                    ["ComputeSharp.Float3.UnitZ"] = "float3(0.0f, 0.0f, 1.0f)",

                    // Float4
                    ["ComputeSharp.Float4.Zero"] = "(float4)0",
                    ["ComputeSharp.Float4.One"] = "float4(1.0f, 1.0f, 1.0f, 1.0f)",
                    ["ComputeSharp.Float4.UnitX"] = "float4(1.0f, 0.0f, 0.0f, 0.0f)",
                    ["ComputeSharp.Float4.UnitY"] = "float4(0.0f, 1.0f, 0.0f, 0.0f)",
                    ["ComputeSharp.Float4.UnitZ"] = "float4(0.0f, 0.0f, 1.0f, 0.0f)",
                    ["ComputeSharp.Float4.UnitW"] = "float4(0.0f, 0.0f, 0.0f, 1.0f)",

                    // Double2
                    ["ComputeSharp.Double2.Zero"] = "(double2)0",
                    ["ComputeSharp.Double2.One"] = "double2(1.0, 1.0)",
                    ["ComputeSharp.Double2.UnitX"] = "double2(1.0, 0.0)",
                    ["ComputeSharp.Double2.UnitY"] = "double2(0.0, 1.0)",

                    // Double3
                    ["ComputeSharp.Double3.Zero"] = "(double3)0",
                    ["ComputeSharp.Double3.One"] = "double3(1.0, 1.0, 1.0)",
                    ["ComputeSharp.Double3.UnitX"] = "double3(1.0, 0.0, 0.0)",
                    ["ComputeSharp.Double3.UnitY"] = "double3(0.0, 1.0, 0.0)",
                    ["ComputeSharp.Double3.UnitZ"] = "double3(0.0, 0.0, 1.0)",

                    // Double4
                    ["ComputeSharp.Double4.Zero"] = "(double4)0",
                    ["ComputeSharp.Double4.One"] = "double4(1.0, 1.0, 1.0, 1.0)",
                    ["ComputeSharp.Double4.UnitX"] = "double4(1.0, 0.0, 0.0, 0.0)",
                    ["ComputeSharp.Double4.UnitY"] = "double4(0.0, 1.0, 0.0, 0.0)",
                    ["ComputeSharp.Double4.UnitZ"] = "double4(0.0, 0.0, 1.0, 0.0)",
                    ["ComputeSharp.Double4.UnitW"] = "double4(0.0, 0.0, 0.0, 1.0)"
                };

                // Programmatically load mappings from the Hlsl class as well
                foreach (var method in
                    from method in typeof(Hlsl).GetMethods(BindingFlags.Public | BindingFlags.Static)
                    group method by method.Name
                    into groups
                    select (Name: groups.Key, MethodInfo: groups.First()))
                {
                    // Check whether the current method should be translated with the original name
                    // or with the lowercase version. This is needed because all C# methods are exposed
                    // with the upper camel case format, while HLSL intrinsics use multiple different formats.
                    string hlslName = method.MethodInfo.GetCustomAttribute<PreserveMemberNameAttribute>() != null
                        ? method.Name
                        : method.Name.ToLowerInvariant();

                    knownMethods.Add($"{typeof(Hlsl).FullName}{Type.Delimiter}{method.Name}", hlslName);
                }

                // Programmatically load mappings for the vector types
                foreach (var item in
                    from type in HlslKnownTypes.HlslMappedVectorTypes
                    from property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    select (Type: type, Property: property))
                {
                    knownMethods.Add($"{item.Type.FullName}{Type.Delimiter}{item.Property.Name}", $".{item.Property.Name.ToLowerInvariant()}");
                }

                return _KnownMethods = knownMethods;
            }
        }

        /// <summary>
        /// Tries to get the mapped HLSL-compatible mthod name for the input symbols
        /// </summary>
        /// <param name="containingMemberSymbol">The containing member symbol for the method to check</param>
        /// <param name="memberSymbol">The symbol for the method to check</param>
        /// <param name="mapped">The mapped name, if one is found</param>
        /// <returns>The HLSL-compatible method name that can be used in an HLSL shader</returns>
        [Pure]
        public static bool TryGetMappedName(ISymbol containingMemberSymbol, ISymbol memberSymbol, [NotNullWhen(true)] out string? mapped)
        {
            string fullTypeName = containingMemberSymbol.IsStatic ? containingMemberSymbol.ToString() : memberSymbol.ContainingType.ToString();

            // Check if the target method is a known method
            return KnownMethods.TryGetValue($"{fullTypeName}{Type.Delimiter}{memberSymbol.Name}", out mapped);
        }
    }
}
