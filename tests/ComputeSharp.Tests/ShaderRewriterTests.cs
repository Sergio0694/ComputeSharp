using System;
using System.Collections.Generic;
using System.Linq;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0002, IDE0007, IDE0008, IDE0009

namespace ComputeSharp.Tests;

[TestClass]
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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [RequiresDoublePrecisionSupport]
    [GeneratedComputeShaderDescriptor]
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
        float[] data = [1, 6, 7, 3, 5, 2, 8, 4];

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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
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
        int[] data = [186456131, 215486738, unchecked((int)416439712738), unchecked((int)1437124371243), 0, 0, 0, 0, 0, 0, 0, 0];

        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer(data);

        device.Get().For(1, new BitwiseOperatorsShader(buffer));

        int[] results = buffer.ToArray();

        int2 a = new(data[0], data[1]);
        uint2 b = new((uint)data[2], (uint)data[3]);

        int2 c = new(~a.X, ~a.Y);
        int2 d = new(a.X & (int)b.X, a.Y & (int)b.Y);
        int2 e = new(a.X | (int)b.X, a.Y | (int)b.Y);
        int2 f = new(a.X ^ (int)b.X, a.Y ^ (int)b.Y);

        int2 g = new(a.X >> 5, a.Y >> 12);
        int2 h = new(a.X << 22, a.Y << 17);

        Assert.AreEqual(results[0], c.X);
        Assert.AreEqual(results[1], c.Y);
        Assert.AreEqual(results[2], d.X);
        Assert.AreEqual(results[3], d.Y);
        Assert.AreEqual(results[4], e.X);
        Assert.AreEqual(results[5], e.Y);
        Assert.AreEqual(results[6], f.X);
        Assert.AreEqual(results[7], f.Y);
        Assert.AreEqual(results[8], g.X);
        Assert.AreEqual(results[9], g.Y);
        Assert.AreEqual(results[10], h.X);
        Assert.AreEqual(results[11], h.Y);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
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

            int2 sr = new(5, 12);
            uint2 sl = new(22, 17);
            int2 g = a >> sr;
            int2 h = a << sl;

            buffer[0] = c.X;
            buffer[1] = c.Y;
            buffer[2] = d.X;
            buffer[3] = d.Y;
            buffer[4] = e.X;
            buffer[5] = e.Y;
            buffer[6] = f.X;
            buffer[7] = f.Y;
            buffer[8] = g.X;
            buffer[9] = g.Y;
            buffer[10] = h.X;
            buffer[11] = h.Y;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void BooleanHlslOperators(Device device)
    {
        int[] data = [1, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer(data);

        device.Get().For(1, new BooleanOperatorsShader(buffer));

        int[] results = buffer.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { 1, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0, 1 },
            actual: results);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
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
        float[] data1 = [3.14f, 0, 1.44f, 0, 0, 0.4445f];
        int[] data2 = [3, 0, 1, 0, 0, 156];

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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
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
        int[] data = [1, 0, 1, 0, 0, 1];

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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ConstantsInShaderConstantFieldsShader : IComputeShader
    {
        public const float rot_angle = (float)(137.2 / 180.0 * Math.PI);
        public const float rot_angle2 = rot_angle + ((float)Math.E * 100);
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

    [CombinatorialTestMethod]
    [AllDevices]
    public void ShaderWithMixedStaticFieldsInExternalTypes(Device device)
    {
        using ReadWriteBuffer<float> buffer = device.Get().AllocateReadWriteBuffer<float>(16);

        device.Get().For(1, new MixedStaticFieldsInExternalTypesShader(buffer));

        float[] results = buffer.ToArray();

        Assert.AreEqual(3.14f, results[0]);
        Assert.AreEqual(111, results[1]);
        Assert.AreEqual(0, results[2]);
        Assert.AreEqual(222, results[3]);
        Assert.AreEqual(6.28f, results[4]);
        Assert.AreEqual(42, results[5]);
        Assert.AreEqual(0, results[6]);
        Assert.AreEqual(333, results[7]);
        Assert.AreEqual(2, results[8]);
        Assert.AreEqual(444, results[9]);
        Assert.AreEqual(123, results[10]);
        Assert.AreEqual(666, results[11]);
        Assert.AreEqual(3.14f, results[12]);
        Assert.AreEqual(3.14f, results[13]);
        Assert.AreEqual(6.28f, results[14]);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct MixedStaticFieldsInExternalTypesShader : IComputeShader
    {
        public static readonly float PI = 3.14f;
        public static readonly int A = 111;
        public static int B;
        public static int C = 222;

        private readonly ReadWriteBuffer<float> buffer;

        public void Execute()
        {
            // Read static fields from this shader
            buffer[0] = PI;
            buffer[1] = A;
            buffer[2] = B;
            buffer[3] = C;

            // Read static fields from an external type
            buffer[4] = ExternalType.PI2;
            buffer[5] = ExternalType.A;
            buffer[6] = ExternalType.B;
            buffer[7] = ExternalType.C;

            // Mutate the fields and check again
            B += 2;
            C *= 2;
            ExternalType.B = 123;
            ExternalType.C *= 2;

            buffer[8] = B;
            buffer[9] = C;
            buffer[10] = ExternalType.B;
            buffer[11] = ExternalType.C;

            // Access shader fields via a member access
            buffer[12] = MixedStaticFieldsInExternalTypesShader.PI;
            buffer[13] = ExternalType.ReadPI();

            // Access an external field as an identifier name
            buffer[14] = ExternalType.ReadPI2();
        }
    }

    internal static class ExternalType
    {
        public static readonly float PI2 = 6.28f;
        public static readonly int A = 42;
        public static int B;
        public static int C = 333;

        public static float ReadPI()
        {
            return MixedStaticFieldsInExternalTypesShader.PI;
        }

        // See https://github.com/Sergio0694/ComputeSharp/issues/298
        public static float ReadPI2()
        {
            return PI2;
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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [RequiresDoublePrecisionSupport]
    [GeneratedComputeShaderDescriptor]
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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
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
        float[] data1 = [0.727829933f, 0.6413954f, 0.9373726f, 0.7044427f, 0.46349f, 0.8098116f, 0.604649544f, 0.309247822f, 0.470999569f, 0.7374923f, 0.6993038f, 0.518516064f, 0.44598946f];
        int[] data2 = [287, 295, 953, 465, 1011, 308, 982, 186, 323, 325, 156, 454, 580];

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
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
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

    // See https://github.com/Sergio0694/ComputeSharp/issues/390
    [CombinatorialTestMethod]
    [AllDevices]
    public void CustomStructWypeWithInstanceMethod(Device device)
    {
        CustomStructType[] data = Enumerable.Range(0, 1024).Select(static i => new CustomStructType() { value = i }).ToArray();

        using ReadWriteBuffer<CustomStructType> source = device.Get().AllocateReadWriteBuffer(data);
        using ReadWriteBuffer<int> destination = device.Get().AllocateReadWriteBuffer<int>(data.Length, AllocationMode.Clear);

        device.Get().For(source.Length, new CustomStructTypeShader(source, destination));

        CustomStructType[] results1 = source.ToArray();
        int[] results2 = destination.ToArray();

        for (int i = 0; i < source.Length; i++)
        {
            Assert.AreEqual((i * 2) + 1, results1[i].value);
            Assert.AreEqual(results1[i].value, results2[i]);
        }
    }

    public struct CustomStructType
    {
        public int value;

        public int AddAndGetSum(int x)
        {
            this.value += x;

            return this.value;
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct CustomStructTypeShader : IComputeShader
    {
        public readonly ReadWriteBuffer<CustomStructType> source;
        public readonly ReadWriteBuffer<int> destination;

        public void Execute()
        {
            destination[ThreadIds.X] = source[ThreadIds.X].AddAndGetSum(ThreadIds.X + 1);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void MiscHlslIntrinsics(Device device)
    {
        float[] data1 = [1, 2, 0.4f, 3.14f, 2.4f, 0.8f, 1.5f, -0.8f, 2.4f, 0.5f, 0.6f, -0.9f, 0, 0, 0, 0];
        int[] data2 = [111, 222, 333, 2, 4, 8, 63, 42, 77, 0, 0, 0];

        using ReadWriteBuffer<float> buffer1 = device.Get().AllocateReadWriteBuffer(data1);
        using ReadWriteBuffer<int> buffer2 = device.Get().AllocateReadWriteBuffer(data2);

        device.Get().For(1, new MiscHlslIntrinsicsShader(buffer1, buffer2));

        float[] results1 = buffer1.ToArray();
        int[] results2 = buffer2.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { 1, 2, 0.4f, 3.14f, 2.4f, 0.8f, 1.5f, -0.8f, 2.4f, 0.5f, 0.6f, -0.9f, 2.8f, 7.08f, -1.5f, 0.8f },
            actual: results1,
            comparer: Comparer<float>.Create(static (x, y) => Math.Abs(x - y) < 0.000001f ? 0 : x.CompareTo(y)));

        CollectionAssert.AreEqual(
            expected: new[] { 111, 222, 333, 2, 4, 8, 63, 42, 77, 285, 930, 2741 },
            actual: results2);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct MiscHlslIntrinsicsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer1;
        public readonly ReadWriteBuffer<int> buffer2;

        public void Execute()
        {
            float2 f1 = new(buffer1[0], buffer1[1]);
            float2 f2 = new(buffer1[2], buffer1[3]);
            float2 f3 = new(buffer1[4], buffer1[5]);
            int3 i1 = new(buffer2[0], buffer2[1], buffer2[2]);
            int3 i2 = new(buffer2[3], buffer2[4], buffer2[5]);
            int3 i3 = new(buffer2[6], buffer2[7], buffer2[8]);
            float2 f4 = new(buffer1[6], buffer1[7]);
            float2 f5 = new(buffer1[8], buffer1[9]);
            float2 f6 = new(buffer1[10], buffer1[11]);

            float2 r1 = Hlsl.Mad(f1, f2, f3);
            int3 r2 = Hlsl.Mad(i1, i2, i3);
            float2 r3 = Hlsl.FaceForward(f4, f5, f6);

            buffer1[12] = r1.X;
            buffer1[13] = r1.Y;
            buffer2[9] = r2.X;
            buffer2[10] = r2.Y;
            buffer2[11] = r2.Z;
            buffer1[14] = r3.X;
            buffer1[15] = r3.Y;
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/525
    [CombinatorialTestMethod]
    [AllDevices]
    public void ReadonlyModifierInMethods(Device device)
    {
        int[] data = [0];

        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer(data);

        device.Get().For(1, new ReadonlyModifierInMethodsShader(buffer, new MyStructWithReadonlyMethod { Number = 40 }));

        int[] results = buffer.ToArray();

        CollectionAssert.AreEqual(expected: new[] { 42 }, actual: results);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadonlyModifierInMethodsShader : IComputeShader
    {
        private readonly ReadWriteBuffer<int> buffer;
        private readonly MyStructWithReadonlyMethod myStruct;

        /// <inheritdoc/>
        public void Execute()
        {
            this.buffer[ThreadIds.X] = myStruct.GetNumber() + Foo();
        }

        private readonly int Foo()
        {
            return 2;
        }
    }

    public struct MyStructWithReadonlyMethod
    {
        public int Number;

        public readonly int GetNumber()
        {
            return this.Number;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void HlslMatrixTypesCastOperators(Device device)
    {
        using ReadWriteBuffer<float> buffer1 = device.Get().AllocateReadWriteBuffer<float>(3);
        using ReadWriteBuffer<int> buffer2 = device.Get().AllocateReadWriteBuffer<int>(8);
        using ReadWriteBuffer<uint> buffer3 = device.Get().AllocateReadWriteBuffer<uint>(8);

        device.Get().For(1, new HlslMatrixTypesCastOperatorsShader(buffer1, buffer2, buffer3));

        float[] results1 = buffer1.ToArray();
        int[] results2 = buffer2.ToArray();
        uint[] results3 = buffer3.ToArray();

        CollectionAssert.AreEqual(
            expected: new float[] { 111, 222, 333 },
            actual: results1,
            comparer: Comparer<float>.Create(static (x, y) => Math.Abs(x - y) < 0.000001f ? 0 : x.CompareTo(y)));

        CollectionAssert.AreEqual(
            expected: new[] { 1, 3, 4, 5, 6, 1, 2, 3 },
            actual: results2);

        CollectionAssert.AreEqual(
            expected: new uint[] { 1, 3, 4, 5, 6, 1, 2, 3 },
            actual: results3);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct HlslMatrixTypesCastOperatorsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer1;
        public readonly ReadWriteBuffer<int> buffer2;
        public readonly ReadWriteBuffer<uint> buffer3;

        public void Execute()
        {
            int1x3 i1x3 = new(111, 222, 333);
            float2x4 f2x4 = new(1, 3.14f, 4, 5, 6.28f, 1.11f, 2.22f, 3.33f);

            float1x3 f1x3 = (float1x3)i1x3;
            int2x4 i2x4 = (int2x4)f2x4;
            uint2x4 ui2x4 = (uint2x4)f2x4;

            buffer1[0] = f1x3.M11;
            buffer1[1] = f1x3.M12;
            buffer1[2] = f1x3.M13;

            buffer2[0] = i2x4.M11;
            buffer2[1] = i2x4.M12;
            buffer2[2] = i2x4.M13;
            buffer2[3] = i2x4.M14;
            buffer2[4] = i2x4.M21;
            buffer2[5] = i2x4.M22;
            buffer2[6] = i2x4.M23;
            buffer2[7] = i2x4.M24;

            buffer3[0] = ui2x4.M11;
            buffer3[1] = ui2x4.M12;
            buffer3[2] = ui2x4.M13;
            buffer3[3] = ui2x4.M14;
            buffer3[4] = ui2x4.M21;
            buffer3[5] = ui2x4.M22;
            buffer3[6] = ui2x4.M23;
            buffer3[7] = ui2x4.M24;
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/735
    [CombinatorialTestMethod]
    [AllDevices]
    public void KnownNamedIntrinsic_ConditionalSelect(Device device)
    {
        using ReadWriteBuffer<float> buffer1 = device.Get().AllocateReadWriteBuffer<float>(4);
        using ReadWriteBuffer<int> buffer2 = device.Get().AllocateReadWriteBuffer<int>(3);
        using ReadWriteBuffer<uint> buffer3 = device.Get().AllocateReadWriteBuffer<uint>(8);

        device.Get().For(1, new KnownNamedIntrinsic_ConditionalSelectShader(buffer1, buffer2, buffer3));

        float[] results1 = buffer1.ToArray();
        int[] results2 = buffer2.ToArray();
        uint[] results3 = buffer3.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { 1, 6, 3.14f, 4 },
            actual: results1,
            comparer: Comparer<float>.Create(static (x, y) => Math.Abs(x - y) < 0.000001f ? 0 : x.CompareTo(y)));

        CollectionAssert.AreEqual(
            expected: new[] { 1, 2, 6 },
            actual: results2);

        CollectionAssert.AreEqual(
           expected: new uint[] { 1, 2, 333, 4, 555, 666, 777, 8 },
           actual: results3);

        ShaderInfo info = ReflectionServices.GetShaderInfo<KnownNamedIntrinsic_ConditionalSelectShader>();

        Assert.AreEqual(
            """
            #define __GroupSize__get_X 64
            #define __GroupSize__get_Y 1
            #define __GroupSize__get_Z 1

            cbuffer _ : register(b0)
            {
                uint __x;
                uint __y;
                uint __z;
            }

            RWStructuredBuffer<float> buffer1 : register(u0);

            RWStructuredBuffer<int> buffer2 : register(u1);

            RWStructuredBuffer<uint> buffer3 : register(u2);

            [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
            void Execute(uint3 ThreadIds : SV_DispatchThreadID)
            {
                if (ThreadIds.x < __x && ThreadIds.y < __y && ThreadIds.z < __z)
                {
                    bool4 mask4 = bool4(true, false, true, true);
                    float4 float4_1 = float4(1, 2, 3.14, 4);
                    float4 float4_2 = float4(5, 6, 7, 8);
                    float4 float4_r = select(mask4, float4_1, float4_2);
                    buffer1[0] = float4_r.x;
                    buffer1[1] = float4_r.y;
                    buffer1[2] = float4_r.z;
                    buffer1[3] = float4_r.w;
                    bool1x3 mask1x3 = bool1x3((bool)true, (bool)true, (bool)false);
                    int1x3 int1x3_1 = int1x3((int)1, (int)2, (int)3);
                    int1x3 int1x3_2 = int1x3((int)4, (int)5, (int)6);
                    int1x3 int1x3_r = select(mask1x3, int1x3_1, int1x3_2);
                    buffer2[0] = int1x3_r._m00;
                    buffer2[1] = int1x3_r._m01;
                    buffer2[2] = int1x3_r._m02;
                    bool2x4 mask2x4 = bool2x4((bool)true, (bool)true, (bool)false, (bool)true, (bool)false, (bool)false, (bool)false, (bool)true);
                    uint2x4 uint2x4_1 = uint2x4((uint)1, (uint)2, (uint)3, (uint)4, (uint)5, (uint)6, (uint)7, (uint)8);
                    uint2x4 uint2x4_2 = uint2x4((uint)111, (uint)222, (uint)333, (uint)444, (uint)555, (uint)666, (uint)777, (uint)888);
                    uint2x4 uint2x4_r = select(mask2x4, uint2x4_1, uint2x4_2);
                    buffer3[0] = uint2x4_r._m00;
                    buffer3[1] = uint2x4_r._m01;
                    buffer3[2] = uint2x4_r._m02;
                    buffer3[3] = uint2x4_r._m03;
                    buffer3[4] = uint2x4_r._m10;
                    buffer3[5] = uint2x4_r._m11;
                    buffer3[6] = uint2x4_r._m12;
                    buffer3[7] = uint2x4_r._m13;
                }
            }
            """,
            info.HlslSource);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct KnownNamedIntrinsic_ConditionalSelectShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer1;
        public readonly ReadWriteBuffer<int> buffer2;
        public readonly ReadWriteBuffer<uint> buffer3;

        public void Execute()
        {
            bool4 mask4 = new(true, false, true, true);
            float4 float4_1 = new(1, 2, 3.14f, 4);
            float4 float4_2 = new(5, 6, 7, 8);

            float4 float4_r = Hlsl.Select(mask4, float4_1, float4_2);

            buffer1[0] = float4_r.X;
            buffer1[1] = float4_r.Y;
            buffer1[2] = float4_r.Z;
            buffer1[3] = float4_r.W;

            bool1x3 mask1x3 = new(true, true, false);
            int1x3 int1x3_1 = new(1, 2, 3);
            int1x3 int1x3_2 = new(4, 5, 6);

            int1x3 int1x3_r = Hlsl.Select(mask1x3, int1x3_1, int1x3_2);

            buffer2[0] = int1x3_r.M11;
            buffer2[1] = int1x3_r.M12;
            buffer2[2] = int1x3_r.M13;

            bool2x4 mask2x4 = new(true, true, false, true, false, false, false, true);
            uint2x4 uint2x4_1 = new(1, 2, 3, 4, 5, 6, 7, 8);
            uint2x4 uint2x4_2 = new(111, 222, 333, 444, 555, 666, 777, 888);

            uint2x4 uint2x4_r = Hlsl.Select(mask2x4, uint2x4_1, uint2x4_2);

            buffer3[0] = uint2x4_r.M11;
            buffer3[1] = uint2x4_r.M12;
            buffer3[2] = uint2x4_r.M13;
            buffer3[3] = uint2x4_r.M14;
            buffer3[4] = uint2x4_r.M21;
            buffer3[5] = uint2x4_r.M22;
            buffer3[6] = uint2x4_r.M23;
            buffer3[7] = uint2x4_r.M24;
        }
    }

    [TestMethod]
    public void KnownNamedIntrinsic_AndOr()
    {
        ShaderInfo info = ReflectionServices.GetShaderInfo<KnownNamedIntrinsic_AndOrShader>();

        Assert.AreEqual(
            """
            #define __GroupSize__get_X 64
            #define __GroupSize__get_Y 1
            #define __GroupSize__get_Z 1

            cbuffer _ : register(b0)
            {
                uint __x;
                uint __y;
                uint __z;
            }

            RWStructuredBuffer<float> __reserved__buffer : register(u0);

            [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
            void Execute(uint3 ThreadIds : SV_DispatchThreadID)
            {
                if (ThreadIds.x < __x && ThreadIds.y < __y && ThreadIds.z < __z)
                {
                    bool4 mask4_1 = bool4(true, false, true, true);
                    bool4 mask4_2 = bool4(true, false, true, true);
                    bool4 mask4_r_and = and(mask4_1, mask4_2);
                    bool4 mask4_r_or = or(mask4_1, mask4_2);
                    bool2x3 mask2x3_1 = bool2x3((bool)true, (bool)false, (bool)true, (bool)true, (bool)false, (bool)false);
                    bool2x3 mask2x3_2 = bool2x3((bool)true, (bool)false, (bool)true, (bool)true, (bool)true, (bool)true);
                    bool2x3 mask2x3_r_and = and(mask2x3_1, mask2x3_2);
                    bool2x3 mask2x3_r_or = or(mask2x3_1, mask2x3_2);
                    __reserved__buffer[0] = mask4_r_and.x ? 1 : 0;
                    __reserved__buffer[1] = mask4_r_or.y ? 1 : 0;
                    __reserved__buffer[2] = mask2x3_r_and._m00 ? 1 : 0;
                    __reserved__buffer[3] = mask2x3_r_or._m00 ? 1 : 0;
                }
            }
            """,
            info.HlslSource);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct KnownNamedIntrinsic_AndOrShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer;

        public void Execute()
        {
            bool4 mask4_1 = new(true, false, true, true);
            bool4 mask4_2 = new(true, false, true, true);
            bool4 mask4_r_and = Hlsl.And(mask4_1, mask4_2);
            bool4 mask4_r_or = Hlsl.Or(mask4_1, mask4_2);

            bool2x3 mask2x3_1 = new(true, false, true, true, false, false);
            bool2x3 mask2x3_2 = new(true, false, true, true, true, true);
            bool2x3 mask2x3_r_and = Hlsl.And(mask2x3_1, mask2x3_2);
            bool2x3 mask2x3_r_or = Hlsl.Or(mask2x3_1, mask2x3_2);

            buffer[0] = mask4_r_and.X ? 1 : 0;
            buffer[1] = mask4_r_or.Y ? 1 : 0;
            buffer[2] = mask2x3_r_and.M11 ? 1 : 0;
            buffer[3] = mask2x3_r_or.M11 ? 1 : 0;
        }
    }
}