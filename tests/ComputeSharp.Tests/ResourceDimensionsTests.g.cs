using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class ResourceDimensionsTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadOnlyBuffer_T1_AsReadOnlyBuffer(Device device, int axis0)
    {
        using ReadOnlyBuffer<float> resource = device.Get().AllocateReadOnlyBuffer<float>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadOnlyBuffer_T1_AsReadOnlyBufferShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyBuffer_T1_AsReadOnlyBufferShader : IComputeShader
    {
        public readonly ReadOnlyBuffer<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Length;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadWriteBuffer_T1_AsReadWriteBuffer(Device device, int axis0)
    {
        using ReadWriteBuffer<float> resource = device.Get().AllocateReadWriteBuffer<float>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadWriteBuffer_T1_AsReadWriteBufferShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteBuffer_T1_AsReadWriteBufferShader : IComputeShader
    {
        public readonly ReadWriteBuffer<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Length;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadOnlyTexture1D_T1_AsReadOnlyTexture1D(Device device, int axis0)
    {
        using ReadOnlyTexture1D<float> resource = device.Get().AllocateReadOnlyTexture1D<float>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadOnlyTexture1D_T1_AsReadOnlyTexture1DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture1D_T1_AsReadOnlyTexture1DShader : IComputeShader
    {
        public readonly ReadOnlyTexture1D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadOnlyTexture1D_T1_AsIReadOnlyTexture1D(Device device, int axis0)
    {
        using ReadOnlyTexture1D<float> resource = device.Get().AllocateReadOnlyTexture1D<float>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadOnlyTexture1D_T1_AsIReadOnlyTexture1DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture1D_T1_AsIReadOnlyTexture1DShader : IComputeShader
    {
        public readonly IReadOnlyTexture1D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadWriteTexture1D_T1_AsReadWriteTexture1D(Device device, int axis0)
    {
        using ReadWriteTexture1D<float> resource = device.Get().AllocateReadWriteTexture1D<float>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadWriteTexture1D_T1_AsReadWriteTexture1DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture1D_T1_AsReadWriteTexture1DShader : IComputeShader
    {
        public readonly ReadWriteTexture1D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadWriteTexture1D_T1_AsIReadOnlyTexture1D(Device device, int axis0)
    {
        using ReadWriteTexture1D<float> resource = device.Get().AllocateReadWriteTexture1D<float>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        using (var context = device.Get().CreateComputeContext())
        {
            context.Transition(resource, ResourceState.ReadOnly);
            context.For(1, new ReadWriteTexture1D_T1_AsIReadOnlyTexture1DShader(resource.AsReadOnly(), result));
        }

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture1D_T1_AsIReadOnlyTexture1DShader : IComputeShader
    {
        public readonly IReadOnlyTexture1D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadOnlyTexture1D_T2_AsReadOnlyTexture1D(Device device, int axis0)
    {
        using ReadOnlyTexture1D<Rgba32, float4> resource = device.Get().AllocateReadOnlyTexture1D<Rgba32, float4>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadOnlyTexture1D_T2_AsReadOnlyTexture1DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture1D_T2_AsReadOnlyTexture1DShader : IComputeShader
    {
        public readonly ReadOnlyTexture1D<Rgba32, float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadOnlyTexture1D_T2_AsIReadOnlyNormalizedTexture1D(Device device, int axis0)
    {
        using ReadOnlyTexture1D<Rgba32, float4> resource = device.Get().AllocateReadOnlyTexture1D<Rgba32, float4>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadOnlyTexture1D_T2_AsIReadOnlyNormalizedTexture1DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture1D_T2_AsIReadOnlyNormalizedTexture1DShader : IComputeShader
    {
        public readonly IReadOnlyNormalizedTexture1D<float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadWriteTexture1D_T2_AsReadWriteTexture1D(Device device, int axis0)
    {
        using ReadWriteTexture1D<Rgba32, float4> resource = device.Get().AllocateReadWriteTexture1D<Rgba32, float4>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadWriteTexture1D_T2_AsReadWriteTexture1DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture1D_T2_AsReadWriteTexture1DShader : IComputeShader
    {
        public readonly ReadWriteTexture1D<Rgba32, float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8)]
    [Data(64)]
    [Data(128)]
    [Data(376)]
    public void ReadWriteTexture1D_T2_AsIReadWriteNormalizedTexture1D(Device device, int axis0)
    {
        using ReadWriteTexture1D<Rgba32, float4> resource = device.Get().AllocateReadWriteTexture1D<Rgba32, float4>(axis0);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(1);

        device.Get().For(1, new ReadWriteTexture1D_T2_AsIReadWriteNormalizedTexture1DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture1D_T2_AsIReadWriteNormalizedTexture1DShader : IComputeShader
    {
        public readonly IReadWriteNormalizedTexture1D<float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12)]
    [Data(64, 13)]
    [Data(128, 32)]
    [Data(376, 112)]
    public void ReadOnlyTexture2D_T1_AsReadOnlyTexture2D(Device device, int axis0, int axis1)
    {
        using ReadOnlyTexture2D<float> resource = device.Get().AllocateReadOnlyTexture2D<float>(axis0, axis1);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(2);

        device.Get().For(1, new ReadOnlyTexture2D_T1_AsReadOnlyTexture2DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture2D_T1_AsReadOnlyTexture2DShader : IComputeShader
    {
        public readonly ReadOnlyTexture2D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12)]
    [Data(64, 13)]
    [Data(128, 32)]
    [Data(376, 112)]
    public void ReadOnlyTexture2D_T1_AsIReadOnlyTexture2D(Device device, int axis0, int axis1)
    {
        using ReadOnlyTexture2D<float> resource = device.Get().AllocateReadOnlyTexture2D<float>(axis0, axis1);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(2);

        device.Get().For(1, new ReadOnlyTexture2D_T1_AsIReadOnlyTexture2DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture2D_T1_AsIReadOnlyTexture2DShader : IComputeShader
    {
        public readonly IReadOnlyTexture2D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12)]
    [Data(64, 13)]
    [Data(128, 32)]
    [Data(376, 112)]
    public void ReadWriteTexture2D_T1_AsReadWriteTexture2D(Device device, int axis0, int axis1)
    {
        using ReadWriteTexture2D<float> resource = device.Get().AllocateReadWriteTexture2D<float>(axis0, axis1);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(2);

        device.Get().For(1, new ReadWriteTexture2D_T1_AsReadWriteTexture2DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture2D_T1_AsReadWriteTexture2DShader : IComputeShader
    {
        public readonly ReadWriteTexture2D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12)]
    [Data(64, 13)]
    [Data(128, 32)]
    [Data(376, 112)]
    public void ReadWriteTexture2D_T1_AsIReadOnlyTexture2D(Device device, int axis0, int axis1)
    {
        using ReadWriteTexture2D<float> resource = device.Get().AllocateReadWriteTexture2D<float>(axis0, axis1);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(2);

        using (var context = device.Get().CreateComputeContext())
        {
            context.Transition(resource, ResourceState.ReadOnly);
            context.For(1, new ReadWriteTexture2D_T1_AsIReadOnlyTexture2DShader(resource.AsReadOnly(), result));
        }

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture2D_T1_AsIReadOnlyTexture2DShader : IComputeShader
    {
        public readonly IReadOnlyTexture2D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12)]
    [Data(64, 13)]
    [Data(128, 32)]
    [Data(376, 112)]
    public void ReadOnlyTexture2D_T2_AsReadOnlyTexture2D(Device device, int axis0, int axis1)
    {
        using ReadOnlyTexture2D<Rgba32, float4> resource = device.Get().AllocateReadOnlyTexture2D<Rgba32, float4>(axis0, axis1);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(2);

        device.Get().For(1, new ReadOnlyTexture2D_T2_AsReadOnlyTexture2DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture2D_T2_AsReadOnlyTexture2DShader : IComputeShader
    {
        public readonly ReadOnlyTexture2D<Rgba32, float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12)]
    [Data(64, 13)]
    [Data(128, 32)]
    [Data(376, 112)]
    public void ReadOnlyTexture2D_T2_AsIReadOnlyNormalizedTexture2D(Device device, int axis0, int axis1)
    {
        using ReadOnlyTexture2D<Rgba32, float4> resource = device.Get().AllocateReadOnlyTexture2D<Rgba32, float4>(axis0, axis1);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(2);

        device.Get().For(1, new ReadOnlyTexture2D_T2_AsIReadOnlyNormalizedTexture2DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture2D_T2_AsIReadOnlyNormalizedTexture2DShader : IComputeShader
    {
        public readonly IReadOnlyNormalizedTexture2D<float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12)]
    [Data(64, 13)]
    [Data(128, 32)]
    [Data(376, 112)]
    public void ReadWriteTexture2D_T2_AsReadWriteTexture2D(Device device, int axis0, int axis1)
    {
        using ReadWriteTexture2D<Rgba32, float4> resource = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(axis0, axis1);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(2);

        device.Get().For(1, new ReadWriteTexture2D_T2_AsReadWriteTexture2DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture2D_T2_AsReadWriteTexture2DShader : IComputeShader
    {
        public readonly ReadWriteTexture2D<Rgba32, float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12)]
    [Data(64, 13)]
    [Data(128, 32)]
    [Data(376, 112)]
    public void ReadWriteTexture2D_T2_AsIReadWriteNormalizedTexture2D(Device device, int axis0, int axis1)
    {
        using ReadWriteTexture2D<Rgba32, float4> resource = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(axis0, axis1);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(2);

        device.Get().For(1, new ReadWriteTexture2D_T2_AsIReadWriteNormalizedTexture2DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture2D_T2_AsIReadWriteNormalizedTexture2DShader : IComputeShader
    {
        public readonly IReadWriteNormalizedTexture2D<float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12, 3)]
    [Data(64, 24, 4)]
    [Data(128, 32, 4)]
    [Data(376, 64, 3)]
    public void ReadOnlyTexture3D_T1_AsReadOnlyTexture3D(Device device, int axis0, int axis1, int axis2)
    {
        using ReadOnlyTexture3D<float> resource = device.Get().AllocateReadOnlyTexture3D<float>(axis0, axis1, axis2);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(3);

        device.Get().For(1, new ReadOnlyTexture3D_T1_AsReadOnlyTexture3DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1, axis2 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture3D_T1_AsReadOnlyTexture3DShader : IComputeShader
    {
        public readonly ReadOnlyTexture3D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
            result[2] = source.Depth;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12, 3)]
    [Data(64, 24, 4)]
    [Data(128, 32, 4)]
    [Data(376, 64, 3)]
    public void ReadOnlyTexture3D_T1_AsIReadOnlyTexture3D(Device device, int axis0, int axis1, int axis2)
    {
        using ReadOnlyTexture3D<float> resource = device.Get().AllocateReadOnlyTexture3D<float>(axis0, axis1, axis2);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(3);

        device.Get().For(1, new ReadOnlyTexture3D_T1_AsIReadOnlyTexture3DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1, axis2 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture3D_T1_AsIReadOnlyTexture3DShader : IComputeShader
    {
        public readonly IReadOnlyTexture3D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
            result[2] = source.Depth;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12, 3)]
    [Data(64, 24, 4)]
    [Data(128, 32, 4)]
    [Data(376, 64, 3)]
    public void ReadWriteTexture3D_T1_AsReadWriteTexture3D(Device device, int axis0, int axis1, int axis2)
    {
        using ReadWriteTexture3D<float> resource = device.Get().AllocateReadWriteTexture3D<float>(axis0, axis1, axis2);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(3);

        device.Get().For(1, new ReadWriteTexture3D_T1_AsReadWriteTexture3DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1, axis2 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture3D_T1_AsReadWriteTexture3DShader : IComputeShader
    {
        public readonly ReadWriteTexture3D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
            result[2] = source.Depth;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12, 3)]
    [Data(64, 24, 4)]
    [Data(128, 32, 4)]
    [Data(376, 64, 3)]
    public void ReadWriteTexture3D_T1_AsIReadOnlyTexture3D(Device device, int axis0, int axis1, int axis2)
    {
        using ReadWriteTexture3D<float> resource = device.Get().AllocateReadWriteTexture3D<float>(axis0, axis1, axis2);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(3);

        using (var context = device.Get().CreateComputeContext())
        {
            context.Transition(resource, ResourceState.ReadOnly);
            context.For(1, new ReadWriteTexture3D_T1_AsIReadOnlyTexture3DShader(resource.AsReadOnly(), result));
        }

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1, axis2 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture3D_T1_AsIReadOnlyTexture3DShader : IComputeShader
    {
        public readonly IReadOnlyTexture3D<float> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
            result[2] = source.Depth;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12, 3)]
    [Data(64, 24, 4)]
    [Data(128, 32, 4)]
    [Data(376, 64, 3)]
    public void ReadOnlyTexture3D_T2_AsReadOnlyTexture3D(Device device, int axis0, int axis1, int axis2)
    {
        using ReadOnlyTexture3D<Rgba32, float4> resource = device.Get().AllocateReadOnlyTexture3D<Rgba32, float4>(axis0, axis1, axis2);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(3);

        device.Get().For(1, new ReadOnlyTexture3D_T2_AsReadOnlyTexture3DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1, axis2 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture3D_T2_AsReadOnlyTexture3DShader : IComputeShader
    {
        public readonly ReadOnlyTexture3D<Rgba32, float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
            result[2] = source.Depth;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12, 3)]
    [Data(64, 24, 4)]
    [Data(128, 32, 4)]
    [Data(376, 64, 3)]
    public void ReadOnlyTexture3D_T2_AsIReadOnlyNormalizedTexture3D(Device device, int axis0, int axis1, int axis2)
    {
        using ReadOnlyTexture3D<Rgba32, float4> resource = device.Get().AllocateReadOnlyTexture3D<Rgba32, float4>(axis0, axis1, axis2);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(3);

        device.Get().For(1, new ReadOnlyTexture3D_T2_AsIReadOnlyNormalizedTexture3DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1, axis2 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadOnlyTexture3D_T2_AsIReadOnlyNormalizedTexture3DShader : IComputeShader
    {
        public readonly IReadOnlyNormalizedTexture3D<float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
            result[2] = source.Depth;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12, 3)]
    [Data(64, 24, 4)]
    [Data(128, 32, 4)]
    [Data(376, 64, 3)]
    public void ReadWriteTexture3D_T2_AsReadWriteTexture3D(Device device, int axis0, int axis1, int axis2)
    {
        using ReadWriteTexture3D<Rgba32, float4> resource = device.Get().AllocateReadWriteTexture3D<Rgba32, float4>(axis0, axis1, axis2);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(3);

        device.Get().For(1, new ReadWriteTexture3D_T2_AsReadWriteTexture3DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1, axis2 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture3D_T2_AsReadWriteTexture3DShader : IComputeShader
    {
        public readonly ReadWriteTexture3D<Rgba32, float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
            result[2] = source.Depth;
        }
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Data(8, 12, 3)]
    [Data(64, 24, 4)]
    [Data(128, 32, 4)]
    [Data(376, 64, 3)]
    public void ReadWriteTexture3D_T2_AsIReadWriteNormalizedTexture3D(Device device, int axis0, int axis1, int axis2)
    {
        using ReadWriteTexture3D<Rgba32, float4> resource = device.Get().AllocateReadWriteTexture3D<Rgba32, float4>(axis0, axis1, axis2);
        using ReadWriteBuffer<int> result = device.Get().AllocateReadWriteBuffer<int>(3);

        device.Get().For(1, new ReadWriteTexture3D_T2_AsIReadWriteNormalizedTexture3DShader(resource, result));

        int[] dimensions = result.ToArray();

        CollectionAssert.AreEqual(
            expected: new[] { axis0, axis1, axis2 },
            actual: dimensions);
    }

    [AutoConstructor]
    [ThreadGroupSize(DefaultThreadGroupSizes.X)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct ReadWriteTexture3D_T2_AsIReadWriteNormalizedTexture3DShader : IComputeShader
    {
        public readonly IReadWriteNormalizedTexture3D<float4> source;
        public readonly ReadWriteBuffer<int> result;

        public void Execute()
        {
            result[0] = source.Width;
            result[1] = source.Height;
            result[2] = source.Depth;
        }
    }
}