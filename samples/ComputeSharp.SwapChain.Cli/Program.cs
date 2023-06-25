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

    static HelloWorld HelloWorld(TimeSpan time) => new((float)time.TotalSeconds);
    static ColorfulInfinity ColorfulInfinity(TimeSpan time) => new((float)time.TotalSeconds);
    static FractalTiling FractalTiling(TimeSpan time) => new((float)time.TotalSeconds);
    static TwoTiledTruchet TwoTiledTruchet(TimeSpan time) => new((float)time.TotalSeconds);
    static MengerJourney MengerJourney(TimeSpan time) => new((float)time.TotalSeconds);
    static Octagrams Octagrams(TimeSpan time) => new((float)time.TotalSeconds);
    static ProteanClouds ProteanClouds(TimeSpan time) => new((float)time.TotalSeconds);
    static ExtrudedTruchetPattern ExtrudedTruchetPattern(TimeSpan time) => new((float)time.TotalSeconds);
    static PyramidPattern PyramidPattern(TimeSpan time) => new((float)time.TotalSeconds);
    static TriangleGridContouring TriangleGridContouring(TimeSpan time) => new((float)time.TotalSeconds);
    static TerracedHills TerracedHills(TimeSpan time) => new((float)time.TotalSeconds);

    Win32Application? application;

    unsafe
    {
        // Get the requested shader, if possible
        application = shaderName.ToLowerInvariant() switch
        {
            "helloworld" => new SwapChainApplication<HelloWorld>(&HelloWorld),
            "colorfulinfinity" => new SwapChainApplication<ColorfulInfinity>(&ColorfulInfinity),
            "fractaltiling" => new SwapChainApplication<FractalTiling>(&FractalTiling),
            "twotiledtruchet" => new SwapChainApplication<TwoTiledTruchet>(&TwoTiledTruchet),
            "mengerjourney" => new SwapChainApplication<MengerJourney>(&MengerJourney),
            "octagrams" => new SwapChainApplication<Octagrams>(&Octagrams),
            "proteanclouds" => new SwapChainApplication<ProteanClouds>(&ProteanClouds),
            "extrudedtruchetpattern" => new SwapChainApplication<ExtrudedTruchetPattern>(&ExtrudedTruchetPattern),
            "pyramidpattern" => new SwapChainApplication<PyramidPattern>(&PyramidPattern),
            "trianglegridcontouring" => new SwapChainApplication<TriangleGridContouring>(&TriangleGridContouring),
            "terracedhills" => new SwapChainApplication<TerracedHills>(&TerracedHills),
            _ => null
        };
    }

    // If a shader is found, run it
    if (application is not null)
    {
        return Win32ApplicationRunner.Run(application, "ComputeSharp", "ComputeSharp");
    }
}

return E.E_INVALIDARG;