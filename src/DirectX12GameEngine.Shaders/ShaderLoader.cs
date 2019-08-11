using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using DirectX12GameEngine.Shaders.Mappings;
using DirectX12GameEngine.Shaders.Primitives;
using DirectX12GameEngine.Shaders.Renderer.Models.Abstract;
using DirectX12GameEngine.Shaders.Renderer.Models.Fields;

namespace DirectX12GameEngine.Shaders
{
    /// <summary>
    /// A <see langword="class"/> responsible for loading and processing <see cref="Action{T}"/> instances
    /// </summary>
    public sealed class ShaderLoader
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
            ShaderType = action.Method.DeclaringType ?? throw new InvalidOperationException("Invalid closure type");
            ShaderInstance = action.Target;
            ShaderFields = ShaderType.GetFields().ToArray();
        }

        private readonly List<FieldInfoBase> _FieldsInfo = new List<FieldInfoBase>();

        /// <summary>
        /// Gets the collection of <see cref="FieldInfoBase"/> items for the shader fields
        /// </summary>
        public IReadOnlyList<FieldInfoBase> FieldsInfo => _FieldsInfo;

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

            return @this;
        }

        /// <summary>
        /// Loads the fields info for the current shader being loaded
        /// </summary>
        private void LoadFieldsInfo()
        {
            if (FieldsInfo.Count > 0) throw new InvalidOperationException("Shader fields already loaded");

            int readWriteBuffersCount = 0;

            foreach (FieldInfo fieldInfo in ShaderFields)
            {
                Type fieldType = fieldInfo.FieldType;
                string fieldName = fieldInfo.Name;
                object fieldValue = fieldInfo.GetValue(ShaderInstance);
                FieldInfoBase processedFieldInfo;

                // Read write buffer
                if (HlslKnownTypes.IsReadWriteBufferType(fieldType))
                {
                    processedFieldInfo = new ReadWriteBufferFieldInfo(fieldType.Name, fieldName, readWriteBuffersCount++);
                }
                else if (HlslKnownTypes.IsKnownScalarType(fieldType))
                {
                    // Static scalar field
                    string typeName = HlslKnownTypes.GetMappedName(fieldType);
                    processedFieldInfo = new StaticScalarFieldInfo(typeName, fieldName, fieldValue);
                }
                else throw new NotSupportedException($"Unsupported field of type {fieldType.FullName}");

                _FieldsInfo.Add(processedFieldInfo);
            }
        }
    }
}
