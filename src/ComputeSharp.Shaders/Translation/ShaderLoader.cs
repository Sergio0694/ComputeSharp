using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Shaders.Extensions;
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
    /// A <see langword="class"/> responsible for loading and processing <see cref="Action{T}"/> instances
    /// </summary>
    internal sealed class ShaderLoader
    {
        /// <summary>
        /// The <see cref="Action{T}"/> that represents the shader to load
        /// </summary>
        private readonly Action<ThreadIds> Action;

        /// <summary>
        /// Creates a new <see cref="ShaderLoader"/> with the specified parameters
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to use to build the shader</param>
        private ShaderLoader(Action<ThreadIds> action)
        {
            Action = action;
            ShaderType = action.Method.DeclaringType;
        }

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
        /// Gets the closure <see cref="Type"/> for the <see cref="Action"/> field
        /// </summary>
        public Type ShaderType { get; }

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="DescriptorRange1"/> items that are required to load the captured values
        /// </summary>
        private readonly List<DescriptorRange1> DescriptorRanges = new List<DescriptorRange1>();

        private RootParameter1[] _RootParameters;

        /// <summary>
        /// Gets the <see cref="RootParameter1"/> array for the current shader
        /// </summary>
        public RootParameter1[] RootParameters => _RootParameters ??= DescriptorRanges.Select(range => new RootParameter1(new RootDescriptorTable1(range), ShaderVisibility.All)).ToArray();

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="ReadableMember"/> instances mapping the captured buffers in the current shader
        /// </summary>
        private readonly List<(ReadableMember Member, IEnumerable<ReadableMember>? Parents)> _BufferMembers = new List<(ReadableMember, IEnumerable<ReadableMember>?)>();

        /// <summary>
        /// Gets the ordered collection of buffers used as fields in the current shader
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to use to build the shader</param>
        public IEnumerable<(int Index, GraphicsResource Resource)> GetBuffers(Action<ThreadIds> action)
        {
            foreach (var (item, i) in _BufferMembers.Select((item, i) => (item, i)))
            {
                if (item.Parents == null) yield return (i + 1, (GraphicsResource)item.Member.GetValue(action.Target));
                else
                {
                    object target = item.Parents.Aggregate(action.Target, (obj, member) => member.GetValue(obj));
                    yield return (i + 1, (GraphicsResource)item.Member.GetValue(target));
                }
            }
        }

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="ReadableMember"/> instances mapping the captured scalar/vector variables in the current shader
        /// </summary>
        private readonly List<(ReadableMember Member, IEnumerable<ReadableMember>? Parents)> _VariableMembers = new List<(ReadableMember, IEnumerable<ReadableMember>?)>();

        /// <summary>
        /// Gets the collection of values of the captured fields for the current shader
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> to use to build the shader</param>
        public IEnumerable<object> GetVariables(Action<ThreadIds> action)
        {
            foreach (var item in _VariableMembers)
            {
                if (item.Parents == null) yield return item.Member.GetValue(action.Target);
                else
                {
                    object target = item.Parents.Aggregate(action.Target, (obj, member) => member.GetValue(obj));
                    yield return item.Member.GetValue(target);
                }
            }
        }

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
        /// Gets a unique hash code for a given <see cref="Action{T}"/>
        /// </summary>
        /// <param name="action">The input <see cref="Action{T}"/> instance to inspect</param>
        [Pure]
        public static int GetHashCode(Action<ThreadIds> action)
        {
            int hashcode = action.Method.GetHashCode();

            foreach (FieldInfo fieldInfo in action.Method.DeclaringType.GetFields())
            {
                if (fieldInfo.FieldType.IsDelegate() &&
                    fieldInfo.GetValue(action.Target) is Delegate func &&
                    (func.Method.IsStatic || func.Method.DeclaringType.IsStatelessDelegateContainer()) &&
                    (HlslKnownTypes.IsKnownScalarType(func.Method.ReturnType) || HlslKnownTypes.IsKnownVectorType(func.Method.ReturnType)) &&
                    fieldInfo.FieldType.GenericTypeArguments.All(type => HlslKnownTypes.IsKnownScalarType(type) ||
                                                                         HlslKnownTypes.IsKnownVectorType(type)))
                {
                    hashcode = unchecked(hashcode * 17 + func.Method.GetHashCode());
                }
            }

            return hashcode;
        }

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

            // Descriptor for the buffer for captured scalar/vector variables
            DescriptorRanges.Add(new DescriptorRange1(DescriptorRangeType.ConstantBufferView, 1, _ConstantBuffersCount++));

            // Inspect the captured fields
            foreach (FieldInfo fieldInfo in shaderFields)
            {
                LoadFieldInfo(fieldInfo);
            }
        }

        /// <summary>
        /// Loads a specified <see cref="ReadableMember"/> and adds it to the shader model
        /// </summary>
        /// <param name="memberInfo">The target <see cref="ReadableMember"/> to load</param>
        /// <param name="name">The optional explicit name to use for the field</param>
        /// <param name="parents">The list of parent fields to reach the current <see cref="ReadableMember"/> from a given <see cref="Action{T}"/></param>
        private void LoadFieldInfo(ReadableMember memberInfo, string? name = null, IReadOnlyList<ReadableMember>? parents = null)
        {
            Type fieldType = memberInfo.MemberType;
            string fieldName = HlslKnownKeywords.GetMappedName(name ?? memberInfo.Name);

            // Constant buffer
            if (HlslKnownTypes.IsConstantBufferType(fieldType))
            {
                DescriptorRanges.Add(new DescriptorRange1(DescriptorRangeType.ConstantBufferView, 1, _ConstantBuffersCount));

                // Track the buffer field
                _BufferMembers.Add((memberInfo, parents));

                string typeName = HlslKnownTypes.GetMappedName(fieldType.GenericTypeArguments[0]);
                _BuffersList.Add(new ConstantBufferFieldInfo(fieldType, typeName, fieldName, _ConstantBuffersCount++));
            }
            else if (HlslKnownTypes.IsReadOnlyBufferType(fieldType))
            {
                // Root parameter for a readonly buffer
                DescriptorRanges.Add(new DescriptorRange1(DescriptorRangeType.ShaderResourceView, 1, _ReadOnlyBuffersCount));

                // Track the buffer field
                _BufferMembers.Add((memberInfo, parents));

                string typeName = HlslKnownTypes.GetMappedName(fieldType);
                _BuffersList.Add(new ReadOnlyBufferFieldInfo(fieldType, typeName, fieldName, _ReadOnlyBuffersCount++));
            }
            else if (HlslKnownTypes.IsReadWriteBufferType(fieldType))
            {
                // Root parameter for a read write buffer
                DescriptorRanges.Add(new DescriptorRange1(DescriptorRangeType.UnorderedAccessView, 1, _ReadWriteBuffersCount));

                // Track the buffer field
                _BufferMembers.Add((memberInfo, parents));

                string typeName = HlslKnownTypes.GetMappedName(fieldType);
                _BuffersList.Add(new ReadWriteBufferFieldInfo(fieldType, typeName, fieldName, _ReadWriteBuffersCount++));
            }
            else if (HlslKnownTypes.IsKnownScalarType(fieldType) || HlslKnownTypes.IsKnownVectorType(fieldType))
            {
                // Register the captured field
                _VariableMembers.Add((memberInfo, parents));
                string typeName = HlslKnownTypes.GetMappedName(fieldType);
                _FieldsList.Add(new CapturedFieldInfo(fieldType, typeName, fieldName));
            }
            else if (fieldType.IsClass && fieldName.StartsWith("CS$<>"))
            {
                // Captured scope, update the parents list
                List<ReadableMember> updatedParents = parents?.ToList() ?? new List<ReadableMember>();
                updatedParents.Add(memberInfo);

                // Recurse on the new compiler generated class
                IReadOnlyList<FieldInfo> fields = fieldType.GetFields().ToArray();
                foreach (FieldInfo fieldInfo in fields)
                {
                    LoadFieldInfo(fieldInfo, null, updatedParents);
                }
            }
            else if (fieldType.IsDelegate() &&
                     memberInfo.GetValue(Action.Target) is Delegate func &&
                     (func.Method.IsStatic || func.Method.DeclaringType.IsStatelessDelegateContainer()) &&
                     (HlslKnownTypes.IsKnownScalarType(func.Method.ReturnType) || HlslKnownTypes.IsKnownVectorType(func.Method.ReturnType)) &&
                     fieldType.GenericTypeArguments.All(type => HlslKnownTypes.IsKnownScalarType(type) ||
                                                                HlslKnownTypes.IsKnownVectorType(type)))
            {
                // Captured static delegates with a return type
                LoadStaticMethodSource(fieldName, func.Method);
            }
        }

        /// <summary>
        /// Loads the entry method for the current shader being loaded
        /// </summary>
        private void LoadMethodSource()
        {
            // Decompile the shader method
            MethodDecompiler.Instance.GetSyntaxTree(Action.Method, MethodType.Closure, out MethodDeclarationSyntax root, out SemanticModel semanticModel);

            // Rewrite the shader method (eg. to fix the type declarations)
            ShaderSyntaxRewriter syntaxRewriter = new ShaderSyntaxRewriter(semanticModel, ShaderType);
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
                LoadFieldInfo(member.Value, member.Key);
            }

            // Register the captured static methods
            foreach (var method in syntaxRewriter.StaticMethods)
            {
                LoadStaticMethodSource(method.Key, method.Value);
            }

            // Get the thread ids identifier name and shader method body
            ThreadsIdsVariableName = root.ParameterList.Parameters.First().Identifier.Text;
            MethodBody = root.Body.ToFullString();

            // Additional preprocessing
            MethodBody = Regex.Replace(MethodBody, @"(?<=\W)(\d+)[fFdD]", m => m.Groups[1].Value);
            MethodBody = MethodBody.TrimEnd('\n', '\r', ' ');
            MethodBody = HlslKnownKeywords.GetMappedText(MethodBody);
        }

        /// <summary>
        /// Loads additional static methods used by the shader
        /// </summary>
        /// <param name="name">The HLSL name of the new method to load</param>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> instance for the method to load</param>
        private void LoadStaticMethodSource(string name, MethodInfo methodInfo)
        {
            // Decompile the target method
            MethodDecompiler.Instance.GetSyntaxTree(methodInfo, MethodType.Static, out MethodDeclarationSyntax root, out SemanticModel semanticModel);

            // Rewrite the method
            ShaderSyntaxRewriter syntaxRewriter = new ShaderSyntaxRewriter(semanticModel, methodInfo.DeclaringType);
            root = (MethodDeclarationSyntax)syntaxRewriter.Visit(root);

            // Register the captured static members
            foreach (var member in syntaxRewriter.StaticMembers)
            {
                LoadFieldInfo(member.Value, member.Key);
            }

            // Register the captured static methods
            foreach (var method in syntaxRewriter.StaticMethods)
            {
                LoadStaticMethodSource(method.Key, method.Value);
            }

            // Get the function parameters
            IReadOnlyList<ParameterInfo> parameters = (
                from parameter in root.ParameterList.Parameters.Select((p, i) => (Node: p, Index: i))
                let modifiers = parameter.Node.Modifiers
                let type = parameter.Node.Type.ToFullString()
                let parameterName = parameter.Node.Identifier.ToFullString()
                let last = parameter.Index == root.ParameterList.Parameters.Count - 1
                select new ParameterInfo(modifiers, type, parameterName, last)).ToArray();

            // Get the function body
            string body = root.Body.ToFullString();
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
