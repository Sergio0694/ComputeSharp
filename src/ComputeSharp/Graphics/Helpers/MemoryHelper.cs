using System;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A <see langword="class"/> with some helper methods to copy data between memory areas.
    /// </summary>
    internal static class MemoryHelper
    {
        /// <summary>
        /// Copies the content of a source memory area to a target one, accounting for padding.
        /// The destination memory area has padding for each element, while the source does not.
        /// </summary>
        /// <param name="source">The source memory area to read from.</param>
        /// <param name="destination">The pointer for the destination memory area.</param>
        /// <param name="offset">The destination offset to start writing data to.</param>
        /// <param name="length">The number of items to copy.</param>
        /// <param name="elementPitchInBytes">The padded size of each element.</param>
        /// <param name="isPaddingInSourceToo">Indicates whether or not the source is padded too.</param>
        public static unsafe void Copy<T>(
            void* source,
            void* destination,
            uint offset,
            uint length,
            uint elementPitchInBytes,
            bool isPaddingInSourceToo)
            where T : unmanaged
        {
            destination = (byte*)destination + offset * elementPitchInBytes;

            if (isPaddingInSourceToo)
            {
                for (int i = 0; i < length; i++)
                {
                    *(T*)&((byte*)destination)[i * elementPitchInBytes] = *(T*)&((byte*)source)[i * elementPitchInBytes];
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    *(T*)&((byte*)destination)[i * elementPitchInBytes] = ((T*)source)[i];
                }
            }
        }

        /// <summary>
        /// Copies the content of a source memory area to a target one.
        /// </summary>
        /// <param name="source">The source memory area to read from.</param>
        /// <param name="offset">The source offset to start reading data from.</param>
        /// <param name="length">The number of items to copy.</param>
        /// <param name="elementSizeInBytes">The size in bytes of each item to copy.</param>
        /// <param name="destination">The pointer for the destination memory area.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy(
            void* source,
            uint offset,
            uint length,
            uint elementSizeInBytes,
            void* destination)
        {
            Buffer.MemoryCopy(
                (byte*)source + offset * elementSizeInBytes,
                destination,
                ulong.MaxValue,
                elementSizeInBytes * length);
        }

        /// <summary>
        /// Copies the content of a source memory area to a target one, accounting for padding.
        /// The source memory area has padding for each element, while the destination does not.
        /// </summary>
        /// <param name="source">The source memory area to read from.</param>
        /// <param name="offset">The source offset to start reading data from.</param>
        /// <param name="length">The number of items to copy.</param>
        /// <param name="elementPitchInBytes">The padded size of each element.</param>
        /// <param name="destination">The pointer for the destination memory area.</param>
        public static unsafe void Copy<T>(
            void* source,
            uint offset,
            uint length,
            uint elementPitchInBytes,
            void* destination)
            where T : unmanaged
        {
            source = (byte*)source + offset * elementPitchInBytes;

            for (int i = 0; i < length; i++)
            {
                ((T*)destination)[i] = *(T*)&((byte*)source)[i * elementPitchInBytes];
            }
        }

        /// <summary>
        /// Copies the content of a source memory area to the 2D area pointed by an input pointer.
        /// The destination memory area has padding in each row, while the source does not.
        /// </summary>
        /// <param name="source">The source pointer to read from.</param>
        /// <param name="destination">The pointer that indicates the 2D memory area to write to.</param>
        /// <param name="height">The height of the 2D memory area to write to.</param>
        /// <param name="widthInBytes">The width of the memory area in bytes.</param>
        /// <param name="pitchInBytes">The pitch (padded width) of the memory area in bytes.</param>
        public static unsafe void Copy(
            void* source,
            void* destination,
            uint height,
            ulong widthInBytes,
            ulong pitchInBytes)
        {
            if (widthInBytes == pitchInBytes)
            {
                Buffer.MemoryCopy(source, destination, ulong.MaxValue, widthInBytes * height);
            }
            else
            {
                for (int i = 0; i < height; i++)
                {
                    Buffer.MemoryCopy(
                        (byte*)source + (uint)i * widthInBytes,
                        (byte*)destination + (uint)i * pitchInBytes,
                        ulong.MaxValue,
                        widthInBytes);
                }
            }
        }

        /// <summary>
        /// Copies a 2D memory area pointed by a pointer value to a target memory area.
        /// The source memory area has padding in each row, while the target does not.
        /// </summary>
        /// <param name="source">The pointer that indicates the memory area to read from.</param>
        /// <param name="height">The height of the 2D memory area to read.</param>
        /// <param name="widthInBytes">The width of the memory area in bytes.</param>
        /// <param name="pitchInBytes">The pitch (padded width) of the memory area in bytes.</param>
        /// <param name="destination">The destination pointer to write to.</param>
        public static unsafe void Copy(
            void* source,
            uint height,
            ulong widthInBytes,
            ulong pitchInBytes,
            void* destination)
        {
            if (widthInBytes == pitchInBytes)
            {
                Buffer.MemoryCopy(source, destination, ulong.MaxValue, widthInBytes * height);
            }
            else
            {
                for (int i = 0; i < height; i++)
                {
                    Buffer.MemoryCopy(
                        (byte*)source + (uint)i * pitchInBytes,
                        (byte*)destination + (uint)i * widthInBytes,
                        ulong.MaxValue,
                        widthInBytes);
                }
            }
        }

        /// <summary>
        /// Copies the content of a source memory area to the 3D area pointed by an input pointer.
        /// The destination memory area has padding in each row, while the source does not.
        /// </summary>
        /// <param name="source">The source memory area to read from.</param>
        /// <param name="destination">The pointer that indicates the 3D memory area to write to.</param>
        /// <param name="height">The height of the 3D memory area to write to.</param>
        /// <param name="depth">The depth of the 3D memory area to write to.</param>
        /// <param name="widthInBytes">The width of the memory area in bytes.</param>
        /// <param name="pitchInBytes">The pitch (padded width) of the memory area in bytes.</param>
        /// <param name="sliceInBytes">The size in bytes of each 2D slice within the target 3D memory area.</param>
        public static unsafe void Copy(
            void* source,
            void* destination,
            uint height,
            uint depth,
            ulong widthInBytes,
            ulong pitchInBytes,
            ulong sliceInBytes)
        {
            if (widthInBytes == pitchInBytes)
            {
                Buffer.MemoryCopy(source, destination, ulong.MaxValue, widthInBytes * height * depth);
            }
            else
            {
                for (int z = 0; z < depth; z++)
                {
                    ulong
                        sourceZOffset = (uint)z * widthInBytes * height,
                        destinationZOffset = (uint)z * sliceInBytes;

                    for (int i = 0; i < height; i++)
                    {
                        Buffer.MemoryCopy(
                            (byte*)source + sourceZOffset + (uint)i * widthInBytes,
                            (byte*)destination + destinationZOffset + (uint)i * pitchInBytes,
                            ulong.MaxValue,
                            widthInBytes);
                    }
                }
            }
        }

        /// <summary>
        /// Copies a 3D memory area pointed by a pointer value to a target memory area.
        /// The source memory area has padding in each row, while the target does not.
        /// </summary>
        /// <param name="source">The pointer that indicates the memory area to read from.</param>
        /// <param name="height">The height of the 3D memory area to read.</param>
        /// <param name="depth">The depth of the 3D memory area to read.</param>
        /// <param name="widthInBytes">The width of the memory area in bytes.</param>
        /// <param name="pitchInBytes">The pitch (padded width) of the memory area in bytes.</param>
        /// <param name="sliceInBytes">The size in bytes of each 2D slice within the source 3D memory area.</param>
        /// <param name="destination">The destination pointer to write.</param>
        public static unsafe void Copy(
            void* source,
            uint height,
            uint depth,
            ulong widthInBytes,
            ulong pitchInBytes,
            ulong sliceInBytes,
            void* destination)
        {
            if (widthInBytes == pitchInBytes)
            {
                Buffer.MemoryCopy(source, destination, ulong.MaxValue, widthInBytes * height * depth);
            }
            else
            {
                for (int z = 0; z < depth; z++)
                {
                    ulong
                        sourceZOffset = (uint)z * sliceInBytes,
                        destinationZOffset = (uint)z * widthInBytes * height;

                    for (int i = 0; i < height; i++)
                    {
                        Buffer.MemoryCopy(
                            (byte*)source + sourceZOffset + (uint)i * pitchInBytes,
                            (byte*)destination + destinationZOffset + (uint)i * widthInBytes,
                            ulong.MaxValue,
                            widthInBytes);
                    }
                }
            }
        }
    }
}
