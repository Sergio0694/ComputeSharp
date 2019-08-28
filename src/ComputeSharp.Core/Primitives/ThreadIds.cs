using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that indicates the ids of a given GPU thread running a compute shader
    /// </summary>
    public readonly struct ThreadIds
    {
        /// <summary>
        /// Gets the X id of the current thread
        /// </summary>
        public int X => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(X)}");

        /// <summary>
        /// Gets the Y id of the current thread
        /// </summary>
        public int Y => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(Y)}");

        /// <summary>
        /// Gets the Z id of the current thread
        /// </summary>
        public int Z => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(Z)}");
    }
}
