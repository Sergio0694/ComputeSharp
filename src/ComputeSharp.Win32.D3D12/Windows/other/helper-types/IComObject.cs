using System;

namespace ComputeSharp.Win32;

/// <summary>
/// An interface for a COM object, with a specific IID.
/// </summary>
internal unsafe interface IComObject
{
    /// <summary>
    /// Gets a pointer to the IID for the object type.
    /// </summary>
    static abstract Guid* IID { get; }
}