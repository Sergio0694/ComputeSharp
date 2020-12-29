#if NETSTANDARD2_0

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComputeSharp
{
    namespace System
    {
        /// <summary>
        /// A polyfill class for the <see cref="GC"/> type.
        /// </summary>
        internal static class GC
        {
            /// <summary>
            /// Allocates an array while skipping zero-initialization, if possible.
            /// </summary>
            /// <typeparam name="T">Specifies the type of the array element.</typeparam>
            /// <param name="length">Specifies the length of the array.</param>
            /// <param name="pinned">Specifies whether the allocated array must be pinned.</param>
            /// <returns>An array object with uninitialized memory, if possible.</returns>
            public static T[] AllocateUninitializedArray<T>(int length, bool pinned = false)
            {
                return new T[length];
            }
        }

        namespace Runtime.InteropServices
        {
            /// <summary>
            /// A polyfill class for the <see cref="MemoryMarshal"/> type.
            /// </summary>
            internal static class MemoryMarshal
            {
                /// <summary>
                /// Returns a reference to the 0th element of array. If the array is empty, returns
                /// a reference to where the 0th element would have been stored. Such a reference
                /// may be used for pinning but must never be dereferenced.
                /// </summary>
                /// <typeparam name="T">The type of the array elements.</typeparam>
                /// <param name="array">The array to analyze.</param>
                /// <returns>Reference to the 0th element in array.</returns>
                public static ref T GetArrayDataReference<T>(T[] array)
                {
                    IntPtr offset = TypeInfo<T>.ArrayDataByteOffset;
                    RawObjectData rawObject = Unsafe.As<RawObjectData>(array)!;
                    ref byte r0 = ref rawObject.Data;
                    ref byte r1 = ref Unsafe.AddByteOffset(ref r0, offset);
                    ref T r2 = ref Unsafe.As<byte, T>(ref r1);

                    return ref r2;
                }

                /// <summary>
                /// A private generic class to preload type info for arbitrary runtime types.
                /// </summary>
                /// <typeparam name="T">The type to load info for.</typeparam>
                private static class TypeInfo<T>
                {
                    /// <summary>
                    /// The byte offset to the first <typeparamref name="T"/> element in a SZ array.
                    /// </summary>
                    public static readonly IntPtr ArrayDataByteOffset = MeasureArrayDataByteOffset();

                    /// <summary>
                    /// Computes the value for <see cref="ArrayDataByteOffset"/>.
                    /// </summary>
                    /// <returns>The value of <see cref="ArrayDataByteOffset"/> for the current runtime.</returns>
                    private static IntPtr MeasureArrayDataByteOffset()
                    {
                        T[] array = new T[1];
                        RawObjectData rawObject = Unsafe.As<RawObjectData>(array)!;
                        ref byte r0 = ref rawObject.Data;
                        ref byte r1 = ref Unsafe.As<T, byte>(ref array[0]);

                        return Unsafe.ByteOffset(ref r0, ref r1);
                    }
                }

                /// <summary>
                /// Mapping type for a raw object.
                /// </summary>
                [StructLayout(LayoutKind.Explicit)]
                private sealed class RawObjectData
                {
                    [FieldOffset(0)]
                    public byte Data;
                }
            }
        }
    }
}

namespace System.Collections.Generic
{
    /// <summary>
    /// Extensions for the <see cref="Queue{T}"/> type.
    /// </summary>
    internal static class QueueExtensions
    {
        /// <summary>
        /// Removes the object at the beginning of the <see cref="Queue{T}"/> instance and copies it to the result parameter.
        /// </summary>
        /// <typeparam name="T">The type of values in the input <see cref="Queue{T}"/>.</typeparam>
        /// <param name="queue">The input <see cref="Queue{T}"/>.</param>
        /// <param name="result">The removed object.</param>
        /// <returns>Whether or not the object was successfully removed.</returns>
        public static bool TryDequeue<T>(this Queue<T> queue, out T? result)
        {
            if (queue.Count > 0)
            {
                result = queue.Dequeue();

                return true;
            }

            result = default;

            return false;
        }
    }
}

#endif
