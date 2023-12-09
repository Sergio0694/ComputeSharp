extern alias Core;
extern alias D3D12;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Basic.Reference.Assemblies;
using ComputeSharp.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.SourceGenerators;

[TestClass]
public class DiagnosticsTests
{
    [TestMethod]
    public void InvalidShaderField()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;
                public string text;

                public void Execute() { }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0001", "CMPS0047");
    }

    [TestMethod]
    public void InvalidShaderFixedField()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public unsafe partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;
                public fixed int text[16];

                public void Execute() { }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0001", "CMPS0047");
    }

    [TestMethod]
    public void MissingShaderResources()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public void Execute() { }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0005", "CMPS0047");
    }

    [TestMethod]
    [DataRow(nameof(ThreadIds), "CMPS0006")]
    [DataRow(nameof(GroupIds), "CMPS0007")]
    [DataRow(nameof(GroupSize), "CMPS0008")]
    [DataRow(nameof(GridIds), "CMPS0009")]
    [DataRow(nameof(DispatchSize), "CMPS0039")]
    public void InvalidDispatchInfoUsage_LocalFunction(string typeName, string diagnosticsId)
    {
        string source = $$"""
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    static int Fail() => {{typeName}}.X;

                    Fail();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, diagnosticsId, "CMPS0047");
    }

    [TestMethod]
    [DataRow(nameof(ThreadIds), "CMPS0006")]
    [DataRow(nameof(GroupIds), "CMPS0007")]
    [DataRow(nameof(GroupSize), "CMPS0008")]
    [DataRow(nameof(GridIds), "CMPS0009")]
    [DataRow(nameof(DispatchSize), "CMPS0039")]
    public void InvalidDispatchInfoUsage_StaticMethod(string typeName, string diagnosticsId)
    {
        string source = $$"""
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public static int Fail() => {{typeName}}.X;

                public void Execute() => Fail();
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, diagnosticsId, "CMPS0047");
    }

    [TestMethod]
    public void InvalidThreadIdsNormalizedUsage()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public static int Fail() => ThreadIds.Normalized.X;

                public void Execute()
                {
                    buffer[0] = Fail();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0006", "CMPS0047");
    }

    [TestMethod]
    public void InvalidObjectCreationExpression_Explicit()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    object o = new object();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0010", "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidObjectCreationExpression_Implicit()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    object o = new();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0010", "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void AnonymousObjectCreationExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    var o = new { Age = 12 };
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0011", "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void AsyncModifierOnMethodOrFunction_LocalFunction()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    static async void Fail() { }

                    Fail();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0012", "CMPS0047");
    }

    [TestMethod]
    public void AsyncModifierOnMethodOrFunction_StaticMethod()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public static async void Fail() { }

                public void Execute() => Fail();
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0012", "CMPS0047");
    }

    [TestMethod]
    public void AwaitExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public async void Execute()
                {
                    await System.Threading.Tasks.Task.CompletedTask;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0012", "CMPS0013", "CMPS0047");
    }

    [TestMethod]
    [DataRow("checked")]
    [DataRow("unchecked")]
    public void CheckedExpression(string text)
    {
        string source = $$"""
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    int a = {{text}}(1 + 1);
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0014", "CMPS0047");
    }

    [TestMethod]
    [DataRow("checked")]
    [DataRow("unchecked")]
    public void CheckedStatement(string text)
    {
        string source = $$"""
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    {{text}}
                    {
                        int a = 1 + 1;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0015", "CMPS0047");
    }

    [TestMethod]
    public void FixedStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public unsafe void Execute()
                {                        
                    fixed (void* p = buffer.Data)
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0016", "CMPS0032", "CMPS0035", "CMPS0047");
    }

    [TestMethod]
    public void ForEachStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {                        
                    foreach (int i in buffer.Data)
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0017", "CMPS0047");
    }

    [TestMethod]
    public void LockStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {                        
                    lock (buffer)
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0018", "CMPS0047");
    }

    [TestMethod]
    public void QueryExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {               
                    _ = from x in buffer.Data select x;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0019", "CMPS0047");
    }

    [TestMethod]
    public void RangeExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {               
                    _ = buffer.Data[1..];
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0020", "CMPS0047");
    }

    [TestMethod]
    public void RecursivePattern()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {               
                    if (buffer.Data is int[] { Length: 50 } foo)
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0021", "CMPS0047");
    }

    [TestMethod]
    public void RefType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {               
                    ref float x = ref buffer[0];
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0022", "CMPS0047");
    }

    [TestMethod]
    public void RelationalPattern()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {               
                    if (buffer.Data is > 0 and < 10)
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0023", "CMPS0047");
    }

    [TestMethod]
    public void SizeOfExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {               
                    _ = sizeof(float);
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0024", "CMPS0047");
    }

    [TestMethod]
    public void StackAllocArrayCreationExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {               
                    int* p = stackalloc int[10];
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0032", "CMPS0025", "CMPS0047");
    }

    [TestMethod]
    public void ThrowExpressionOrStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {               
                    throw null;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0026", "CMPS0047");
    }

    [TestMethod]
    public void TryStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    try
                    {
                    }
                    catch
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0027", "CMPS0047");
    }

    [TestMethod]
    public void TupleType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    (int, float) foo;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0028", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void UsingDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    using IDisposable foo = null
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0029", "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void UsingStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    using (IDisposable foo = null)
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0029", "CMPS0031", "CMPS0047");
    }

    [TestMethod]
    public void YieldStatement()
    {
        const string source = """
            using System.Collections.Generic;
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public static IEnumerable<float> Fail()
                {
                    yield return 3.14f;
                }

                public void Execute()
                {
                    Fail();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0030", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidObjectDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    string bar;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void PointerType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    int* p;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0032", "CMPS0047");
    }

    [TestMethod]
    public void FunctionPointer()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    delegate*<int, void> f;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0033", "CMPS0047");
    }

    [TestMethod]
    public void UnsafeStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    unsafe
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0034", "CMPS0047");
    }

    [TestMethod]
    public void UnsafeModifier_LocalFunction()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    static unsafe void Fail() { }

                    Fail();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0035", "CMPS0047");
    }

    [TestMethod]
    public void UnsafeModifier_StaticMethod()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public static unsafe void Fail() { }

                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    Fail();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0035", "CMPS0047");
    }

    [TestMethod]
    public void StringLiteralExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    _ = "Hello world";
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0036", "CMPS0047");
    }

    [TestMethod]
    public void NonConstantMatrixSwizzledIndex()
    {
        const string source = """
            using ComputeSharp;
            using static ComputeSharp.MatrixIndex;
            using float4 = ComputeSharp.Float4;
            using float4x4 = ComputeSharp.Float4x4;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    float4x4 m = default;
                    MatrixIndex index = M12;
                    float4 = m[M11, index, M13, M11];
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0037", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidShaderStaticFieldType()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                private static string Pi = ""Hello"";

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0038", "CMPS0047");
    }

    [TestMethod]
    public void PropertyDeclaration_AutoProp()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                private static float Pi { get; } = 3.14f;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void PropertyDeclaration_AutoProp_WithExplicitInterfaceImplementation()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            public interface IFoo
            {
                float Pi { get; }
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader, IFoo
            {
                public ReadWriteBuffer<float> buffer;

                float IFoo.Pi { get; } = 3.14f;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void PropertyDeclaration_GetterBlock()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                private static float Pi
                {
                    get => 3.14f;
                }

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void StaticPropertyDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                private static float Pi { get; set; } = 3.14f;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void InstancePropertyDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                private float Pi => 3.14f;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void ShaderDispatchDataSizeExceeded()
    {
        const string source = """
            using ComputeSharp;
            using double4x4 = ComputeSharp.Double4x4;
            using float4 = ComputeSharp.Float4;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public readonly ReadWriteBuffer<float> buffer;
                public readonly double4x4 a;
                public readonly double4x4 b;
                public readonly int c;
                public readonly float4 d;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0041", "CMPS0047");
    }

    [TestMethod]
    [DataRow(-1, 1, 1)]
    [DataRow(1, -1, 1)]
    [DataRow(1, 1, -1)]
    [DataRow(1050, 1, 1)]
    [DataRow(1, 1050, 1)]
    [DataRow(1, 1, 70)]
    public void InvalidThreadGroupSizeValues(int threadsX, int threadsY, int threadsZ)
    {
        string source = $$"""
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            [ThreadGroupSize({{threadsX}}, {{threadsY}}, {{threadsZ}})]
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0044");
    }

    [TestMethod]
    public void InvalidThreadGroupSizeDispatchSize_Flags()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            [ThreadGroupSize(default)]
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0048");
    }

    [TestMethod]
    public void InvalidThreadGroupSizeDispatchSize_ExplicitValue()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            [ThreadGroupSize((DefaultThreadGroupSizes)243712)]
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0048");
    }

    [TestMethod]
    public void InvalidThreadGroupSizeDispatchSize_ExplicitValue_Negative()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            [ThreadGroupSize((DefaultThreadGroupSizes)(-289))]
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0048");
    }

    [TestMethod]
    public void InvalidMethodCall()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    buffer[0] = Convert.ToSingle(42);
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0049", "CMPS0047");
    }

    [TestMethod]
    public void InvalidMethodCall_SystemMath()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    buffer[0] = Math.Abs(42);
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0063", "CMPS0047");
    }

    [TestMethod]
    public void InvalidDiscoveredType_Primitive()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    buffer[0] = (byte)42;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidDiscoveredType_CustomType()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            public struct Foo
            {
                public DateTime bar;
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public readonly ReadWriteBuffer<float> buffer;

                public void Execute()
                {
                    Foo foo = default;
                    buffer[0] = 42;
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidDiscoveredType_CustomType_SystemBoolean()
    {
        string source = """
            using System;
            using ComputeSharp;

            namespace MyFancyApp.Sample;

            public struct Foo
            {
                public bool bar;
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public readonly ReadWriteBuffer<Foo> buffer;

                public void Execute()
                {
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0050");
    }

    // See https://github.com/Sergio0694/ComputeSharp/issues/690
    [TestMethod]
    public void InvalidInitializerExpression_ObjectCreation()
    {
        const string source = """
            using ComputeSharp;

            internal struct StructType
            {
                public float Field;
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<StructType> buffer;

                public void Execute()
                {
                    buffer[0] = new StructType { Age = 12 };
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0059");
    }

    [TestMethod]
    public void InvalidInitializerExpression_ImplicitObjectCreation()
    {
        const string source = """
            using ComputeSharp;

            internal struct StructType
            {
                public float Field;
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<StructType> buffer;

                public void Execute()
                {
                    buffer[0] = new() { Age = 12 };
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0059");
    }

    [TestMethod]
    public void InvalidInitializerExpression_CollectionInitializer()
    {
        const string source = """
            using ComputeSharp;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<int> buffer;

                public void Execute()
                {
                    int[] numbers = { 1, 2, 3, 4 };
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0031", "CMPS0047", "CMPS0059");
    }

    [TestMethod]
    public void InvalidCollectionExpression()
    {
        const string source = """
            using ComputeSharp;
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<int> buffer;

                public void Execute()
                {
                    int[] numbers = [1, 2, 3, 4];
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0031", "CMPS0047", "CMPS0060");
    }

    [TestMethod]
    public void InvalidBaseConstructorDeclaration()
    {
        const string source = """
            using ComputeSharp;

            public struct MyStruct
            {
                public MyStruct(int x)
                {
                }

                public MyStruct(float x)
                    : this((int)x)
                {
                }
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<int> buffer;

                public void Execute()
                {
                    MyStruct value = new(3.14f);
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0061");
    }

    [TestMethod]
    public void InvalidMethodOrConstructorCall_PrimaryConstructor()
    {
        const string source = """
            using ComputeSharp;

            public struct MyStruct(int x)
            {
                public int PlusOne()
                {
                    return x + 1;
                }
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<int> buffer;

                public void Execute()
                {
                    buffer[ThreadIds.X] = new MyStruct(ThreadIds.X).PlusOne();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0049");
    }

    [TestMethod]
    public void InvalidThisExpression_Return()
    {
        const string source = """
            using ComputeSharp;

            public struct MyStruct
            {
                public int X;

                public MyStruct Copy()
                {
                    return this;
                }
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<int> buffer;

                public void Execute()
                {
                    MyStruct x = (MyStruct)x;
                    MyStruct y = x.Copy();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0062");
    }

    [TestMethod]
    public void InvalidThisExpression_Argument()
    {
        const string source = """
            using ComputeSharp;

            public struct MyStruct
            {
                public int X;

                public int Read()
                {
                    return Extract(this);
                }

                public static int Extract(MyStruct value)
                {
                    return value.X;
                }
            }
            
            [GeneratedComputeShaderDescriptor]
            public partial struct MyShader : IComputeShader
            {
                public ReadWriteBuffer<int> buffer;

                public void Execute()
                {
                    MyStruct x = (MyStruct)x;
                    int y = x.Read();
                }
            }
            """;

        VerifyGeneratedDiagnostics<ComputeShaderDescriptorGenerator>(source, "CMPS0047", "CMPS0062");
    }

    /// <summary>
    /// Verifies the output of a source generator.
    /// </summary>
    /// <typeparam name="TGenerator">The generator type to use.</typeparam>
    /// <param name="source">The input source to process.</param>
    /// <param name="diagnosticsIds">The expected diagnostics ids to be generated.</param>
    private static void VerifyGeneratedDiagnostics<TGenerator>(string source, params string[] diagnosticsIds)
        where TGenerator : class, IIncrementalGenerator, new()
    {
        // Get all assembly references for the .NET TFM and ComputeSharp
        IEnumerable<MetadataReference> metadataReferences =
        [
            .. Net80.References.All,
            MetadataReference.CreateFromFile(typeof(Core::ComputeSharp.Hlsl).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(D3D12::ComputeSharp.IComputeShader).Assembly.Location)
        ];

        // Parse the source text (C# 12)
        SyntaxTree sourceTree = CSharpSyntaxTree.ParseText(
            source,
            CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp12));

        // Create the original compilation
        CSharpCompilation compilation = CSharpCompilation.Create(
            "original",
            [sourceTree],
            metadataReferences,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true));

        // Run the generator on the source compilation, get the diagnostics
        _ = CSharpGeneratorDriver.Create(new TGenerator()).RunGeneratorsAndUpdateCompilation(
            compilation,
            out Compilation outputCompilation,
            out ImmutableArray<Diagnostic> diagnostics);

        Dictionary<string, Diagnostic> diagnosticMap = diagnostics.DistinctBy(diagnostic => diagnostic.Id).ToDictionary(diagnostic => diagnostic.Id);

        // Check that the diagnostics match
        Assert.IsTrue(diagnosticMap.Keys.ToHashSet().SetEquals(diagnosticsIds), $"Diagnostics didn't match. {string.Join(", ", diagnosticMap.Values)}");

        // If the compilation was supposed to succeed, ensure that no further errors were generated
        if (diagnosticsIds.Length == 0)
        {
            // Compute diagnostics for the final compiled output (just include errors)
            List<Diagnostic> outputCompilationDiagnostics = outputCompilation.GetDiagnostics().Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error).ToList();

            Assert.IsTrue(outputCompilationDiagnostics.Count == 0, $"resultingIds: {string.Join(", ", outputCompilationDiagnostics)}");
        }
    }
}