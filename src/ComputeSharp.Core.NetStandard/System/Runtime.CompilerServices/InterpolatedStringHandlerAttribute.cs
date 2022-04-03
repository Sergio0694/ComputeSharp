namespace System.Runtime.CompilerServices;

/// <summary>
/// Indicates the attributed type is to be used as an interpolated string handler.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public sealed class InterpolatedStringHandlerAttribute : Attribute
{
}