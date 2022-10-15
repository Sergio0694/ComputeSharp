namespace ComputeSharp.D2D1.Interop;

/// <summary>
/// A model representing a property binding for an effect.
/// </summary>
public readonly unsafe struct D2D1PropertyBinding
{
    /// <summary>
    /// Creates a new <see cref="D2D1PropertyBinding"/> instance with the specified parameters.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <param name="getFunction">A pointer to the property get function callback</param>
    /// <param name="setFunction">A pointer to the property set function callback</param>
    internal D2D1PropertyBinding(string name, void* getFunction, void* setFunction)
    {
        PropertyName = name;
        GetFunction = getFunction;
        SetFunction = setFunction;
    }

    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// Gets the property get function (a <see langword="delegate* unmanaged[Stdcall]&lt;IUnknown*, byte*, uint, uint*, HRESULT&gt;"/>).
    /// </summary>
    public void* GetFunction { get; }

    /// <summary>
    /// Gets the property set function (a <see langword="delegate* unmanaged[Stdcall]&lt;IUnknown*, byte*, uint, HRESULT&gt;"/>).
    /// </summary>
    public void* SetFunction { get; }
}