using System;

namespace ComputeSharp.SourceGenerators.Helpers
{
    /// <summary>
    /// A polyfill type that mirrors some methods from <see cref="System.Runtime.InteropServices.MemoryMarshal"/> on .NET 5.
    /// </summary>
    internal static class MemoryMarshal
    {
        /// <inheritdoc cref="System.Runtime.InteropServices.MemoryMarshal.GetReference{T}(Span{T})"/>
        public static ref T GetReference<T>(Span<T> span)
        {
            return ref System.Runtime.InteropServices.MemoryMarshal.GetReference(span);
        }

        /// <summary>
        /// Creates a new <see cref="Span{T}"/> from a given reference.
        /// </summary>
        /// <typeparam name="T">The type of reference to wrap.</typeparam>
        /// <param name="value">The target reference.</param>
        /// <param name="length">The length of the <see cref="Span{T}"/> to create.</param>
        /// <returns>A new <see cref="Span{T}"/> wrapping <paramref name="value"/>.</returns>
        public static Span<T> CreateSpan<T>(ref T value, int length) => default;
    }
}
