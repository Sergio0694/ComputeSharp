using System;
using System.Linq;
using ComputeSharp.Resources;
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

            Assert.IsTrue(Math.Abs(3.14f - results[0]) < 0.00001f);
            Assert.IsTrue(Math.Abs(results[1] - results[2]) < 0.00001f);
            Assert.IsTrue(Math.Abs(1 - results[3]) < 0.00001f);
            Assert.IsTrue(Math.Abs(2 - results[4]) < 0.00001f);
            Assert.IsTrue(Math.Abs(3 - results[5]) < 0.00001f);
            Assert.IsTrue(Math.Abs(4 - results[6]) < 0.00001f);
            Assert.IsTrue(Math.Abs(3.14f - results[7]) < 0.00001f);
        }

        [AutoConstructor]
        internal readonly partial struct StaticConstantsShader : IComputeShader
        {
            public readonly ReadWriteBuffer<float> buffer;

            static float Pi => 3.14f;
            static float SinPi => Hlsl.Sin(Pi);
            static Int2x2 Mat => new(1, 2, 3, 4);
            static float Combo => Hlsl.Abs(Hlsl.Clamp(Hlsl.Min(Hlsl.Max(3.14f, 2), 10), 0, 42));

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
    }
}
