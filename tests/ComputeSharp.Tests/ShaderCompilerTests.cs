using System.Threading;
using ComputeSharp.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("ShaderCompiler")]
    public partial class ShaderCompilerTests
    {
        [TestMethod]
        public void ReservedKeywords()
        {
            ReflectionServices.GetShaderInfo<ReservedKeywords_Kernel>(out _);
        }

        [AutoConstructor]
        public readonly partial struct ReservedKeywords_Kernel : IComputeShader
        {
            public readonly ReadWriteBuffer<float> row_major;
            public readonly float dword;

            public void Execute()
            {
                float exp = Hlsl.Exp(dword * row_major[ThreadIds.X]);
                float log = Hlsl.Log(1 + exp);

                row_major[ThreadIds.X] = log / dword;
            }
        }

        [TestMethod]
        public void SpecialTypeAsReturnType()
        {
            ReflectionServices.GetShaderInfo<SpecialTypeAsReturnType_Kernel>(out _);
        }

        [AutoConstructor]
        public readonly partial struct SpecialTypeAsReturnType_Kernel : IComputeShader
        {
            public readonly ReadWriteBuffer<Float2> buffer;

            Float2 Foo(float x) => x;

            public void Execute()
            {
                static Float3 Bar(float x) => x;

                buffer[ThreadIds.X] = Foo(ThreadIds.X) + Bar(ThreadIds.X).XY;
            }
        }

        [TestMethod]
        public void LocalFunctionInExternalMethods()
        {
            ReflectionServices.GetShaderInfo<LocalFunctionInExternalMethods_Kernel>(out _);
        }

        [AutoConstructor]
        public readonly partial struct LocalFunctionInExternalMethods_Kernel : IComputeShader
        {
            public readonly ReadWriteBuffer<Float2> buffer;

            Float2 Foo(float x)
            {
                static Float2 Baz(float y) => y;

                return Baz(x);
            }

            public void Execute()
            {
                buffer[ThreadIds.X] = Foo(ThreadIds.X);
            }
        }
    }
}
