using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using ComputeSharp.Core.Intrinsics.Attributes;

namespace ComputeSharp.Shaders.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL method names to common .NET methods.
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
                    ["System.Math.Abs"] = "abs",
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

                    // MathF
                    ["System.MathF.Abs"] = "abs",
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

                    // Float
                    ["System.Single.IsFinite"] = "isfinite",
                    ["System.Single.IsInfinity"] = "isinf",
                    ["System.Single.IsNaN"] = "isnan",

                    // Double
                    ["System.Double.IsFinite"] = "isfinite",
                    ["System.Double.IsInfinity"] = "isinf",
                    ["System.Double.IsNaN"] = "isnan",

                    // Vector2
                    ["System.Numerics.Vector2.Dot"] = "dot",
                    ["System.Numerics.Vector2.Lerp"] = "lerp",
                    ["System.Numerics.Vector2.Transform"] = "mul",
                    ["System.Numerics.Vector2.TransformNormal"] = "mul",
                    ["System.Numerics.Vector2.Normalize"] = "normalize",

                    // Vector3
                    ["System.Numerics.Vector3.Cross"] = "cross",
                    ["System.Numerics.Vector3.Dot"] = "dot",
                    ["System.Numerics.Vector3.Lerp"] = "lerp",
                    ["System.Numerics.Vector3.Transform"] = "mul",
                    ["System.Numerics.Vector3.TransformNormal"] = "mul",
                    ["System.Numerics.Vector3.Normalize"] = "normalize",

                    // Vector4
                    ["System.Numerics.Vector4.Lerp"] = "lerp",
                    ["System.Numerics.Vector4.Transform"] = "mul",
                    ["System.Numerics.Vector4.Normalize"] = "normalize"
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

                return _KnownMethods = knownMethods;
            }
        }

        /// <summary>
        /// Tries to get the mapped HLSL-compatible method name for the input method name.
        /// </summary>
        /// <param name="name">The input fully qualified method name.</param>
        /// <param name="mapped">The mapped name, if one is found.</param>
        /// <returns>The HLSL-compatible member name that can be used in an HLSL shader.</returns>
        [Pure]
        public static bool TryGetMappedName(string name, out string? mapped)
        {
            return KnownMethods.TryGetValue(name, out mapped);
        }
    }
}
