namespace ComputeSharp.NetStandard.System;

/// <summary>
/// A polyfill type that mirrors some methods from <see cref="global::System.Enum"/> on .NET 6.
/// </summary>
internal static class Enum
{
    /// <summary>
    /// Returns the name of the input enum value.
    /// </summary>
    /// <typeparam name="T">Specifies the type of the enum element.</typeparam>
    /// <param name="value">The input enum value.</param>
    /// <returns>The name of <paramref name="value"/>.</returns>
    public static string GetName<T>(T value)
        where T : unmanaged, global::System.Enum
    {
        return global::System.Enum.GetName(typeof(T), value);
    }
}