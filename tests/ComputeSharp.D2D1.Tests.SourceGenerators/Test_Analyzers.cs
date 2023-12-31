using System.Threading.Tasks;
using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_Analyzers
{
    [TestMethod]
    public async Task MissingComputeShaderDescriptor_ComputeShader()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            internal partial struct {|CMPSD2D0065:MyShader|} : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<MissingPixelShaderDescriptorOnPixelShaderAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MissingComputeShaderDescriptor_ManuallyImplemented_DoesNotWarn()
    {
        const string source = """
            using System;
            using ComputeSharp;
            using ComputeSharp.D2D1;
            using ComputeSharp.D2D1.Descriptors;
            using ComputeSharp.D2D1.Interop;

            internal partial struct MyShader : ID2D1PixelShader, ID2D1PixelShaderDescriptor<MyShader>
            {
                public static ref readonly Guid EffectId => throw new NotImplementedException();

                public static string? EffectDisplayName => throw new NotImplementedException();

                public static string? EffectDescription => throw new NotImplementedException();

                public static string? EffectCategory => throw new NotImplementedException();

                public static string? EffectAuthor => throw new NotImplementedException();

                public static int ConstantBufferSize => throw new NotImplementedException();

                public static int InputCount => throw new NotImplementedException();

                public static int ResourceTextureCount => throw new NotImplementedException();

                public static ReadOnlyMemory<D2D1PixelShaderInputType> InputTypes => throw new NotImplementedException();

                public static ReadOnlyMemory<D2D1InputDescription> InputDescriptions => throw new NotImplementedException();

                public static ReadOnlyMemory<D2D1ResourceTextureDescription> ResourceTextureDescriptions => throw new NotImplementedException();

                public static D2D1PixelOptions PixelOptions => throw new NotImplementedException();

                public static D2D1BufferPrecision BufferPrecision => throw new NotImplementedException();

                public static D2D1ChannelDepth ChannelDepth => throw new NotImplementedException();

                public static D2D1ShaderProfile ShaderProfile => throw new NotImplementedException();

                public static D2D1CompileOptions CompileOptions => throw new NotImplementedException();

                public static string HlslSource => throw new NotImplementedException();

                public static ReadOnlyMemory<byte> HlslBytecode => throw new NotImplementedException();

                public static MyShader CreateFromConstantBuffer(ReadOnlySpan<byte> buffer)
                {
                    throw new NotImplementedException();
                }

                public static void LoadConstantBuffer<TLoader>(in MyShader shader, ref TLoader loader) where TLoader : struct, ID2D1ConstantBufferLoader
                {
                    throw new NotImplementedException();
                }

                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<MissingPixelShaderDescriptorOnPixelShaderAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NotReadOnlyShaderType_WithFields_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            namespace MyFancyApp.Sample;

            internal partial struct {|CMPSD2D0070:MyShader|} : ID2D1PixelShader
            {
                public float number;

                public Float4 Execute()
                {
                    return this.number;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<NotReadOnlyPixelShaderTypeWithFieldsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NotReadOnlyShaderType_WithNoFields_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            namespace MyFancyApp.Sample;

            internal partial struct MyShader : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return default;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<NotReadOnlyPixelShaderTypeWithFieldsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidD2DGeneratedPixelShaderDescriptorAttributeTarget_NoInterface()
    {
        const string source = """
            using ComputeSharp.D2D1;

            [{|CMPSD2D0066:D2DGeneratedPixelShaderDescriptor|}]
            internal partial struct MyType
            {
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidD2DGeneratedPixelShaderDescriptorAttributeTarget_GenericType()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [{|CMPSD2D0066:D2DGeneratedPixelShaderDescriptor|}]
            internal partial struct MyType<T> : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidD2DGeneratedPixelShaderDescriptorAttributeTarget_TypeNestedInsideGenericType()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            internal partial class Foo<T>
            {
                [{|CMPSD2D0066:D2DGeneratedPixelShaderDescriptor|}]
                internal partial struct MyType : ID2D1PixelShader
                {
                    public Float4 Execute()
                    {
                        return 0;
                    }
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithNoPrecompiledBytecode_WithNoD2DEnableRuntimeCompilation_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            [D2DGeneratedPixelShaderDescriptor]
            internal partial struct {|CMPSD2D0076:MyType|} : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithNoPrecompiledBytecode_WithD2DEnableRuntimeCompilation_OnType_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            [D2DGeneratedPixelShaderDescriptor]
            [D2DEnableRuntimeCompilation]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithNoPrecompiledBytecode_WithD2DEnableRuntimeCompilation_OnAssembly_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [assembly: D2DEnableRuntimeCompilation]

            [D2DInputCount(0)]
            [D2DGeneratedPixelShaderDescriptor]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithNoPrecompiledBytecode_WithUnnecessaryD2DEnableRuntimeCompilation_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [assembly: D2DEnableRuntimeCompilation]

            [D2DInputCount(0)]
            [{|CMPSD2D0077:D2DEnableRuntimeCompilation|}]
            [D2DGeneratedPixelShaderDescriptor]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithPrecompiledBytecode_FromAssembly_WithUnnecessaryD2DEnableRuntimeCompilation_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [assembly: D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]

            [D2DInputCount(0)]
            [{|CMPSD2D0078:D2DEnableRuntimeCompilation|}]
            [D2DGeneratedPixelShaderDescriptor]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithPrecompiledBytecode_FromType_WithUnnecessaryD2DEnableRuntimeCompilation_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [{|CMPSD2D0078:D2DEnableRuntimeCompilation|}]
            [D2DGeneratedPixelShaderDescriptor]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task UnnecessaryD2DEnableRuntimeCompilation_OnAssembly_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;

            [assembly: D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [assembly: {|CMPSD2D0079:D2DEnableRuntimeCompilation|}]
            """;

        await CSharpAnalyzerWithLanguageVersionTest<D2DEnableRuntimeCompilationOnAssemblyAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithPrecompiledBytecode_FromAssembly_WithD2DRequiresDoublePrecisionSupport_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [assembly: D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]

            [D2DInputCount(0)]
            [D2DRequiresDoublePrecisionSupport]
            [D2DGeneratedPixelShaderDescriptor]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DRequiresDoublePrecisionSupportAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithPrecompiledBytecode_FromType_WithD2DRequiresDoublePrecisionSupport_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [D2DRequiresDoublePrecisionSupport]
            [D2DGeneratedPixelShaderDescriptor]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DRequiresDoublePrecisionSupportAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ShaderWithNoPrecompiledBytecode_WithD2DRequiresDoublePrecisionSupport_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            [{|CMPSD2D0082:D2DRequiresDoublePrecisionSupport|}]
            [D2DGeneratedPixelShaderDescriptor]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DRequiresDoublePrecisionSupportAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MissingD2DResourceTextureIndexAttribute_WarnsOnlyIfMissing()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            internal partial struct MyType : ID2D1PixelShader
            {
                public D2D1ResourceTexture1D<float> {|CMPSD2D0050:texture1D|};
                public D2D1ResourceTexture2D<float> {|CMPSD2D0050:texture2D|};
                public D2D1ResourceTexture3D<float> {|CMPSD2D0050:texture3D|};

                [D2DResourceTextureIndex(0)]
                public D2D1ResourceTexture1D<float> texture1D_2;

                [D2DResourceTextureIndex(1)]
                public D2D1ResourceTexture2D<float> texture2D_2;

                [D2DResourceTextureIndex(2)]
                public D2D1ResourceTexture3D<float> texture3D_2;

                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ResourceTextureIndexOverlappingWithInputIndex_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(2)]
            internal partial struct {|CMPSD2D0046:MyType|} : ID2D1PixelShader
            {
                [D2DResourceTextureIndex(1)]
                public D2D1ResourceTexture1D<float> texture;

                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ResourceTextureIndexOverlappingWithInputIndex_WarnsOnlyOnce()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(2)]
            internal partial struct {|CMPSD2D0046:MyType|} : ID2D1PixelShader
            {
                [D2DResourceTextureIndex(0)]
                public D2D1ResourceTexture1D<float> texture1D;

                [D2DResourceTextureIndex(1)]
                public D2D1ResourceTexture2D<float> texture2D;

                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task OutOfRangeResourceTextureIndex_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            internal partial struct {|CMPSD2D0047:MyType|} : ID2D1PixelShader
            {
                [D2DResourceTextureIndex(16)]
                public D2D1ResourceTexture1D<float> texture;

                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task OutOfRangeResourceTextureIndex_WarnsOnlyOnce()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            internal partial struct {|CMPSD2D0047:MyType|} : ID2D1PixelShader
            {
                [D2DResourceTextureIndex(16)]
                public D2D1ResourceTexture1D<float> texture1D;

                [D2DResourceTextureIndex(17)]
                public D2D1ResourceTexture2D<float> texture2D;

                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task RepeatedD2DResourceTextureIndices_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            internal partial struct {|CMPSD2D0048:MyType|} : ID2D1PixelShader
            {
                [D2DResourceTextureIndex(0)]
                public D2D1ResourceTexture1D<float> texture1;

                [D2DResourceTextureIndex(0)]
                public D2D1ResourceTexture1D<float> texture2;

                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidD2DResourceTextureIndices_MultipleErrors_WarnsOncePerDiagnosticId()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(3)]
            internal partial struct {|CMPSD2D0046:{|CMPSD2D0047:{|CMPSD2D0048:MyType|}|}|} : ID2D1PixelShader
            {
                [D2DResourceTextureIndex(0)]
                public D2D1ResourceTexture1D<float> texture1;

                [D2DResourceTextureIndex(1)]
                public D2D1ResourceTexture1D<float> texture2;

                [D2DResourceTextureIndex(16)]
                public D2D1ResourceTexture1D<float> texture3;

                [D2DResourceTextureIndex(17)]
                public D2D1ResourceTexture2D<float> texture4;

                [D2DResourceTextureIndex(2)]
                public D2D1ResourceTexture1D<float> texture5;

                [D2DResourceTextureIndex(2)]
                public D2D1ResourceTexture1D<float> texture6;

                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidPackMatrixColumnMajorOption_AssemblyLevel_WithColumnMajor_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;

            [assembly: {|CMPSD2D0044:D2DCompileOptions(D2D1CompileOptions.PackMatrixColumnMajor)|}]
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidAssemblyLevelCompileOptionsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidPackMatrixColumnMajorOption_ShaderType_WithColumnMajor_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [D2DInputCount(0)]
            [{|CMPSD2D0044:D2DCompileOptions(D2D1CompileOptions.PackMatrixColumnMajor)|}]
            internal partial struct MyType : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerWithLanguageVersionTest<InvalidShaderTypeCompileOptionsAnalyzer>.VerifyAnalyzerAsync(source);
    }
}