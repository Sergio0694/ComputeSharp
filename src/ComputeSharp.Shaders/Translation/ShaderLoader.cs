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
        /// The closure instance for the <see cref="Action"/> field
        /// </summary>
        private readonly object ShaderInstance;

        /// <summary>
        /// Creates a new <see cref="ShaderLoader"/> with the specified parameters
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to use to build the shader</param>
        private ShaderLoader(Action<ThreadIds> action)
        {
            Action = action;
            ShaderType = action.Method.DeclaringType;
            ShaderInstance = action.Target;
        }

        /// <summary>
        /// Gets the closure <see cref="Type"/> for the <see cref="Action"/> field
        /// </summary>
        public Type ShaderType { get; }

        /// <summary>
        /// Gets the <see cref="RootParameter"/> array for the current shader
        /// </summary>
        public RootParameter[] RootParameters { get; private set; }

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="FieldInfo"/> instances mapping the captured buffers in the current shader
        /// </summary>
        private readonly List<FieldInfo> _BufferFields = new List<FieldInfo>();

        /// <summary>
        /// Gets the ordered collection of buffers used as fields in the current shader
        /// </summary>
        public IEnumerable<(int Index, GraphicsResource Resource)> Buffers => _BufferFields.Select((field, i) => (i + 1, (GraphicsResource)field.GetValue(ShaderInstance)));

        private readonly List<HlslBufferInfo> _BuffersList = new List<HlslBufferInfo>();

        /// <summary>
        /// Gets the collection of <see cref="HlslBufferInfo"/> items for the shader fields
        /// </summary>
        public IReadOnlyList<HlslBufferInfo> BuffersList => _BuffersList;

        private readonly List<CapturedFieldInfo> _FieldsList = new List<CapturedFieldInfo>();

        /// <summary>
        /// Gets the collection of <see cref="CapturedFieldInfo"/> items for the shader fields
        /// </summary>
        public IReadOnlyList<CapturedFieldInfo> FieldsList => _FieldsList;

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="FieldInfo"/> instances mapping the captured scalar/vector variables in the current shader
        /// </summary>
        private readonly List<FieldInfo> _VariableFields = new List<FieldInfo>();

        /// <summary>
        /// Gets the collection of values of the captured fields for the current shader
        /// </summary>
        public IEnumerable<object> FieldValuesList => _VariableFields.Select(field => field.GetValue(ShaderInstance));

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
            IReadOnlyList<FieldInfo> shaderFields = ShaderType.GetFields().ToArray();
            if (shaderFields.Any(fieldInfo => fieldInfo.IsStatic)) throw new InvalidOperationException("Empty shader body");

            List<DescriptorRange> descriptorRanges = new List<DescriptorRange>();
            int constantBuffersCount = 0;
            int readOnlyBuffersCount = 0;
            int readWriteBuffersCount = 0;

            // Descriptor for the buffer for captured scalar/vector variables
            descriptorRanges.Add(new DescriptorRange(DescriptorRangeType.ConstantBufferView, 1, constantBuffersCount++));

            // Inspect the captured fields
            foreach (FieldInfo fieldInfo in shaderFields)
            {
                Type fieldType = fieldInfo.FieldType;
                string fieldName = fieldInfo.Name;

                // Constant buffer
                if (HlslKnownTypes.IsConstantBufferType(fieldType))
                {
                    descriptorRanges.Add(new DescriptorRange(DescriptorRangeType.ConstantBufferView, 1, constantBuffersCount));

                    // Track the buffer field
                    _BufferFields.Add(fieldInfo);

                    string typeName = HlslKnownTypes.GetMappedName(fieldType.GenericTypeArguments[0]);
                    _BuffersList.Add(new ConstantBufferFieldInfo(typeName, fieldName, constantBuffersCount++));
                }
                else if (HlslKnownTypes.IsReadOnlyBufferType(fieldType))
                {
                    // Root parameter for a readonly buffer
                    descriptorRanges.Add(new DescriptorRange(DescriptorRangeType.ShaderResourceView, 1, readOnlyBuffersCount));

                    // Track the buffer field
                    _BufferFields.Add(fieldInfo);

                    string typeName = HlslKnownTypes.GetMappedName(fieldType);
                    _BuffersList.Add(new ReadOnlyBufferFieldInfo(typeName, fieldName, readOnlyBuffersCount++));
                }
                else if (HlslKnownTypes.IsReadWriteBufferType(fieldType))
                {
                    // Root parameter for a read write buffer
                    descriptorRanges.Add(new DescriptorRange(DescriptorRangeType.UnorderedAccessView, 1, readWriteBuffersCount));

                    // Track the buffer field
                    _BufferFields.Add(fieldInfo);

                    string typeName = HlslKnownTypes.GetMappedName(fieldType);
                    _BuffersList.Add(new ReadWriteBufferFieldInfo(typeName, fieldName, readWriteBuffersCount++));
                }
                else if (HlslKnownTypes.IsKnownScalarType(fieldType) || HlslKnownTypes.IsKnownVectorType(fieldType))
                {
                    // Register the captured field
                    _VariableFields.Add(fieldInfo);
                    string typeName = HlslKnownTypes.GetMappedName(fieldType);
                    _FieldsList.Add(new CapturedFieldInfo(typeName, fieldName));
                }
                else throw new NotSupportedException($"Unsupported field of type {fieldType.FullName}");
            }
            RootParameters = descriptorRanges.Select(range => new RootParameter(ShaderVisibility.All, range)).ToArray();
        }

        /// <summary>
        /// Loads the entry method for the current shader being loaded
        /// </summary>
        private void LoadMethodSource()
        {
            // Decompile the shader method
            MethodDecompiler.Instance.GetSyntaxTree(Action.Method, out MethodDeclarationSyntax root, out SemanticModel semanticModel);

            // Rewrite the shader method (eg. to fix the type declarations)
            ShaderSyntaxRewriter syntaxRewriter = new ShaderSyntaxRewriter(semanticModel);
            root = (MethodDeclarationSyntax)syntaxRewriter.Visit(root);

            // Get the thread ids identifier name and shader method body
            ThreadsIdsVariableName = root.ParameterList.Parameters.First().Identifier.Text;
            MethodBody = root.Body.ToFullString();

            // Additional preprocessing
            MethodBody = Regex.Replace(MethodBody, @"\d+[fFdD]", m => m.Value.Replace("f", ""));
            MethodBody = MethodBody.TrimEnd('\n', '\r', ' ');
            MethodBody = Regex.Replace(MethodBody, @"(?<!A-Za-z)vector(?!\w)", "_vector"); // The decompiler can name a local Vector[2,3,4] variable as "vector"
        }
    }
}
