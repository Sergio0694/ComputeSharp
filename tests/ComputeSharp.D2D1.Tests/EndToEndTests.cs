using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Tests.Effects;
using ComputeSharp.D2D1.Tests.Helpers;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("EndToEnd")]
public class EndToEndTests
{
    [TestMethod]
    public unsafe void InvertWithCustomThreshold()
    {
        RunAndCompareShader(new InvertEffect(1), null, "Landscape.png", "Landscape_Inverted.png");
    }

    [TestMethod]
    public unsafe void Pixelate()
    {
        RunAndCompareShader(
            new PixelateEffect.Shader(new PixelateEffect.Shader.Constants(1280, 720, 16)),
            static () => new PixelateEffect(),
            "Landscape.png",
            "Landscape_Pixelate.png");
    }

    /// <summary>
    /// Executes a pixel shader and compares the expected results.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to run.</typeparam>
    /// <param name="originalFileName">The name of the source image.</param>
    /// <param name="transformMapperFactory">A custom <see cref="ID2D1TransformMapper{T}"/> factory for the effect.</param>
    /// <param name="expectedFileName">The name of the expected result image.</param>
    /// <param name="destinationFileName">The name of the destination image to save results to.</param>
    /// <param name="shader">The shader to run.</param>
    private static void RunAndCompareShader<T>(
        in T shader,
        Func<ID2D1TransformMapper<T>>? transformMapperFactory,
        string originalFileName,
        string expectedFileName,
        [CallerMemberName] string destinationFileName = "")
        where T : unmanaged, ID2D1PixelShader
    {
        string assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Assets");
        string temporaryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "temp");

        _ = Directory.CreateDirectory(temporaryPath);

        string originalPath = Path.Combine(assetsPath, originalFileName);
        string expectedPath = Path.Combine(assetsPath, expectedFileName);
        string destinationPath = Path.Combine(temporaryPath, $"{destinationFileName}.png");

        // Run the shader
        D2D1TestRunner.ExecutePixelShaderAndCompareResults(
            in shader,
            transformMapperFactory,
            originalPath,
            destinationPath);

        // Compare the results
        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }
}