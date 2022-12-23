// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// Specifies that null is disallowed as an input even if the corresponding type allows it.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
internal sealed class DisallowNullAttribute : Attribute
{
}