using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;

namespace System.Reflection.Emit
{
    /// <summary>
    /// A <see langword="class"/> that can be used to easily (lol) create dynamic methods
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Delegate"/> that will be used to wrap the new methods</typeparam>
    internal static class DynamicMethod<T> where T : Delegate
    {
        /// <summary>
        /// The owner type for new dynamic methods
        /// </summary>
        private static readonly Type OwnerType = typeof(DynamicMethod<T>);

        /// <summary>
        /// The return type of the <typeparamref name="T"/> <see langword="delegate"/>
        /// </summary>
        private static readonly Type ReturnType;

        /// <summary>
        /// The types of the arguments of the <typeparamref name="T"/> <see langword="delegate"/>
        /// </summary>
        private static readonly Type[] ParameterTypes;

        /// <summary>
        /// Loads all the necessary <see cref="Type"/> info for the current <typeparamref name="T"/> parameter
        /// </summary>
        static DynamicMethod()
        {
            MethodInfo method = typeof(T).GetMethod("Invoke");
            ReturnType = method.ReturnType;
            ParameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
        }

        /// <summary>
        /// Local counter for dynamic methods of type <typeparamref name="T"/> ever generated
        /// </summary>
        private static int _Count;

        /// <summary>
        /// Creates a new <typeparamref name="T"/> <see langword="delegate"/> for the target owner
        /// </summary>
        /// <param name="builder">An <see cref="Action"/> that builds the IL bytecode for the new method</param>
        /// <returns>A new dynamic method wrapped as a <typeparamref name="T"/> <see langword="delegate"/></returns>
        [Pure]
        public static T New(Action<ILGenerator> builder)
        {
            // Create a new dynamic method with a unique id
            string id = $"__IL__{typeof(T).Name}_{Interlocked.Increment(ref _Count)}";
            DynamicMethod method = new DynamicMethod(id, ReturnType, ParameterTypes, OwnerType);

            // Build the IL bytecode
            ILGenerator il = method.GetILGenerator();
            builder(il);

            // Build and delegate instance
            return (T)method.CreateDelegate(typeof(T));
        }
    }
}
