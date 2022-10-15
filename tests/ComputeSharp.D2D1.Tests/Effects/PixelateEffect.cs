namespace ComputeSharp.D2D1.Tests.Effects;

[D2DInputCount(1)]
[D2DInputComplex(0)]
[D2DRequiresScenePosition]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader40)]
[AutoConstructor]
public partial struct PixelateEffect : ID2D1PixelShader
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

        float4 sample0 = D2D.SampleInputAtPosition(0, new float2(x0 + 0.5f, y0 + 0.5f));
        float4 sample1 = D2D.SampleInputAtPosition(0, new float2(x1 + 0.5f, y0 + 0.5f));
        float4 sample2 = D2D.SampleInputAtPosition(0, new float2(x0 + 0.5f, y1 + 0.5f));
        float4 sample3 = D2D.SampleInputAtPosition(0, new float2(x1 + 0.5f, y1 + 0.5f));

        float4 color = (sample0 + sample1 + sample2 + sample3) / 4;

        return color;
    }
}