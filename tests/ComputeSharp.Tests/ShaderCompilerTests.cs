using System;
using ComputeSharp;
using ComputeSharp.Descriptors;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable IDE0008, IDE0022, IDE0009, IDE0060, IDE0290

namespace ComputeSharp.Tests
{
    [TestClass]
    public partial class ShaderCompilerTests
    {
        [TestMethod]
        public void ReflectionBytecode()
        {
            static ReadOnlyMemory<byte> GetHlslBytecode<T>()
            where T : struct, IComputeShaderDescriptor<T>
            {
                return T.HlslBytecode;
            }

            ShaderInfo shaderInfo = ReflectionServices.GetShaderInfo<ReservedKeywordsShader>();

            CollectionAssert.AreEqual(GetHlslBytecode<ReservedKeywordsShader>().ToArray(), shaderInfo.HlslBytecode.ToArray());
        }

        [TestMethod]
        public void ReservedKeywords()
        {
            _ = ReflectionServices.GetShaderInfo<ReservedKeywordsShader>();
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        public readonly partial struct CustomStructType
        {
            public readonly float2 a;
            public readonly int b;
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        public readonly partial struct OutOfOrderMethodsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            static int A() => B();

            static int B() => 1 + C();

            static int C() => 1;

            public int D() => A() + E() + F();

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
            Assert.AreEqual("""
                #define __GroupSize__get_X 8
                #define __GroupSize__get_Y 8
                #define __GroupSize__get_Z 1
                
                cbuffer _ : register(b0)
                {
                    uint __x;
                    uint __y;
                }
                
                RWTexture2D<unorm float4> __outputTexture : register(u0);
                
                [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
                void Execute(uint3 ThreadIds : SV_DispatchThreadID)
                {
                    if (ThreadIds.x < __x && ThreadIds.y < __y)
                    {
                        {
                            __outputTexture[ThreadIds.xy] = float4(1, 1, 1, 1);
                            return;
                        }
                    }
                }
                """, info.HlslSource);
        }

        [ThreadGroupSize(DefaultThreadGroupSizes.XY)]
        [GeneratedComputeShaderDescriptor]
        public readonly partial struct StatelessPixelShader : IComputeShader<float4>
        {
            /// <inheritdoc/>
            public float4 Execute()
            {
                return new(1, 1, 1, 1);
            }
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [RequiresDoublePrecisionSupport]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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
        public void ComputeShaderWithScopedParameterInMethods()
        {
            _ = ReflectionServices.GetShaderInfo<ComputeShaderWithScopedParameterInMethodsShader>();
        }

        internal static class HelpersForComputeShaderWithScopedParameterInMethods
        {
            public static void Baz(scoped in float a, scoped ref float b)
            {
                b = a;
            }
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct ComputeShaderWithScopedParameterInMethodsShader : IComputeShader
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
                float x = this.number + ThreadIds.X;

                Foo(ref this.buffer[ThreadIds.X], ref x);
                Bar(ref this.buffer[ThreadIds.X], ref x);
                HelpersForComputeShaderWithScopedParameterInMethods.Baz(in this.buffer[ThreadIds.X], ref x);

                this.buffer[ThreadIds.X] *= x;
            }
        }

        [TestMethod]
        public void ShaderWithStrippedReflectionData()
        {
            ShaderInfo info1 = ReflectionServices.GetShaderInfo<ShaderWithStrippedReflectionData1>();

            // With no reflection data available, the instruction count is just 0
            Assert.AreEqual(0u, info1.InstructionCount);

            ShaderInfo info2 = ReflectionServices.GetShaderInfo<ShaderWithStrippedReflectionData2>();

            // Sanity check, here instead we should have some valid count
            Assert.AreNotEqual(0u, info2.InstructionCount);

            // Verify that the bytecode with stripped reflection is much smaller
            Assert.IsTrue(info1.HlslBytecode.Length < 1800);
            Assert.IsTrue(info2.HlslBytecode.Length > 3300);
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [CompileOptions(CompileOptions.Default | CompileOptions.StripReflectionData)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct ShaderWithStrippedReflectionData1 : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            /// <inheritdoc/>
            public void Execute()
            {
                this.buffer[ThreadIds.X] = ThreadIds.X;
            }
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [CompileOptions(CompileOptions.Default)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct ShaderWithStrippedReflectionData2 : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            /// <inheritdoc/>
            public void Execute()
            {
                this.buffer[ThreadIds.X] = ThreadIds.X;
            }
        }

        [TestMethod]
        public void GloballyCoherentBuffers()
        {
            ShaderInfo info = ReflectionServices.GetShaderInfo<GloballyCoherentBufferShader>();

            Assert.AreEqual(
                """
                #define __GroupSize__get_X 64
                #define __GroupSize__get_Y 1
                #define __GroupSize__get_Z 1

                cbuffer _ : register(b0)
                {
                    uint __x;
                    uint __y;
                    uint __z;
                }

                globallycoherent RWStructuredBuffer<int> __reserved__buffer : register(u0);

                [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
                void Execute(uint3 ThreadIds : SV_DispatchThreadID)
                {
                    if (ThreadIds.x < __x && ThreadIds.y < __y && ThreadIds.z < __z)
                    {
                        InterlockedAdd(__reserved__buffer[0], 1);
                    }
                }
                """,
                info.HlslSource);
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct GloballyCoherentBufferShader : IComputeShader
        {
            [GloballyCoherent]
            private readonly ReadWriteBuffer<int> buffer;

            public void Execute()
            {
                Hlsl.InterlockedAdd(ref this.buffer[0], 1);
            }
        }

        [TestMethod]
        public void ComputeShaderWithRefReadonlyParameterInMethods()
        {
            _ = ReflectionServices.GetShaderInfo<ComputeShaderWithRefReadonlyParameterInMethodsShader>();
        }

        internal static class HelpersForCommputeShaderWithRefReadonlyParameterInMethods
        {
            public static float Baz(ref readonly float a, scoped ref readonly float b)
            {
                return a + b;
            }
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct ComputeShaderWithRefReadonlyParameterInMethodsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;
            public readonly float number;

            private static float Foo(ref readonly float a, scoped ref readonly float b)
            {
                return a + b;
            }

            private float Bar(ref readonly float a, scoped ref readonly float b)
            {
                return a + b;
            }

            /// <inheritdoc/>
            public void Execute()
            {
                float x = this.number + ThreadIds.X;

                x += Foo(ref x, in x);
                x += Foo(in this.number, in this.number);
                x += Bar(ref x, in x);
                x += HelpersForCommputeShaderWithRefReadonlyParameterInMethods.Baz(in this.buffer[ThreadIds.X], ref x);

                this.buffer[ThreadIds.X] = x;
            }
        }

        [TestMethod]
        public void AllRefTypesShader_RewritesRefParametersCorrectly()
        {
            ShaderInfo info = ReflectionServices.GetShaderInfo<AllRefTypesShader>();

            Assert.AreEqual(
                """
                #define __GroupSize__get_X 64
                #define __GroupSize__get_Y 1
                #define __GroupSize__get_Z 1

                static void Foo(in int a, in int b, inout int c, out int d);

                static void Bar(in int a, in int b, inout int c, out int d);

                cbuffer _ : register(b0)
                {
                    uint __x;
                    uint __y;
                    uint __z;
                }

                RWStructuredBuffer<float> __reserved__buffer : register(u0);

                static void Foo(in int a, in int b, inout int c, out int d)
                {
                    d = 0;
                }

                static void Bar(in int a, in int b, inout int c, out int d)
                {
                    d = 0;
                }

                [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
                void Execute(uint3 ThreadIds : SV_DispatchThreadID)
                {
                    if (ThreadIds.x < __x && ThreadIds.y < __y && ThreadIds.z < __z)
                    {
                    }
                }
                """,
                info.HlslSource);
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct AllRefTypesShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            public static void Foo(
                in int a,
                ref readonly int b,
                ref int c,
                out int d)
            {
                d = 0;
            }

            public static void Bar(
                scoped in int a,
                scoped ref readonly int b,
                scoped ref int c,
                scoped out int d)
            {
                d = 0;
            }

            /// <inheritdoc/>
            public void Execute()
            {
            }
        }

        [TestMethod]
        public void ShaderWithPartialDeclarations_IsCombinedCorrectly()
        {
            ShaderInfo info = ReflectionServices.GetShaderInfo<ShaderWithPartialDeclarations>();

            Assert.AreEqual(
                """
                #define __GroupSize__get_X 64
                #define __GroupSize__get_Y 1
                #define __GroupSize__get_Z 1
                #define __ComputeSharp_Tests_ShaderCompilerTests_ShaderWithPartialDeclarations__a 2

                static int Sum(int x, int y);

                static const int b = 4;

                cbuffer _ : register(b0)
                {
                    uint __x;
                    uint __y;
                    uint __z;
                }

                RWStructuredBuffer<float> __reserved__buffer : register(u0);

                static int Sum(int x, int y)
                {
                    return x + y;
                }

                [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
                void Execute(uint3 ThreadIds : SV_DispatchThreadID)
                {
                    if (ThreadIds.x < __x && ThreadIds.y < __y && ThreadIds.z < __z)
                    {
                        __reserved__buffer[ThreadIds.x] = Sum(__ComputeSharp_Tests_ShaderCompilerTests_ShaderWithPartialDeclarations__a, b);
                    }
                }
                """,
                info.HlslSource);
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct ShaderWithPartialDeclarations : IComputeShader;

        partial struct ShaderWithPartialDeclarations
        {
            public readonly ReadWriteBuffer<float> buffer;
        }

        partial struct ShaderWithPartialDeclarations
        {
            /// <inheritdoc/>
            public void Execute()
            {
                buffer[ThreadIds.X] = Sum(a, b);
            }
        }

        partial struct ShaderWithPartialDeclarations
        {
            private const int a = 2;
        }

        partial struct ShaderWithPartialDeclarations
        {
            private static readonly int b = 4;
        }

        partial struct ShaderWithPartialDeclarations
        {
            private static int Sum(int x, int y)
            {
                return x + y;
            }
        }

        [TestMethod]
        public void ShaderWithStructMethodCallingOtherStructMethod_IsProcessedCorrectly()
        {
            ShaderInfo info = ReflectionServices.GetShaderInfo<ShaderWithStructMethodCallingOtherStructMethod>();

            Assert.AreEqual(
                """
                #define __GroupSize__get_X 64
                #define __GroupSize__get_Y 1
                #define __GroupSize__get_Z 1

                struct ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod1;
                struct ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod2;

                struct ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod1
                {
                    int InstanceMethod();
                };

                struct ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod2
                {
                    int InstanceMethod();
                };

                cbuffer _ : register(b0)
                {
                    uint __x;
                    uint __y;
                    uint __z;
                }

                RWStructuredBuffer<int> __reserved__buffer : register(u0);

                int ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod1::InstanceMethod()
                {
                    ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod2 bar = (ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod2)0;
                    return bar.InstanceMethod();
                }

                int ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod2::InstanceMethod()
                {
                    return 42;
                }

                [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
                void Execute(uint3 ThreadIds : SV_DispatchThreadID)
                {
                    if (ThreadIds.x < __x && ThreadIds.y < __y && ThreadIds.z < __z)
                    {
                        ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod1 foo = (ComputeSharp_Tests_ShaderCompilerTests_StructWithInstanceMethod1)0;
                        __reserved__buffer[ThreadIds.x] = foo.InstanceMethod();
                    }
                }
                """,
                info.HlslSource);
        }

        // See https://github.com/Sergio0694/ComputeSharp/issues/479
        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        public readonly partial struct ShaderWithStructMethodCallingOtherStructMethod : IComputeShader
        {
            private readonly ReadWriteBuffer<int> buffer;

            public void Execute()
            {
                StructWithInstanceMethod1 foo = default;

                buffer[ThreadIds.X] = foo.InstanceMethod();
            }
        }

        public struct StructWithInstanceMethod1
        {
            public int InstanceMethod()
            {
                StructWithInstanceMethod2 bar = default;

                return bar.InstanceMethod();
            }
        }

        public struct StructWithInstanceMethod2
        {
            public int InstanceMethod()
            {
                return 42;
            }
        }

        [TestMethod]
        public void ShaderWithAllSupportedMembers_IsProcessedCorrectly()
        {
            ShaderInfo info = ReflectionServices.GetShaderInfo<ShaderWithAllSupportedMembers>();

            Assert.AreEqual(
                """
                #define __GroupSize__get_X 64
                #define __GroupSize__get_Y 1
                #define __GroupSize__get_Z 1
                #define __ComputeSharp_Tests_ShaderCompilerTests_ExternalContainerClass__Factor 8
                #define __ComputeSharp_Tests_ShaderCompilerTests_ShaderWithAllSupportedMembers__PI 3.14

                struct ComputeSharp_Tests_ShaderCompilerTests_StructType1;
                struct ComputeSharp_Tests_ShaderCompilerTests_StructType2;

                struct ComputeSharp_Tests_ShaderCompilerTests_StructType1
                {
                    int X;
                    float Y;
                    float Combine(ComputeSharp_Tests_ShaderCompilerTests_StructType2 other);
                    static ComputeSharp_Tests_ShaderCompilerTests_StructType1 __ctor(int x);
                    void __ctor__init(int x);
                };

                struct ComputeSharp_Tests_ShaderCompilerTests_StructType2
                {
                    float2 V;
                    float Combine(ComputeSharp_Tests_ShaderCompilerTests_StructType1 other);
                };

                int InstanceMethodInShader();

                static float StaticMethodInShader(float x);

                static float ComputeSharp_Tests_ShaderCompilerTests_StructType1_StaticMethod(int x);

                static float ComputeSharp_Tests_ShaderCompilerTests_StructType2_StaticMethod(int x);

                static const float Init = abs(__ComputeSharp_Tests_ShaderCompilerTests_ShaderWithAllSupportedMembers__PI);
                static int Temp;
                static int ComputeSharp_Tests_ShaderCompilerTests_ExternalContainerClass_Temp;
                static const float ComputeSharp_Tests_ShaderCompilerTests_ExternalContainerClass_PI2 = 3.14 * 2;

                cbuffer _ : register(b0)
                {
                    uint __x;
                    uint __y;
                    uint __z;
                    int number;
                    float4 __reserved__vector;
                }

                RWStructuredBuffer<int> __reserved__buffer : register(u0);

                float ComputeSharp_Tests_ShaderCompilerTests_StructType1::Combine(ComputeSharp_Tests_ShaderCompilerTests_StructType2 other)
                {
                    return Y + other.V.y;
                }

                static ComputeSharp_Tests_ShaderCompilerTests_StructType1 ComputeSharp_Tests_ShaderCompilerTests_StructType1::__ctor(int x)
                {
                    ComputeSharp_Tests_ShaderCompilerTests_StructType1 __this = (ComputeSharp_Tests_ShaderCompilerTests_StructType1)0;
                    __this.__ctor__init(x);
                    return __this;
                }

                void ComputeSharp_Tests_ShaderCompilerTests_StructType1::__ctor__init(int x)
                {
                    X = x;
                    Y = (float)0;
                }

                float ComputeSharp_Tests_ShaderCompilerTests_StructType2::Combine(ComputeSharp_Tests_ShaderCompilerTests_StructType1 other)
                {
                    return V.x + other.X;
                }

                int InstanceMethodInShader()
                {
                    return (int)(number + __reserved__vector.x);
                }

                static float StaticMethodInShader(float x)
                {
                    return x + 1;
                }

                static float ComputeSharp_Tests_ShaderCompilerTests_StructType1_StaticMethod(int x)
                {
                    return x * 2;
                }

                static float ComputeSharp_Tests_ShaderCompilerTests_StructType2_StaticMethod(int x)
                {
                    return x * 4;
                }

                [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
                void Execute(uint3 ThreadIds : SV_DispatchThreadID)
                {
                    if (ThreadIds.x < __x && ThreadIds.y < __y && ThreadIds.z < __z)
                    {
                        ComputeSharp_Tests_ShaderCompilerTests_StructType1 type1 = ComputeSharp_Tests_ShaderCompilerTests_StructType1::__ctor(__ComputeSharp_Tests_ShaderCompilerTests_ExternalContainerClass__Factor);
                        ComputeSharp_Tests_ShaderCompilerTests_StructType2 type2 = (ComputeSharp_Tests_ShaderCompilerTests_StructType2)0;
                        float combine1 = type1.Combine(type2);
                        float combine2 = type2.Combine(type1);
                        combine1 += ComputeSharp_Tests_ShaderCompilerTests_StructType1_StaticMethod(Temp++);
                        combine2 += ComputeSharp_Tests_ShaderCompilerTests_StructType2_StaticMethod(ComputeSharp_Tests_ShaderCompilerTests_ExternalContainerClass_Temp++);
                        float dummy = Init + ComputeSharp_Tests_ShaderCompilerTests_ExternalContainerClass_PI2;
                        __reserved__buffer[ThreadIds.x] = (int)(combine1 + combine2 + dummy);
                    }
                }
                """,
                info.HlslSource);
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct ShaderWithAllSupportedMembers : IComputeShader
        {
            private const float PI = 3.14f;

            private static readonly float Init = Hlsl.Abs(PI);
            private static int Temp;

            private readonly ReadWriteBuffer<int> buffer;
            private readonly int number;
            private readonly float4 vector;

            public void Execute()
            {
                StructType1 type1 = new(ExternalContainerClass.Factor);
                StructType2 type2 = default;

                float combine1 = type1.Combine(type2);
                float combine2 = type2.Combine(type1);

                combine1 += StructType1.StaticMethod(Temp++);
                combine2 += StructType2.StaticMethod(ExternalContainerClass.Temp++);

                float dummy = Init + ExternalContainerClass.PI2;

                buffer[ThreadIds.X] = (int)(combine1 + combine2 + dummy);
            }

            public int InstanceMethodInShader()
            {
                return (int)(number + vector.X);
            }

            public static float StaticMethodInShader(float x)
            {
                return x + 1;
            }
        }

        public static class ExternalContainerClass
        {
            public const int Factor = 8;

            public static readonly float PI2 = 3.14f * 2;
            public static int Temp;
        }

        internal struct StructType1
        {
            public int X;
            public float Y;

            public StructType1(int x)
            {
                X = x;
                Y = default;
            }

            public float Combine(StructType2 other)
            {
                return Y + other.V.Y;
            }

            public float InstanceMethod()
            {
                StructType2 other = default;
                other.V = ExternalContainerClass.PI2;

                return Y + other.Combine(default) + StructType2.StaticMethod(X);
            }

            public static float StaticMethod(int x)
            {
                return x * 2;
            }
        }

        internal struct StructType2
        {
            public float2 V;

            public float Combine(StructType1 other)
            {
                return V.X + other.X;
            }

            public float InstanceMethod()
            {
                StructType1 other = new(ExternalContainerClass.Temp);

                return V.X + other.Combine(default) + StructType1.StaticMethod((int)V.X);
            }

            public static float StaticMethod(int x)
            {
                return x * 4;
            }
        }

        // See https://github.com/Sergio0694/ComputeSharp/issues/726
        [TestMethod]
        public void ShaderUsingThisExpressions_IsProcessedCorrectly()
        {
            ShaderInfo info = ReflectionServices.GetShaderInfo<ShaderUsingThisExpressions>();

            Assert.AreEqual(
                """
                #define __GroupSize__get_X 64
                #define __GroupSize__get_Y 1
                #define __GroupSize__get_Z 1

                struct ComputeSharp_Tests_ShaderCompilerTests_ShaderUsingThisExpressions_Data;

                struct ComputeSharp_Tests_ShaderCompilerTests_ShaderUsingThisExpressions_Data
                {
                    int value;
                    void SetValue(int value);
                };

                cbuffer _ : register(b0)
                {
                    uint __x;
                    uint __y;
                    uint __z;
                    float alpha;
                }

                RWStructuredBuffer<float4> __reserved__buffer : register(u0);

                void ComputeSharp_Tests_ShaderCompilerTests_ShaderUsingThisExpressions_Data::SetValue(int value)
                {
                    this.value = value;
                }

                [NumThreads(__GroupSize__get_X, __GroupSize__get_Y, __GroupSize__get_Z)]
                void Execute(uint3 ThreadIds : SV_DispatchThreadID)
                {
                    if (ThreadIds.x < __x && ThreadIds.y < __y && ThreadIds.z < __z)
                    {
                        ComputeSharp_Tests_ShaderCompilerTests_ShaderUsingThisExpressions_Data data = (ComputeSharp_Tests_ShaderCompilerTests_ShaderUsingThisExpressions_Data)0;
                        data.SetValue(3);
                        __reserved__buffer[ThreadIds.x] = float4(data.value, data.value, data.value, alpha);
                    }
                }
                """,
                info.HlslSource);
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        internal readonly partial struct ShaderUsingThisExpressions : IComputeShader
        {
            public readonly ReadWriteBuffer<float4> buffer;
            public readonly float alpha;

            private struct Data
            {
                public int value;

                public void SetValue(int value)
                {
                    this.value = value;
                }
            }

            public void Execute()
            {
                Data data = default;

                data.SetValue(3);

                // The 'this.' expression for shader captured values should be stripped.
                // The one for accessing struct instance members, however, should not be.
                this.buffer[ThreadIds.X] = new float4(data.value, data.value, data.value, this.alpha);
            }
        }

        // See https://github.com/Sergio0694/ComputeSharp/issues/435
        [TestMethod]
        public void HlslVectorTypeConstructorCombinations()
        {
            _ = ReflectionServices.GetShaderInfo<HlslVectorTypeConstructorCombinationsShader>();
        }

        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
        public readonly partial struct HlslVectorTypeConstructorCombinationsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;
            public readonly float2 f2;
            public readonly int4 i4;
            public readonly int2 i2;
            public readonly int3 i3;

            public void Execute()
            {
                float3 f3 = new float3(f2, 0) + new float3(0, f2);
                float4 f4 = new float4(0, f3) + new float4(0, f2, 1) + new float4(0, 1, f2) + new float4((float1x3)f3, 0);

                int4 temp = new int4(i2, 0, 1) + new int4(new int3x1(i3.X, i3.Y, i3.Z), 0);

                // Just here to avoid warnings, this shader doesn't really have to do anything
                buffer[0] = f3[0];
                buffer[1] = f4[1];
                buffer[2] = temp[0];
            }
        }
    }
}

namespace ExternalNamespace
{
    [TestClass]
    public partial class ShaderCompilerTestsInExternalNamespace
    {
        [AutoConstructor]
        [ThreadGroupSize(DefaultThreadGroupSizes.X)]
        [GeneratedComputeShaderDescriptor]
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