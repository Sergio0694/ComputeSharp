using ComputeSharp.__Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS0618

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("ShaderDataLoader")]
    public partial class ShaderDataLoaderTests
    {
        [AutoConstructor]
        public partial struct Shader1 : IComputeShader
        {
            public ReadWriteBuffer<float> _;

            public void Execute()
            {
            }
        }

        [TestMethod]
        public unsafe void LoadShader1()
        {
            ulong* p0 = stackalloc ulong[128];
            byte* p1 = stackalloc byte[256];

            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            int size = DispatchDataLoader.LoadDispatchData(Gpu.Default, new Shader1(buffer), ref *p0, ref *p1);

            Assert.AreEqual(12, size);

            Assert.AreEqual(p0[0], buffer.D3D12GpuDescriptorHandle.ptr);
        }

        [AutoConstructor]
        public partial struct Shader2 : IComputeShader
        {
            public ReadWriteBuffer<float> _0;
            public ReadWriteBuffer<float> _1;
            public int a;
            public int b;
            public int c;

            public void Execute()
            {
            }
        }

        [TestMethod]
        public unsafe void LoadShader2()
        {
            ulong* p0 = stackalloc ulong[128];
            byte* p1 = stackalloc byte[256];

            using ReadWriteBuffer<float> buffer0 = Gpu.Default.AllocateReadWriteBuffer<float>(16);
            using ReadWriteBuffer<float> buffer1 = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            int size = DispatchDataLoader.LoadDispatchData(Gpu.Default, new Shader2(buffer0, buffer1, 1, 22, 77), ref *p0, ref *p1);

            Assert.AreEqual(24, size);

            Assert.AreEqual(p0[0], buffer0.D3D12GpuDescriptorHandle.ptr);
            Assert.AreEqual(p0[1], buffer1.D3D12GpuDescriptorHandle.ptr);

            Assert.AreEqual(1, *(int*)&p1[12]);
            Assert.AreEqual(22, *(int*)&p1[16]);
            Assert.AreEqual(77, *(int*)&p1[20]);
        }

        [AutoConstructor]
        public partial struct Shader3 : IComputeShader
        {
            public ReadWriteBuffer<float> _;
            public Float3 f3;
            public int a;
            public int b;
            public Double2 d2;
            public int c;
            public int d;

            public void Execute()
            {
            }
        }

        [TestMethod]
        public unsafe void LoadShader3()
        {
            ulong* p0 = stackalloc ulong[128];
            byte* p1 = stackalloc byte[256];

            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            Shader3 shader = new(buffer, new(55, 44, 888), 22, 77, new(3.14, 6.28), 42, 9999);

            int size = DispatchDataLoader.LoadDispatchData(Gpu.Default, in shader, ref *p0, ref *p1);

            Assert.AreEqual(72, size);

            Assert.AreEqual(p0[0], buffer.D3D12GpuDescriptorHandle.ptr);

            Assert.AreEqual(55, *(float*)&p1[16]);
            Assert.AreEqual(44, *(float*)&p1[20]);
            Assert.AreEqual(888, *(float*)&p1[24]);
            Assert.AreEqual(22, *(int*)&p1[28]);
            Assert.AreEqual(77, *(int*)&p1[32]);
            Assert.AreEqual(3.14, *(double*)&p1[48]);
            Assert.AreEqual(6.28, *(double*)&p1[56]);
            Assert.AreEqual(42, *(int*)&p1[64]);
            Assert.AreEqual(9999, *(int*)&p1[68]);
        }

        [AutoConstructor]
        public partial struct Shader4 : IComputeShader
        {
            public ReadWriteBuffer<float> _;
            public Float2x3 f2x3;
            public int a;
            public Int1x3 i1x3;
            public Double2 d2;
            public int c;
            public Int1x2 i1x2;
            public Int2x2 i2x2;
            public int d;

            public void Execute()
            {
            }
        }

        [TestMethod]
        public unsafe void LoadShader4()
        {
            ulong* p0 = stackalloc ulong[128];
            byte* p1 = stackalloc byte[256];

            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            Shader4 shader = new(
                buffer,
                f2x3: new(55, 44, 888, 111, 222, 333),
                a: 22,
                i1x3: new(1, 2, 3),
                d2: new(3.14, 6.28),
                c: 42,
                i1x2: new(111, 222),
                i2x2: new(11, 22, 33, 44),
                d: 9999);

            int size = DispatchDataLoader.LoadDispatchData(Gpu.Default, in shader, ref *p0, ref *p1);

            Assert.AreEqual(124, size);

            Assert.AreEqual(p0[0], buffer.D3D12GpuDescriptorHandle.ptr);

            Assert.AreEqual(55, *(float*)&p1[16]);
            Assert.AreEqual(44, *(float*)&p1[20]);
            Assert.AreEqual(888, *(float*)&p1[24]);
            Assert.AreEqual(111, *(float*)&p1[32]);
            Assert.AreEqual(222, *(float*)&p1[36]);
            Assert.AreEqual(333, *(float*)&p1[40]);
            Assert.AreEqual(22, *(int*)&p1[44]);
            Assert.AreEqual(1, *(int*)&p1[48]);
            Assert.AreEqual(2, *(int*)&p1[52]);
            Assert.AreEqual(3, *(int*)&p1[56]);
            Assert.AreEqual(3.14, *(double*)&p1[64]);
            Assert.AreEqual(6.28, *(double*)&p1[72]);
            Assert.AreEqual(42, *(int*)&p1[80]);
            Assert.AreEqual(111, *(int*)&p1[84]);
            Assert.AreEqual(222, *(int*)&p1[88]);
            Assert.AreEqual(11, *(int*)&p1[96]);
            Assert.AreEqual(22, *(int*)&p1[100]);
            Assert.AreEqual(33, *(int*)&p1[112]);
            Assert.AreEqual(44, *(int*)&p1[116]);
            Assert.AreEqual(9999, *(int*)&p1[120]);
        }
    }
}
