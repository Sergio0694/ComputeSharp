using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("SourceGenerator")]
    public class SourceGeneratorTests
    {
        [TestMethod]
        public void SequentialEnum()
        {
            string source = @"
            using System.Numerics;
            using MyLibrary;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public readonly struct ThreadIds
                {
                    public int X => throw null;
                    public int Y => throw null;
                    public int Z => throw null;
                }

                public interface IComputeShader
                {
                    void Execute((int X, int Y, int Z) ids);
                }
            }

            namespace MyLibrary
            {
                public class Buffer<T>
                {
                }

                public interface IFluff
                {
                }
            }

            namespace MyFancyApp.Sample
            {
                public partial interface IFoo<T> : IFluff
                    where T : notnull, new()
                {
                    public partial class Foo
                    {
                        [AutoConstructor]
                        public readonly partial struct MyShader : IComputeShader
                        {
                            private readonly Buffer<Vector4> A;
                            private readonly Vector4 B;

                            /// <inheritdoc/>
                            public void Execute(ThreadIds ids)
                            {
                                Vector4 foo = default;
                                Vector4 bar = default(Vector4);
                                Vector4 baz = B;
                                A[ids.X] = (Vector4)foo + bar + baz;
                            }
                        }
                    }
                }
            }";

            string expectedForAutoConstructor = @"
            namespace MyFancyApp.Sample
            {
                public partial interface IFoo<T>
                {
                    public partial class Foo
                    {
                        public readonly partial struct MyShader
                        {
                            public MyShader(MyLibrary.Buffer<System.Numerics.Vector4> A, System.Numerics.Vector4 B)
                            {
                                this.A = A;
                                this.B = B;
                            }
                        }
                    }
                }
            }";

            VerifyGeneratedMethodLines<AutoConstructorAttributeGenerator>(source, 0, expectedForAutoConstructor);

            string expectedForShaderSource = @"
            #pragma warning disable
            [assembly: ComputeSharp.IComputeShaderSource(""MyFancyApp.Sample.IFoo`1+Foo+MyShader"", ""Execute"", ""void CSMain(uint3 ids : SV_DispatchThreadId)\r\n{\r\n    if (ids.x < __x && ids.y < __y && ids.z < __z)\r\n    {\r\n        float4 foo = (float4)0;\r\n        float4 bar = (float4)0;\r\n        float4 baz = B;\r\n        A[ids.x] = (float4)foo + bar + baz;\r\n    }\r\n}"")]";

            VerifyGeneratedMethodLines<IComputeShaderSourceAttributeGenerator>(source, 0, expectedForShaderSource);
        }

        /// <summary>
        /// Verifies the output of a source generator.
        /// </summary>
        /// <typeparam name="TGenerator">The generator type to use.</typeparam>
        /// <param name="source">The input source to process.</param>
        /// <param name="index">The target index to check in the resulting output.</param>
        /// <param name="expectedBody">The expected body to compare with the generated code.</param>
        private void VerifyGeneratedMethodLines<TGenerator>(string source, int index, string expectedBody)
            where TGenerator : class, ISourceGenerator, new()
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

            IEnumerable<MetadataReference> references =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                where !assembly.IsDynamic
                let reference = MetadataReference.CreateFromFile(assembly.Location)
                select reference;

            CSharpCompilation compilation = CSharpCompilation.Create("original", new SyntaxTree[] { syntaxTree }, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            ISourceGenerator generator = new TGenerator();

            CSharpGeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            driver.RunGeneratorsAndUpdateCompilation(compilation, out Compilation outputCompilation, out ImmutableArray<Diagnostic> diagnostics);

            string outputBody = outputCompilation.SyntaxTrees.Skip(index + 1).First().ToString();

            string[]
                outputLines = outputBody.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                expectedLines = expectedBody.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            Assert.AreEqual(expectedLines.Length, outputLines.Length);

            for (int i = 0; i < outputLines.Length; i++)
            {
                Assert.AreEqual(expectedLines[i], outputLines[i]);
            }
        }
    }
}