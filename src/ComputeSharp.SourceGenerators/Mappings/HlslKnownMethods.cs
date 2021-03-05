using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Reflection;
using ComputeSharp.Core.Intrinsics.Attributes;

namespace ComputeSharp.SourceGenerators.Mappings
{
    /// <summary>
    /// A <see langword="class"/> that contains and maps known HLSL method names to common .NET methods.
    /// </summary>
    internal static class HlslKnownMethods
    {
        /// <summary>
        /// The mapping of supported known methods to HLSL names.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> KnownMethods = BuildKnownMethodsMap();

        /// <summary>
        /// Builds the mapping of supported known methods to HLSL names.
        /// </summary>
        [Pure]
        private static IReadOnlyDictionary<string, string> BuildKnownMethodsMap()
        {
            Dictionary<string, string> knownMethods = new()
            {
                [$"{typeof(Math).FullName}.{nameof(Math.Abs)}"] = "abs",
                [$"{typeof(Math).FullName}.{nameof(Math.Max)}"] = "max",
                [$"{typeof(Math).FullName}.{nameof(Math.Min)}"] = "min",
                [$"{typeof(Math).FullName}.{nameof(Math.Pow)}"] = "pow",
                [$"{typeof(Math).FullName}.{nameof(Math.Sin)}"] = "sin",
                [$"{typeof(Math).FullName}.{nameof(Math.Sinh)}"] = "sinh",
                [$"{typeof(Math).FullName}.{nameof(Math.Asin)}"] = "asin",
                [$"{typeof(Math).FullName}.{nameof(Math.Cos)}"] = "cos",
                [$"{typeof(Math).FullName}.{nameof(Math.Cosh)}"] = "cosh",
                [$"{typeof(Math).FullName}.{nameof(Math.Acos)}"] = "acos",
                [$"{typeof(Math).FullName}.{nameof(Math.Tan)}"] = "tan",
                [$"{typeof(Math).FullName}.{nameof(Math.Tanh)}"] = "tanh",
                [$"{typeof(Math).FullName}.{nameof(Math.Atan)}"] = "atan",
                [$"{typeof(Math).FullName}.{nameof(Math.Atan2)}"] = "atan2",
                [$"{typeof(Math).FullName}.{nameof(Math.Ceiling)}"] = "ceil",
                [$"{typeof(Math).FullName}.{nameof(Math.Floor)}"] = "floor",
                [$"{typeof(Math).FullName}.Clamp"] = "clamp",
                [$"{typeof(Math).FullName}.{nameof(Math.Exp)}"] = "exp",
                [$"{typeof(Math).FullName}.{nameof(Math.Log)}"] = "log",
                [$"{typeof(Math).FullName}.{nameof(Math.Log10)}"] = "log10",
                [$"{typeof(Math).FullName}.{nameof(Math.Round)}"] = "round",
                [$"{typeof(Math).FullName}.{nameof(Math.Sqrt)}"] = "sqrt",
                [$"{typeof(Math).FullName}.{nameof(Math.Sign)}"] = "sign",
                [$"{typeof(Math).FullName}.{nameof(Math.Truncate)}"] = "trunc",

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

                [$"{typeof(float).FullName}.IsFinite"] = "isfinite",
                [$"{typeof(float).FullName}.{nameof(float.IsInfinity)}"] = "isinf",
                [$"{typeof(float).FullName}.{nameof(float.IsNaN)}"] = "isnan",

                [$"{typeof(double).FullName}.IsFinite"] = "isfinite",
                [$"{typeof(double).FullName}.{nameof(double.IsInfinity)}"] = "isinf",
                [$"{typeof(double).FullName}.{nameof(double.IsNaN)}"] = "isnan",

                [$"{typeof(Vector2).FullName}.{nameof(Vector2.Dot)}"] = "dot",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.Lerp)}"] = "lerp",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.Transform)}"] = "mul",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.TransformNormal)}"] = "mul",
                [$"{typeof(Vector2).FullName}.{nameof(Vector2.Normalize)}"] = "normalize",

                [$"{typeof(Vector3).FullName}.{nameof(Vector3.Cross)}"] = "cross",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.Dot)}"] = "dot",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.Lerp)}"] = "lerp",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.Transform)}"] = "mul",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.TransformNormal)}"] = "mul",
                [$"{typeof(Vector3).FullName}.{nameof(Vector3.Normalize)}"] = "normalize",

                [$"{typeof(Vector4).FullName}.{nameof(Vector4.Lerp)}"] = "lerp",
                [$"{typeof(Vector4).FullName}.{nameof(Vector4.Transform)}"] = "mul",
                [$"{typeof(Vector4).FullName}.{nameof(Vector4.Normalize)}"] = "normalize"
            };

            // Programmatically load mappings from the Hlsl class as well
            foreach (var method in
                from method in typeof(Hlsl).GetMethods(BindingFlags.Public | BindingFlags.Static)
                group method by method.Name
                into groups
                select groups.First())
            {
                // Check whether the current method should be translated with the original name
                // or with the lowercase version. This is needed because all C# methods are exposed
                // with the upper camel case format, while HLSL intrinsics use multiple different formats.
                string hlslName = method.GetCustomAttribute<HlslIntrinsicNameAttribute>()?.Name ?? method.Name;

                knownMethods.Add($"{typeof(Hlsl).FullName}{Type.Delimiter}{method.Name}", hlslName);
            }

            return knownMethods;
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
