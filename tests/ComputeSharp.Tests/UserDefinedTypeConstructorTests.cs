using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0008, IDE0009, IDE0290, CA1861

namespace ComputeSharp.Tests;

[TestClass]
public partial class UserDefinedTypeConstructorTests
{
    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    public void ImplicitParameterlessConstructor(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer([111, 222]);

        device.Get().For(1, new ImplicitParameterlessConstructorShader(buffer));

        int[] results = buffer.ToArray();

        CollectionAssert.AreEqual(new[] { 0, 0 }, results);
    }

    public struct MyValueTypeWithNoConstructor
    {
        public int X;
        public int Y;
    }

    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct ImplicitParameterlessConstructorShader(ReadWriteBuffer<int> buffer) : IComputeShader
    {
        public void Execute()
        {
            MyValueTypeWithNoConstructor valueType = new();

            buffer[0] = valueType.X;
            buffer[1] = valueType.Y;
        }
    }

    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    public void ExplicitParameterlessConstructor(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(2);

        device.Get().For(1, new ExplicitParameterlessConstructorShader(buffer));

        int[] results = buffer.ToArray();

        CollectionAssert.AreEqual(new[] { 42, 12345 }, results);
    }

    public struct MyValueTypeWithParameterlessConstructor
    {
        public int X;
        public int Y;

        public MyValueTypeWithParameterlessConstructor()
        {
            X = 42;
            this.Y = 12345;
        }
    }

    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct ExplicitParameterlessConstructorShader(ReadWriteBuffer<int> buffer) : IComputeShader
    {
        public void Execute()
        {
            MyValueTypeWithParameterlessConstructor valueType = new();

            buffer[0] = valueType.X;
            buffer[1] = valueType.Y;
        }
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/481
    [CombinatorialTestMethod]
    [Device(Device.Warp)]
    public void ConstructorWithParameters(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(8);

        device.Get().For(1, new ConstructorWithParametersShader(buffer));

        int[] results = buffer.ToArray();

        CollectionAssert.AreEqual(new[] { 42, 123456, 3, 4, 111, 222, 5, 6 }, results);
    }

    public struct MyValueTypeWithConstructorWithParameters
    {
        public int X;
        public int Y;
        public float2 Z;

        public MyValueTypeWithConstructorWithParameters(int x, int y, float2 z)
        {
            X = x;
            this.Y = y;
            this.Z = z;
        }
    }

    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct ConstructorWithParametersShader(ReadWriteBuffer<int> buffer) : IComputeShader
    {
        public void Execute()
        {
            MyValueTypeWithConstructorWithParameters valueType = new(42, 123456, new float2(3, 4));

            buffer[0] = valueType.X;
            buffer[1] = valueType.Y;
            buffer[2] = (int)valueType.Z.X;
            buffer[3] = (int)valueType.Z.Y;

            var valueType2 = new MyValueTypeWithConstructorWithParameters(111, 222, new float2(5, 6));

            buffer[4] = valueType2.X;
            buffer[5] = valueType2.Y;
            buffer[6] = (int)valueType2.Z.X;
            buffer[7] = (int)valueType2.Z.Y;
        }
    }
}