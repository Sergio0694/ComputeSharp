// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace ComputeSharp.NetStandard.System.Runtime.InteropServices; // This cannot be in System.Runtime.InteropServices, or Roslyn will recognize it

/// <summary>
/// Any method marked with <see cref="UnmanagedCallersOnlyAttribute" /> can be directly called from native code.
/// </summary>
/// <remarks>
/// This polyfill is deliberately in the wrong namespace to avoid the C# compiler failing to build below .NET 5.
/// </remarks>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
internal sealed class UnmanagedCallersOnlyAttribute : Attribute
{
}