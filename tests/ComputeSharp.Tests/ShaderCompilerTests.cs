using ComputeSharp;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0008, IDE0022, IDE0009

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("ShaderCompiler")]
    public partial class ShaderCompilerTests
    {
        [TestMethod]
        public void ReservedKeywords()
        {
            _ = ReflectionServices.GetShaderInfo<ReservedKeywordsShader>();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct ReservedKeywordsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> row_major;
            public readonly float dword;
            public readonly float float2;
            public readonly int int2x2;

            public void Execute()
            {
                float exp = Hlsl.Exp(dword * row_major[ThreadIds.X]);
                float log = Hlsl.Log(1 + exp);

                row_major[ThreadIds.X] = (log / dword) + float2 + int2x2;
            }
        }

        [TestMethod]
        public void ReservedKeywordsInCustomTypes()
        {
            _ = ReflectionServices.GetShaderInfo<ReservedKeywordsInCustomTypesShader>();
        }

        public struct CellData
        {
            public float testX;
            public float testY;
            public uint seed;

            public float distance;
            public readonly float dword;
            public readonly float float2;
            public readonly int int2x2;
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct ReservedKeywordsInCustomTypesShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> row_major;
            public readonly CellData cellData;
            public readonly float dword;
            public readonly float float2;
            public readonly int int2x2;
            public readonly CellData cbuffer;

            public void Execute()
            {
                float exp = Hlsl.Exp(cellData.distance * row_major[ThreadIds.X]);
                float log = Hlsl.Log(1 + exp);
                float temp = log + cellData.dword + cellData.int2x2;

                row_major[ThreadIds.X] = (log / dword) + float2 + int2x2 + cbuffer.float2 + temp;
            }
        }

        // See https://github.com/Sergio0694/ComputeSharp/issues/313
        [TestMethod]
        public void ReservedKeywordsFromHlslTypesAndBuiltInValues()
        {
            _ = ReflectionServices.GetShaderInfo<ReservedKeywordsFromHlslTypesAndBuiltInValuesShader>();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct ReservedKeywordsFromHlslTypesAndBuiltInValuesShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> fragmentKeyword;
            public readonly ReadWriteBuffer<float> compile_fragment;
            public readonly ReadWriteBuffer<float> shaderProfile;
            public readonly ReadWriteBuffer<float> maxvertexcount;
            public readonly ReadWriteBuffer<float> TriangleStream;
            public readonly ReadWriteBuffer<float> Buffer;
            public readonly ReadWriteBuffer<float> ByteAddressBuffer;
            public readonly int ConsumeStructuredBuffer;
            public readonly int RWTexture2D;
            public readonly int Texture2D;
            public readonly int Texture2DArray;
            public readonly int SV_DomainLocation;
            public readonly int SV_GroupIndex;
            public readonly int SV_OutputControlPointID;
            public readonly int SV_StencilRef;

            public void Execute()
            {
                float sum = ConsumeStructuredBuffer + RWTexture2D + Texture2D + Texture2DArray;

                sum += SV_DomainLocation + SV_GroupIndex + SV_OutputControlPointID + SV_StencilRef;

                fragmentKeyword[ThreadIds.X] = sum;
                compile_fragment[ThreadIds.X] = sum;
                shaderProfile[ThreadIds.X] = sum;
                maxvertexcount[ThreadIds.X] = sum;
                TriangleStream[ThreadIds.X] = sum;
                Buffer[ThreadIds.X] = sum;
                ByteAddressBuffer[ThreadIds.X] = sum;
            }
        }

        [TestMethod]
        public void ReservedKeywordsPrecompiled()
        {
            _ = ReflectionServices.GetShaderInfo<ReservedKeywordsPrecompiledShader>();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct ReservedKeywordsPrecompiledShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> row_major;
            public readonly float dword;
            public readonly float float2;
            public readonly int int2x2;
            private readonly float sin;
            private readonly float cos;
            private readonly float scale;
            private readonly float intensity;

            public void Execute()
            {
                float exp = Hlsl.Exp(dword * row_major[ThreadIds.X]);
                float log = Hlsl.Log(1 + exp);

                float s1 = this.cos * exp * this.sin * log;
                float t1 = -this.sin * exp * this.cos * log;

                float s2 = s1 + this.intensity + Hlsl.Tan(s1 * this.scale);
                float t2 = t1 + this.intensity + Hlsl.Tan(t1 * this.scale);

                float u2 = (this.cos * s2) - (this.sin * t2);
                float v2 = (this.sin * s2) - (this.cos * t2);

                row_major[ThreadIds.X] = (log / dword) + float2 + int2x2 + u2 + v2;
            }
        }

        [TestMethod]
        public void SpecialTypeAsReturnType()
        {
            _ = ReflectionServices.GetShaderInfo<SpecialTypeAsReturnTypeShader>();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct SpecialTypeAsReturnTypeShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float2> buffer;

            float2 Foo(float x) => x;

            public void Execute()
            {
                static float3 Bar(float x) => x;

                buffer[ThreadIds.X] = Foo(ThreadIds.X) + Bar(ThreadIds.X).XY;
            }
        }

        [TestMethod]
        public void LocalFunctionInExternalMethods()
        {
            _ = ReflectionServices.GetShaderInfo<LocalFunctionInExternalMethodsShader>();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct LocalFunctionInExternalMethodsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float2> buffer;

            float2 Foo(float x)
            {
                static float2 Baz(float y) => y;

                return Baz(x);
            }

            public void Execute()
            {
                buffer[ThreadIds.X] = Foo(ThreadIds.X);
            }
        }

        [TestMethod]
        public void CapturedNestedStructType()
        {
            _ = ReflectionServices.GetShaderInfo<CapturedNestedStructTypeShader>();
        }

        [AutoConstructor]
        public partial struct CustomStructType
        {
            public float2 a;
            public int b;
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct CapturedNestedStructTypeShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;
            public readonly CustomStructType foo;

            /// <inheritdoc/>
            public void Execute()
            {
                buffer[ThreadIds.X] *= foo.a.X + foo.b;
            }
        }

        [TestMethod]
        public void ExternalStructType_Ok()
        {
            _ = ReflectionServices.GetShaderInfo<ExternalStructTypeShader>();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct ExternalStructTypeShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            /// <inheritdoc/>
            public void Execute()
            {
                float value = buffer[ThreadIds.X];
                ExternalStructType type = ExternalStructType.New((int)value, Hlsl.Abs(value));

                buffer[ThreadIds.X] = ExternalStructType.Sum(type);
            }
        }

        [TestMethod]
        public void OutOfOrderMethods()
        {
            _ = ReflectionServices.GetShaderInfo<OutOfOrderMethodsShader>();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct OutOfOrderMethodsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            static int A() => B();

            static int B() => 1 + C();

            static int C() => 1;

            int D() => A() + E() + F();

            int E() => 1;

            static int F() => 1;

            /// <inheritdoc/>
            public void Execute()
            {
                float value = buffer[ThreadIds.X];
                ExternalStructType type = ExternalStructType.New((int)value, Hlsl.Abs(value));

                buffer[ThreadIds.X] = ExternalStructType.Sum(type);
            }
        }

        [TestMethod]
        public void PixelShader()
        {
            ShaderInfo info = ReflectionServices.GetShaderInfo<StatelessPixelShader, float4>();

            Assert.AreEqual(info.TextureStoreInstructionCount, 1u);
            Assert.AreEqual(info.BoundResourceCount, 2u);
        }

        [EmbeddedBytecode(DispatchAxis.XY)]
        public readonly partial struct StatelessPixelShader : IComputeShader<float4>
        {
            /// <inheritdoc/>
            public float4 Execute()
            {
                return new(1, 1, 1, 1);
            }
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct LoopWithVarCounterShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            /// <inheritdoc/>
            public void Execute()
            {
                for (var i = 0; i < 10; i++)
                {
                    buffer[(ThreadIds.X * 10) + i] = i;
                }
            }
        }

        [TestMethod]
        public void LoopWithVarCounter()
        {
            _ = ReflectionServices.GetShaderInfo<LoopWithVarCounterShader>();
        }

        [TestMethod]
        public void DoublePrecisionSupport()
        {
            ShaderInfo info = ReflectionServices.GetShaderInfo<DoublePrecisionSupportShader>();

            Assert.IsTrue(info.RequiresDoublePrecisionSupport);
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct DoublePrecisionSupportShader : IComputeShader
        {
            public readonly ReadWriteBuffer<double> buffer;
            public readonly double factor;

            /// <inheritdoc/>
            public void Execute()
            {
                buffer[ThreadIds.X] *= factor + 3.14;
            }
        }

        [TestMethod]
        public void FieldAccessWithThisExpression()
        {
            _ = ReflectionServices.GetShaderInfo<FieldAccessWithThisExpressionShader>();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        internal readonly partial struct FieldAccessWithThisExpressionShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;
            public readonly float number;

            /// <inheritdoc/>
            public void Execute()
            {
                this.buffer[ThreadIds.X] *= this.number;
            }
        }

        [TestMethod]
        public void ComputeShaderWithInheritedShaderInterface()
        {
            _ = ReflectionServices.GetShaderInfo<ComputeShaderWithInheritedShaderInterfaceShader>();
        }

        public interface IMyBaseShader : IComputeShader
        {
            public int A { get; }

            public void B();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        internal readonly partial struct ComputeShaderWithInheritedShaderInterfaceShader : IMyBaseShader
        {
            int IMyBaseShader.A => 42;

            void IMyBaseShader.B()
            {
            }

            public readonly ReadWriteBuffer<float> buffer;
            public readonly float number;

            /// <inheritdoc/>
            public void Execute()
            {
                this.buffer[ThreadIds.X] *= this.number;
            }
        }

        [TestMethod]
        public void PixelShaderWithInheritedShaderInterface()
        {
            _ = ReflectionServices.GetShaderInfo<PixelShaderWithInheritedShaderInterfaceShader, float4>();
        }

        public interface IMyBaseShader<T> : IComputeShader<T>
            where T : unmanaged
        {
            public int A { get; }

            public void B();
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        internal readonly partial struct PixelShaderWithInheritedShaderInterfaceShader : IMyBaseShader<float4>
        {
            int IMyBaseShader<float4>.A => 42;

            void IMyBaseShader<float4>.B()
            {
            }

            public readonly float number;

            /// <inheritdoc/>
            public float4 Execute()
            {
                return default;
            }
        }

        [TestMethod]
        public void StructInstanceMethods()
        {
            _ = ReflectionServices.GetShaderInfo<StructInstanceMethodsShader>();
        }

        public struct MyStructTypeA
        {
            public int A;
            public float B;

            public float Sum()
            {
                return A + Bar();
            }

            public float Bar() => this.B;
        }

        public struct MyStructTypeB
        {
            public MyStructTypeA A;
            public float B;

            public float Combine()
            {
                return A.Sum() + this.B;
            }
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        internal readonly partial struct StructInstanceMethodsShader : IComputeShader
        {
            public readonly MyStructTypeA a;
            public readonly MyStructTypeB b;
            public readonly ReadWriteBuffer<MyStructTypeA> bufferA;
            public readonly ReadWriteBuffer<MyStructTypeB> bufferB;
            public readonly ReadWriteBuffer<float> results;

            /// <inheritdoc/>
            public void Execute()
            {
                float result1 = a.Sum() + a.Bar();
                float result2 = b.Combine();

                results[ThreadIds.X] = result1 + result2 + bufferA[ThreadIds.X].Sum() + bufferB[0].Combine();
            }
        }

        [TestMethod]
        public void PixelShaderWithScopedParameterInMethods()
        {
            _ = ReflectionServices.GetShaderInfo<PixelShaderWithScopedParameterInMethodsShader>();
        }

        internal static class HelpersForPixelShaderWithScopedParameterInMethods
        {
            public static void Baz(scoped in float a, scoped ref float b)
            {
                b = a;
            }
        }

        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        internal readonly partial struct PixelShaderWithScopedParameterInMethodsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;
            public readonly float number;

            private static void Foo(scoped ref float a, ref float b)
            {
                b = a;
            }

            private void Bar(scoped ref float a, scoped ref float b)
            {
                b = a;
            }

            /// <inheritdoc/>
            public void Execute()
            {
                float number = this.number;

                Foo(ref this.buffer[ThreadIds.X], ref number);
                Bar(ref this.buffer[ThreadIds.X], ref number);
                HelpersForPixelShaderWithScopedParameterInMethods.Baz(in this.buffer[ThreadIds.X], ref number);

                this.buffer[ThreadIds.X] *= number;
            }
        }
    }
}

namespace ExternalNamespace
{
    [TestClass]
    [TestCategory("ShaderCompiler")]
    public partial class ShaderCompilerTestsInExternalNamespace
    {
        [AutoConstructor]
        [EmbeddedBytecode(DispatchAxis.X)]
        public readonly partial struct UserDefinedTypeShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            /// <inheritdoc/>
            public void Execute()
            {
                for (var i = 0; i < 10; i++)
                {
                    buffer[(ThreadIds.X * 10) + i] = i;
                }
            }
        }

        [TestMethod]
        public void UserDefinedType()
        {
            _ = ReflectionServices.GetShaderInfo<UserDefinedTypeShader>();
        }
    }
}