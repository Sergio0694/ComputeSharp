using System.Diagnostics;
using System.Linq;
using ComputeSharp;

float[] array = Enumerable.Range(1, 100).Select(static i => (float)i).ToArray();

// Create the graphics buffer
using ReadWriteBuffer<float> gpuBuffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer(array);

// Run the shader
GraphicsDevice.GetDefault().For(100, new MultiplyByTwo(gpuBuffer));

// Get the data back
float[] result = gpuBuffer.ToArray();

// Validate results
for (int i = 0; i < array.Length; i++)
{
    Trace.Assert(result[i] == array[i] * 2);
}

/// <summary>
/// A sample kernel that requires dynamic compilation, as it's not precompiled.
/// </summary>
[AutoConstructor]
internal readonly partial struct MultiplyByTwo : IComputeShader
{
    public readonly ReadWriteBuffer<float> buffer;

    /// <inheritdoc/>
    public void Execute()
    {
        buffer[ThreadIds.X] *= 2;
    }
}
