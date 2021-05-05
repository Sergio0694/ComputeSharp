using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using ComputeSharp.__Internals;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Renderer.Models;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_DESCRIPTOR_RANGE_TYPE;

#pragma warning disable CS0419, CS0618, CS8618

namespace ComputeSharp.Shaders.Translation
{
    /// <summary>
    /// A <see langword="class"/> responsible for loading and processing shaders of a given type.
    /// </summary>
    /// <typeparam name="T">The type of compute shader currently in use.</typeparam>
    internal sealed partial class ShaderLoader<T> : IShaderLoader
        where T : struct
    {
        /// <summary>
        /// The associated <see cref="IComputeShaderSourceAttribute"/> instance for the current shader type.
        /// </summary>
        private static readonly IComputeShaderSourceAttribute Attribute = IComputeShaderSourceAttribute.GetForType<T>();

        /// <summary>
        /// The number of constant buffers to define in the shader.
        /// </summary>
        /// <remarks>
        /// This starts at 1 as there's always an initial constant buffer before user-defined ones, containing
        /// the captured scalar and vector values as well as the iteration ranges for the shader dispatch.
        /// </remarks>
        private uint constantBuffersCount = 1;

        /// <summary>
        /// The number of readonly buffers to define in the shader.
        /// </summary>
        private uint readOnlyBuffersCount;

        /// <summary>
        /// The number of read write buffers to define in the shader.
        /// </summary>
        private uint readWriteBuffersCount;

        /// <summary>
        /// The array of <see cref="D3D12_DESCRIPTOR_RANGE1"/> items that are required to load the captured values.
        /// </summary>
        private D3D12_DESCRIPTOR_RANGE1[] d3D12DescriptorRanges1;

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
        /// The <see cref="Dictionary{TKey,TValue}"/> with the discovered define declarations for the shader.
        /// </summary>
        private readonly Dictionary<string, string> definesInfo = new();

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
        /// Gets the number of 32 bit constants in the root signature for the shader.
        /// </summary>
        public int D3D12Root32BitConstantsCount { get; private set; }

        /// <summary>
        /// Gets a <see cref="Span{T}"/> with the loaded <see cref="D3D12_DESCRIPTOR_RANGE1"/> items for the shader.
        /// </summary>
        public ReadOnlySpan<D3D12_DESCRIPTOR_RANGE1> D3D12DescriptorRanges1
        {
            get => this.d3D12DescriptorRanges1;
        }

        /// <inheritdoc/>
        public string EntryPoint { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyList<HlslResourceInfo> HlslResourceInfo => this.hlslResourceInfo;

        /// <inheritdoc/>
        public IReadOnlyList<CapturedFieldInfo> FieldsInfo => this.fieldsInfo;

        /// <inheritdoc/>
        public IReadOnlyCollection<string> ForwardDeclarations { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<string> MethodsInfo => this.methodsInfo;

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, string> DefinesInfo => this.definesInfo;

        /// <inheritdoc/>
        public IReadOnlyCollection<string> DeclaredTypes => this.declaredTypes;

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, (string TypeDeclaration, string? Assignment)> StaticFields { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, (string Type, int? Count)> SharedBuffers { get; private set; }

        /// <summary>
        /// Loads and processes an input<typeparamref name="T"/> shadeer.
        /// </summary>
        /// <param name="shader">The <typeparamref name="T"/> instance to use to build the shader.</param>
        /// <returns>A new <see cref="ShaderLoader{T}"/> instance representing the input shader.</returns>
        [Pure]
        public static ShaderLoader<T> Load(in T shader)
        {
            ShaderLoader<T> @this = new();

            @this.LoadMethodMetadata();

            // Reading members through reflection requires an object parameter, so here we're just boxing
            // the input shader once to avoid allocating it multiple times while processing the shader.
            @this.d3D12DescriptorRanges1 = @this.LoadFieldsInfo(shader);

            @this.InitializeDispatchDataLoader();

            return @this;
        }

        /// <summary>
        /// Loads the metadata info for the current shader.
        /// </summary>
        private void LoadMethodMetadata()
        {
            ForwardDeclarations = Attribute.ForwardDeclarations;
            EntryPoint = Attribute.ExecuteMethod;
            StaticFields = Attribute.StaticFields;
            SharedBuffers = Attribute.SharedBuffers;

            this.declaredTypes.AddRange(Attribute.Types);
            this.methodsInfo.AddRange(Attribute.Methods);

            foreach (var pair in Attribute.Defines)
            {
                this.definesInfo.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Loads the fields info for the current shader being loaded.
        /// </summary>
        /// <param name="shader">The boxed <typeparamref name="T"/> instance to use to build the shader.</param>
        /// <returns>The array of <see cref="D3D12_DESCRIPTOR_RANGE1"/> items that are required to load the captured values.</returns>
        private D3D12_DESCRIPTOR_RANGE1[] LoadFieldsInfo(object shader)
        {
            IReadOnlyList<FieldInfo> shaderFields = typeof(T).GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic).ToArray();

            if (shaderFields.Count == 0)
            {
                ThrowHelper.ThrowInvalidOperationException("The shader body must contain at least one field");
            }

            List<D3D12_DESCRIPTOR_RANGE1> d3D12DescriptorRanges1 = new();

            // Add the implicit texture descriptor if the shader is a pixel shader
            foreach (Type interfaceType in typeof(T).GetInterfaces())
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IPixelShader<>))
                {
                    d3D12DescriptorRanges1.Add(new D3D12_DESCRIPTOR_RANGE1(D3D12_DESCRIPTOR_RANGE_TYPE_UAV, 1, this.readWriteBuffersCount));

                    (string hlslType, string hlslName) = Attribute.ImplicitTextureField!.Value;

                    this.hlslResourceInfo.Add(new HlslResourceInfo.ReadWrite(hlslType, hlslName, (int)this.readWriteBuffersCount++));
                    this.totalResourceCount++;

                    break;
                }
            }

            // Inspect the captured fields
            foreach (FieldInfo fieldInfo in shaderFields)
            {
                LoadFieldInfo(shader, fieldInfo, d3D12DescriptorRanges1);
            }

            return d3D12DescriptorRanges1.ToArray();
        }

        /// <summary>
        /// Loads a specified <see cref="FieldInfo"/> and adds it to the shader model.
        /// </summary>
        /// <param name="shader">The boxed <typeparamref name="T"/> instance to use to build the shader.</param>
        /// <param name="fieldInfo">The target <see cref="FieldInfo"/> to load.</param>
        /// <param name="d3D12DescriptorRanges1">The list of discovered <see cref="D3D12_DESCRIPTOR_RANGE1"/> values.</param>
        private void LoadFieldInfo(object shader, FieldInfo fieldInfo, List<D3D12_DESCRIPTOR_RANGE1> d3D12DescriptorRanges1)
        {
            Type fieldType = fieldInfo.FieldType;
            (string hlslName, string hlslType) = Attribute.Fields[fieldInfo.Name];

            if (HlslKnownTypes.IsConstantBufferType(fieldType))
            {
                d3D12DescriptorRanges1.Add(new D3D12_DESCRIPTOR_RANGE1(D3D12_DESCRIPTOR_RANGE_TYPE_CBV, 1, this.constantBuffersCount));

                this.capturedFields.Add(fieldInfo);
                this.hlslResourceInfo.Add(new HlslResourceInfo.Constant(hlslType, hlslName, (int)this.constantBuffersCount++));
                this.totalResourceCount++;
            }
            else if (HlslKnownTypes.IsReadOnlyResourceType(fieldType))
            {
                d3D12DescriptorRanges1.Add(new D3D12_DESCRIPTOR_RANGE1(D3D12_DESCRIPTOR_RANGE_TYPE_SRV, 1, this.readOnlyBuffersCount));

                this.capturedFields.Add(fieldInfo);
                this.hlslResourceInfo.Add(new HlslResourceInfo.ReadOnly(hlslType, hlslName, (int)this.readOnlyBuffersCount++));
                this.totalResourceCount++;
            }
            else if (HlslKnownTypes.IsReadWriteResourceType(fieldType))
            {
                d3D12DescriptorRanges1.Add(new D3D12_DESCRIPTOR_RANGE1(D3D12_DESCRIPTOR_RANGE_TYPE_UAV, 1, this.readWriteBuffersCount));

                this.capturedFields.Add(fieldInfo);
                this.hlslResourceInfo.Add(new HlslResourceInfo.ReadWrite(hlslType, hlslName, (int)this.readWriteBuffersCount++));
                this.totalResourceCount++;
            }
            else if (fieldInfo.GetValue(shader) is Delegate func)
            {
                Guard.IsTrue(func.Method.IsStatic, fieldInfo.Name, "Captured delegates must be pointing to static methods");

                // Captured static delegates with a return type
                var methodSource = ShaderMethodSourceAttribute.GetForDelegate(func);

                this.declaredTypes.AddRange(methodSource.Types);
                this.methodsInfo.Add(methodSource.GetMappedInvokeMethod(hlslName));
                this.methodsInfo.AddRange(methodSource.Methods);

                foreach (var pair in methodSource.Constants)
                {
                    this.definesInfo[pair.Key] = pair.Value;
                }
            }
            else
            {
                this.capturedFields.Add(fieldInfo);
                this.fieldsInfo.Add(new CapturedFieldInfo(hlslType, hlslName));
            }
        }
    }
}
