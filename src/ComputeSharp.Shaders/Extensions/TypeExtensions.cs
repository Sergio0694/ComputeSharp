using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
    /// <summary>
    /// A <see langword="class"/> that provides extension methods for the <see cref="Type"/> type
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Gets whether or not the input <see cref="Type"/> represents a <see cref="Delegate"/>
        /// </summary>
        /// <param name="type">The input type to analyze</param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDelegate(this Type type) => type.IsClass &&
                                                         type.IsSubclassOf(typeof(Delegate));

        /// <summary>
        /// Gets whether or not the input <see cref="Type"/> represents a <see cref="Delegate"/> with no captured variables
        /// </summary>
        /// <param name="type">The input type to analyze</param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStatelessDelegateContainer(this Type type) => type.GetFields(BindingFlags.Static | BindingFlags.Public).Length > 0;

        /// <summary>
        /// Tries to find an ancestor member starting from the input <see cref="Type"/>
        /// </summary>
        /// <param name="type">The input type to start the search from</param>
        /// <param name="name">The name of the member to retrieve</param>
        /// <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"></see> that specify how the search is conducted</param>
        /// <returns><see langword="true"/> when a member with the given name is found, <see langword="false"/> otherwise</returns>
        public static bool TryFindAncestorMember(this Type type, string name, BindingFlags bindingAttr, [NotNullWhen(true)] out (Type DeclaringType, MemberInfo MemberInfo)? result)
        {
            while ((type = type.DeclaringType) != null)
            {
                MemberInfo[] memberInfos = type.GetMember(name, bindingAttr);
                if (memberInfos.Length > 0)
                {
                    result = (type, memberInfos[0]);
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}
