using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Core.Helpers
{
    /// <summary>
    /// A <see langword="class"/> with helper methods to work with alignment and padding.
    /// </summary>
    internal static class AlignmentHelper
    {
        /// <summary>
        /// Pads the input size so that its pitch is aligned to a multiple of a specified length.
        /// </summary>
        /// <param name="size">The input size to pad, if needed.</param>
        /// <param name="alignment">The target alignment.</param>
        /// <returns>The padded value relative to the given inputs.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Pad(int size, int alignment)
        {
            return (size + alignment - 1) & ~(alignment - 1);
        }

        /// <summary>
        /// Aligns a given offset to a specified boundary, if a given size increment would have crossed it.
        /// </summary>
        /// <param name="offset">The starting offset to align, if needed.</param>
        /// <param name="size">The size increment to apply to <paramref name="offset"/>.</param>
        /// <param name="alignment">The alignment boundary to calculate the final offset.</param>
        /// <returns>A valid starting offset for the specified alignment.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AlignToBoundary(int offset, int size, int alignment)
        {
            if ((uint)offset / (uint)alignment == (uint)(offset + size - 1) / (uint)alignment)
            {
                return offset;
            }

            return (offset + alignment - 1) & ~(alignment - 1);
        }
    }
}
