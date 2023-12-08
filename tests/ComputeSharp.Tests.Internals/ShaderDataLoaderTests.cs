using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Descriptors;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Internals.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals;

[TestClass]
public partial class ShaderDataLoaderTests
{
    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct CapturedResourceShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> _buffer;

        public void Execute()
        {
        }
    }

    [TestMethod]
    public void CapturedResource()
    {
        using ReadWriteBuffer<float> buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);

        CapturedResourceShader shader = new(buffer);
        DebugDispatchDataLoader dataLoader = new();

        LoadConstantBuffer(in shader, ref dataLoader, 111, 222, 333);

        Assert.AreEqual(12, ConstantBufferSize<CapturedResourceShader>());
        Assert.IsNotNull(dataLoader.ConstantBuffer);
        Assert.AreEqual(12, dataLoader.ConstantBuffer.Length);
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[0]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[4]));
        Assert.AreEqual(333, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[8]));

        LoadGraphicsResources(in shader, ref dataLoader);

        Assert.AreEqual(1, ResourceDescriptorRanges<CapturedResourceShader>().Length);
        Assert.AreSame(buffer, dataLoader.GraphicsResources[0].Resource);
        Assert.AreEqual(0u, dataLoader.GraphicsResources[0].Index);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct MultipleResourcesAndPrimitivesShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> _buffer0;
        public readonly ReadWriteBuffer<float> _buffer1;
        public readonly int a;
        public readonly int b;
        public readonly int c;

        public void Execute()
        {
        }
    }

    [TestMethod]
    public void LoadMultipleResourcesAndPrimitives()
    {
        using ReadWriteBuffer<float> buffer0 = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);
        using ReadWriteBuffer<float> buffer1 = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);

        MultipleResourcesAndPrimitivesShader shader = new(buffer0, buffer1, 1, 22, 77);
        DebugDispatchDataLoader dataLoader = new();

        LoadConstantBuffer(in shader, ref dataLoader, 111, 222, 333);

        Assert.AreEqual(24, ConstantBufferSize<MultipleResourcesAndPrimitivesShader>());
        Assert.IsNotNull(dataLoader.ConstantBuffer);
        Assert.AreEqual(24, dataLoader.ConstantBuffer.Length);
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[0]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[4]));
        Assert.AreEqual(333, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[8]));
        Assert.AreEqual(1, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[12]));
        Assert.AreEqual(22, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[16]));
        Assert.AreEqual(77, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[20]));

        LoadGraphicsResources(in shader, ref dataLoader);

        Assert.AreEqual(2, ResourceDescriptorRanges<MultipleResourcesAndPrimitivesShader>().Length);
        Assert.AreSame(buffer0, dataLoader.GraphicsResources[0].Resource);
        Assert.AreEqual(0u, dataLoader.GraphicsResources[0].Index);
        Assert.AreSame(buffer1, dataLoader.GraphicsResources[1].Resource);
        Assert.AreEqual(1u, dataLoader.GraphicsResources[1].Index);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct ScalarAndVectorTypesShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> _buffer;
        public readonly float3 f3;
        public readonly int a;
        public readonly int b;
        public readonly double2 d2;
        public readonly int c;
        public readonly int d;

        public void Execute()
        {
        }
    }

    [TestMethod]
    public void LoadScalarAndVectorTypes()
    {
        using ReadWriteBuffer<float> buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);

        ScalarAndVectorTypesShader shader = new(buffer, new(55, 44, 888), 22, 77, new(3.14, 6.28), 42, 9999);
        DebugDispatchDataLoader dataLoader = new();

        LoadConstantBuffer(in shader, ref dataLoader, 111, 222, 333);

        Assert.AreEqual(72, ConstantBufferSize<ScalarAndVectorTypesShader>());
        Assert.IsNotNull(dataLoader.ConstantBuffer);
        Assert.AreEqual(72, dataLoader.ConstantBuffer.Length);
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[0]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[4]));
        Assert.AreEqual(333, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[8]));
        Assert.AreEqual(55, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[16]));
        Assert.AreEqual(44, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[20]));
        Assert.AreEqual(888, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[24]));
        Assert.AreEqual(22, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[28]));
        Assert.AreEqual(77, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[32]));
        Assert.AreEqual(3.14, Unsafe.As<byte, double>(ref dataLoader.ConstantBuffer[48]));
        Assert.AreEqual(6.28, Unsafe.As<byte, double>(ref dataLoader.ConstantBuffer[56]));
        Assert.AreEqual(42, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[64]));
        Assert.AreEqual(9999, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[68]));

        LoadGraphicsResources(in shader, ref dataLoader);

        Assert.AreEqual(1, ResourceDescriptorRanges<ScalarAndVectorTypesShader>().Length);
        Assert.AreSame(buffer, dataLoader.GraphicsResources[0].Resource);
        Assert.AreEqual(0u, dataLoader.GraphicsResources[0].Index);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct ScalarVectorAndMatrixTypesShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> _buffer;
        public readonly float2x3 f2x3;
        public readonly int a;
        public readonly Int1x3 i1x3;
        public readonly double2 d2;
        public readonly int c;
        public readonly Int1x2 i1x2;
        public readonly int2x2 i2x2;
        public readonly int d;

        public void Execute()
        {
        }
    }

    [TestMethod]
    public void LoadScalarVectorAndMatrixTypes()
    {
        using ReadWriteBuffer<float> buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);

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
        DebugDispatchDataLoader dataLoader = new();

        LoadConstantBuffer(in shader, ref dataLoader, 111, 222, 333);

        Assert.AreEqual(124, ConstantBufferSize<ScalarVectorAndMatrixTypesShader>());
        Assert.IsNotNull(dataLoader.ConstantBuffer);
        Assert.AreEqual(124, dataLoader.ConstantBuffer.Length);
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[0]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[4]));
        Assert.AreEqual(333, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[8]));
        Assert.AreEqual(55, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[16]));
        Assert.AreEqual(44, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[20]));
        Assert.AreEqual(888, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[24]));
        Assert.AreEqual(111, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[32]));
        Assert.AreEqual(222, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[36]));
        Assert.AreEqual(333, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[40]));
        Assert.AreEqual(22, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[44]));
        Assert.AreEqual(1, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[48]));
        Assert.AreEqual(2, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[52]));
        Assert.AreEqual(3, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[56]));
        Assert.AreEqual(3.14, Unsafe.As<byte, double>(ref dataLoader.ConstantBuffer[64]));
        Assert.AreEqual(6.28, Unsafe.As<byte, double>(ref dataLoader.ConstantBuffer[72]));
        Assert.AreEqual(42, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[80]));
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[84]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[88]));
        Assert.AreEqual(11, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[96]));
        Assert.AreEqual(22, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[100]));
        Assert.AreEqual(33, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[112]));
        Assert.AreEqual(44, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[116]));
        Assert.AreEqual(9999, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[120]));

        LoadGraphicsResources(in shader, ref dataLoader);

        Assert.AreEqual(1, ResourceDescriptorRanges<ScalarVectorAndMatrixTypesShader>().Length);
        Assert.AreSame(buffer, dataLoader.GraphicsResources[0].Resource);
        Assert.AreEqual(0u, dataLoader.GraphicsResources[0].Index);
    }

    [AutoConstructor]
    public readonly partial struct SimpleTypes
    {
        public readonly float2x3 f2x3;
        public readonly int a;
        public readonly Int1x3 i1x3;
        public readonly double2 d2;
        public readonly int c;
        public readonly Int1x2 i1x2;
        public readonly int2x2 i2x2;
        public readonly int d;
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct FlatCustomTypeShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> _buffer0;
        public readonly SimpleTypes a;

        public void Execute()
        {
        }
    }

    [TestMethod]
    public void LoadFlatCustomTypeShader()
    {
        using ReadWriteBuffer<float> buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);

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
        DebugDispatchDataLoader dataLoader = new();

        LoadConstantBuffer(in shader, ref dataLoader, 111, 222, 333);

        Assert.AreEqual(124, ConstantBufferSize<FlatCustomTypeShader>());
        Assert.IsNotNull(dataLoader.ConstantBuffer);
        Assert.AreEqual(124, dataLoader.ConstantBuffer.Length);
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[0]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[4]));
        Assert.AreEqual(333, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[8]));
        Assert.AreEqual(55, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[16]));
        Assert.AreEqual(44, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[20]));
        Assert.AreEqual(888, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[24]));
        Assert.AreEqual(111, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[32]));
        Assert.AreEqual(222, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[36]));
        Assert.AreEqual(333, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[40]));
        Assert.AreEqual(22, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[44]));
        Assert.AreEqual(1, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[48]));
        Assert.AreEqual(2, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[52]));
        Assert.AreEqual(3, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[56]));
        Assert.AreEqual(3.14, Unsafe.As<byte, double>(ref dataLoader.ConstantBuffer[64]));
        Assert.AreEqual(6.28, Unsafe.As<byte, double>(ref dataLoader.ConstantBuffer[72]));
        Assert.AreEqual(42, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[80]));
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[84]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[88]));
        Assert.AreEqual(11, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[96]));
        Assert.AreEqual(22, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[100]));
        Assert.AreEqual(33, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[112]));
        Assert.AreEqual(44, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[116]));
        Assert.AreEqual(9999, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[120]));

        LoadGraphicsResources(in shader, ref dataLoader);

        Assert.AreEqual(1, ResourceDescriptorRanges<FlatCustomTypeShader>().Length);
        Assert.AreSame(buffer, dataLoader.GraphicsResources[0].Resource);
        Assert.AreEqual(0u, dataLoader.GraphicsResources[0].Index);
    }

    [AutoConstructor]
    public readonly partial struct CustomType1
    {
        public readonly float3 a;
        public readonly SimpleTypes b;
        public readonly int c;
        public readonly CustomType2 d;
        public readonly CustomType3 e;
    }

    [AutoConstructor]
    public readonly partial struct CustomType2
    {
        public readonly int a;
        public readonly float2 b;
        public readonly CustomType3 c;
    }

    [AutoConstructor]
    public readonly partial struct CustomType3
    {
        public readonly int a;
        public readonly float2 b;
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct NestedCustomTypesShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> _buffer0;
        public readonly CustomType1 a;

        public void Execute()
        {
        }
    }

    [TestMethod]
    public void LoadNestedCustomTypes()
    {
        using ReadWriteBuffer<float> buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);

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
                        b: new(111.1f, 222.2f))),
                e: new CustomType3(
                    a: 888888,
                    b: new(333.3f, 444.4f))));
        DebugDispatchDataLoader dataLoader = new();

        LoadConstantBuffer(in shader, ref dataLoader, 111, 222, 333);

        Assert.AreEqual(188, ConstantBufferSize<NestedCustomTypesShader>());
        Assert.IsNotNull(dataLoader.ConstantBuffer);
        Assert.AreEqual(188, dataLoader.ConstantBuffer.Length);
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[0]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[4]));
        Assert.AreEqual(333, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[8]));
        Assert.AreEqual(3.14f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[16]));
        Assert.AreEqual(6.28f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[20]));
        Assert.AreEqual(123.4f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[24]));
        Assert.AreEqual(55, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[32]));
        Assert.AreEqual(44, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[36]));
        Assert.AreEqual(888, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[40]));
        Assert.AreEqual(111, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[48]));
        Assert.AreEqual(222, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[52]));
        Assert.AreEqual(333, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[56]));
        Assert.AreEqual(22, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[60]));
        Assert.AreEqual(1, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[64]));
        Assert.AreEqual(2, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[68]));
        Assert.AreEqual(3, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[72]));
        Assert.AreEqual(3.14, Unsafe.As<byte, double>(ref dataLoader.ConstantBuffer[80]));
        Assert.AreEqual(6.28, Unsafe.As<byte, double>(ref dataLoader.ConstantBuffer[88]));
        Assert.AreEqual(42, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[96]));
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[100]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[104]));
        Assert.AreEqual(11, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[112]));
        Assert.AreEqual(22, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[116]));
        Assert.AreEqual(33, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[128]));
        Assert.AreEqual(44, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[132]));
        Assert.AreEqual(9999, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[136]));
        Assert.AreEqual(42, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[140]));
        Assert.AreEqual(1234567, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[144]));
        Assert.AreEqual(44.4f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[148]));
        Assert.AreEqual(55.5f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[152]));
        Assert.AreEqual(7654321, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[160]));
        Assert.AreEqual(111.1f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[164]));
        Assert.AreEqual(222.2f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[168]));
        Assert.AreEqual(888888, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[176]));
        Assert.AreEqual(333.3f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[180]));
        Assert.AreEqual(444.4f, Unsafe.As<byte, float>(ref dataLoader.ConstantBuffer[184]));

        LoadGraphicsResources(in shader, ref dataLoader);

        Assert.AreEqual(1, ResourceDescriptorRanges<NestedCustomTypesShader>().Length);
        Assert.AreSame(buffer, dataLoader.GraphicsResources[0].Resource);
        Assert.AreEqual(0u, dataLoader.GraphicsResources[0].Index);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    public readonly partial struct AmbiguousNamesShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> a;
        public readonly ReadWriteBuffer<float> b;
        public readonly ReadWriteBuffer<float> c;
        public readonly ReadWriteBuffer<float> x;
        public readonly ReadWriteBuffer<float> y;
        public readonly ReadWriteBuffer<float> z;
        public readonly int dataLoader;
        public readonly int device;
        public readonly int dummy;

        public void Execute()
        {
        }
    }

    [TestMethod]
    public void AmbiguousNames()
    {
        using ReadWriteBuffer<float> a = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);
        using ReadWriteBuffer<float> b = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);
        using ReadWriteBuffer<float> c = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);
        using ReadWriteBuffer<float> x = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);
        using ReadWriteBuffer<float> y = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);
        using ReadWriteBuffer<float> z = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(16);

        AmbiguousNamesShader shader = new(a, b, c, x, y, z, 7777, 8888, 9999);
        DebugDispatchDataLoader dataLoader = new();

        LoadConstantBuffer(in shader, ref dataLoader, 111, 222, 333);

        Assert.AreEqual(24, ConstantBufferSize<AmbiguousNamesShader>());
        Assert.IsNotNull(dataLoader.ConstantBuffer);
        Assert.AreEqual(24, dataLoader.ConstantBuffer.Length);
        Assert.AreEqual(111, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[0]));
        Assert.AreEqual(222, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[4]));
        Assert.AreEqual(333, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[8]));
        Assert.AreEqual(7777, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[12]));
        Assert.AreEqual(8888, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[16]));
        Assert.AreEqual(9999, Unsafe.As<byte, int>(ref dataLoader.ConstantBuffer[20]));

        LoadGraphicsResources(in shader, ref dataLoader);

        Assert.AreEqual(6, ResourceDescriptorRanges<AmbiguousNamesShader>().Length);
        Assert.AreSame(a, dataLoader.GraphicsResources[0].Resource);
        Assert.AreEqual(0u, dataLoader.GraphicsResources[0].Index);
        Assert.AreSame(b, dataLoader.GraphicsResources[1].Resource);
        Assert.AreEqual(1u, dataLoader.GraphicsResources[1].Index);
        Assert.AreSame(c, dataLoader.GraphicsResources[2].Resource);
        Assert.AreEqual(2u, dataLoader.GraphicsResources[2].Index);
        Assert.AreSame(x, dataLoader.GraphicsResources[3].Resource);
        Assert.AreEqual(3u, dataLoader.GraphicsResources[3].Index);
        Assert.AreSame(y, dataLoader.GraphicsResources[4].Resource);
        Assert.AreEqual(4u, dataLoader.GraphicsResources[4].Index);
        Assert.AreSame(z, dataLoader.GraphicsResources[5].Resource);
        Assert.AreEqual(5u, dataLoader.GraphicsResources[5].Index);
    }

    /// <inheritdoc cref="IComputeShaderDescriptor{T}.ConstantBufferSize"/>
    private static int ConstantBufferSize<T>()
        where T : struct, IComputeShaderDescriptor<T>
    {
        return T.ConstantBufferSize;
    }

    /// <inheritdoc cref="IComputeShaderDescriptor{T}.ResourceDescriptorRanges"/>
    private static ReadOnlyMemory<ResourceDescriptorRange> ResourceDescriptorRanges<T>()
        where T : struct, IComputeShaderDescriptor<T>
    {
        return T.ResourceDescriptorRanges;
    }

    /// <inheritdoc cref="IComputeShaderDescriptor{T}.LoadConstantBuffer"/>
    private static void LoadConstantBuffer<T, TLoader>(in T shader, ref TLoader loader, int x, int y, int z)
        where T : struct, IComputeShaderDescriptor<T>
        where TLoader : struct, IConstantBufferLoader
    {
        T.LoadConstantBuffer(in shader, ref loader, x, y, z);
    }

    /// <inheritdoc cref="IComputeShaderDescriptor{T}.LoadGraphicsResources"/>
    private static void LoadGraphicsResources<T, TLoader>(in T shader, ref TLoader loader)
        where T : struct, IComputeShaderDescriptor<T>
        where TLoader : struct, IGraphicsResourceLoader
    {
        T.LoadGraphicsResources(in shader, ref loader);
    }
}