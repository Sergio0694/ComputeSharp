using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.SourceGenerators;

[TestClass]
[TestCategory("Diagnostics")]
public class DiagnosticsTests
{
    [TestMethod]
    public void InvalidShaderField()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;
                    public string text;

                    public void Execute() { }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0001", "CMPS0047");
    }

    [TestMethod]
    public void InvalidShaderFixedField()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public unsafe struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;
                    public fixed int text[16];

                    public void Execute() { }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0001", "CMPS0047");
    }

    [TestMethod]
    public void InvalidGroupSharedFieldType()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
                public class GroupSharedAttribute : Attribute { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    [GroupShared]
                    public static string text;

                    public void Execute() { }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0002", "CMPS0047");
    }

    [TestMethod]
    public void InvalidGroupSharedFieldElementType()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
                public class GroupSharedAttribute : Attribute { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    [GroupShared]
                    public static string[] text;

                    public void Execute() { }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0003", "CMPS0047");
    }

    [TestMethod]
    public void InvalidGroupSharedFieldDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
                public class GroupSharedAttribute : Attribute { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    [GroupShared]
                    public ReadWriteBuffer<float> buffer;

                    public void Execute() { }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0004", "CMPS0047");
    }

    [TestMethod]
    public void MissingShaderResources()
    {
        const string source = """
            using ComputeSharp;

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public void Execute() { }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0005", "CMPS0047");
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

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
                public static class {{typeName}} { public static int X => 0; }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        static int Fail() => {{typeName}}.X;

                        Fail();
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, diagnosticsId, "CMPS0047");
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

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
                public static class {{typeName}} { public static int X => 0; }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public static int Fail() => {{typeName}}.X;

                    public void Execute() => Fail();
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, diagnosticsId, "CMPS0047");
    }

    [TestMethod]
    public void InvalidThreadIdsNormalizedUsage()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
                public static class ThreadIds { public static class Normalized { public static int X => 0; } }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public static int Fail() => ThreadIds.Normalized.X;

                    public void Execute()
                    {
                        buffer[0] = Fail();
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0006", "CMPS0047");
    }

    [TestMethod]
    public void InvalidObjectCreationExpression_Explicit()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        object o = new object();
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0010", "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidObjectCreationExpression_Implicit()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        object o = new();
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0010", "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void AnonymousObjectCreationExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        var o = new { Age = 12 };
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0011", "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void AsyncModifierOnMethodOrFunction_LocalFunction()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        static async void Fail() { }

                        Fail();
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0012", "CMPS0047");
    }

    [TestMethod]
    public void AsyncModifierOnMethodOrFunction_StaticMethod()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public static async void Fail() { }

                    public void Execute() => Fail();
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0012", "CMPS0047");
    }

    [TestMethod]
    public void AwaitExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public async void Execute()
                    {
                        await null;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0012", "CMPS0013", "CMPS0047");
    }

    [TestMethod]
    [DataRow("checked")]
    [DataRow("unchecked")]
    public void CheckedExpression(string text)
    {
        string source = $$"""
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        int a = {{text}}(1 + 1);
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0014", "CMPS0047");
    }

    [TestMethod]
    [DataRow("checked")]
    [DataRow("unchecked")]
    public void CheckedStatement(string text)
    {
        string source = $$"""
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
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
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0015", "CMPS0047");
    }

    [TestMethod]
    public void FixedStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T>
                {
                    public int[] Data;
                }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {                        
                        fixed (void* p = buffer.Data)
                        {
                        }
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0016", "CMPS0032", "CMPS0047");
    }

    [TestMethod]
    public void ForEachStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T>
                {
                    public int[] Data;
                }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {                        
                        foreach (int i in buffer.Data)
                        {
                        }
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0017", "CMPS0047");
    }

    [TestMethod]
    public void LockStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {                        
                        lock (buffer)
                        {
                        }
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0018", "CMPS0047");
    }

    [TestMethod]
    public void QueryExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T>
                {
                    public int[] Data;
                }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {               
                        _ = from x in buffer.Data select x;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0019", "CMPS0047");
    }

    [TestMethod]
    public void RangeExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T>
                {
                    public int[] Data;
                }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {               
                        _ = buffer.Data[1..];
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0020", "CMPS0047");
    }

    [TestMethod]
    public void RecursivePattern()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T>
                {
                    public object Data;
                }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {               
                        if (buffer.Data is int[] { Length: 50 } foo)
                        {
                        }
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0021", "CMPS0047");
    }

    [TestMethod]
    public void RefType()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T>
                {
                    public ref T this[int x] => throw null;
                }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {               
                        ref float x = ref buffer[0];
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0022", "CMPS0047");
    }

    [TestMethod]
    public void RelationalPattern()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T>
                {
                    public int Data;
                }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {               
                        if (buffer.Data is > 0 and < 10)
                        {
                        }
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0023", "CMPS0047");
    }

    [TestMethod]
    public void SizeOfExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {               
                        _ = sizeof(float);
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0024", "CMPS0047");
    }

    [TestMethod]
    public void StackAllocArrayCreationExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {               
                        int* p = stackalloc int[10];
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0032", "CMPS0025", "CMPS0047");
    }

    [TestMethod]
    public void ThrowExpressionOrStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {               
                        throw null;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0026", "CMPS0047");
    }

    [TestMethod]
    public void TryStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
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
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0027", "CMPS0047");
    }

    [TestMethod]
    public void TupleType()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        (int, float) foo;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0028", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void UsingDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        using IDisposable foo = null
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0029", "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void UsingStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        using (IDisposable foo = null)
                        {
                        }
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0029", "CMPS0031", "CMPS0047");
    }

    [TestMethod]
    public void YieldStatement()
    {
        const string source = """
            using System.Collections.Generic;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
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
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0030", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidObjectDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        string bar;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0031", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void PointerType()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        int* p;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0032", "CMPS0047");
    }

    [TestMethod]
    public void FunctionPointer()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        delegate*<int, void> f;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0033", "CMPS0047");
    }

    [TestMethod]
    public void UnsafeStatement()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        unsafe
                        {
                        }
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0034", "CMPS0047");
    }

    [TestMethod]
    public void UnsafeModifier_LocalFunction()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        static unsafe void Fail() { }

                        Fail();
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0035", "CMPS0047");
    }

    [TestMethod]
    public void UnsafeModifier_StaticMethod()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public static unsafe void Fail() { }

                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        Fail();
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0035", "CMPS0047");
    }

    [TestMethod]
    public void StringLiteralExpression()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        _ = "Hello world";
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0036", "CMPS0047");
    }

    [TestMethod]
    public void NonConstantMatrixSwizzledIndex()
    {
        const string source = """
            using ComputeSharp;
            using static ComputeSharp.MatrixIndex;
            using float4 = ComputeSharp.Float4;
            using float4x4 = ComputeSharp.Float4x4;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
                public enum MatrixIndex
                {
                    M11,
                    M12,
                    M13
                },
                public struct Float4 { }
                public struct Float4x4
                {
                    public Float4 this[MatrixIndex m0, MatrixIndex m1, MatrixIndex m2, MatrixIndex m3] => default;
                }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        float4x4 m = default;
                        MatrixIndex index = M12;
                        float4 = m[M11, index, M13, M11];
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0037", "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidShaderStaticFieldType()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    private static string Pi = ""Hello"";

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0038", "CMPS0047");
    }

    [TestMethod]
    public void PropertyDeclaration_AutoProp()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    private static float Pi { get; } = 3.14f;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void PropertyDeclaration_AutoProp_WithExplicitInterfaceImplementation()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public interface IFoo
                {
                    float Pi { get; }
                }

                public struct MyShader : IComputeShader, IFoo
                {
                    public ReadWriteBuffer<float> buffer;

                    float IFoo.Pi { get; } = 3.14f;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void PropertyDeclaration_GetterBlock()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
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
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void StaticPropertyDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    private static float Pi { get; set; } = 3.14f;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void InstancePropertyDeclaration()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    private float Pi => 3.14f;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0040", "CMPS0047");
    }

    [TestMethod]
    public void ShaderDispatchDataSizeExceeded()
    {
        const string source = """
            using ComputeSharp;
            using double4x4 = ComputeSharp.Double4x4;
            using float4 = ComputeSharp.Float4;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
                public struct Double4x4 { }
                public struct Float4 { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
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
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0041", "CMPS0047");
    }

    [TestMethod]
    public void MultipleShaderTypes()
    {
        const string source = """
            using ComputeSharp;

            namespace ComputeSharp
            {
                public interface IComputeShader { }
                public interface IPixelShader<TPixel> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader, IPixelShader<float4>
                {
                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0042", "CMPS0047");
    }

    [TestMethod]
    public void EmbeddedBytecodeWithDynamicShader()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public interface IComputeShader { }
                public class ReadWriteBuffer<T> { }
                public class EmbeddedBytecodeAttribute : Attribute
                {
                    public EmbeddedBytecodeAttribute(int threadsX, int threadsY, int threadsZ) { }
                }
            }

            namespace MyFancyApp.Sample
            {
                [EmbeddedBytecode(8, 8, 1)]
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;
                    public Action action;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0043");
    }

    [TestMethod]
    [DataRow(-1, 1, 1)]
    [DataRow(1, -1, 1)]
    [DataRow(1, 1, -1)]
    [DataRow(1050, 1, 1)]
    [DataRow(1, 1050, 1)]
    [DataRow(1, 1, 70)]
    public void InvalidEmbeddedBytecodeThreadIds(int threadsX, int threadsY, int threadsZ)
    {
        string source = $$"""
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public interface IComputeShader { }
                public class ReadWriteBuffer<T> { }
                public class EmbeddedBytecodeAttribute : Attribute
                {
                    public EmbeddedBytecodeAttribute(int threadsX, int threadsY, int threadsZ) { }
                }
            }

            namespace MyFancyApp.Sample
            {
                [EmbeddedBytecode({{threadsX}}, {{threadsY}}, {{threadsZ}})]
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0044");
    }

    [TestMethod]
    public void InvalidEmbeddedBytecodeDispatchSize_Flags()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public interface IComputeShader { }
                public class ReadWriteBuffer<T> { }
                public enum DispatchAxis { X, Y, Z, XY, XZ, YZ, XYZ }
                public class EmbeddedBytecodeAttribute : Attribute
                {
                    public EmbeddedBytecodeAttribute(DispatchAxis dispatchAxis) { }
                }
            }

            namespace MyFancyApp.Sample
            {
                [EmbeddedBytecode(DispatchAxis.XYZ | DispatchAxis.Y)]
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0048");
    }

    [TestMethod]
    public void InvalidEmbeddedBytecodeDispatchSize_ExplicitValue()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public interface IComputeShader { }
                public class ReadWriteBuffer<T> { }
                public enum DispatchAxis { X, Y, Z, XY, XZ, YZ, XYZ }
                public class EmbeddedBytecodeAttribute : Attribute
                {
                    public EmbeddedBytecodeAttribute(DispatchAxis dispatchAxis) { }
                }
            }

            namespace MyFancyApp.Sample
            {
                [EmbeddedBytecode((DispatchAxis)243712)]
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0048");
    }

    [TestMethod]
    public void InvalidEmbeddedBytecodeDispatchSize_ExplicitValue_Negative()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public interface IComputeShader { }
                public class ReadWriteBuffer<T> { }
                public enum DispatchAxis { X, Y, Z, XY, XZ, YZ, XYZ }
                public class EmbeddedBytecodeAttribute : Attribute
                {
                    public EmbeddedBytecodeAttribute(DispatchAxis dispatchAxis) { }
                }
            }

            namespace MyFancyApp.Sample
            {
                [EmbeddedBytecode((DispatchAxis)(-289))]
                public struct MyShader : IComputeShader
                {
                    public ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0048");
    }

    [TestMethod]
    public void InvalidMethodCall()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public readonly ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        buffer[0] = Convert.ToSingle(42);
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0049", "CMPS0047");
    }

    [TestMethod]
    public void InvalidDiscoveredType_Primitive()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct MyShader : IComputeShader
                {
                    public readonly ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        buffer[0] = (byte)42;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidDiscoveredType_CustomType()
    {
        const string source = """
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct Foo
                {
                    public DateTime bar;
                }

                public struct MyShader : IComputeShader
                {
                    public readonly ReadWriteBuffer<float> buffer;

                    public void Execute()
                    {
                        Foo foo = default;
                        buffer[0] = 42;
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0047", "CMPS0050");
    }

    [TestMethod]
    public void InvalidDiscoveredType_CustomType_SystemBoolean()
    {
        string source = """
            using System;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public struct Foo
                {
                    public bool bar;
                }

                public struct MyShader : IComputeShader
                {
                    public readonly ReadWriteBuffer<Foo> buffer;

                    public void Execute()
                    {
                    }
                }
            }
            """;

        VerifyGeneratedDiagnostics<IShaderGenerator>(source, "CMPS0047", "CMPS0050");
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
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        IEnumerable<MetadataReference> references =
            from assembly in AppDomain.CurrentDomain.GetAssemblies()
            where !assembly.IsDynamic
            let reference = MetadataReference.CreateFromFile(assembly.Location)
            select reference;

        CSharpCompilation compilation = CSharpCompilation.Create(
            "original",
            new SyntaxTree[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true));

        IIncrementalGenerator generator = new TGenerator();

        CSharpGeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out Compilation outputCompilation, out ImmutableArray<Diagnostic> diagnostics);

        HashSet<string> resultingIds = diagnostics.Select(diagnostic => diagnostic.Id).ToHashSet();

        Assert.IsTrue(resultingIds.SetEquals(diagnosticsIds));
    }
}