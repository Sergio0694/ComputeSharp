namespace System;

/// <summary>
/// A polyfill type that mirrors some methods from <see cref="HashCode"/> on .NET 6.
/// </summary>
internal struct HashCode
{
    /// <summary>
    /// Combines six values into a hash code.
    /// </summary>
    /// <typeparam name="T1">The type of the first value to combine into the hash code.</typeparam>
    /// <typeparam name="T2">The type of the second value to combine into the hash code.</typeparam>
    /// <typeparam name="T3">The type of the third value to combine into the hash code.</typeparam>
    /// <typeparam name="T4">The type of the fourth value to combine into the hash code.</typeparam>
    /// <typeparam name="T5">The type of the fifth value to combine into the hash code.</typeparam>
    /// <typeparam name="T6">The type of the sixth value to combine into the hash code.</typeparam>
    /// <param name="value1">The first value to combine into the hash code.</param>
    /// <param name="value2">The second value to combine into the hash code.</param>
    /// <param name="value3">The third value to combine into the hash code.</param>
    /// <param name="value4">The fourth value to combine into the hash code.</param>
    /// <param name="value5">The fifth value to combine into the hash code.</param>
    /// <param name="value6">The sixth value to combine into the hash code.</param>
    /// <returns>The hash code that represents the six values.</returns>
    public static int Combine<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Adds a single value to the hash code.
    /// </summary>
    /// <typeparam name="T">The type of the value to add to the hash code.</typeparam>
    /// <param name="value">The value to add to the hash code.</param>
    public void Add<T>(T value)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Calculates the final hash code after consecutive <see cref="Add{T}(T)"/> invocations.
    /// </summary>
    /// <returns>The calculated hash code.</returns>
    public int ToHashCode()
    {
        throw new NotSupportedException();
    }
}