using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Buffers.Abstract;

namespace ComputeSharp.Shaders.Translation.Models
{
    /// <summary>
    /// A <see langword="struct"/> that contains all the captured data to dispatch a shader
    /// </summary>
    internal readonly ref struct DispatchData
    {
        /// <summary>
        /// The <see cref="GraphicsResource"/> array with the captured buffers
        /// </summary>
        private readonly GraphicsResource[] ResourcesArray;

        /// <summary>
        /// The number of <see cref="GraphicsResource"/> instances in <see cref="ResourcesArray"/>
        /// </summary>
        private readonly int ResourcesCount;

        /// <summary>
        /// The <see cref="byte"/> array with all the captured variables, with proper padding
        /// </summary>
        private readonly byte[] VariablesArray;

        /// <summary>
        /// The actual size in bytes to use from <see cref="VariablesArray"/>
        /// </summary>
        private readonly int VariablesByteSize;

        /// <summary>
        /// Creates a new <see cref="DispatchData"/> instance with the specified parameters
        /// </summary>
        /// <param name="resourcesArray">The <see cref="GraphicsResource"/> array with the captured buffers</param>
        /// <param name="resourcesCount">The number of <see cref="GraphicsResource"/> instances in <see cref="ResourcesArray"/></param>
        /// <param name="variablesArray">The <see cref="byte"/> array with all the captured variables, with proper padding</param>
        /// <param name="variablesByteSize">The actual size in bytes to use from <see cref="VariablesArray"/></param>
        public DispatchData(GraphicsResource[] resourcesArray, int resourcesCount, byte[] variablesArray, int variablesByteSize)
        {
            ResourcesArray = resourcesArray;
            VariablesArray = variablesArray;
            ResourcesCount = resourcesCount;
            VariablesByteSize = variablesByteSize;
        }

        /// <summary>
        /// Gets a <see cref="Span{T}"/> with all the captured buffers
        /// </summary>
        public Span<GraphicsResource> Resources
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ResourcesArray.AsSpan(0, ResourcesCount);
        }

        /// <summary>
        /// Gets a <see cref="Span{T}"/> with the padded data representing all the captured variables
        /// </summary>
        public Span<Int4> Variables
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref Int4 r = ref Unsafe.As<byte, Int4>(ref VariablesArray[0]);
                bool mod = (VariablesByteSize & 15) > 0;
                int length = VariablesByteSize / 4 + Unsafe.As<bool, byte>(ref mod);
                return MemoryMarshal.CreateSpan(ref r, length);
            }
        }

        /// <summary>
        /// Implements the <see cref="IDisposable.Dispose"/> method and returns the rented buffers
        /// </summary>
        public void Dispose()
        {
            ArrayPool<GraphicsResource>.Shared.Return(ResourcesArray, true);
            ArrayPool<byte>.Shared.Return(VariablesArray);
        }
    }
}
