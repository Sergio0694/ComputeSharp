using System.Threading.Tasks;
using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_D2DPixelShaderSourceGenerator_Analyzers
{
    [TestMethod]
    public async Task InvalidD2DPixelShaderSourceMethodReturnType_ReadOnlySpanOfByte_DoesNotWarn()
    {
        const string source = """"
            using System;
            using ComputeSharp.D2D1;

            public partial class MyClass
            {
                [D2DPixelShaderSource("""
                    #define D2D_INPUT_COUNT 0

                    #include "d2d1effecthelpers.hlsli"

                    D2D_PS_ENTRY(Execute)
                    {
                        return 0;
                    }
                    """)]
                public static partial ReadOnlySpan<byte> InvertEffect();

                public static partial ReadOnlySpan<byte> InvertEffect() => default;
            }
            """";

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DPixelShaderSourceMethodReturnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    [DataRow("TimeSpan")]
    [DataRow("ReadOnlyMemory<byte>")]
    [DataRow("ReadOnlySpan<int>")]
    [DataRow("string")]
    [DataRow("object")]
    [DataRow("byte")]
    [DataRow("dynamic")]
    [DataRow("byte*")]
    public async Task InvalidD2DPixelShaderSourceMethodReturnType_InvalidType_Warns(string returnType)
    {
        string source = $$""""
            using System;
            using ComputeSharp.D2D1;

            public unsafe partial class MyClass
            {
                [D2DPixelShaderSource("""
                    #define D2D_INPUT_COUNT 0

                    #include "d2d1effecthelpers.hlsli"

                    D2D_PS_ENTRY(Execute)
                    {
                        return 0;
                    }
                    """)]
                public static partial {{returnType}} {|CMPSD2D0057:InvertEffect|}();

                public static partial {{returnType}} InvertEffect() => default;
            }
            """";

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DPixelShaderSourceMethodReturnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }
}