using System;
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

        using ReadWriteBuffer<float> buffer = device.Get().AllocateReadWriteBuffer(data);

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
            buffer[5] = greaterThan.Y ? 1 : 0;
            buffer[6] = greaterThan.Z ? 1 : 0;
            buffer[7] = greaterThan.W ? 1 : 0;
        }
    }

    // See: https://github.com/Sergio0694/ComputeSharp/issues/259
    [CombinatorialTestMethod]
    [AllDevices]
    public void ConstantsInShaderConstantFields(Device device)
    {
        using ReadWriteBuffer<float> buffer = device.Get().AllocateReadWriteBuffer<float>(16);

        device.Get().For(1, new ConstantsInShaderConstantFieldsShader(buffer));

        float[] results = buffer.ToArray();

        Assert.AreEqual(results[0], ConstantsInShaderConstantFieldsShader.rot_angle, 0.0001f);
        Assert.AreEqual(results[1], ConstantsInShaderConstantFieldsShader.rot_angle2, 0.0001f);
        Assert.AreEqual(results[2], (float)Math.Cos(ConstantsInShaderConstantFieldsShader.rot_angle), 0.0001f);
        Assert.AreEqual(results[3], -(float)Math.Sin((float)(137.2 / 180.0 * Constants.PI2)), 0.0001f);
        Assert.AreEqual(results[4], (float)Math.Cos(Math.Sin(ConstantsInShaderConstantFieldsShader.sin)), 0.0001f);
        Assert.AreEqual(results[5], (float)(Math.Sin(ConstantsInShaderConstantFieldsShader.sin) + Math.Cos(Math.Sin(ConstantsInShaderConstantFieldsShader.sin))), 0.0001f);
        Assert.AreEqual(results[6], ConstantsInShaderConstantFieldsShader.exponentLowercase, 0.0001f);
        Assert.AreEqual(results[7], ConstantsInShaderConstantFieldsShader.exponentUppercase, 0.0001f);
        Assert.AreEqual(results[8], ConstantsInShaderConstantFieldsShader.exponentLowercaseField, 0.0001f);
        Assert.AreEqual(results[9], ConstantsInShaderConstantFieldsShader.exponentUppercaseField, 0.0001f);
    }

    private static class Constants
    {
        public const float PI2 = (float)(Math.PI * 2);
    }

    [AutoConstructor]
    internal readonly partial struct ConstantsInShaderConstantFieldsShader : IComputeShader
    {
        public const float rot_angle = (float)(137.2 / 180.0 * Math.PI);
        public const float rot_angle2 = rot_angle + (float)Math.E * 100;
        public const float sin = 42;
        public const float exponentLowercase = 1234567e-4f;
        public const float exponentUppercase = 1234567E-4f;

        private static readonly float rot_11 = Hlsl.Cos(rot_angle);
        private static readonly float rot_12 = -Hlsl.Sin((float)(137.2 / 180.0 * Constants.PI2));
        private static readonly float cos = Hlsl.Cos(Hlsl.Sin(sin));
        private static readonly float lerp = Hlsl.Sin(sin) + cos;
        public static readonly float exponentLowercaseField = 1234567e-4f;
        public static readonly float exponentUppercaseField = 1234567E-4f;

        public readonly ReadWriteBuffer<float> buffer;

        public void Execute()
        {
            buffer[0] = rot_angle;
            buffer[1] = rot_angle2;
            buffer[2] = rot_11;
            buffer[3] = rot_12;
            buffer[4] = cos;
            buffer[5] = lerp;
            buffer[6] = exponentLowercase;
            buffer[7] = exponentUppercase;
            buffer[8] = exponentLowercaseField;
            buffer[9] = exponentUppercaseField;
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/278
    [CombinatorialTestMethod]
    [AllDevices]
    public void DoubleConstantsInShaderConstantFields(Device device)
    {
        if (!device.Get().IsDoublePrecisionSupportAvailable())
        {
            Assert.Inconclusive();
        }

        using ReadWriteBuffer<float> buffer1 = device.Get().AllocateReadWriteBuffer<float>(8);
        using ReadWriteBuffer<double> buffer2 = device.Get().AllocateReadWriteBuffer<double>(8);

        device.Get().For(1, new DoubleConstantsInShaderConstantFieldsShader(buffer1, buffer2));

        float[] results1 = buffer1.ToArray();

        Assert.AreEqual(results1[0], DoubleConstantsInShaderConstantFieldsShader.DoubleToFloatConstant, 0.0001f);
        Assert.AreEqual(results1[1], DoubleConstantsInShaderConstantFieldsShader.DoubleToFloatField, 0.0001f);
        Assert.AreEqual(results1[2], DoubleConstants.PI, 0.0001f);
        Assert.AreEqual(results1[3], (float)(1 / 255.0), 0.0001f);
        Assert.AreEqual(results1[4], DoubleConstantsInShaderConstantFieldsShader.floatExponentLowercase, 0.0001f);
        Assert.AreEqual(results1[5], DoubleConstantsInShaderConstantFieldsShader.floatExponentUppercase, 0.0001f);
        Assert.AreEqual(results1[6], DoubleConstantsInShaderConstantFieldsShader.floatExponentLowercaseField, 0.0001f);
        Assert.AreEqual(results1[7], DoubleConstantsInShaderConstantFieldsShader.floatExponentUppercaseField, 0.0001f);

        double[] results2 = buffer2.ToArray();

        Assert.AreEqual(results2[0], DoubleConstantsInShaderConstantFieldsShader.DoubleConstant, 0.0001f);
        Assert.AreEqual(results2[1], DoubleConstantsInShaderConstantFieldsShader.DoubleField, 0.0001f);
        Assert.AreEqual(results2[2], DoubleConstants.PI2, 0.0001f);
        Assert.AreEqual(results2[3], 1 / 255.0, 0.0001f);
        Assert.AreEqual(results2[4], DoubleConstantsInShaderConstantFieldsShader.exponentLowercase, 0.0001f);
        Assert.AreEqual(results2[5], DoubleConstantsInShaderConstantFieldsShader.exponentUppercase, 0.0001f);
        Assert.AreEqual(results2[6], DoubleConstantsInShaderConstantFieldsShader.exponentLowercaseField, 0.0001f);
        Assert.AreEqual(results2[7], DoubleConstantsInShaderConstantFieldsShader.exponentUppercaseField, 0.0001f);
    }

    private static class DoubleConstants
    {
        public const float PI = (float)3.14;
        public const double PI2 = 3.14 * 2;
    }

    [AutoConstructor]
    internal readonly partial struct DoubleConstantsInShaderConstantFieldsShader : IComputeShader
    {
        public const float DoubleToFloatConstant = (float)(1 / 255.0);
        public const double DoubleConstant = 1.0 / 255;
        public const float floatExponentLowercase = (float)1234567e-4;
        public const float floatExponentUppercase = (float)1234567E-4;
        public const double exponentLowercase = 1234567e-4;
        public const double exponentUppercase = 1234567E-4;

        public static readonly float DoubleToFloatField = (float)(1D / 255);
        public static readonly double DoubleField = 1 / 255D;
        public static readonly float floatExponentLowercaseField = (float)1234567e-4;
        public static readonly float floatExponentUppercaseField = (float)1234567E-4;
        public static readonly double exponentLowercaseField = 1234567e-4;
        public static readonly double exponentUppercaseField = 1234567E-4;

        public readonly ReadWriteBuffer<float> buffer1;
        public readonly ReadWriteBuffer<double> buffer2;

        public void Execute()
        {
            const float localFloat = (float)(1 / 255.0);
            const double localDouble = 1.0 / 255;

            buffer1[0] = DoubleToFloatConstant;
            buffer1[1] = DoubleToFloatField;
            buffer1[2] = DoubleConstants.PI;
            buffer1[3] = localFloat;
            buffer1[4] = floatExponentLowercase;
            buffer1[5] = floatExponentUppercase;
            buffer1[6] = floatExponentLowercaseField;
            buffer1[7] = floatExponentUppercaseField;

            buffer2[0] = DoubleConstant;
            buffer2[1] = DoubleField;
            buffer2[2] = DoubleConstants.PI2;
            buffer2[3] = localDouble;
            buffer2[4] = exponentLowercase;
            buffer2[5] = exponentUppercase;
            buffer2[6] = exponentLowercaseField;
            buffer2[7] = exponentUppercaseField;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void InterlockedOperations(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(16);

        device.Get().For(16, new InterlockedOperationsShader(buffer));

        int[] result = buffer.ToArray();

        for (int i = 0; i < result.Length; i++)
        {
            Assert.AreEqual(i, result[i]);
        }
    }

    [AutoConstructor]
    [EmbeddedBytecode(DispatchAxis.X)]
    internal readonly partial struct InterlockedOperationsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            Hlsl.InterlockedAdd(buffer[ThreadIds.X], ThreadIds.X);
        }
    }
}
