namespace ComputeSharp.Sample.FSharp.Shaders;

/// <summary>
/// A sample shader thaat just multiplies items in a buffer by two.
/// </summary>
[AutoConstructor]
[ThreadGroupSize(DispatchAxis.X)]
[GeneratedComputeShaderDescriptor]
public readonly partial struct MultiplyByTwo : IComputeShader
{
    private readonly ReadWriteBuffer<float> buffer;

    /// <inheritdoc/>
    public void Execute()
    {
        this.buffer[ThreadIds.X] *= 2;
    }
}