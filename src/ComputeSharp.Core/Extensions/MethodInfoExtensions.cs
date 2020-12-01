using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace ComputeSharp.Core.Extensions
{
    /// <summary>
    /// A <see langword="class"/> that provides extension methods for the <see cref="MethodInfo"/> type.
    /// </summary>
    internal static class MethodInfoExtensions
    {
        /// <summary>
        /// Gets a fully qualified name for the input method, including parameters.
        /// </summary>
        /// <param name="method">The input <see cref="MethodInfo"/> instance.</param>
        /// <returns>A fully qualified name for <paramref name="method"/>, including parameters.</returns>
        [Pure]
        public static string GetFullName(this MethodInfo method)
        {
            var parameters = string.Join(", ", method.GetParameters().Select(static p => p.ParameterType.FullName));

            if (method.IsGenericMethod)
            {
                var arguments = string.Join(", ", method.GetGenericArguments().Select(static t => t.FullName));

                return $"{method.DeclaringType!.FullName}.{method.Name}<{arguments}>({parameters})";
            }

            return $"{method.DeclaringType!.FullName}.{method.Name}({parameters})";
        }
    }
}
