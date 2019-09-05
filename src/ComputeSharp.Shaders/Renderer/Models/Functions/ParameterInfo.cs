using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ComputeSharp.Shaders.Renderer.Models.Functions
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a function parameter
    /// </summary>
    internal sealed class ParameterInfo
    {
        /// <summary>
        /// Gets the modifier to use for the current parameter
        /// </summary>
        public string ParameterModifier { get; }

        /// <summary>
        /// Gets the type of the current parameter
        /// </summary>
        public string ParameterType { get; }

        /// <summary>
        /// Gets the name to use for the current parameter
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// Gets whether or not the current parameter is the last one for the parent function
        /// </summary>
        public bool IsLastParameter { get; }

        /// <summary>
        /// Creates a new <see cref="ParameterInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="parameterModifiers">The modifiers used in the current parameter</param>
        /// <param name="parameterType">The type of the current parameter</param>
        /// <param name="parameterName">The name of the current parameter</param>
        /// <param name="last">Indicates whether or not the current parameter is the last one</param>
        public ParameterInfo(IReadOnlyList<SyntaxToken> parameterModifiers, string parameterType, string parameterName, bool last)
        {
            if (parameterModifiers.Count == 0) ParameterModifier = "in";
            else if (parameterModifiers.First().IsKind(SyntaxKind.OutKeyword)) ParameterModifier = "out";
            else if (parameterModifiers.Any(m => m.IsKind(SyntaxKind.RefKeyword)) && !parameterModifiers.Any(m => m.IsKind(SyntaxKind.ReadOnlyKeyword))) ParameterModifier = "inout";
            else ParameterModifier = "in";

            ParameterType = parameterType;
            ParameterName = parameterName;
            IsLastParameter = last;
        }
    }
}
