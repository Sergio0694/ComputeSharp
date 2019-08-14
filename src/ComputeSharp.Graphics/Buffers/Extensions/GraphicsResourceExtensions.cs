using System;
using System.Collections;
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
        /// Allocates a new constant buffer with the specified generic value
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to use to allocate the buffer</param>
        /// <param name="data">The input <see cref="object"/> to copy to the allocated buffer</param>
        /// <returns>A constant <see cref="ReadOnlyBuffer{T}"/> instance (as a <see cref="GraphicsResource"/>) with the input data</returns>
        [Pure]
        public static GraphicsResource AllocateReadOnlyBufferFromReflectedSingleValue(this GraphicsDevice device, object data)
        {
            // Create the generic constant buffer
            Type
                dataType = data.GetType(),
                bufferType = typeof(ReadOnlyBuffer<>).MakeGenericType(dataType);
            ConstructorInfo constructor = bufferType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).First();
            GraphicsResource buffer = (GraphicsResource)constructor.Invoke(new object[] { device, 1 });

            // Create the array with the input value
            Type arrayType = dataType.MakeArrayType();
            ConstructorInfo[] arrayConstructors = arrayType.GetConstructors(BindingFlags.CreateInstance
                                                                            | BindingFlags.Public
                                                                            | BindingFlags.Instance
                                                                            | BindingFlags.OptionalParamBinding);
            Array array = (Array)arrayConstructors[0].Invoke(new object[] { 1 });

            // Use the IList indexer to set the generic object
            IList ilist = array;
            ilist[0] = data;

            // Set the input data to the new buffer
            MethodInfo setDataMethod = bufferType.GetMethod(nameof(HlslBuffer<byte>.SetData), new[] { arrayType });
            setDataMethod.Invoke(buffer, new object[] { array });

            return buffer;
        }
    }
}
