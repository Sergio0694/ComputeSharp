using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace ComputeSharp.__Internals
{
    /// <summary>
    /// An attribute that contains info on a processed compute shader that can be executed.
    /// </summary>
    /// <remarks>This attribute is not meant to be directly used by applications using ComputeSharp.</remarks>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This attribute is not intended to be used directly by user code")]
    public sealed class IComputeShaderSourceAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="IComputeShaderSourceAttribute"/> instance with the specified parameters.
        /// </summary>
        /// <param name="shaderType">The type of the current shader.</param>
        /// <param name="types">The collection of custom types.</param>
        /// <param name="args">The mapped collection of shader fields.</param>
        /// <param name="executeMethod">The source code for the <see cref="IComputeShader.Execute"/> method.</param>
        /// <param name="methods">The collection of processed methods.</param>
        /// <param name="constants">The collection of discovered constants.</param>
        /// <param name="sharedBuffers">The collection of group shared buffers.</param>
        public IComputeShaderSourceAttribute(
            Type shaderType,
            string[] types,
            object[] args,
            string executeMethod,
            string[] methods,
            object[] constants,
            object[] sharedBuffers)
        {
            ShaderType = shaderType;
            Types = types;
            Fields = args.Cast<string[]>().ToDictionary(static arg => arg[0], static arg => (arg[1], arg[2]));
            ExecuteMethod = executeMethod;
            Methods = methods;
            Constants = constants.Cast<string[]>().ToDictionary(static c => c[0], static c => c[1]);
            SharedBuffers = sharedBuffers.Cast<object[]>().ToDictionary(static t => (string)t[0], static t => ((string)t[1], (int?)t[2]));
        }

        /// <summary>
        /// Gets the type of the current shader.
        /// </summary>
        internal Type ShaderType { get; }

        /// <summary>
        /// Gets the collection of processed custom types.
        /// </summary>
        internal IReadOnlyCollection<string> Types { get; }

        /// <summary>
        /// Gets the mapping of field names.
        /// </summary>
        internal IReadOnlyDictionary<string, (string Name, string Type)> Fields { get; }

        /// <summary>
        /// Gets the source code for the <see cref="IComputeShader.Execute"/> method.
        /// </summary>
        internal string ExecuteMethod { get; }

        /// <summary>
        /// Gets the collection of processed methods.
        /// </summary>
        internal IReadOnlyCollection<string> Methods { get; }

        /// <summary>
        /// Gets the collection of discovered constants.
        /// </summary>
        internal IReadOnlyDictionary<string, string> Constants { get; }

        /// <summary>
        /// Gets the collection of discovered constants.
        /// </summary>
        internal IReadOnlyDictionary<string, (string Type, int? Count)> SharedBuffers { get; }

        /// <summary>
        /// Gets the associated <see cref="IComputeShaderSourceAttribute"/> instance for a specified type.
        /// </summary>
        /// <typeparam name="T">The shader type to get the attribute for.</typeparam>
        /// <returns>The associated <see cref="IComputeShaderSourceAttribute"/> instance for type <typeparamref name="T"/>.</returns>
        [Pure]
        internal static IComputeShaderSourceAttribute GetForType<T>()
        {
            Type shaderType = typeof(T);

            return shaderType.Assembly.GetCustomAttributes<IComputeShaderSourceAttribute>().Single(attribute => attribute.ShaderType == shaderType);
        }
    }
}