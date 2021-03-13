using System;
using System.Linq;
using ComputeSharp.Resources;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.Toolkit.HighPerformance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("TransferTexture3D")]
    public partial class TransferTexture3DTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(UploadTexture3D<>))]
        [Resource(typeof(ReadBackTexture3D<>))]
        [Data(AllocationMode.Default)]
        [Data(AllocationMode.Clear)]
        public unsafe void Allocate_Uninitialized_Ok(Device device, Type bufferType, AllocationMode allocationMode)
        {
            using TransferTexture3D<float> texture = device.Get().AllocateTransferTexture3D<float>(bufferType, 128, 256, 32, allocationMode);

            Assert.IsNotNull(texture);
            Assert.AreEqual(texture.Width, 128);
            Assert.AreEqual(texture.Height, 256);
            Assert.AreEqual(texture.Depth, 32);
            Assert.AreEqual(texture.View.Width, 128);
            Assert.AreEqual(texture.View.Height, 256);
            Assert.AreEqual(texture.View.Depth, 32);
            Assert.AreSame(texture.GraphicsDevice, device.Get());

            if (allocationMode == AllocationMode.Clear)
            {
                for (int z = 0; z < texture.Depth; z++)
                {
                    for (int y = 0; y < texture.Height; y++)
                    {
                        foreach (float x in texture.View.GetRowSpan(y, z))
                        {
                            Assert.AreEqual(x, 0f);
                        }
                    }
                }
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(UploadTexture3D<>))]
        [Resource(typeof(ReadBackTexture3D<>))]
        [Data(128, -14253, 4)]
        [Data(128, -1, 4)]
        [Data(0, -4314, 4)]
        [Data(-14, -53, 4)]
        [Data(12, 12, -1)]
        [Data(2, 4, ushort.MaxValue + 1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Allocate_Uninitialized_Fail(Device device, Type bufferType, int width, int height, int depth)
        {
            using TransferTexture3D<float> texture = device.Get().AllocateTransferTexture3D<float>(bufferType, width, height, depth);
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Resource(typeof(UploadTexture3D<>))]
        [Resource(typeof(ReadBackTexture3D<>))]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void UsageAfterDispose(Device device, Type bufferType)
        {
            using TransferTexture3D<float> texture = device.Get().AllocateTransferTexture3D<float>(bufferType, 32, 32, 16);

            texture.Dispose();

            _ = texture.View;
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Allocate_UploadTexture3D_Copy_Full(Device device)
        {
            using UploadTexture3D<int> uploadTexture3D = device.Get().AllocateUploadTexture3D<int>(256, 256, 16);

            for (int i = 0; i < uploadTexture3D.Depth; i++)
            {
                for (int j = 0; j < uploadTexture3D.Height; j++)
                {
                    new Random(i * 5381 + j).NextBytes(uploadTexture3D.View.GetRowSpan(j, i).AsBytes());
                }
            }

            using ReadOnlyTexture3D<int> readOnlyTexture3D = device.Get().AllocateReadOnlyTexture3D<int>(uploadTexture3D.Width, uploadTexture3D.Height, uploadTexture3D.Depth);

            uploadTexture3D.CopyTo(readOnlyTexture3D);

            int[,,] result = readOnlyTexture3D.ToArray();

            Assert.AreEqual(uploadTexture3D.Width, result.GetLength(2));
            Assert.AreEqual(uploadTexture3D.Height, result.GetLength(1));
            Assert.AreEqual(uploadTexture3D.Depth, result.GetLength(0));

            for (int i = 0; i < uploadTexture3D.Depth; i++)
            {
                for (int j = 0; j < uploadTexture3D.Height; j++)
                {
                    Span<int> sourceRow = uploadTexture3D.View.GetRowSpan(j, i);
                    Span<int> destinationRow = result.AsSpan2D(i).GetRowSpan(j);

                    Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
                }
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(0, 0, 0, 256, 256, 16)]
        [Data(128, 0, 0, 72, 256, 16)]
        [Data(0, 128, 0, 189, 67, 8)]
        [Data(0, 0, 15, 32, 64, 1)]
        [Data(97, 33, 4, 128, 99, 7)]
        public void Allocate_UploadTexture3D_Copy_Range(Device device, int x, int y, int z, int width, int height, int depth)
        {
            using UploadTexture3D<int> uploadTexture3D = device.Get().AllocateUploadTexture3D<int>(256, 256, 16);

            for (int i = 0; i < uploadTexture3D.Depth; i++)
            {
                for (int j = 0; j < uploadTexture3D.Height; j++)
                {
                    new Random(i * 5381 + j).NextBytes(uploadTexture3D.View.GetRowSpan(j, i).AsBytes());
                }
            }

            using ReadOnlyTexture3D<int> readOnlyTexture3D = device.Get().AllocateReadOnlyTexture3D<int>(uploadTexture3D.Width, uploadTexture3D.Height, uploadTexture3D.Depth);

            uploadTexture3D.CopyTo(readOnlyTexture3D, x, y, z, width, height, depth);

            int[,,] result = readOnlyTexture3D.ToArray();

            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Span<int> sourceRow = uploadTexture3D.View.GetRowSpan(j + y, i + z).Slice(x, width);
                    Span<int> destinationRow = result.AsSpan2D(i).GetRowSpan(j).Slice(0, width);

                    Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
                }
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void Allocate_ReadBackTexture3D_Copy_Full(Device device)
        {
            int[,,] source = new int[16, 256, 256];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    new Random(i * 5381 + j).NextBytes(source.AsSpan2D(i).GetRowSpan(j).AsBytes());
                }
            }

            using ReadOnlyTexture3D<int> readOnlyTexture3D = device.Get().AllocateReadOnlyTexture3D(source);
            using ReadBackTexture3D<int> readBackTexture3D = device.Get().AllocateReadBackTexture3D<int>(256, 256, 16);

            readOnlyTexture3D.CopyTo(readBackTexture3D);

            Assert.AreEqual(readBackTexture3D.Width, 256);
            Assert.AreEqual(readBackTexture3D.Height, 256);
            Assert.AreEqual(readBackTexture3D.Depth, 16);

            for (int i = 0; i < readBackTexture3D.Depth; i++)
            {
                for (int j = 0; j < readBackTexture3D.Height; j++)
                {
                    Span<int> sourceRow = source.AsSpan2D(i).GetRowSpan(j);
                    Span<int> destinationRow = readBackTexture3D.View.GetRowSpan(j, i);

                    Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
                }
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        [Data(0, 0, 0, 256, 256, 16)]
        [Data(128, 0, 0, 72, 256, 16)]
        [Data(0, 128, 0, 189, 67, 8)]
        [Data(0, 0, 15, 32, 64, 1)]
        [Data(97, 33, 4, 128, 99, 7)]
        public void Allocate_ReadBackTexture3D_Copy_Range(Device device, int x, int y, int z, int width, int height, int depth)
        {
            int[,,] source = new int[16, 256, 256];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    new Random(i * 5381 + j).NextBytes(source.AsSpan2D(i).GetRowSpan(j).AsBytes());
                }
            }

            using ReadOnlyTexture3D<int> readOnlyTexture3D = device.Get().AllocateReadOnlyTexture3D(source);
            using ReadBackTexture3D<int> readBackTexture3D = device.Get().AllocateReadBackTexture3D<int>(256, 256, 16);

            readOnlyTexture3D.CopyTo(readBackTexture3D, x, y, z, width, height, depth);

            for (int i = 0; i < depth; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Span<int> sourceRow = source.AsSpan2D(i + z).GetRowSpan(j + y).Slice(x, width);
                    Span<int> destinationRow = readBackTexture3D.View.GetRowSpan(j, i).Slice(0, width);

                    Assert.IsTrue(sourceRow.SequenceEqual(destinationRow));
                }
            }
        }
    }
}
