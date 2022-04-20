﻿using System;

namespace ComputeSharp.D2D1.Tests.Effects;

[D2DInputCount(0)]
[D2DRequiresScenePosition]
[D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader50)]
[AutoConstructor]
public readonly partial struct ZonePlateEffect : ID2D1PixelShader
{
    private readonly int width;
    private readonly int height;
    private readonly float diameter;

    /// <inheritdoc/>
    public float4 Execute()
    {
        float2 scenePos = D2D.GetScenePosition().XY;

        float xo = scenePos.X - (this.width >> 1);
        float yo = scenePos.Y - (this.height >> 1);

        float rm = 0.5f * this.diameter;
        float km = 0.7f / this.diameter * MathF.PI;
        float w = rm / 10.0f;

        float yd = yo * yo;
        float xd = xo * xo;
        float d = xd + yd;
        float v = 1.0f + (1.0f + Hlsl.Tanh((rm - Hlsl.Sqrt(d)) / w)) * Hlsl.Sin(km * d) * 0.5f;

        return new Float4(v, v, v, 1.0f);
    }
}
