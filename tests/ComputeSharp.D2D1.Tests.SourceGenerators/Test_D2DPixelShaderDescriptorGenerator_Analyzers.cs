using System.Threading.Tasks;
using ComputeSharp.D2D1.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.SourceGenerators;

[TestClass]
public class Test_D2DPixelShaderDescriptorGenerator_Analyzers
{
    [TestMethod]
    public async Task AllowsUnsafeBlocksNotEnabled_Warns()
    {
        const string source = """
            using ComputeSharp;
            using ComputeSharp.D2D1;

            [{|CMPSD2D0064:D2DGeneratedPixelShaderDescriptor|}]
            internal partial struct MyShader : ID2D1PixelShader
            {
                public Float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<MissingAllowUnsafeBlocksCompilationOptionAnalyzer>.VerifyAnalyzerAsync(source, allowUnsafeBlocks: false);
    }

    [TestMethod]
    public async Task MissingD2DPixelShaderDescriptor_Warns()
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

        await CSharpAnalyzerTest<MissingPixelShaderDescriptorOnPixelShaderAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MissingD2DPixelShaderDescriptor_ManuallyImplemented_DoesNotWarn()
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

        await CSharpAnalyzerTest<MissingPixelShaderDescriptorOnPixelShaderAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<NotReadOnlyPixelShaderTypeWithFieldsAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<NotReadOnlyPixelShaderTypeWithFieldsAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DGeneratedPixelShaderDescriptorAttributeTargetAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<D2DEnableRuntimeCompilationOnTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task UnnecessaryD2DEnableRuntimeCompilation_OnAssembly_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;

            [assembly: D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
            [assembly: {|CMPSD2D0079:D2DEnableRuntimeCompilation|}]
            """;

        await CSharpAnalyzerTest<D2DEnableRuntimeCompilationOnAssemblyAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DRequiresDoublePrecisionSupportAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DRequiresDoublePrecisionSupportAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DRequiresDoublePrecisionSupportAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidD2DResourceTextureIndexAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidPackMatrixColumnMajorOption_AssemblyLevel_WithColumnMajor_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;

            [assembly: {|CMPSD2D0044:D2DCompileOptions(D2D1CompileOptions.PackMatrixColumnMajor)|}]
            """;

        await CSharpAnalyzerTest<InvalidAssemblyLevelCompileOptionsAnalyzer>.VerifyAnalyzerAsync(source);
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

        await CSharpAnalyzerTest<InvalidShaderTypeCompileOptionsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task ExceededDispatchDataSize_ConstantBufferTooLarge_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float1x3 = ComputeSharp.Float1x3;
            using float2x2 = ComputeSharp.Float2x2;
            using float3x2 = ComputeSharp.Float3x2;
            using float4x3 = ComputeSharp.Float4x3;
            using float4x4 = ComputeSharp.Float4x4;
            using float2 = ComputeSharp.Float2;
            using float3 = ComputeSharp.Float3;
            using float4 = ComputeSharp.Float4;

            public struct Data0
            {
                public float4x4 m0;
                public float4 v0;
                public float4 v1;
                public float s0;
                public float s1;
                public float s2;
                public float s3;
            }

            public struct Data1
            {
                public Data0 d0;
                public Data0 d1;
                public Data0 d2;
                public Data0 d3;
                public Data0 d4;
                public float3x2 m0;
                public float2x2 m1;
                public float3 v0;
                public float s0;
                public float s1;
            }

            public struct Data2
            {
                public Data1 d0;
                public Data1 d1;
                public Data1 d2;
                public Data1 d3;
                public float2x2 m0;
                public float1x3 m1;
                public float3 v0;
                public float4 v1;
                public float2 v2;
                public float s0;
            }

            public struct Data3
            {
                public Data1 d0;
                public Data1 d1;
                public Data1 d2;
                public Data1 d3;
                public Data2 d4;
                public Data2 d5;
                public Data2 d6;
                public Data2 d7;
                public Data2 d8;
                public Data2 d9;
                public float3 v0;
            }

            public struct Data4
            {
                public Data3 d0;
                public Data3 d1;
                public Data3 d2;
                public Data3 d3;
                public float4x3 m0;
                public float s0;
            }

            [D2DInputCount(0)]
            internal readonly partial struct {|CMPSD2D0032:MyType|}(Data4 data4) : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return data4.s0;
                }
            }
            """;

        await CSharpAnalyzerTest<ExceededPixelShaderDispatchDataSizeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task OutOfRangeInputDescriptionIndex_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            internal readonly partial struct {|CMPSD2D0042:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputDescriptionAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task OutOfRangeInputDescriptionIndex_WithMissingD2DInputCount_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputDescriptionAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task OutOfRangeInputDescriptionIndex_WithInvalidD2DInputCount_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(37)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputDescriptionAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task RepeatedD2DInputDescriptionIndices_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(4)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(2, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            internal readonly partial struct {|CMPSD2D0043:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputDescriptionAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task RepeatedD2DInputDescriptionIndices_WithMissingD2DInputCount_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;
            
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(2, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputDescriptionAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task RepeatedD2DInputDescriptionIndices_WithInvalidD2DInputCount_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(37)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(2, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputDescriptionAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidInputDescriptions_MultipleErrors_WarnsOncePerDiagnosticId()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(2)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(2, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(1, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(1, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(33, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(34, D2D1Filter.MinMagMipPoint)]
            [D2DInputDescription(-4, D2D1Filter.MinMagMipPoint)]
            internal readonly partial struct {|CMPSD2D0042:{|CMPSD2D0043:MyType|}|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputDescriptionAttributeUseAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task MissingD2DInputCountAttribute_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            internal readonly partial struct {|CMPSD2D0058:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(9)]
    [DataRow(int.MaxValue)]
    public async Task InvalidD2DInputCount_Warns(int inputCount)
    {
        string source = $$"""
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount({{inputCount}})]
            internal readonly partial struct {|CMPSD2D0035:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    [DataRow("Simple")]
    [DataRow("Complex")]
    public async Task OutOfRangeInputIndex_NoInputs_Warns(string inputType)
    {
        string source = $$"""
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(0)]
            [D2DInput{{inputType}}(0)]
            internal readonly partial struct {|CMPSD2D0039:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(6)]
    [DataRow(int.MaxValue)]
    public async Task OutOfRangeInputIndex_OutOfRange_Warns(int inputIndex)
    {
        string source = $$"""
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(6)]
            [D2DInputSimple({{inputIndex}})]
            internal readonly partial struct {|CMPSD2D0039:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task OutOfRangeInputIndex_OutOfRange_BothInputTypes_WarnsOnce()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(6)]
            [D2DInputSimple(0)]
            [D2DInputSimple(1)]
            [D2DInputSimple(6)]
            [D2DInputSimple(8)]
            [D2DInputComplex(-2)]
            [D2DInputComplex(4)]
            [D2DInputComplex(9)]
            internal readonly partial struct {|CMPSD2D0039:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task RepeatedD2DInputSimpleIndices_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(4)]
            [D2DInputSimple(0)]
            [D2DInputSimple(1)]
            [D2DInputSimple(1)]
            [D2DInputSimple(3)]
            internal readonly partial struct {|CMPSD2D0036:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task RepeatedD2DInputComplexIndices_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(4)]
            [D2DInputComplex(0)]
            [D2DInputComplex(1)]
            [D2DInputComplex(1)]
            [D2DInputComplex(3)]
            internal readonly partial struct {|CMPSD2D0037:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidSimpleAndComplexIndicesCombination_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(6)]
            [D2DInputSimple(0)]
            [D2DInputSimple(2)]
            [D2DInputSimple(3)]
            [D2DInputComplex(1)]
            [D2DInputComplex(2)]
            [D2DInputComplex(5)]
            internal readonly partial struct {|CMPSD2D0038:MyType|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidInputTypes_MultipleErrors_WarnsOncePerDiagnosticId()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(17)]
            [D2DInputSimple(0)]
            [D2DInputSimple(1)]
            [D2DInputSimple(3)]
            [D2DInputSimple(3)]
            [D2DInputSimple(-1)]
            [D2DInputSimple(32)]
            [D2DInputComplex(4)]
            [D2DInputComplex(5)]
            [D2DInputComplex(5)]
            [D2DInputComplex(-100)]
            [D2DInputComplex(99)]
            [D2DInputComplex(1)]
            internal readonly partial struct {|CMPSD2D0035:{|CMPSD2D0039:{|CMPSD2D0036:{|CMPSD2D0037:{|CMPSD2D0038:MyType|}|}|}|}|} : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidShaderTypeInputsAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task TrivialSamplingWithComplexInput_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(1)]
            [{|CMPSD2D0069:D2DPixelOptions(D2D1PixelOptions.TrivialSampling)|}]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2D1PixelOptionsTrivialSamplingOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task TrivialSamplingWithExplicitComplexInput_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(1)]
            [D2DInputComplex(1)]
            [{|CMPSD2D0069:D2DPixelOptions(D2D1PixelOptions.TrivialSampling)|}]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2D1PixelOptionsTrivialSamplingOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task TrivialSamplingWithLeftoverComplexInput_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(2)]
            [D2DInputSimple(1)]
            [{|CMPSD2D0069:D2DPixelOptions(D2D1PixelOptions.TrivialSampling)|}]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2D1PixelOptionsTrivialSamplingOnShaderTypeAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task IndexOutOfRangeForD2DIntrinsic_NoCalls_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(0)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task IndexOutOfRangeForD2DIntrinsic_InvalidCall_WithNoD2DInputCount_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput(10);
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task IndexOutOfRangeForD2DIntrinsic_ValidArgument1_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(1)]
            [D2DInputSimple(0)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput(0);
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(4)]
    public async Task IndexOutOfRangeForD2DIntrinsic_ValidArgument2_DoesNotWarn(int index)
    {
        string source = $$"""
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(5)]
            [D2DInputSimple(0)]
            [D2DInputSimple(1)]
            [D2DInputSimple(2)]
            [D2DInputSimple(4)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput({{index}});
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(16)]
    public async Task IndexOutOfRangeForD2DIntrinsic_InvalidArgument1_Warns(int index)
    {
        string source = $$"""
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(0)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput({|CMPSD2D0083:{{index}}|});
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task IndexOutOfRangeForD2DIntrinsic_InvalidArgument2_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(0)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput({|CMPSD2D0086:{|CMPSD2D0083:-1|}|});
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(6)]
    [DataRow(16)]
    public async Task IndexOutOfRangeForD2DIntrinsic_InvalidArgument3_Warns(int index)
    {
        string source = $$"""
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(5)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput({|CMPSD2D0083:{{index}}|});
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task IndexOutOfRangeForD2DIntrinsic_InvalidArgument4_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(5)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput({|CMPSD2D0086:{|CMPSD2D0083:-1|}|});
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidInputTypeForD2DIntrinsic_ValidArguments_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(2)]
            [D2DInputSimple(0)]
            [D2DInputComplex(1)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    D2D.GetInput(0);
                    D2D.GetInput(1);
                    D2D.GetInputCoordinate(1);
                    D2D.SampleInput(1, 0);
                    D2D.SampleInputAtOffset(1, 0);
                    D2D.SampleInputAtPosition(1, 0);

                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidInputTypeForD2DIntrinsic_InvalidArguments_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(2)]
            [D2DInputSimple(0)]
            [D2DInputComplex(1)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    D2D.GetInputCoordinate({|CMPSD2D0084:0|});
                    D2D.SampleInput({|CMPSD2D0084:0|}, 0);
                    D2D.SampleInputAtOffset({|CMPSD2D0084:0|}, 0);
                    D2D.SampleInputAtPosition({|CMPSD2D0084:0|}, 0);

                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task InvalidInputTypeForD2DIntrinsic_InvalidSyntax_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(2)]
            [D2DInputSimple(0)]
            [D2DInputComplex(1)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                const int BaseIndex = 0;

                public float4 Execute()
                {
                    D2D.GetInput({|CMPSD2D0086:+1|});
                    D2D.GetInput({|CMPSD2D0086:-0|});
                    D2D.GetInput({|CMPSD2D0086:(1)|});
                    D2D.GetInput({|CMPSD2D0086:BaseIndex + 1|});
                    D2D.GetInput({|CMPSD2D0086:0 + 1|});

                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<InvalidD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NonConstantSourceInputIndexForD2DIntrinsic_LiteralExpression_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(1)]
            [D2DInputSimple(0)]
            internal readonly partial struct MyType : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput(0);
                }
            }
            """;

        await CSharpAnalyzerTest<NonConstantD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NonConstantSourceInputIndexForD2DIntrinsic_LiteralExpression_WithOtherMethodsTakingNonConstants_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(1)]
            [D2DInputSimple(0)]
            internal readonly partial struct MyType(int x) : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    return D2D.GetInput(0) + M1(x) + M2(x, 1) + M3(x) + M4(x, 1);
                }

                public int M1(int index) => 1;
                public int M2(int index, int x) => 2;

                public static int M3(int index) => 1;
                public static int M4(int index, int x) => 1;
            }
            """;

        await CSharpAnalyzerTest<NonConstantD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NonConstantSourceInputIndexForD2DIntrinsic_NonLiteralConstant_DoesNotWarn()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(1)]
            internal readonly partial struct MyType(int x) : ID2D1PixelShader
            {
                public const int SourceIndex = 0;

                public float4 Execute()
                {
                    D2D.GetInput(SourceIndex);
                    D2D.GetInputCoordinate(SourceIndex);
                    D2D.SampleInput(SourceIndex, 0);
                    D2D.SampleInputAtOffset(SourceIndex, 0);
                    D2D.SampleInputAtPosition(SourceIndex, 0);

                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<NonConstantD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task NonConstantSourceInputIndexForD2DIntrinsic_NonConstantExpression_Warns()
    {
        const string source = """
            using ComputeSharp.D2D1;
            using float4 = ComputeSharp.Float4;

            [D2DInputCount(1)]
            internal readonly partial struct MyType(int x) : ID2D1PixelShader
            {
                public float4 Execute()
                {
                    D2D.GetInput({|CMPSD2D0085:x|});
                    D2D.GetInputCoordinate({|CMPSD2D0085:x|});
                    D2D.SampleInput({|CMPSD2D0085:x|}, 0);
                    D2D.SampleInputAtOffset({|CMPSD2D0085:x|}, 0);
                    D2D.SampleInputAtPosition({|CMPSD2D0085:x|}, 0);

                    return 0;
                }
            }
            """;

        await CSharpAnalyzerTest<NonConstantD2DInputArgumentAnalyzer>.VerifyAnalyzerAsync(source);
    }
}