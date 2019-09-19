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
    internal sealed partial class ShaderLoader
    {
        private int _ResourcesCount;

        private int _VariablesByteSize = 3 * sizeof(int); // X, Y and Z threads count

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
        private delegate void MembersLoader(object obj, ref GraphicsResource r0, ref byte r1);

        // The cached loader method
        private MembersLoader? _MembersLoader;

        [Pure]
        public ShaderDispatchData GetDispatchData(Action<ThreadIds> action, int x, int y, int z)
        {
            if (_MembersLoader == null)
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
                HashSet<int> loaded = new HashSet<int>(new [] { 0 });

                // Build the dynamic IL method
                _MembersLoader = DynamicMethod<MembersLoader>.New(il =>
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
                                if (i == 0) il.EmitLoadLocal(0);
                                for (; i < member.Parents.Count; i++)
                                {
                                    il.Emit(OpCodes.Ldfld, member.Parents[i]);
                                    il.EmitStoreLocal(map[member.Parents[i]]);
                                    il.EmitLoadLocal(map[member.Parents[i]]);
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

                            /* Load the current member accordingly.
                             * When this method returns, the value of the current
                             * member will be at the top of the execution stack */
                            LoadMember(member);

                            il.EmitStoreToAddress(member.MemberType);
                        }
                        else if (HlslKnownTypes.IsKnownScalarType(member.MemberType) ||
                                 HlslKnownTypes.IsKnownVectorType(member.MemberType))
                        {
                            // Calculate the right offset with 16-bytes padding (HLSL constant buffer)
                            int size = Marshal.SizeOf(member.MemberType);
                            if (_VariablesByteSize % 16 > 16 - size) _VariablesByteSize += 16 - _VariablesByteSize % 16;

                            // Load the target address into the variables buffer
                            il.Emit(OpCodes.Ldarg_2);
                            il.EmitAddOffset(_VariablesByteSize);
                            _VariablesByteSize += size;

                            // Load the variable and store it in the loaded address
                            LoadMember(member);
                            il.EmitStoreToAddress(member.MemberType);
                        }
                        else throw new InvalidOperationException($"Invalid captured member of type {member.MemberType}");
                    }

                    il.Emit(OpCodes.Ret);
                });
            }

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
            _MembersLoader(action.Target, ref r0, ref r1);

            return new ShaderDispatchData(resources, _ResourcesCount, variables, _VariablesByteSize);
        }

        public readonly ref struct ShaderDispatchData
        {
            private readonly GraphicsResource[] ResourcesArray;

            private readonly byte[] VariablesArray;

            private readonly int ResourcesCount;

            private readonly int VariablesInt4Size;

            public Span<GraphicsResource> Resources
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => ResourcesArray.AsSpan(0, ResourcesCount);
            }

            public Span<Int4> Variables
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    ref Int4 r = ref Unsafe.As<byte, Int4>(ref VariablesArray[0]);
                    return MemoryMarshal.CreateSpan(ref r, VariablesInt4Size);
                }
            }

            public ShaderDispatchData(GraphicsResource[] resourcesArray, int resourcesCount, byte[] variablesArray, int variablesInt4Size)
            {
                ResourcesArray = resourcesArray;
                VariablesArray = variablesArray;
                ResourcesCount = resourcesCount;
                VariablesInt4Size = variablesInt4Size;
            }

            public void Dispose()
            {
                ArrayPool<GraphicsResource>.Shared.Return(ResourcesArray);
                ArrayPool<byte>.Shared.Return(VariablesArray);
            }
        }
    }
}
