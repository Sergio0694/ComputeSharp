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

    // See https://github.com/Sergio0694/ComputeSharp/issues/233
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

    // See https://github.com/Sergio0694/ComputeSharp/issues/361
    [CombinatorialTestMethod]
    [AllDevices]
    public void BitwiseHlslOperators(Device device)
    {
        int[] data = { 186456131, 215486738, unchecked((int)416439712738), unchecked((int)1437124371243), 0, 0, 0, 0 };

        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer(data);

        device.Get().For(1, new BitwiseOperatorsShader(buffer));

        int[] results = buffer.ToArray();

        int2 a = new(data[0], data[1]);
        uint2 b = new((uint)data[2], (uint)data[3]);

        int2 c = new(~a.X, ~a.Y);
        int2 d = new(a.X & (int)b.X, a.Y & (int)b.Y);
        int2 e = new(a.X | (int)b.X, a.Y | (int)b.Y);
        int2 f = new(a.X ^ (int)b.X, a.Y ^ (int)b.Y);

        Assert.AreEqual(results[0], c.X);
        Assert.AreEqual(results[1], c.Y);
        Assert.AreEqual(results[2], d.X);
        Assert.AreEqual(results[3], d.Y);
        Assert.AreEqual(results[4], e.X);
        Assert.AreEqual(results[5], e.Y);
        Assert.AreEqual(results[6], f.X);
        Assert.AreEqual(results[7], f.Y);
    }

    [AutoConstructor]
    internal readonly partial struct BitwiseOperatorsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            int2 a = new(buffer[0], buffer[1]);
            uint2 b = new((uint)buffer[2], (uint)buffer[3]);

            int2 c = ~a;
            int2 d = a & b;
            int2 e = a | b;
            int2 f = a ^ b;

#if NEEDS_CSHARP_11
            // TODO: add bit shifting tests
#endif

            buffer[0] = c.X;
            buffer[1] = c.Y;
            buffer[2] = d.X;
            buffer[3] = d.Y;
            buffer[4] = e.X;
            buffer[5] = e.Y;
            buffer[6] = f.X;
            buffer[7] = f.Y;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void BooleanHlslOperators(Device device)
    {
        int[] data = { 1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer(data);

        device.Get().For(1, new BooleanOperatorsShader(buffer));

        int[] results = buffer.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { 1, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0, 1 },
            actual: results);
    }

    [AutoConstructor]
    internal readonly partial struct BooleanOperatorsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            bool4 a = new int4(buffer[0], buffer[1], buffer[2], buffer[3]) == 1;
            bool4 b = new int4(buffer[4], buffer[5], buffer[6], buffer[7]) == 1;

            bool4 c = a & b;
            bool4 d = a | b;
            bool4 e = a ^ b;

            buffer[8] = c.X ? 1 : 0;
            buffer[9] = c.Y ? 1 : 0;
            buffer[10] = c.Z ? 1 : 0;
            buffer[11] = c.W ? 1 : 0;
            buffer[12] = d.X ? 1 : 0;
            buffer[13] = d.Y ? 1 : 0;
            buffer[14] = d.Z ? 1 : 0;
            buffer[15] = d.W ? 1 : 0;
            buffer[16] = e.X ? 1 : 0;
            buffer[17] = e.Y ? 1 : 0;
            buffer[18] = e.Z ? 1 : 0;
            buffer[19] = e.W ? 1 : 0;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void ToBooleanConversionHlslIntrinsics(Device device)
    {
        float[] data1 = new[] { 3.14f, 0, 1.44f, 0, 0, 0.4445f };
        int[] data2 = new[] { 3, 0, 1, 0, 0, 156 };

        using ReadOnlyBuffer<float> buffer1 = device.Get().AllocateReadOnlyBuffer(data1);
        using ReadOnlyBuffer<int> buffer2 = device.Get().AllocateReadOnlyBuffer(data2);
        using ReadWriteBuffer<int> buffer3 = device.Get().AllocateReadWriteBuffer<int>(18, AllocationMode.Clear);

        device.Get().For(1, new ToBooleanConversionIntrinsicsShader(buffer1, buffer2, buffer3));

        int[] results = buffer3.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1 },
            actual: results);
    }

    [AutoConstructor]
    internal readonly partial struct ToBooleanConversionIntrinsicsShader : IComputeShader
    {
        public readonly ReadOnlyBuffer<float> buffer1;
        public readonly ReadOnlyBuffer<int> buffer2;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            float f1 = buffer1[0];
            float f2 = buffer1[1];
            float4 f4 = new(buffer1[2], buffer1[3], buffer1[4], buffer1[5]);

            int i1 = buffer2[0];
            int i2 = buffer2[1];
            int4 i4 = new(buffer2[2], buffer2[3], buffer2[4], buffer2[5]);

            uint u1 = (uint)buffer2[0];
            uint u2 = (uint)buffer2[1];
            uint4 u4 = new((uint)buffer2[2], (uint)buffer2[3], (uint)buffer2[4], (uint)buffer2[5]);

            result[0] = Hlsl.FloatToBool(f1) ? 1 : 0;
            result[1] = Hlsl.FloatToBool(f2) ? 1 : 0;

            bool4 f4b = Hlsl.FloatToBool(f4);

            result[2] = f4b[0] ? 1 : 0;
            result[3] = f4b[1] ? 1 : 0;
            result[4] = f4b[2] ? 1 : 0;
            result[5] = f4b[3] ? 1 : 0;

            result[6] = Hlsl.IntToBool(i1) ? 1 : 0;
            result[7] = Hlsl.IntToBool(i2) ? 1 : 0;

            bool4 i4b = Hlsl.IntToBool(i4);

            result[8] = i4b[0] ? 1 : 0;
            result[9] = i4b[1] ? 1 : 0;
            result[10] = i4b[2] ? 1 : 0;
            result[11] = i4b[3] ? 1 : 0;

            result[12] = Hlsl.UIntToBool(u1) ? 1 : 0;
            result[13] = Hlsl.UIntToBool(u2) ? 1 : 0;

            bool4 u4b = Hlsl.UIntToBool(u4);

            result[14] = u4b[0] ? 1 : 0;
            result[15] = u4b[1] ? 1 : 0;
            result[16] = u4b[2] ? 1 : 0;
            result[17] = u4b[3] ? 1 : 0;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void FromBooleanConversionHlslIntrinsics(Device device)
    {
        int[] data = new[] { 1, 0, 1, 0, 0, 1 };

        using ReadOnlyBuffer<int> buffer1 = device.Get().AllocateReadOnlyBuffer(data);
        using ReadWriteBuffer<float> buffer2 = device.Get().AllocateReadWriteBuffer<float>(6, AllocationMode.Clear);
        using ReadWriteBuffer<int> buffer3 = device.Get().AllocateReadWriteBuffer<int>(6, AllocationMode.Clear);
        using ReadWriteBuffer<uint> buffer4 = device.Get().AllocateReadWriteBuffer<uint>(6, AllocationMode.Clear);

        device.Get().For(1, new FromBooleanConversionIntrinsicsShader(buffer1, buffer2, buffer3, buffer4));

        float[] results1 = buffer2.ToArray();
        int[] results2 = buffer3.ToArray();
        uint[] results3 = buffer4.ToArray();

        CollectionAssert.AreEqual(
            expected: new float[] { 1, 0, 1, 0, 0, 1 },
            actual: results1);

        CollectionAssert.AreEqual(
            expected: new int[] { 1, 0, 1, 0, 0, 1 },
            actual: results2);

        CollectionAssert.AreEqual(
            expected: new uint[] { 1, 0, 1, 0, 0, 1 },
            actual: results3);
    }

    [AutoConstructor]
    internal readonly partial struct FromBooleanConversionIntrinsicsShader : IComputeShader
    {
        public readonly ReadOnlyBuffer<int> source;
        public readonly ReadWriteBuffer<float> destination1;
        public readonly ReadWriteBuffer<int> destination2;
        public readonly ReadWriteBuffer<uint> destination3;

        public void Execute()
        {
            bool b1 = source[0] != 0;
            bool b2 = source[1] != 0;
            bool4 b4 = new(source[2] != 0, source[3] != 0, source[4] != 0, source[5] != 0);

            bool b11 = source[0] != 0;
            bool b22 = source[1] != 0;
            bool4 b44 = new(source[2] != 0, source[3] != 0, source[4] != 0, source[5] != 0);

            bool b111 = source[0] != 0;
            bool b222 = source[1] != 0;
            bool4 b444 = new(source[2] != 0, source[3] != 0, source[4] != 0, source[5] != 0);

            destination1[0] = Hlsl.BoolToFloat(b1);
            destination1[1] = Hlsl.BoolToFloat(b2);

            float4 f4 = Hlsl.BoolToFloat(b4);

            destination1[2] = f4.X;
            destination1[3] = f4.Y;
            destination1[4] = f4.Z;
            destination1[5] = f4.W;

            destination2[0] = Hlsl.BoolToInt(b11);
            destination2[1] = Hlsl.BoolToInt(b22);

            int4 i4 = Hlsl.BoolToInt(b44);

            destination2[2] = i4.X;
            destination2[3] = i4.Y;
            destination2[4] = i4.Z;
            destination2[5] = i4.W;

            destination3[0] = Hlsl.BoolToUInt(b111);
            destination3[1] = Hlsl.BoolToUInt(b222);

            uint4 u4 = Hlsl.BoolToUInt(b444);

            destination3[2] = u4.X;
            destination3[3] = u4.Y;
            destination3[4] = u4.Z;
            destination3[5] = u4.W;
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/259
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
            Assert.AreEqual(i * 2, result[i]);
        }
    }

    [AutoConstructor]
    [EmbeddedBytecode(DispatchAxis.X)]
    internal readonly partial struct InterlockedOperationsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            // InterlockedExchange 0, which effectively just writes 0 to the target.
            // Then InterlockedAdd to increment the value to ThreadIds.X. Finally,
            // InterlockedCompareExchange with the expected value, setting twice as much.
            // The result each value in the buffer should have after this is ThreadIds.X * 2.
            Hlsl.InterlockedExchange(ref buffer[ThreadIds.X], 0, out _);
            Hlsl.InterlockedAdd(ref buffer[ThreadIds.X], ThreadIds.X);
            Hlsl.InterlockedCompareExchange(ref buffer[ThreadIds.X], ThreadIds.X, ThreadIds.X * 2, out _);
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/156
    [CombinatorialTestMethod]
    [AllDevices]
    public void GlslStyleMulOperators(Device device)
    {
        float[] data1 = { 0.727829933f, 0.6413954f, 0.9373726f, 0.7044427f, 0.46349f, 0.8098116f, 0.604649544f, 0.309247822f, 0.470999569f, 0.7374923f, 0.6993038f, 0.518516064f, 0.44598946f };
        int[] data2 = { 287, 295, 953, 465, 1011, 308, 982, 186, 323, 325, 156, 454, 580 };

        using ReadOnlyBuffer<float> buffer1 = device.Get().AllocateReadOnlyBuffer(data1);
        using ReadOnlyBuffer<int> buffer2 = device.Get().AllocateReadOnlyBuffer(data2);
        using ReadWriteBuffer<float> buffer3 = device.Get().AllocateReadWriteBuffer<float>(22, AllocationMode.Clear);
        using ReadWriteBuffer<int> buffer4 = device.Get().AllocateReadWriteBuffer<int>(22, AllocationMode.Clear);

        device.Get().For(1, new GlslStyleMulOperatorsShader(buffer1, buffer2, buffer3, buffer4));

        float[] results1 = buffer3.ToArray();
        int[] results2 = buffer4.ToArray();

        CollectionAssert.AreEqual(
            expected: results1.AsSpan(11).ToArray(),
            actual: results1.AsSpan(0, 11).ToArray());

        CollectionAssert.AreEqual(
            expected: results2.AsSpan(11).ToArray(),
            actual: results2.AsSpan(0, 11).ToArray());
    }

    [AutoConstructor]
    internal readonly partial struct GlslStyleMulOperatorsShader : IComputeShader
    {
        public readonly ReadOnlyBuffer<float> source1;
        public readonly ReadOnlyBuffer<int> source2;
        public readonly ReadWriteBuffer<float> destination1;
        public readonly ReadWriteBuffer<int> destination2;

        public void Execute()
        {
            float2 f2 = new(source1[0], source1[1]);
            float f = source1[2];
            float2x2 f22 = new(source1[3], source1[4], source1[5], source1[6]);
            float2x3 f23 = new(source1[7], source1[8], source1[9], source1[10], source1[11], source1[12]);

            float2 rf1 = f2 * f;
            float2 rf2 = f * f2;
            float2 rf3 = f2 * f22;
            float3 rf4 = f2 * f23;

            float2 f2_copy1 = f2;

            f2_copy1 *= f22;

            float2 rf5 = new(f2.X * f, f2.Y * f);
            float2 rf6 = Hlsl.Mul(f, f2);
            float2 rf7 = Hlsl.Mul(f2, f22);
            float3 rf8 = Hlsl.Mul(f2, f23);

            var f2_copy2 = f2;

            f2_copy2 = Hlsl.Mul(f2_copy2, f22);

            destination1[0] = rf1.X;
            destination1[1] = rf1.Y;
            destination1[2] = rf2.X;
            destination1[3] = rf2.Y;
            destination1[4] = rf3.X;
            destination1[5] = rf3.Y;
            destination1[6] = rf4.X;
            destination1[7] = rf4.Y;
            destination1[8] = rf4.Z;
            destination1[9] = f2_copy1.X;
            destination1[10] = f2_copy1.Y;

            destination1[11] = rf5.X;
            destination1[12] = rf5.Y;
            destination1[13] = rf6.X;
            destination1[14] = rf6.Y;
            destination1[15] = rf7.X;
            destination1[16] = rf7.Y;
            destination1[17] = rf8.X;
            destination1[18] = rf8.Y;
            destination1[19] = rf8.Z;
            destination1[20] = f2_copy2.X;
            destination1[21] = f2_copy2.Y;

            int2 i2 = new(source2[0], source2[1]);
            int i = source2[2];
            int2x2 i22 = new(source2[3], source2[4], source2[5], source2[6]);
            int2x3 i23 = new(source2[7], source2[8], source2[9], source2[10], source2[11], source2[12]);

            int2 ri1 = i2 * i;
            int2 ri2 = i * i2;
            int2 ri3 = i2 * i22;
            int3 ri4 = i2 * i23;

            int2 i2_copy1 = i2;

            i2_copy1 *= i22;

            int2 ri5 = new(i2.X * i, i2.Y * i);
            int2 ri6 = Hlsl.Mul(i, i2);
            int2 ri7 = Hlsl.Mul(i2, i22);
            int3 ri8 = Hlsl.Mul(i2, i23);

            var i2_copy2 = i2;

            i2_copy2 = Hlsl.Mul(i2_copy2, i22);

            destination2[0] = ri1.X;
            destination2[1] = ri1.Y;
            destination2[2] = ri2.X;
            destination2[3] = ri2.Y;
            destination2[4] = ri3.X;
            destination2[5] = ri3.Y;
            destination2[6] = ri4.X;
            destination2[7] = ri4.Y;
            destination2[8] = ri4.Z;
            destination2[9] = i2_copy1.X;
            destination2[10] = i2_copy1.Y;

            destination2[11] = ri5.X;
            destination2[12] = ri5.Y;
            destination2[13] = ri6.X;
            destination2[14] = ri6.Y;
            destination2[15] = ri7.X;
            destination2[16] = ri7.Y;
            destination2[17] = ri8.X;
            destination2[18] = ri8.Y;
            destination2[19] = ri8.Z;
            destination2[20] = i2_copy2.X;
            destination2[21] = i2_copy2.Y;
        }
    }
}
