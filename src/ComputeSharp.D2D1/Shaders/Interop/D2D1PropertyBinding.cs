namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A model representing a property binding for an effect.
/// </summary>
public unsafe readonly struct D2D1PropertyBinding
{
    /// <summary>
    /// Creates a new <see cref="D2D1PropertyBinding"/> instance with the specified parameters.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="getter">A pointer to the property getter callback</param>
    /// <param name="setter">A pointer to the property setter callback</param>
    internal D2D1PropertyBinding(string name, void* getter, void* setter)
    {
        Name = name;
        Getter = getter;
        Setter = setter;
    }

    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the property getter (a <see langword="delegate* unmanaged[Stdcall]&lt;IUnknown*, byte*, uint, uint*, HRESULT&gt;"/>).
    /// </summary>
    public void* Getter { get; }

    /// <summary>
    /// Gets the property setter (a <see langword="delegate* unmanaged[Stdcall]&lt;IUnknown*, byte*, uint, HRESULT&gt;"/>).
    /// </summary>
    public void* Setter { get; }
}