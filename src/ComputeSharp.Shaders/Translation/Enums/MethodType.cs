namespace ComputeSharp.Shaders.Translation.Enums
{
    /// <summary>
    /// An <see langword="enum"/> that indicates a type of method to decompile
    /// </summary>
    internal enum MethodType
    {
        /// <summary>
        /// The instance <see cref="IComputeShader.Execute"/> method on some shader type
        /// </summary>
        Execute,

        /// <summary>
        /// A standalone, static method
        /// </summary>
        Static
    }
}
