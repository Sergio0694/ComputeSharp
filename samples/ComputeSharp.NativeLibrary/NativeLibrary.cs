using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ComputeSharp;
using ComputeSharp.D2D1;
using ComputeSharp.D2D1.Interop;

[assembly: SupportedOSPlatform("windows6.1")]

/// <summary>
/// Native exports for the library.
/// </summary>
public static unsafe class Library
{
    /// <summary>
    /// Registers the <see cref="HelloWorld"/> effect into a target <c>ID2D1Factory1</c> instance.
    /// </summary>
    /// <param name="d2D1Factory1"><inheritdoc cref="D2D1PixelShaderEffect.RegisterForD2D1Factory1" path="/param[@name='d2D1Factory1']/node()"/></param>
    /// <param name="effectId"><inheritdoc cref="D2D1PixelShaderEffect.RegisterForD2D1Factory1" path="/param[@name='effectId']/node()"/></param>
    /// <returns>The <c>HRESULT</c> for the operation.</returns>
    [UnmanagedCallersOnly(EntryPoint = nameof(RegisterHelloWorldEffect))]
    public static int RegisterHelloWorldEffect(void* d2D1Factory1, Guid* effectId)
    {
        try
        {
            D2D1PixelShaderEffect.RegisterForD2D1Factory1<HelloWorld>(d2D1Factory1, out *effectId);

            return 0;
        }
        catch (Exception e)
        {
            return Marshal.GetHRForException(e);
        }
    }

    /// <summary>
    /// Sets the constant buffer for the input <c>ID2D1Effect</c> (assumed to be <see cref="HelloWorld"/>).
    /// </summary>
    /// <param name="d2D1Effect"><inheritdoc cref="D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect" path="/param[@name='d2D1Effect']/node()"/></param>
    /// <param name="time"><inheritdoc cref="HelloWorld(float, Int2)" path="/param[@name='time']/node()"/></param>
    /// <param name="dispatchWidth">The horizontal dispatch size for the current output.</param>
    /// <param name="dispatchHeight">The vertical dispatch size for the current output.</param>
    /// <returns></returns>
    [UnmanagedCallersOnly(EntryPoint = nameof(SetHelloWorldEffectConstantBuffer))]
    public static int SetHelloWorldEffectConstantBuffer(void* d2D1Effect, float time, int dispatchWidth, int dispatchHeight)
    {
        try
        {
            D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(d2D1Effect, new HelloWorld(time, new int2(dispatchWidth, dispatchHeight)));

            return 0;
        }
        catch (Exception e)
        {
            return Marshal.GetHRForException(e);
        }
    }
}

/// <summary>
/// A hello world effect that displays a color gradient.
/// </summary>
/// <param name="time">The current time since the start of the application.</param>
/// <param name="dispatchSize">The dispatch size for the current output.</param>
[D2DEffectDisplayName(nameof(HelloWorld))]
[D2DEffectDescription("A hello world effect that displays a color gradient.")]
[D2DEffectCategory("Render")]
[D2DEffectAuthor("ComputeSharp.D2D1")]
[D2DInputCount(0)]
[D2DRequiresScenePosition]
[D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
[D2DGeneratedPixelShaderDescriptor]
internal readonly partial struct HelloWorld(float time, int2 dispatchSize) : ID2D1PixelShader
{
    /// <inheritdoc/>
    public float4 Execute()
    {
        int2 xy = (int2)D2D.GetScenePosition().XY;
        float2 uv = xy / (float2)dispatchSize;
        float3 color = 0.5f + (0.5f * Hlsl.Cos(time + new float3(uv, uv.X) + new float3(0, 2, 4)));

        return new(color, 1f);
    }
}