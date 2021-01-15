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
        void Execute();
    }
}
