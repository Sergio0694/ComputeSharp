using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComputeSharp.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.SourceGenerators
{
    [TestClass]
    [TestCategory("AutoConstructorGenerator")]
    public class AutoConstructorGeneratorTests
    {
        [TestMethod]
        public void GenerateConstructor()
        {
            string source = @"
            using System.Numerics;
            using ComputeSharp;

            namespace ComputeSharp
            {
                public class ReadWriteBuffer<T> { }
            }

            namespace MyFancyApp.Sample
            {
                public partial interface IFoo<T> : IFluff
                    where T : notnull, new()
                {
                    public partial class Foo
                    {
                        [AutoConstructor]
                        public readonly partial struct Test
                        {
                            private readonly float a;
                            private readonly Vector2 b;
                            public readonly ReadWriteBuffer<Vector4> c;
                            public readonly ReadWriteBuffer<int> d;
                        }
                    }
                }
            }";

            string expected = @"
            namespace MyFancyApp.Sample
            {
                public partial interface IFoo<T>
                {
                    public partial class Foo
                    {
                        public readonly partial struct Test
                        {
                            public Test(float a, System.Numerics.Vector2 b, ComputeSharp.ReadWriteBuffer<System.Numerics.Vector4> c, ComputeSharp.ReadWriteBuffer<int> d)
                            {
                                this.a = a;
                                this.b = b;
                                this.c = c;
                                this.d = d;
                            }
                        }
                    }
                }
            }";

            VerifyGeneratedMethodLines<AutoConstructorGenerator>(source, 0, expected);
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