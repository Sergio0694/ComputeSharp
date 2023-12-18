using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CA1822

namespace ComputeSharp.D2D1.Tests;

[TestClass]
public partial class ShaderRewriterTests
{
    // See https://github.com/Sergio0694/ComputeSharp/issues/725
    [TestMethod]
    public void ShaderWithStructAndInstanceMethodUsingIt_IsRewrittenCorrectly()
    {
        D2D1ShaderInfo shaderInfo = D2D1ReflectionServices.GetShaderInfo<ClassWithShader.ShaderWithStructAndInstanceMethodUsingIt>();

        Assert.AreEqual("""
            // ================================================
            //                  AUTO GENERATED
            // ================================================
            // This shader was created by ComputeSharp.
            // See: https://github.com/Sergio0694/ComputeSharp.

            #define D2D_INPUT_COUNT 0

            #include "d2d1effecthelpers.hlsli"

            struct ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data
            {
                int value;
            };

            void UseData(inout ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data data);

            void UseData(inout ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data data)
            {
                ++data.value;
            }

            D2D_PS_ENTRY(Execute)
            {
                ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data data = (ComputeSharp_D2D1_Tests_ShaderRewriterTests_ClassWithShader_ShaderWithStructAndInstanceMethodUsingIt_Data)0;
                UseData(data);
                return float4(data.value, data.value, data.value, data.value);
            }
            """, shaderInfo.HlslSource);
    }

    internal sealed partial class ClassWithShader
    {
        [D2DInputCount(0)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DGeneratedPixelShaderDescriptor]
        internal readonly partial struct ShaderWithStructAndInstanceMethodUsingIt : ID2D1PixelShader
        {
            private struct Data
            {
                public int value;
            }

            public float4 Execute()
            {
                Data data = default;

                UseData(ref data);

                return new float4(data.value, data.value, data.value, data.value);
            }

            private void UseData(ref Data data)
            {
                ++data.value;
            }
        }
    }
}