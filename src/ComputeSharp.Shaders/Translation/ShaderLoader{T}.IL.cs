using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Core.Interop;
using ComputeSharp.Graphics;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Shaders.Extensions;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Translation.Models;
using Microsoft.Toolkit.Diagnostics;
using TerraFX.Interop;

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
        /// <param name="r0">A reference to the buffer with all the captured <see cref="D3D12_GPU_DESCRIPTOR_HANDLE"/> instances.</param>
        /// <param name="r1">A reference to the buffer to use to store all the captured value types.</param>
        private delegate void DispatchDataLoader(GraphicsDevice device, in T shader, ref D3D12_GPU_DESCRIPTOR_HANDLE r0, ref byte r1);

        /// <summary>
        /// The number of captured graphics resources (buffers).
        /// </summary>
        private int totalResourceCount;

        /// <summary>
        /// The size in bytes of the buffer with the captured variables.
        /// </summary>
        /// <remarks>Initially set to 3 <see cref="int"/> values for X, Y and Z threads counts.</remarks>
        private int totalVariablesByteSize = 3 * sizeof(int);

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
            D3D12_GPU_DESCRIPTOR_HANDLE[] resources = ArrayPool<D3D12_GPU_DESCRIPTOR_HANDLE>.Shared.Rent(this.totalResourceCount);
            byte[] variables = ArrayPool<byte>.Shared.Rent(this.totalVariablesByteSize);

            ref D3D12_GPU_DESCRIPTOR_HANDLE r0 = ref MemoryMarshal.GetArrayDataReference(resources);
            ref byte r1 = ref MemoryMarshal.GetArrayDataReference(variables);

            // Set the x, y and z counters
            Unsafe.As<byte, int>(ref r1) = x;
            Unsafe.Add(ref Unsafe.As<byte, int>(ref r1), 1) = y;
            Unsafe.Add(ref Unsafe.As<byte, int>(ref r1), 2) = z;

            // Invoke the dynamic method to extract the captured data
            this.dispatchDataLoader!(device, in shader, ref r0, ref r1);

            return new DispatchData(resources, this.totalResourceCount, variables, this.totalVariablesByteSize);
        }

        /// <summary>
        /// Builds a new <see cref="DispatchDataLoader"/> instance that loads the dispatch data for the current shader type.
        /// </summary>
        private void BuildDispatchDataLoader()
        {
            // The delegate signature is <GraphicsDevice, in T, ref D3D12_GPU_DESCRIPTOR_HANDLE, ref byte, void>.
            // Due to how DynamicMethod<T> is implemented (see notes in the XML docs for DynamicMethod<T>.New),
            // all offsets for the input arguments are shifted by one. Because of this, we have the following:
            //
            // ldarg.1 = GraphicsDevice
            // ldarg.2 = in T shader
            // ldarg.3 = ref D3D12_GPU_DESCRIPTOR_HANDLE r0
            // ldarg.4 = ref byte r1
            this.dispatchDataLoader = DynamicMethod<DispatchDataLoader>.New(il =>
            {
                foreach (FieldInfo fieldInfo in this.capturedFields)
                {
                    if (HlslKnownTypes.IsKnownBufferType(fieldInfo.FieldType))
                    {
                        // Load the offset address into the resource buffers
                        il.Emit(OpCodes.Ldarg_3);

                        if (totalResourceCount > 0)
                        {
                            il.EmitAddOffset<D3D12_GPU_DESCRIPTOR_HANDLE>(this.totalResourceCount);
                        }

                        il.Emit(OpCodes.Ldarg_2);
                        il.EmitReadMember(fieldInfo);

                        // Call NativeObject.ThrowIfDisposed()
                        il.Emit(OpCodes.Dup);
                        il.EmitCall(OpCodes.Callvirt, typeof(NativeObject).GetMethod(
                            nameof(NativeObject.ThrowIfDisposed),
                            BindingFlags.Instance | BindingFlags.NonPublic)!, null);

                        // Call ThrowIfDeviceMismatch(GraphicsDevice)
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Ldarg_1);
                        il.EmitCall(OpCodes.Callvirt, fieldInfo.FieldType.GetMethod(
                            nameof(Buffer<byte>.ThrowIfDeviceMismatch),
                            BindingFlags.Instance | BindingFlags.NonPublic)!, null);

                        // Access D3D12GpuDescriptorHandle
                        il.EmitReadMember(fieldInfo.FieldType.GetField(
                            nameof(Buffer<byte>.D3D12GpuDescriptorHandle),
                            BindingFlags.Instance | BindingFlags.NonPublic)!);
                        il.EmitStoreToAddress(typeof(D3D12_GPU_DESCRIPTOR_HANDLE));

                        this.totalResourceCount++;
                    }
                    else if (HlslKnownTypes.IsKnownScalarType(fieldInfo.FieldType) ||
                             HlslKnownTypes.IsKnownVectorType(fieldInfo.FieldType))
                    {
                        // Calculate the right offset with 16-bytes padding (HLSL constant buffer)
                        int size = Marshal.SizeOf(fieldInfo.FieldType);

                        if (this.totalVariablesByteSize % 16 > 16 - size)
                        {
                            this.totalVariablesByteSize += 16 - this.totalVariablesByteSize % 16;
                        }

                        // Load the target address into the variables buffer
                        il.Emit(OpCodes.Ldarg_S, (byte)4);
                        il.EmitAddOffset(totalVariablesByteSize);

                        il.Emit(OpCodes.Ldarg_2);
                        il.EmitReadMember(fieldInfo);
                        il.EmitStoreToAddress(fieldInfo.FieldType);

                        this.totalVariablesByteSize += size;
                    }
                    else ThrowHelper.ThrowArgumentException("Invalid captured member type");
                }

                il.Emit(OpCodes.Ret);
            });
        }
    }
}
