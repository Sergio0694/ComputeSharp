using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using ComputeSharp.D2D1Interop.Tests.Helpers;
using ComputeSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.D2D1Interop.Tests;

[TestClass]
[TestCategory("EndToEnd")]
public class EndToEndTests
{
    [TestMethod]
    public unsafe void InvertWithCustomThreshold()
    {
        RunAndCompareShader(new InvertShader(1), "Landscape.png", "Landscape_Inverted.png");
    }

    [TestMethod]
    public unsafe void Pixelate()
    {
        RunAndCompareShader(new PixelateShader(new PixelateShader.Constants(1280, 720, 16)), "Landscape.png", "Landscape_Pixelate.png");
    }

    /// <summary>
    /// Executes a pixel shader and compares the expected results.
    /// </summary>
    /// <typeparam name="T">The type of pixel shader to run.</typeparam>
    /// <param name="originalFileName">The name of the source image.</param>
    /// <param name="expectedFileName">The name of the expected result image.</param>
    /// <param name="destinationFileName">The name of the destination image to save results to.</param>
    /// <param name="shader">The shader to run.</param>
    private static void RunAndCompareShader<T>(
        in T shader,
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
        D2D1ShaderTestHelper.ExecutePixelShaderAndCompareResults(
            originalPath,
            destinationPath,
            in shader);

        // Compare the results
        TolerantImageComparer.AssertEqual(destinationPath, expectedPath, 0.00001f);
    }
}

[D2DInputCount(1)]
[D2DInputSimple(0)]
[D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader50)]
[AutoConstructor]
public partial struct InvertShader : ID2D1PixelShader
{
    public float number;

    public float4 Execute()
    {
        float4 color = D2D1.GetInput(0);
        float3 rgb = Hlsl.Saturate(this.number - color.RGB);

        return new(rgb, 1);
    }
}

[D2DInputCount(1)]
[D2DInputComplex(0)]
[D2DRequiresScenePosition]
[D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader40)]
[AutoConstructor]
public partial struct PixelateShader : ID2D1PixelShader
{
    [AutoConstructor]
    public partial struct Constants
    {
        public readonly int inputWidth;
        public readonly int inputHeight;
        public readonly int cellSize;
    }

    private readonly Constants constants; 

    public float4 Execute()
    {
        float2 scenePos = D2D1.GetScenePosition().XY;
        uint x = (uint)Hlsl.Floor(scenePos.X);
        uint y = (uint)Hlsl.Floor(scenePos.Y);

        int cellX = (int)Hlsl.Floor(x / this.constants.cellSize);
        int cellY = (int)Hlsl.Floor(y / this.constants.cellSize);

        int x0 = cellX * this.constants.cellSize;
        int y0 = cellY * this.constants.cellSize;

        int x1 = Hlsl.Min(this.constants.inputWidth, x0 + this.constants.cellSize) - 1;
        int y1 = Hlsl.Min(this.constants.inputHeight, y0 + this.constants.cellSize) - 1;

        float4 sample0 = D2D1.SampleInputAtPosition(0, new int2(x0, y0));
        float4 sample1 = D2D1.SampleInputAtPosition(0, new int2(x1, y0));
        float4 sample2 = D2D1.SampleInputAtPosition(0, new int2(x0, y1));
        float4 sample3 = D2D1.SampleInputAtPosition(0, new int2(x1, y1));

        float4 color = (sample0 + sample1 + sample2 + sample3) / 4;

        return color;
    }
}