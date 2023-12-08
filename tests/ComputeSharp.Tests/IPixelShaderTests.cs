using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class IPixelShaderTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public unsafe void PixelShader_EarlyReturn(Device device)
    {
        using ReadWriteTexture2D<Rgba32, float4> texture = device.Get().AllocateReadWriteTexture2D<Rgba32, float4>(128, 128);

        device.Get().ForEach<EarlyReturnShader, float4>(texture);

        Rgba32[,] result = texture.ToArray();

        for (int i = 0; i < texture.Height; i++)
        {
            for (int j = 0; j < texture.Width; j++)
            {
                if (j % 2 == 0)
                {
                    Assert.AreEqual(result[i, j], new Rgba32(255, 0, 0, 0));
                }
                else
                {
                    Assert.AreEqual(result[i, j], new Rgba32(0, 255, 0, 0));
                }
            }
        }
    }

    [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
    [GeneratedComputeShaderDescriptor]
    internal readonly partial struct EarlyReturnShader : IComputeShader<float4>
    {
        public float4 Execute()
        {
            if (ThreadIds.X % 2 == 0)
            {
                return float4.UnitX;
            }

            return float4.UnitY;
        }
    }
}