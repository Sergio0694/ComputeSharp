namespace DirectX12GameEngine.Shaders.Numerics
{
    /// <summary>
    /// A <see langword="struct"/> that indicates the ids of a given GPU thread running a compute shader
    /// </summary>
    public readonly struct ThreadIds
    {
        /// <summary>
        /// The X id of the current thread
        /// </summary>
        public readonly uint X;

        /// <summary>
        /// The Y id of the current thread
        /// </summary>
        public readonly uint Y;

        /// <summary>
        /// The Z id of the current thread
        /// </summary>
        public readonly uint Z;
    }
}
