using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_D2DPixelShaderDescriptorGenerator_Diagnostics
{
    [TestMethod]
    public void MissingD2DRequiresDoublePrecisionSupportAttribute()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            namespace MyNamespace;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader : ID2D1PixelShader
            {
                private readonly float time;

                public float4 Execute()
                {
                    return (float)(time * 2.0);
                }
            }
            """;

        CSharpGeneratorTest<D2DPixelShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPSD2D0080");
    }

    [TestMethod]
    public void UnnecessaryD2DRequiresDoublePrecisionSupportAttribute()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using float4 = global::ComputeSharp.Float4;

            namespace MyNamespace;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DRequiresDoublePrecisionSupport]
            [D2DGeneratedPixelShaderDescriptor]
            internal readonly partial struct MyShader : ID2D1PixelShader
            {
                private readonly float time;

                public float4 Execute()
                {
                    return (float)(time * 2.0f);
                }
            }
            """;

        CSharpGeneratorTest<D2DPixelShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPSD2D0081");
    }
}