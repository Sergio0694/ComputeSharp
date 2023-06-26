using System;
using ComputeSharp;
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

    // This CLI shader is heavily tuned for the smallest binary size possible when doing NativeAOT publish builds.
    // As a result, the code is intentionally not really standard, and pulling all sorts of tricks to minimize the
    // final binary size. These are callback functions to draw a shader, which are passed to the shared Win32 runner
    // as function pointers. This is done to avoid the additional binary size increase due to delegate instantiations.
    // Similarly, there is a single type hosting all rendering code to avoid per-shader reified generic instantiations.
    static void _0(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new HelloWorld((float)time.TotalSeconds));
    static void _1(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new ColorfulInfinity((float)time.TotalSeconds));
    static void _2(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new FractalTiling((float)time.TotalSeconds));
    static void _3(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new TwoTiledTruchet((float)time.TotalSeconds));
    static void _4(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new MengerJourney((float)time.TotalSeconds));
    static void _5(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new Octagrams((float)time.TotalSeconds));
    static void _6(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new ProteanClouds((float)time.TotalSeconds));
    static void _7(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new ExtrudedTruchetPattern((float)time.TotalSeconds));
    static void _8(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new PyramidPattern((float)time.TotalSeconds));
    static void _9(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new TriangleGridContouring((float)time.TotalSeconds));
    static void _A(IReadWriteNormalizedTexture2D<float4> texture, TimeSpan time) => texture.GraphicsDevice.ForEach(texture, new TerracedHills((float)time.TotalSeconds));

    unsafe
    {
        // Get the requested shader, if possible
        delegate*<IReadWriteNormalizedTexture2D<float4>, TimeSpan, void> shaderCallback = shaderName.ToLowerInvariant() switch
        {
            "helloworld" => &_0,
            "colorfulinfinity" => &_1,
            "fractaltiling" => &_2,
            "twotiledtruchet" => &_3,
            "mengerjourney" => &_4,
            "octagrams" => &_5,
            "proteanclouds" => &_6,
            "extrudedtruchetpattern" => &_7,
            "pyramidpattern" => &_8,
            "trianglegridcontouring" => &_9,
            "terracedhills" => &_A,
            _ => null
        };

        // Initialize the application instance and run it
        if (shaderCallback is not null)
        {
            Win32Application? application = new(shaderCallback);

            return Win32ApplicationRunner.Run(application, "ComputeSharp", "ComputeSharp");
        }
    }
}

return E.E_INVALIDARG;