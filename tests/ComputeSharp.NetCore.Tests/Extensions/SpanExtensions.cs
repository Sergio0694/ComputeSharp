using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace ComputeSharp.NetCore.Tests.Extensions
{
    /// <summary>
    /// An helper <see langword="class"/> with methods to process fixed-size buffers
    /// </summary>
    public static class SpanExtensions
    {
        /// <summary>
        /// Fills an input <see cref="Span{T}"/> instance with random <see langword="float"/> values
        /// </summary>
        /// <param name="span">The target <see cref="Span{T}"/> to fill</param>
        public static void FillRandom(this Span<float> span)
        {
            if (span.Length == 0) return;

            var l = span.Length;
            ref var rspan = ref span.GetPinnableReference();

            for (var i = 0; i < l; i++)
                Unsafe.Add(ref rspan, i) = (float)ConcurrentRandom.Instance.NextDouble();
        }

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
