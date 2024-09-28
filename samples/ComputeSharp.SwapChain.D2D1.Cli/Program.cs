using System;
using ComputeSharp.SwapChain.D2D1.Backend;
using ComputeSharp.SwapChain.Shaders.D2D1;
using TerraFX.Interop.Windows;

// Accepted inputs:
//   - []
//   - [<SHADER_NAME>]
if (args is [] or [_])
{
    // If there are no args, just run the default shader
    if (args is not [string shaderName])
    {
        shaderName = nameof(ColorfulInfinity);
    }

    // Get the requested shader, if possible
    PixelShaderEffect? effect = shaderName.ToLowerInvariant() switch
    {
        "helloworld" => new PixelShaderEffect.For<HelloWorld>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "colorfulinfinity" => new PixelShaderEffect.For<ColorfulInfinity>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "fractaltiling" => new PixelShaderEffect.For<FractalTiling>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "twotiledtruchet" => new PixelShaderEffect.For<TwoTiledTruchet>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "mengerjourney" => new PixelShaderEffect.For<MengerJourney>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "octagrams" => new PixelShaderEffect.For<Octagrams>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "proteanclouds" => new PixelShaderEffect.For<ProteanClouds>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "pyramidpattern" => new PixelShaderEffect.For<PyramidPattern>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "trianglegridcontouring" => new PixelShaderEffect.For<TriangleGridContouring>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        "terracedhills" => new PixelShaderEffect.For<TerracedHills>(static (time, width, height) => new((float)time.TotalSeconds, new int2(width, height))),
        _ => null
    };

    // If a shader is found, run it
    if (effect is not null)
    {
        Win32Application application = new();

        application.Draw += (_, e) =>
        {
            // Set the effect properties
            effect.ElapsedTime = e.TotalTime;
            effect.ScreenWidth = (int)e.ScreenWidth;
            effect.ScreenHeight = (int)e.ScreenHeight;

            // Draw the effect
            e.DrawingSession.DrawImage(effect);
        };

        Win32ApplicationRunner.Run(application, "ComputeSharp.D2D1");

        return S.S_OK;
    }

    // If no shader matches, check if help was requested
    if (shaderName is "-h")
    {
        Console.WriteLine($"""
            Available shaders:

            - {nameof(HelloWorld)}
            - {nameof(ColorfulInfinity)}
            - {nameof(FractalTiling)}
            - {nameof(TwoTiledTruchet)}
            - {nameof(MengerJourney)}
            - {nameof(Octagrams)}
            - {nameof(ProteanClouds)}
            - {nameof(PyramidPattern)}
            - {nameof(TriangleGridContouring)}
            - {nameof(TerracedHills)}
            """);

        return S.S_OK;
    }
}

return E.E_INVALIDARG;