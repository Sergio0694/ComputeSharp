namespace ComputeSharp.Sample.FSharp.Shaders
{
    /// <summary>
    /// A sample shader thaat just multiplies items in a buffer by two.
    /// </summary>
    [AutoConstructor]
    public readonly partial struct MultiplyByTwo : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer;

        /// <inheritdoc/>
        public void Execute()
        {
            buffer[ThreadIds.X] *= 2;
        }
    }
}
