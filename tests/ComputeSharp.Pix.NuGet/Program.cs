using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using ComputeSharp;

[assembly: SupportedOSPlatform("windows6.2")]

float[] array = Enumerable.Range(1, 100).Select(static i => (float)i).ToArray();

// Create the graphics buffer
using ReadWriteBuffer<float> gpuBuffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer(array);

// Run the shader
using (ComputeContext context = GraphicsDevice.GetDefault().CreateComputeContext())
{
    context.Log("Dispatching shader");
    context.BeginEvent("Dispatching event");
    context.For(100, new MultiplyByTwo(gpuBuffer));
    context.EndEvent();
}

// Get the data back
float[] result = gpuBuffer.ToArray();

// Validate results
for (int i = 0; i < array.Length; i++)
{
    Trace.Assert(result[i] == array[i] * 2);
}

/// <summary>
/// A sample kernel that is precompiled.
/// </summary>
[EmbeddedBytecode(DispatchAxis.X)]
[GeneratedComputeShaderDescriptor]
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