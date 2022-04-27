using System;
using System.Drawing;

namespace ComputeSharp.D2D1.Tests.Effects;

public sealed partial class PixelateEffect : ID2D1TransformMapper<PixelateEffect.Shader>
{
    /// <inheritdoc/>
    void ID2D1TransformMapper<Shader>.MapInputsToOutput(in Shader shader, ReadOnlySpan<Rectangle> inputs, ReadOnlySpan<Rectangle> opaqueInputs, out Rectangle output, out Rectangle opaqueOutput)
    {
        output = inputs[0];
        opaqueOutput = Rectangle.Empty;
    }

    /// <inheritdoc/>
    void ID2D1TransformMapper<Shader>.MapInvalidOutput(int inputIndex, Rectangle invalidInput, out Rectangle invalidOutput)
    {
        invalidOutput = invalidInput;
    }

    /// <inheritdoc/>
    void ID2D1TransformMapper<Shader>.MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
    {
        inputs.Fill(output);
    }

    [D2DInputCount(1)]
    [D2DInputComplex(0)]
    [D2DRequiresScenePosition]
    [D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader40)]
    [AutoConstructor]
    public partial struct Shader : ID2D1PixelShader
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
            float2 scenePos = D2D.GetScenePosition().XY;
            uint x = (uint)Hlsl.Floor(scenePos.X);
            uint y = (uint)Hlsl.Floor(scenePos.Y);

            int cellX = (int)Hlsl.Floor(x / this.constants.cellSize);
            int cellY = (int)Hlsl.Floor(y / this.constants.cellSize);

            int x0 = cellX * this.constants.cellSize;
            int y0 = cellY * this.constants.cellSize;

            int x1 = Hlsl.Min(this.constants.inputWidth, x0 + this.constants.cellSize) - 1;
            int y1 = Hlsl.Min(this.constants.inputHeight, y0 + this.constants.cellSize) - 1;

            float4 sample0 = D2D.SampleInputAtPosition(0, new int2(x0, y0));
            float4 sample1 = D2D.SampleInputAtPosition(0, new int2(x1, y0));
            float4 sample2 = D2D.SampleInputAtPosition(0, new int2(x0, y1));
            float4 sample3 = D2D.SampleInputAtPosition(0, new int2(x1, y1));

            float4 color = (sample0 + sample1 + sample2 + sample3) / 4;

            return color;
        }
    }
}