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

[AutoConstructor]
[ThreadGroupSize(DefaultThreadGroupSizes.X)]
[GeneratedComputeShaderDescriptor]
public readonly partial struct ApplyFunctionShader : IComputeShader
{
    private readonly ReadWriteBuffer<float> sourceTexture;

    public static float AddHalf(float input)
    {
        return input + 0.5f;
    }

    public void Execute()
    {
        this.sourceTexture[ThreadIds.X] = AddHalf(this.sourceTexture[ThreadIds.X]);
    }
}