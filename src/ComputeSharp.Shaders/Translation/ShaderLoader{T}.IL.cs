using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Translation.Models;

namespace ComputeSharp.Shaders.Translation
{
    /// <inheritdoc cref="ShaderLoader{T}"/>
    internal sealed partial class ShaderLoader<T>
    {
        /// <summary>
        /// The number of captured graphics resources (buffers)
        /// </summary>
        private int _ResourcesCount;

        /// <summary>
        /// The size in bytes of the buffer with the captured variables
        /// </summary>
        private int _VariablesByteSize = 3 * sizeof(int); // X, Y and Z threads count

        // The cached loader method
        private DispatchDataLoader? _DispatchDataLoader;

        /// <summary>
        /// The <see cref="List{T}"/> of <see cref="ReadableMember"/> instances mapping the captured variables in the current shader
        /// </summary>
        private readonly List<ReadableMember> _CapturedMembers = new List<ReadableMember>();

        /// <summary>
        /// A <see langword="delegate"/> that loads all the captured members from a shader instance
        /// </summary>
        /// <param name="shader">The shader instance to load the data from</param>
        /// <param name="r0">A reference to the buffer with all the captured <see cref="GraphicsResource"/> instances</param>
        /// <param name="r1">A reference to the buffer to use to store all the captured value types</param>
        private delegate void DispatchDataLoader(in T shader, ref GraphicsResource r0, ref byte r1);

        /// <summary>
        /// Gets a new <see cref="DispatchData"/> instance with all the captured data for the current shader instance
        /// </summary>
        /// <param name="shader">The input <typeparamref name="T"/> instance representing the compute shader to run</param>
        /// <param name="x">The number of iterations to run on the X axis</param>
        /// <param name="y">The number of iterations to run on the Y axis</param>
        /// <param name="z">The number of iterations to run on the Z axis</param>
        [Pure]
        public DispatchData GetDispatchData(in T shader, int x, int y, int z)
        {
            // Resources and variables buffers
            GraphicsResource[] resources = ArrayPool<GraphicsResource>.Shared.Rent(_ResourcesCount);
            byte[] variables = ArrayPool<byte>.Shared.Rent(_VariablesByteSize);
            ref GraphicsResource r0 = ref resources[0];
            ref byte r1 = ref variables[0];

            // Set the x, y and z counters
            Unsafe.As<byte, int>(ref r1) = x;
            Unsafe.Add(ref Unsafe.As<byte, int>(ref r1), 1) = y;
            Unsafe.Add(ref Unsafe.As<byte, int>(ref r1), 2) = z;

            // Invoke the dynamic method to extract the captured data
            _DispatchDataLoader!(shader, ref r0, ref r1);

            return new DispatchData(resources, _ResourcesCount, variables, _VariablesByteSize);
        }

        /// <summary>
        /// Builds a new <see cref="DispatchDataLoader"/> instance that loads the dispatch data for the current shader type
        /// </summary>
        private void BuildDispatchDataLoader()
        {
            _DispatchDataLoader = DynamicMethod<DispatchDataLoader>.New(il =>
            {
                foreach (ReadableMember member in _CapturedMembers)
                {
                    if (HlslKnownTypes.IsKnownBufferType(member.MemberType))
                    {
                        // Load the offset address into the resource buffers
                        il.Emit(OpCodes.Ldarg_1);
                        if (_ResourcesCount > 0) il.EmitAddOffset<GraphicsResource>(_ResourcesCount);
                        _ResourcesCount++;
                    }
                    else if (HlslKnownTypes.IsKnownScalarType(member.MemberType) || HlslKnownTypes.IsKnownVectorType(member.MemberType))
                    {
                        // Calculate the right offset with 16-bytes padding (HLSL constant buffer)
                        int size = member.MemberType == typeof(bool) ? sizeof(uint) : Marshal.SizeOf(member.MemberType); // bool is 4 bytes in HLSL
                        if (_VariablesByteSize % 16 > 16 - size) _VariablesByteSize += 16 - _VariablesByteSize % 16;

                        // Load the target address into the variables buffer
                        il.Emit(OpCodes.Ldarg_2);
                        il.EmitAddOffset(_VariablesByteSize);
                        _VariablesByteSize += size;
                    }
                    else throw new InvalidOperationException($"Invalid captured member of type {member.MemberType}");

                    // Load the reference to the input delegate if the member is not static
                    if (!member.IsStatic) il.Emit(OpCodes.Ldarg_0);

                    // Read the current member and store it to the target address
                    il.EmitReadMember(member);
                    il.EmitStoreToAddress(member.MemberType);
                }

                il.Emit(OpCodes.Ret);
            });
        }
    }
}
