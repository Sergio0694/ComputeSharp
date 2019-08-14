using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using ComputeSharp.Graphics.Buffers.Abstract;

namespace ComputeSharp.Graphics.Buffers.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with extension methods for the <see cref="GraphicsDevice"/> type to allocate generic resources
    /// </summary>
    internal static class GraphicsResourceExtensions
    {
        /// <summary>
        /// The <see cref="Dictionary{TKey,TValue}"/> that maps types to the necessary data to create buffers, for quick lookup
        /// </summary>
        private static readonly Dictionary<Type, (ConstructorInfo Constructor, IList Array, MethodInfo Setter)> TypeMapping = new Dictionary<Type, (ConstructorInfo, IList, MethodInfo)>();

        /// <summary>
        /// Allocates a new constant buffer with the specified generic value
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="data">The input <see cref="object"/> to copy to the allocated buffer</param>
        /// <returns>A constant <see cref="ReadOnlyBuffer{T}"/> instance (as a <see cref="GraphicsResource"/>) with the input data</returns>
        [Pure]
        public static GraphicsResource AllocateReadOnlyBufferFromReflectedSingleValue(this GraphicsDevice device, object data)
        {
            Type dataType = data.GetType();
            if (!TypeMapping.TryGetValue(dataType, out var info))
            {
                // Get the generic buffer constructor
                Type bufferType = typeof(ReadOnlyBuffer<>).MakeGenericType(dataType);
                info.Constructor = bufferType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).First();

                // Create the reusable array
                Type arrayType = dataType.MakeArrayType();
                ConstructorInfo[] arrayConstructors = arrayType.GetConstructors(BindingFlags.CreateInstance
                                                                                | BindingFlags.Public
                                                                                | BindingFlags.Instance
                                                                                | BindingFlags.OptionalParamBinding);
                info.Array = (IList)arrayConstructors[0].Invoke(new object[] { 1 });

                // Set the input data to the new buffer
                info.Setter = bufferType.GetMethod(nameof(HlslBuffer<byte>.SetData), new[] { arrayType });

                // Cache for later reuse
                TypeMapping.Add(dataType, info);
            }

            // Create the buffer and set the content
            GraphicsResource buffer = (GraphicsResource)info.Constructor.Invoke(new object[] { device, 1 });
            info.Array[0] = data;
            info.Setter.Invoke(buffer, new object[] { info.Array });

            return buffer;
        }
    }
}
