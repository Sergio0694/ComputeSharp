using ComputeSharp.__Internals;
using ComputeSharp.Tests.Internals.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS0618

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("ShaderDataLoader")]
    public partial class ShaderDataLoaderTests
    {
        [AutoConstructor]
        public partial struct CapturedResourceShader : IComputeShader
        {
            public ReadWriteBuffer<float> _;

            public void Execute()
            {
            }
        }

        [TestMethod]
        public unsafe void CapturedResource()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            DebugDispatchDataLoader dataLoader = DebugDispatchDataLoader.Create();

            ((IShader)new CapturedResourceShader(buffer)).LoadDispatchData(ref dataLoader, Gpu.Default, 111, 222, 333);

            Assert.AreEqual(3, dataLoader.Values.Length);
            Assert.AreEqual(1, dataLoader.Resources.Length);

            Assert.AreEqual(111, (int)dataLoader.Values[0]);
            Assert.AreEqual(222, (int)dataLoader.Values[1]);
            Assert.AreEqual(333, (int)dataLoader.Values[2]);
            Assert.AreEqual(buffer.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[0]);
        }

        [AutoConstructor]
        public partial struct MultipleResourcesAndPrimitivesShader : IComputeShader
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
        public unsafe void LoadMultipleResourcesAndPrimitives()
        {
            using ReadWriteBuffer<float> buffer0 = Gpu.Default.AllocateReadWriteBuffer<float>(16);
            using ReadWriteBuffer<float> buffer1 = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            DebugDispatchDataLoader dataLoader = DebugDispatchDataLoader.Create();

            ((IShader)new MultipleResourcesAndPrimitivesShader(buffer0, buffer1, 1, 22, 77)).LoadDispatchData(ref dataLoader, Gpu.Default, 111, 222, 333);

            Assert.AreEqual(6, dataLoader.Values.Length);
            Assert.AreEqual(2, dataLoader.Resources.Length);

            Assert.AreEqual(111, (int)dataLoader.Values[0]);
            Assert.AreEqual(222, (int)dataLoader.Values[1]);
            Assert.AreEqual(333, (int)dataLoader.Values[2]);
            Assert.AreEqual(1, (int)dataLoader.Values[3]);
            Assert.AreEqual(22, (int)dataLoader.Values[4]);
            Assert.AreEqual(77, (int)dataLoader.Values[5]);
            Assert.AreEqual(buffer0.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[0]);
            Assert.AreEqual(buffer1.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[1]);
        }

        [AutoConstructor]
        public partial struct ScalarAndVectorTypesShader : IComputeShader
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
        public unsafe void LoadScalarAndVectorTypes()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            DebugDispatchDataLoader dataLoader = DebugDispatchDataLoader.Create();
            ScalarAndVectorTypesShader shader = new(buffer, new(55, 44, 888), 22, 77, new(3.14, 6.28), 42, 9999);

            ((IShader)shader).LoadDispatchData(ref dataLoader, Gpu.Default, 111, 222, 333);

            Assert.AreEqual(18, dataLoader.Values.Length);
            Assert.AreEqual(1, dataLoader.Resources.Length);

            Assert.AreEqual(111, (int)dataLoader.Values[0]);
            Assert.AreEqual(222, (int)dataLoader.Values[1]);
            Assert.AreEqual(333, (int)dataLoader.Values[2]);

            Assert.AreEqual(buffer.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[0]);

            fixed (void* p0 = &dataLoader.Values[0])
            {
                byte* p1 = (byte*)p0;

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
        }

        [AutoConstructor]
        public partial struct ScalarVectorAndMatrixTypesShader : IComputeShader
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
        public unsafe void LoadScalarVectorAndMatrixTypes()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            DebugDispatchDataLoader dataLoader = DebugDispatchDataLoader.Create();
            ScalarVectorAndMatrixTypesShader shader = new(
                buffer,
                f2x3: new(55, 44, 888, 111, 222, 333),
                a: 22,
                i1x3: new(1, 2, 3),
                d2: new(3.14, 6.28),
                c: 42,
                i1x2: new(111, 222),
                i2x2: new(11, 22, 33, 44),
                d: 9999);

            ((IShader)shader).LoadDispatchData(ref dataLoader, Gpu.Default, 111, 222, 333);

            Assert.AreEqual(31, dataLoader.Values.Length);
            Assert.AreEqual(1, dataLoader.Resources.Length);

            Assert.AreEqual(111, (int)dataLoader.Values[0]);
            Assert.AreEqual(222, (int)dataLoader.Values[1]);
            Assert.AreEqual(333, (int)dataLoader.Values[2]);

            Assert.AreEqual(buffer.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[0]);

            fixed (void* p0 = &dataLoader.Values[0])
            {
                byte* p1 = (byte*)p0;

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

        [AutoConstructor]
        public partial struct SimpleTypes
        {
            public Float2x3 f2x3;
            public int a;
            public Int1x3 i1x3;
            public Double2 d2;
            public int c;
            public Int1x2 i1x2;
            public Int2x2 i2x2;
            public int d;
        }

        [AutoConstructor]
        public partial struct FlatCustomTypeShader : IComputeShader
        {
            public ReadWriteBuffer<float> _0;
            public SimpleTypes a;

            public void Execute()
            {
            }
        }

        [TestMethod]
        public unsafe void LoadFlatCustomTypeShader()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            DebugDispatchDataLoader dataLoader = DebugDispatchDataLoader.Create();
            FlatCustomTypeShader shader = new(
                buffer,
                new SimpleTypes(
                    f2x3: new(55, 44, 888, 111, 222, 333),
                    a: 22,
                    i1x3: new(1, 2, 3),
                    d2: new(3.14, 6.28),
                    c: 42,
                    i1x2: new(111, 222),
                    i2x2: new(11, 22, 33, 44),
                    d: 9999));

            ((IShader)shader).LoadDispatchData(ref dataLoader, Gpu.Default, 111, 222, 333);

            Assert.AreEqual(31, dataLoader.Values.Length);
            Assert.AreEqual(1, dataLoader.Resources.Length);

            Assert.AreEqual(111, (int)dataLoader.Values[0]);
            Assert.AreEqual(222, (int)dataLoader.Values[1]);
            Assert.AreEqual(333, (int)dataLoader.Values[2]);

            Assert.AreEqual(buffer.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[0]);

            fixed (void* p0 = &dataLoader.Values[0])
            {
                byte* p1 = (byte*)p0;

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

        [AutoConstructor]
        public partial struct CustomType1
        {
            public Float3 a;
            public SimpleTypes b;
            public int c;
            public CustomType2 d;
            public CustomType3 e;
        }

        [AutoConstructor]
        public partial struct CustomType2
        {
            public int a;
            public Float2 b;
            public CustomType3 c;
        }

        [AutoConstructor]
        public partial struct CustomType3
        {
            public int a;
            public Float2 b;
        }

        [AutoConstructor]
        public partial struct NestedCustomTypesShader : IComputeShader
        {
            public ReadWriteBuffer<float> _0;
            public CustomType1 a;

            public void Execute()
            {
            }
        }

        [TestMethod]
        public unsafe void LoadNestedCustomTypes()
        {
            using ReadWriteBuffer<float> buffer = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            DebugDispatchDataLoader dataLoader = DebugDispatchDataLoader.Create();
            NestedCustomTypesShader shader = new(
                buffer,
                new CustomType1(
                    a: new(3.14f, 6.28f, 123.4f),
                    b: new SimpleTypes(
                        f2x3: new(55, 44, 888, 111, 222, 333),
                        a: 22,
                        i1x3: new(1, 2, 3),
                        d2: new(3.14, 6.28),
                        c: 42,
                        i1x2: new(111, 222),
                        i2x2: new(11, 22, 33, 44),
                        d: 9999),
                    c: 42,
                    d: new CustomType2(
                        a: 1234567,
                        b: new(44.4f, 55.5f),
                        c: new CustomType3(
                            a: 7654321,
                            b: new (111.1f, 222.2f))),
                    e: new CustomType3(
                        a: 888888,
                        b: new(333.3f, 444.4f))));

            ((IShader)shader).LoadDispatchData(ref dataLoader, Gpu.Default, 111, 222, 333);

            Assert.AreEqual(47, dataLoader.Values.Length);
            Assert.AreEqual(1, dataLoader.Resources.Length);

            Assert.AreEqual(111, (int)dataLoader.Values[0]);
            Assert.AreEqual(222, (int)dataLoader.Values[1]);
            Assert.AreEqual(333, (int)dataLoader.Values[2]);

            Assert.AreEqual(buffer.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[0]);

            fixed (void* p0 = &dataLoader.Values[0])
            {
                byte* p1 = (byte*)p0;

                Assert.AreEqual(3.14f, *(float*)&p1[16]);
                Assert.AreEqual(6.28f, *(float*)&p1[20]);
                Assert.AreEqual(123.4f, *(float*)&p1[24]);
                Assert.AreEqual(55, *(float*)&p1[32]);
                Assert.AreEqual(44, *(float*)&p1[36]);
                Assert.AreEqual(888, *(float*)&p1[40]);
                Assert.AreEqual(111, *(float*)&p1[48]);
                Assert.AreEqual(222, *(float*)&p1[52]);
                Assert.AreEqual(333, *(float*)&p1[56]);
                Assert.AreEqual(22, *(int*)&p1[60]);
                Assert.AreEqual(1, *(int*)&p1[64]);
                Assert.AreEqual(2, *(int*)&p1[68]);
                Assert.AreEqual(3, *(int*)&p1[72]);
                Assert.AreEqual(3.14, *(double*)&p1[80]);
                Assert.AreEqual(6.28, *(double*)&p1[88]);
                Assert.AreEqual(42, *(int*)&p1[96]);
                Assert.AreEqual(111, *(int*)&p1[100]);
                Assert.AreEqual(222, *(int*)&p1[104]);
                Assert.AreEqual(11, *(int*)&p1[112]);
                Assert.AreEqual(22, *(int*)&p1[116]);
                Assert.AreEqual(33, *(int*)&p1[128]);
                Assert.AreEqual(44, *(int*)&p1[132]);
                Assert.AreEqual(9999, *(int*)&p1[136]);
                Assert.AreEqual(42, *(int*)&p1[140]);
                Assert.AreEqual(1234567, *(int*)&p1[144]);
                Assert.AreEqual(44.4f, *(float*)&p1[148]);
                Assert.AreEqual(55.5f, *(float*)&p1[152]);
                Assert.AreEqual(7654321, *(int*)&p1[160]);
                Assert.AreEqual(111.1f, *(float*)&p1[164]);
                Assert.AreEqual(222.2f, *(float*)&p1[168]);
                Assert.AreEqual(888888, *(int*)&p1[176]);
                Assert.AreEqual(333.3f, *(float*)&p1[180]);
                Assert.AreEqual(444.4f, *(float*)&p1[184]);
            }
        }

        [AutoConstructor]
        public partial struct AmbiguousNamesShader : IComputeShader
        {
            public ReadWriteBuffer<float> a;
            public ReadWriteBuffer<float> b;
            public ReadWriteBuffer<float> c;
            public ReadWriteBuffer<float> x;
            public ReadWriteBuffer<float> y;
            public ReadWriteBuffer<float> z;
            public int dataLoader;
            public int device;
            public int dummy;

            public void Execute()
            {
            }
        }

        [TestMethod]
        public unsafe void AmbiguousNames()
        {
            using ReadWriteBuffer<float> a = Gpu.Default.AllocateReadWriteBuffer<float>(16);
            using ReadWriteBuffer<float> b = Gpu.Default.AllocateReadWriteBuffer<float>(16);
            using ReadWriteBuffer<float> c = Gpu.Default.AllocateReadWriteBuffer<float>(16);
            using ReadWriteBuffer<float> x = Gpu.Default.AllocateReadWriteBuffer<float>(16);
            using ReadWriteBuffer<float> y = Gpu.Default.AllocateReadWriteBuffer<float>(16);
            using ReadWriteBuffer<float> z = Gpu.Default.AllocateReadWriteBuffer<float>(16);

            DebugDispatchDataLoader dataLoader = DebugDispatchDataLoader.Create();

            ((IShader)new AmbiguousNamesShader(a, b, c, x, y, z, 7777, 8888, 9999)).LoadDispatchData(ref dataLoader, Gpu.Default, 111, 222, 333);

            Assert.AreEqual(6, dataLoader.Values.Length);
            Assert.AreEqual(6, dataLoader.Resources.Length);

            Assert.AreEqual(111, (int)dataLoader.Values[0]);
            Assert.AreEqual(222, (int)dataLoader.Values[1]);
            Assert.AreEqual(333, (int)dataLoader.Values[2]);
            Assert.AreEqual(7777, (int)dataLoader.Values[3]);
            Assert.AreEqual(8888, (int)dataLoader.Values[4]);
            Assert.AreEqual(9999, (int)dataLoader.Values[5]);
            Assert.AreEqual(a.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[0]);
            Assert.AreEqual(b.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[1]);
            Assert.AreEqual(c.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[2]);
            Assert.AreEqual(x.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[3]);
            Assert.AreEqual(y.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[4]);
            Assert.AreEqual(z.D3D12GpuDescriptorHandle.ptr, dataLoader.Resources[5]);
        }
    }
}
