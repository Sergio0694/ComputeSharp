using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.DirectX;

namespace ComputeSharp.D2D1.Uwp.Extensions;

/// <summary>
/// A <see langword="class"/> with extensions for the <see cref="ID2D1Effect"/> type.
/// </summary>
internal static unsafe class ID2D1EffectExtensions
{
    /// <summary>
    /// Gets the <see cref="D2D1_PROPERTY.D2D1_PROPERTY_CACHED"/> property for a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <returns>The <see cref="D2D1_PROPERTY.D2D1_PROPERTY_CACHED"/> value for <paramref name="d2D1Effect"/>.</returns>
    public static bool GetCachedProperty(this ref ID2D1Effect d2D1Effect)
    {
        int cacheOutput;

        d2D1Effect.GetValue(
            index: (uint)D2D1_PROPERTY.D2D1_PROPERTY_CACHED,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BOOL,
            data: (byte*)&cacheOutput,
            dataSize: sizeof(int)).Assert();

        return cacheOutput != 0;
    }

    /// <summary>
    /// Sets the <see cref="D2D1_PROPERTY.D2D1_PROPERTY_CACHED"/> property for a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <param name="value">The property value to set.</param>
    public static void SetCachedProperty(this ref ID2D1Effect d2D1Effect, bool value)
    {
        int cacheOutput = value ? 1 : 0;

        d2D1Effect.SetValue(
            index: (uint)D2D1_PROPERTY.D2D1_PROPERTY_CACHED,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_BOOL,
            data: (byte*)&cacheOutput,
            dataSize: sizeof(int)).Assert();
    }

    /// <summary>
    /// Gets the <see cref="D2D1_PROPERTY.D2D1_PROPERTY_PRECISION"/> property for a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <returns>The <see cref="D2D1_PROPERTY.D2D1_PROPERTY_PRECISION"/> value for <paramref name="d2D1Effect"/>.</returns>
    public static D2D1_BUFFER_PRECISION GetPrecisionProperty(this ref ID2D1Effect d2D1Effect)
    {
        D2D1_BUFFER_PRECISION d2D1BufferPrecision;

        d2D1Effect.GetValue(
            index: (uint)D2D1_PROPERTY.D2D1_PROPERTY_PRECISION,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_UNKNOWN,
            data: (byte*)&d2D1BufferPrecision,
            dataSize: sizeof(D2D1_BUFFER_PRECISION)).Assert();

        return d2D1BufferPrecision;
    }

    /// <summary>
    /// Sets the <see cref="D2D1_PROPERTY.D2D1_PROPERTY_PRECISION"/> property for a given <see cref="ID2D1Effect"/> object.
    /// </summary>
    /// <param name="d2D1Effect">The input <see cref="ID2D1Effect"/> instance.</param>
    /// <param name="value">The property value to set.</param>
    public static void SetPrecisionProperty(this ref ID2D1Effect d2D1Effect, D2D1_BUFFER_PRECISION value)
    {
        d2D1Effect.SetValue(
            index: (uint)D2D1_PROPERTY.D2D1_PROPERTY_PRECISION,
            type: D2D1_PROPERTY_TYPE.D2D1_PROPERTY_TYPE_UNKNOWN,
            data: (byte*)&value,
            dataSize: sizeof(D2D1_BUFFER_PRECISION)).Assert();
    }
}