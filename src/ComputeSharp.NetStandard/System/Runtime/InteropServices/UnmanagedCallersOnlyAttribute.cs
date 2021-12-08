using System;

namespace ComputeSharp.NetStandard.System.Runtime.InteropServices;

/// <summary>
/// Any method marked with <see cref="UnmanagedCallersOnlyAttribute" /> can be directly called from native code.
/// </summary>
/// <remarks>
/// This polyfill is deliberately in the wrong namespace to avoid the C# compiler failing to build below .NET 5.
/// </remarks>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class UnmanagedCallersOnlyAttribute : Attribute
{
}