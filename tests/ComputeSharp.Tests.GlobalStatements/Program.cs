using ComputeSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

float[] numbers = Enumerable.Range(0, 128).Select(i => (float)i).ToArray();

using GraphicsDevice device = GraphicsDevice.GetDefault();
using ReadWriteBuffer<float> buffer = device.AllocateReadWriteBuffer(numbers);

device.For(buffer.Length, new ApplyFunctionShader(buffer, AddHalf));

foreach (ref float number in numbers.AsSpan())
{
    number = AddHalf(number);
}

float[] result = buffer.ToArray();

CollectionAssert.AreEqual(
    expected: numbers,
    actual: result);

Console.WriteLine("Test passed successfully");

// See https://github.com/Sergio0694/ComputeSharp/issues/374
[ShaderMethod]
static float AddHalf(float input) => input + 0.5f;

[AutoConstructor]
public readonly partial struct ApplyFunctionShader : IComputeShader
{
    private readonly ReadWriteBuffer<float> sourceTexture;
    private readonly Func<float, float> func;

    public void Execute()
    {
        this.sourceTexture[ThreadIds.X] = this.func(this.sourceTexture[ThreadIds.X]);
    }
}