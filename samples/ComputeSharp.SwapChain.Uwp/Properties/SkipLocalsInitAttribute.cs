namespace System.Runtime.CompilerServices;

/// <summary>
/// Used to indicate to the compiler that the <c>.locals init</c> flag should not be set in method headers.
/// </summary>
[AttributeUsage(AttributeTargets.Module
    | AttributeTargets.Class
    | AttributeTargets.Struct
    | AttributeTargets.Interface
    | AttributeTargets.Constructor
    | AttributeTargets.Method
    | AttributeTargets.Property
    | AttributeTargets.Event, Inherited = false)]
internal sealed class SkipLocalsInitAttribute : Attribute
{
}