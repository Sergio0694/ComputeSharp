using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ComputeSharp.Tests.Extensions
{
    /// <summary>
    /// An helper <see langword="class"/> with methods to process fixed-size buffers
    /// </summary>
    public static class SpanExtensions
    {
        /// <summary>
        /// The maximum threshold to validate two <see langword="float"/> values as being equal
        /// </summary>
        private const float FloatThreshold = 0.00001f;

        /// <summary>
        /// Checks whether or not the two input <see cref="Span{T}"/> instances have the same content
        /// </summary>
        /// <param name="a">The first <see cref="Span{T}"/> to check</param>
        /// <param name="b">The second <see cref="Span{T}"/> to check</param>
        [Pure]
        public static bool ContentEquals(this Span<float> a, Span<float> b)
        {
            if (a.Length != b.Length) return false;
            if (a.Length == 0) return true;

            var l = a.Length;
            ref var ra = ref a.GetPinnableReference();
            ref var rb = ref b.GetPinnableReference();

            for (var i = 0; i < l; i++)
                if (Math.Abs(Unsafe.Add(ref ra, i) - Unsafe.Add(ref rb, i)) > FloatThreshold)
                    return false;

            return true;
        }
    }
}
