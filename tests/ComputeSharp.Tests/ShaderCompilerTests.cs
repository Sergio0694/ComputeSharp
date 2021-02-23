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
            ReflectionServices.GetShaderInfo<SoftmaxActivation>(out _);
        }

        [AutoConstructor]
        public readonly partial struct SoftmaxActivation : IComputeShader
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
            ReflectionServices.GetShaderInfo<FloatReturnType>(out _);
        }

        [AutoConstructor]
        public readonly partial struct FloatReturnType : IComputeShader
        {
            public readonly ReadWriteBuffer<Float2> buffer;

            Float2 Foo(float x) => x;

            public void Execute()
            {
                static Float3 Bar(float x) => x;

                buffer[ThreadIds.X] = Foo(ThreadIds.X) + Bar(ThreadIds.X).XY;
            }
        }
    }
}
