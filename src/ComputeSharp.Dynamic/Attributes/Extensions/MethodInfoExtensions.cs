using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ComputeSharp.Extensions;

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
    public static string GetFullyQualifiedName(this MethodInfo method)
    {
        string parameters = string.Join(", ", method.GetParameters().Select(static p => p.ParameterType.FullName));

        // Fixup the method name for static local functions in global statements. This only needs to
        // be done when the method is in a type with fully qualified name "Program", as that's the only
        // case where it might happen. If the declaring type has a name, it can't be that for sure.
        (string declaringType, string methodName) = (method.DeclaringType!.FullName!, method.Name) switch
        {
            ("Program", var name) => ("Program", Regex.Replace(name, @"<<Main>\$>g__(\w+)\|\d+_\d+", static m => m.Groups[1].Value)),
            (var type, var name) => (type, name)
        };

        if (method.IsGenericMethod)
        {
            string arguments = string.Join(", ", method.GetGenericArguments().Select(static t => t.FullName));

            return $"{declaringType}.{methodName}<{arguments}>({parameters})";
        }

        return $"{declaringType}.{methodName}({parameters})";
    }
}