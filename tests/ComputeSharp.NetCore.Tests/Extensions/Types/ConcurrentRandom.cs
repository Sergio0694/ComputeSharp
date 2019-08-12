using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
    /// <summary>
    /// A thread-safe <see cref="Random"/> wrapper class
    /// </summary>
    public static class ConcurrentRandom
    {
        // Shared seed
        private static int _Seed = Environment.TickCount;

        /// <summary>
        /// The <see cref="ThreadLocal{T}"/> object that provides the <see cref="Random"/> instances
        /// </summary>
        private static readonly ThreadLocal<Random> _Instance = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _Seed)));

        /// <summary>
        /// Gets a thread-safe <see cref="Random"/> instance
        /// </summary>
        public static Random Instance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _Instance.Value;
        }
    }
}