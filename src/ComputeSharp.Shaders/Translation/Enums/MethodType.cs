namespace ComputeSharp.Shaders.Translation.Enums
{
    /// <summary>
    /// An <see langword="enum"/> that indicates a type of method to decompile
    /// </summary>
    internal enum MethodType
    {
        /// <summary>
        /// An instance method that belongs to a closure class
        /// </summary>
        Closure,

        /// <summary>
        /// A standalone, static method
        /// </summary>
        Static
    }
}
