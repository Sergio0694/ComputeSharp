using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using ComputeSharp.__Internals;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Renderer.Models;
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
    internal sealed partial class ShaderLoader<T> : IShaderLoader
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
        /// The <see cref="List{T}"/> with the <see cref="Renderer.Models.HlslResourceInfo"/> items for the shader fields.
        /// </summary>
        private readonly List<HlslResourceInfo> hlslResourceInfo = new();

        /// <summary>
        /// The <see cref="List{T}"/> with the <see cref="CapturedFieldInfo"/> items for the shader fields.
        /// </summary>
        private readonly List<CapturedFieldInfo> fieldsInfo = new();

        /// <summary>
        /// The <see cref="List{T}"/> with the collected methods for the shader.
        /// </summary>
        private readonly List<string> methodsInfo = new();

        /// <summary>
        /// The <see cref="List{T}"/> with the collected declared types for the shader.
        /// </summary>
        private readonly List<string> declaredTypes = new();

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

        /// <inheritdoc/>
        public string EntryPoint { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyList<HlslResourceInfo> HlslResourceInfo => this.hlslResourceInfo;

        /// <inheritdoc/>
        public IReadOnlyList<CapturedFieldInfo> FieldsInfo => this.fieldsInfo;

        /// <inheritdoc/>
        public IReadOnlyCollection<string> MethodsInfo => this.methodsInfo;

        /// <inheritdoc/>
        public IReadOnlyCollection<string> DeclaredTypes => this.declaredTypes;

        /// <summary>
        /// Loads and processes an input<typeparamref name="T"/> shadeer.
        /// </summary>
        /// <param name="shader">The <typeparamref name="T"/> instance to use to build the shader.</param>
        /// <returns>A new <see cref="ShaderLoader"/> instance representing the input shader.</returns>
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
            @this.InitializeDispatchDataLoader();

            return @this;
        }

        /// <summary>
        /// Loads the fields info for the current shader being loaded.
        /// </summary>
        /// <param name="shader">The boxed <typeparamref name="T"/> instance to use to build the shader.</param>
        private void LoadFieldsInfo(object shader)
        {
            IReadOnlyList<FieldInfo> shaderFields = typeof(T).GetFields(
                BindingFlags.Instance |
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
        /// Loads a specified <see cref="ReadableMember"/> and adds it to the shader model.
        /// </summary>
        /// <param name="shader">The boxed <typeparamref name="T"/> instance to use to build the shader.</param>
        /// <param name="memberInfo">The target <see cref="FieldInfo"/> to load.</param>
        private void LoadFieldInfo(object shader, FieldInfo fieldInfo)
        {
            Type fieldType = fieldInfo.FieldType;
            (string hlslName, string hlslType) = Attribute.Fields[fieldInfo.Name];

            if (HlslKnownTypes.IsConstantBufferType(fieldType))
            {
                D3D12_DESCRIPTOR_RANGE1 d3D12DescriptorRange1 = new(D3D12_DESCRIPTOR_RANGE_TYPE_CBV, 1, this.constantBuffersCount);

                this.d3D12DescriptorRanges1.Add(d3D12DescriptorRange1);
                this.capturedFields.Add(fieldInfo);
                this.hlslResourceInfo.Add(new HlslResourceInfo.Constant(hlslType, hlslName, (int)this.constantBuffersCount++));
                this.totalResourceCount++;
            }
            else if (HlslKnownTypes.IsReadOnlyResourceType(fieldType))
            {
                D3D12_DESCRIPTOR_RANGE1 d3D12DescriptorRange1 = new(D3D12_DESCRIPTOR_RANGE_TYPE_SRV, 1, this.readOnlyBuffersCount);

                this.d3D12DescriptorRanges1.Add(d3D12DescriptorRange1);
                this.capturedFields.Add(fieldInfo);
                this.hlslResourceInfo.Add(new HlslResourceInfo.ReadOnly(hlslType, hlslName, (int)this.readOnlyBuffersCount++));
                this.totalResourceCount++;
            }
            else if (HlslKnownTypes.IsReadWriteResourceType(fieldType))
            {
                D3D12_DESCRIPTOR_RANGE1 d3D12DescriptorRange1 = new(D3D12_DESCRIPTOR_RANGE_TYPE_UAV, 1, this.readWriteBuffersCount);

                this.d3D12DescriptorRanges1.Add(d3D12DescriptorRange1);
                this.capturedFields.Add(fieldInfo);
                this.hlslResourceInfo.Add(new HlslResourceInfo.ReadWrite(hlslType, hlslName, (int)this.readWriteBuffersCount++));
                this.totalResourceCount++;
            }
            else if (HlslKnownTypes.IsKnownScalarType(fieldType) || HlslKnownTypes.IsKnownVectorType(fieldType))
            {
                this.capturedFields.Add(fieldInfo);
                this.fieldsInfo.Add(new CapturedFieldInfo(hlslType, hlslName));
            }
            else if (fieldInfo.GetValue(shader) is Delegate { Method: { IsStatic: true } } func)
            {
                // Captured static delegates with a return type
                var methodSource = ShaderMethodSourceAttribute.GetForDelegate(func);

                this.declaredTypes.AddRange(methodSource.Types);
                this.methodsInfo.Add(methodSource.GetMappedInvokeMethod(hlslName));
                this.methodsInfo.AddRange(methodSource.Methods);
            }
            else ThrowHelper.ThrowArgumentException("Invalid captured variable");
        }

        /// <summary>
        /// Loads the metadata info for the current shader.
        /// </summary>
        private void LoadMethodMetadata()
        {
            EntryPoint = Attribute.ExecuteMethod;

            this.methodsInfo.AddRange(Attribute.Methods);
            this.declaredTypes.AddRange(Attribute.Types);
        }
    }
}
