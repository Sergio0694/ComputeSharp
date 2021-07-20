using ComputeSharp.__Internals;

#pragma warning disable CS0618

namespace ComputeSharp
{
    /// <summary>
    /// An <see langword="interface"/> representing a compute shader.
    /// </summary>
    public interface IComputeShader : IShader
    {
        /// <summary>
        /// Executes the current compute shader.
        /// </summary>
        void Execute();
    }
}
