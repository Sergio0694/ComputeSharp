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

    [TestMethod]
    public void MengerJourney()
    {
        RunTest<Shaders.MengerJourney>(0.000011f);
    }

    [TestMethod]
    public void Octagrams()
    {
        RunTest<Shaders.Octagrams>();
    }

    [TestMethod]
    public void ProteanClouds()
    {
        RunTest<Shaders.ProteanClouds>();
    }

    private static void RunTest<T>(float threshold = 0.00001f)
        where T : unmanaged, ID2D1PixelShader
    {
        T shader = (T)Activator.CreateInstance(typeof(T), 0f, new int2(1280, 720))!;

        D2D1TestRunner.RunAndCompareShader(in shader, 1280, 720, $"{typeof(T).Name}.png", $"{typeof(T).Name}.png", threshold);
    }
}