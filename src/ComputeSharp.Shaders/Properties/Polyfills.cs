#if NETSTANDARD2_0

using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Reserved to be used by the compiler for tracking metadata.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit
    {
    }
}

namespace System.Reflection
{
    /// <summary>
    /// Extensions for the <see cref="MethodInfo"/> type.
    /// </summary>
    internal static class MethodInfoExtensions
    {
        /// <summary>
        /// Creates a delegate of type T from a given method.
        /// </summary>
        /// <typeparam name="T">The type of delegate to create.</typeparam>
        /// <param name="method">The input <see cref="MethodInfo"/>.</param>
        /// <returns>The delegate for this method.</returns>
        public static T CreateDelegate<T>(this MethodInfo method)
            where T : Delegate
        {
            return (T)method.CreateDelegate(typeof(T));
        }
    }
}

#endif
