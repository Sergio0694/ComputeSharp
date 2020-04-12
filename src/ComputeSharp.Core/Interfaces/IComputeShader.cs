namespace ComputeSharp
{
    /// <summary>
    /// An <see langword="interface"/> representing a compute shader.
    /// </summary>
    public interface IComputeShader
    {
        /// <summary>
        /// Executes the current compute shader.
        /// </summary>
        /// <param name="ids">The <see cref="ThreadIds"/> value with the current execution indices.</param>
        void Execute(ThreadIds ids);
    }
}
