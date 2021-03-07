using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.__Internals;
using ComputeSharp.Shaders.Translation.Models;
using TerraFX.Interop;

#pragma warning disable CS0419, CS0618

namespace ComputeSharp.Shaders.Translation
{
    /// <inheritdoc cref="ShaderLoader{T}"/>
    internal sealed partial class ShaderLoader<T>
    {
        /// <summary>
        /// A <see langword="delegate"/> that loads all the captured members from a shader instance.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
        /// <param name="shader">The shader instance to load the data from.</param>
        /// <param name="r0">
        /// A reference to the buffer with all the captured <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instances.
        /// This is just represented as a <see cref="ulong"/> reference, to avoid exposing the type publicly.
        /// </param>
        /// <param name="r1">A reference to the buffer to use to store all the captured value types.</param>
        /// <returns>The returned value indicates the total number of written bytes into <paramref name="r1"/>.</returns>
        private delegate int DispatchDataLoader(GraphicsDevice device, in T shader, ref ulong r0, ref byte r1);

        /// <summary>
        /// The number of captured graphics resources (buffers).
        /// </summary>
        private int totalResourceCount;

        /// <summary>
        /// The cached <see cref="DispatchDataLoader"/> instance to load dispatch data before execution.
        /// </summary>
        private DispatchDataLoader? dispatchDataLoader;

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="FieldInfo"/> instances mapping the captured variables in the current shader.
        /// </summary>
        private readonly List<FieldInfo> capturedFields = new();

        /// <summary>
        /// Gets a new <see cref="DispatchData"/> instance with all the captured data for the current shader instance.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> to use to run the shader.</param>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run.</param>
        /// <param name="x">The number of iterations to run on the X axis.</param>
        /// <param name="y">The number of iterations to run on the Y axis.</param>
        /// <param name="z">The number of iterations to run on the Z axis.</param>
        [Pure]
        public DispatchData GetDispatchData(GraphicsDevice device, in T shader, int x, int y, int z)
        {
            // Resources and variables buffers
            ulong[] resources = ArrayPool<ulong>.Shared.Rent(this.totalResourceCount);
            byte[] variables = ArrayPool<byte>.Shared.Rent(256);

            ref ulong r0 = ref MemoryMarshal.GetArrayDataReference(resources);
            ref byte r1 = ref MemoryMarshal.GetArrayDataReference(variables);

            // Set the x, y and z counters
            Unsafe.As<byte, int>(ref r1) = x;
            Unsafe.Add(ref Unsafe.As<byte, int>(ref r1), 1) = y;
            Unsafe.Add(ref Unsafe.As<byte, int>(ref r1), 2) = z;

            // Invoke the dynamic method to extract the captured data
            int totalVariablesByteSize = this.dispatchDataLoader!(device, in shader, ref r0, ref r1);

            return new(resources, this.totalResourceCount, variables, totalVariablesByteSize);
        }

        /// <summary>
        /// Initializes the <see cref="DispatchDataLoader"/> instance that loads the dispatch data for the current shader type.
        /// </summary>
        private void InitializeDispatchDataLoader()
        {
            // Get the generated data loader
            Type[] argumentTypes = new[] { typeof(GraphicsDevice), typeof(T).MakeByRefType(), typeof(ulong).MakeByRefType(), typeof(byte).MakeByRefType() };
            Type type = typeof(T).Assembly.GetType("ComputeSharp.__Internals.DispatchDataLoader")!;
            MethodInfo method = type.GetMethod("LoadDispatchData", argumentTypes)!;

            // Extract the computed count of 32 bit root constants to load
            D3D12Root32BitConstantsCount = ((ComputeRoot32BitConstantsAttribute)method.ReturnTypeCustomAttributes.GetCustomAttributes(false)[0]).Count;

            // Create a delegate from the generated shader data loader
            this.dispatchDataLoader = method.CreateDelegate<DispatchDataLoader>();
        }
    }
}
