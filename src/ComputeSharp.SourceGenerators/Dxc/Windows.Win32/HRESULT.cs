using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Windows.Win32.Foundation;

/// <inheritdoc/>
partial struct HRESULT
{
    /// <summary>
    /// Throws a <see cref="Win32Exception"/> if the current value represents an error.
    /// </summary>
    /// <exception cref="Win32Exception">Thrown if the current value represents an error.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Assert()
    {
        if (Failed)
        {
            throw new Win32Exception(this.Value);
        }
    }
}