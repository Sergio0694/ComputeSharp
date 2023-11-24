using ComputeSharp;

float[] numbers = Enumerable.Range(0, 128).Select(i => (float)i).ToArray();

using GraphicsDevice device = GraphicsDevice.GetDefault();
using ReadWriteBuffer<float> buffer = device.AllocateReadWriteBuffer(numbers);

device.For(buffer.Length, new ApplyFunctionShader(buffer));

foreach (ref float number in numbers.AsSpan())
{
    number = ApplyFunctionShader.AddHalf(number);
}

float[] result = buffer.ToArray();

if (!numbers.SequenceEqual(result))
{
    throw new Exception("The processed buffer does not match the expected results.");
}

Console.WriteLine("Test passed successfully!");

[ThreadGroupSize(DefaultThreadGroupSizes.X)]
[GeneratedComputeShaderDescriptor]
public readonly partial struct ApplyFunctionShader(ReadWriteBuffer<float> sourceTexture) : IComputeShader
{
    public static float AddHalf(float input)
    {
        return input + 0.5f;
    }

    public void Execute()
    {
        sourceTexture[ThreadIds.X] = AddHalf(sourceTexture[ThreadIds.X]);
    }
}