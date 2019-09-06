using ComputeSharp.Graphics.Extensions;
using System;
using System.Collections.Generic;

namespace ComputeSharp.Shaders.Renderer.Models.Functions
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a shader function
    /// </summary>
    internal sealed class FunctionInfo
    {
        /// <summary>
        /// Gets the return type of the current function in the C# source
        /// </summary>
        public string FunctionCsharpReturnType { get; }

        /// <summary>
        /// Gets the fullname of the current function in the C# source
        /// </summary>
        public string FunctionFullName { get; }

        /// <summary>
        /// Gets the parameters of the function in the C# source
        /// </summary>
        public string FunctionCsharpParameters { get; }

        /// <summary>
        /// Gets the return type of the current function
        /// </summary>
        public string ReturnType { get; }

        /// <summary>
        /// Gets the name of the current function
        /// </summary>
        public string FunctionName { get; }

        /// <summary>
        /// Gets the list of parameters for the current function
        /// </summary>
        public IReadOnlyList<ParameterInfo> ParametersList { get; }

        /// <summary>
        /// Gets the body of the current function
        /// </summary>
        public string FunctionBody { get; }

        /// <summary>
        /// Creates a new <see cref="FunctionInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="functionCsharpType">The concrete return type for the function</param>
        /// <param name="functionFullname">The fullname of the function in the C# source</param>
        /// <param name="functionCsharpParameters">The parameters of the function in the C# source</param>
        /// <param name="returnType">The return type of the current function</param>
        /// <param name="functionName">The name of the current function</param>
        /// <param name="parameters">The function parameters, if any</param>
        /// <param name="functionBody">The current function</param>
        public FunctionInfo(
            Type functionCsharpType,
            string functionFullname,
            string functionCsharpParameters,
            string returnType,
            string functionName,
            IReadOnlyList<ParameterInfo> parameters,
            string functionBody)
        {
            FunctionCsharpReturnType = functionCsharpType.ToFriendlyString();
            FunctionFullName = functionFullname;
            FunctionCsharpParameters = functionCsharpParameters;
            ReturnType = returnType;
            FunctionName = functionName;
            ParametersList = parameters;
            FunctionBody = functionBody;
        }
    }
}
