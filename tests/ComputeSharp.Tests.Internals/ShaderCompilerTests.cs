#if !DISABLE_RUNTIME_SHADER_COMPILATION_SUPPORT

using System;
using ComputeSharp.Exceptions;
using ComputeSharp.Shaders.Translation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.Tests.Internals;

[TestClass]
[TestCategory("ShaderCompiler")]
public class ShaderCompilerTests
{
    private const string ShaderSource = @"
    cbuffer _ : register(b0)
    {
        uint __x;
        uint __y;
        uint __z;
    }

    RWStructuredBuffer<float> buffer : register(u0);

    [NumThreads(32, 1, 1)]
    void Execute(uint3 ids : SV_DispatchThreadId)
    {
        if (ids.x < __x && ids.y < __y && ids.z < __z)
        {
            buffer[ids.x] *= 2;
        }
    }";

    [TestMethod]
    public void CompileTest_Ok()
    {
        using ComPtr<IDxcBlob> dxcBlob = ShaderCompiler.Instance.CompileShader(ShaderSource.AsSpan());
    }

    [TestMethod]
    [ExpectedException(typeof(HlslCompilationException))]
    public void CompileTest_Fail()
    {
        var faultyShader = ShaderSource.Replace("ids.x", "ids.X");

        using ComPtr<IDxcBlob> dxcBlob = ShaderCompiler.Instance.CompileShader(faultyShader.AsSpan());
    }
}

#endif
