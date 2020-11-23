using System;
using System.ComponentModel;

namespace ComputeSharp
{
    /// <summary>
    /// A shader that indicates that a target shader type should get an automatic constructor for all fields.
    /// This attribute is not meant to be directly used by applications using ComputeSharp.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This attribute is meant to be used from the source generator only", true)]
    public sealed class IComputeShaderSourceAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="IComputeShaderSourceAttribute"/> instance with the specified parameters.
        /// </summary>
        /// <param name="shaderTypeName">The fully qualified name of the shader type.</param>
        /// <param name="methodName">The name of the current method.</param>
        /// <param name="source">The source code of the target method.</param>
        public IComputeShaderSourceAttribute(string shaderTypeName, string methodName, string source)
        {
            ShaderTypeName = shaderTypeName;
            MethodName = methodName;
            Source = source;
        }

        /// <summary>
        /// Gets the fully qualified name of the shader type.
        /// </summary>
        public string ShaderTypeName { get; }

        /// <summary>
        /// Gets the name of the current method.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Gets the source code of the target method.
        /// </summary>
        public string Source { get; }
    }
}