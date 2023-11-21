using System;
using System.Linq;
using ComputeSharp;
using ComputeSharp.Sample;

float[] array = Enumerable.Range(1, 100).Select(static i => (float)i).ToArray();

// Create the graphics buffer
using ReadWriteBuffer<float> gpuBuffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer(array);

// Run the shader
GraphicsDevice.GetDefault().For(100, new MultiplyByTwo(gpuBuffer));

// Print the initial matrix
Formatting.PrintMatrix(array, 10, 10, "BEFORE");

// Get the data back
gpuBuffer.CopyTo(array);

// Print the updated matrix
Formatting.PrintMatrix(array, 10, 10, "AFTER");

/// <summary>
/// The sample kernel that multiples all items by two.
/// </summary>
[AutoConstructor]
[ThreadGroupSize(DefaultThreadGroupSizes.X)]
[GeneratedComputeShaderDescriptor]
internal readonly partial struct MultiplyByTwo : IComputeShader
{
    private readonly ReadWriteBuffer<float> buffer;

    /// <inheritdoc/>
    public void Execute()
    {
        this.buffer[ThreadIds.X] *= 2;
    }
}