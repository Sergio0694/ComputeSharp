using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Renderer.Models.Fields;
using ComputeSharp.Shaders.Renderer.Models.Fields.Abstract;
using ComputeSharp.Shaders.Renderer.Models.Functions;
using ComputeSharp.Shaders.Translation.Enums;
using ComputeSharp.Shaders.Translation.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vortice.Direct3D12;
using ParameterInfo = ComputeSharp.Shaders.Renderer.Models.Functions.ParameterInfo;

#pragma warning disable CS8618 // Non-nullable field is uninitialized

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> responsible for loading and processing shaders of a given type
    /// </summary>
    /// <typeparam name="T">The type of compute shader currently in use</typeparam>
    internal sealed partial class ShaderLoader<T>
        where T : struct, IComputeShader
    {
        /// <summary>
        /// Creates a new <see cref="ShaderLoader{T}"/> instance
        /// </summary>
        private ShaderLoader() { }

        /// <summary>
        /// The number of constant buffers to define in the shader
        /// </summary>
        private int _ConstantBuffersCount;

        /// <summary>
        /// The number of readonly buffers to define in the shader
        /// </summary>
        private int _ReadOnlyBuffersCount;

        /// <summary>
        /// The number of read write buffers to define in the shader
        /// </summary>
        private int _ReadWriteBuffersCount;

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="DescriptorRange1"/> items that are required to load the captured values
        /// </summary>
        private readonly List<DescriptorRange1> DescriptorRanges = new List<DescriptorRange1>();

        private RootParameter1[]? _RootParameters;

        /// <summary>
        /// Gets the <see cref="RootParameter1"/> array for the current shader
        /// </summary>
        public RootParameter1[] RootParameters => _RootParameters ??= DescriptorRanges.Select(range => new RootParameter1(new RootDescriptorTable1(range), ShaderVisibility.All)).ToArray();

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
        /// Gets the name of the <see cref="ThreadIds"/> variable used as input for the shader method
        /// </summary>
        public string ThreadsIdsVariableName { get; private set; }

        /// <summary>
        /// Gets the generated source code for the method in the current shader
        /// </summary>
        public string MethodBody { get; private set; }

        private readonly List<FunctionInfo> _FunctionsList = new List<FunctionInfo>();

        /// <summary>
        /// Gets the collection of <see cref="FunctionInfo"/> items for the shader
        /// </summary>
        public IReadOnlyList<FunctionInfo> FunctionsList => _FunctionsList;

        private readonly List<LocalFunctionInfo> _LocalFunctionsList = new List<LocalFunctionInfo>();

        /// <summary>
        /// Gets the collection of <see cref="LocalFunctionInfo"/> items for the shader
        /// </summary>
        public IReadOnlyList<LocalFunctionInfo> LocalFunctionsList => _LocalFunctionsList;

        /// <summary>
        /// Loads and processes an input<typeparamref name="T"/> shadeer
        /// </summary>
        /// <param name="shader">The <typeparamref name="T"/> instance to use to build the shader</param>
        /// <returns>A new <see cref="ShaderLoader"/> instance representing the input shader</returns>
        [Pure]
        public static ShaderLoader<T> Load(in T shader)
        {
            ShaderLoader<T> @this = new ShaderLoader<T>();

            /* Reading members through reflection requires an object parameter,
             * so here we're just boxing the input shader once to avoid allocating
             * it multiple times in the managed heap while processing the shader. */
            object box = shader;

            @this.LoadFieldsInfo(box);
            @this.LoadMethodSource(box);
            @this.BuildDispatchDataLoader();

            return @this;
        }

        /// <summary>
        /// Loads the fields info for the current shader being loaded
        /// </summary>
        /// <param name="shader">The boxed <typeparamref name="T"/> instance to use to build the shader</param>
        private void LoadFieldsInfo(object shader)
        {
            IReadOnlyList<FieldInfo> shaderFields = typeof(T).GetFields(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic).ToArray();

            if (shaderFields.Count == 0) throw new InvalidOperationException("Empty shader body");

            // Descriptor for the buffer for captured scalar/vector variables
            DescriptorRanges.Add(new DescriptorRange1(DescriptorRangeType.ConstantBufferView, 1, _ConstantBuffersCount++));

            // Inspect the captured fields
            foreach (FieldInfo fieldInfo in shaderFields)
            {
                LoadFieldInfo(shader, fieldInfo);
            }
        }

        /// <summary>
        /// Loads a specified <see cref="ReadableMember"/> and adds it to the shader model
        /// </summary>
        /// <param name="shader">The boxed <typeparamref name="T"/> instance to use to build the shader</param>
        /// <param name="memberInfo">The target <see cref="ReadableMember"/> to load</param>
        /// <param name="name">The optional explicit name to use for the field</param>
        private void LoadFieldInfo(object shader, ReadableMember memberInfo, string? name = null)
        {
            Type fieldType = memberInfo.MemberType;
            string fieldName = HlslKnownKeywords.GetMappedName(name ?? memberInfo.Name);

            // Constant buffer
            if (HlslKnownTypes.IsConstantBufferType(fieldType))
            {
                DescriptorRanges.Add(new DescriptorRange1(DescriptorRangeType.ConstantBufferView, 1, _ConstantBuffersCount));

                _CapturedMembers.Add(memberInfo);

                string typeName = HlslKnownTypes.GetMappedName(fieldType.GenericTypeArguments[0]);
                _BuffersList.Add(new ConstantBufferFieldInfo(fieldType, typeName, fieldName, _ConstantBuffersCount++));
            }
            else if (HlslKnownTypes.IsReadOnlyBufferType(fieldType))
            {
                // Root parameter for a readonly buffer
                DescriptorRanges.Add(new DescriptorRange1(DescriptorRangeType.ShaderResourceView, 1, _ReadOnlyBuffersCount));

                _CapturedMembers.Add(memberInfo);

                string typeName = HlslKnownTypes.GetMappedName(fieldType);
                _BuffersList.Add(new ReadOnlyBufferFieldInfo(fieldType, typeName, fieldName, _ReadOnlyBuffersCount++));
            }
            else if (HlslKnownTypes.IsReadWriteBufferType(fieldType))
            {
                // Root parameter for a read write buffer
                DescriptorRanges.Add(new DescriptorRange1(DescriptorRangeType.UnorderedAccessView, 1, _ReadWriteBuffersCount));

                _CapturedMembers.Add(memberInfo);

                string typeName = HlslKnownTypes.GetMappedName(fieldType);
                _BuffersList.Add(new ReadWriteBufferFieldInfo(fieldType, typeName, fieldName, _ReadWriteBuffersCount++));
            }
            else if (HlslKnownTypes.IsKnownScalarType(fieldType) || HlslKnownTypes.IsKnownVectorType(fieldType))
            {
                _CapturedMembers.Add(memberInfo);

                string typeName = HlslKnownTypes.GetMappedName(fieldType);
                _FieldsList.Add(new CapturedFieldInfo(fieldType, typeName, fieldName));
            }
            else if (fieldType.IsDelegate() &&
                     memberInfo.GetValue(shader) is Delegate func &&
                     (func.Method.IsStatic || func.Method.DeclaringType.IsStatelessDelegateContainer()) &&
                     (HlslKnownTypes.IsKnownScalarType(func.Method.ReturnType) || HlslKnownTypes.IsKnownVectorType(func.Method.ReturnType)) &&
                     fieldType.GenericTypeArguments.All(type => HlslKnownTypes.IsKnownScalarType(type) ||
                                                                HlslKnownTypes.IsKnownVectorType(type)))
            {
                // Captured static delegates with a return type
                LoadStaticMethodSource(shader, fieldName, func.Method);
            }
            else throw new ArgumentException($"Invalid captured variable of type {fieldType} with name \"{memberInfo.Name}\"");
        }

        /// <summary>
        /// Loads the entry method for the current shader being loaded
        /// </summary>
        /// <param name="shader">The boxed <typeparamref name="T"/> instance to use to build the shader</param>
        private void LoadMethodSource(object shader)
        {
            // Decompile the shader method
            MethodInfo methodInfo = typeof(T).GetMethod(nameof(IComputeShader.Execute));
            MethodDecompiler.Instance.GetSyntaxTree(methodInfo, MethodType.Execute, out MethodDeclarationSyntax root, out SemanticModel semanticModel);

            // Rewrite the shader method (eg. to fix the type declarations)
            ShaderSyntaxRewriter syntaxRewriter = new ShaderSyntaxRewriter(semanticModel, typeof(T));
            root = (MethodDeclarationSyntax)syntaxRewriter.Visit(root);

            // Extract the implicit local functions
            var locals = root.DescendantNodes().OfType<LocalFunctionStatementSyntax>().ToArray();
            root = root.RemoveNodes(locals, SyntaxRemoveOptions.KeepNoTrivia);
            foreach (var local in locals)
            {
                string alignedLocal = local.ToFullString().RemoveLeftPadding().Trim(' ', '\r', '\n');
                alignedLocal = Regex.Replace(alignedLocal, @"(?<=\W)(\d+)[fFdD]", m => m.Groups[1].Value);
                alignedLocal = HlslKnownKeywords.GetMappedText(alignedLocal);

                _LocalFunctionsList.Add(new LocalFunctionInfo(alignedLocal));
            }

            // Register the captured static members
            foreach (var member in syntaxRewriter.StaticMembers)
            {
                LoadFieldInfo(shader, member.Value, member.Key);
            }

            // Register the captured static methods
            foreach (var method in syntaxRewriter.StaticMethods)
            {
                LoadStaticMethodSource(shader, method.Key, method.Value);
            }

            // Get the thread ids identifier name and shader method body
            ThreadsIdsVariableName = root.ParameterList.Parameters.First().Identifier.Text;
            MethodBody = root.Body!.ToFullString();

            // Additional preprocessing
            MethodBody = Regex.Replace(MethodBody, @"(?<=\W)(\d+)[fFdD]", m => m.Groups[1].Value);
            MethodBody = MethodBody.TrimEnd('\n', '\r', ' ');
            MethodBody = HlslKnownKeywords.GetMappedText(MethodBody);
        }

        /// <summary>
        /// Loads additional static methods used by the shader
        /// </summary>
        /// <param name="shader">The boxed <typeparamref name="T"/> instance to use to build the shader</param>
        /// <param name="name">The HLSL name of the new method to load</param>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> instance for the method to load</param>
        private void LoadStaticMethodSource(object shader, string name, MethodInfo methodInfo)
        {
            // Decompile the target method
            MethodDecompiler.Instance.GetSyntaxTree(methodInfo, MethodType.Static, out MethodDeclarationSyntax root, out SemanticModel semanticModel);

            // Rewrite the method
            ShaderSyntaxRewriter syntaxRewriter = new ShaderSyntaxRewriter(semanticModel, methodInfo.DeclaringType);
            root = (MethodDeclarationSyntax)syntaxRewriter.Visit(root);

            // Register the captured static members
            foreach (var member in syntaxRewriter.StaticMembers)
            {
                LoadFieldInfo(shader, member.Value, member.Key);
            }

            // Register the captured static methods
            foreach (var method in syntaxRewriter.StaticMethods)
            {
                LoadStaticMethodSource(shader, method.Key, method.Value);
            }

            // Get the function parameters
            IReadOnlyList<ParameterInfo> parameters = (
                from parameter in root.ParameterList.Parameters.Select((p, i) => (Node: p, Index: i))
                let modifiers = parameter.Node.Modifiers
                let type = parameter.Node.Type!.ToFullString()
                let parameterName = parameter.Node.Identifier.ToFullString()
                let last = parameter.Index == root.ParameterList.Parameters.Count - 1
                select new ParameterInfo(modifiers, type, parameterName, last)).ToArray();

            // Get the function body
            string body = root.Body!.ToFullString();
            body = Regex.Replace(body, @"(?<=\W)(\d+)[fFdD]", m => m.Groups[1].Value);
            body = body.TrimEnd('\n', '\r', ' ');
            body = HlslKnownKeywords.GetMappedText(body);

            // Get the final function info instance
            FunctionInfo functionInfo = new FunctionInfo(
                methodInfo.ReturnType,
                $"{methodInfo.DeclaringType.FullName}{Type.Delimiter}{methodInfo.Name}",
                string.Join(", ", methodInfo.GetParameters().Select(p => $"{p.ParameterType.ToFriendlyString()} {p.Name}")),
                root.ReturnType.ToString(),
                name,
                parameters,
                body);

            _FunctionsList.Add(functionInfo);
        }
    }
}
