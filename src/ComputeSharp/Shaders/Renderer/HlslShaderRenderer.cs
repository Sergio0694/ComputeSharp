using System.Diagnostics.Contracts;
using ComputeSharp.__Internals;
using ComputeSharp.Shaders.Renderer.Models;
using ComputeSharp.Shaders.Translation;

namespace ComputeSharp.Shaders.Renderer
{
    /// <summary>
    /// A type that renders an HLSL template from the previously loaded shader data.
    /// </summary>
    internal static class HlslShaderRenderer
    {
        /// <summary>
        /// Renders a new HLSL shader source with the given parameters.
        /// </summary>
        /// <param name="threadsX">The number of threads in each thread group for the X axis.</param>
        /// <param name="threadsY">The number of threads in each thread group for the Y axis.</param>
        /// <param name="threadsZ">The number of threads in each thread group for the Z axis.</param>
        /// <param name="info">The input <see cref="IShaderLoader"/> instance with the shader information.</param>
        /// <returns>The source code for the new HLSL shader.</returns>
        [Pure]
        public static ArrayPoolStringBuilder Render(int threadsX, int threadsY, int threadsZ, IShaderLoader info)
        {
            var builder = ArrayPoolStringBuilder.Create();

            // Header
            builder.AppendLine("// ================================================");
            builder.AppendLine("//                  AUTO GENERATED");
            builder.AppendLine("// ================================================");
            builder.AppendLine("// This shader was created by ComputeSharp.");
            builder.AppendLine("// See: https://github.com/Sergio0694/ComputeSharp.");

            // Group size constants
            builder.AppendLine();
            builder.Append("#define __GroupSize__get_X ");
            builder.AppendLine(threadsX.ToString());
            builder.Append("#define __GroupSize__get_Y ");
            builder.AppendLine(threadsY.ToString());
            builder.Append("#define __GroupSize__get_Z ");
            builder.AppendLine(threadsZ.ToString());

            // Define declarations
            foreach (var define in info.DefinesInfo)
            {
                builder.Append("#define ");
                builder.Append(define.Key);
                builder.Append(' ');
                builder.AppendLine(define.Value);
            }

            // Static fields
            if (info.StaticFields.Count > 0)
            {
                builder.AppendLine();

                foreach (var constant in info.StaticFields)
                {
                    builder.Append(constant.Value.TypeDeclaration);
                    builder.Append(' ');
                    builder.Append(constant.Key);

                    if (constant.Value.Assignment is string assignment)
                    {
                        builder.Append(" = ");
                        builder.Append(assignment);
                    }

                    builder.AppendLine(";");
                }
            }

            // Declared types
            foreach (var type in info.DeclaredTypes)
            {
                builder.AppendLine();
                builder.AppendLine(type);
            }

            // Captured variables
            builder.AppendLine();
            builder.AppendLine("cbuffer _ : register(b0)");
            builder.AppendLine('{');
            builder.AppendLine("    uint __x;");
            builder.AppendLine("    uint __y;");

            if (info.IsComputeShader)
            {
                builder.AppendLine("    uint __z;");
            }

            foreach (var local in info.FieldsInfo)
            {
                builder.Append("    ");
                builder.Append(local.FieldType);
                builder.Append(' ');
                builder.Append(local.FieldName);
                builder.AppendLine(';');
            }

            builder.AppendLine('}');

            // Resources
            foreach (var resource in info.HlslResourceInfo)
            {
                builder.AppendLine();

                switch (resource)
                {
                    // Constant buffer go to cbuffer fields with a dummy local
                    case HlslResourceInfo.Constant _:
                        builder.Append("cbuffer _");
                        builder.Append(resource.FieldName);
                        builder.Append(" : register(b");
                        builder.Append(resource.RegisterIndex.ToString());
                        builder.AppendLine(')');
                        builder.AppendLine('{');
                        builder.Append("    ");
                        builder.Append(resource.FieldType);
                        builder.Append(' ');
                        builder.Append(resource.FieldName);
                        builder.AppendLine("[2];");
                        builder.AppendLine('}');
                        break;

                    // Structured buffer have the same syntax but different id
                    case HlslResourceInfo.ReadOnly _:
                        char registerId = 't';
                        goto StructuredBuffer;
                    case HlslResourceInfo.ReadWrite _:
                        registerId = 'u';
                        StructuredBuffer:
                        builder.Append(resource.FieldType);
                        builder.Append(' ');
                        builder.Append(resource.FieldName);
                        builder.Append(" : register(");
                        builder.Append(registerId);
                        builder.Append(resource.RegisterIndex.ToString());
                        builder.AppendLine(");");
                        break;

                    // Texture samplers
                    case HlslResourceInfo.Sampler _:
                        builder.Append(resource.FieldType);
                        builder.Append(' ');
                        builder.Append(resource.FieldName);
                        builder.Append(" : register(");
                        builder.Append('s');
                        builder.Append(resource.RegisterIndex.ToString());
                        builder.AppendLine(");");
                        break;
                }
            }

            // Shared buffers
            foreach (var buffer in info.SharedBuffers)
            {
                builder.AppendLine();
                builder.Append("groupshared ");
                builder.Append(buffer.Value.Type);
                builder.Append(' ');
                builder.Append(buffer.Key);
                builder.Append('[');

                if (buffer.Value.Count is int count) builder.Append(count.ToString());
                else builder.Append((threadsX * threadsY * threadsZ).ToString());

                builder.AppendLine("];");
            }

            // Forward declarations
            foreach (var declaration in info.ForwardDeclarations)
            {
                builder.AppendLine();
                builder.AppendLine(declaration);
            }

            // Captured methods
            foreach (var function in info.MethodsInfo)
            {
                builder.AppendLine();
                builder.AppendLine(function);
            }

            // Entry point
            builder.AppendLine();
            builder.AppendLine("[NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]");
            builder.AppendLine(info.EntryPoint);

            return builder;
        }
    }
}
