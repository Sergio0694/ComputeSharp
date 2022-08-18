using ComputeSharp.D2D1;

[assembly: D2DShaderProfile(D2D1ShaderProfile.PixelShader41)]
[assembly: D2DCompileOptions(D2D1CompileOptions.IeeeStrictness | D2D1CompileOptions.OptimizationLevel2 | D2D1CompileOptions.PartialPrecision)]