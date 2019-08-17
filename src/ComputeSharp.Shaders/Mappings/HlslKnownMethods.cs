using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.Shaders.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL method names to common .NET method
    /// </summary>
    internal static class HlslKnownMethods
    {
        /// <summary>
        /// The mapping of supported known methods to HLSL methods
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> KnownMethods = new Dictionary<string, string>
        {
            // Math
            ["System.Math.Cos"] = "cos",
            ["System.Math.Max"] = "max",
            ["System.Math.Pow"] = "pow",
            ["System.Math.Sin"] = "sin",
            ["System.Math.PI"] = "3.1415926535897931",

            // MathF
            ["System.MathF.Cos"] = "cos",
            ["System.MathF.Max"] = "max",
            ["System.MathF.Pow"] = "pow",
            ["System.MathF.Sin"] = "sin",
            ["System.MathF.PI"] = "3.14159274f",

            // ThreadIds
            ["ComputeSharp.ThreadIds.X"] = ".x",
            ["ComputeSharp.ThreadIds.Y"] = ".y",
            ["ComputeSharp.ThreadIds.Z"] = ".z",

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
            ["System.Numerics.Vector4.One"] = "float4(1.0f, 1.0f, 1.0f, 1.0f)"
        };

        /// <summary>
        /// Checks whether or not a specified method is a known HLSL method
        /// </summary>
        /// <param name="containingMemberSymbol">The containing member symbol for the method to check</param>
        /// <param name="memberSymbol">The symbol for the method to check</param>
        /// <returns>A <see langword="bool"/> indicating whether the input method is in fact an HLSL known method</returns>
        [Pure]
        public static bool IsKnownMethod(ISymbol containingMemberSymbol, ISymbol memberSymbol)
        {
            string fullTypeName = containingMemberSymbol.IsStatic ? containingMemberSymbol.ToString() : memberSymbol.ContainingType.ToString();
            return KnownMethods.ContainsKey(fullTypeName + Type.Delimiter + memberSymbol.Name);
        }

        /// <summary>
        /// Tries to get the mapped HLSL-compatible mthod name for the input symbols
        /// </summary>
        /// <param name="containingMemberSymbol">The containing member symbol for the method to check</param>
        /// <param name="memberSymbol">The symbol for the method to check</param>
        /// <returns>The HLSL-compatible method name that can be used in an HLSL shader</returns>
        [Pure]
        public static string? TryGetMappedName(ISymbol containingMemberSymbol, ISymbol memberSymbol)
        {
            string fullTypeName = containingMemberSymbol.IsStatic ? containingMemberSymbol.ToString() : memberSymbol.ContainingType.ToString();

            // Check if the target method is a known method
            if (KnownMethods.TryGetValue($"{fullTypeName}{Type.Delimiter}{memberSymbol.Name}", out string mapped))
            {
                // Return the static method if possible, otherwise access the parent instance
                if (memberSymbol.IsStatic) return mapped;
                return $"{containingMemberSymbol.Name}{mapped}";
            }

            if (memberSymbol.IsStatic) return $"{containingMemberSymbol.Name}::{memberSymbol.Name}";

            return null;
        }
    }
}
