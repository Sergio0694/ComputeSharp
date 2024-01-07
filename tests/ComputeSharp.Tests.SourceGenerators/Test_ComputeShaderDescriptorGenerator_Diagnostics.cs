using ComputeSharp.SourceGenerators;
using ComputeSharp.Tests.SourceGenerators.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.SourceGenerators;

[TestClass]
public class Test_ComputeShaderDescriptorGenerator_Diagnostics
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0001");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0001");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0005");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, diagnosticsId);
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, diagnosticsId);
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0006");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0010", "CMPS0031", "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0010", "CMPS0031", "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0011", "CMPS0031", "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0012");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0012");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0012", "CMPS0013");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0014");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0015");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0016", "CMPS0032", "CMPS0035");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0017");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0018");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0019");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0020");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0021");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0022");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0023");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0024");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0032", "CMPS0025");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0026");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0027");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0028", "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0029", "CMPS0031", "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0029", "CMPS0031");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0030", "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0031", "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0032");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0033");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0034");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0035");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0035");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0036");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0037", "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0038");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0040");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0040");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0040");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0040");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0040");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0049");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0063");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0050");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0059");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0059");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0031", "CMPS0059");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0031", "CMPS0060");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0061");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0049");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0062");
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

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0062");
    }

    [TestMethod]
    public void RecursiveMembers()
    {
        const string source = """
            using ComputeSharp;

            internal static class ClassWithRecursiveMembers
            {
                public static int A(int x)
                {
                    return B(x);
                }

                public static int B(int x)
                {
                    if (x <= 0)
                    {
                        return 0;
                    }

                    return A(x - 1);
                }
            }

            public struct StructTypeWithRecursiveMembers
            {
                public int x;

                public int Fib()
                {
                    if (x <= 0)
                    {
                        return x;
                    }

                    x--;

                    return Fib();
                }
            }

            [ThreadGroupSize(DefaultThreadGroupSizes.X)]
            [GeneratedComputeShaderDescriptor]
            internal readonly struct ShaderWithRecursiveMembers : IComputeShader
            {
                public readonly ReadWriteBuffer<int> results;

                public void Execute()
                {
                    results[0] = ClassWithRecursiveMembers.A(42);

                    StructTypeWithRecursiveMembers instance;
                    instance.x = 42;

                    results[1] = instance.Fib();
                }
            }
            """;

        // This should be correctly handled by the HLSL rewriter, and not cause a StackOverflowException.
        // The HLSL compilation is expected to fail because recursion is not actually supported:
        //
        // "error: recursive functions not allowed"
        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0046");
    }

    [TestMethod]
    public void MissingRequiresDoublePrecisionSupportAttribute()
    {
        const string source = """
            using ComputeSharp;

            namespace MyNamespace;

            [ThreadGroupSize(DefaultThreadGroupSizes.X)]
            [GeneratedComputeShaderDescriptor]
            internal readonly partial struct MyShader : IComputeShader
            {
                private readonly ReadWriteBuffer<float> results;
                private readonly float time;

                public void Execute()
                {
                    results[ThreadIds.X] = (float)(Hlsl.Abs(time * 2.0));
                }
            }
            """;

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0064");
    }

    [TestMethod]
    public void UnnecessaryRequiresDoublePrecisionSupportAttribute()
    {
        const string source = """
            using ComputeSharp;

            namespace MyNamespace;

            [ThreadGroupSize(DefaultThreadGroupSizes.X)]
            [RequiresDoublePrecisionSupport]
            [GeneratedComputeShaderDescriptor]
            internal readonly partial struct MyShader : IComputeShader
            {
                private readonly ReadWriteBuffer<float> results;
                private readonly float time;

                public void Execute()
                {
                    results[ThreadIds.X] = (float)(time * 2.0f);
                }
            }
            """;

        CSharpGeneratorTest<ComputeShaderDescriptorGenerator>.VerifyDiagnostics(source, "CMPS0065");
    }
}