namespace ComputeSharp.SwapChain.Shaders;

/// <summary>
/// Draws Mandelbrot set
/// </summary>
[AutoConstructor]
#if SAMPLE_APP
[EmbeddedBytecode(DispatchAxis.XY)]
#endif
internal readonly partial struct Mandelbrot : IPixelShader<float4>
{
    /// <summary>
    /// The current time since the start of the application.
    /// </summary>
    private readonly float time;

    /// <summary>
    /// The current time Hlsl.Since the start of the application.
    /// </summary>
    private readonly float4 startColor;

    /// <summary>
    /// The current time Hlsl.Since the start of the application.
    /// </summary>
    private readonly float4 endColor;

    private const int MaxIterations = 400;

    /// <inheritdoc/>
    public float4 Execute()
    {
        // Change coordinate system so (0,0) is center of the scree.
        Int2 uv = ThreadIds.XY - (DispatchSize.XY / 2);
        //double2 center = new(0.3006818, 0.6600910);
        double2 center = new(-0.55113284371278159, 0.62783963563277212);
        // Increase zoom as time passes.
        double zoomFactor = this.time * 10;
        double2 zoom = new double2(0.00375, 0.00375) * new double2(zoomFactor, zoomFactor);
        double2 point = center + (zoom * uv / DispatchSize.XY);

        int i = CalculateIterations(point.X, point.Y);
        if (i > MaxIterations - 1)
        {
            return new(0.0f, 0.0f, 0.0f, 1.0f);
        }

        // Color pixel based on count of iterations.
        float c = Hlsl.Sqrt((float)i * 4 / MaxIterations);
        return (this.startColor * c / 600) + (this.endColor * (1 - c));
    }

    private static int CalculateIterations(double real, double imag)
    {
        int limit = MaxIterations;
        double zReal = real;
        double zImag = imag;

        for (int i = 0; i < limit; ++i)
        {
            double r2 = zReal * zReal;
            double i2 = zImag * zImag;

            if (r2 + i2 > 4.0)
            {
                return i;
            }

            zImag = (2.0f * zReal * zImag) + imag;
            zReal = r2 - i2 + real;
        }

        return limit;
    }
}