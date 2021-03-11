using System;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("ShaderMembers")]
    public partial class ShaderMembersTests
    {
        [CombinatorialTestMethod]
        [AllDevices]
        public void StaticConstants(Device device)
        {
            using ReadWriteBuffer<float> buffer = device.Get().AllocateReadWriteBuffer<float>(8);

            device.Get().For(1, new StaticConstantsShader(buffer));

            float[] results = buffer.ToArray();

            Assert.AreEqual(3.14f, results[0], 0.00001f);
            Assert.AreEqual(results[1], results[2], 0.00001f);
            Assert.AreEqual(1, results[3], 0.00001f);
            Assert.AreEqual(2, results[4], 0.00001f);
            Assert.AreEqual(3, results[5], 0.00001f);
            Assert.AreEqual(4, results[6], 0.00001f);
            Assert.AreEqual(3.14f, results[7], 0.00001f);
        }

        [AutoConstructor]
        internal readonly partial struct StaticConstantsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            static readonly float Pi = 3.14f;
            static readonly float SinPi = Hlsl.Sin(Pi);
            static readonly Int2x2 Mat = new(1, 2, 3, 4);
            static readonly float Combo = Hlsl.Abs(Hlsl.Clamp(Hlsl.Min(Hlsl.Max(3.14f, 2), 10), 0, 42));

            public void Execute()
            {
                buffer[0] = Pi;
                buffer[1] = SinPi;
                buffer[2] = Hlsl.Sin(3.14f);
                buffer[3] = Mat.M11;
                buffer[4] = Mat.M12;
                buffer[5] = Mat.M21;
                buffer[6] = Mat.M22;
                buffer[7] = Combo;
            }
        }

        [CombinatorialTestMethod]
        [AllDevices]
        public void GlobalVariable(Device device)
        {
            using ReadWriteBuffer<int> buffer = device.Get().AllocateReadWriteBuffer<int>(32);

            device.Get().For(32, new GlobalVariableShader(buffer));

            int[] results = buffer.ToArray();

            for (int i = 0; i < results.Length; i++)
            {
                Assert.AreEqual(results[i], i);
            }
        }

        [AutoConstructor]
        internal readonly partial struct GlobalVariableShader : IComputeShader
        {
            public readonly ReadWriteBuffer<int> buffer;

            static int total;

            public void Execute()
            {
                for (int i = 0; i < ThreadIds.X; i++)
                {
                    total++;
                }

                buffer[ThreadIds.X] = total;
            }
        }
    }
}
