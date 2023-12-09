using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
public partial class HlslPrimitiveTests
{
    [TestMethod]
    public void Float3x2_ImplicitConversions()
    {
        Matrix3x2 m3x2 = new(1, 2, 3, 4, 5, 6);

        float3x2 f3x2 = m3x2;

        Assert.AreEqual(m3x2.M11, f3x2.M11);
        Assert.AreEqual(m3x2.M12, f3x2.M12);
        Assert.AreEqual(m3x2.M21, f3x2.M21);
        Assert.AreEqual(m3x2.M22, f3x2.M22);
        Assert.AreEqual(m3x2.M31, f3x2.M31);
        Assert.AreEqual(m3x2.M32, f3x2.M32);

        Matrix3x2 roundTrip3x2 = f3x2;

        Assert.AreEqual(m3x2, roundTrip3x2);
    }

    [TestMethod]
    public void Float4x4_ImplicitConversions()
    {
        Matrix4x4 m4x4 = new(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

        float4x4 f4x4 = m4x4;

        Assert.AreEqual(m4x4.M11, f4x4.M11);
        Assert.AreEqual(m4x4.M12, f4x4.M12);
        Assert.AreEqual(m4x4.M13, f4x4.M13);
        Assert.AreEqual(m4x4.M14, f4x4.M14);
        Assert.AreEqual(m4x4.M21, f4x4.M21);
        Assert.AreEqual(m4x4.M22, f4x4.M22);
        Assert.AreEqual(m4x4.M23, f4x4.M23);
        Assert.AreEqual(m4x4.M24, f4x4.M24);
        Assert.AreEqual(m4x4.M31, f4x4.M31);
        Assert.AreEqual(m4x4.M32, f4x4.M32);
        Assert.AreEqual(m4x4.M33, f4x4.M33);
        Assert.AreEqual(m4x4.M34, f4x4.M34);
        Assert.AreEqual(m4x4.M41, f4x4.M41);
        Assert.AreEqual(m4x4.M42, f4x4.M42);
        Assert.AreEqual(m4x4.M43, f4x4.M43);
        Assert.AreEqual(m4x4.M44, f4x4.M44);

        Matrix4x4 roundTrip4x4 = f4x4;

        Assert.AreEqual(m4x4, roundTrip4x4);
    }
}