using System.Linq;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0009

namespace ComputeSharp.Tests;

[TestClass]
public partial class ShaderMembersTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public void StaticConstants(Device device)
    {
        using ReadWriteBuffer<float> buffer = device.Get().AllocateReadWriteBuffer<float>(8);

        device.Get().For(1, new StaticConstantsShader(buffer));

        float[] results = buffer.ToArray();

        Assert.AreEqual(3.14f, results[0], 0.00001f);
        Assert.AreEqual(results[1], results[2], 0.00001f);
        Assert.AreEqual(1, results[3], 0.00001f);
        Assert.AreEqual(2, results[4], 0.00001f);
        Assert.AreEqual(3, results[5], 0.00001f);
        Assert.AreEqual(4, results[6], 0.00001f);
        Assert.AreEqual(3.14f, results[7], 0.00001f);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct StaticConstantsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> buffer;

        static readonly float Pi = 3.14f;
        static readonly float SinPi = Hlsl.Sin(Pi);
        static readonly int2x2 Mat = new(1, 2, 3, 4);
        static readonly float Combo = Hlsl.Abs(Hlsl.Clamp(Hlsl.Min(Hlsl.Max(3.14f, 2), 10), 0, 42));

        public void Execute()
        {
            buffer[0] = Pi;
            buffer[1] = SinPi;
            buffer[2] = Hlsl.Sin(3.14f);
            buffer[3] = Mat.M11;
            buffer[4] = Mat.M12;
            buffer[5] = Mat.M21;
            buffer[6] = Mat.M22;
            buffer[7] = Combo;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void GlobalVariable(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(32);

        device.Get().For(32, new GlobalVariableShader(buffer));

        int[] results = buffer.ToArray();

        for (int i = 0; i < results.Length; i++)
        {
            Assert.AreEqual(results[i], i);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct GlobalVariableShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        static int total;

        public void Execute()
        {
            for (int i = 0; i < ThreadIds.X; i++)
            {
                total++;
            }

            buffer[ThreadIds.X] = total;
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/193
    [CombinatorialTestMethod]
    [AllDevices]
    public void BoolInstanceFields(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(13, AllocationMode.Clear);

        device.Get().For(1, new BoolInstanceFieldsShader(buffer, true, false, 42, false, true, new int3(1, 2, 3), true, new bool2(false, true), 123, true));

        int[] results = buffer.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { 1, 0, 42, 0, 1, 1, 2, 3, 1, 0, 1, 123, 1 },
            actual: results);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct BoolInstanceFieldsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public readonly bool a;
        public readonly bool b;
        public readonly int c;
        public readonly bool d;
        public readonly bool e;
        public readonly int3 f;
        public readonly bool g;
        public readonly bool2 h;
        public readonly int i;
        public readonly Bool j; // See https://github.com/Sergio0694/ComputeSharp/issues/727

        public void Execute()
        {
            buffer[0] = Hlsl.BoolToInt(a);
            buffer[1] = Hlsl.BoolToInt(b);
            buffer[2] = c;
            buffer[3] = Hlsl.BoolToInt(d);
            buffer[4] = Hlsl.BoolToInt(e);
            buffer[5] = f.X;
            buffer[6] = f.Y;
            buffer[7] = f.Z;
            buffer[8] = Hlsl.BoolToInt(g);
            buffer[9] = Hlsl.BoolToInt(h.X);
            buffer[10] = Hlsl.BoolToInt(h.Y);
            buffer[11] = i;
            buffer[12] = Hlsl.BoolToInt(j);
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/193
    [CombinatorialTestMethod]
    [AllDevices]
    public void BoolInstanceFieldInCustomStruct(Device device)
    {
        Int2AndBoolField[] data = Enumerable.Range(1, 100).Select(i => new Int2AndBoolField { P = new Int2(i, i) }).ToArray();

        using ReadWriteBuffer<Int2AndBoolField> buffer = device.Get().AllocateReadWriteBuffer(data);

        device.Get().For(100, new BoolInstanceFieldInCustomStructShader(buffer));

        Int2AndBoolField[] results = buffer.ToArray();

        for (int i = 0; i < results.Length; i++)
        {
            ref Int2AndBoolField value = ref results[i];

            Assert.AreEqual(i + 1, value.P.X);
            Assert.AreEqual(i + 1, value.P.Y);
            Assert.AreEqual(true, (bool)value.B);
        }
    }

    public struct Int2AndBoolField
    {
        public Int2 P;
        public Bool B;
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct BoolInstanceFieldInCustomStructShader : IComputeShader
    {
        public readonly ReadWriteBuffer<Int2AndBoolField> buffer;

        public void Execute()
        {
            buffer[ThreadIds.X].B = true;
        }
    }
}