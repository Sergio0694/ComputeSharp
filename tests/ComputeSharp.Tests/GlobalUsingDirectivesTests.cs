using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("GlobalUsingDirectives")]
public unsafe partial class GlobalUsingDirectivesTests
{
    [TestMethod]
    public void EnsureGlobalUsingDirectivesArePresent()
    {
        Assert.AreEqual(typeof(bool2), typeof(Bool2));
        Assert.AreEqual(typeof(bool3), typeof(Bool3));
        Assert.AreEqual(typeof(bool4), typeof(Bool4));
        Assert.AreEqual(typeof(bool1x1), typeof(Bool1x1));
        Assert.AreEqual(typeof(bool1x2), typeof(Bool1x2));
        Assert.AreEqual(typeof(bool1x3), typeof(Bool1x3));
        Assert.AreEqual(typeof(bool1x4), typeof(Bool1x4));
        Assert.AreEqual(typeof(bool2x1), typeof(Bool2x1));
        Assert.AreEqual(typeof(bool2x2), typeof(Bool2x2));
        Assert.AreEqual(typeof(bool2x3), typeof(Bool2x3));
        Assert.AreEqual(typeof(bool2x4), typeof(Bool2x4));
        Assert.AreEqual(typeof(bool3x1), typeof(Bool3x1));
        Assert.AreEqual(typeof(bool3x2), typeof(Bool3x2));
        Assert.AreEqual(typeof(bool3x3), typeof(Bool3x3));
        Assert.AreEqual(typeof(bool3x4), typeof(Bool3x4));
        Assert.AreEqual(typeof(bool4x1), typeof(Bool4x1));
        Assert.AreEqual(typeof(bool4x2), typeof(Bool4x2));
        Assert.AreEqual(typeof(bool4x3), typeof(Bool4x3));
        Assert.AreEqual(typeof(bool4x4), typeof(Bool4x4));

        Assert.AreEqual(typeof(double2), typeof(Double2));
        Assert.AreEqual(typeof(double3), typeof(Double3));
        Assert.AreEqual(typeof(double4), typeof(Double4));
        Assert.AreEqual(typeof(double1x1), typeof(Double1x1));
        Assert.AreEqual(typeof(double1x2), typeof(Double1x2));
        Assert.AreEqual(typeof(double1x3), typeof(Double1x3));
        Assert.AreEqual(typeof(double1x4), typeof(Double1x4));
        Assert.AreEqual(typeof(double2x1), typeof(Double2x1));
        Assert.AreEqual(typeof(double2x2), typeof(Double2x2));
        Assert.AreEqual(typeof(double2x3), typeof(Double2x3));
        Assert.AreEqual(typeof(double2x4), typeof(Double2x4));
        Assert.AreEqual(typeof(double3x1), typeof(Double3x1));
        Assert.AreEqual(typeof(double3x2), typeof(Double3x2));
        Assert.AreEqual(typeof(double3x3), typeof(Double3x3));
        Assert.AreEqual(typeof(double3x4), typeof(Double3x4));
        Assert.AreEqual(typeof(double4x1), typeof(Double4x1));
        Assert.AreEqual(typeof(double4x2), typeof(Double4x2));
        Assert.AreEqual(typeof(double4x3), typeof(Double4x3));
        Assert.AreEqual(typeof(double4x4), typeof(Double4x4));

        Assert.AreEqual(typeof(float2), typeof(Float2));
        Assert.AreEqual(typeof(float3), typeof(Float3));
        Assert.AreEqual(typeof(float4), typeof(Float4));
        Assert.AreEqual(typeof(float1x1), typeof(Float1x1));
        Assert.AreEqual(typeof(float1x2), typeof(Float1x2));
        Assert.AreEqual(typeof(float1x3), typeof(Float1x3));
        Assert.AreEqual(typeof(float1x4), typeof(Float1x4));
        Assert.AreEqual(typeof(float2x1), typeof(Float2x1));
        Assert.AreEqual(typeof(float2x2), typeof(Float2x2));
        Assert.AreEqual(typeof(float2x3), typeof(Float2x3));
        Assert.AreEqual(typeof(float2x4), typeof(Float2x4));
        Assert.AreEqual(typeof(float3x1), typeof(Float3x1));
        Assert.AreEqual(typeof(float3x2), typeof(Float3x2));
        Assert.AreEqual(typeof(float3x3), typeof(Float3x3));
        Assert.AreEqual(typeof(float3x4), typeof(Float3x4));
        Assert.AreEqual(typeof(float4x1), typeof(Float4x1));
        Assert.AreEqual(typeof(float4x2), typeof(Float4x2));
        Assert.AreEqual(typeof(float4x3), typeof(Float4x3));
        Assert.AreEqual(typeof(float4x4), typeof(Float4x4));

        Assert.AreEqual(typeof(int2), typeof(Int2));
        Assert.AreEqual(typeof(int3), typeof(Int3));
        Assert.AreEqual(typeof(int4), typeof(Int4));
        Assert.AreEqual(typeof(int1x1), typeof(Int1x1));
        Assert.AreEqual(typeof(int1x2), typeof(Int1x2));
        Assert.AreEqual(typeof(int1x3), typeof(Int1x3));
        Assert.AreEqual(typeof(int1x4), typeof(Int1x4));
        Assert.AreEqual(typeof(int2x1), typeof(Int2x1));
        Assert.AreEqual(typeof(int2x2), typeof(Int2x2));
        Assert.AreEqual(typeof(int2x3), typeof(Int2x3));
        Assert.AreEqual(typeof(int2x4), typeof(Int2x4));
        Assert.AreEqual(typeof(int3x1), typeof(Int3x1));
        Assert.AreEqual(typeof(int3x2), typeof(Int3x2));
        Assert.AreEqual(typeof(int3x3), typeof(Int3x3));
        Assert.AreEqual(typeof(int3x4), typeof(Int3x4));
        Assert.AreEqual(typeof(int4x1), typeof(Int4x1));
        Assert.AreEqual(typeof(int4x2), typeof(Int4x2));
        Assert.AreEqual(typeof(int4x3), typeof(Int4x3));
        Assert.AreEqual(typeof(int4x4), typeof(Int4x4));

        Assert.AreEqual(typeof(uint2), typeof(UInt2));
        Assert.AreEqual(typeof(uint3), typeof(UInt3));
        Assert.AreEqual(typeof(uint4), typeof(UInt4));
        Assert.AreEqual(typeof(uint1x1), typeof(UInt1x1));
        Assert.AreEqual(typeof(uint1x2), typeof(UInt1x2));
        Assert.AreEqual(typeof(uint1x3), typeof(UInt1x3));
        Assert.AreEqual(typeof(uint1x4), typeof(UInt1x4));
        Assert.AreEqual(typeof(uint2x1), typeof(UInt2x1));
        Assert.AreEqual(typeof(uint2x2), typeof(UInt2x2));
        Assert.AreEqual(typeof(uint2x3), typeof(UInt2x3));
        Assert.AreEqual(typeof(uint2x4), typeof(UInt2x4));
        Assert.AreEqual(typeof(uint3x1), typeof(UInt3x1));
        Assert.AreEqual(typeof(uint3x2), typeof(UInt3x2));
        Assert.AreEqual(typeof(uint3x3), typeof(UInt3x3));
        Assert.AreEqual(typeof(uint3x4), typeof(UInt3x4));
        Assert.AreEqual(typeof(uint4x1), typeof(UInt4x1));
        Assert.AreEqual(typeof(uint4x2), typeof(UInt4x2));
        Assert.AreEqual(typeof(uint4x3), typeof(UInt4x3));
        Assert.AreEqual(typeof(uint4x4), typeof(UInt4x4));
    }
}
