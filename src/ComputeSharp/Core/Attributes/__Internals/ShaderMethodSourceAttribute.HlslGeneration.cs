using System;
using System.Collections.Generic;

namespace ComputeSharp.__Internals
{
    /// <inheritdoc/>
    public sealed partial class ShaderMethodSourceAttribute : Attribute
    {
        /// <summary>
        /// Appends the mapped source code for the current method.
        /// </summary>
        /// <param name="builder">The target <see cref="ArrayPoolStringBuilder"/> instance to write to.</param>
        /// <param name="mapping">The mapping of already discovered constant names.</param>
        public void AppendConstants(ref ArrayPoolStringBuilder builder, HashSet<string> mapping)
        {
            foreach (KeyValuePair<string, string> constant in this.constants)
            {
                if (mapping.Add(constant.Key))
                {
                    builder.Append($"#define {constant.Key} {constant.Value}\n");
                }
            }
        }

        /// <summary>
        /// Appends the discovered types for the current method.
        /// </summary>
        /// <param name="builder">The target <see cref="ArrayPoolStringBuilder"/> instance to write to.</param>
        /// <param name="mapping">The mapping of already discovered type names.</param>
        public void AppendTypes(ref ArrayPoolStringBuilder builder, HashSet<string> mapping)
        {
            foreach (KeyValuePair<string, string> type in this.types)
            {
                if (mapping.Add(type.Key))
                {
                    builder.Append(type.Value);
                }
            }
        }

        /// <summary>
        /// Appends the mapped source code for the current method.
        /// </summary>
        /// <param name="builder">The target <see cref="ArrayPoolStringBuilder"/> instance to write to.</param>
        /// <param name="name">The name to bind the method to.</param>
        public void AppendMappedInvokeMethod(ref ArrayPoolStringBuilder builder, string name)
        {
            builder.Append(this.invokeMethod.Replace(InvokeMethodIdentifier, name));
        }
    }
}