using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace ComputeSharp
{
    /// <summary>
    /// An attribute that indicates that a target shader type should get an automatic constructor for all fields.
    /// This attribute is not meant to be directly used by applications using ComputeSharp.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This attribute is meant to be used from the source generator only")]
    public sealed class IComputeShaderSourceAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="IComputeShaderSourceAttribute"/> instance with the specified parameters.
        /// </summary>
        /// <param name="shaderTypeName">The fully qualified name of the shader type.</param>
        /// <param name="args">The mapped collection of shader fields.</param>
        /// <param name="methods">The mapped collection of shader methods.</param>
        /// <param name="types">The collection of custom types.</param>
        public IComputeShaderSourceAttribute(string shaderTypeName, object[] args, object[] methods, string[] types)
        {
            ShaderTypeName = shaderTypeName;
            Fields = args.Cast<string[]>().ToDictionary(static arg => arg[0], static arg => arg[1]);
            Methods = methods.Cast<string[]>().ToDictionary(static method => method[0], static method => method[1]);
            Types = types;
        }

        /// <summary>
        /// Gets the fully qualified name of the shader type.
        /// </summary>
        internal string ShaderTypeName { get; }

        /// <summary>
        /// Gets the mapping of field names.
        /// </summary>
        internal IReadOnlyDictionary<string, string> Fields { get; }

        /// <summary>
        /// Gets the mapping of methods and their source code.
        /// </summary>
        internal IReadOnlyDictionary<string, string> Methods { get; }

        /// <summary>
        /// Gets the collection of processed custom types.
        /// </summary>
        internal IReadOnlyCollection<string> Types { get; }

        /// <summary>
        /// Gets the associated <see cref="IComputeShaderSourceAttribute"/> instance for a specified type.
        /// </summary>
        /// <typeparam name="T">The shader type to get the attribute for.</typeparam>
        /// <returns>The associated <see cref="IComputeShaderSourceAttribute"/> instance for type <typeparamref name="T"/>.</returns>
        [Pure]
        internal static IComputeShaderSourceAttribute GetForType<T>()
        {
            // Resolve the fully qualified name of the current type, without generic parameters.
            // We can't use Type.FullName, as when T is a closed generic it includes the assembly too.
            string fullname = typeof(T) switch
            {
                { IsGenericType: true } => typeof(T).GetGenericTypeDefinition().ToString().Split('[')[0],
                _ => typeof(T).ToString()
            };

            return (
                from attribute in typeof(T).Assembly.GetCustomAttributes<IComputeShaderSourceAttribute>()
                where attribute.ShaderTypeName == fullname
                select attribute).Single();
        }
    }
}