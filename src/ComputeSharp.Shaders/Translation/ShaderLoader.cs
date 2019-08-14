using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Renderer.Models.Fields;
using ComputeSharp.Shaders.Renderer.Models.Fields.Abstract;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SharpDX.Direct3D12;

#pragma warning disable CS8618 // Non-nullable field is uninitialized

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> responsible for loading and processing <see cref="Action{T}"/> instances
    /// </summary>
    internal sealed class ShaderLoader
    {
        /// <summary>
        /// The <see cref="Action{T}"/> that represents the shader to load
        /// </summary>
        private readonly Action<ThreadIds> Action;

        /// <summary>
        /// The closure <see cref="Type"/> for the <see cref="Action"/> field
        /// </summary>
        private readonly Type ShaderType;

        /// <summary>
        /// The closure instance for the <see cref="Action"/> field
        /// </summary>
        private readonly object ShaderInstance;

        /// <summary>
        /// The sequence of fields in the targeted closure
        /// </summary>
        private readonly IReadOnlyList<FieldInfo> ShaderFields;

        /// <summary>
        /// Creates a new <see cref="ShaderLoader"/> with the specified parameters
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to use to build the shader</param>
        private ShaderLoader(Action<ThreadIds> action)
        {
            Action = action;
            ShaderType = action.Method.DeclaringType;
            ShaderInstance = action.Target;
            ShaderFields = ShaderType.GetFields().ToArray();
        }

        /// <summary>
        /// Gets the <see cref="RootParameter"/> array for the current shader
        /// </summary>
        public RootParameter[] RootParameters { get; private set; }

        /// <summary>
        /// Gets the ordered collection of buffers used as fields in the current shader
        /// </summary>
        public IReadOnlyList<(int Index, GraphicsResource Resource)> Buffers { get; private set; }

        /// <summary>
        /// Gets the ordered collection of captured variables that need to be assigned to constant buffers
        /// </summary>
        public IReadOnlyList<(int Index, object Value)> CapturedConstantBufferValues { get; private set; }

        private readonly List<FieldInfoBase> _FieldsInfo = new List<FieldInfoBase>();

        /// <summary>
        /// Gets the collection of <see cref="FieldInfoBase"/> items for the shader fields
        /// </summary>
        public IReadOnlyList<FieldInfoBase> FieldsInfo => _FieldsInfo;

        /// <summary>
        /// Gets the name of the <see cref="ThreadIds"/> variable used as input for the shader method
        /// </summary>
        public string ThreadsIdsVariableName { get; private set; }

        /// <summary>
        /// Gets the generated source code for the method in the current shader
        /// </summary>
        public string MethodBody { get; private set; }

        /// <summary>
        /// Loads and processes an input <see cref="Action{T}"/>
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to use to build the shader</param>
        /// <returns>A new <see cref="ShaderLoader"/> instance representing the input shader</returns>
        [Pure]
        public static ShaderLoader Load(Action<ThreadIds> action)
        {
            ShaderLoader @this = new ShaderLoader(action);

            @this.LoadFieldsInfo();
            @this.LoadMethodSource();

            return @this;
        }

        /// <summary>
        /// Loads the fields info for the current shader being loaded
        /// </summary>
        private void LoadFieldsInfo()
        {
            if (FieldsInfo.Count > 0) throw new InvalidOperationException("Shader fields already loaded");

            if (ShaderFields.Any(fieldInfo => fieldInfo.IsStatic)) throw new InvalidOperationException("Empty shader body");

            List<DescriptorRange> descriptorRanges = new List<DescriptorRange>();
            List<(int, GraphicsResource)> buffers = new List<(int, GraphicsResource)>();
            List<(int, object)> variables = new List<(int, object)>();
            int readWriteBuffersCount = 0;
            int readOnlyBuffersCount = 0;

            foreach (FieldInfo fieldInfo in ShaderFields)
            {
                Type fieldType = fieldInfo.FieldType;
                string fieldName = fieldInfo.Name;
                object fieldValue = fieldInfo.GetValue(ShaderInstance);
                FieldInfoBase processedFieldInfo;

                // Read write buffer
                if (HlslKnownTypes.IsReadWriteBufferType(fieldType))
                {
                    // Root parameter for a read write buffer
                    DescriptorRange range = new DescriptorRange(DescriptorRangeType.UnorderedAccessView, 1, readWriteBuffersCount);
                    descriptorRanges.Add(range);

                    // Reference to the underlying buffer
                    buffers.Add((descriptorRanges.Count - 1, (GraphicsResource)fieldValue));

                    string typeName = HlslKnownTypes.GetMappedName(fieldType);
                    processedFieldInfo = new ReadWriteBufferFieldInfo(typeName, fieldName, readWriteBuffersCount++);
                }
                else if (HlslKnownTypes.IsReadOnlyBufferType(fieldType))
                {
                    // Read only buffer
                    DescriptorRange range = new DescriptorRange(DescriptorRangeType.ConstantBufferView, 1, readOnlyBuffersCount);
                    descriptorRanges.Add(range);

                    // Reference to the underlying buffer
                    buffers.Add((descriptorRanges.Count - 1, (GraphicsResource)fieldValue));

                    string typeName = HlslKnownTypes.GetMappedName(fieldType.GenericTypeArguments[0]);
                    processedFieldInfo = new ConstantBufferFieldInfo(typeName, fieldName, readOnlyBuffersCount++, false);
                }
                else if (HlslKnownTypes.IsKnownScalarType(fieldType))
                {
                    // Read only buffer
                    DescriptorRange range = new DescriptorRange(DescriptorRangeType.ConstantBufferView, 1, readOnlyBuffersCount);
                    descriptorRanges.Add(range);

                    // Register the captured value
                    variables.Add((descriptorRanges.Count - 1, fieldValue));

                    // Constant buffer field
                    string typeName = HlslKnownTypes.GetMappedName(fieldType);
                    processedFieldInfo = new ConstantBufferFieldInfo(typeName, fieldName, readOnlyBuffersCount++, true);
                }
                else throw new NotSupportedException($"Unsupported field of type {fieldType.FullName}");

                _FieldsInfo.Add(processedFieldInfo);
            }

            RootParameters = descriptorRanges.Select(range => new RootParameter(ShaderVisibility.All, range)).ToArray();
            Buffers = buffers;
            CapturedConstantBufferValues = variables;
        }

        /// <summary>
        /// Loads the entry method for the current shader being loaded
        /// </summary>
        private void LoadMethodSource()
        {
            // Decompile the shader method
            MethodDecompiler.Instance.GetSyntaxTree(Action.Method, out SyntaxNode root, out SemanticModel semanticModel);

            // Rewrite the shader method (eg. to fix the type declarations)
            ShaderSyntaxRewriter syntaxRewriter = new ShaderSyntaxRewriter(semanticModel);
            root = syntaxRewriter.Visit(root);

            // Get the thread ids identifier name and shader method body
            MethodDeclarationSyntax methodNode = root.ChildNodes().OfType<MethodDeclarationSyntax>().First();
            ThreadsIdsVariableName = methodNode.ParameterList.Parameters.First().Identifier.Text;
            MethodBody = methodNode.Body.ToFullString();

            // Additional preprocessing
            MethodBody = Regex.Replace(MethodBody, @"\d+[fFdD]", m => m.Value.Replace("f", ""));
            MethodBody = MethodBody.TrimEnd('\n', '\r', ' ');
            MethodBody = $"    {MethodBody.Replace(Environment.NewLine, $"{Environment.NewLine}    ")}";
        }
    }
}
