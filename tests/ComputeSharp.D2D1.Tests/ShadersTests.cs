using System;
using ComputeSharp.D2D1.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("Shaders")]
public class ShadersTests
{
    [TestMethod]
    public void HelloWorld()
    {
        RunTest<Shaders.HelloWorld>();
    }

    [TestMethod]
    public void ColorfulInfinity()
    {
        RunTest<Shaders.ColorfulInfinity>();
    }

    [TestMethod]
    public void FractalTiling()
    {
        RunTest<Shaders.FractalTiling>();
    }

    private static void RunTest<T>()
        where T : unmanaged, ID2D1PixelShader
    {
        T shader = (T)Activator.CreateInstance(typeof(T), 0f, new int2(1280, 720))!;

        D2D1TestRunner.RunAndCompareShader(in shader, 1280, 720, $"{typeof(T).Name}.png", $"{typeof(T).Name}.png");
    }
}