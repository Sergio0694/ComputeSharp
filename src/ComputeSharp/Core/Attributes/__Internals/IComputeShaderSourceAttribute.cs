using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using ComputeSharp.Exceptions;

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
        /// <param name="fields">The mapped collection of shader fields.</param>
        /// <param name="forwardDeclarations">The forward declarations for static methods.</param>
        /// <param name="executeMethod">The source code for the <see cref="IComputeShader.Execute"/> method.</param>
        /// <param name="methods">The collection of processed methods.</param>
        /// <param name="defines">The collection of discovered defines.</param>
        /// <param name="staticFields">The collection of discovered static fields.</param>
        /// <param name="sharedBuffers">The collection of group shared buffers.</param>
        public IComputeShaderSourceAttribute(
            Type shaderType,
            string[] types,
            object[] fields,
            string[] forwardDeclarations,
            string executeMethod,
            string[] methods,
            object[] defines,
            object[] staticFields,
            object[] sharedBuffers)
        {
            ShaderType = shaderType;
            Types = types;
            Fields = fields.Cast<string[]>().ToDictionary(static arg => arg[0], static arg => (arg[1], arg[2]));
            ForwardDeclarations = forwardDeclarations;
            ExecuteMethod = executeMethod;
            Methods = methods;
            Defines = defines.Cast<string[]>().ToDictionary(static c => c[0], static c => c[1]);
            StaticFields = staticFields.Cast<string?[]>().ToDictionary(static arg => arg[0]!, static arg => (arg[1]!, arg[2]));
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
        /// The forward declarations for static methods.
        /// </summary>
        internal IReadOnlyCollection<string> ForwardDeclarations { get; }

        /// <summary>
        /// Gets the source code for the <see cref="IComputeShader.Execute"/> method.
        /// </summary>
        internal string ExecuteMethod { get; }

        /// <summary>
        /// Gets the collection of processed methods.
        /// </summary>
        internal IReadOnlyCollection<string> Methods { get; }

        /// <summary>
        /// Gets the collection of discovered define declarations.
        /// </summary>
        internal IReadOnlyDictionary<string, string> Defines { get; }

        /// <summary>
        /// Gets the collection of discovered static fields.
        /// </summary>
        internal IReadOnlyDictionary<string, (string TypeDeclaration, string? Assignment)> StaticFields { get; }

        /// <summary>
        /// Gets the collection of discovered shared buffers.
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
            IEnumerable<IComputeShaderSourceAttribute> attributes = typeof(T).Assembly.GetCustomAttributes<IComputeShaderSourceAttribute>();
            IComputeShaderSourceAttribute? attribute = attributes.SingleOrDefault(static attribute => attribute.ShaderType == typeof(T));

            if (attribute is null)
            {
                MissingShaderMetadataException.Throw<T>();
            }

            return attribute!;
        }
    }
}