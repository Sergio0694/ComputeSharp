using System;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS0649, CS8618

namespace ComputeSharp.Tests;

[TestClass]
public partial class DispatchTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void Verify_ThreadIds(Device device)
    {
        using ReadWriteTexture3D<int4> buffer = device.Get().AllocateReadWriteTexture3D<int4>(50, 50, 50);

        device.Get().For(buffer.Width, buffer.Height, buffer.Depth, new ThreadIdsShader(buffer));

        int4[,,] data = buffer.ToArray();
        int* value = stackalloc int[4];

        for (int z = 0; z < 50; z++)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    *(int4*)value = data[z, y, x];

                    Assert.AreEqual(x, value[0]);
                    Assert.AreEqual(y, value[1]);
                    Assert.AreEqual(z, value[2]);
                }
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(0, 8, 8)]
    [Data(8, 0, 8)]
    [Data(8, 8, 0)]
    [Data(8, -3, 16)]
    [Data(-1, -1, -1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Verify_ThreadIds_OutOfRange(Device device, int x, int y, int z)
    {
        using ReadWriteTexture3D<int4> buffer = device.Get().AllocateReadWriteTexture3D<int4>(50, 50, 50);

        device.Get().For(x, y, z, new ThreadIdsShader(buffer));

        Assert.Fail();
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ThreadIdsShader : IComputeShader
    {
        public readonly ReadWriteTexture3D<int4> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.XYZ].XYZ = ThreadIds.XYZ;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(1, 1, 1)]
    [Data(1, 1, 2)]
    [Data(1, 2, 1)]
    [Data(2, 1, 1)]
    [Data(10, 1, 1)]
    [Data(10, 1, 20)]
    [Data(1, 2, 3)]
    [Data(2, 3, 4)]
    [Data(3, 2, 1)]
    [Data(10, 20, 30)]
    [Data(10, 2, 3)]
    public unsafe void Verify_ThreadIdsNormalized(Device device, int width, int height, int depth)
    {
        using ReadWriteTexture3D<float4> buffer = device.Get().AllocateReadWriteTexture3D<float4>(width, height, depth);

        device.Get().For(buffer.Width, buffer.Height, buffer.Depth, new ThreadIdsNormalizedShader(buffer));

        float4[,,] data = buffer.ToArray();
        float* value = stackalloc float[4];

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    *(float4*)value = data[z, y, x];

                    float expectedX = width == 1 ? 0 : (x / (float)(buffer.Width - 1));
                    float expectedY = height == 1 ? 0 : (y / (float)(buffer.Height - 1));
                    float expectedZ = depth == 1 ? 0 : (z / (float)(buffer.Depth - 1));

                    Assert.AreEqual(expectedX, value[0], 0.000001f);
                    Assert.AreEqual(expectedY, value[1], 0.000001f);
                    Assert.AreEqual(expectedZ, value[2], 0.000001f);
                    Assert.AreEqual(expectedX, value[3], 0.000001f);
                }
            }
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ThreadIdsNormalizedShader : IComputeShader
    {
        public readonly ReadWriteTexture3D<float4> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.XYZ].XYZ = ThreadIds.Normalized.XYZ;
            this.buffer[ThreadIds.XYZ].XY = ThreadIds.Normalized.XY;
            this.buffer[ThreadIds.XYZ].W = ThreadIds.Normalized.X;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void Verify_GroupIds(Device device)
    {
        using ReadWriteTexture3D<int4> buffer = device.Get().AllocateReadWriteTexture3D<int4>(50, 50, 50);

        device.Get().For(buffer.Width, buffer.Height, buffer.Depth, new GroupIdsShader(buffer));

        int4[,,] data = buffer.ToArray();
        int* value = stackalloc int[4];

        for (int z = 0; z < 50; z++)
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    *(int4*)value = data[z, y, x];

                    Assert.AreEqual(x % 4, value[0]);
                    Assert.AreEqual(y % 4, value[1]);
                    Assert.AreEqual(z % 4, value[2]);
                    Assert.AreEqual((value[2] * 4 * 4) + (value[1] * 4) + value[0], value[3]);
                }
            }
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct GroupIdsShader : IComputeShader
    {
        public readonly ReadWriteTexture3D<int4> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.XYZ].XYZ = GroupIds.XYZ;
            this.buffer[ThreadIds.XYZ].W = GroupIds.Index;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Verify_GroupSize(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(32);

        device.Get().For(1, 1, 1, new GroupSizeShader(buffer));

        int[] data = buffer.ToArray();

        Assert.AreEqual(4, data[0]);
        Assert.AreEqual(15, data[1]);
        Assert.AreEqual(7, data[2]);
        Assert.AreEqual(4 * 15 * 7, data[3]);
        Assert.AreEqual(4 + 15, data[4]);
        Assert.AreEqual(4 + 15 + 7, data[5]);
    }

    [AutoConstructor]
    [ThreadGroupSize(4, 15, 7)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct GroupSizeShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[0] = GroupSize.X;
            this.buffer[1] = GroupSize.Y;
            this.buffer[2] = GroupSize.Z;
            this.buffer[3] = GroupSize.Count;
            this.buffer[4] = (int)Hlsl.Dot(GroupSize.XY, float2.One);
            this.buffer[5] = (int)Hlsl.Dot(GroupSize.XYZ, float3.One);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Verify_GridIds(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(256);

        device.Get().For(256, 1, 1, new GridIdsShader(buffer));

        int[] data = buffer.ToArray();

        for (int i = 0; i < data.Length; i++)
        {
            Assert.AreEqual(data[i], i / 32);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(32, 1, 1)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct GridIdsShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[ThreadIds.X] = GridIds.X;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Verify_DispatchSize(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(6);

        device.Get().For(11, 22, 3, new DispatchSizeShader(buffer));

        ShaderInfo info = ReflectionServices.GetShaderInfo<DispatchSizeShader>();

        int[] data = buffer.ToArray();

        Assert.AreEqual(data[0], 11 * 22 * 3);
        Assert.AreEqual(data[1], 11);
        Assert.AreEqual(data[2], 22);
        Assert.AreEqual(data[3], 3);
        Assert.AreEqual(data[4], 11 + 22);
        Assert.AreEqual(data[5], 11 + 22 + 3);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.XYZ)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct DispatchSizeShader : IComputeShader
    {
        public readonly ReadWriteBuffer<int> buffer;

        public void Execute()
        {
            this.buffer[0] = DispatchSize.Count;
            this.buffer[1] = DispatchSize.X;
            this.buffer[2] = DispatchSize.Y;
            this.buffer[3] = DispatchSize.Z;
            this.buffer[4] = (int)Hlsl.Dot(DispatchSize.XY, float2.One);
            this.buffer[5] = (int)Hlsl.Dot(DispatchSize.XYZ, float3.One);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Verify_DispatchAsPixelShader(Device device)
    {
        using ReadWriteTexture2D<Rgba32, float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(256, 256);

        device.Get().ForEach<DispatchPixelShader, float4>(texture);

        Rgba32[,] data = texture.ToArray();

        for (int y = 0; y < texture.Height; y++)
        {
            for (int x = 0; x < texture.Width; x++)
            {
                Rgba32 pixel = data[y, x];

                Assert.AreEqual((float)pixel.R / 255, (float)x / texture.Width, 0.1f);
                Assert.AreEqual((float)pixel.G / 255, (float)y / texture.Height, 0.1f);
                Assert.AreEqual(pixel.B, 255);
                Assert.AreEqual(pixel.A, 255);
            }
        }
    }

    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct DispatchPixelShader : IComputeShader<float4>
    {
        public float4 Execute()
        {
            return new(
                (float)ThreadIds.X / DispatchSize.X,
                (float)ThreadIds.Y / DispatchSize.Y,
                1,
                1);
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Verify_GroupShared_WithFixedSize(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(128);

        device.Get().For(256, new FixedGroupSharedPixelShader(buffer));

        int[] result = buffer.ToArray();

        for (int i = 0; i < 128; i++)
        {
            Assert.AreEqual(result[i], i);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct FixedGroupSharedPixelShader : IComputeShader
    {
        private readonly ReadWriteBuffer<int> buffer;

        [GroupShared(128)]
        private static readonly int[] cache;

        public void Execute()
        {
            int index = ThreadIds.X / 2;
            bool isWritingToGroupShared = ThreadIds.X % 2 == 0;

            if (isWritingToGroupShared)
            {
                cache[index] = index;
            }

            Hlsl.GroupMemoryBarrier();

            if (!isWritingToGroupShared)
            {
                this.buffer[index] = cache[index];
            }
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    public void Verify_GroupShared_WithDynamicSize(Device device)
    {
        using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(32);

        device.Get().For(64, 1, 1, new DynamicGroupSharedPixelShader(buffer));

        int[] result = buffer.ToArray();

        for (int i = 0; i < 32; i++)
        {
            Assert.AreEqual(result[i], i);
        }
    }

    [AutoConstructor]
    [ThreadGroupSize(32, 1, 1)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct DynamicGroupSharedPixelShader : IComputeShader
    {
        private readonly ReadWriteBuffer<int> buffer;

        [GroupShared]
        private static readonly int[] cache;

        public void Execute()
        {
            int index = ThreadIds.X / 2;
            bool isWritingToGroupShared = ThreadIds.X % 2 == 0;

            if (isWritingToGroupShared)
            {
                cache[index] = index;
            }

            Hlsl.GroupMemoryBarrier();

            if (!isWritingToGroupShared)
            {
                this.buffer[index] = cache[index];
            }
        }
    }
}