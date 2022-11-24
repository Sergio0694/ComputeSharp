using System;
using ComputeSharp.SwapChain.Backend;
using ComputeSharp.SwapChain.Shaders;
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
    Win32Application? application = shaderName.ToLowerInvariant() switch
    {
        "helloworld" => new SwapChainApplication<HelloWorld>(static time => new((float)time.TotalSeconds)),
        "colorfulinfinity" => new SwapChainApplication<ColorfulInfinity>(static time => new((float)time.TotalSeconds)),
        "fractaltiling" => new SwapChainApplication<FractalTiling>(static time => new((float)time.TotalSeconds)),
        "twotiledtruchet" => new SwapChainApplication<TwoTiledTruchet>(static time => new((float)time.TotalSeconds)),
        "mengerjourney" => new SwapChainApplication<MengerJourney>(static time => new((float)time.TotalSeconds)),
        "octagrams" => new SwapChainApplication<Octagrams>(static time => new((float)time.TotalSeconds)),
        "proteanclouds" => new SwapChainApplication<ProteanClouds>(static time => new((float)time.TotalSeconds)),
        "extrudedtruchetpattern" => new SwapChainApplication<ExtrudedTruchetPattern>(static time => new((float)time.TotalSeconds)),
        "pyramidpattern" => new SwapChainApplication<PyramidPattern>(static time => new((float)time.TotalSeconds)),
        "trianglegridcontouring" => new SwapChainApplication<TriangleGridContouring>(static time => new((float)time.TotalSeconds)),
        "terracedhills" => new SwapChainApplication<TerracedHills>(static time => new((float)time.TotalSeconds)),
        _ => null
    };

    // If a shader is found, run it
    if (application is not null)
    {
        return Win32ApplicationRunner.Run(application, "ComputeSharp", "ComputeSharp");
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
            - {nameof(ExtrudedTruchetPattern)}
            - {nameof(PyramidPattern)}
            - {nameof(TriangleGridContouring)}
            - {nameof(TerracedHills)}
            """);

        return S.S_OK;
    }
}

return E.E_INVALIDARG;