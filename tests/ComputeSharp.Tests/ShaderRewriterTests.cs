using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("ShaderRewriter")]
public partial class ShaderRewriterTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public void NanAndInfinite(Device device)
    {
        if (!device.Get().IsDoublePrecisionSupportAvailable())
        {
            Assert.Inconclusive();
        }

        using ReadWriteBuffer<float> buffer1 = device.Get().AllocateReadWriteBuffer<float>(16);
        using ReadWriteBuffer<double> buffer2 = device.Get().AllocateReadWriteBuffer<double>(16);

        device.Get().For(1, new NanAndInfiniteShader(buffer1, buffer2));

        float[] results1 = buffer1.ToArray();
        double[] results2 = buffer2.ToArray();

        Assert.IsTrue(float.IsNaN(results1[0]));
        Assert.IsTrue(float.IsPositiveInfinity(results1[1]));
        Assert.IsTrue(float.IsNegativeInfinity(results1[2]));
        Assert.AreEqual(results1[3], 1);
        Assert.AreEqual(results1[4], 1);
        Assert.AreEqual(results1[5], 1);
        Assert.IsTrue(float.IsNaN(results1[6]));
        Assert.IsTrue(float.IsPositiveInfinity(results1[7]));
        Assert.IsTrue(float.IsNegativeInfinity(results1[8]));
        Assert.AreEqual(results1[9], 1);
        Assert.AreEqual(results1[10], 1);
        Assert.AreEqual(results1[11], 1);

        Assert.IsTrue(double.IsNaN(results2[0]));
        Assert.IsTrue(double.IsPositiveInfinity(results2[1]));
        Assert.IsTrue(double.IsNegativeInfinity(results2[2]));
        Assert.IsTrue(double.IsNaN(results2[3]));
        Assert.IsTrue(double.IsPositiveInfinity(results2[4]));
        Assert.IsTrue(double.IsNegativeInfinity(results2[5]));
    }

    [AutoConstructor]
    internal readonly partial struct NanAndInfiniteShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer1;
        public readonly ReadWriteBuffer<double> buffer2;

        static readonly float float1 = float.NaN;
        static readonly float float2 = float.PositiveInfinity;
        static readonly float float3 = float.NegativeInfinity;
        static readonly double double1 = double.NaN;
        static readonly double double2 = double.PositiveInfinity;
        static readonly double double3 = double.NegativeInfinity;

        public void Execute()
        {
            buffer1[0] = float.NaN;
            buffer1[1] = float.PositiveInfinity;
            buffer1[2] = float.NegativeInfinity;
            buffer1[3] = Hlsl.IsNaN(buffer1[0]) ? 1 : 0;
            buffer1[4] = Hlsl.IsInfinite(buffer1[1]) ? 1 : 0;
            buffer1[5] = Hlsl.IsInfinite(buffer1[2]) ? 1 : 0;
            buffer1[6] = float1;
            buffer1[7] = float2;
            buffer1[8] = float3;
            buffer1[9] = Hlsl.IsNaN(buffer1[6]) ? 1 : 0;
            buffer1[10] = Hlsl.IsInfinite(buffer1[7]) ? 1 : 0;
            buffer1[11] = Hlsl.IsInfinite(buffer1[8]) ? 1 : 0;

            buffer2[0] = double.NaN;
            buffer2[1] = double.PositiveInfinity;
            buffer2[2] = double.NegativeInfinity;
            buffer2[3] = double1;
            buffer2[4] = double2;
            buffer2[5] = double3;
        }
    }

    // See: https://github.com/Sergio0694/ComputeSharp/issues/233
    [CombinatorialTestMethod]
    [AllDevices]
    public void CustomHlslOperators(Device device)
    {
        float[] data = { 1, 6, 7, 3, 5, 2, 8, 4 };

        using ReadWriteBuffer<float> buffer = device.Get().AllocateReadWriteBuffer<float>(data);

        device.Get().For(1, new ModAndComparisonOperatorsShader(buffer));

        float[] results = buffer.ToArray();

        Assert.AreEqual(results[0], 1);
        Assert.AreEqual(results[1], 0);
        Assert.AreEqual(results[2], 7);
        Assert.AreEqual(results[3], 3);
        Assert.AreEqual(results[4], 0);
        Assert.AreEqual(results[5], 1);
        Assert.AreEqual(results[6], 0);
        Assert.AreEqual(results[7], 0);
    }

    [AutoConstructor]
    internal readonly partial struct ModAndComparisonOperatorsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer;

        public void Execute()
        {
            float4 a = new(buffer[0], buffer[1], buffer[2], buffer[3]);
            int4 b = (int4)new float4(buffer[4], buffer[5], buffer[6], buffer[7]);
            float4 mod = a % b;
            bool4 greaterThan = a > b;

            buffer[0] = mod.X;
            buffer[1] = mod.Y;
            buffer[2] = mod.Z;
            buffer[3] = mod.W;
            buffer[4] = greaterThan.X ? 1 : 0;
            buffer[5] = greaterThan.Y ? 1 : 0;;
            buffer[6] = greaterThan.Z ? 1 : 0;;
            buffer[7] = greaterThan.W ? 1 : 0; ;
        }
    }
}
