namespace ComputeSharp.SwapChain.Shaders;

/// <summary>
/// A shader creating a flythrough in a Menger system.
/// Ported from <see href="https://www.shadertoy.com/view/Mdf3z7"/>.
/// <para>Created by Syntopia.</para>
/// </summary>
[AutoConstructor]
[EmbeddedBytecode(DispatchAxis.XY)]
internal readonly partial struct MengerJourney : IPixelShader<float4>
{
    /// <summary>
    /// The current time since the start of the application.
    /// </summary>
    private readonly float time;

    private const int MaxSteps = 30;
    private const float MinimumDistance = 0.0009f;
    private const float NormalDistance = 0.0002f;
    private const int Iterations = 7;
    private const float Scale = 3.0f;
    private const float FieldOfView = 1.0f;
    private const float Jitter = 0.05f;
    private const float FudgeFactor = 0.7f;
    private const float NonLinearPerspective = 2.0f;
    private const float Ambient = 0.32184f;
    private const float Diffuse = 0.5f;

    private static readonly float3 LightDir = 1.0f;
    private static readonly float3 LightColor = new(1.0f, 1.0f, 0.858824f);
    private static readonly float3 LightDir2 = new(1.0f, -1.0f, 1.0f);
    private static readonly float3 LightColor2 = new(0, 0.333333f, 1.0f);
    private static readonly float3 Offset = new(0.92858f, 0.92858f, 0.32858f);

    /// <summary>
    /// Rotates a given <see cref="float2"/> value by a specified amount.
    /// </summary>
    private static float2 Rotate(float2 v, float a)
    {
        return new((Hlsl.Cos(a) * v.X) + (Hlsl.Sin(a) * v.Y), (-Hlsl.Sin(a) * v.X) + (Hlsl.Cos(a) * v.Y));
    }

    /// <summary>
    /// Calculates the light at a specified normal and starting color.
    /// Uses two light sources and no specular lights.
    /// </summary>
    private static float3 GetLight(in float3 color, in float3 normal)
    {
        float3 lightDir = Hlsl.Normalize(LightDir);
        float diffuse = Hlsl.Max(0.0f, Hlsl.Dot(-normal, lightDir));

        float3 lightDir2 = Hlsl.Normalize(LightDir2);
        float diffuse2 = Hlsl.Max(0.0f, Hlsl.Dot(-normal, lightDir2));

        return
            (diffuse * Diffuse * (LightColor * color)) +
            (diffuse2 * Diffuse * (LightColor2 * color));
    }

    /// <summary>
    /// DE: Infinitely tiled Menger IFS.
    /// For more info on KIFS, see <a href="http://www.fractalforums.com/3d-fractal-generation/kaleidoscopic-%28escape-time-ifs%29/"/>
    /// </summary>
    private float DE(float3 z)
    {
        // Performs the GLSL mod function.
        static float3 Mod(float3 x, float y)
        {
            return x - (y * Hlsl.Floor(x / y));
        }

        z = Hlsl.Abs(1.0f - Mod(z, 2.0f));

        float d = 1000.0f;

        for (int n = 0; n < Iterations; n++)
        {
            z.XY = Rotate(z.XY, 4.0f + (2.0f * Hlsl.Cos(this.time / 8.0f)));
            z = Hlsl.Abs(z);

            if (z.X < z.Y) { z.XY = z.YX; }

            if (z.X < z.Z) { z.XZ = z.ZX; }

            if (z.Y < z.Z) { z.YZ = z.ZY; }

            z = (Scale * z) - (Offset * (Scale - 1.0f));

            if (z.Z < -0.5f * Offset.Z * (Scale - 1.0f))
            {
                z.Z += Offset.Z * (Scale - 1.0f);
            }

            d = Hlsl.Min(d, Hlsl.Length(z) * Hlsl.Pow(Scale, -n - 1.0f));
        }

        return d - 0.001f;
    }

    /// <summary>
    /// Calculates the finite difference normal at a given position.
    /// </summary>
    private float3 GetNormal(in float3 pos)
    {
        float3 e = new(0.0f, NormalDistance, 0.0f);

        return Hlsl.Normalize(new float3(
            DE(pos + e.YXX) - DE(pos - e.YXX),
            DE(pos + e.XYX) - DE(pos - e.XYX),
            DE(pos + e.XXY) - DE(pos - e.XXY)));
    }

    /// <summary>
    /// Performs a ray march operation on a specific position.
    /// </summary>
    private float4 RayMarch(in float3 from, float3 dir, in float2 fragCoord)
    {
        // Computes a pseudo-random number
        static float Rand(float2 co)
        {
            return Hlsl.Frac(Hlsl.Cos(Hlsl.Dot(co, new float2(4.898f, 7.23f))) * 23421.631f);
        }

        float totalDistance = Jitter * Rand(fragCoord.XY + this.time);
        float3 dir2 = dir;
        float distance = 0;
        int steps = 0;
        float3 pos = 0;

        for (int i = 0; i < MaxSteps; i++)
        {
            dir.ZY = Rotate(dir2.ZY, totalDistance * Hlsl.Cos(this.time / 4.0f) * NonLinearPerspective);
            pos = from + (totalDistance * dir);
            distance = DE(pos) * FudgeFactor;
            totalDistance += distance;

            if (distance < MinimumDistance)
            {
                break;
            }

            steps = i;
        }

        float smoothStep = steps + (distance / MinimumDistance);
        float ao = 1.1f - (smoothStep / MaxSteps);
        float3 normal = GetNormal(pos - (dir * NormalDistance * 3.0f));
        float3 color = 1.0f;
        float3 light = GetLight(color, normal);

        color = ((color * Ambient) + light) * ao;

        return new(color.X, color.Y, color.Z, 1.0f);
    }

    /// <inheritdoc/>
    public float4 Execute()
    {
        float3 camPos = 0.5f * this.time * new float3(1.0f, 0.0f, 0.0f);
        float3 target = camPos + new float3(1.0f, 0.0f * Hlsl.Cos(this.time), 0.0f * Hlsl.Sin(0.4f * this.time));
        float3 camDir = Hlsl.Normalize(target - camPos);
        float3 camUp = new(0.0f, 1.0f, 0.0f);

        camUp = Hlsl.Normalize(camUp - (Hlsl.Dot(camDir, camUp) * camDir));

        float3 camRight = Hlsl.Normalize(Hlsl.Cross(camDir, camUp));
        float2 coord = -1.0f + (2.0f * (float2)ThreadIds.XY / DispatchSize.XY);

        coord.X *= (float)DispatchSize.X / DispatchSize.Y;

        float3 rayDir = Hlsl.Normalize(camDir + (((coord.X * camRight) + (coord.Y * camUp)) * FieldOfView));

        return RayMarch(camPos, rayDir, ThreadIds.XY);
    }
}