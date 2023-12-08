using System;
using System.Buffers;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests.AssemblyLevelAttributes;

[TestClass]
public partial class D2D1PixelShaderTests
{
    [TestMethod]
    public unsafe void ShaderWithAssemblyLevelAttributes()
    {
        ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithNoCompileAttributes>();

        // Verify the shader was precompiled
        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? manager));
        Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

        // Compile with the same assembly level options
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithNoCompileAttributes>(
            shaderProfile: D2D1ShaderProfile.PixelShader41,
            compileOptions: D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2 | D2D1CompileOptions.PartialPrecision);

        // Verify the same bytecode is returned
        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

        // Incorrect profile
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithNoCompileAttributes>(D2D1ShaderProfile.PixelShader50);

        Assert.IsFalse(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? _));
        Assert.IsTrue(MemoryMarshal.TryGetArray(bytecode, out ArraySegment<byte> segment));
        Assert.AreEqual(0, segment.Offset);
        Assert.IsTrue(segment.Count > 0);

        // Incorrect options
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithNoCompileAttributes>(D2D1CompileOptions.Default);

        Assert.IsFalse(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? _));
        Assert.IsTrue(MemoryMarshal.TryGetArray(bytecode, out segment));
        Assert.AreEqual(0, segment.Offset);
        Assert.IsTrue(segment.Count > 0);
    }

    [D2DInputCount(3)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    [D2DInputSimple(2)]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct ShaderWithNoCompileAttributes : ID2D1PixelShader
    {
        public float4 Execute()
        {
            float4 dst = D2D.GetInput(0);
            float4 src = D2D.GetInput(1);
            float4 srcMask = D2D.GetInput(2);
            float4 result = ((1 - srcMask) * dst) + (srcMask * src);

            return result;
        }
    }

    [TestMethod]
    public unsafe void ShaderWithAssemblyLevelAttributesAndOverriddenProfile()
    {
        ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithOverriddenProfile>();

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? manager));
        Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

        // Try to recompile with the correct combined options
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithOverriddenProfile>(
            shaderProfile: D2D1ShaderProfile.PixelShader50,
            compileOptions: D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2 | D2D1CompileOptions.PartialPrecision);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

        // Incorrect profile (overridden profile)
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithOverriddenProfile>(
            shaderProfile: D2D1ShaderProfile.PixelShader41,
            compileOptions: D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2 | D2D1CompileOptions.PartialPrecision);

        Assert.IsFalse(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? _));
        Assert.IsTrue(MemoryMarshal.TryGetArray(bytecode, out ArraySegment<byte> segment));
        Assert.AreEqual(0, segment.Offset);
        Assert.IsTrue(segment.Count > 0);
    }

    [D2DInputCount(3)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    [D2DInputSimple(2)]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct ShaderWithOverriddenProfile : ID2D1PixelShader
    {
        public float4 Execute()
        {
            float4 dst = D2D.GetInput(0);
            float4 src = D2D.GetInput(1);
            float4 srcMask = D2D.GetInput(2);
            float4 result = ((1 - srcMask) * dst) + (srcMask * src);

            return result;
        }
    }

    [TestMethod]
    public unsafe void ShaderWithAssemblyLevelAttributesAndOverriddenOptions()
    {
        ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithOverriddenOptions>();

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? manager));
        Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

        // Try to recompile with the correct combined options
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithOverriddenOptions>(
            shaderProfile: D2D1ShaderProfile.PixelShader41,
            compileOptions: D2D1CompileOptions.Debug | D2D1CompileOptions.AvoidFlowControl | D2D1CompileOptions.PartialPrecision);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

        // Incorrect profile (overridden compile options)
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithOverriddenOptions>(
            shaderProfile: D2D1ShaderProfile.PixelShader41,
            compileOptions: D2D1CompileOptions.Default);

        Assert.IsFalse(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? _));
        Assert.IsTrue(MemoryMarshal.TryGetArray(bytecode, out ArraySegment<byte> segment));
        Assert.AreEqual(0, segment.Offset);
        Assert.IsTrue(segment.Count > 0);
    }

    [D2DInputCount(3)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    [D2DInputSimple(2)]
    [D2DCompileOptions(D2D1CompileOptions.Debug | D2D1CompileOptions.AvoidFlowControl | D2D1CompileOptions.PartialPrecision)]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct ShaderWithOverriddenOptions : ID2D1PixelShader
    {
        public float4 Execute()
        {
            float4 dst = D2D.GetInput(0);
            float4 src = D2D.GetInput(1);
            float4 srcMask = D2D.GetInput(2);
            float4 result = ((1 - srcMask) * dst) + (srcMask * src);

            return result;
        }
    }

    [TestMethod]
    public unsafe void ShaderWithAssemblyLevelAttributesAndEverythingOverridden()
    {
        ReadOnlyMemory<byte> bytecode = D2D1PixelShader.LoadBytecode<ShaderWithOverriddenProfileAndOptions>();

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out MemoryManager<byte>? manager));
        Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));

        // Try to get the new bytecode
        bytecode = D2D1PixelShader.LoadBytecode<ShaderWithOverriddenProfileAndOptions>(
            shaderProfile: D2D1ShaderProfile.PixelShader50,
            compileOptions: D2D1CompileOptions.Debug | D2D1CompileOptions.AvoidFlowControl | D2D1CompileOptions.PartialPrecision);

        Assert.IsTrue(MemoryMarshal.TryGetMemoryManager(bytecode, out manager));
        Assert.IsTrue(manager!.GetType().Name.Contains("HlslBytecodeMemoryManager"));
    }

    [D2DInputCount(3)]
    [D2DInputSimple(0)]
    [D2DInputSimple(1)]
    [D2DInputSimple(2)]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
    [D2DCompileOptions(D2D1CompileOptions.Debug | D2D1CompileOptions.AvoidFlowControl | D2D1CompileOptions.PartialPrecision)]
    [D2DGeneratedPixelShaderDescriptor]
    internal readonly partial struct ShaderWithOverriddenProfileAndOptions : ID2D1PixelShader
    {
        public float4 Execute()
        {
            float4 dst = D2D.GetInput(0);
            float4 src = D2D.GetInput(1);
            float4 srcMask = D2D.GetInput(2);
            float4 result = ((1 - srcMask) * dst) + (srcMask * src);

            return result;
        }
    }
}