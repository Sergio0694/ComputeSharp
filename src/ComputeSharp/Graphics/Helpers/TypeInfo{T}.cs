using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ComputeSharp.Graphics.Helpers;

/// <summary>
/// A class providing info on specific types.
/// </summary>
/// <typeparam name="T">The input type to extract info for.</typeparam>
internal static class TypeInfo<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)] T>
    where T : unmanaged
{
    /// <summary>
    /// Whether or not type <typeparamref name="T"/> is <see cref="double"/> or contains a <see cref="double"/> field.
    /// </summary>
    public static readonly bool IsDoubleOrContainsDoubles = ChecksIsDoubleOrContainsDoubles(typeof(T));

    /// <summary>
    /// Checks the value for <see cref="IsDoubleOrContainsDoubles"/> for type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="type">The current type to check.</param>
    /// <returns>Whether or not <paramref name="type"/> is <see cref="double"/> or contains a <see cref="double"/> field.</returns>
    [UnconditionalSuppressMessage(
        "ReflectionAnalysis",
        "IL2026:RequiresUnreferencedCode",
        Justification = "The types of all fields (recursively) are already guaranteed to be preserved given it's all value types (plus T is already annotated).")]
    private static bool ChecksIsDoubleOrContainsDoubles([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)] Type type)
    {
        if (type == typeof(double)) return true;

        if (type.IsPrimitive) return false;

        foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (ChecksIsDoubleOrContainsDoubles(fieldInfo.FieldType))
            {
                return true;
            }
        }

        return false;
    }
}
