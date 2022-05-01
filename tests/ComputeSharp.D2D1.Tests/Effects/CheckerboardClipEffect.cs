namespace ComputeSharp.D2D1.Tests.Effects;

[D2DInputCount(1)]
[D2DInputSimple(0)]
[D2DRequiresScenePosition]
[D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader50)]
[AutoConstructor]
public partial struct CheckerboardClipEffect : ID2D1PixelShader
{
    public int width;
    public int height;
    public int cellSize;

    /// <inheritdoc/>
    public float4 Execute()
    {
        float2 position = D2D.GetScenePosition().XY;
        uint x = (uint)Hlsl.Floor(position.X);
        uint y = (uint)Hlsl.Floor(position.Y);

        uint cellX = (uint)(int)Hlsl.Floor(x / cellSize);
        uint cellY = (uint)(int)Hlsl.Floor(y / cellSize);

        if ((cellX % 2 == 0 && cellY % 2 == 0) ||
            (cellX % 2 == 1 && cellY % 2 == 1))
        {
            Hlsl.Clip(-1);
        }

        return D2D.GetInput(0);
    }
}
