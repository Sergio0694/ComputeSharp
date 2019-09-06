using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ComputeSharp.Graphics.Extensions
{
    /// <summary>
    /// A <see langword="class"/> that provides extension methods for the <see cref="Type"/> type
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// The mapping of supported known types to display string
        /// </summary>
        private static readonly IReadOnlyDictionary<Type, string> KnownTypes = new Dictionary<Type, string>
        {
            [typeof(bool)] = "bool",
            [typeof(int)] = "int",
            [typeof(uint)] = "uint",
            [typeof(float)] = "float",
            [typeof(double)] = "double"
        };

        /// <summary>
        /// Gets a friendly display <see langword="string"/> for the input type
        /// </summary>
        /// <param name="type">The input type to analyze</param>
        [Pure]
        public static string ToFriendlyString(this Type type)
        {
            // Array types of arbitrary rank
            if (type.IsArray)
            {
                int rank = type.GetArrayRank();
                return $"{type.GetElementType().ToFriendlyString()}[{new string(Enumerable.Repeat(',', rank - 1).ToArray())}]";
            }

            // Non generic types
            string name;
            if (type.GetGenericArguments().Length == 0) return KnownTypes.TryGetValue(type, out name) ? name : type.Name;

            // Generic types
            Type[] genericArguments = type.GetGenericArguments();
            string typeDefeninition = KnownTypes.TryGetValue(type, out name) ? name : type.Name;
            string polishedName = typeDefeninition.Substring(0, typeDefeninition.IndexOf("`"));
            return $"{polishedName}<{string.Join(",", genericArguments.Select(ToFriendlyString))}>";
        }
    }
}
