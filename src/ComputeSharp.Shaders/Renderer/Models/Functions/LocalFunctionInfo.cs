using System;

namespace ComputeSharp.Shaders.Renderer.Models.Functions
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a local shader function
    /// </summary>
    internal sealed class LocalFunctionInfo
    {
        /// <summary>
        /// Gets the prototype for the current function
        /// </summary>
        public string FunctionPrototype => FunctionDeclaration.Split(Environment.NewLine)[0];

        /// <summary>
        /// Gets the declaration of the current function
        /// </summary>
        public string FunctionDeclaration { get; }

        /// <summary>
        /// Creates a new <see cref="LocalFunctionInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="functionDeclaration">The declaration of the current function</param>
        public LocalFunctionInfo(string functionDeclaration)
        {
            FunctionDeclaration = functionDeclaration;
        }
    }
}
