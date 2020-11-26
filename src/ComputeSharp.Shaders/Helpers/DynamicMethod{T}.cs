using System.Diagnostics.Contracts;
using System.Linq;

namespace System.Reflection.Emit
{
    /// <summary>
    /// A <see langword="class"/> that can be used to easily create dynamic methods.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="Delegate"/> that will be used to wrap the new methods.</typeparam>
    internal static class DynamicMethod<T>
        where T : Delegate
    {
        /// <summary>
        /// The return type of the <typeparamref name="T"/> <see langword="delegate"/>.
        /// </summary>
        private static readonly Type ReturnType;

        /// <summary>
        /// The types of the arguments of the <typeparamref name="T"/> <see langword="delegate"/>.
        /// </summary>
        private static readonly Type[] ParameterTypes;

        /// <summary>
        /// Loads all the necessary <see cref="Type"/> info for the current <typeparamref name="T"/> parameter.
        /// </summary>
        static DynamicMethod()
        {
            MethodInfo method = typeof(T).GetMethod("Invoke")!;

            ReturnType = method.ReturnType;
            ParameterTypes = new[] { typeof(object) }.Concat(method.GetParameters().Select(static p => p.ParameterType)).ToArray();
        }

        /// <summary>
        /// Creates a new <typeparamref name="T"/> <see langword="delegate"/> for the target owner. For performance reasons,
        /// the underlying dynamic method will be created with an extra <see cref="object"/> parameter, and then bound to
        /// a <see langword="null"/> instance. This is to emulate the behavior of instance methods, which unlike static ones
        /// don't need to go through the shuffle thunk when the delegate is invoked. This would be needed to redirect the
        /// invocation to a stub that performs parameters swapping, as the Invoke method is an instance one with the parameters
        /// starting from offset 1, while the target static method would expect them to start from offset 0. This solution
        /// allows the delegate created for the dynamic method to avoid having a shuffle thunk at all, and with it only having
        /// to replace the hidden instance argument when being invoked, resulting in much better performance. Because of this,
        /// it is expected that <paramref name="builder"/> will emit the bytecode assuming that there is an extra unused argument
        /// at offset 0. As a result of this, all argument load and store opcodes will need to be shifted by one position forwards.
        /// </summary>
        /// <param name="builder">An <see cref="Action"/> that builds the IL bytecode for the new method.</param>
        /// <returns>A new dynamic method wrapped as a <typeparamref name="T"/> <see langword="delegate"/>.</returns>
        [Pure]
        public static T New(Action<ILGenerator> builder)
        {
            DynamicMethod method = new(string.Empty, ReturnType, ParameterTypes, true);

            builder(method.GetILGenerator());

            return method.CreateDelegate<T>(null);
        }
    }
}
