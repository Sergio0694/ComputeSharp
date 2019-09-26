using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Translation.Models;

namespace ComputeSharp.Shaders.Translation
{
    /// <inheritdoc cref="ShaderLoader"/>
    internal sealed partial class ShaderLoader
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
        /// A <see langword="delegate"/> that loads all the captured members from a shader closure
        /// </summary>
        /// <param name="obj">The shader closure instance to load the data from</param>
        /// <param name="r0">A reference to the buffer with all the captured <see cref="GraphicsResource"/> instances</param>
        /// <param name="r1">A reference to the buffer to use to store all the captured value types</param>
        private delegate void DispatchDataLoader(object obj, ref GraphicsResource r0, ref byte r1);

        /// <summary>
        /// Gets a new <see cref="DispatchData"/> instance with all the captured data for the current shader instance
        /// </summary>
        /// <param name="action">The input <see cref="Action{T}"/> representing the compute shader to run</param>
        /// <param name="x">The number of iterations to run on the X axis</param>
        /// <param name="y">The number of iterations to run on the Y axis</param>
        /// <param name="z">The number of iterations to run on the Z axis</param>
        [Pure]
        public DispatchData GetDispatchData(Action<ThreadIds> action, int x, int y, int z)
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
            _DispatchDataLoader!(action.Target, ref r0, ref r1);

            return new DispatchData(resources, _ResourcesCount, variables, _VariablesByteSize);
        }

        /// <summary>
        /// Builds a new <see cref="DispatchDataLoader"/> instance that loads the dispatch data for the current shader type
        /// </summary>
        private void BuildDispatchDataLoader()
        {
            // Mapping of parent members to index of the relative local variable
            var map =
                _CapturedMembers
                .Where(m => !m.IsStatic && m.Parents != null)
                .SelectMany(m => m.Parents)
                .Distinct()
                .Select((m, i) => (Member: m, Index: i))
                .ToDictionary(p => (object)p.Member, p => p.Index + 1);
            object root = new object(); // Placeholder
            map.Add(root, 0);

            // Set of indices of the loaded parent members
            HashSet<int> loaded = new HashSet<int>(new[] { 0 });

            // Build the dynamic IL method
            _DispatchDataLoader = DynamicMethod<DispatchDataLoader>.New(il =>
            {
                // Loads a given member on the top of the execution stack
                void LoadMember(ReadableMember member)
                {
                    if (!member.IsStatic)
                    {
                        // Load the parent instance on the execution stack if the member isn't static
                        int index = map[member.Parents?.Last() ?? root];
                        if (loaded.Contains(index)) il.EmitLoadLocal(index);
                        else
                        {
                            // Seek upwards to find the most in depth loaded parent
                            int i = member.Parents!.Count - 1;
                            while (i > 0 && !loaded.Contains(map[member.Parents[i]])) i--;

                            // Load the local variables for all the parents of the current member
                            il.EmitLoadLocal(i);
                            for (; i < member.Parents.Count; i++)
                            {
                                index = map[member.Parents[i]];
                                il.Emit(OpCodes.Ldfld, member.Parents[i]);
                                il.EmitStoreLocal(index);
                                il.EmitLoadLocal(index);
                                loaded.Add(index);
                            }
                        }
                    }

                    il.EmitReadMember(member);
                }

                // Declare the local variables
                il.DeclareLocal(ShaderType);
                foreach (ReadableMember member in map.OrderBy(p => p.Value).Skip(1).Select(p => p.Key))
                {
                    il.DeclareLocal(member.MemberType);
                }

                // Cast the closure instance and assign it to the local variable
                il.Emit(OpCodes.Ldarg_0);
                il.EmitCastOrUnbox(ShaderType);
                il.Emit(OpCodes.Stloc_0);

                // Handle all the captured members, both resources and variables
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

                    /* Load the current member accordingly.
                     * When this method returns, the value of the current
                     * member will be at the top of the execution stack */
                    LoadMember(member);

                    il.EmitStoreToAddress(member.MemberType);
                }

                il.Emit(OpCodes.Ret);
            });
        }
    }
}
