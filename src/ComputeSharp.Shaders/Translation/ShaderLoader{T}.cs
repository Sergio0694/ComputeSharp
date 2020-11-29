using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Renderer.Models;
using ComputeSharp.Shaders.Translation.Models;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_DESCRIPTOR_RANGE_TYPE;

#pragma warning disable CS0618, CS8618

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> responsible for loading and processing shaders of a given type.
    /// </summary>
    /// <typeparam name="T">The type of compute shader currently in use.</typeparam>
    internal sealed partial class ShaderLoader<T>
        where T : struct, IComputeShader
    {
        /// <summary>
        /// The associated <see cref="IComputeShaderSourceAttribute"/> instance for the current shader type.
        /// </summary>
        private static readonly IComputeShaderSourceAttribute Attribute = IComputeShaderSourceAttribute.GetForType<T>();

        /// <summary>
        /// The number of constant buffers to define in the shader.
        /// </summary>
        private uint constantBuffersCount;

        /// <summary>
        /// The number of readonly buffers to define in the shader.
        /// </summary>
        private uint readOnlyBuffersCount;

        /// <summary>
        /// The number of read write buffers to define in the shader.
        /// </summary>
        private uint readWriteBuffersCount;

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="D3D12_DESCRIPTOR_RANGE1"/> items that are required to load the captured values.
        /// </summary>
        private readonly List<D3D12_DESCRIPTOR_RANGE1> d3D12DescriptorRanges1 = new();

        /// <summary>
        /// The <see cref="List{T}"/> with the <see cref="HlslBufferInfo"/> items for the shader fields.
        /// </summary>
        private readonly List<HlslBufferInfo> hlslBuffersInfo = new();

        /// <summary>
        /// The <see cref="List{T}"/> with the <see cref="CapturedFieldInfo"/> items for the shader fields.
        /// </summary>
        private readonly List<CapturedFieldInfo> fieldsInfo = new();

        /// <summary>
        /// The <see cref="List{T}"/> with the <see cref="FunctionInfo"/> items for the shader functions.
        /// </summary>
        private readonly List<object> functionsInfo = new();

        /// <summary>
        /// The <see cref="List{T}"/> with the <see cref="FunctionInfo"/> items for the shader local functions.
        /// </summary>
        private readonly List<string> localFunctionsInfo = new();

        /// <summary>
        /// Creates a new <see cref="ShaderLoader{T}"/> instance.
        /// </summary>
        private ShaderLoader()
        {
        }

        /// <summary>
        /// Gets a <see cref="Span{T}"/> with the loaded <see cref="D3D12_DESCRIPTOR_RANGE1"/> items for the shader.
        /// </summary>
        public ReadOnlySpan<D3D12_DESCRIPTOR_RANGE1> D3D12DescriptorRanges1
        {
            get => CollectionsMarshal.AsSpan(this.d3D12DescriptorRanges1);
        }

        /// <summary>
        /// Gets the name of the <see cref="ThreadIds"/> variable used as input for the shader method.
        /// </summary>
        public string ThreadsIdsVariableName { get; private set; }

        /// <summary>
        /// Gets the generated source code for the method in the current shader.
        /// </summary>
        public string EntryPoint { get; private set; }

        /// <summary>
        /// Gets the collection of <see cref="HlslBufferInfo"/> items for the shader fields.
        /// </summary>
        public IReadOnlyList<HlslBufferInfo> HslsBuffersInfo => this.hlslBuffersInfo;

        /// <summary>
        /// Gets the collection of <see cref="CapturedFieldInfo"/> items for the shader fields.
        /// </summary>
        public IReadOnlyList<CapturedFieldInfo> FieldsInfo => this.fieldsInfo;

        /// <summary>
        /// Gets the collection of <see cref="FunctionInfo"/> items for the shader.
        /// </summary>
        public IReadOnlyList<object> FunctionsInfo => this.functionsInfo;

        /// <summary>
        /// Gets the collection of <see cref="LocalFunctionInfo"/> items for the shader.
        /// </summary>
        public IReadOnlyCollection<string> LocalFunctionsInfo => this.localFunctionsInfo;

        /// <summary>
        /// Gets the collection of declared types for the shader.
        /// </summary>
        public IReadOnlyCollection<string> DeclaredTypes { get; private set; }

        /// <summary>
        /// Loads and processes an input<typeparamref name="T"/> shadeer
        /// </summary>
        /// <param name="shader">The <typeparamref name="T"/> instance to use to build the shader</param>
        /// <returns>A new <see cref="ShaderLoader"/> instance representing the input shader</returns>
        [Pure]
        public static ShaderLoader<T> Load(in T shader)
        {
            ShaderLoader<T> @this = new();

            // Reading members through reflection requires an object parameter,
            // so here we're just boxing the input shader once to avoid allocating
            // it multiple times in the managed heap while processing the shader.
            object box = shader;

            @this.LoadFieldsInfo(box);
            @this.LoadMethodMetadata();
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

            if (shaderFields.Count == 0)
            {
                ThrowHelper.ThrowInvalidOperationException("The shader body must contain at least one field");
            }

            // Descriptor for the buffer for captured scalar/vector variables
            D3D12_DESCRIPTOR_RANGE1 d3D12DescriptorRange1 = new(D3D12_DESCRIPTOR_RANGE_TYPE_CBV, 1, this.constantBuffersCount++);

            this.d3D12DescriptorRanges1.Add(d3D12DescriptorRange1);

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
        private void LoadFieldInfo(object shader, ReadableMember memberInfo)
        {
            Type fieldType = memberInfo.MemberType;
            (string hlslName, string hlslType) = Attribute.Fields[memberInfo.Name];

            // Constant buffer
            if (HlslKnownTypes.IsConstantBufferType(fieldType))
            {
                D3D12_DESCRIPTOR_RANGE1 d3D12DescriptorRange1 = new(D3D12_DESCRIPTOR_RANGE_TYPE_CBV, 1, this.constantBuffersCount);

                this.d3D12DescriptorRanges1.Add(d3D12DescriptorRange1);
                this.capturedMembers.Add(memberInfo);
                this.hlslBuffersInfo.Add(new HlslBufferInfo.Constant(hlslType, hlslName, (int)this.constantBuffersCount++));
            }
            else if (HlslKnownTypes.IsReadOnlyBufferType(fieldType))
            {
                // Root parameter for a readonly buffer
                D3D12_DESCRIPTOR_RANGE1 d3D12DescriptorRange1 = new(D3D12_DESCRIPTOR_RANGE_TYPE_SRV, 1, this.readOnlyBuffersCount);

                this.d3D12DescriptorRanges1.Add(d3D12DescriptorRange1);
                this.capturedMembers.Add(memberInfo);
                this.hlslBuffersInfo.Add(new HlslBufferInfo.ReadOnly(hlslType, hlslName, (int)this.readOnlyBuffersCount++));
            }
            else if (HlslKnownTypes.IsReadWriteBufferType(fieldType))
            {
                // Root parameter for a read write buffer
                D3D12_DESCRIPTOR_RANGE1 d3D12DescriptorRange1 = new(D3D12_DESCRIPTOR_RANGE_TYPE_UAV, 1, this.readWriteBuffersCount);

                this.d3D12DescriptorRanges1.Add(d3D12DescriptorRange1);
                this.capturedMembers.Add(memberInfo);
                this.hlslBuffersInfo.Add(new HlslBufferInfo.ReadWrite(hlslType, hlslName, (int)this.readWriteBuffersCount++));
            }
            else if (HlslKnownTypes.IsKnownScalarType(fieldType) || HlslKnownTypes.IsKnownVectorType(fieldType))
            {
                this.capturedMembers.Add(memberInfo);
                this.fieldsInfo.Add(new CapturedFieldInfo(hlslType, hlslName));
            }
            else if (fieldType.IsDelegate() &&
                     memberInfo.GetValue(shader) is Delegate func &&
                     (func.Method.IsStatic || func.Method.DeclaringType!.IsStatelessDelegateContainer()) &&
                     (HlslKnownTypes.IsKnownScalarType(func.Method.ReturnType) || HlslKnownTypes.IsKnownVectorType(func.Method.ReturnType)) &&
                     fieldType.GenericTypeArguments.All(type => HlslKnownTypes.IsKnownScalarType(type) ||
                                                                HlslKnownTypes.IsKnownVectorType(type)))
            {
                // Captured static delegates with a return type
                throw new NotImplementedException();
            }
            else ThrowHelper.ThrowArgumentException("Invalid captured variable");
        }

        /// <summary>
        /// Loads the metadata info for the current shader.
        /// </summary>
        private void LoadMethodMetadata()
        {
            // Extract the method sources
            foreach (var method in Attribute.Methods)
            {
                switch (method.Key)
                {
                    case nameof(IComputeShader.Execute): EntryPoint = method.Value; break;
                    default: this.localFunctionsInfo.Add(method.Value); break;
                }
            }

            // Extract the dependent types
            DeclaredTypes = Attribute.Types;
        }
    }
}
