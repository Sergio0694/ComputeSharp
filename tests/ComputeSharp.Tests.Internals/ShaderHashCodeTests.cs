using System;
using ComputeSharp.__Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS0618

namespace ComputeSharp.Tests.Internals;

[TestClass]
[TestCategory("ShaderHashCode")]
public partial class ShaderHashCodeTests
{
    public partial struct Shader1 : IComputeShader
    {
        public float A;
        public ReadWriteBuffer<float> B;

        public void Execute()
        {
            B[0] = A;
        }
    }

    [TestMethod]
    public void ShaderWithNoCapturedDelegates()
    {
        float value = 10;
        using ReadWriteBuffer<float> buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(1);

        Shader1 shader1 = new() { A = value, B = buffer };

        int hash1 = ((IShader)shader1).GetDispatchId();
        int hash2 = ((IShader)shader1).GetDispatchId();

        Assert.IsTrue(hash1 == hash2);

        Shader1 shader2 = new() { A = value, B = buffer };

        int hash3 = ((IShader)shader2).GetDispatchId();

        Assert.IsTrue(hash1 == hash3);
    }

    public partial struct Shader2 : IComputeShader
    {
        public float A;
        public ReadWriteBuffer<float> B;
        public Func<float, float> F;

        public void Execute()
        {
            B[0] = F(A);
        }
    }

    [TestMethod]
    public void ShaderWithCapturedDelegates()
    {
        Func<float, float> f = static x => x * x;

        using ReadWriteBuffer<float> buffer = GraphicsDevice.GetDefault().AllocateReadWriteBuffer<float>(1);

        Shader2 shader1 = new() { A = 1, B = buffer, F = f };

        int hash1 = ((IShader)shader1).GetDispatchId();
        int hash2 = ((IShader)shader1).GetDispatchId();

        Assert.IsTrue(hash1 == hash2);

        f = static x => x + 1;

        Shader2 shader2 = new() { A = 1, B = buffer, F = f };

        int hash3 = ((IShader)shader2).GetDispatchId();

        Assert.IsFalse(hash1 == hash3);
    }
}
